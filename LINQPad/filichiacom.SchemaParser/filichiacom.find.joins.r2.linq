<Query Kind="Program">
  <Reference>&lt;ProgramFilesX86&gt;\Microsoft.NET\ADOMD.NET\110\Microsoft.AnalysisServices.AdomdClient.dll</Reference>
  <Reference>&lt;ProgramFilesX86&gt;\Microsoft SQL Server\110\SDK\Assemblies\Microsoft.AnalysisServices.DLL</Reference>
  <Reference Relative="..\..\SkyDrive\Assemblies\SchemaParser\bin\Release\SchemaParser.dll">C:\Users\joeyf\SkyDrive\Assemblies\SchemaParser\bin\Release\SchemaParser.dll</Reference>
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>filichiacom</Namespace>
  <Namespace>Microsoft.AnalysisServices</Namespace>
  <Namespace>Microsoft.AnalysisServices.AdomdClient</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
	SchemaParser.oDataBase OlapData = new SchemaParser.oDataBase(XElement.Load(@"c:\Projects\SSAS\BankerSchema.xml"));
	String mdxregex = @"\[[^\]]*\](\.(&)?\[[^\]]*\])*";
	
	string mdxTupleRegEx = @"\([^\)]*\)";
	string mdxSetRegEx = @"\{[^\}]*\}";
	
	var cubeName = "DDA";
	
	  var filterList = new[]{"[Calendar Period].[Calendar Period].[Year].&[2011]"};
var columnList= new[]{
	"[Measures].[Product Count]",
	"[Measures].[Current Balance]",
	"[Products].[Products].[Product Name]",
	"[Customers].[Customer Names].[Customer Name]",
	"[Organization].[Organization Region].[Branch]",
	"[Accounts].[Accounts]"
	};
