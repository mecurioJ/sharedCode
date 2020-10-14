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

	String SchemaLiteral;

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
	
		SchemaLiteral = 
		connection.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.XmlMetadata].Id,
											xmlRestrictions).Tables[0].Rows[0][0].ToString();
	
		XElement SchemaX = XElement.Parse(SchemaLiteral);
		
		
		var DataSourceViewsElements = SchemaX.Element(engine+"DataSourceViews").Elements(engine+"DataSourceView").Select(view => new DataSourceViewItem(view));
		
		var DimensionsElements = SchemaX.Element(engine+"Dimensions").Elements(engine+"Dimension").Select(view => view
		.Elements(engine+"Attributes").Elements(engine+"Attribute").Select(attr => new {
				DimensionName = view.Element(engine+"Name").Value, 
				DimensionID = view.Element(engine+"ID").Value, 
				DataSourceViewID = DataSourceViewsElements.Where(dsv => dsv.Id.Equals(view.Element(engine+"Source").Element(engine+"DataSourceViewID").Value)).FirstOrDefault(),
				Name = attr.Element(engine+"Name").Value, 
				ID = attr.Element(engine+"ID").Value, 
				Type = attr.Element(engine+"Type").Value, 
				DefaultMember = attr.Element(engine+"DefaultMember").Value, 
				IsAggregatable = attr.Element(engine+"IsAggregatable").Value, 
				KeyColumns = attr.Elements(engine+"KeyColumns").Elements(engine+"KeyColumn").Elements(engine+"Source").Select(s => new TableColumn{Table = s.Element(engine+"TableID").Value, Column = s.Elements(engine+"ColumnID").Select(v => v.Value).FirstOrDefault()}), 
				NameColumn = attr.Elements(engine+"NameColumn").Elements(engine+"Source").Select(s => new TableColumn{Table = s.Element(engine+"TableID").Value, Column = s.Elements(engine+"ColumnID").Select(v => v.Value).FirstOrDefault()}), 
				AttributeRelationships = attr.Elements(engine+"AttributeRelationships").Elements(engine+"AttributeRelationship").Select(aRel => new{
					AttributeID = aRel.Element(engine + "AttributeID").Value,
					Name = aRel.Element(engine + "Name").Value,
					RelationshipType = aRel.Element(engine + "RelationshipType").Value,
					Cardinality = aRel.Element(engine + "Cardinality").Value,
				}), 
			})
			
			).Dump();
		
		
		var CubesElements = SchemaX.Element(engine+"Cubes");
		
//		DimensionsElements.Elements(engine+"Dimension").Elements().Select(xn => 
//		(!xn.HasElements)
//		? String.Format("{0} = {1}.Element(engine+\"{0}\").Value,",xn.Name.LocalName,"view")
//		: String.Format("{0} = {1}.Elements(engine+\"{0}\").Elements(),",xn.Name.LocalName,"view")
//		).Distinct();//.Dump();

	if (connection.State != ConnectionState.Closed)
		connection.Close();
	}
	
}

public class DataSourceViewItem
{

	public DataSourceViewItem(){}
	public DataSourceViewItem(XElement view)
	{
		var viewItem = view
			.Elements(engine+"Schema")
			.Elements()
			.Where(s => s.FirstNode != null)
			.Select(xSch => {
					DataSet schemaSet = new DataSet("DataSourceView");
					schemaSet.ReadXml(xSch.CreateReader());
					//Get ParentTables from DataRelations
					IEnumerable<RelationshipDef> sRels = schemaSet.Relations.Cast<DataRelation>().Select(dr => new RelationshipDef(dr));
					IEnumerable<TableColumn> sTables = schemaSet.Tables.Cast<DataTable>()
						.SelectMany(tb => tb.Columns.Cast<DataColumn>().Select(dc => new TableColumn{Table = tb.TableName.Replace("dbo_","dbo."), Column = dc.ColumnName}));
					return new DataSourceViewItem{
						Name = view.Element(engine+"Name").Value, 
						Id = view.Element(engine+"ID").Value, 
						DataSourceID = view.Element(engine+"DataSourceID").Value,
						Relationships = sRels, 
						Tables = sTables};
				}).FirstOrDefault();
		Name = viewItem.Name;
		Id = viewItem.Id;
		DataSourceID = viewItem.DataSourceID;
		Relationships = viewItem.Relationships;
		Tables = viewItem.Tables;
	
	}
	public string Name {get;set;}
	public string Id {get;set;}
	public string DataSourceID {get;set;}
	public IEnumerable<RelationshipDef> Relationships {get;set;}
	public IEnumerable<TableColumn> Tables {get;set;}
}

// Define other methods and classes here
public class RelationshipDef
{
	public RelationshipDef (){}
	public RelationshipDef (DataRelation relation)
	{
		RelationName = relation.RelationName;
		Parent = new TableColumn{Table = relation.ParentTable.TableName, Column = relation.ParentColumns.Cast<DataColumn>().First().ColumnName};
		Child = new TableColumn{Table = relation.ChildTable.TableName, Column = relation.ChildColumns.Cast<DataColumn>().First().ColumnName};
		ParentTableDef = relation.ParentTable.Columns.Cast<DataColumn>().Select(dc => new TableColumn{Table = relation.ParentTable.TableName.Replace("dbo_","dbo."), Column = dc.ColumnName});
		ChildTableDef = relation.ChildTable.Columns.Cast<DataColumn>().Select(dc => new TableColumn{Table = relation.ChildTable.TableName.Replace("dbo_","dbo."), Column = dc.ColumnName});
	}
	public String RelationName {get;set;}
	public TableColumn Parent {get;set;}
	public TableColumn Child {get;set;}
	public IEnumerable<TableColumn> ParentTableDef {get;set;}
	public IEnumerable<TableColumn> ChildTableDef {get;set;}
	
}

public struct AttributeRelationship
{
	public string AttributeID;
	public string Name;
	public string RelationshipType;
	public string Cardinality;
	
}

public struct TableColumn
{
	public string Table;
	public string Column;
	
	public  override string ToString()
	{
		return String.Format("{0}.{1}",Table,Column);
	}
}


protected class Namespaces
{
	
	protected static XNamespace engine = "http://schemas.microsoft.com/analysisservices/2003/engine";
	protected static XNamespace engine200 = "http://schemas.microsoft.com/analysisservices/2010/engine/200";
	protected static XNamespace engine300 = "http://schemas.microsoft.com/analysisservices/2011/engine/300";
	protected static XNamespace engine2 = "http://schemas.microsoft.com/analysisservices/2003/engine/2";
	protected static XNamespace xs = "http://www.w3.org/2001/XMLSchema";
	protected static XNamespace xsi="http://www.w3.org/2001/XMLSchema-instance";
	protected static XNamespace msData = "urn:schemas-microsoft-com:xml-msdata";
	protected static XNamespace diffGram = "urn:schemas-microsoft-com:xml-diffgram-v1";
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