<Query Kind="Program">
  <Connection>
    <ID>57b8e721-62f2-4f6a-88ff-6c40878b38a8</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>SNLBanker_ApplicationDB</Database>
  </Connection>
  <Reference>&lt;ProgramFilesX86&gt;\Microsoft.NET\ADOMD.NET\110\Microsoft.AnalysisServices.AdomdClient.dll</Reference>
  <Reference>&lt;ProgramFilesX86&gt;\Microsoft SQL Server\110\SDK\Assemblies\Microsoft.AnalysisServices.DLL</Reference>
  <Namespace>Microsoft.AnalysisServices</Namespace>
  <Namespace>Microsoft.AnalysisServices.AdomdClient</Namespace>
</Query>

void Main()
{
    AdomdConnection MetaDataConnection = new AdomdConnection(Utils.ConnectionString(ServerName, CatalogName));
      if (MetaDataConnection.State != ConnectionState.Open)
      {
          MetaDataConnection.Open();
      }
	  
	  
	  
	  
	  var Restrictions = new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection();
	  	Restrictions.Add("DatabaseID",CatalogName);
//		Restrictions.Add("CubeID","Loans");
	  //Restrictions.Add("DatabaseID",CatalogName);
	  //Restrictions.Add("CubeID","Global 1");
	  //Restrictions.Add("DIMENSION_NAME", "[Customer]");
	  
	  var results = MetaDataConnection.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.XmlMetadata].Id,
												Restrictions).Tables[0].Rows[0][0].ToString()
												;

  
  
	if (MetaDataConnection.State != ConnectionState.Closed)
	{
		MetaDataConnection.Close();
	}
	
	DataSet ds = new DataSet();
	XElement rslt = new XElement("DataSourceViews",XElement.Parse(results).Elements().Where(xn => xn.Name.LocalName.Contains("DataSourceViews")).Descendants().Where(xn => xn.Name.LocalName.Equals("Schema")));
	
	XNamespace engine = "http://schemas.microsoft.com/analysisservices/2003/engine";
	XNamespace xs = "http://www.w3.org/2001/XMLSchema";
	
	
	ds.ReadXml(rslt.Elements(engine + "Schema").First().Element(xs + "schema").CreateReader());
	ds.Tables[0].Dump();
	
	
	
	/*
	var metaDS = XDocument.Parse(results.Rows[0][0].ToString())
	.Descendants().Where(xn => xn.Name.LocalName.Equals("Cube"))
	.Select(cb => new{
		Name = cb.Elements().Where(xn => xn.Name.LocalName.Equals("Name")).FirstOrDefault().Value,
		ID = cb.Elements().Where(xn => xn.Name.LocalName.Equals("ID")).FirstOrDefault().Value,
	})
	.Dump();
	*/
//	results.Rows.Cast<DataRow>().Select(dr => new{
//		ColumnName = dr.Field<String>("DIMENSION_NAME"),
//		ColumnName = dr.Field<String>("DIMENSION_UNIQUE_NAME"),
//		ColumnName = dr.Field<String>("DIMENSION_UNIQUE_NAME"),
//		DataType = dr.Field<System.Type>("DataType")
//	}).Dump();
	
	//results.Dump();
	
//	  results.Rows.Cast<DataRow>().
//	  	Select(fld => new{
//			DimensionName = fld.Field<String>("DIMENSION_NAME")
//		}).Dump();
	  //XElement DDL = XElement.Parse(schemaTable.Rows.Cast<DataRow>().FirstOrDefault().Field<String>("METADATA"));
	  
	  //DDL.Descendants().Where(xn => xn.Name.LocalName == "Attribute").Dump();
//	  LevelsTable.Dump();
//	  HierarchyTable.Dump();
//	  MeasureTable.Dump();
	  
//	  CubeDef currCube = MetaDataConnection.Cubes.Cast<CubeDef>().Where(cub => !cub.Name.StartsWith("$")).Where(cub => cub.Name == "Loans").FirstOrDefault();
//	  currCube.Dimensions.Cast<Microsoft.AnalysisServices.AdomdClient.Dimension>()
//	  .Where(dim => dim.UniqueName == "[Account Status]")
//	  .SelectMany(dim => dim.Hierarchies.Cast<Microsoft.AnalysisServices.AdomdClient.Hierarchy>().Where(hier => hier.UniqueName=="[Account Status].[Account Status]"))
//	  .FirstOrDefault().Dump();
	  
	  
/*
	var cubeEnum = MetaDataConnection
					.Cubes
					.Cast<CubeDef>()
					.Where(cb => !cb.Name.StartsWith("$"))
					.Select(cb => new {
						Name = cb.Name,
						Caption = cb.Caption,
						//DefaultMeasure = cb.Measures.ca\
						cb.NamedSets,
						cb.Kpis,
						Measures = cb.Measures.Cast<Microsoft.AnalysisServices.AdomdClient.Measure>().Select(ms => new
							oMeasure
						{
							Name = ms.Name,
							UniqueName = ms.UniqueName,
							Caption = ms.Caption,
							Description = ms.Description,
							DisplayFolder = ms.DisplayFolder,
							NumericPrecision = ms.NumericPrecision,
							NumericScale = ms.NumericScale,
							Properties = ms.Properties.Cast<Microsoft.AnalysisServices.AdomdClient.Property>().ToList()
						}).ToList(),
						Dimensions = cb.Dimensions.Cast<Microsoft.AnalysisServices.AdomdClient.Dimension>().Select(
							cDim => new
							{
								cDim.Name,
								cDim.UniqueName,
								cDim.Description,
								cDim.DimensionType,
								cDim.WriteEnabled,
								cDim.Caption,
								Hierarchies = cDim.Hierarchies.Cast<Microsoft.AnalysisServices.AdomdClient.Hierarchy>().Select(hier => new{
									hier.Name,
									hier.UniqueName,
									hier.Description,
									//hier.ParentDimension,
									hier.DefaultMember,
									hier.DisplayFolder,
									hier.Caption,
									hier.HierarchyOrigin,
									hier.Levels,
									hier.Properties 
									}),
								AttributeHierarchies = cDim.AttributeHierarchies.Cast<Microsoft.AnalysisServices.AdomdClient.Hierarchy>().Select(hier => new{
									hier.Name,
									hier.UniqueName,
									hier.Description,
									//hier.ParentDimension,
									hier.DefaultMember,
									hier.DisplayFolder,
									hier.Caption,
									hier.HierarchyOrigin,
									hier.Levels,
									hier.Properties 
									}),
								Properties = cDim.Properties.Cast<Microsoft.AnalysisServices.AdomdClient.Property>().ToList() 
							}).ToList(),
						
						
					})
					.ToList();
				
	cubeEnum.Dump();			
	*/
}