var rowList	= new[]{
	"[Products].[Products].[Product Type].&[Deposits]",
	"[Customers].[Officer Portfolio].[Officer Name]",
	"[Products].[Products].[Product Family]"
	};
	
	            #region Build Source objects

            //get expressions from tables
            var dataTableExpressions = SchemaParser.GetDataSourceViewExpressions(OlapData);
            var dataTableDefinitions = SchemaParser.GetDataSourceViewDefinitions(OlapData);

            /*
            var ExpressionSet =
                dataTableExpressions.SelectMany(
                    dsEx => dsEx.ExpressionTree.Where(tree => !String.IsNullOrEmpty(tree.Expression)))
                                    .Select(tree => new {tree.TableName, tree.ColumnName, tree.Expression});
            */

            //Get the DataSourceViewId from the current Cube
            String DataSourceViewId = OlapData.Cubes.First(cub => cub.Name.Equals(cubeName)).Source;

            //Build a lookup for the Table Joins
            ILookup<string, string> cubeSourceTablePkLookup = PrimaryKeyLookup(cubeName);

            //Build a set of Data Relation Objects
            IEnumerable<string> sourceSet =
                filterList.Union(columnList.Union(rowList)).Where(ss => !String.IsNullOrEmpty(ss)).ToArray();

            //Build a Temp Object to hold the contents of the members
            IEnumerable<SchemaParser.TableColumn> measureSet = SchemaParser.BuildMeasureSet(OlapData, sourceSet,
                                                                                            cubeName).ToArray();

            var msrDef = SchemaParser.GetMeasureDefinitions(OlapData).Where(cNm => cNm.CubeName.Equals(cubeName)).ToArray();
            var mbrSet = SchemaParser.BuildMemberSet(sourceSet).ToArray();
            var hrySet = SchemaParser.GetHierarchySet(OlapData, cubeName).ToArray();
            var relSet = SchemaParser.GetRelationSet(OlapData, DataSourceViewId).ToArray();


            //Build a definition of the members that are not measures...
            var memberItemDefinitions = SchemaParser.GetMemberItemDefinitions(
                OlapData,
                SchemaParser.GetMeasureDefinitions(OlapData).Where(cNm => cNm.CubeName.Equals(cubeName)),
                SchemaParser.BuildMemberSet(sourceSet),
                SchemaParser.GetHierarchySet(OlapData, cubeName),
                SchemaParser.GetRelationSet(OlapData, DataSourceViewId)).ToArray();


            //Get the names of the tables
            var memberTables = memberItemDefinitions
                .Select(mDef => SchemaParser.GetDbTableName(OlapData, FixTableName(mDef.TableName)))
                .Distinct()
                .ToArray();

            #endregion
            
            #region Build Context Joins

            //built an array to hold the joins...
            var joins = memberItemDefinitions
                //Removed the filtering function to ensure we get all joins. Since the expression parser
                //will handle expressions instead of columns, this will be handled with its implementation
                //.Where(r => r.DataType.Equals("Integer") || r.DataType.Equals("BigInt"))
                .ToArray()
                .SelectMany(r => r.Relations
                                  .Select(rel =>
                                          new
                                              {
                                                  Right =
                                              rel.ChildColumns.Select(cc => new {cc.TableName, cc.ColumnName}),
                                                  Left =
                                              rel.ParentColumns.Select(cc => new {cc.TableName, cc.ColumnName})
                                              })
                )
                .Select(tb =>
                        new
                            {
                                LeftTable =
                            SchemaParser.GetDbTableName(OlapData, FixTableName(tb.Left.First().TableName)),
                                LeftColumn = tb.Left.First().ColumnName,
                                RightTable =
                            SchemaParser.GetDbTableName(OlapData, FixTableName(tb.Right.First().TableName)),
                                RightColumn = tb.Right.First().ColumnName,
                            })
                .Distinct()
                .ToArray();


            //Get all tables with a direct join to the measures
            var measureJoins =
                joins.Where(MeasureJoin => measureSet.Select(ms => ms.TableName).Contains(MeasureJoin.RightTable))
                     .ToArray();

            //Get all the tables that joins were not found for.
            var missingTables = memberTables.Except(measureJoins.Select(mj => mj.LeftTable)).ToArray();

            //Get the tables for linking the missing tables to known joins
            var linkingTables =
                joins.Where(js => missingTables.Contains(js.LeftTable)).Select(js => js.RightTable).ToArray();

            //get the joins for the tables that are missing and attach them to the objects...
            var joinSet = joins
                .Where(js => linkingTables.Contains(js.RightTable))
                .Union(measureJoins)
                .Select(js => new TableJoin
                    {

                        LeftTable = FixTableName(js.LeftTable),
                        LeftColumns = new[] {js.LeftColumn},
                        RightTable = FixTableName(js.RightTable),
                        RightColumns = new[] {js.RightColumn}
                    }).ToArray();

            #endregion
            
            #region Build Context tables

            //Get a simplified list of member definitions, defined similar to the 
            //object they will populate
            var memberDefinitions = memberItemDefinitions.Select(tbl => new
            {
                Caption = CountMeasures.Any(msr => msr.Name.Contains(tbl.Hierarchy))
								? tbl.Hierarchy
								: tbl.ColumnName,
                DisplayColumn = CountMeasures.Any(msr => msr.Name.Contains(tbl.Hierarchy))
                                ? null
                                : tbl.ColumnName,
                TableName = SchemaParser.GetDbTableName(OlapData, FixTableName(tbl.TableName)),
                Value = tbl.Value,
                CanFilter = !String.IsNullOrEmpty(tbl.Value),
                IsKeyColumn = tbl.IsPrimaryKey,
                Expression = CountMeasures.Any(msr => msr.Name.Contains(tbl.Hierarchy))
                            ? "1"
                            : tbl.ExpressionStatement
            })
            //.DistinctBy(tbl => new { tbl.TableName, tbl.DisplayColumn })
            .ToArray();

            //Get an array of Key Columns
            var keyColumnArray = memberItemDefinitions
                .Where(
                    miDef => miDef.IsPrimaryKey && miDef.DataType.Equals("Integer") || miDef.DataType.Equals("BigInt"))
                .DistinctBy(dc => String.Format("{0}.{1}", dc.TableName, dc.ColumnName))
                .Select(tt => new
                    {
                        TableName = SchemaParser.GetDbTableName(OlapData, FixTableName(tt.TableName)),
                        tt.ColumnName
                    });

            //Create a Dictionary of table definitions of the members
            var tableLookup = memberDefinitions
                .Select(ts => new
                    {
                        TableName = FixTableName(ts.TableName),
                        KeyColumn = keyColumnArray
                                        .Where(tn => tn.TableName.Equals(ts.TableName))
                                        .Select(dcol => dcol.ColumnName)
                                        .FirstOrDefault() ?? String.Empty,
                        Caption = ts.Caption,

                        ts.DisplayColumn,
                        Value = (ts.IsKeyColumn && ts.CanFilter) ? ts.Value : null,
                        Expression = ts.Expression

                    }).Distinct()
                .ToLookup(tbl => tbl.TableName);

            //create a final defintion of table objects to use in the context
            var relationalTables = tableLookup.Select(tbl =>
                                                      new Table
                                                          {
                                                              Name = tbl.Key,
                                                              Columns = tbl.Select(cc => new TableColumn()
                                                                  {
                                                                      Caption = cc.Caption,
                                                                      DisplayColumn = cc.DisplayColumn,
                                                                      //if the TableLookup Key Column is missing, populate it with values from the PK Lookup Table
                                                                      KeyColumn = String.IsNullOrEmpty(cc.KeyColumn)
                                                                                      ? cubeSourceTablePkLookup[
                                                                                          "dbo_" + cc.TableName]
                                                                                            .FirstOrDefault()
                                                                                      : cc.KeyColumn,
                                                                      TableName = cc.TableName,
                                                                      Value = cc.Value,
                                                                      Expression = cc.Expression

                                                                  })
                                                          }).ToList();

            #endregion

            #region Build Final Context 

            var relationalSet = new[]
                {
                    new RelationalBuilderContext()
                        {
                            DatabaseId = databaseId,
                            Tables = relationalTables,
                            Joins = joinSet.ToArray()
                        },

                };

            #endregion
            
}

        public class HierarchyChildren
        {
		
			public HierarchyChildren(){}
            public HierarchyChildren(DataRow row)
            {
                CubeName = row.Field<String>("CUBE_NAME");
                DimensionUniqueName = row.Field<String>("DIMENSION_UNIQUE_NAME");
                HierarchyUniqueName = row.Field<String>("HIERARCHY_UNIQUE_NAME");
                LevelName = row.Field<String>("LEVEL_NAME");
                LevelUniqueName = row.Field<String>("LEVEL_UNIQUE_NAME");
                LevelCaption = row.Field<String>("LEVEL_CAPTION");
                SortOrder = (int)row.Field<UInt32>("LEVEL_NUMBER");
                IsVisible = row.Field<bool>("LEVEL_IS_VISIBLE");
                AttributeHierarchyName = row.Field<String>("LEVEL_ATTRIBUTE_HIERARCHY_NAME");
            }
			
            public String CubeName { get; set; }
            public String DimensionUniqueName {get; set;}
            public String HierarchyUniqueName {get; set;}
            public String LevelName {get; set;}
            public String LevelUniqueName {get; set;}
            public String LevelCaption {get; set;}
            public int SortOrder {get; set;}
            public bool IsVisible {get; set;}
            public String AttributeHierarchyName {get; set;}
        }

