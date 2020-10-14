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

protected static String ServerName = "LT-JFILICHA";
protected static String CatalogName = "BankingCustomerIntelligence_JHLive";

protected static XNamespace engine = "http://schemas.microsoft.com/analysisservices/2003/engine";
protected static XNamespace engine200 = "http://schemas.microsoft.com/analysisservices/2010/engine/200";
protected static XNamespace engine300 = "http://schemas.microsoft.com/analysisservices/2011/engine/300";
protected static XNamespace engine2 = "http://schemas.microsoft.com/analysisservices/2003/engine/2";
protected static XNamespace xs = "http://www.w3.org/2001/XMLSchema";
protected static XNamespace xsi="http://www.w3.org/2001/XMLSchema-instance";
protected static XNamespace msData = "urn:schemas-microsoft-com:xml-msdata";
protected static XNamespace diffGram = "urn:schemas-microsoft-com:xml-diffgram-v1";

void Main()
{
	//AdomdConnection MetaDataConnection = new AdomdConnection(Utils.ConnectionString(ServerName, CatalogName));
	
	DataRowCollection MeasureGroupsDimensions;
	DataRowCollection Dimensions;
	DataRowCollection Measures;
	DataRowCollection MeasureGroups;
	DataRowCollection Hierarchies;
	DataRowCollection Members;
	
	DataRowCollection dbColumns;
	
	DataRowCollection dbTables;
	
	DataRowCollection SchemaObjects;
	
	var mdRestrictions = 
							new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection()
								{
									new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CATALOG_NAME",CatalogName),
									//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CUBE_NAME","Loans"),
								};
								
								
	var tbRestrictions = 
							new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection()
								{
									new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("TABLE_CATALOG",CatalogName),
									new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("TABLE_SCHEMA","Loans"),
								};

	var xmlRestrictions = 
						new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection()
							{
								new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("DatabaseID",CatalogName),
								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CubeID","DDA"),
								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("MeasureGroupID","FACT DDA"),
								
							};
	
	using(AdomdConnection connection = new AdomdConnection(Utils.ConnectionString(ServerName, CatalogName)))
	{	
	if (connection.State != ConnectionState.Open)
			connection.Open();
			
			
			
//			MeasureGroupsDimensions =
//			connection.GetSchemaDataSet(
//			AdomdSchema.sSchemaList[AdomdSchemaGuid.MeasureGroupDimensions].Id, mdRestrictions).Tables[0].Rows;
//			
//			MeasureGroups=
//			connection.GetSchemaDataSet(
//			AdomdSchema.sSchemaList[AdomdSchemaGuid.MeasureGroups].Id, mdRestrictions).Tables[0].Rows;
			
//			Dimensions =
//			connection.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.Dimensions].Id,
//								new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection()
//								{
//									new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CATALOG_NAME",CatalogName),
//									new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CUBE_NAME","DDA")
//								}).Tables[0].Rows;
			

			SchemaObjects = 
			connection.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.XmlMetadata].Id,
												xmlRestrictions).Tables[0].Rows;
		


			var Schema = XElement.Parse(SchemaObjects[0].Field<String>("METADATA"));
			var CubeDimensions = Schema.Elements(engine+"Dimensions").Elements(engine+"Dimension")
			.Select(cd => new{
				Attributes = cd.Element(engine+"Attributes").Elements(engine+"Attribute").Select(at => new{
				DimensionName = cd.Element(engine+"Name").Value,
				DimensionID = cd.Element(engine+"ID").Value,
				DataSourceViewID = cd.Element(engine+"Source").Element(engine+"DataSourceViewID").Value,
					AttributeID = at.Element(engine+"ID").Value,
					KeyColumnBinding = at.Element(engine+"KeyColumns").Element(engine+"KeyColumn").Element(engine+"Source").Attribute(xsi+"type").Value,
					KeyColumns = at.Element(engine+"KeyColumns").Element(engine+"KeyColumn").Elements(engine+"Source").Select(kc => new{
						TableID = kc.Element(engine+"TableID").Value,
						ColumnID = kc.Element(engine+"ColumnID").Value
					}),
					NameColumn = at.Element(engine+"NameColumn").Elements(engine+"Source").Select(kc => new{
						TableID = kc.Element(engine+"TableID").Value,
						ColumnID = kc.Element(engine+"ColumnID").Value
					}),
				})
			}).SelectMany(d => d.Attributes);
			var CubesDimensions = Schema.Elements(engine+"Cubes").Elements(engine+"Cube").Select(cb =>new{
				Name = cb.Element(engine+"Name").Value,
				Dimensions = cb.Elements(engine+"Dimensions").Elements(engine+"Dimension")
			}).SelectMany(d => d.Dimensions.Select(dm => new{
				//Attributes = dm.Element(engine+"Attributes").Elements(engine+"Attribute").Select(at => at.Element(engine+"AttributeID").Value),
				DataSourceView = CubeDimensions
										.Where(c => c.DimensionID.Equals(dm.Element(engine+"DimensionID").Value))
										.Where(c => dm.Element(engine+"Attributes").Elements(engine+"Attribute").Elements(engine+"AttributeID").Select (x => x.Value
										).Contains(c.AttributeID))
										.Select(c => new{
										Cube = d.Name,
										Dimension = dm.Element(engine+"Name").Value,
										DimensionID = dm.Element(engine+"DimensionID").Value,
										Visible = dm.Element(engine+"Visible").Value,
										c.AttributeID,
										c.DataSourceViewID, 
										KeyColumnPairs = c.KeyColumns.Select(kc => new KeyValuePair<String,String>(kc.ColumnID,kc.TableID)), 
										NameColumnPairs = c.NameColumn.Select(kc => new KeyValuePair<String,String>(kc.ColumnID,kc.TableID))
										}),
			})).SelectMany(t => t.DataSourceView)
			.Dump();
			
			//.Select(xn => xn.Name.LocalName).Distinct()
			

	if (connection.State != ConnectionState.Closed)
		connection.Close();
	}
	
	
		
}

