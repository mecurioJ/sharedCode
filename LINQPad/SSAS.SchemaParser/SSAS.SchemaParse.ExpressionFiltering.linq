<Query Kind="Program">
  <GACReference>Microsoft.AnalysisServices.AdomdClient, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>Microsoft.AnalysisServices.AdomdClient</Namespace>
</Query>

void Main()
{
var ValueParse = @".&";
	var currentMember = "[Responsibility].[Opened by Officer].&[AJM: Ezra Patel]";
						//"{[Calendar Period].[Calendar Period].[Date].&[142]}";
	;

	SchemaParser.oDataBase OlapData = new SchemaParser.oDataBase(XElement.Load(@"c:\Projects\SSAS\BankerSchema.xml"));
	        var Rowlist = new List<String>(){"[Responsibility].[Opened by Officer].&[AJM: Ezra Patel]"};
            var columnList = new List<String>(){"[Measures].[Current Balance]"};
            var FilterList = new List<String>(){"[Calendar Period].[Calendar Period].[Date].&[142]"};
	
            var memberMap = new List<String>(){"[Calendar Period].[Calendar Period]","[Measures]","[Responsibility].[Opened by Officer]","[Products].[Products]","[Account Status].[Account Status Group]","[Organization].[Organization]",};
            var levelMap = new List<String>(){"[Calendar Period].[Calendar Period]","[Measures]","[Responsibility].[Opened by Officer]","[Products].[Products]","[Account Status].[Account Status Group]","[Organization].[Organization]",};
            var dimensionMap = new List<String>(){"[Responsibility].[Opened by Officer]","[Calendar Period].[Calendar Period]","[Account Status].[Account Status Group]","[Organization].[Organization]","[Products].[Products]",};
			
			var measureGroupName = 	"Global Facts"; //measure.MeasureGroup
			var cubeName = "Global"; //sourceReport.CubeRegistration.CubeName	
	
            var LevelDefinition = SchemaParser.GetHierarchyByLevelDefinition(OlapData);
            var measureGroupDefinition = SchemaParser.GetMeasureGroupDimensionDefinitions(OlapData);
			
			measureGroupDefinition.Where(mgd => mgd.AttributeType.Equals("Granularity") 
												&& mgd.CubeName.Equals(cubeName)
												&& currentMember.Contains(mgd.MeasureGroupCubeDimensionID)).Dump();
//Name ID CreatedTimestamp LastSchemaUpdate Description Annotations LastProcessed State Source MiningModelID Type 
//UnknownMember MdxMissingMemberMode StringStoresCompatibilityLevel CurrentStringStoresCompatibilityLevel StorageMode 
//CurrentStorageMode ProcessingPriority WriteEnabled DependsOnDimensionID Language Collation UnknownMemberName 
//ProcessingMode AttributeAllMemberName ProactiveCaching UnknownMemberTranslations 
//AttributeAllMemberTranslations Translations Attributes Hierarchies 
												
//			OlapData.Dimensions
//			.Where(mgd => currentMember.Contains(mgd.Name))
//			.Where(mgd => mgd.Attributes.Where(att => currentMember.Contains(att.Name) ).Any())
//			.SelectMany(mem =>
//                                                   mem.KeyColumns.Select(
//                                                       kc => new {mem.ID, mem.AttributeID, kc.TableName, kc.ColumnName}))
//                                       .ToArray()
//			.Dump();
	LevelDefinition.Dump();
}
// Define other methods and classes here



    internal class Namespaces
    {
        public static XNamespace engine = "http://schemas.microsoft.com/analysisservices/2003/engine";
        public static XNamespace engine200 = "http://schemas.microsoft.com/analysisservices/2010/engine/200";
        public static XNamespace engine300 = "http://schemas.microsoft.com/analysisservices/2011/engine/300";
        public static XNamespace engine2 = "http://schemas.microsoft.com/analysisservices/2003/engine/2";
        public static XNamespace xs = "http://www.w3.org/2001/XMLSchema";
        public static XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        public static XNamespace msData = "urn:schemas-microsoft-com:xml-msdata";
        public static XNamespace diffGram = "urn:schemas-microsoft-com:xml-diffgram-v1";

        public static String ParseNamespace(XElement element)
        {
            return ParseNamespace(element.Name);
        }

        public static String ParseNamespace(XAttribute attribute)
        {
            return ParseNamespace(attribute.Name);
        }

        public static String ParseNamespace(XName name)
        {
            String result = String.Empty;
            switch (name.NamespaceName)
            {
                case "http://schemas.microsoft.com/analysisservices/2003/engine":
                    result = "engine";
                    break;
                case "http://schemas.microsoft.com/analysisservices/2010/engine/200":
                    result = "engine200";
                    break;
                case "http://schemas.microsoft.com/analysisservices/2011/engine/300":
                    result = "engine300";
                    break;
                case "http://schemas.microsoft.com/analysisservices/2003/engine/2":
                    result = "engine2";
                    break;
                case "http://www.w3.org/2001/XMLSchema":
                    result = "xs";
                    break;
                case "http://www.w3.org/2001/XMLSchema-instance":
                    result = "xsi";
                    break;
                case "urn:schemas-microsoft-com:xml-msdata":
                    result = "msData";
                    break;
                case "urn:schemas-microsoft-com:xml-diffgram-v1":
                    result = "diffGram";
                    break;
            }
            return result;
        }

    }

    internal class AdomdSchema
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
	
	
    public static class SchemaParser
    {
        public static string[] SqlReservedKeywords
        {
            get
            {

                return new []{
                    "ADD","EXTERNAL","PROCEDUREALL","FETCH","PUBLICALTER","FILE","RAISERRORAND",
                    "FILLFACTOR","READANY","FOR","READTEXTAS","FOREIGN","RECONFIGUREASC","FREETEXT",
                    "REFERENCESAUTHORIZATION","FREETEXTTABLE","REPLICATIONBACKUP","FROM","RESTOREBEGIN",
                    "FULL","RESTRICTBETWEEN","FUNCTION","RETURNBREAK","GOTO","REVERTBROWSE","GRANT",
                    "REVOKEBULK","GROUP","RIGHTBY","HAVING","ROLLBACKCASCADE","HOLDLOCK","ROWCOUNTCASE",
                    "IDENTITY","ROWGUIDCOLCHECK","IDENTITY_INSERT","RULECHECKPOINT","IDENTITYCOL",
                    "SAVECLOSE","IF","SCHEMACLUSTERED","IN","SECURITYAUDITCOALESCE","INDEX","SELECTCOLLATE",
                    "INNER","SEMANTICKEYPHRASETABLECOLUMN","INSERT","SEMANTICSIMILARITYDETAILSTABLECOMMIT",
                    "INTERSECT","SEMANTICSIMILARITYTABLECOMPUTE","INTO","SESSION_USERCONSTRAINT","IS",
                    "SETCONTAINS","JOIN","SETUSERCONTAINSTABLE","KEY","SHUTDOWNCONTINUE","KILL","SOMECONVERT",
                    "LEFT","STATISTICSCREATE","LIKE","SYSTEM_USERCROSS","LINENO","TABLECURRENT","LOAD",
                    "TABLESAMPLECURRENT_DATE","MERGE","TEXTSIZECURRENT_TIME","NATIONAL","THENCURRENT_TIMESTAMP",
                    "NOCHECK","TOCURRENT_USER","NONCLUSTERED","TOPCURSOR","NOT","TRANDATABASE","NULL",
                    "TRANSACTIONDBCC","NULLIF","TRIGGERDEALLOCATE","OF","TRUNCATEDECLARE","OFF","TRY_CONVERTDEFAULT",
                    "OFFSETS","TSEQUALDELETE","ON","UNIONDENY","OPEN","UNIQUEDESC","OPENDATASOURCE","UNPIVOTDISK",
                    "OPENQUERY","UPDATEDISTINCT","OPENROWSET","UPDATETEXTDISTRIBUTED","OPENXML","USEDOUBLE",
                    "OPTION","USERDROP","OR","VALUESDUMP","ORDER","VARYINGELSE","OUTER","VIEWEND","OVER",
                    "WAITFORERRLVL","PERCENT","WHENESCAPE","PIVOT","WHEREEXCEPT","PLAN","WHILEEXEC","PRECISION",
                    "WITHEXECUTE","PRIMARY","WITHIN GROUPEXISTS","PRINT","WRITETEXTEXIT","PROC"
                    };
            }
        }
        public static string[] MdxReservedKeywords
        {
            get
            {

                return new []{
                    "ABSOLUTE","DESC","LEAVES","SELF_BEFORE_AFTERACTIONPARAMETERSET","DESCENDANTS","LEVEL",
                    "SESSIONADDCALCULATEDMEMBERS","DESCRIPTION","LEVELS","SETAFTER","DIMENSION","LINKMEMBER",
                    "SETTOARRAYAGGREGATE","DIMENSIONS","LINREGINTERCEPT","SETTOSTRALL","DISTINCT","LINREGPOINT",
                    "SORTALLMEMBERS","DISTINCTCOUNT","LINREGR2","STDDEVANCESTOR","DRILLDOWNLEVEL","LINREGSLOPE",
                    "STDDEVPANCESTORS","DRILLDOWNLEVELBOTTOM","LINREGVARIANCE","STDEVAND","DRILLDOWNLEVELTOP",
                    "LOOKUPCUBE","STDEVPAS","DRILLDOWNMEMBER","MAX","STORAGEASC","DRILLDOWNMEMBERBOTTOM",
                    "MEASURE","STRIPCALCULATEDMEMBERSASCENDANTS","DRILLDOWNMEMBERTOP","MEDIAN","STRTOMEMBERAVERAGE",
                    "DRILLUPLEVEL","MEMBER","STRTOSETAXIS","DRILLUPMEMBER","MEMBERS","STRTOTUPLEBASC","DROP",
                    "MEMBERTOSTR","STRTOVALBDESC","EMPTY","MIN","STRTOVALUEBEFORE","END","MTD","SUBSETBEFORE_AND_AFTER",
                    "ERROR","NAME","SUMBOTTOMCOUNT","EXCEPT","NAMETOSET","TAILBOTTOMPERCENT","EXCLUDEEMPTY","NEST",
                    "THISBOTTOMSUM","EXTRACT","NEXTMEMBER","TOGGLEDRILLSTATEBY","FALSE","NO_ALLOCATION","TOPCOUNTCACHE",
                    "FILTER","NO_PROPERTIES","TOPPERCENTCALCULATE","FIRSTCHILD","NON","TOPSUMCALCULATION","FIRSTSIBLING",
                    "NONEMPTYCROSSJOIN","TOTALSCALCULATIONCURRENTPASS","FOR","NOT_RELATED_TO_FACTS",
                    "TREECALCULATIONPASSVALUE","FREEZE","NULL","TRUECALCULATIONS","FROM","ON","TUPLETOSTRCALL",
                    "GENERATE","OPENINGPERIOD","TYPECELL","GLOBAL","OR","UNIONCELLFORMULASETLIST","GROUP","PAGES",
                    "UNIQUECHAPTERS","GROUPING","PARALLELPERIOD","UNIQUENAMECHILDREN","HEAD","PARENT","UPDATECLEAR",
                    "HIDDEN","PASS","USECLOSINGPERIOD","HIERARCHIZE","PERIODSTODATE","USE_EQUAL_ALLOCATIONCOALESCEEMPTY",
                    "HIERARCHY","POST","USE_WEIGHTED_ALLOCATIONCOLUMN","IGNORE","PREDICT","USE_WEIGHTED_INCREMENTCOLUMNS",
                    "IIF","PREVMEMBER","USERNAMECORRELATION","INCLUDEEMPTY","PROPERTIES","VALIDMEASURECOUNT","INDEX",
                    "PROPERTY","VALUECOUSIN","INTERSECT","QTD","VARCOVARIANCE","IS","RANK","VARIANCECOVARIANCEN",
                    "ISANCESTOR","RECURSIVE","VARIANCEPCREATE","ISEMPTY","RELATIVE","VARPCREATEPROPERTYSET","ISGENERATION",
                    "ROLLUPCHILDREN","VISUALCREATEVIRTUALDIMENSION","ISLEAF","ROOT","VISUALTOTALSCROSSJOIN","ISSIBLING",
                    "ROWS","WHERECUBE","ITEM","SCOPE","WITHCURRENT","LAG","SECTIONS","WTDCURRENTCUBE","LASTCHILD",
                    "SELECT","XORCURRENTMEMBER","LASTPERIODS","SELF","YTDDEFAULT_MEMBER","LASTSIBLING","SELF_AND_AFTER",
                    "DEFAULTMEMBER","LEAD","SELF_AND_BEFORE"
                    };
            }
        }

		public class MemberDefinition
		{
			public String Dimension {get;set;}
			public String Hierarchy {get;set;}
			public String Level {get;set;}
			public String Value  {get;set;}
			public IEnumerable<AttributeDefinition> AttributeDefinitions {get;set;}
			public IEnumerable<HierarchyByLevelDefinition> LevelDefinitions {get;set;}
			public IEnumerable<MeasureGroupMeasureDefinition> MeasureDefinitions {get;set;} 

		}

        public class MeasureGroupDimensionDefinition
        {
            public String CubeID { get; set; }
            public String CubeName { get; set; }
            public String MeasureGroupName { get; set; }
            public String MeasureGroupID { get; set; }
            public String MeasureGroupCardinality { get; set; }
            public String MeasureGroupCubeDimensionID { get; set; }
            public String MeasureGroupSource { get; set; }
            public String MeasureGroupDirectSlice { get; set; }
            public String AttributeID { get; set; }
            public String AttributeType { get; set; }
            public String KeyColumnDataType { get; set; }
            public String KeyColumnDataSize { get; set; }
            public String KeyColumnNullProcessing { get; set; }
            public String KeyColumnCollation { get; set; }
            public String KeyColumnFormat { get; set; }
            public String SourceColumnBindingType { get; set; }
            public String SourceColumnTableName { get; set; }
            public String SourceColumnName { get; set; }
        }

        public static IEnumerable<MeasureGroupDimensionDefinition> GetMeasureGroupDimensionDefinitions(oDataBase olapData)
        {
            return olapData.Cubes.SelectMany(cube =>
    cube.MeasureGroups.SelectMany(mg => mg.Dimensions.SelectMany(mgDim =>
                mgDim.Attributes.SelectMany(attr => attr.KeyColumns.Cast<SchemaParser.KeyColumn>()
                    .SelectMany(kc =>
                        kc.Source.Cast<SchemaParser.TableColumn>().Select(src => new SchemaParser.MeasureGroupDimensionDefinition
                        {
                            CubeID = cube.ID,
                            CubeName = cube.Name,
                            MeasureGroupName = mg.Name,
                            MeasureGroupID = mg.ID,
                            MeasureGroupCardinality = mgDim.Cardinality,
                            MeasureGroupCubeDimensionID = mgDim.CubeDimensionID,
                            MeasureGroupSource = mgDim.MeasureGroupID,
                            MeasureGroupDirectSlice = mgDim.DirectSlice,
                            AttributeID = attr.AttributeID,
                            AttributeType = attr.Type,
                            KeyColumnDataType = kc.DataType,
                            KeyColumnDataSize = kc.DataSize,
                            KeyColumnNullProcessing = kc.NullProcessing,
                            KeyColumnCollation = kc.Collation,
                            KeyColumnFormat = kc.Format,
                            SourceColumnBindingType = src.Type,
                            SourceColumnTableName = src.TableName,
                            SourceColumnName = src.ColumnName
                        }))))));
        }

        public class MeasureGroupMeasureDefinition
        {
            public String CubeID { get; set; }
            public String CubeName { get; set; }
            public String MeasureGroupName { get; set; }
            public String MeasureGroupID { get; set; }
            public String MeasureName { get; set; }
            public String MeasureID { get; set; }
            public String MeasureDescription { get; set; }
            public String MeasureAggregateFunction { get; set; }
            public String MeasureDataType { get; set; }
            public String MeasureIsVisible { get; set; }
            public String MeasureFormatString { get; set; }
            public String MeasureSourceDataType { get; set; }
            public String SourceDataType { get; set; }
            public String SourceDataTableName { get; set; }
            public String SourceDataColumnName { get; set; }
        }

        public static IEnumerable<MeasureGroupMeasureDefinition> GetMeasureGroupMeasureDefinitions(oDataBase olapData)
        {
            return olapData.Cubes.SelectMany(cube =>
            cube.MeasureGroups.SelectMany(mg =>
            mg.Measures.SelectMany(msr =>
                msr.Source.SelectMany(mSrc =>
                    mSrc.Source.Select(kc =>
                        new SchemaParser.MeasureGroupMeasureDefinition
                        {
                            CubeID = cube.ID,
                            CubeName = cube.Name,
                            MeasureGroupName = mg.Name,
                            MeasureGroupID = mg.ID,
                            MeasureName = msr.Name,
                            MeasureID = msr.ID,
                            MeasureDescription = msr.Description,
                            MeasureAggregateFunction = msr.AggregateFunction,
                            MeasureDataType = msr.DataType,
                            MeasureIsVisible = msr.Visible,
                            MeasureFormatString = msr.FormatString,
                            MeasureSourceDataType = mSrc.DataType,
                            SourceDataType = kc.Type,
                            SourceDataTableName = kc.TableName,
                            SourceDataColumnName = kc.ColumnName
                        })))));
        }
		
		

        public static IEnumerable<DataSourceComputedColumnDefinition> GetExpressionDefinition(oDataBase olapData, String tableName, String columnName)
        {
            var TableSources = SchemaParser.GetDataSourceViewDefinitions(olapData).SelectMany(dt => dt.Tables.Select(tb => new { tb.TableName, tb.ColumnName }))
            .ToLookup(tbl => tbl.TableName, tbl => tbl.ColumnName);

            return SchemaParser.GetDataSourceViewExpressions(olapData)
            .SelectMany(dsv => dsv.ExpressionTree.Where(tree => !String.IsNullOrEmpty(tree.Expression)).Select(tree => new SchemaParser.DataSourceComputedColumnDefinition
            {
                DataSourceViewId = dsv.DataSourceViewId,
                DataSourceViewName = dsv.DataSourceViewName,
                TableName = tree.TableName,
                ColumnName = tree.ColumnName,
                Expression = tree.Expression,
                ExpressionTree = tree.ExpressionTree.ToList().Intersect(new HashSet<String>(TableSources[tree.TableName]).Select(v => v).ToList())
                .Select(tt => new SchemaParser.DataSourceComputedColumnDefinition.ExpressionDefinition { TableName = tree.TableName, ColumnName = tt })
            }))
            .Where(eTree => eTree.TableName.Equals(tableName))
            .Where(eTree => eTree.ColumnName.Equals(columnName));
        }


        public static IEnumerable<HierarchyByLevelDefinition> GetHierarchyByLevelDefinition(oDataBase olapData)
        {
            return olapData.Dimensions.SelectMany(cDim =>
        cDim.Hierarchies.SelectMany(cdh =>
            cdh.Levels.SelectMany(cLvl => cDim.Attributes
                .Where(cAtt => cAtt.ID.Equals(cLvl.SourceAttributeID))
                .Select(cAtt => new SchemaParser.HierarchyByLevelDefinition
                {
                    FullHierarchyName = String.Format("[{0}].[{1}]", cDim.Name, cdh.Name),
                    FullLevelName = String.Format("[{0}].[{1}].[{2}]", cDim.Name, cdh.Name, cLvl.Name),
                    DimensionName = cDim.Name,
                    DimensionID = cDim.ID,
                    DimensionDataSourceViewID = cDim.Source,
                    AttributeAllMemberName = String.Format("[{0}].[{1}].[{2}]", cDim.Name, cdh.Name, 
						String.IsNullOrEmpty(cDim.AttributeAllMemberName)
						? cdh.AllMemberName
						: cDim.AttributeAllMemberName
					),
                    HierarchyName = cdh.Name,
                    HierarchyID = cdh.ID,
                    HierarchyAllMemberName = cdh.AllMemberName,
                    MemberNamesUnique = cdh.MemberNamesUnique,
                    AllowDuplicateNames = cdh.AllowDuplicateNames,
                    MemberKeysUnique = cdh.MemberKeysUnique,
                    LevelName = cLvl.Name,
                    LevelID = cLvl.ID,
                    AttributeName = cAtt.Name,
                    AttributeID = cAtt.ID,
                    AttributeType = cAtt.Type,
                    AttributeUsage = cAtt.Usage,
                    AttributeOrderBy = cAtt.OrderBy,
                    HierarchyDisplayFolder = cAtt.AttributeHierarchyDisplayFolder,
                    KeyColumns = cAtt.KeyColumns.SelectMany(src => src.Source.Select(kc => new SchemaParser.TableColumn
                    {
                        TableName = kc.TableName,
                        ColumnName = kc.ColumnName
                    })),
                    //String.Format("{0}.{1}",kc.TableName,kc.ColumnName))),
                    NameColumn = cAtt.NameColumn.SelectMany(src => src.Source.Select(kc => new SchemaParser.TableColumn
                    {
                        TableName = kc.TableName,
                        ColumnName = kc.ColumnName
                    })),
                    HasAttributeRelationships = cAtt.AttributeRelationships.Any(),
                    //We should be able to get the relationships underlying data here, if there are relationships.
                    //We can also include these items (columns and tables) in the resulting info passed to the 
                    //	relational report builder stuff.
                    AttributeRelationships = cAtt.AttributeRelationships,
                })
            ))
    );
        }

        public class HierarchyByLevelDefinition
        {
            public String FullHierarchyName { get; set; }
            public String FullLevelName { get; set; }
            public String DimensionName { get; set; }
            public String DimensionID { get; set; }
            public String DimensionDataSourceViewID { get; set; }
            public String AttributeAllMemberName { get; set; }
            public String HierarchyName { get; set; }
            public String HierarchyID { get; set; }
            public String HierarchyAllMemberName { get; set; }
            public String MemberNamesUnique { get; set; }
            public String AllowDuplicateNames { get; set; }
            public String MemberKeysUnique { get; set; }
            public String LevelName { get; set; }
            public String LevelID { get; set; }
            public String AttributeName { get; set; }
            public String AttributeID { get; set; }
            public String AttributeType { get; set; }
            public String AttributeUsage { get; set; }
            public String AttributeOrderBy { get; set; }
            public String HierarchyDisplayFolder { get; set; }
            public IEnumerable<TableColumn> KeyColumns { get; set; }
            public IEnumerable<TableColumn> NameColumn { get; set; }
            public bool HasAttributeRelationships { get; set; }
            public IEnumerable<oAttributeRelationship> AttributeRelationships { get; set; }
        }

        public class DataSourceComputedColumnDefinition
        {
            public String DataSourceViewId { get; set; }
            public String DataSourceViewName { get; set; }
            public String TableName { get; set; }
            public String ColumnName { get; set; }
            public String Expression { get; set; }
            public IEnumerable<ExpressionDefinition> ExpressionTree { get; set; }

            public class ExpressionDefinition
            {
                public String TableName { get; set; }
                public String ColumnName { get; set; }
            }

        }

        public static IEnumerable<MeasureSourceDefinition> GetMeasureSourceDefinitions(oDataBase olapData)
        {
            return olapData.Cubes.SelectMany(cub => cub.MeasureGroups.SelectMany(mg =>
                            mg.Measures.SelectMany(mm =>
                            mm.Source.SelectMany(src => src.Source.Select(kc => new MeasureSourceDefinition
                            {
                                DataSourceViewId = cub.Source,
                                MeasureName = mm.Name,
                                MeasureGroupName = mg.Name,
                                MeasureGroupId = mg.ID,
                                MeasureId = mm.ID,
                                TableName = kc.TableName,
                                ColumnName = kc.ColumnName
                            })))));
        }

        public static IEnumerable<DataSourceViewDefinition> GetDataSourceViewDefinitions(oDataBase olapData)
        {
            return olapData.DataSourceViews.Select(dsv => new DataSourceViewDefinition
            {
                DataSourceViewId = dsv.ID,
                Relations = dsv.Schema.Relations.Cast<DataRelation>().Select(dr => new Relation
                {
                    Child = dr.ChildColumns.Cast<DataColumn>().Select(col => new TableColumn { TableName = col.Table.TableName, ColumnName = col.ColumnName }).First(),
                    Parent = dr.ParentColumns.Cast<DataColumn>().Select(col => new TableColumn { TableName = col.Table.TableName, ColumnName = col.ColumnName }).First(),
                }),
                Tables = dsv.Schema.Tables.Cast<DataTable>().SelectMany(tbl =>
                    tbl.Columns.Cast<DataColumn>().Select(exp =>
                    new DataSourceViewTableDefinition
                    {
                        TableName = tbl.TableName,
                        ColumnName = exp.ExtendedProperties["DbColumnName"].ToString(),
                        Expression = (exp.ExtendedProperties["ComputedColumnExpression"] != null) ? exp.ExtendedProperties["ComputedColumnExpression"].ToString() : String.Empty,
                    })
                )
            });
        }

        public static IEnumerable<AttributeSourceDefinition> GetAttributeSourceDefinitions(oDataBase olapData)
        {
            return olapData.Dimensions.SelectMany(cDim => cDim.Attributes.Select(cAtt => new AttributeSourceDefinition
                {
                    KeyColumnDefinitions =
                        cAtt.KeyColumns.SelectMany(kc => kc.Source.Select(src => new AttributeColumnSourceDefinition
                            {
                                DimensionId = cDim.ID,
                                DimensionName = cDim.Name,
                                DataSourceViewId = cDim.Source,
                                AttributeName = cAtt.Name,
                                AttributeId = cAtt.ID,
                                Usage = cAtt.Usage,
                                TableName = src.TableName,
                                ColumnName = src.ColumnName
                            })).ToArray(),
                    NameColumnDefinitions =
                        cAtt.NameColumn.SelectMany(kc => kc.Source.Select(src => new AttributeColumnSourceDefinition
                            {
                                DimensionId = cDim.ID,
                                DimensionName = cDim.Name,
                                DataSourceViewId = cDim.Source,
                                AttributeName = cAtt.Name,
                                AttributeId = cAtt.ID,
                                Usage = cAtt.Usage,
                                TableName = src.TableName,
                                ColumnName = src.ColumnName
                            })).ToArray(),
                }));
        }

        public static Lookup<string, IEnumerable<string>> GetTableLookup(oDataBase olapData)
        {
            return olapData.DataSourceViews.SelectMany(dsv => dsv.Schema.Tables.Cast<DataTable>())
                           .ToLookup(t => t.TableName, t => t.Columns.Cast<DataColumn>().Select(tCol => tCol.ColumnName)) as Lookup<string, IEnumerable<string>>;   
        }

        public static IEnumerable<DataSourceViewExpression> GetDataSourceViewExpressions(oDataBase olapData)
        {
            return GetDataSourceViews(olapData.SchemaSource).SelectMany(SS => SS.Select(G =>
                    new
                    DataSourceViewExpression
                    {
                        DataSourceViewName = G.Name,
                        DataSourceViewId = G.ID,
                        ExpressionTree = ((DataSet)G.Schema)
                                    .Tables.Cast<DataTable>()
                                    .SelectMany(t =>
                                        t.Columns.Cast<DataColumn>().Select(dc =>
                                            new SchemaParser.ExpressionTreeItem
                                            {
                                                TableName = t.TableName,
                                                ColumnName = dc.ColumnName,
                                                Expression = (dc.ExtendedProperties["ComputedColumnExpression"] == null)
                                                                ? String.Empty
                                                                : dc.ExtendedProperties["ComputedColumnExpression"].ToString(),
                                                ExpressionTree = (dc.ExtendedProperties["ComputedColumnExpression"] == null)
                                                            ? new String[] { String.Empty }
                                                            : Regex.Split(
                                                            dc.ExtendedProperties["ComputedColumnExpression"].ToString(),
                                                                @"[^a-zA-Z0-9\-_]+",
                                                            RegexOptions.Multiline
                                                            )
                                            }))
                    }));
        }

        public static IEnumerable<string> GetSingleColumnExpression(oDataBase olapData, String columnName, string tableName)
        {
            var dsTables =
                GetDataSourceViewDefinitions(olapData)
                    .SelectMany(dsvDef => dsvDef.Tables)
                    .Where(tbl => tbl.TableName.Equals(tableName));

            IEnumerable<string> expressionTree = GetDataSourceViewExpressions(olapData).SelectMany(expTree => expTree.ExpressionTree)
                                                                     .Where(exTbl => exTbl.TableName.Equals(tableName))
                                                                     .Where(exCol => exCol.ColumnName.Equals(columnName))
                                                                     .Select(exP => exP.ExpressionTree)
                                                                     .SelectMany(
                                                                         tCol =>
                                                                         dsTables.Where(
                                                                             ccol => ccol.ColumnName.Equals(tCol))
                                                                             .Select(col => col.ColumnName)
                                                                             );

            return expressionTree;
        }


        public static Dictionary<string, oDataSourceView> GetAttributeDataSources(oDataBase olapData, AttributeDefinition[] attributes)
        {
            return GetDataSourceViews(olapData.SchemaSource)
                               .Join(
                                   attributes.Select(def => def.DataSourceViewId).Distinct(),
                                   Sources => Sources.Key,
                                   Filters => Filters,
                                   (Sources, Filters) => Sources.Select(v => v)
                ).SelectMany(tt => tt)
                               .ToDictionary(k => k.ID);
        }

        public static oDataBase GetSchemaObjects(AdomdConnection connection, String DatabaseID)
        {
            XElement source;
            if (connection.State != ConnectionState.Open)
                connection.Open();
            using (connection)
            {
            source = XElement.Parse(
                connection.GetSchemaDataSet(
                    AdomdSchema.sSchemaList[AdomdSchemaGuid.XmlMetadata].Id,
                    new AdomdRestrictionCollection
                        {
                            new AdomdRestriction("DatabaseID", DatabaseID)
                        })
                          .Tables[0]
                    .Rows[0]
                    .Field<String>("METADATA"));
            }

            return GetSchemaObjects(source);
        }

        public static oDataBase GetSchemaObjects(XElement SchemaSource)
        {
            return new oDataBase(SchemaSource);
        }

        public static Lookup<String, oDataSourceView> GetDataSourceViews(XElement SchemaSource)
        {
            return new oDataBase(SchemaSource).DataSourceViews.ToLookup(dsv => dsv.ID, dsv => dsv) as Lookup<String, oDataSourceView>;
        }

        public static Lookup<String,oDimension> GetCubeDimensions(XElement SchemaSource)
        {
            return new oDataBase(SchemaSource).Dimensions.ToLookup(dim => dim.ID, dim => dim) as Lookup<String, oDimension>;
        }

        public static Lookup<String, oMeasureGroup> GetMeasureGroups(XElement SchemaSource)
        {
            return new oDataBase(SchemaSource).Cubes.SelectMany(cub => cub.MeasureGroups)
                                              .ToLookup(mg => mg.ID, mg => mg) as Lookup<String, oMeasureGroup>;
        }

        public static IEnumerable<oMeasureGroupDimension> GetMeasureGroupDimensions(oDataBase olapData, string measureGroup)
        {
            return olapData.Cubes.SelectMany(mGroup =>
                                                                   mGroup.MeasureGroups.Where(
                                                                       mgName => mgName.Name.Equals(measureGroup))
                ).SelectMany(smg => smg.Dimensions); 

        }

        public static Lookup<String,ComputedColumnExpression> GetComputedColumns(oDataBase olapData)
        {
            List<DataSet> SchemaObjects = new List<DataSet>();

            foreach (var ds in olapData.SchemaSource.Descendants(Namespaces.engine + "Schema"))
            {
                DataSet dSet = new DataSet();
                dSet.ReadXml(ds.CreateReader());
                SchemaObjects.Add(dSet);
            }

            return SchemaObjects.SelectMany(
                dst => dst.Tables.Cast<DataTable>().SelectMany(dset => dset.Columns.Cast<DataColumn>()))
                                .Where(dCol => dCol.ExtendedProperties["ComputedColumnExpression"] != null)
                                .Select(Col => new ComputedColumnExpression
                                    {
                                        TableName = Col.Table.TableName,
                                        ColumnName = Col.ColumnName,
                                        Expression = Col.ExtendedProperties["ComputedColumnExpression"].ToString()
                                    })
                                .ToLookup(t => t.ColumnName)
                                .Select(v => v.First())
                                .ToLookup(l => l.ColumnName) as Lookup<string, ComputedColumnExpression>;
        }

//        public static IEnumerable<DataSet> GetDataSourceViewSchemas(oDataBase olapData, List<CubeMetaMember> memberList, IEnumerable<oMeasureGroupDimension> measureGroupDimensions)
//        {
//
//            List<DataSet> Schemas = new List<DataSet>
//                {
//                    olapData.DataSourceViews.Join(
//                        olapData.Dimensions.Join(
//                            measureGroupDimensions,
//                            dim => dim.ID,
//                            sourceIds => sourceIds.CubeDimensionID,
//                            (dim, sourceIds) => dim),
//                        t1 => t1.ID,
//                        t2 => t2.Source,
//                        (t1, t2) => t1).Select(sv => sv.Schema).First()
//                };
//
//            Schemas.AddRange(olapData.DataSourceViews.Join(
//                olapData.Dimensions.Join(
//                    olapData.Cubes.SelectMany(cub => cub.Dimensions).Join(
//                        memberList.Select(m => m.Dimension),
//                        t1 => t1.Name,
//                        t2 => t2.Name,
//                        (t1, t2) => t1),
//                    t1 => t1.ID,
//                    t2 => t2.DimensionID,
//                    (t1, t2) => t1).Select(d => d.Source).Distinct(),
//                t1 => t1.ID,
//                t2 => t2,
//                (t1, t2) => t1).Select(sv => sv.Schema));
//
//            return Schemas;
//        }
//
//        public static IEnumerable<oDimension> GetMeasureGroupSourceDimensions(oDataBase olapData,
//                                                                       IEnumerable<oMeasureGroupDimension>
//                                                                           measureGroupDimensions)
//        {
//            return olapData.Dimensions.Join(
//                measureGroupDimensions,
//                dim => dim.ID,
//                sourceIds => sourceIds.CubeDimensionID,
//                (dim, sourceIds) => dim);
//        }

        public static Lookup<String, oMeasureGroupMeasure> GetMeasureGroupMeasures(XElement SchemaSource)
        {
            return new oDataBase(SchemaSource).Cubes.SelectMany(cub => cub.MeasureGroups.SelectMany(cMg => cMg.Measures))
                                              .ToLookup(mg => mg.ID, mg => mg) as Lookup<String, oMeasureGroupMeasure>;
            
        }

        public static IEnumerable<String> GetHierarchyAttributeIds(oDataBase olapData, IEnumerable<String> memberList)
        {
            return olapData.Dimensions
                    .SelectMany(dim => dim.Hierarchies)
                    .Join(
                        memberList.Distinct(),
                        t1 => t1.Name,
                        t2 => t2,
                        (t1, t2) => t1)
                        .SelectMany(hier => hier.Levels.Select(lvl => lvl.SourceAttributeID));
        }

        public static Lookup<String, TableColumn> GetAttributeSourceTablesColumns(oDataBase olapData,
                                                                           IEnumerable<String> hierarchySet)
        {
            return olapData.Dimensions.SelectMany(aDim => aDim.Attributes).Join(
                hierarchySet,
                t1 => t1.ID,
                t2 => t2,
                (t1, t2) => t1
                       ).Where(att => att.KeyColumns != null)
                           .SelectMany(atx => atx.KeyColumns.SelectMany(kc => kc.Source
                                                  ))
                           .ToLookup(g => g.TableName) as Lookup<string, TableColumn>;
        }
//
//        public static IEnumerable<JoinItemSource> GetJoinItemSource(oDataBase olapData, 
//            List<CubeMetaMember> memberDetails, IEnumerable<AttributeDefinition> attributeDef, IEnumerable<DataSourceViewDefinition> dataSourceViewDef
//            )
//        {
//            /*
//            memberDetails.SelectMany(md => attributeDef
//                           .Where(aDef => aDef.DimensionName.Equals(md.Dimension.Name))
//                           .Where(aDef => aDef.AttributeName.Equals(md.Name))
//                           .Union(
//                               attributeDef
//                               .Where(aDef => aDef.DimensionName.Equals(md.Dimension.Name))
//                               .Where(aDef => aDef.AttributeName.Equals(md.Hierarchy.Name))
//                            )
//                           .Union(
//                               attributeDef
//                               .Where(aDef => aDef.DimensionName.Equals(md.Dimension.Name))
//                               .Where(aDef => aDef.AttributeName.Equals(SchemaParser.ParseUniqueName(md.UniqueName)))
//                            ));
//            */
//            /*
//            var UnifiedAttributeDefinitions =
//                attributeDef.Where(aDef => aDef.DimensionName.Equals(dimName))
//                        .Where(aDef => aDef.AttributeName.Equals(attribName))
//                .Union(attributeDef.Where(aDef => aDef.DimensionName.Equals(dimName))
//                        .Where(aDef => aDef.AllAttributeName.Equals(attribName)));
//            */
//
//            return memberDetails.SelectMany(md => attributeDef
//                           .Where(aDef => aDef.DimensionName.Equals(md.Dimension.Name))
//                           .Where(aDef => aDef.AttributeName.Equals(md.Name))
//                           .Union(
//                               attributeDef
//                               .Where(aDef => aDef.DimensionName.Equals(md.Dimension.Name))
//                               .Where(aDef => aDef.AttributeName.Equals(md.Hierarchy.Name))
//                            )
//                           .Union(
//                               attributeDef
//                               .Where(aDef => aDef.DimensionName.Equals(md.Dimension.Name))
//                               .Where(aDef => aDef.AttributeName.Equals(SchemaParser.ParseUniqueName(md.UniqueName)))
//                            ))//.Where(aDef => aDef.DimensionName.Equals(dimName))
//                        //.Where(aDef => aDef.AttributeName.Equals(attribName))
//                        .Select(aDef => new SchemaParser.JoinItemSource
//                        {
//                            DataSourceViewId = aDef.DataSourceViewId,
//                            DimensionId = aDef.DimensionID,
//                            DimensionName = aDef.DimensionName,
//                            AttributeId = aDef.AttributeID,
//                            AttributeName = aDef.AttributeName,
//                            AttributeType = aDef.AttributeType,
//
//                            keyColumns = aDef.KeyColumn.SelectMany(input => SchemaParser.ParseExpression(
//                                input,
//                                SchemaParser.GetExpression(olapData, aDef.DataSourceViewId, input.TableName, input.ColumnName),
//                                dataSourceViewDef.Where(dsv => dsv.DataSourceViewId.Equals(aDef.DataSourceViewId)))),
//
//                            nameColumns = aDef.NameColumn.SelectMany(input => SchemaParser.ParseExpression(
//                                input,
//                                SchemaParser.GetExpression(olapData, aDef.DataSourceViewId, input.TableName, input.ColumnName),
//                                dataSourceViewDef.Where(dsv => dsv.DataSourceViewId.Equals(aDef.DataSourceViewId))))
//                        });
//        }

        public static String ParseUniqueName(string member)
        {
            var ExtractText = @"[\]?&\.\[]";
            var SplitValue = @"[&]";

            return Regex.Split(
                Regex.Split(member, SplitValue)[0]
                , ExtractText).Last(t => !String.IsNullOrEmpty(t));
        }

        public static IEnumerable<SchemaParser.Relation> GetJoinRelations(oDataBase olapData, IEnumerable<String> leftTables, IEnumerable<String> rightTables )
        {

            
            var dataSourceViewDef = SchemaParser.GetDataSourceViewDefinitions(olapData);

            //What we can do is pass the table objects in the constructor of the method...
            //then return the list of tables to be joined.

            //this is a test pass...

            //Find how the tables Relate to each other...
            var Relations = rightTables
                            .SelectMany(rt =>
                                dataSourceViewDef.SelectMany(dr =>
                                    dr.Relations.Where(p => p.Parent.TableName.Equals(rt) || p.Child.TableName.Equals(rt))
                                    ).Where(cr => cr.Child.TableName.Equals(leftTables.First()) || cr.Parent.TableName.Equals(leftTables.First())))
                            .ToList();

            //Find any tables that have no direct relations to the LeftTable					
            var UnlinkedTables = rightTables.Except(Relations.Select(p => p.Parent.TableName).Union(Relations.Select(p => p.Child.TableName)));

            //Get all the right table relations
            var rightTableRelations = rightTables
                .SelectMany(rt => dataSourceViewDef
                    .SelectMany(dr => dr.Relations.Where(p => p.Parent.TableName.Equals(rt) || p.Child.TableName.Equals(rt))
                    ));

            List<SchemaParser.Relation> AltRelations = new List<SchemaParser.Relation>();

            //We have three table names in the right join table
            //we need to find out how they relate to each other....
            foreach (var ut in UnlinkedTables)
            {

                var utRelations = rightTableRelations.Where(p => p.Parent.TableName.Equals(ut) || p.Child.TableName.Equals(ut));
                AltRelations.Add(utRelations.First());
                var linkingTable = utRelations.Select(p => p.Parent.TableName).Union(utRelations.Select(p => p.Child.TableName)).Where(nm => !nm.Equals(ut)).Single();

                AltRelations.Add(
                rightTableRelations
                .Where(cr => !cr.Child.TableName.Equals(ut))
                .Where(cr => !cr.Parent.TableName.Equals(ut))
                .Where(cx => cx.Child.TableName.Equals(linkingTable) || cx.Parent.TableName.Equals(linkingTable)).First()
                );

            }

            return Relations.Union(AltRelations);
        }

        public static IEnumerable<SchemaParser.TableColumn> ParseExpression(SchemaParser.TableColumn input, String expression, IEnumerable<SchemaParser.DataSourceViewDefinition> DataViews)
        {
            var pattern = @"(\w*)";
            var ExpressionColumns = Regex.Matches(expression, pattern).Cast<Match>().Select(v => v.Value).Where(v => !String.IsNullOrEmpty(v));

            if (ExpressionColumns.Any())
            {
                return DataViews
                    .SelectMany(tbl => tbl.Tables.Where(nm => nm.TableName.Equals(input.TableName)))
                    .Select(col => col.ColumnName)
                    .Intersect(ExpressionColumns)
                    .Select(kc => new SchemaParser.TableColumn
                    {
                        TableName = input.TableName,
                        ColumnName = kc
                    });
            }
            else
            {
                return new[] { input };
            }
        }

        public static IEnumerable<AttributeDefinition> GetAttributeDefinitions(oDataBase olapData)
        {
            return olapData.Dimensions.SelectMany(cDim => cDim.Attributes.Select(aInfo => new AttributeDefinition
            {
                DataSourceViewId = cDim.Source,
                DimensionName = cDim.Name,
                DimensionID = cDim.ID,
                AllAttributeName = cDim.AttributeAllMemberName,
                AttributeName = aInfo.Name,
                AttributeID = aInfo.ID,
                AttributeType = aInfo.Type,
                KeyColumn = aInfo.KeyColumns.SelectMany(kc => kc.Source),
                NameColumn = aInfo.NameColumn.SelectMany(nc => nc.Source),
            }));
        }

        public static IEnumerable<HierarchyLevelDefinition> GetHierarchyLevelDefinitions(oDataBase olapData)
        {
            return
                olapData.Dimensions.SelectMany(
                    cDim =>
                    cDim.Hierarchies.SelectMany(
                        cdH => cdH.Levels.Select(lvl => new SchemaParser.HierarchyLevelDefinition
                            {
                                DimensionId = cDim.ID,
                                DimensionName = cDim.Name,
                                DataSourceViewId = cDim.Source,
                                HierarchyName = cdH.Name,
                                HierarchyId = cdH.ID,
                                HierarchyAll = cdH.AllMemberName,
                                LevelName = lvl.Name,
                                LevelId = lvl.ID,
                                LevelSourceAttributeId = lvl.SourceAttributeID
                            })));
        }

        public static String GetExpression(oDataBase olapData, String dataSourceViewId, String tableName, String columnName)
        {

            return SchemaParser.GetDataSourceViewDefinitions(olapData)
                .Where(dsv => dsv.DataSourceViewId.Equals(dataSourceViewId))
                .SelectMany(tbl => tbl.Tables.Where(nm => nm.TableName.Equals(tableName)))
                .Where(col => col.ColumnName.Equals(columnName))
                .Select(exp => exp.Expression).FirstOrDefault();

        }


        public static IEnumerable<FilterItemDefinition> GetFilterItemDefinitions(oDataBase olapData, String CubeName, List<String> FilterList, List<String> RowList)
        {
            var OlapObjects = FilterList.Select(li => new
            {
                Dimension = li.Split('.')[0],
                Hierarchy = li.Split('.')[1],
                Attribute = li.Split('.')[2],
            }).Distinct().Union(RowList.Select(li => new
            {
                Dimension = li.Split('.')[0],
                Hierarchy = li.Split('.')[1],
                Attribute = li.Split('.')[2],
            }).Distinct());

            var ActionableCubeDataSource = olapData.Cubes.Where(cub => cub.Name.Equals(CubeName)).First();

            return ActionableCubeDataSource.Dimensions.Join(
                OlapObjects.Select(oo => oo.Dimension.Replace("[", String.Empty).Replace("]", String.Empty)),
                dimSrc => dimSrc.Name,
                ooP => ooP,
                (dimSrc, ooP) => dimSrc)
                .SelectMany(dSrc => olapData.Dimensions.Where(dim => dim.ID.Equals(dSrc.DimensionID))
                .SelectMany(cDim => cDim.Attributes.Join(
                        OlapObjects.Where(oo => oo.Dimension.Replace("[", String.Empty).Replace("]", String.Empty).Equals(dSrc.Name)),
                        source => source.Name,
                        filter => filter.Attribute.Replace("[", String.Empty).Replace("]", String.Empty),
                        (source, filter) => source
                    ).Select(cAtt => new SchemaParser.FilterItemDefinition
                    {
                        DataSourceViewId = cDim.Source,
                        DimensionName = dSrc.Name,
                        AttributeName = cAtt.Name,
                        AttributeId = cAtt.ID,
                        AttributeType = cAtt.Type,
                        AttributeKeyColumns = cAtt.KeyColumns.SelectMany(kc => kc.Source),
                        AttributeNameColumn = cAtt.NameColumn.SelectMany(kc => kc.Source),
                        DataSourceDefinition = SchemaParser.GetDataSourceViewDefinitions(olapData)
                            .Where(dsvID => dsvID.DataSourceViewId.Equals(cDim.Source))
                            .SelectMany(dsv => dsv.Tables
                                                .Where(dsvTbl =>
                                                    dsvTbl.TableName.Equals(cAtt.KeyColumns.SelectMany(kc => kc.Source.Select(tSrc => tSrc.TableName)).First())))
                    }))
                );
        }


        public class MemberListUniqueName
        {
            public String Dimension { get; set; }
            public String Hierarchy { get; set; }
            public String Attribute { get; set; }
        }

//        public static IEnumerable<SourceCacheColumnBinding[]> GetNavigationPathsWithOrphans(oDataBase olapData,
//                                                                                            string currentCubeName,
//                                                                                            String leftJoinTable,
//                                                                                            List<String>
//                                                                                                MemberListUniqueNames)
//        {
//            IEnumerable<SchemaParser.DataSourceViewDefinition> dataSourceViewDef = SchemaParser.GetDataSourceViewDefinitions(olapData);
//
//            //pointer to the measures table source
//            var MeasureTable = leftJoinTable;
//
//
//            //Get the current cuble
//            var CurrentCube = olapData.Cubes.Where(cub => cub.Name.Contains(currentCubeName)).First();
//
//            //Create a matrix of information about the items
//            var JoinSources =
//                    MemberListUniqueNames.Select(li => new MemberListUniqueName
//                    {
//                        Dimension = li.Split('.')[0].Replace("[", String.Empty).Replace("]", String.Empty),
//                        Hierarchy = li.Split('.')[1].Replace("[", String.Empty).Replace("]", String.Empty),
//                        Attribute = li.Split('.')[2].Replace("[", String.Empty).Replace("]", String.Empty),
//                    }).Distinct();
//
//            //Narrow down the list of dimension . hierarchy. attribute
//            var DimensionHash = new HashSet<String>(JoinSources.Select(js => js.Dimension));
//            var AttributeHash = new HashSet<String>(JoinSources.Select(js => js.Attribute));
//            var HierarchyHash = new HashSet<String>(JoinSources.Select(js => js.Hierarchy));
//
//            //narrow down the dimensions
//            var dimensionFilter = CurrentCube.Dimensions.Where(Dim => DimensionHash.Contains(Dim.Name)).Select(cDim => olapData.Dimensions.Where(dm => dm.ID.Equals(cDim.DimensionID)));
//
//            //generate the source for the joins
//            var SourceData =
//            dimensionFilter
//                .SelectMany(df => df
//                        .SelectMany(dAtt => dAtt.Attributes
//                            .Where(att => AttributeHash.Contains(att.Name) || HierarchyHash.Contains(att.Name))
//                            .SelectMany(att => att.KeyColumns.SelectMany(kc =>
//                            kc.Source.Select(src => new
//                            {
//                                Selector = att.Name,
//                                DimensionID = dAtt.ID,
//                                DimensionName = dAtt.Name,
//                                AttributeID = att.ID,
//                                AttributeName = att.Name,
//                                src.TableName,
//                                src.ColumnName,
//                                Relations = dataSourceViewDef.SelectMany(Rel => Rel.Relations.Where(r =>
//                                                        r.Parent.TableName.Contains(src.TableName)
//                                                        || r.Child.TableName.Contains(src.TableName)
//                                                        ))
//                            })
//                            ))));
//
//            //Get a disinct list of the tables in the source, then add in the 
//            //  table for the left hand side of the join.
//            List<String> Tables = SourceData.Select(sd => sd.TableName).ToList();
//            Tables.Add(leftJoinTable);
//
//            //build a matrix of results
//            var RelationsMatrix =
//            SourceData.SelectMany(sd => sd.Relations.Select(rel => new
//            {
//                Left = new SourceCacheColumnBinding
//                {
//                    DimensionId = sd.DimensionID,
//                    AttributeId = sd.AttributeID,
//                    TableId = rel.Parent.TableName,
//                    ColumnId = rel.Parent.ColumnName,
//                },
//                Right = new SourceCacheColumnBinding
//                {
//                    DimensionId = sd.DimensionID,
//                    AttributeId = sd.AttributeID,
//                    TableId = rel.Child.TableName,
//                    ColumnId = rel.Child.ColumnName,
//                }
//            })).ToLookup(k => new[] { k.Left.TableId, k.Right.TableId });
//
//
//            //get a matrix of known and orphaned relationships.
//            var JoinSetKnown =
//            Tables.Select(tbl => new
//            {
//                tbl,
//                IsOrphan = !RelationsMatrix.Where(mtx => mtx.Key[0].Contains(MeasureTable) || mtx.Key[1].Contains(MeasureTable)).Where(mtx => mtx.Key[0].Equals(tbl) || mtx.Key[1].Equals(tbl)).Any(),
//                RelationToMeasure = RelationsMatrix.Where(mtx => mtx.Key[0].Contains(MeasureTable) || mtx.Key[1].Contains(MeasureTable)).Where(mtx => mtx.Key[0].Equals(tbl) || mtx.Key[1].Equals(tbl))
//                                        .SelectMany(r => r.Select(rel => new { rel.Left, rel.Right }))
//            });
//
//            //get all tables that are not orphans
//            var knownTables = JoinSetKnown.Where(js => !js.IsOrphan).Select(t => t.tbl);
//
//            //get all tables that are orphans
//            var orphanTables = JoinSetKnown.Where(js => js.IsOrphan).Select(t => t.tbl);
//
//            //Find a path for the orphan tables to join
//            var OrphanSet =
//            JoinSetKnown.Where(js => !js.tbl.Equals(MeasureTable)).Where(js => js.IsOrphan).SelectMany(
//            oh => RelationsMatrix
//                    .Where(mtx => mtx.Key[0].Contains(oh.tbl) || mtx.Key[1].Contains(oh.tbl))
//                    .SelectMany(rm => RelationsMatrix
//                        .Where(mtx => mtx.Key[0].Contains(rm.Key[0]) || mtx.Key[0].Contains(rm.Key[1]) || mtx.Key[1].Contains(rm.Key[0]) || mtx.Key[1].Contains(rm.Key[1]))
//                        )
//                    .Where(rx => knownTables.Contains(rx.Key[0]) || knownTables.Contains(rx.Key[1]))
//                    .First()
//
//            ).ToList();
//
//            //add the Orphan Table back in
//            OrphanSet.Add(RelationsMatrix.Where(mtx => mtx.Key[0].Contains(orphanTables.First()) || mtx.Key[1].Contains(orphanTables.First()))
//                            .SelectMany(r => r.Select(rel => new { rel.Left, rel.Right }))
//                            .First());
//
//            
//           
//
//            IEnumerable<SourceCacheColumnBinding[]> finalJoinSet;
//
//            if (OrphanSet.Any())
//            {
//                finalJoinSet = JoinSetKnown.SelectMany(k => k.RelationToMeasure).Union(OrphanSet).Select(eg => new[]
//                {
//                    new SourceCacheColumnBinding()
//                        {
//                            DimensionId = eg.Left.DimensionId,
//                            AttributeId = eg.Left.AttributeId,
//                            TableId = eg.Left.TableId.Replace("dbo_",String.Empty),
//                            ColumnId = eg.Left.ColumnId,
//                            Type = SourceColumnType.Key
//                        },
//                        
//                    new SourceCacheColumnBinding()
//                        {
//                            DimensionId = eg.Right.DimensionId,
//                            AttributeId = eg.Right.AttributeId,
//                            TableId = eg.Right.TableId.Replace("dbo_",String.Empty),
//                            ColumnId = eg.Right.ColumnId,
//                            Type = SourceColumnType.Key
//                        }
//                });
//            }
//            else
//            {
//                finalJoinSet = JoinSetKnown.SelectMany(k => k.RelationToMeasure).Select(eg => new[]
//                {
//                    new SourceCacheColumnBinding()
//                        {
//                            DimensionId = eg.Left.DimensionId,
//                            AttributeId = eg.Left.AttributeId,
//                            TableId = eg.Left.TableId.Replace("dbo_",String.Empty),
//                            ColumnId = eg.Left.ColumnId,
//                            Type = SourceColumnType.Key
//                        },
//                        
//                    new SourceCacheColumnBinding()
//                        {
//                            DimensionId = eg.Right.DimensionId,
//                            AttributeId = eg.Right.AttributeId,
//                            TableId = eg.Right.TableId.Replace("dbo_",String.Empty),
//                            ColumnId = eg.Right.ColumnId,
//                            Type = SourceColumnType.Key
//                        }
//                });
//                
//            }
//
//            return finalJoinSet;
//
//        }
//
//        public static IEnumerable<SourceCacheColumnBinding> GetNavigationPaths(oDataBase olapData, string currentCubeName, String leftJoinTable, List<String> MemberListUniqueNames)
//        {
//	        var dataSourceViewDef = SchemaParser.GetDataSourceViewDefinitions(olapData);
//	        var CurrentCube = olapData.Cubes.Where(cub => cub.Name.Contains(currentCubeName)).First();
//
//            var JoinSources =
//                    MemberListUniqueNames.Select(li => new
//                    {
//                        Dimension = li.Split('.')[0].Replace("[", String.Empty).Replace("]", String.Empty),
//                        Hierarchy = li.Split('.')[1].Replace("[", String.Empty).Replace("]", String.Empty),
//                        Attribute = li.Split('.')[2].Replace("[", String.Empty).Replace("]", String.Empty),
//                    }).Distinct();
//
//            //SchemaParser.FindPath(dataSourceViewDef,dataSourceViewId,leftJoinTable,rightJoinTable).Dump();
//            var RelationTable =
//            dataSourceViewDef.SelectMany(Rel => Rel.Relations.Select(r => new RelationPath
//            {
//                DataSourceViewId = Rel.DataSourceViewId,
//                ParentTable = r.Parent.TableName,
//                NavigateFrom = r.Parent.ColumnName,
//                NavigateTo = r.Child.ColumnName,
//                ChildTable = r.Child.TableName,
//            }).OrderBy(p => p.ParentTable));
//
//            //Find all the navigation to the left join table:
//            var interceptLeft = RelationTable.Where(rt => rt.ParentTable.Equals(leftJoinTable) || rt.ChildTable.Equals(leftJoinTable));
//
//            var NavPath =
//            RelationTable.Join(
//                RelationTable.Where(rt => rt.ParentTable.Equals(leftJoinTable) || rt.ChildTable.Equals(leftJoinTable)),
//                left => left.ParentTable,
//                right => right.ParentTable,
//                (left, right) => new
//                {
//                    Table3 = right.ChildTable,
//                    t2NavTo = right.NavigateTo,
//                    t2NavFrom = right.NavigateFrom,
//                    Table2 = left.ParentTable,
//                    t1NavTo = left.NavigateTo,
//                    t1NavFrom = left.NavigateFrom,
//                    Table1 = left.ChildTable,
//                }
//                )
//                .Where(tmp => !tmp.Table1.Equals(tmp.Table3))
//                .Join(
//                RelationTable,
//                left => left.Table1,
//                right => right.ChildTable,
//                (left, right) => new
//                {
//                    right.ParentTable,
//                    ParentTableColumn = right.NavigateFrom,
//                    ParentIntermediaryColumn = right.NavigateTo,
//                    IntermediaryTable = left.Table1,
//                    IntermediaryColumn = left.t1NavFrom,
//                    TertiaryIntermediaryColumn = left.t1NavTo,
//                    TertiaryTable = left.Table2,
//                    TertiaryTableColumn = left.t2NavFrom,
//                    SourceIntermediaryColumn = left.t2NavTo,
//                    SourceTable = left.Table3
//                }).Distinct();
//
//
//            var dimensionFilter = CurrentCube.Dimensions.Join(
//                JoinSources,
//                src => src.Name,
//                filter => filter.Dimension,
//                (src, filter) => new
//                {
//                    src.DimensionID,
//                    src.Name,
//                    filter.Hierarchy,
//                    filter.Attribute
//                }
//            ).SelectMany(dimID => olapData.Dimensions.Where(dim => dim.ID.Equals(dimID.DimensionID))
//
//            .SelectMany(dim => dim.Attributes.Where(dAtt => dAtt.Name.Equals(dimID.Attribute) || dAtt.Name.Equals(dimID.Hierarchy))
//                    .Select(dAtt => new
//                    {
//                        dimID.DimensionID,
//
//                        dimID.Attribute,
//                        dimID.Hierarchy,
//                        DimensionSource = dim.Source,
//                        dAtt.Name,
//                        dAtt.ID,
//                        dAtt.Usage,
//                        //DataSource = dataSourceViewDef.Where(dsv => dsv.DataSourceViewId.Equals(dim.Source)).SelectMany(dRel => dRel.Relations),
//                        KeyColumns = dAtt.KeyColumns.SelectMany(kc => kc.Source.Select(kSrc => new
//                        {
//                            dimID.DimensionID,
//                            AttributeID = dAtt.ID,
//                            kSrc.TableName,
//                            kSrc.ColumnName,
//                            DataSource = (RelationTable.Where(rt => rt.ParentTable.Equals(kSrc.TableName) || rt.ChildTable.Equals(kSrc.TableName))
//                                        .Where(rt => rt.ChildTable.Equals(leftJoinTable)).Any())
//                                        ? RelationTable.Where(rt => rt.ParentTable.Equals(kSrc.TableName) || rt.ChildTable.Equals(kSrc.TableName))
//                                            .Where(rt => rt.ChildTable.Equals(leftJoinTable))
//                                        : NavPath.Where(pt => pt.ParentTable.Equals(kSrc.TableName))
//                                            .Select(np => new RelationPath
//                                            {
//                                                DataSourceViewId = "",
//                                                ParentTable = np.ParentTable,
//                                                NavigateFrom = np.ParentTableColumn,
//                                                NavigateTo = np.ParentIntermediaryColumn,
//                                                ChildTable = np.IntermediaryTable
//                                            }).Union(NavPath.Where(pt => pt.ParentTable.Equals(kSrc.TableName))
//                                            .Select(np => new RelationPath
//                                            {
//                                                DataSourceViewId = "",
//                                                ParentTable = np.IntermediaryTable,
//                                                NavigateFrom = np.IntermediaryColumn,
//                                                NavigateTo = np.TertiaryIntermediaryColumn,
//                                                ChildTable = np.TertiaryTable
//                                            })).Union(NavPath.Where(pt => pt.ParentTable.Equals(kSrc.TableName))
//                                            .Select(np => new RelationPath
//                                            {
//                                                DataSourceViewId = "",
//                                                ParentTable = np.TertiaryTable,
//                                                NavigateFrom = np.TertiaryTableColumn,
//                                                NavigateTo = np.SourceIntermediaryColumn,
//                                                ChildTable = np.SourceTable
//                                            }))
//                        })),
//                        NameColumn = dAtt.NameColumn.SelectMany(kc => kc.Source.Select(kSrc => new
//                        {
//                            kSrc.TableName,
//                            kSrc.ColumnName,
//                            DataSource = dataSourceViewDef.Where(dsv => dsv.DataSourceViewId.Equals(dim.Source))
//                                .SelectMany(dRel => dRel.Relations.Where(r => r.Child.Equals(kSrc) || r.Parent.Equals(kSrc)))
//                        }))
//
//                    }).SelectMany(kn => kn.KeyColumns.SelectMany(ds => ds.DataSource.Select(tt =>
//                    new
//                    {
//                        tt.ParentTable,
//                        tt.NavigateFrom,
//                        tt.NavigateTo,
//                        tt.ChildTable,
//                        kn.DimensionID,
//                        ds.AttributeID
//                    })))
//            ));
//            
//
//            return
//            dimensionFilter.Select(df => new SourceCacheColumnBinding
//            {
//                DimensionId = df.DimensionID,
//                AttributeId = df.AttributeID,
//                TableId = df.ParentTable,
//                ColumnId = df.NavigateFrom,
//                Type = SourceColumnType.Key
//
//            }).Union(dimensionFilter.Select(df => new SourceCacheColumnBinding
//            {
//                DimensionId = df.DimensionID,
//                AttributeId = df.AttributeID,
//                TableId = df.ChildTable,
//                ColumnId = df.NavigateTo,
//                Type = SourceColumnType.Key
//            }));
//
//            
//            
//        }

        public class RelationPath
        {
            public String DataSourceViewId { get; set; }
            public String ParentTable { get; set; }
            public String NavigateFrom { get; set; }
            public String NavigateTo { get; set; }
            public String ChildTable { get; set; }
        }

        public class FilterItemDefinition
        {
            public String DataSourceViewId { get; set; }
            public String DimensionName { get; set; }
            public String AttributeName { get; set; }
            public String AttributeId { get; set; }
            public String AttributeType { get; set; }
            public IEnumerable<TableColumn> AttributeKeyColumns { get; set; }
            public IEnumerable<TableColumn> AttributeNameColumn { get; set; }
            public IEnumerable<DataSourceViewTableDefinition> DataSourceDefinition { get; set; }

        }

        public class JoinItemSource
        {
            public String DataSourceViewId { get; set; }
            public String DimensionId { get; set; }
            public String DimensionName { get; set; }
            public String AttributeName { get; set; }
            public String AttributeId { get; set; }
            public String AttributeType { get; set; }
            public IEnumerable<SchemaParser.TableColumn> keyColumns { get; set; }
            public IEnumerable<SchemaParser.TableColumn> nameColumns { get; set; }

        }

        public class DataSourceViewDefinition
        {
            public String DataSourceViewId { get; set; }
            public IEnumerable<Relation> Relations { get; set; }
            public IEnumerable<DataSourceViewTableDefinition> Tables { get; set; }
        }

        public class DataSourceViewTableDefinition
        {
            public String TableName { get; set; }
            public String ColumnName { get; set; }
            public String Expression { get; set; }
        }

        public class Relation
        {
            public SchemaParser.TableColumn Parent { get; set; }
            public SchemaParser.TableColumn Child { get; set; }
        }

        public class HierarchyLevelDefinition
        {
            public String DimensionId { get; set; }
            public String DimensionName { get; set; }
            public String DataSourceViewId { get; set; }
            public String HierarchyName { get; set; }
            public String HierarchyId { get; set; }
            public String HierarchyAll { get; set; }
            public String LevelName { get; set; }
            public String LevelId { get; set; }
            public String LevelSourceAttributeId { get; set; }

        }

        public class MeasureSourceDefinition
        {
            public String DataSourceViewId { get; set; }
            public String MeasureGroupName { get; set; }
            public String MeasureGroupId { get; set; }
            public String MeasureName { get; set; }
            public String MeasureId { get; set; }
            public String TableName { get; set; }
            public String ColumnName { get; set; }
        }

        public class AttributeSourceDefinition
        {
            public IEnumerable<AttributeColumnSourceDefinition> KeyColumnDefinitions { get; set; }
            public IEnumerable<AttributeColumnSourceDefinition> NameColumnDefinitions { get; set; }
        }

        public class AttributeColumnSourceDefinition
        {
            public String DimensionId { get; set; }
            public String DimensionName { get; set; }
            public String DataSourceViewId { get; set; }
            public String AttributeName { get; set; }
            public String AttributeId { get; set; }
            public String Usage { get; set; }
            public String TableName { get; set; }
            public String ColumnName { get; set; }
        }

        public class RelationDetails
        {
            public IEnumerable<TableColumn> ParentTableColumns { get; set; }
            public IEnumerable<TableColumn> ChildTableColumns { get; set; }
        }

        private enum KeyColumnType
        {
            Key,
            Name
        };

        public class KeySource
        {
            public String AttributeId { get; set; }
            public String TableName { get; set; }
            public String KeyColumn { get; set; }
            public IEnumerable<ExpressionColumn> Expressions { get; set; }
            public IEnumerable<RelationDetails> ParentRelations { get; set; }
            public IEnumerable<RelationDetails> ChildRelations { get; set; }

            public static IEnumerable<KeySource> ParseKeySourceFromDataTable(DataTable tableSource)
            {
                return new KeySource[]
                    {
                        new KeySource()
                            {
                                ChildRelations = tableSource.ChildRelations.Cast<DataRelation>()
                                                            .Select(dr => new SchemaParser.RelationDetails()
                                                                {
                                                                    ParentTableColumns =
                                                                        dr.ParentColumns.Select(
                                                                            ptc =>
                                                                            new SchemaParser.TableColumn()
                                                                                {
                                                                                    TableName = dr.ParentTable.TableName,
                                                                                    ColumnName = ptc.ColumnName
                                                                                }),
                                                                    ChildTableColumns =
                                                                        dr.ChildColumns.Select(
                                                                            ptc =>
                                                                            new SchemaParser.TableColumn()
                                                                                {
                                                                                    TableName = dr.ChildTable.TableName,
                                                                                    ColumnName = ptc.ColumnName
                                                                                })
                                                                }),
                                ParentRelations = tableSource.ParentRelations.Cast<DataRelation>()
                                                             .Select(dr => new SchemaParser.RelationDetails()
                                                                 {
                                                                     ParentTableColumns =
                                                                         dr.ParentColumns.Select(
                                                                             ptc =>
                                                                             new SchemaParser.TableColumn()
                                                                                 {
                                                                                     TableName = dr.ParentTable.TableName,
                                                                                     ColumnName = ptc.ColumnName
                                                                                 }),
                                                                     ChildTableColumns =
                                                                         dr.ChildColumns.Select(
                                                                             ptc =>
                                                                             new SchemaParser.TableColumn()
                                                                                 {
                                                                                     TableName = dr.ChildTable.TableName,
                                                                                     ColumnName = ptc.ColumnName
                                                                                 })
                                                                 }),
                            },
                    };
                
              


            }


            public static IEnumerable<KeySource> ParseKeySourceFromAttributes(oDataBase olapData, AttributeDefinition[] attributes)
            {

                Dictionary<string, SchemaParser.oDataSourceView> dataSources = SchemaParser.GetAttributeDataSources(
                    olapData, attributes);

                return attributes.SelectMany(t =>
                    t.KeyColumn.SelectMany(kc => dataSources[t.DataSourceViewId].Schema.Tables.Cast<DataTable>().Where(dt => dt.TableName.Equals(kc.TableName))
                    .Select(tbl => new KeySource
                    {
                        AttributeId = t.AttributeID,
                        TableName = tbl.TableName,
                        KeyColumn = tbl.Columns.Cast<DataColumn>().Where(dc => dc.ColumnName.Equals(kc.ColumnName)).Select(kCol => kCol.ColumnName).FirstOrDefault(),
                        Expressions = tbl.Columns.Cast<DataColumn>()
                        .Where(ex => ex.ExtendedProperties["ComputedColumnExpression"] != null)
                        .Where(ex => ex.ExtendedProperties["DbColumnName"].ToString().Equals(kc.ColumnName))
                        .Select(dc => new SchemaParser.ExpressionColumn
                        {
                            ColumnName = dc.ExtendedProperties["DbColumnName"].ToString(),
                            Expression = dc.ExtendedProperties["ComputedColumnExpression"].ToString(),
                        }),
                        ParentRelations = tbl.ParentRelations.Cast<DataRelation>()
                        .Select(dr => new SchemaParser.RelationDetails()
                        {
                            ParentTableColumns = dr.ParentColumns.Select(ptc => new SchemaParser.TableColumn() { TableName = dr.ParentTable.TableName, ColumnName = ptc.ColumnName }),
                            ChildTableColumns = dr.ChildColumns.Select(ptc => new SchemaParser.TableColumn() { TableName = dr.ChildTable.TableName, ColumnName = ptc.ColumnName })
                        }),
                        ChildRelations = tbl.ChildRelations.Cast<DataRelation>()
                        .Select(dr => new SchemaParser.RelationDetails()
                        {
                            ParentTableColumns = dr.ParentColumns.Select(ptc => new SchemaParser.TableColumn() { TableName = dr.ParentTable.TableName, ColumnName = ptc.ColumnName }),
                            ChildTableColumns = dr.ChildColumns.Select(ptc => new SchemaParser.TableColumn() { TableName = dr.ChildTable.TableName, ColumnName = ptc.ColumnName })
                        }),
                    })
                ));
            }
            
            
            public static IEnumerable<KeySource> ParseNameSourceFromAttributes(oDataBase olapData, AttributeDefinition[] attributes)
            {

                Dictionary<string, SchemaParser.oDataSourceView> dataSources = SchemaParser.GetAttributeDataSources(
                    olapData, attributes);

                return attributes.SelectMany(t => 
                    t.NameColumn.SelectMany(kc => dataSources[t.DataSourceViewId].Schema.Tables.Cast<DataTable>().Where(dt => dt.TableName.Equals(kc.TableName))
                    .Select(tbl => new KeySource
                    {
                        AttributeId = t.AttributeID,
                        TableName = tbl.TableName,
                        KeyColumn = tbl.Columns.Cast<DataColumn>().Where(dc => dc.ColumnName.Equals(kc.ColumnName)).Select(kCol => kCol.ColumnName).FirstOrDefault(),
                        Expressions = tbl.Columns.Cast<DataColumn>()
                        .Where(ex => ex.ExtendedProperties["ComputedColumnExpression"] != null)
                        .Where(ex => ex.ExtendedProperties["DbColumnName"].ToString().Equals(kc.ColumnName))
                        .Select(dc => new SchemaParser.ExpressionColumn
                        {
                            ColumnName = dc.ExtendedProperties["DbColumnName"].ToString(),
                            Expression = dc.ExtendedProperties["ComputedColumnExpression"].ToString(),
                        }),
                        ParentRelations = tbl.ParentRelations.Cast<DataRelation>()
                        .Select(dr => new SchemaParser.RelationDetails()
                        {
                            ParentTableColumns = dr.ParentColumns.Select(ptc => new SchemaParser.TableColumn() { TableName = dr.ParentTable.TableName, ColumnName = ptc.ColumnName }),
                            ChildTableColumns = dr.ChildColumns.Select(ptc => new SchemaParser.TableColumn() { TableName = dr.ChildTable.TableName, ColumnName = ptc.ColumnName })
                        }),
                        ChildRelations = tbl.ChildRelations.Cast<DataRelation>()
                        .Select(dr => new SchemaParser.RelationDetails()
                        {
                            ParentTableColumns = dr.ParentColumns.Select(ptc => new SchemaParser.TableColumn() { TableName = dr.ParentTable.TableName, ColumnName = ptc.ColumnName }),
                            ChildTableColumns = dr.ChildColumns.Select(ptc => new SchemaParser.TableColumn() { TableName = dr.ChildTable.TableName, ColumnName = ptc.ColumnName })
                        }),
                    })
                ));
            }
        }

        public class ExpressionColumn
        {
            public String ColumnName { get; set; }
            public String Expression { get; set; }
        }

        public class AttributeDefinition
        {
            public String DataSourceViewId { get; set; }
            public String DimensionName { get; set; }
            public String DimensionID { get; set; }
            public String AllAttributeName { get; set; }
            public String AttributeName { get; set; }
            public String AttributeID { get; set; }
            public String AttributeType { get; set; }
            public IEnumerable<TableColumn> KeyColumn { get; set; }
            public IEnumerable<TableColumn> NameColumn { get; set; }
        }

        public class DataSourceViewExpression
        {
            public String DataSourceViewName { get; set; }
            public String DataSourceViewId { get; set; }
            public IEnumerable<ExpressionTreeItem> ExpressionTree { get; set; }
        }


        public class ExpressionTreeItem
        {
            public String TableName { get; set; }
            public String ColumnName { get; set; }
            public String Expression { get; set; }
            public String[] ExpressionTree { get; set; }
        }

        public class ComputedColumnExpression
        {

            public ComputedColumnExpression()
            {
                
            }

            public ComputedColumnExpression(String tableName, String columnName, String expression)
            {
                TableName = tableName;
                ColumnName = columnName;
                Expression = expression;
            }
            public String TableName { get; set; }
            public String ColumnName { get; set; }
            public String Expression { get; set; }
        }


        #region Generic and Common Parsers

        /// <summary>
        /// Generic Attribute handler and parser
        /// </summary>
        public class Attribute
        {
            public Attribute()
            {
            }

            public Attribute(XAttribute attribute)
            {
                Namespace = Namespaces.ParseNamespace(attribute);
                Name = attribute.Name.LocalName;
                Value = attribute.Value;
            }

            public String Namespace { get; set; }
            public String Name { get; set; }
            public String Value { get; set; }
        }

        /// <summary>
        /// Generic Element handler and parser
        /// </summary>
        public class Element
        {
            public Element()
            {
            }

            public Element(XElement element)
            {
                Namespace = Namespaces.ParseNamespace(element);
                Name = element.Name.LocalName;
                Value = (element.HasElements) ? String.Empty : element.Value;
                varRetrieval = (element.HasElements)
                                   ? String.Format("{1} = var.Element(Namespaces.{0}+\"{1}\"),",
                                                   Namespaces.ParseNamespace(element), element.Name.LocalName)
                                   : String.Format("{1} = var.Element(Namespaces.{0}+\"{1}\").Value,",
                                                   Namespaces.ParseNamespace(element), element.Name.LocalName);
                Retrieval = (element.HasElements)
                                ? String.Format("{1} = var.Element(Namespaces.{0}+\"{1}\");",
                                                Namespaces.ParseNamespace(element), element.Name.LocalName)
                                : String.Format("{1} = var.Element(Namespaces.{0}+\"{1}\").Value;",
                                                Namespaces.ParseNamespace(element), element.Name.LocalName);
                Method = (element.HasElements)
                             ? String.Format("public IEnumerable<XElement> {0} {1}", element.Name.LocalName,
                                             "{get;set;}")
                             : String.Format("public String {0} {1}", element.Name.LocalName, "{get;set;}");
                HasAttributes = element.HasAttributes;
                Attributes = (element.HasAttributes)
                                 ? element.Attributes().Select(a => new Attribute(a)).ToList()
                                 : null;
                HasChildren = element.HasElements;
                Children = (element.HasElements) ? element.Elements().Select(e => new Element(e)).ToList() : null;
            }

            public String Namespace { get; set; }
            public String Name { get; set; }
            public String Method { get; set; }
            public String Value { get; set; }
            public String varRetrieval { get; set; }
            public String Retrieval { get; set; }
            public bool HasChildren { get; set; }
            public List<Element> Children { get; set; }
            public bool HasAttributes { get; set; }
            public List<Attribute> Attributes { get; set; }

            public DataSet Convert(XElement converter)
            {
                var ds = new DataSet();
                ds.ReadXml(converter.CreateReader());
                return ds;
            }
        }

        /// <summary>
        /// Used to parse the properties of Calculations inside of
        /// the MDX Scripts for the cubes
        /// </summary>
        public class CalculationProperty
        {
            public CalculationProperty()
            {
            }

            public CalculationProperty(XElement calculationProperty)
            {
                CalculationReference = calculationProperty.Element(Namespaces.engine + "CalculationReference").Value;
                CalculationType = calculationProperty.Element(Namespaces.engine + "CalculationType").Value;
                Description = calculationProperty.Element(Namespaces.engine + "Description").Value;
                FormatString = calculationProperty.Element(Namespaces.engine + "FormatString").Value;
                ForeColor = calculationProperty.Element(Namespaces.engine + "ForeColor").Value;
                BackColor = calculationProperty.Element(Namespaces.engine + "BackColor").Value;
                FontName = calculationProperty.Element(Namespaces.engine + "FontName").Value;
                FontSize = calculationProperty.Element(Namespaces.engine + "FontSize").Value;
                FontFlags = calculationProperty.Element(Namespaces.engine + "FontFlags").Value;
                NonEmptyBehavior = calculationProperty.Element(Namespaces.engine + "NonEmptyBehavior").Value;
                AssociatedMeasureGroupID =
                    calculationProperty.Element(Namespaces.engine + "AssociatedMeasureGroupID").Value;
                DisplayFolder = calculationProperty.Element(Namespaces.engine + "DisplayFolder").Value;
                Language = calculationProperty.Element(Namespaces.engine + "Language").Value;
                Translations = calculationProperty.Element(Namespaces.engine + "Translations").Value;

            }

            public String CalculationReference { get; set; }
            public String CalculationType { get; set; }
            public String Description { get; set; }
            public String FormatString { get; set; }
            public String ForeColor { get; set; }
            public String BackColor { get; set; }
            public String FontName { get; set; }
            public String FontSize { get; set; }
            public String FontFlags { get; set; }
            public String NonEmptyBehavior { get; set; }
            public String AssociatedMeasureGroupID { get; set; }
            public String DisplayFolder { get; set; }
            public String Language { get; set; }
            public String Translations { get; set; }

        }

        /// <summary>
        /// Parser for column, table and binding type
        /// </summary>
        public class TableColumn
        {
            public TableColumn()
            {
            }

            public TableColumn(XElement source)
            {
                Type = source.Attribute(Namespaces.xsi + "type").Value;
                TableName = source.Element(Namespaces.engine + "TableID").Value;
                ColumnName = source.Element(Namespaces.engine + "ColumnID").Value;
            }

            public String Type { get; set; }
            public String TableName { get; set; }
            public String ColumnName { get; set; }

            public bool Equals(TableColumn Compare)
            {
                return
                    TableName.Equals(Compare.TableName)
                    && ColumnName.Equals(Compare.ColumnName);
            }

            public override string ToString()
            {
                return String.Format("{0}.{1}", TableName, ColumnName);
            }
        }

        /// <summary>
        /// Parser for Column metadata in AS Objects
        /// </summary>
        public class KeyColumn
        {
            public KeyColumn()
            {
            }

            public KeyColumn(XElement keyColumn)
            {
                DataType = keyColumn.Element(Namespaces.engine + "DataType").Value;
                DataSize = keyColumn.Element(Namespaces.engine + "DataSize").Value;
                NullProcessing = keyColumn.Element(Namespaces.engine + "NullProcessing").Value;
                Collation = keyColumn.Element(Namespaces.engine + "Collation").Value;
                Format = keyColumn.Element(Namespaces.engine + "Format").Value;
                Source = keyColumn.Elements(Namespaces.engine + "Source").Select(source => new TableColumn(source));
            }

            public String DataType { get; set; }
            public String DataSize { get; set; }
            public String NullProcessing { get; set; }
            public String Collation { get; set; }
            public String Format { get; set; }
            public IEnumerable<TableColumn> Source { get; set; }
        }

        /// <summary>
        /// Parser for Partition metadata in MeasureGroup Objects
        /// </summary>
        public class PartitionSource
        {
            public PartitionSource()
            {
            }

            public PartitionSource(XElement source)
            {
                Type = source.Attribute(Namespaces.xsi + "type").Value;
                Properties = source.Elements().ToDictionary(d => d.Name.LocalName, d => d.Value);
            }

            public String Type { get; set; }
            public Dictionary<String, String> Properties { get; set; }
        }

        #endregion

        #region Schema Objects

        public class oAccount
        {
            public oAccount()
            {
            }

            public oAccount(XElement account)
            {
                AccountType = account.Element(Namespaces.engine + "AccountType").Value;
                AggregationFunction = account.Element(Namespaces.engine + "AggregationFunction").Value;
                Aliases =
                    account.Element(Namespaces.engine + "Aliases")
                           .Elements(Namespaces.engine + "Alias")
                           .Select(v => v.Value)
                           .ToArray();
            }

            public String AccountType { get; set; }
            public String AggregationFunction { get; set; }
            public String[] Aliases { get; set; }
        }

        public class oAttributeRelationship
        {
            public oAttributeRelationship()
            {
            }

            public oAttributeRelationship(XElement attributeRelationship)
            {
                AttributeID = attributeRelationship.Element(Namespaces.engine + "AttributeID").Value;
                Name = attributeRelationship.Element(Namespaces.engine + "Name").Value;
                RelationshipType = attributeRelationship.Element(Namespaces.engine + "RelationshipType").Value;
                Cardinality = attributeRelationship.Element(Namespaces.engine + "Cardinality").Value;
                Optionality = attributeRelationship.Element(Namespaces.engine + "Optionality").Value;
                OverrideBehavior = attributeRelationship.Element(Namespaces.engine + "OverrideBehavior").Value;
                Translations = attributeRelationship.Element(Namespaces.engine + "Translations").Value;
            }

            public String AttributeID { get; set; }
            public String Name { get; set; }
            public String RelationshipType { get; set; }
            public String Cardinality { get; set; }
            public String Optionality { get; set; }
            public String OverrideBehavior { get; set; }
            public String Translations { get; set; }
        }

        public class oCube
        {
            public oCube()
            {
            }

            public oCube(XElement cube)
            {
                Name = cube.Element(Namespaces.engine + "Name").Value;
                ID = cube.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = cube.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = cube.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = cube.Element(Namespaces.engine + "Description").Value;
                Annotations = cube.Elements(Namespaces.engine + "Annotations");
                LastProcessed = cube.Element(Namespaces.engine + "LastProcessed").Value;
                State = cube.Element(Namespaces.engine + "State").Value;
                Language = cube.Element(Namespaces.engine + "Language").Value;
                Collation = cube.Element(Namespaces.engine + "Collation").Value;
                DefaultMeasure = cube.Element(Namespaces.engine + "DefaultMeasure").Value;
                Visible = cube.Element(Namespaces.engine + "Visible").Value;
                AggregationPrefix = cube.Element(Namespaces.engine + "AggregationPrefix").Value;
                StorageMode = cube.Element(Namespaces.engine + "StorageMode").Value;
                ProcessingMode = cube.Element(Namespaces.engine + "ProcessingMode").Value;
                ProcessingPriority = cube.Element(Namespaces.engine + "ProcessingPriority").Value;
                ScriptCacheProcessingMode = cube.Element(Namespaces.engine + "ScriptCacheProcessingMode").Value;
                ScriptErrorHandlingMode = cube.Element(Namespaces.engine + "ScriptErrorHandlingMode").Value;
                StorageLocation = cube.Element(Namespaces.engine + "StorageLocation").Value;
                EstimatedRows = cube.Element(Namespaces.engine + "EstimatedRows").Value;
                Source =
                    cube.Element(Namespaces.engine + "Source").Element(Namespaces.engine + "DataSourceViewID").Value;
                ProactiveCaching =
                    cube.Elements(Namespaces.engine + "ProactiveCaching")
                        .Select(proactiveCaching => new oProactiveCaching(proactiveCaching));
                Translations = cube.Element(Namespaces.engine + "Translations").Value;
                Dimensions =
                    cube.Elements(Namespaces.engine + "Dimensions")
                        .Elements(Namespaces.engine + "Dimension")
                        .Select(cubeDimension => new oCubeDimension(cubeDimension));
                Kpis = cube.Element(Namespaces.engine + "Kpis").Value;
                Actions = cube.Element(Namespaces.engine + "Actions").Value;
                MeasureGroups =
                    cube.Elements(Namespaces.engine + "MeasureGroups")
                        .Elements(Namespaces.engine + "MeasureGroup")
                        .Select(measureGroup => new oMeasureGroup(measureGroup));
                MdxScripts =
                    cube.Elements(Namespaces.engine + "MdxScripts")
                        .Elements(Namespaces.engine + "MdxScript")
                        .Select(mdxScript => new oMdxScript(mdxScript));
                CubePermissions =
                    cube.Elements(Namespaces.engine + "CubePermissions")
                        .Elements(Namespaces.engine + "CubePermission")
                        .Select(cubePermission => new oCubePermission(cubePermission));
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public IEnumerable<XElement> Annotations { get; set; }
            public String LastProcessed { get; set; }
            public String State { get; set; }
            public String Language { get; set; }
            public String Collation { get; set; }
            public String DefaultMeasure { get; set; }
            public String Visible { get; set; }
            public String AggregationPrefix { get; set; }
            public String StorageMode { get; set; }
            public String ProcessingMode { get; set; }
            public String ProcessingPriority { get; set; }
            public String ScriptCacheProcessingMode { get; set; }
            public String ScriptErrorHandlingMode { get; set; }
            public String StorageLocation { get; set; }
            public String EstimatedRows { get; set; }
            public String Source { get; set; }
            public IEnumerable<oProactiveCaching> ProactiveCaching { get; set; }
            public String Translations { get; set; }
            public IEnumerable<oCubeDimension> Dimensions { get; set; }
            public String Kpis { get; set; }
            public String Actions { get; set; }
            public IEnumerable<oMeasureGroup> MeasureGroups { get; set; }
            public IEnumerable<oMdxScript> MdxScripts { get; set; }
            public IEnumerable<oCubePermission> CubePermissions { get; set; }
        }

        public class oCubeDimension
        {
            public oCubeDimension()
            {
            }

            public oCubeDimension(XElement cubeDimension)
            {
                ID = cubeDimension.Element(Namespaces.engine + "ID").Value;
                Name = cubeDimension.Element(Namespaces.engine + "Name").Value;
                DimensionID = cubeDimension.Element(Namespaces.engine + "DimensionID").Value;
                Visible = cubeDimension.Element(Namespaces.engine + "Visible").Value;
                HierarchyUniqueNameStyle = cubeDimension.Element(Namespaces.engine + "HierarchyUniqueNameStyle").Value;
                MemberUniqueNameStyle = cubeDimension.Element(Namespaces.engine + "MemberUniqueNameStyle").Value;
                AllMemberAggregationUsage = cubeDimension.Element(Namespaces.engine + "AllMemberAggregationUsage").Value;
                Translations = cubeDimension.Element(Namespaces.engine + "Translations").Value;
                Attributes =
                    cubeDimension.Element(Namespaces.engine + "Attributes")
                                 .Elements(Namespaces.engine + "Attribute")
                                 .Select(cubeDimensionAttribute => new oCubeDimensionAttribute(cubeDimensionAttribute));
                Hierarchies =
                    cubeDimension.Element(Namespaces.engine + "Hierarchies")
                                 .Elements(Namespaces.engine + "Hierarchy")
                                 .Select(cubeDimensionHierarchy => new oCubeDimensionHierarchy(cubeDimensionHierarchy));
            }

            public String ID { get; set; }
            public String Name { get; set; }
            public String DimensionID { get; set; }
            public String Visible { get; set; }
            public String HierarchyUniqueNameStyle { get; set; }
            public String MemberUniqueNameStyle { get; set; }
            public String AllMemberAggregationUsage { get; set; }
            public String Translations { get; set; }
            public IEnumerable<oCubeDimensionAttribute> Attributes { get; set; }
            public IEnumerable<oCubeDimensionHierarchy> Hierarchies { get; set; }
        }

        public class oCubeDimensionAttribute
        {
            public oCubeDimensionAttribute()
            {
            }

            public oCubeDimensionAttribute(XElement cubeDimensionAttribute)
            {
                AttributeID = cubeDimensionAttribute.Element(Namespaces.engine + "AttributeID").Value;
                AggregationUsage = cubeDimensionAttribute.Element(Namespaces.engine + "AggregationUsage").Value;
                AttributeHierarchyEnabled =
                    cubeDimensionAttribute.Element(Namespaces.engine + "AttributeHierarchyEnabled").Value;
                AttributeHierarchyVisible =
                    cubeDimensionAttribute.Element(Namespaces.engine + "AttributeHierarchyVisible").Value;
                AttributeHierarchyOptimizedState =
                    cubeDimensionAttribute.Element(Namespaces.engine + "AttributeHierarchyOptimizedState").Value;

            }

            public String AttributeID { get; set; }
            public String AggregationUsage { get; set; }
            public String AttributeHierarchyEnabled { get; set; }
            public String AttributeHierarchyVisible { get; set; }
            public String AttributeHierarchyOptimizedState { get; set; }
        }

        public class oCubeDimensionHierarchy
        {
            public oCubeDimensionHierarchy()
            {
            }

            public oCubeDimensionHierarchy(XElement cubeDimensionHierarchy)
            {
                HierarchyID = cubeDimensionHierarchy.Element(Namespaces.engine + "HierarchyID").Value;
                Enabled = cubeDimensionHierarchy.Element(Namespaces.engine + "Enabled").Value;
                Visible = cubeDimensionHierarchy.Element(Namespaces.engine + "Visible").Value;
                OptimizedState = cubeDimensionHierarchy.Element(Namespaces.engine + "OptimizedState").Value;

            }

            public String HierarchyID { get; set; }
            public String Enabled { get; set; }
            public String Visible { get; set; }
            public String OptimizedState { get; set; }

        }

        public class oCubePermission
        {
            public oCubePermission()
            {
            }

            public oCubePermission(XElement cubePermission)
            {
                Name = cubePermission.Element(Namespaces.engine + "Name").Value;
                ID = cubePermission.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = cubePermission.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = cubePermission.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = cubePermission.Element(Namespaces.engine + "Description").Value;
                RoleID = cubePermission.Element(Namespaces.engine + "RoleID").Value;
                ReadDefinition = cubePermission.Element(Namespaces.engine + "ReadDefinition").Value;
                Process = cubePermission.Element(Namespaces.engine + "Process").Value;
                Read = cubePermission.Element(Namespaces.engine + "Read").Value;
                ReadSourceData = cubePermission.Element(Namespaces.engine + "ReadSourceData").Value;
                Write = cubePermission.Element(Namespaces.engine + "Write").Value;
                DimensionPermissions = cubePermission.Element(Namespaces.engine + "DimensionPermissions").Value;
                CellPermissions = cubePermission.Element(Namespaces.engine + "CellPermissions").Value;
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public String RoleID { get; set; }
            public String ReadDefinition { get; set; }
            public String Process { get; set; }
            public String Read { get; set; }
            public String ReadSourceData { get; set; }
            public String Write { get; set; }
            public String DimensionPermissions { get; set; }
            public String CellPermissions { get; set; }

        }

        public class oDataBase
        {
            public oDataBase()
            {
            }

            public oDataBase(XElement d)
            {

                Name = d.Element(Namespaces.engine + "Name").Value;
                ID = d.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = d.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = d.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = d.Element(Namespaces.engine + "Description").Value;
                LastProcessed = d.Element(Namespaces.engine + "LastProcessed").Value;
                State = d.Element(Namespaces.engine + "State").Value;
                LastUpdate = d.Element(Namespaces.engine + "LastUpdate").Value;
                AggregationPrefix = d.Element(Namespaces.engine + "AggregationPrefix").Value;
                Language = d.Element(Namespaces.engine + "Language").Value;
                Collation = d.Element(Namespaces.engine + "Collation").Value;
                Visible = d.Element(Namespaces.engine + "Visible").Value;
                MasterDataSourceID = d.Element(Namespaces.engine + "MasterDataSourceID").Value;
                ProcessingPriority = d.Element(Namespaces.engine + "ProcessingPriority").Value;
                CompatibilityLevel = d.Element(Namespaces.engine200 + "CompatibilityLevel").Value;
                Accounts =
                    d.Elements(Namespaces.engine + "Accounts")
                     .Elements(Namespaces.engine + "Account")
                     .Select(account => new oAccount(account));
                EstimatedSize = d.Element(Namespaces.engine + "EstimatedSize").Value;
                Translations = d.Element(Namespaces.engine + "Translations").Value;
                DataSourceImpersonationInfo =
                    d.Elements(Namespaces.engine + "DataSourceImpersonationInfo")
                     .Select(ImpersonationInfo => new oDataSourceImpersonationInfo(ImpersonationInfo));

                Dimensions = d
                    .Elements(Namespaces.engine + "Dimensions")
                    .SelectMany(
                        Dimension =>
                        Dimension.Elements(Namespaces.engine + "Dimension").Select(Detail => new oDimension(Detail)));

                DataSourceViews =
                    d.Elements(Namespaces.engine + "DataSourceViews")
                     .Elements(Namespaces.engine + "DataSourceView")
                     .Select(DataSourceView => new oDataSourceView(DataSourceView));

                Cubes =
                    d.Elements(Namespaces.engine + "Cubes")
                     .Elements(Namespaces.engine + "Cube")
                     .Select(cube => new oCube(cube));

                MiningStructures =
                    d.Elements(Namespaces.engine + "MiningStructures")
                     .Elements(Namespaces.engine + "MiningStructure")
                     .Select(MiningStructure => new oMiningStructure(MiningStructure)); //MiningStructure

                DataSources =
                    d.Elements(Namespaces.engine + "DataSources")
                     .Elements(Namespaces.engine + "DataSource")
                     .Select(dataSource => new oDataSource(dataSource));

                Roles =
                    d.Elements(Namespaces.engine + "Roles")
                     .Elements(Namespaces.engine + "Role")
                     .Select(role => new oRole(role));

                DatabasePermissions =
                    d.Elements(Namespaces.engine + "DatabasePermissions")
                     .Elements(Namespaces.engine + "DatabasePermission")
                     .Select(permission => new oDatabasePermissions(permission));

                SchemaSource = d;
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public String LastProcessed { get; set; }
            public String State { get; set; }
            public String LastUpdate { get; set; }
            public String AggregationPrefix { get; set; }
            public String Language { get; set; }
            public String Collation { get; set; }
            public String Visible { get; set; }
            public String MasterDataSourceID { get; set; }
            public String ProcessingPriority { get; set; }
            public String CompatibilityLevel { get; set; }
            public IEnumerable<oAccount> Accounts { get; set; }
            public String EstimatedSize { get; set; }
            public String Translations { get; set; }
            public IEnumerable<oDataSourceImpersonationInfo> DataSourceImpersonationInfo { get; set; }
            public IEnumerable<oDimension> Dimensions { get; set; }
            public IEnumerable<oCube> Cubes { get; set; }
            public IEnumerable<oMiningStructure> MiningStructures { get; set; }
            public IEnumerable<oDataSource> DataSources { get; set; }
            public IEnumerable<oDataSourceView> DataSourceViews { get; set; }
            public IEnumerable<oRole> Roles { get; set; }
            public IEnumerable<oDatabasePermissions> DatabasePermissions { get; set; }
            public XElement SchemaSource { get; set; }

        }

        public class oDatabasePermissions
        {
            public oDatabasePermissions()
            {
            }

            public oDatabasePermissions(XElement permission)
            {
                Name = permission.Element(Namespaces.engine + "Name").Value;
                ID = permission.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = permission.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = permission.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = permission.Element(Namespaces.engine + "Description").Value;
                RoleID = permission.Element(Namespaces.engine + "RoleID").Value;
                ReadDefinition = permission.Element(Namespaces.engine + "ReadDefinition").Value;
                Process = permission.Element(Namespaces.engine + "Process").Value;
                Read = permission.Element(Namespaces.engine + "Read").Value;
                Administer = permission.Element(Namespaces.engine + "Administer").Value;
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public String RoleID { get; set; }
            public String ReadDefinition { get; set; }
            public String Process { get; set; }
            public String Read { get; set; }
            public String Administer { get; set; }

        }

        public class oDataSource
        {
            public oDataSource()
            {
            }

            public oDataSource(XElement dataSource)
            {
                Type = dataSource.Attribute(Namespaces.xsi + "type").Value;
                Name = dataSource.Element(Namespaces.engine + "Name").Value;
                ID = dataSource.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = dataSource.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = dataSource.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = dataSource.Element(Namespaces.engine + "Description").Value;
                ConnectionString = dataSource.Element(Namespaces.engine + "ConnectionString").Value;
                ConnectionStringSecurity = dataSource.Element(Namespaces.engine + "ConnectionStringSecurity").Value;
                Timeout = dataSource.Element(Namespaces.engine + "Timeout").Value;
                ImpersonationInfo =
                    dataSource.Elements(Namespaces.engine + "ImpersonationInfo")
                              .Select(impersonationInfo => new oDataSourceImpersonationInfo(impersonationInfo));
                MaxActiveConnections = dataSource.Element(Namespaces.engine + "MaxActiveConnections").Value;
                Isolation = dataSource.Element(Namespaces.engine + "Isolation").Value;
                DataSourcePermissions =
                    dataSource.Elements(Namespaces.engine + "DataSourcePermissions")
                              .Elements(Namespaces.engine + "DataSourcePermission")
                              .Select(dataSourcePermission => new oDataSourcePermission(dataSourcePermission));
            }

            public String Type { get; set; }
            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public String ConnectionString { get; set; }
            public String ConnectionStringSecurity { get; set; }
            public String Timeout { get; set; }
            public IEnumerable<oDataSourceImpersonationInfo> ImpersonationInfo { get; set; }
            public String MaxActiveConnections { get; set; }
            public String Isolation { get; set; }
            public IEnumerable<oDataSourcePermission> DataSourcePermissions { get; set; }

        }

        public class oDataSourceImpersonationInfo
        {
            public oDataSourceImpersonationInfo()
            {
            }

            public oDataSourceImpersonationInfo(XElement ImpersonationInfo)
            {
                ImpersonationMode = ImpersonationInfo.Element(Namespaces.engine + "ImpersonationMode").Value;
                Account = ImpersonationInfo.Element(Namespaces.engine + "Account").Value;
                Password = ImpersonationInfo.Element(Namespaces.engine + "Password").Value;
                ImpersonationInfoSecurity =
                    ImpersonationInfo.Element(Namespaces.engine + "ImpersonationInfoSecurity").Value;
            }

            public String ImpersonationMode { get; set; }
            public String Account { get; set; }
            public String Password { get; set; }
            public String ImpersonationInfoSecurity { get; set; }

        }

        public class oDataSourcePermission
        {
            public oDataSourcePermission()
            {
            }

            public oDataSourcePermission(XElement dataSourcePermission)
            {
                Name = dataSourcePermission.Element(Namespaces.engine + "Name").Value;
                ID = dataSourcePermission.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = dataSourcePermission.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = dataSourcePermission.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = dataSourcePermission.Element(Namespaces.engine + "Description").Value;
                RoleID = dataSourcePermission.Element(Namespaces.engine + "RoleID").Value;
                ReadDefinition = dataSourcePermission.Element(Namespaces.engine + "ReadDefinition").Value;
                Process = dataSourcePermission.Element(Namespaces.engine + "Process").Value;
                Read = dataSourcePermission.Element(Namespaces.engine + "Read").Value;
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public String RoleID { get; set; }
            public String ReadDefinition { get; set; }
            public String Process { get; set; }
            public String Read { get; set; }

        }

        public class oDataSourceView
        {
            public oDataSourceView()
            {
            }

            public oDataSourceView(XElement DataSourceView)
            {
                Name = DataSourceView.Element(Namespaces.engine + "Name").Value;
                ID = DataSourceView.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = DataSourceView.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = DataSourceView.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = DataSourceView.Element(Namespaces.engine + "Description").Value;
                Annotations = DataSourceView.Elements(Namespaces.engine + "Annotations");
                Schema = new Element().Convert(DataSourceView.Element(Namespaces.engine + "Schema"));
                DataSourceID = DataSourceView.Element(Namespaces.engine + "DataSourceID").Value;

            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public IEnumerable<XElement> Annotations { get; set; }
            public DataSet Schema { get; set; }
            public String DataSourceID { get; set; }
        }

        public class oDimension
        {
            public oDimension()
            {
            }

            public oDimension(XElement DimensionDetail)
            {
                Name = DimensionDetail.Element(Namespaces.engine + "Name").Value;
                ID = DimensionDetail.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = DimensionDetail.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = DimensionDetail.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = DimensionDetail.Element(Namespaces.engine + "Description").Value;
                Annotations = DimensionDetail.Elements(Namespaces.engine + "Annotations");
                LastProcessed = DimensionDetail.Element(Namespaces.engine + "LastProcessed").Value;
                State = DimensionDetail.Element(Namespaces.engine + "State").Value;
                Source =
                    DimensionDetail.Element(Namespaces.engine + "Source")
                                   .Element(Namespaces.engine + "DataSourceViewID")
                                   .Value;
                MiningModelID = DimensionDetail.Element(Namespaces.engine + "MiningModelID").Value;
                Type = DimensionDetail.Element(Namespaces.engine + "Type").Value;
                UnknownMember = DimensionDetail.Element(Namespaces.engine + "UnknownMember").Value;
                MdxMissingMemberMode = DimensionDetail.Element(Namespaces.engine + "MdxMissingMemberMode").Value;
                StringStoresCompatibilityLevel =
                    DimensionDetail.Element(Namespaces.engine300 + "StringStoresCompatibilityLevel").Value;
                CurrentStringStoresCompatibilityLevel =
                    DimensionDetail.Element(Namespaces.engine300 + "CurrentStringStoresCompatibilityLevel").Value;
                StorageMode = DimensionDetail.Element(Namespaces.engine + "StorageMode").Value;
                CurrentStorageMode = DimensionDetail.Element(Namespaces.engine + "CurrentStorageMode").Value;
                ProcessingPriority = DimensionDetail.Element(Namespaces.engine + "ProcessingPriority").Value;
                WriteEnabled = DimensionDetail.Element(Namespaces.engine + "WriteEnabled").Value;
                DependsOnDimensionID = DimensionDetail.Element(Namespaces.engine + "DependsOnDimensionID").Value;
                Language = DimensionDetail.Element(Namespaces.engine + "Language").Value;
                Collation = DimensionDetail.Element(Namespaces.engine + "Collation").Value;
                UnknownMemberName = DimensionDetail.Element(Namespaces.engine + "UnknownMemberName").Value;
                ProcessingMode = DimensionDetail.Element(Namespaces.engine + "ProcessingMode").Value;
                AttributeAllMemberName = DimensionDetail.Element(Namespaces.engine + "AttributeAllMemberName").Value;
                UnknownMemberTranslations =
                    DimensionDetail.Element(Namespaces.engine + "UnknownMemberTranslations").Value;
                AttributeAllMemberTranslations =
                    DimensionDetail.Element(Namespaces.engine + "AttributeAllMemberTranslations").Value;
                Translations = DimensionDetail.Element(Namespaces.engine + "Translations").Value;
                ProactiveCaching =
                    DimensionDetail.Elements(Namespaces.engine + "ProactiveCaching")
                                   .Select(proactiveCaching => new oProactiveCaching(proactiveCaching));
                Attributes =
                    DimensionDetail.Elements(Namespaces.engine + "Attributes")
                                   .Elements(Namespaces.engine + "Attribute")
                                   .Select(dimensionAttribute => new oDimensionAttribute(dimensionAttribute));
                Hierarchies =
                    DimensionDetail.Elements(Namespaces.engine + "Hierarchies")
                                   .Elements(Namespaces.engine + "Hierarchy")
                                   .Select(dimensionHierarchy => new oDimensionHierarchy(dimensionHierarchy));


            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public IEnumerable<XElement> Annotations { get; set; }
            public String LastProcessed { get; set; }
            public String State { get; set; }
            public String Source { get; set; }
            public String MiningModelID { get; set; }
            public String Type { get; set; }
            public String UnknownMember { get; set; }
            public String MdxMissingMemberMode { get; set; }
            public String StringStoresCompatibilityLevel { get; set; }
            public String CurrentStringStoresCompatibilityLevel { get; set; }
            public String StorageMode { get; set; }
            public String CurrentStorageMode { get; set; }
            public String ProcessingPriority { get; set; }
            public String WriteEnabled { get; set; }
            public String DependsOnDimensionID { get; set; }
            public String Language { get; set; }
            public String Collation { get; set; }
            public String UnknownMemberName { get; set; }
            public String ProcessingMode { get; set; }
            public String AttributeAllMemberName { get; set; }
            public IEnumerable<oProactiveCaching> ProactiveCaching { get; set; }
            public String UnknownMemberTranslations { get; set; }
            public String AttributeAllMemberTranslations { get; set; }
            public String Translations { get; set; }
            public IEnumerable<oDimensionAttribute> Attributes { get; set; }
            public IEnumerable<oDimensionHierarchy> Hierarchies { get; set; }
            //public String Hierarchies {get;set;} 

        }

        public class oDimensionAttribute
        {
            public oDimensionAttribute()
            {
            }

            public oDimensionAttribute(XElement dimensionAttribute)
            {
                Name = dimensionAttribute.Element(Namespaces.engine + "Name").Value;
                ID = dimensionAttribute.Element(Namespaces.engine + "ID").Value;
                Description = dimensionAttribute.Element(Namespaces.engine + "Description").Value;
                Type = dimensionAttribute.Element(Namespaces.engine + "Type").Value;
                Usage = dimensionAttribute.Element(Namespaces.engine + "Usage").Value;
                EstimatedCount = dimensionAttribute.Element(Namespaces.engine + "EstimatedCount").Value;
                DiscretizationMethod = dimensionAttribute.Element(Namespaces.engine + "DiscretizationMethod").Value;
                DiscretizationBucketCount =
                    dimensionAttribute.Element(Namespaces.engine + "DiscretizationBucketCount").Value;
                OrderBy = dimensionAttribute.Element(Namespaces.engine + "OrderBy").Value;
                InstanceSelection = dimensionAttribute.Element(Namespaces.engine + "InstanceSelection").Value;
                DefaultMember = dimensionAttribute.Element(Namespaces.engine + "DefaultMember").Value;
                OrderByAttributeID = dimensionAttribute.Element(Namespaces.engine + "OrderByAttributeID").Value;
                NamingTemplate = dimensionAttribute.Element(Namespaces.engine + "NamingTemplate").Value;
                MembersWithData = dimensionAttribute.Element(Namespaces.engine + "MembersWithData").Value;
                MembersWithDataCaption = dimensionAttribute.Element(Namespaces.engine + "MembersWithDataCaption").Value;
                MemberNamesUnique = dimensionAttribute.Element(Namespaces.engine + "MemberNamesUnique").Value;
                KeyUniquenessGuarantee = dimensionAttribute.Element(Namespaces.engine + "KeyUniquenessGuarantee").Value;
                IsAggregatable = dimensionAttribute.Element(Namespaces.engine + "IsAggregatable").Value;
                AttributeHierarchyEnabled =
                    dimensionAttribute.Element(Namespaces.engine + "AttributeHierarchyEnabled").Value;
                AttributeHierarchyVisible =
                    dimensionAttribute.Element(Namespaces.engine + "AttributeHierarchyVisible").Value;
                AttributeHierarchyOrdered =
                    dimensionAttribute.Element(Namespaces.engine + "AttributeHierarchyOrdered").Value;
                AttributeHierarchyOptimizedState =
                    dimensionAttribute.Element(Namespaces.engine + "AttributeHierarchyOptimizedState").Value;
                AttributeHierarchyDisplayFolder =
                    dimensionAttribute.Element(Namespaces.engine + "AttributeHierarchyDisplayFolder").Value;
                GroupingBehavior = dimensionAttribute.Element(Namespaces.engine + "GroupingBehavior").Value;
                KeyColumns =
                    dimensionAttribute.Elements(Namespaces.engine + "KeyColumns")
                                      .Elements(Namespaces.engine + "KeyColumn")
                                      .Select(keyColumn => new KeyColumn(keyColumn));
                NameColumn =
                    dimensionAttribute.Elements(Namespaces.engine + "NameColumn")
                                      .Select(nameColumn => new KeyColumn(nameColumn));
                Translations = dimensionAttribute.Element(Namespaces.engine + "Translations").Value;
                NamingTemplateTranslations =
                    dimensionAttribute.Element(Namespaces.engine + "NamingTemplateTranslations").Value;
                //AttributeRelationshipValue = dimensionAttribute.Element(Namespaces.engine+"AttributeRelationships").Value; 
                AttributeRelationships =
                    dimensionAttribute.Elements(Namespaces.engine + "AttributeRelationships")
                                      .Elements(Namespaces.engine + "AttributeRelationship")
                                      .Select(attributeRelationship => new oAttributeRelationship(attributeRelationship));
                RootMemberIf = (dimensionAttribute.Element(Namespaces.engine + "RootMemberIf") == null)
                                   ? String.Empty
                                   : dimensionAttribute.Element(Namespaces.engine + "RootMemberIf").Value;
                UnaryOperatorColumn =
                    dimensionAttribute.Elements(Namespaces.engine + "UnaryOperatorColumn")
                                      .Select(unaryOperatorColumn => new oUnaryOperatorColumn(unaryOperatorColumn));
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String Description { get; set; }
            public String Type { get; set; }
            public String Usage { get; set; }
            public String EstimatedCount { get; set; }
            public String DiscretizationMethod { get; set; }
            public String DiscretizationBucketCount { get; set; }
            public String OrderBy { get; set; }
            public String InstanceSelection { get; set; }
            public String DefaultMember { get; set; }
            public String OrderByAttributeID { get; set; }
            public String NamingTemplate { get; set; }
            public String MembersWithData { get; set; }
            public String MembersWithDataCaption { get; set; }
            public String MemberNamesUnique { get; set; }
            public String KeyUniquenessGuarantee { get; set; }
            public String IsAggregatable { get; set; }
            public String AttributeHierarchyEnabled { get; set; }
            public String AttributeHierarchyVisible { get; set; }
            public String AttributeHierarchyOrdered { get; set; }
            public String AttributeHierarchyOptimizedState { get; set; }
            public String AttributeHierarchyDisplayFolder { get; set; }
            public String GroupingBehavior { get; set; }
            public IEnumerable<KeyColumn> KeyColumns { get; set; }
            public IEnumerable<KeyColumn> NameColumn { get; set; }
            public String Translations { get; set; }
            public String NamingTemplateTranslations { get; set; }
            public IEnumerable<oAttributeRelationship> AttributeRelationships { get; set; }
            public String RootMemberIf { get; set; }
            public IEnumerable<oUnaryOperatorColumn> UnaryOperatorColumn { get; set; }
        }

        public class oDimensionHierarchy
        {
            public oDimensionHierarchy(XElement dimensionHierarchy)
            {
                Name = dimensionHierarchy.Element(Namespaces.engine + "Name").Value;
                ID = dimensionHierarchy.Element(Namespaces.engine + "ID").Value;
                Description = dimensionHierarchy.Element(Namespaces.engine + "Description").Value;
                DisplayFolder = dimensionHierarchy.Element(Namespaces.engine + "DisplayFolder").Value;
                AllMemberName = dimensionHierarchy.Element(Namespaces.engine + "AllMemberName").Value;
                MemberNamesUnique = dimensionHierarchy.Element(Namespaces.engine + "MemberNamesUnique").Value;
                AllowDuplicateNames = dimensionHierarchy.Element(Namespaces.engine + "AllowDuplicateNames").Value;
                MemberKeysUnique = dimensionHierarchy.Element(Namespaces.engine2 + "MemberKeysUnique").Value;
                AllMemberTranslations = dimensionHierarchy.Element(Namespaces.engine + "AllMemberTranslations").Value;
                Translations = dimensionHierarchy.Element(Namespaces.engine + "Translations").Value;
                Levels =
                    dimensionHierarchy.Element(Namespaces.engine + "Levels")
                                      .Elements(Namespaces.engine + "Level")
                                      .Select(hierarchyLevel => new oHierarchyLevel(hierarchyLevel));
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String Description { get; set; }
            public String DisplayFolder { get; set; }
            public String AllMemberName { get; set; }
            public String MemberNamesUnique { get; set; }
            public String AllowDuplicateNames { get; set; }
            public String MemberKeysUnique { get; set; }
            public String AllMemberTranslations { get; set; }
            public String Translations { get; set; }
            public IEnumerable<oHierarchyLevel> Levels { get; set; }

        }

        public class oHierarchyLevel
        {
            public oHierarchyLevel()
            {
            }

            public oHierarchyLevel(XElement herarchyLevel)
            {
                Name = herarchyLevel.Element(Namespaces.engine + "Name").Value;
                ID = herarchyLevel.Element(Namespaces.engine + "ID").Value;
                Description = herarchyLevel.Element(Namespaces.engine + "Description").Value;
                SourceAttributeID = herarchyLevel.Element(Namespaces.engine + "SourceAttributeID").Value;
                HideMemberIf = herarchyLevel.Element(Namespaces.engine + "HideMemberIf").Value;
                Translations = herarchyLevel.Element(Namespaces.engine + "Translations").Value;

            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String Description { get; set; }
            public String SourceAttributeID { get; set; }
            public String HideMemberIf { get; set; }
            public String Translations { get; set; }

        }

        public class oMdxScript
        {
            public oMdxScript()
            {
            }

            public oMdxScript(XElement mdxScript)
            {
                Name = mdxScript.Element(Namespaces.engine + "Name").Value;
                ID = mdxScript.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = mdxScript.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = mdxScript.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = mdxScript.Element(Namespaces.engine + "Description").Value;
                DefaultScript = mdxScript.Element(Namespaces.engine + "DefaultScript").Value;
                Commands =
                    mdxScript.Element(Namespaces.engine + "Commands")
                             .Elements(Namespaces.engine + "Command")
                             .Elements(Namespaces.engine + "Text")
                             .Select(text => text.Value);
                CalculationProperties =
                    mdxScript.Element(Namespaces.engine + "CalculationProperties")
                             .Elements(Namespaces.engine + "CalculationProperty")
                             .Select(calculationProperty => new CalculationProperty(calculationProperty));
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public String DefaultScript { get; set; }
            public IEnumerable<String> Commands { get; set; }
            public IEnumerable<CalculationProperty> CalculationProperties { get; set; }

        }

        public class oMeasureGroup
        {
            public oMeasureGroup()
            {
            }

            public oMeasureGroup(XElement measureGroup)
            {
                Name = measureGroup.Element(Namespaces.engine + "Name").Value;
                ID = measureGroup.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = measureGroup.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = measureGroup.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = measureGroup.Element(Namespaces.engine + "Description").Value;
                LastProcessed = measureGroup.Element(Namespaces.engine + "LastProcessed").Value;
                State = measureGroup.Element(Namespaces.engine + "State").Value;
                Type = measureGroup.Element(Namespaces.engine + "Type").Value;
                DataAggregation = measureGroup.Element(Namespaces.engine + "DataAggregation").Value;
                AggregationPrefix = measureGroup.Element(Namespaces.engine + "AggregationPrefix").Value;
                StorageMode = measureGroup.Element(Namespaces.engine + "StorageMode").Value;
                ProcessingPriority = measureGroup.Element(Namespaces.engine + "ProcessingPriority").Value;
                StorageLocation = measureGroup.Element(Namespaces.engine + "StorageLocation").Value;
                IgnoreUnrelatedDimensions = measureGroup.Element(Namespaces.engine + "IgnoreUnrelatedDimensions").Value;
                EstimatedRows = measureGroup.Element(Namespaces.engine + "EstimatedRows").Value;
                ProcessingMode = measureGroup.Element(Namespaces.engine + "ProcessingMode").Value;
                ProactiveCaching =
                    measureGroup.Elements(Namespaces.engine + "ProactiveCaching")
                                .Select(proactiveCaching => new oProactiveCaching(proactiveCaching));
                EstimatedSize = measureGroup.Element(Namespaces.engine + "EstimatedSize").Value;
                Translations = measureGroup.Element(Namespaces.engine + "Translations").Value;
                Measures =
                    measureGroup.Elements(Namespaces.engine + "Measures")
                                .Elements(Namespaces.engine + "Measure")
                                .Select(measure => new oMeasureGroupMeasure(measure));
                Dimensions =
                    measureGroup.Elements(Namespaces.engine + "Dimensions")
                                .Elements(Namespaces.engine + "Dimension")
                                .Select(measureGroupDimension => new oMeasureGroupDimension(measureGroupDimension));
                AggregationDesigns =
                    measureGroup.Elements(Namespaces.engine + "AggregationDesigns")
                                .Elements(Namespaces.engine + "AggregationDesign");
                Partitions =
                    measureGroup.Elements(Namespaces.engine + "Partitions")
                                .Elements(Namespaces.engine + "Partition")
                                .Select(measureGroupPartition => new oMeasureGroupPartition(measureGroupPartition));

            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public String LastProcessed { get; set; }
            public String State { get; set; }
            public String Type { get; set; }
            public String DataAggregation { get; set; }
            public String AggregationPrefix { get; set; }
            public String StorageMode { get; set; }
            public String ProcessingPriority { get; set; }
            public String StorageLocation { get; set; }
            public String IgnoreUnrelatedDimensions { get; set; }
            public String EstimatedRows { get; set; }
            public String ProcessingMode { get; set; }
            public IEnumerable<oProactiveCaching> ProactiveCaching { get; set; }
            public String EstimatedSize { get; set; }
            public IEnumerable<oMeasureGroupMeasure> Measures { get; set; }
            public IEnumerable<oMeasureGroupDimension> Dimensions { get; set; }
            public String Translations { get; set; }
            public IEnumerable<XElement> AggregationDesigns { get; set; }
            public IEnumerable<oMeasureGroupPartition> Partitions { get; set; }
        }

        public class oMeasureGroupDimension
        {
            public oMeasureGroupDimension()
            {
            }

            public oMeasureGroupDimension(XElement measureGroupDimension)
            {
                Attributes =
                    measureGroupDimension.Elements(Namespaces.engine + "Attributes")
                                         .Elements(Namespaces.engine + "Attribute")
                                         .Select(
                                             measureGroupDimensionAttribute =>
                                             new oMeasureGroupDimensionAttribute(measureGroupDimensionAttribute));
                Cardinality = (measureGroupDimension.Element(Namespaces.engine + "Cardinality") == null)
                                  ? String.Empty
                                  : measureGroupDimension.Element(Namespaces.engine + "Cardinality").Value;
                CubeDimensionID = measureGroupDimension.Element(Namespaces.engine + "CubeDimensionID").Value;
                MeasureGroupID = (measureGroupDimension.Element(Namespaces.engine + "MeasureGroupID") == null)
                                     ? String.Empty
                                     : measureGroupDimension.Element(Namespaces.engine + "MeasureGroupID").Value;
                DirectSlice = (measureGroupDimension.Element(Namespaces.engine + "DirectSlice") == null)
                                  ? String.Empty
                                  : measureGroupDimension.Element(Namespaces.engine + "DirectSlice").Value;

            }

            public String Cardinality { get; set; }
            public String CubeDimensionID { get; set; }
            public String MeasureGroupID { get; set; }
            public String DirectSlice { get; set; }
            public IEnumerable<oMeasureGroupDimensionAttribute> Attributes { get; set; }

        }

        public class oMeasureGroupDimensionAttribute
        {
            public oMeasureGroupDimensionAttribute()
            {
            }

            public oMeasureGroupDimensionAttribute(XElement measureGroupDimensionAttribute)
            {
                AttributeID = measureGroupDimensionAttribute.Element(Namespaces.engine + "AttributeID").Value;
                Type = measureGroupDimensionAttribute.Element(Namespaces.engine + "Type").Value;
                KeyColumns =
                    measureGroupDimensionAttribute.Elements(Namespaces.engine + "KeyColumns")
                                                  .Elements(Namespaces.engine + "KeyColumn")
                                                  .Select(keyColumn => new KeyColumn
                                                      {
                                                          DataType =
                                                              keyColumn.Element(Namespaces.engine + "DataType").Value,
                                                          DataSize =
                                                              keyColumn.Element(Namespaces.engine + "DataSize").Value,
                                                          NullProcessing =
                                                              keyColumn.Element(Namespaces.engine + "NullProcessing")
                                                                       .Value,
                                                          Collation =
                                                              keyColumn.Element(Namespaces.engine + "Collation").Value,
                                                          Format = keyColumn.Element(Namespaces.engine + "Format").Value,
                                                          Source =
                                                              keyColumn.Elements(Namespaces.engine + "Source")
                                                                       .Select(source => new TableColumn
                                                                           {
                                                                               Type =
                                                                                   source.Attribute(Namespaces.xsi +
                                                                                                    "type").Value,
                                                                               ColumnName =
                                                                                   (source.Element(Namespaces.engine +
                                                                                                   "ColumnID") == null)
                                                                                       ? String.Empty
                                                                                       : source.Element(
                                                                                           Namespaces.engine +
                                                                                           "ColumnID").Value,
                                                                               TableName =
                                                                                   (source.Element(Namespaces.engine +
                                                                                                   "TableID") == null)
                                                                                       ? String.Empty
                                                                                       : source.Element(
                                                                                           Namespaces.engine + "TableID")
                                                                                               .Value
                                                                           })
                                                          ,
                                                      }
                        ); //.Select(keyColumn => new KeyColumn(keyColumn)); 

            }

            public String AttributeID { get; set; }
            public String Type { get; set; }
            public IEnumerable<object> KeyColumns { get; set; }

        }

        public class oMeasureGroupMeasure
        {
            public oMeasureGroupMeasure()
            {
            }

            public oMeasureGroupMeasure(XElement measure)
            {

                Name = measure.Element(Namespaces.engine + "Name").Value;
                ID = measure.Element(Namespaces.engine + "ID").Value;
                Description = measure.Element(Namespaces.engine + "Description").Value;
                AggregateFunction = measure.Element(Namespaces.engine + "AggregateFunction").Value;
                DataType = measure.Element(Namespaces.engine + "DataType").Value;
                Visible = measure.Element(Namespaces.engine + "Visible").Value;
                MeasureExpression = measure.Element(Namespaces.engine + "MeasureExpression").Value;
                DisplayFolder = measure.Element(Namespaces.engine + "DisplayFolder").Value;
                FormatString = measure.Element(Namespaces.engine + "FormatString").Value;
                BackColor = measure.Element(Namespaces.engine + "BackColor").Value;
                ForeColor = measure.Element(Namespaces.engine + "ForeColor").Value;
                FontName = measure.Element(Namespaces.engine + "FontName").Value;
                FontSize = measure.Element(Namespaces.engine + "FontSize").Value;
                FontFlags = measure.Element(Namespaces.engine + "FontFlags").Value;
                Source = measure.Elements(Namespaces.engine + "Source").Select(keyColumn => new KeyColumn
                    {
                        DataType = keyColumn.Element(Namespaces.engine + "DataType").Value,
                        DataSize = keyColumn.Element(Namespaces.engine + "DataSize").Value,
                        NullProcessing = keyColumn.Element(Namespaces.engine + "NullProcessing").Value,
                        Collation = keyColumn.Element(Namespaces.engine + "Collation").Value,
                        Format = keyColumn.Element(Namespaces.engine + "Format").Value,
                        Source = keyColumn.Elements(Namespaces.engine + "Source").Select(source => new TableColumn
                            {
                                TableName = source.Element(Namespaces.engine + "TableID").Value,
                                ColumnName = (source.Element(Namespaces.engine + "ColumnID") == null)
                                                 ? String.Empty
                                                 : source.Element(Namespaces.engine + "ColumnID").Value,
                                Type = source.Attribute(Namespaces.xsi + "type").Value
                            })
                    });
                Translations = measure.Element(Namespaces.engine + "Translations").Value;
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String Description { get; set; }
            public String AggregateFunction { get; set; }
            public String DataType { get; set; }
            public String Visible { get; set; }
            public String MeasureExpression { get; set; }
            public String DisplayFolder { get; set; }
            public String FormatString { get; set; }
            public String BackColor { get; set; }
            public String ForeColor { get; set; }
            public String FontName { get; set; }
            public String FontSize { get; set; }
            public String FontFlags { get; set; }
            public IEnumerable<KeyColumn> Source { get; set; }
            public String Translations { get; set; }

        }

        public class oMeasureGroupPartition
        {
            public oMeasureGroupPartition()
            {
            }

            public oMeasureGroupPartition(XElement measureGroupPartition)
            {

                Name = measureGroupPartition.Element(Namespaces.engine + "Name").Value;
                ID = measureGroupPartition.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = measureGroupPartition.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = measureGroupPartition.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = measureGroupPartition.Element(Namespaces.engine + "Description").Value;
                Annotations = measureGroupPartition.Elements(Namespaces.engine + "Annotations");
                LastProcessed = measureGroupPartition.Element(Namespaces.engine + "LastProcessed").Value;
                State = measureGroupPartition.Element(Namespaces.engine + "State").Value;
                Type = measureGroupPartition.Element(Namespaces.engine + "Type").Value;
                AggregationPrefix = measureGroupPartition.Element(Namespaces.engine + "AggregationPrefix").Value;
                StorageMode = measureGroupPartition.Element(Namespaces.engine + "StorageMode").Value;
                CurrentStorageMode = measureGroupPartition.Element(Namespaces.engine + "CurrentStorageMode").Value;
                StringStoresCompatibilityLevel =
                    measureGroupPartition.Element(Namespaces.engine300 + "StringStoresCompatibilityLevel").Value;
                CurrentStringStoresCompatibilityLevel =
                    measureGroupPartition.Element(Namespaces.engine300 + "CurrentStringStoresCompatibilityLevel").Value;
                ProcessingMode = measureGroupPartition.Element(Namespaces.engine + "ProcessingMode").Value;
                ProcessingPriority = measureGroupPartition.Element(Namespaces.engine + "ProcessingPriority").Value;
                StorageLocation = measureGroupPartition.Element(Namespaces.engine + "StorageLocation").Value;
                RemoteDataSourceID = measureGroupPartition.Element(Namespaces.engine + "RemoteDataSourceID").Value;
                Slice = measureGroupPartition.Element(Namespaces.engine + "Slice").Value;
                EstimatedRows = measureGroupPartition.Element(Namespaces.engine + "EstimatedRows").Value;
                AggregationDesignID = measureGroupPartition.Element(Namespaces.engine + "AggregationDesignID").Value;
                Source =
                    measureGroupPartition.Elements(Namespaces.engine + "Source")
                                         .Select(source => new PartitionSource(source));
                ProactiveCaching =
                    measureGroupPartition.Elements(Namespaces.engine + "ProactiveCaching")
                                         .Select(proactiveCaching => new oProactiveCaching(proactiveCaching));
                EstimatedSize = measureGroupPartition.Element(Namespaces.engine + "EstimatedSize").Value;
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public IEnumerable<XElement> Annotations { get; set; }
            public String LastProcessed { get; set; }
            public String State { get; set; }
            public String Type { get; set; }
            public String AggregationPrefix { get; set; }
            public String StorageMode { get; set; }
            public String CurrentStorageMode { get; set; }
            public String StringStoresCompatibilityLevel { get; set; }
            public String CurrentStringStoresCompatibilityLevel { get; set; }
            public String ProcessingMode { get; set; }
            public String ProcessingPriority { get; set; }
            public String StorageLocation { get; set; }
            public String RemoteDataSourceID { get; set; }
            public String Slice { get; set; }
            public String EstimatedRows { get; set; }
            public String AggregationDesignID { get; set; }
            public IEnumerable<PartitionSource> Source { get; set; }
            public IEnumerable<oProactiveCaching> ProactiveCaching { get; set; }
            public String EstimatedSize { get; set; }

        }

        public class oMiningColumn
        {
            public oMiningColumn()
            {
            }

            public oMiningColumn(XElement miningColumn)
            {
                Name = miningColumn.Element(Namespaces.engine + "Name").Value;
                ID = miningColumn.Element(Namespaces.engine + "ID").Value;
                Description = miningColumn.Element(Namespaces.engine + "Description").Value;
                Content = (miningColumn.Element(Namespaces.engine + "Content") == null)
                              ? String.Empty
                              : miningColumn.Element(Namespaces.engine + "Content").Value;
                Type = (miningColumn.Element(Namespaces.engine + "Type") == null)
                           ? String.Empty
                           : miningColumn.Element(Namespaces.engine + "Type").Value;
                IsKey = (miningColumn.Element(Namespaces.engine + "IsKey") == null)
                            ? String.Empty
                            : miningColumn.Element(Namespaces.engine + "IsKey").Value;
                DiscretizationBucketCount = (miningColumn.Element(Namespaces.engine + "DiscretizationBucketCount") ==
                                             null)
                                                ? String.Empty
                                                : miningColumn.Element(Namespaces.engine + "DiscretizationBucketCount")
                                                              .Value;
                ModelingFlags = (miningColumn.Element(Namespaces.engine + "ModelingFlags") == null)
                                    ? String.Empty
                                    : miningColumn.Element(Namespaces.engine + "ModelingFlags").Value;
                ForeignKeyColumns =
                    miningColumn.Elements(Namespaces.engine + "ForeignKeyColumns")
                                .Elements(Namespaces.engine + "ForeignKeyColumn")
                                .Select(keyColumn => new KeyColumn(keyColumn));
                KeyColumns =
                    miningColumn.Elements(Namespaces.engine + "KeyColumns")
                                .Elements(Namespaces.engine + "KeyColumn")
                                .Select(keyColumn => new KeyColumn(keyColumn));
                Translations = miningColumn.Element(Namespaces.engine + "Translations").Value;
                Annotations = miningColumn.Elements(Namespaces.engine + "Annotations");
                Columns =
                    miningColumn.Elements(Namespaces.engine + "Columns")
                                .Elements(Namespaces.engine + "Column")
                                .Select(keyColumn => new oMiningColumnItem(keyColumn));
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String Description { get; set; }
            public String Content { get; set; }
            public String Type { get; set; }
            public String IsKey { get; set; }
            public String DiscretizationBucketCount { get; set; }
            public String ModelingFlags { get; set; }
            public IEnumerable<KeyColumn> KeyColumns { get; set; }
            public String Translations { get; set; }
            public IEnumerable<XElement> Annotations { get; set; }
            public IEnumerable<oMiningColumnItem> Columns { get; set; }
            public IEnumerable<KeyColumn> ForeignKeyColumns { get; set; }
        }

        public class oMiningColumnItem
        {
            public oMiningColumnItem()
            {
            }

            public oMiningColumnItem(XElement item)
            {
                Name = item.Element(Namespaces.engine + "Name").Value;
                ID = item.Element(Namespaces.engine + "ID").Value;
                Description = item.Element(Namespaces.engine + "Description").Value;
                Content = item.Element(Namespaces.engine + "Content").Value;
                Type = item.Element(Namespaces.engine + "Type").Value;
                IsKey = item.Element(Namespaces.engine + "IsKey").Value;
                DiscretizationBucketCount = item.Element(Namespaces.engine + "DiscretizationBucketCount").Value;
                ModelingFlags = item.Element(Namespaces.engine + "ModelingFlags").Value;
                KeyColumns =
                    item.Element(Namespaces.engine + "KeyColumns")
                        .Elements(Namespaces.engine + "KeyColumn")
                        .Select(keyColumn => new KeyColumn(keyColumn));
                Translations = item.Element(Namespaces.engine + "Translations").Value;
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String Description { get; set; }
            public String Content { get; set; }
            public String Type { get; set; }
            public String IsKey { get; set; }
            public String DiscretizationBucketCount { get; set; }
            public String ModelingFlags { get; set; }
            public IEnumerable<KeyColumn> KeyColumns { get; set; }
            public String Translations { get; set; }
        }

        public class oMiningModel
        {
            public oMiningModel()
            {
            }

            public oMiningModel(XElement miningModel)
            {
                Name = miningModel.Element(Namespaces.engine + "Name").Value;
                ID = miningModel.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = miningModel.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = miningModel.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = miningModel.Element(Namespaces.engine + "Description").Value;
                LastProcessed = miningModel.Element(Namespaces.engine + "LastProcessed").Value;
                State = miningModel.Element(Namespaces.engine + "State").Value;
                Language = miningModel.Element(Namespaces.engine + "Language").Value;
                Collation = miningModel.Element(Namespaces.engine + "Collation").Value;
                Algorithm = miningModel.Element(Namespaces.engine + "Algorithm").Value;
                AllowDrillThrough = miningModel.Element(Namespaces.engine + "AllowDrillThrough").Value;
                AlgorithmParameters =
                    miningModel.Elements(Namespaces.engine + "AlgorithmParameters")
                               .Elements(Namespaces.engine + "AlgorithmParameter")
                               .ToDictionary(d => d.Element(Namespaces.engine + "Name").Value,
                                             d => d.Element(Namespaces.engine + "Value").Value);
                Columns = miningModel.Elements(Namespaces.engine + "Columns").Elements(Namespaces.engine + "Column");
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public String LastProcessed { get; set; }
            public String State { get; set; }
            public String Language { get; set; }
            public String Collation { get; set; }
            public String Algorithm { get; set; }
            public String AllowDrillThrough { get; set; }
            public IDictionary<String, String> AlgorithmParameters { get; set; }
            public IEnumerable<XElement> Columns { get; set; }
        }

        public class oMiningStructure
        {
            public oMiningStructure()
            {
            }

            public oMiningStructure(XElement miningStructure)
            {
                Name = miningStructure.Element(Namespaces.engine + "Name").Value;
                ID = miningStructure.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = miningStructure.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = miningStructure.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = miningStructure.Element(Namespaces.engine + "Description").Value;
                Annotations = miningStructure.Elements(Namespaces.engine + "Annotations");
                LastProcessed = miningStructure.Element(Namespaces.engine + "LastProcessed").Value;
                State = miningStructure.Element(Namespaces.engine + "State").Value;
                Language = miningStructure.Element(Namespaces.engine + "Language").Value;
                Collation = miningStructure.Element(Namespaces.engine + "Collation").Value;
                CacheMode = miningStructure.Element(Namespaces.engine + "CacheMode").Value;
                Source =
                    miningStructure.Element(Namespaces.engine + "Source")
                                   .Element(Namespaces.engine + "DataSourceViewID")
                                   .Value;
                Translations = miningStructure.Element(Namespaces.engine + "Translations").Value;
                Columns =
                    miningStructure.Element(Namespaces.engine + "Columns")
                                   .Elements(Namespaces.engine + "Column")
                                   .Select(column => new oMiningColumn(column));
                MiningModels =
                    miningStructure.Element(Namespaces.engine + "MiningModels")
                                   .Elements(Namespaces.engine + "MiningModel")
                                   .Select(miningModel => new oMiningModel(miningModel));
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public IEnumerable<XElement> Annotations { get; set; }
            public String LastProcessed { get; set; }
            public String State { get; set; }
            public String Language { get; set; }
            public String Collation { get; set; }
            public String CacheMode { get; set; }
            public String Source { get; set; }
            public String Translations { get; set; }
            public IEnumerable<oMiningColumn> Columns { get; set; }
            public IEnumerable<oMiningModel> MiningModels { get; set; }

        }

        public class oProactiveCaching
        {
            public oProactiveCaching()
            {
            }

            public oProactiveCaching(XElement proactiveCaching)
            {
                SilenceInterval = proactiveCaching.Element(Namespaces.engine + "SilenceInterval").Value;
                Latency = proactiveCaching.Element(Namespaces.engine + "Latency").Value;
                SilenceOverrideInterval = proactiveCaching.Element(Namespaces.engine + "SilenceOverrideInterval").Value;
                ForceRebuildInterval = proactiveCaching.Element(Namespaces.engine + "ForceRebuildInterval").Value;
                Enabled = proactiveCaching.Element(Namespaces.engine + "Enabled").Value;
                AggregationStorage = proactiveCaching.Element(Namespaces.engine + "AggregationStorage").Value;
                OnlineMode = proactiveCaching.Element(Namespaces.engine + "OnlineMode").Value;
                SourceType =
                    proactiveCaching.Element(Namespaces.engine + "Source").Attribute(Namespaces.xsi + "type").Value;
                NotificationTechnique =
                    proactiveCaching.Element(Namespaces.engine + "Source")
                                    .Element(Namespaces.engine + "NotificationTechnique")
                                    .Value;
            }

            public String SilenceInterval { get; set; }
            public String Latency { get; set; }
            public String SilenceOverrideInterval { get; set; }
            public String ForceRebuildInterval { get; set; }
            public String Enabled { get; set; }
            public String AggregationStorage { get; set; }
            public String OnlineMode { get; set; }
            public String SourceType { get; set; }
            public String NotificationTechnique { get; set; }
        }

        public class oRole
        {
            public oRole()
            {
            }

            public oRole(XElement role)
            {
                Name = role.Element(Namespaces.engine + "Name").Value;
                ID = role.Element(Namespaces.engine + "ID").Value;
                CreatedTimestamp = role.Element(Namespaces.engine + "CreatedTimestamp").Value;
                LastSchemaUpdate = role.Element(Namespaces.engine + "LastSchemaUpdate").Value;
                Description = role.Element(Namespaces.engine + "Description").Value;
                Members =
                    role.Elements(Namespaces.engine + "Members")
                        .Elements(Namespaces.engine + "Member")
                        .Select(roleMember => new oRoleMember(roleMember));
            }

            public String Name { get; set; }
            public String ID { get; set; }
            public String CreatedTimestamp { get; set; }
            public String LastSchemaUpdate { get; set; }
            public String Description { get; set; }
            public IEnumerable<oRoleMember> Members { get; set; }

        }

        public class oRoleMember
        {
            public oRoleMember()
            {
            }

            public oRoleMember(XElement member)
            {
                SID = member.Element(Namespaces.engine + "Sid").Value;
                Name = member.Element(Namespaces.engine + "Name").Value;
            }

            public String SID { get; set; }
            public String Name { get; set; }
        }

        public class oUnaryOperatorColumn
        {
            public oUnaryOperatorColumn()
            {
            }

            public oUnaryOperatorColumn(XElement unaryOperatorColumn)
            {
                DataType = unaryOperatorColumn.Element(Namespaces.engine + "DataType").Value;
                DataSize = unaryOperatorColumn.Element(Namespaces.engine + "DataSize").Value;
                NullProcessing = unaryOperatorColumn.Element(Namespaces.engine + "NullProcessing").Value;
                Collation = unaryOperatorColumn.Element(Namespaces.engine + "Collation").Value;
                Format = unaryOperatorColumn.Element(Namespaces.engine + "Format").Value;
                Source =
                    unaryOperatorColumn.Elements(Namespaces.engine + "Source").Select(source => new TableColumn(source));
            }

            public String DataType { get; set; }
            public String DataSize { get; set; }
            public String NullProcessing { get; set; }
            public String Collation { get; set; }
            public String Format { get; set; }
            public IEnumerable<TableColumn> Source { get; set; }

        }

        #endregion

    }