protected static String ServerName = ".";
//protected static String ServerName = "LT-JFILICHA";
protected static String CatalogName = "BankingCustomerIntelligence_JHLive";
// Define other methods and classes here
public class Utils
{

        public static String ConnectionString(String ServerName)
        {
            return ConnectionString(ServerName, null);
        }

        public static String ConnectionString(String ServerName, String InitialCatalog)
        {
            return !String.IsNullOrEmpty(InitialCatalog)
                       ? string.Format("Data Source={0};Application Name=CubeParser v{1}; Initial Catalog={2}",
                                       ServerName,
                                       System.Reflection.Assembly.GetExecutingAssembly().GetName().Version, InitialCatalog)
                       : string.Format("Data Source={0};Application Name=CubeParser v{1};",
                                       ServerName,
                                       System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
        }
}

private class AdomdSchema
    {
        private System.Guid mGuid;
        private string mId;
        private string mName;
        public static Dictionary<System.Guid, AdomdSchema> sSchemaList = new Dictionary<System.Guid, AdomdSchema>();

        static AdomdSchema()
        {
            sSchemaList.Add(AdomdSchemaGuid.Actions, new AdomdSchema("MDSCHEMA_ACTIONS", AdomdSchemaGuid.Actions, "Actions"));
            sSchemaList.Add(AdomdSchemaGuid.Catalogs, new AdomdSchema("DBSCHEMA_CATALOGS", AdomdSchemaGuid.Catalogs, "Catalogs"));
            sSchemaList.Add(AdomdSchemaGuid.Columns, new AdomdSchema("DBSCHEMA_COLUMNS", AdomdSchemaGuid.Columns, "Columns"));
            sSchemaList.Add(AdomdSchemaGuid.Connections, new AdomdSchema("DISCOVER_CONNECTIONS", AdomdSchemaGuid.Connections, "Connections"));
            sSchemaList.Add(AdomdSchemaGuid.Cubes, new AdomdSchema("MDSCHEMA_CUBES", AdomdSchemaGuid.Cubes, "Cubes"));
            sSchemaList.Add(AdomdSchemaGuid.DataSources, new AdomdSchema("DISCOVER_DATASOURCES", AdomdSchemaGuid.DataSources, "DataSources"));
            sSchemaList.Add(AdomdSchemaGuid.DBConnections, new AdomdSchema("DISCOVER_DB_CONNECTIONS", AdomdSchemaGuid.DBConnections, "DBConnections"));
            sSchemaList.Add(AdomdSchemaGuid.Dimensions, new AdomdSchema("MDSCHEMA_DIMENSIONS", AdomdSchemaGuid.Dimensions, "Dimensions"));
            sSchemaList.Add(AdomdSchemaGuid.DimensionStat, new AdomdSchema("DISCOVER_DIMENSION_STAT", AdomdSchemaGuid.DimensionStat, "DimensionStat"));
            sSchemaList.Add(AdomdSchemaGuid.Enumerators, new AdomdSchema("DISCOVER_ENUMERATORS", AdomdSchemaGuid.Enumerators, "Enumerators"));
            sSchemaList.Add(AdomdSchemaGuid.Functions, new AdomdSchema("MDSCHEMA_FUNCTIONS", AdomdSchemaGuid.Functions, "Functions"));
            sSchemaList.Add(AdomdSchemaGuid.Hierarchies, new AdomdSchema("MDSCHEMA_HIERARCHIES", AdomdSchemaGuid.Hierarchies, "Hierarchies"));
            sSchemaList.Add(AdomdSchemaGuid.InputDataSources, new AdomdSchema("MDSCHEMA_INPUT_DATASOURCES", AdomdSchemaGuid.InputDataSources, "InputDataSources"));
            sSchemaList.Add(AdomdSchemaGuid.Instances, new AdomdSchema("DISCOVER_INSTANCES", AdomdSchemaGuid.Instances, "Instances"));
            sSchemaList.Add(AdomdSchemaGuid.Jobs, new AdomdSchema("DISCOVER_JOBS", AdomdSchemaGuid.Jobs, "Jobs"));
            sSchemaList.Add(AdomdSchemaGuid.Keywords, new AdomdSchema("DISCOVER_KEYWORDS", AdomdSchemaGuid.Keywords, "Keywords"));
            sSchemaList.Add(AdomdSchemaGuid.Kpis, new AdomdSchema("MDSCHEMA_KPIS", AdomdSchemaGuid.Kpis, "Kpis"));
            sSchemaList.Add(AdomdSchemaGuid.Levels, new AdomdSchema("MDSCHEMA_LEVELS", AdomdSchemaGuid.Levels, "Levels"));
            sSchemaList.Add(AdomdSchemaGuid.Literals, new AdomdSchema("DISCOVER_LITERALS", AdomdSchemaGuid.Literals, "Literals"));
            sSchemaList.Add(AdomdSchemaGuid.Locations, new AdomdSchema("DISCOVER_LOCATIONS", AdomdSchemaGuid.Locations, "Locations"));
            sSchemaList.Add(AdomdSchemaGuid.Locks, new AdomdSchema("DISCOVER_LOCKS", AdomdSchemaGuid.Locks, "Locks"));
            sSchemaList.Add(AdomdSchemaGuid.MasterKey, new AdomdSchema("DISCOVER_MASTER_KEY", AdomdSchemaGuid.MasterKey, "MasterKey"));
            sSchemaList.Add(AdomdSchemaGuid.MeasureGroups, new AdomdSchema("MDSCHEMA_MEASUREGROUPS", AdomdSchemaGuid.MeasureGroups, "MeasureGroups"));
            sSchemaList.Add(AdomdSchemaGuid.MeasureGroupDimensions, new AdomdSchema("MDSCHEMA_MEASUREGROUP_DIMENSIONS", AdomdSchemaGuid.MeasureGroupDimensions, "MeasureGroupDimensions"));
            sSchemaList.Add(AdomdSchemaGuid.Measures, new AdomdSchema("MDSCHEMA_MEASURES", AdomdSchemaGuid.Measures, "Measures"));
            sSchemaList.Add(AdomdSchemaGuid.MemberProperties, new AdomdSchema("MDSCHEMA_PROPERTIES", AdomdSchemaGuid.MemberProperties, "MemberProperties"));
            sSchemaList.Add(AdomdSchemaGuid.Members, new AdomdSchema("MDSCHEMA_MEMBERS", AdomdSchemaGuid.Members, "Members"));
            sSchemaList.Add(AdomdSchemaGuid.MemoryGrant, new AdomdSchema("DISCOVER_MEMORYGRANT", AdomdSchemaGuid.MemoryGrant, "MemoryGrant"));
            sSchemaList.Add(AdomdSchemaGuid.MemoryUsage, new AdomdSchema("DISCOVER_MEMORYUSAGE", AdomdSchemaGuid.MemoryUsage, "MemoryUsage"));
            sSchemaList.Add(AdomdSchemaGuid.MiningColumns, new AdomdSchema("DMSCHEMA_MINING_COLUMNS", AdomdSchemaGuid.MiningColumns, "MiningColumns"));
            sSchemaList.Add(AdomdSchemaGuid.MiningFunctions, new AdomdSchema("DMSCHEMA_MINING_FUNCTIONS", AdomdSchemaGuid.MiningFunctions, "MiningFunctions"));
            sSchemaList.Add(AdomdSchemaGuid.MiningModelContent, new AdomdSchema("DMSCHEMA_MINING_MODEL_CONTENT", AdomdSchemaGuid.MiningModelContent, "MiningModelContent"));
            sSchemaList.Add(AdomdSchemaGuid.MiningModelContentPmml, new AdomdSchema("DMSCHEMA_MINING_MODEL_CONTENT_PMML", AdomdSchemaGuid.MiningModelContentPmml, "MiningModelContentPmml"));
            sSchemaList.Add(AdomdSchemaGuid.MiningModels, new AdomdSchema("DMSCHEMA_MINING_MODELS", AdomdSchemaGuid.MiningModels, "MiningModels"));
            sSchemaList.Add(AdomdSchemaGuid.MiningServiceParameters, new AdomdSchema("DMSCHEMA_MINING_SERVICE_PARAMETERS", AdomdSchemaGuid.MiningServiceParameters, "MiningServiceParameters"));
            sSchemaList.Add(AdomdSchemaGuid.MiningServices, new AdomdSchema("DMSCHEMA_MINING_SERVICES", AdomdSchemaGuid.MiningServices, "MiningServices"));
            sSchemaList.Add(AdomdSchemaGuid.MiningStructureColumns, new AdomdSchema("DMSCHEMA_MINING_STRUCTURE_COLUMNS", AdomdSchemaGuid.MiningStructureColumns, "MiningStructureColumns"));
            sSchemaList.Add(AdomdSchemaGuid.MiningStructures, new AdomdSchema("DMSCHEMA_MINING_STRUCTURES", AdomdSchemaGuid.MiningStructures, "MiningStructures"));
            sSchemaList.Add(AdomdSchemaGuid.PartitionDimensionStat, new AdomdSchema("DISCOVER_PARTITION_DIMENSION_STAT", AdomdSchemaGuid.PartitionDimensionStat, "PartitionDimensionStat"));
            sSchemaList.Add(AdomdSchemaGuid.PartitionStat, new AdomdSchema("DISCOVER_PARTITION_STAT", AdomdSchemaGuid.PartitionStat, "PartitionStat"));
            sSchemaList.Add(AdomdSchemaGuid.PerformanceCounters, new AdomdSchema("DISCOVER_PERFORMANCE_COUNTERS", AdomdSchemaGuid.PerformanceCounters, "PerformanceCounters"));
            sSchemaList.Add(AdomdSchemaGuid.ProviderTypes, new AdomdSchema("DBSCHEMA_PROVIDER_TYPES", AdomdSchemaGuid.ProviderTypes, "ProviderTypes"));
            sSchemaList.Add(AdomdSchemaGuid.SchemaRowsets, new AdomdSchema("DISCOVER_SCHEMA_ROWSETS", AdomdSchemaGuid.SchemaRowsets, "SchemaRowsets"));
            sSchemaList.Add(AdomdSchemaGuid.Sessions, new AdomdSchema("DISCOVER_SESSIONS", AdomdSchemaGuid.Sessions, "Sessions"));
            sSchemaList.Add(AdomdSchemaGuid.Sets, new AdomdSchema("MDSCHEMA_SETS", AdomdSchemaGuid.Sets, "Sets"));
            sSchemaList.Add(AdomdSchemaGuid.Tables, new AdomdSchema("DBSCHEMA_TABLES", AdomdSchemaGuid.Tables, "Tables"));
            sSchemaList.Add(AdomdSchemaGuid.TablesInfo, new AdomdSchema("DBSCHEMA_TABLES_INFO", AdomdSchemaGuid.TablesInfo, "TablesInfo"));
            sSchemaList.Add(AdomdSchemaGuid.TraceColumns, new AdomdSchema("DISCOVER_TRACE_COLUMNS", AdomdSchemaGuid.TraceColumns, "TraceColumns"));
            sSchemaList.Add(AdomdSchemaGuid.TraceDefinitionProviderInfo, new AdomdSchema("DISCOVER_TRACE_DEFINITION_PROVIDERINFO", AdomdSchemaGuid.TraceDefinitionProviderInfo, "TraceDefinitionProviderInfo"));
            sSchemaList.Add(AdomdSchemaGuid.TraceEventCategories, new AdomdSchema("DISCOVER_TRACE_EVENT_CATEGORIES", AdomdSchemaGuid.TraceEventCategories, "TraceEventCategories"));
            sSchemaList.Add(AdomdSchemaGuid.Traces, new AdomdSchema("DISCOVER_TRACES", AdomdSchemaGuid.Traces, "Traces"));
            sSchemaList.Add(AdomdSchemaGuid.Transactions, new AdomdSchema("DISCOVER_TRANSACTIONS", AdomdSchemaGuid.Transactions, "Transactions"));
            sSchemaList.Add(AdomdSchemaGuid.XmlaProperties, new AdomdSchema("DISCOVER_PROPERTIES", AdomdSchemaGuid.XmlaProperties, "XmlaProperties"));
            sSchemaList.Add(AdomdSchemaGuid.XmlMetadata, new AdomdSchema("DISCOVER_XML_METADATA", AdomdSchemaGuid.XmlMetadata, "XmlMetadata"));
        }

        public AdomdSchema(string pId, System.Guid pGuid, string pName)
        {
            this.mId = pId;
            this.mGuid = pGuid;
            this.mName = pName;
        }

        public override string ToString()
        {
            return this.mName;
        }

        public System.Guid Guid
        {
            get
            {
                return this.mGuid;
            }
        }

        public string Id
        {
            get
            {
                return this.mId;
            }
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
        }
    }