protected static String ServerName = "LT-JFILICHA";
protected static String CatalogName = "BankingCustomerIntelligence_JHLive";
// Define other methods and classes here

public interface OlapObject
{
        String Name { get; set; }
        String Caption { get; set; }
        String Description { get; set; }
}

public class oCube : OlapObject
{
        public String Name { get; set; }
        public String Caption { get; set; }
        public String Description { get; set; }
        public String DefaultMeasure { get; set; }
		
        private List<oDimension> _dimensions;
        public List<oDimension> Dimensions
        {
            get { return _dimensions ?? (_dimensions = new List<oDimension>()); }
            set { _dimensions = value; }
        }

        private List<oMeasure> _measures;
        public List<oMeasure> Measures
        {
            get { return _measures ?? (_measures = new List<oMeasure>()); }
            set { _measures = value; }
        }

        private List<oNamedSet> _namedSets;
        public List<oNamedSet> NamedSets
        {
            get { return _namedSets ?? (_namedSets = new List<oNamedSet>()); }
            set { _namedSets = value; }
        }

        private List<oKPI> _kpis;
        public List<oKPI> Kpis
        {
            get { return _kpis ?? (_kpis = new List<oKPI>()); }
            set { _kpis = value; }
        }

        private List<oMeasureGroup> _measureGroups;
        public List<oMeasureGroup> MeasureGroups
        {
            get { return _measureGroups ?? (_measureGroups = new List<oMeasureGroup>()); }
            set { _measureGroups = value; }
        }

}

public class oMeasure : OlapObject
{
        public string Name { get; set; }

        public string Caption { get; set; }

        public string Description { get; set; }
        public string DisplayFolder { get; set; }
        public string UniqueName { get; set; }
        public string Expression { get; set; }
        public string MeasureGroup { get; set; }
		
		public int NumericPrecision {get; set;}
		public short NumericScale {get;set;}
		public List<Microsoft.AnalysisServices.AdomdClient.Property> Properties {get;set;}
}

public class oLevel : OlapObject
{
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public Int32 LevelDepth { get; set; }
        public Int64 MemberCount { get; set; }
        public Boolean Visible { get; set; }
	
}

public class oKPI
{
        public String CubeName { get; set; }
        public String MeasureGroupName { get; set; }
        public String KpiName { get; set; }
        public String KpiCaption { get; set; }
        public String KpiDescription { get; set; }
        public String KpiDisplayFolder { get; set; }
        public String KpiValue { get; set; }
        public String KpiGoal { get; set; }
        public String KpiStatus { get; set; }
        public String KpiTrend { get; set; }
        public String KpiWeight { get; set; }
        public String KpiCurrentTimeMember { get; set; }
}

public class oNamedSet : OlapObject
{
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string DisplayFolder { get; set; }
        public string Expression { get; set; }
}

public class oDimension : OlapObject
{        
		public String Name { get; set; }
        public String Caption { get; set; }
        public String Description { get; set; }
        public String UniqueName { get; set; }
        public String DefaultHierarchyName { get; set; }
        public String DimensionType { get; set; }
		
        private List<oHierarchy> _hierarchies;
        public List<oHierarchy> Hierarchies
        {
            get { return _hierarchies ?? (_hierarchies = new List<oHierarchy>()); }
            set { _hierarchies = value; }
        }

        private List<oHierarchy> _attributeHierarchies;
        public List<oHierarchy> AttributeHierarchies
        {
            get { return _attributeHierarchies ?? (_attributeHierarchies = new List<oHierarchy>()); }
            set { _attributeHierarchies = value; }
        }
	
}


public class oHierarchy : OlapObject
{
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string DefaultLevelName { get; set; }
        public string DefaultMemberUniqueName { get; set; }
        public string DisplayFolder { get; set; }
        public string UniqueName { get; set; }
		
		private List<oLevel> _levels;
		public List<oLevel> Levels
		{
			get {return _levels ?? (_levels = new List<oLevel>());}
			set {_levels = value;}
		}
}

public class oMeasureGroup : OlapObject
{
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
	
        private List<oMeasure> _measures;
        public List<oMeasure> Measures
        {
            get { return _measures ?? (_measures = new List<oMeasure>()); }
            set { _measures = value; }
        }

        private List<oDimension> _dimensions;
        public List<oDimension> Dimensions
        {
            get { return _dimensions ?? (_dimensions = new List<oDimension>()); }
            set { _dimensions = value; }
        }
}

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