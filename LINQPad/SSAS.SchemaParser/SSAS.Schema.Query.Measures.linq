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
	 AdomdConnection MetaDataConnection = new AdomdConnection(Utils.ConnectionString(ServerName, CatalogName));
      if (MetaDataConnection.State != ConnectionState.Open)
      {
          MetaDataConnection.Open();
      }
	  
	  
	  var Restrictions = new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection();
	  
		var SchemaObjects = 
		new filichiacom.SchemaParser.oDataBase(
			XDocument.Parse(
			MetaDataConnection.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.XmlMetadata].Id,
							new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection()
							{
								new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("DatabaseID",CatalogName),
								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CubeID","DDA"),
								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("MeasureGroupID","FACT DDA"),
								
							}).Tables[0].Rows[0].Field<String>("METADATA")).Elements()
												.First());

		var MeasureObjects = 
		MetaDataConnection.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.Measures].Id,
							new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection()
							{
								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("DatabaseID",CatalogName),
								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CubeID","DDA"),
								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("MeasureGroupID","FACT DDA"),
								
							}).Tables[0].Rows.Cast<DataRow>().Select(mRow => new{
							CubeName = mRow.Field<String>("CUBE_NAME"),
							MeasureUniqueName = mRow.Field<String>("MEASURE_UNIQUE_NAME"),
							MeasureName = mRow.Field<String>("MEASURE_NAME"),
							MeasureCaption = mRow.Field<String>("MEASURE_CAPTION"),
							Description = mRow.Field<String>("DESCRIPTION"),
							MeasureAggregator = (mRow.Field<Object>("MEASURE_AGGREGATOR") != null)
												? System.Enum.Parse(typeof(eMeasureAggregator),mRow.Field<Object>("MEASURE_AGGREGATOR").ToString())
												: eMeasureAggregator.None,
							DataType = (mRow.Field<ushort>("DATA_TYPE") != null)
										? System.Enum.Parse(typeof(eDataType),mRow.Field<Object>("DATA_TYPE").ToString())
										: eDataType.DBTYPE_IUNKNOWN,
							NumericPrecision = mRow.Field<ushort>("NUMERIC_PRECISION"),
							NumericScale = mRow.Field<short>("NUMERIC_SCALE"),
							Expression = mRow.Field<String>("EXPRESSION") ?? String.Empty,
							IsVisible = mRow.Field<Boolean>("MEASURE_IS_VISIBLE"),							
							MeasureGroupName = mRow.Field<String>("MEASUREGROUP_NAME") ?? String.Empty,							
							DisplayFolder = mRow.Field<String>("MEASURE_DISPLAY_FOLDER"),							
							FormatString = mRow.Field<String>("DEFAULT_FORMAT_STRING") ?? String.Empty,	
							measureDef = SchemaParser.GetMeasureDefinitions(SchemaObjects).Where(mdef => 
								mdef.MeasureName.Equals(mRow.Field<String>("MEASURE_NAME"))
								&& mdef.CubeName.Equals(mRow.Field<String>("CUBE_NAME"))
								&& mdef.MeasureGroupName.Equals(mRow.Field<String>("MEASUREGROUP_NAME"))
								).FirstOrDefault()
							
						});
											
  MeasureObjects
  	.Where(mObj => 
		mObj.MeasureAggregator.Equals(eMeasureAggregator.Count)
		|| mObj.MeasureAggregator.Equals(eMeasureAggregator.DistinctCount)
		)
 	.OrderBy(mObj => mObj.MeasureAggregator)
		.Dump();

  
	if (MetaDataConnection.State != ConnectionState.Closed)
	{
		MetaDataConnection.Close();
	}
	
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



    public enum eDataType
    {
        DBTYPE_ARRAY = 0x2000,
        DBTYPE_BOOL = 11,
        DBTYPE_BSTR = 8,
        DBTYPE_BYREF = 0x4000,
        DBTYPE_BYTES = 0x80,
        DBTYPE_CY = 6,
        DBTYPE_DATE = 7,
        DBTYPE_DBDATE = 0x85,
        DBTYPE_DBTIME = 0x86,
        DBTYPE_DBTIMESTAMP = 0x87,
        DBTYPE_DECIMAL = 14,
        DBTYPE_EMPTY = 0,
        DBTYPE_ERROR = 10,
        DBTYPE_GUID = 0x48,
        DBTYPE_I1 = 0x10,
        DBTYPE_I2 = 2,
        DBTYPE_I4 = 3,
        DBTYPE_I8 = 20,
        DBTYPE_IDISPATCH = 9,
        DBTYPE_IUNKNOWN = 13,
        DBTYPE_NULL = 1,
        DBTYPE_NUMERIC = 0x83,
        DBTYPE_R4 = 4,
        DBTYPE_R8 = 5,
        DBTYPE_RESERVED = 0x8000,
        DBTYPE_STR = 0x81,
        DBTYPE_UDT = 0x84,
        DBTYPE_UI1 = 0x11,
        DBTYPE_UI2 = 0x12,
        DBTYPE_UI4 = 0x13,
        DBTYPE_UI8 = 0x15,
        DBTYPE_VARIANT = 12,
        DBTYPE_VECTOR = 0x1000,
        DBTYPE_WSTR = 130
    }
    public enum eDimensionType
    {
        Unknown,
        Time,
        Measure,
        Other,
        MsNotDefined,
        Quantitative,
        Accounts,
        Customers,
        Products,
        Scenario,
        Utility,
        Currency,
        Rates,
        Channel,
        Promotion,
        Organization,
        BillOfMaterials,
        Geography
    }
    public enum eHierarchyOrigin
    {
        Key = 6,
        ParentChild = 3,
        SystemEnabled = 2,
        SystemInternal = 4,
        UserDefined = 1,
        UserDefinedSystemInternal = 5
    }
	public enum eHierarchyStructure
    {
        FullyBalanced,
        RaggedBalanced,
        Unbalanced,
        Network
    }
	public enum eInstanceSelection
    {
        None,
        DropDown,
        List,
        FilteredList,
        MandatoryFilter
    }
	public enum eLevelType
    {
        Account = 0x1014,
        All = 1,
        BomResource = 0x1012,
        Calculated = 2,
        Channel = 0x1061,
        Company = 0x1042,
        CurrencyDestination = 0x1052,
        CurrencySource = 0x1051,
        Customer = 0x1021,
        CustomerGroup = 0x1022,
        CustomerHousehold = 0x1023,
        GeoCity = 0x2006,
        GeoContinent = 0x2001,
        GeoCountry = 0x2003,
        GeoCounty = 0x2005,
        GeoPoint = 0x2008,
        GeoPostalCode = 0x2007,
        GeoRegion = 0x2002,
        GeoStateOrProvince = 0x2004,
        OrgUnit = 0x1011,
        Person = 0x1041,
        Product = 0x1031,
        ProductGroup = 0x1032,
        Promotion = 0x1071,
        Quantitative = 0x1013,
        Regular = 0,
        Representative = 0x1062,
        Reserved1 = 8,
        Scenario = 0x1015,
        Time = 4,
        TimeDays = 0x204,
        TimeHalfYears = 0x24,
        TimeHours = 0x304,
        TimeMinutes = 0x404,
        TimeMonths = 0x84,
        TimeQuarters = 0x44,
        TimeSeconds = 0x804,
        TimeUndefined = 0x1004,
        TimeWeeks = 260,
        TimeYears = 20,
        Utility = 0x1016
    }
	public enum eMeasureAggregator
    {
        Avg = 5,
        AvgChildren = 10,
        ByAccount = 15,
        Calculated = 127,
        Count = 2,
        DistinctCount = 8,
        FirstChild = 11,
        FirstNonEmpty = 13,
        LastChild = 12,
        LastNonEmpty = 14,
        Max = 4,
        Min = 3,
        None = 9,
        Std = 7,
        Sum = 1,
        Unknown = 0,
        Var = 6
    }
	[Flags]
    public enum ePropertyType
    {
        MDPROP_BLOB = 8,
        MDPROP_CELL = 2,
        MDPROP_MEMBER = 1,
        MDPROP_SYSTEM = 4
    }
	public enum eRelationCardinality
    {
        One,
        Many
    }
	public enum eServerVersion
    {
        Unknown,
        Shiloh,
        Yukon,
        Katmai
    }