class CubeDimension
{
	public CubeDimension(){}
	public CubeDimension(XElement d)
	{
		Name = d.Element(engine+"Name").Value;
		ID = d.Element(engine+"ID").Value;
		DataSourceViewID = d.Descendants(engine+"DataSourceViewID").Select(e => e.Value).ToArray();
		Type = d.Element(engine+"Type").Value;
		Attributes = d.Element(engine+"Attributes").Elements(engine+"Attribute").Select(attr => new CubeDimensionAttribute(attr));
		Hierarchies = d.Elements(engine+"Hierarchies").Where(h => h.HasElements).Elements().Select(hy => new CubeDimensionHierarchy(hy));
	}
	public String Name {get;set;}
	public String ID {get;set;}
	public String[] DataSourceViewID {get;set;}
	public String Type {get;set;}
	public IEnumerable<CubeDimensionAttribute> Attributes {get;set;}
	public IEnumerable<CubeDimensionHierarchy> Hierarchies {get;set;}

}

class CubeDimensionAttribute
{
	public CubeDimensionAttribute(){}
	public CubeDimensionAttribute(XElement attr)
	{
		Name = attr.Element(engine+"Name").Value;
		ID = attr.Element(engine+"ID").Value;
		KeyColumns = attr.Element(engine+"KeyColumns").Elements(engine+"KeyColumn").Select(kc => new AttributeColumn(kc.Element(engine+"Source")));
		NameColumn = attr.Element(engine+"NameColumn").Elements(engine+"Source").Select(nc => new AttributeColumn(nc));
		AttributeRelationships = attr.Elements(engine+"AttributeRelationships").Where(ar => ar.HasElements).Elements().Select(ar => new CubeDimensionAttributeRelationship(ar));
	}
	public String Name {get;set;}
	public String ID {get;set;}
	public IEnumerable<AttributeColumn> KeyColumns {get;set;}
	public IEnumerable<AttributeColumn> NameColumn {get;set;}
	public IEnumerable<CubeDimensionAttributeRelationship> AttributeRelationships {get;set;}
}

class AttributeColumn
{
	public AttributeColumn(){}
	public AttributeColumn(XElement col)
	{
		TableID = col.Element(engine+"TableID").Value;
		ColumnID = col.Element(engine+"ColumnID").Value;
	}
	public String TableID {get;set;}
	public String ColumnID {get;set;}
}

class CubeDimensionAttributeRelationship
{
	public CubeDimensionAttributeRelationship(){}
	public CubeDimensionAttributeRelationship(XElement ar)
	{
		AttributeID = ar.Element(engine+"AttributeID").Value;
		Name = ar.Element(engine+"Name").Value;
		RelationshipType = ar.Element(engine+"RelationshipType").Value;
		Cardinality = ar.Element(engine+"Cardinality").Value;
		Optionality = ar.Element(engine+"Optionality").Value;
		OverrideBehavior = ar.Element(engine+"OverrideBehavior").Value;
	}
	
	public String AttributeID {get;set;}
	public String Name {get;set;}
	public String RelationshipType {get;set;}
	public String Cardinality {get;set;}
	public string Optionality {get;set;}
	public String OverrideBehavior {get;set;}
}

class CubeDimensionHierarchy
{

	public CubeDimensionHierarchy(){}
	public CubeDimensionHierarchy(XElement hy)
	{
		Name = hy.Element(engine+"Name").Value;
		ID = hy.Element(engine+"ID").Value;
		Levels = hy.Elements(engine+"Levels").Where(l => l.HasElements).Elements().Select(lvl => new CubeDimensionHierarchyLevel(lvl));
	
	}
	public String Name {get;set;}
	public String ID {get;set;}
	public IEnumerable<CubeDimensionHierarchyLevel> Levels {get;set;}
	
}

class CubeDimensionHierarchyLevel
{
	public CubeDimensionHierarchyLevel(){}
	public CubeDimensionHierarchyLevel(XElement lvl)
	{
		Name = lvl.Element(engine+"Name").Value;
		ID = lvl.Element(engine+"ID").Value;
		SourceAttributeID = lvl.Element(engine+"SourceAttributeID").Value;
	}
	public String Name {get;set;}
	public String ID {get;set;}
	public String SourceAttributeID {get;set;}
}

private class Utils
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


// Define other methods and classes here
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