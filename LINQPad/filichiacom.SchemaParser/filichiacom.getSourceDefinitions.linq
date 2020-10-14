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
	  //var Restrictions = new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection();
	  
	  //var memberUniqueName = "[Customers].[Customer Geography].[City].&[CO]&[Boulder]&[Boulder]";
	  //var memberUniqueName = "[Customers].[Customer Geography].[State].&[CO]";
	  var memberUniqueName = "[Customers].[Customer Geography].[County].&[CO]&[Boulder]";
	  
	  System.Tuple<String,string,String,String> memberValue;
				MetaDataConnection
					.GetSchemaDataSet(
						AdomdSchema.sSchemaList[AdomdSchemaGuid.Members].Id,new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection()
							{
								new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CATALOG_NAME",CatalogName),
								new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CUBE_NAME","Loans"),
								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("MeasureGroupID","FACT DDA"),
								new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("MEMBER_UNIQUE_NAME",memberUniqueName),
								
							})
							.Tables[0]
							.Rows.Cast<DataRow>()
							.Select(dr => dr).Dump();
	  
//	  
//	  var SchemaObjects = 
//		new filichiacom.SchemaParser.oDataBase(
//			XDocument.Parse(
//				MetaDataConnection
//					.GetSchemaDataSet(
//						AdomdSchema.sSchemaList[AdomdSchemaGuid.MemberProperties].Id,new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection()
//							{
//								new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("DatabaseID",CatalogName),
//								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CubeID","DDA"),
//								new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("MeasureGroupID","FACT DDA"),
//								
//							})
//					.Tables[0]
//					.Rows[0]
//					.Field<String>("METADATA")).Elements()
//												.First());
//					
//		var DataSource = 
//		SchemaObjects.DataSourceViews.SelectMany(dsv => dsv.Schema.Tables.Cast<DataTable>().SelectMany(col => col.Columns.Cast<DataColumn>().Select(cCol => new{
//		dsv.Name,
//		dsv.ID,
//			IsPrimaryKey = cCol.Table.PrimaryKey.Cast<DataColumn>().Contains(cCol),
//			cCol.Table.TableName,
//			cCol.ColumnName,
//			cCol.DataType,
//			DbTableName = (cCol.Table.ExtendedProperties["DbTableName"] != null)
//						? cCol.Table.ExtendedProperties["DbTableName"].ToString()
//							: String.Empty,
//			TableFriendlyName = (cCol.Table.ExtendedProperties["FriendlyName"] != null)
//						? cCol.Table.ExtendedProperties["FriendlyName"].ToString()
//							: String.Empty,
//			Expression = (cCol.ExtendedProperties["ComputedColumnExpression"] != null)
//							? cCol.ExtendedProperties["ComputedColumnExpression"].ToString()
//									.Replace("\n", " ")
//									.Replace("\r", " ")
//							: String.Empty,
//			DbColumnName = (cCol.ExtendedProperties["DbColumnName"] != null)
//							? cCol.ExtendedProperties["DbColumnName"].ToString()
//							: String.Empty,
//			ColumnFriendlyName = (cCol.ExtendedProperties["FriendlyName"] != null)
//									? cCol.ExtendedProperties["FriendlyName"].ToString()
//									: String.Empty
//			})));
//			
//	 	Server svr = new Server();
//		svr.Connect(Utils.ConnectionString(ServerName, CatalogName));
//		var Cubes = svr.Databases.Cast<Database>().First().Cubes.Cast<Cube>().ToArray();
//		var Dimensions = Cubes.SelectMany(cub => cub.Dimensions.Cast<CubeDimension>()).ToArray();
//		var Hierarchies = Dimensions.SelectMany(cDim => cDim.Hierarchies.Cast<CubeHierarchy>()).ToArray();
//		var Attributes = Dimensions.SelectMany(cDim => cDim.Attributes.Cast<CubeAttribute>()).ToArray();
//
//		
//		Hierarchies.Select(dHier => {
//			var SourceHierarchy = (Microsoft.AnalysisServices.Hierarchy)dHier.Hierarchy;
//			return new{
//				//Dimension = SourceHierarchy.Parent.Name,
//				Levels = 
//				SourceHierarchy.Levels.Cast<Microsoft.AnalysisServices.Level>().SelectMany(lvl => 
//				Attributes.Where(att => att.Attribute.Equals(lvl.SourceAttribute) & att.ParentCube.Equals(dHier.ParentCube))
//					
//					.Select(cAtt => {
//						var dimAtt = (DimensionAttribute)cAtt.Attribute;
//						var KeyBindings = dimAtt.KeyColumns.Cast<DataItem>().SelectMany(di => DataSource
//							.Where(dsv => 
//								//dsv.ID.Equals(cAtt.ParentCube.DataSourceView.ID) && 
//							dsv.Name.Equals(cAtt.ParentCube.DataSourceView.Name))
//							.Where(ds => ds.TableName.Equals(((ColumnBinding)di.Source).TableID) 
//								|| 
//								ds.DbTableName.Equals(((ColumnBinding)di.Source).TableID)
//								||
//								ds.TableFriendlyName.Equals(((ColumnBinding)di.Source).TableID)
//								)
//							.Where(ds => ds.ColumnName.Equals(((ColumnBinding)di.Source).ColumnID))
//							);
//							
//						var NameBindings = DataSource
//							.Where(dsv => dsv.ID.Equals(cAtt.ParentCube.DataSourceView.ID) && dsv.Name.Equals(cAtt.ParentCube.DataSourceView.Name))
//							.Where(ds => ds.TableName.Equals(((ColumnBinding)dimAtt.NameColumn.Source).TableID) && ds.ColumnName.Equals(((ColumnBinding)dimAtt.NameColumn.Source).ColumnID));
//									
//						return new{
//							CubeName = cAtt.ParentCube.Name,
//							DimensionName = dimAtt.Parent.Name,
//							HierarchyName = SourceHierarchy.Name,
//							LevelName = lvl.Name,
//							IsDateType = Utils.ParseDateType(dimAtt.Type),
//							Attribute = dimAtt.Name,
//							ColumnBindings = KeyBindings.Union(NameBindings).Select(bd => new{
//								IsKeyBinding = KeyBindings.Contains(bd),
//								//bd.Name,
//								//bd.ID,
//								bd.IsPrimaryKey,
//								bd.DbTableName,
//								bd.TableFriendlyName,
//								bd.DbColumnName,
//								//bd.TableName,
//								//bd.ColumnName,
//								DataType = bd.DataType.ToString(),
//								bd.Expression,
//								bd.ColumnFriendlyName
//							}),
//							KeyBindings,
//							NameBindings
//						};
//					
//					}))
//			
//			};
//		
//		
//		})
//		.SelectMany(li => li.Levels)
//		//.Where(li => li.IsDateType)
//		.Where(li => !li.ColumnBindings.Any())
//		//.Select(li => new{li.CubeName,li.DimensionName,li.HierarchyName,li.LevelName,li.Attribute})
//		.Select(li => li.HierarchyName)
//		.Distinct()
//		.ToArray().Dump();
//		
//		
		
		
		
		/*
		svr.Databases.Cast<Database>().First().Cubes.Cast<Cube>()
		.Select(cub => new{
			Dimensions = cub.Dimensions.Cast<CubeDimension>().Select(cDim => new{
				cDim.Name,
				Hierarchies = cDim.Hierarchies.Cast<CubeHierarchy>(),
				Attributes = cDim.Attributes.Cast<CubeAttribute>()
			}),
			cub.MeasureGroups,
			cub.Name,
			cub.DefaultMdxScript,
			cub.AllMeasures
		})
		
		.Dump();
*/
		/*
		var SchemaObjects = 
		new filichiacom.SchemaParser.oDataBase(
			XDocument.Parse(
				MetaDataConnection
					.GetSchemaDataSet(
						AdomdSchema.sSchemaList[AdomdSchemaGuid.XmlMetadata].Id,new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection()
							{
								new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("DatabaseID",CatalogName),
								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CubeID","DDA"),
								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("MeasureGroupID","FACT DDA"),
								
							})
					.Tables[0]
					.Rows[0]
					.Field<String>("METADATA")).Elements()
												.First());
		*/		
					
  
	if (MetaDataConnection.State != ConnectionState.Closed)
	{
		MetaDataConnection.Close();
	}
	
}




protected static String ServerName = "SNLBANKERDEV1";
//protected static String ServerName = ".";
//protected static String ServerName = "LT-JFILICHA";
protected static String CatalogName = "BankingCustomerIntelligence_JHLive";
// Define other methods and classes here
public class Utils
{
	public static bool ParseDateType (AttributeType aType)
	{
		return Enum.Equals(AttributeType.Date,aType) ||
				Enum.Equals(AttributeType.DayOfHalfYear,aType) ||
				Enum.Equals(AttributeType.DayOfMonth,aType) ||
				Enum.Equals(AttributeType.DayOfQuarter,aType) ||
				Enum.Equals(AttributeType.DayOfTenDays,aType) ||
				Enum.Equals(AttributeType.DayOfTrimester,aType) ||
				Enum.Equals(AttributeType.DayOfWeek,aType) ||
				Enum.Equals(AttributeType.DayOfYear,aType) ||
				Enum.Equals(AttributeType.Days,aType) ||
				Enum.Equals(AttributeType.FiscalDate,aType) ||
				Enum.Equals(AttributeType.FiscalDayOfHalfYear,aType) ||
				Enum.Equals(AttributeType.FiscalDayOfMonth,aType) ||
				Enum.Equals(AttributeType.FiscalDayOfQuarter,aType) ||
				Enum.Equals(AttributeType.FiscalDayOfTrimester,aType) ||
				Enum.Equals(AttributeType.FiscalDayOfWeek,aType) ||
				Enum.Equals(AttributeType.FiscalDayOfYear,aType) ||
				Enum.Equals(AttributeType.FiscalHalfYears,aType) ||
				Enum.Equals(AttributeType.FiscalHalfYearOfYear,aType) ||
				Enum.Equals(AttributeType.FiscalMonths,aType) ||
				Enum.Equals(AttributeType.FiscalMonthOfHalfYear,aType) ||
				Enum.Equals(AttributeType.FiscalMonthOfQuarter,aType) ||
				Enum.Equals(AttributeType.FiscalMonthOfTrimester,aType) ||
				Enum.Equals(AttributeType.FiscalMonthOfYear,aType) ||
				Enum.Equals(AttributeType.FiscalQuarters,aType) ||
				Enum.Equals(AttributeType.FiscalQuarterOfHalfYear,aType) ||
				Enum.Equals(AttributeType.FiscalQuarterOfYear,aType) ||
				Enum.Equals(AttributeType.FiscalTrimesters,aType) ||
				Enum.Equals(AttributeType.FiscalTrimesterOfYear,aType) ||
				Enum.Equals(AttributeType.FiscalWeeks,aType) ||
				Enum.Equals(AttributeType.FiscalWeekOfHalfYear,aType) ||
				Enum.Equals(AttributeType.FiscalWeekOfMonth,aType) ||
				Enum.Equals(AttributeType.FiscalWeekOfQuarter,aType) ||
				Enum.Equals(AttributeType.FiscalWeekOfTrimester,aType) ||
				Enum.Equals(AttributeType.FiscalWeekOfYear,aType) ||
				Enum.Equals(AttributeType.FiscalYears,aType) ||
				Enum.Equals(AttributeType.HalfYears,aType) ||
				Enum.Equals(AttributeType.HalfYearOfYear,aType) ||
				Enum.Equals(AttributeType.Hours,aType) ||
				Enum.Equals(AttributeType.IsHoliday,aType) ||
				/*
				//SQL Server 2012 Analysis Services Specific
				Enum.Equals(AttributeType.ISO8601Date,aType) ||
				Enum.Equals(AttributeType.ISO8601DayOfWeek,aType) ||
				Enum.Equals(AttributeType.ISO8601DayOfYear,aType) ||
				Enum.Equals(AttributeType.ISO8601Weeks,aType) ||
				Enum.Equals(AttributeType.ISO8601WeekOfYear,aType) ||
				Enum.Equals(AttributeType.ISO8601Years,aType) ||
				Enum.Equals(AttributeType.ManufacturingDayOfTrimester,aType) ||
				Enum.Equals(AttributeType.ManufacturingMonthOfTrimester,aType) ||
				Enum.Equals(AttributeType.ManufacturingWeekOfTrimester,aType) ||
				*/
				Enum.Equals(AttributeType.IsPeakDay,aType) ||
				Enum.Equals(AttributeType.IsWeekDay,aType) ||
				Enum.Equals(AttributeType.IsWorkingDay,aType) ||
				Enum.Equals(AttributeType.ManufacturingDate,aType) ||
				Enum.Equals(AttributeType.ManufacturingDayOfHalfYear,aType) ||
				Enum.Equals(AttributeType.ManufacturingDayOfMonth,aType) ||
				Enum.Equals(AttributeType.ManufacturingDayOfQuarter,aType) ||
				Enum.Equals(AttributeType.ManufacturingDayOfWeek,aType) ||
				Enum.Equals(AttributeType.ManufacturingDayOfYear,aType) ||
				Enum.Equals(AttributeType.ManufacturingHalfYears,aType) ||
				Enum.Equals(AttributeType.ManufacturingHalfYearOfYear,aType) ||
				Enum.Equals(AttributeType.ManufacturingMonths,aType) ||
				Enum.Equals(AttributeType.ManufacturingMonthOfHalfYear,aType) ||
				Enum.Equals(AttributeType.ManufacturingMonthOfQuarter,aType) ||
				Enum.Equals(AttributeType.ManufacturingMonthOfYear,aType) ||
				Enum.Equals(AttributeType.ManufacturingQuarters,aType) ||
				Enum.Equals(AttributeType.ManufacturingQuarterOfHalfYear,aType) ||
				Enum.Equals(AttributeType.ManufacturingQuarterOfYear,aType) ||
				Enum.Equals(AttributeType.ManufacturingWeeks,aType) ||
				Enum.Equals(AttributeType.ManufacturingWeekOfHalfYear,aType) ||
				Enum.Equals(AttributeType.ManufacturingWeekOfMonth,aType) ||
				Enum.Equals(AttributeType.ManufacturingWeekOfQuarter,aType) ||
				Enum.Equals(AttributeType.ManufacturingWeekOfYear,aType) ||
				Enum.Equals(AttributeType.ManufacturingYears,aType) ||
				Enum.Equals(AttributeType.Minutes,aType) ||
				Enum.Equals(AttributeType.Months,aType) ||
				Enum.Equals(AttributeType.MonthOfHalfYear,aType) ||
				Enum.Equals(AttributeType.MonthOfQuarter,aType) ||
				Enum.Equals(AttributeType.MonthOfTrimester,aType) ||
				Enum.Equals(AttributeType.MonthOfYear,aType) ||
				Enum.Equals(AttributeType.Quarters,aType) ||
				Enum.Equals(AttributeType.QuarterOfHalfYear,aType) ||
				Enum.Equals(AttributeType.QuarterOfYear,aType) ||
				Enum.Equals(AttributeType.ReportingDate,aType) ||
				Enum.Equals(AttributeType.ReportingDayOfHalfYear,aType) ||
				Enum.Equals(AttributeType.ReportingDayOfMonth,aType) ||
				Enum.Equals(AttributeType.ReportingDayOfQuarter,aType) ||
				Enum.Equals(AttributeType.ReportingDayOfTrimester,aType) ||
				Enum.Equals(AttributeType.ReportingDayOfWeek,aType) ||
				Enum.Equals(AttributeType.ReportingDayOfYear,aType) ||
				Enum.Equals(AttributeType.ReportingHalfYears,aType) ||
				Enum.Equals(AttributeType.ReportingHalfYearOfYear,aType) ||
				Enum.Equals(AttributeType.ReportingMonths,aType) ||
				Enum.Equals(AttributeType.ReportingMonthOfHalfYear,aType) ||
				Enum.Equals(AttributeType.ReportingMonthOfQuarter,aType) ||
				Enum.Equals(AttributeType.ReportingMonthOfTrimester,aType) ||
				Enum.Equals(AttributeType.ReportingMonthOfYear,aType) ||
				Enum.Equals(AttributeType.ReportingQuarters,aType) ||
				Enum.Equals(AttributeType.ReportingQuarterOfHalfYear,aType) ||
				Enum.Equals(AttributeType.ReportingQuarterOfYear,aType) ||
				Enum.Equals(AttributeType.ReportingTrimesters,aType) ||
				Enum.Equals(AttributeType.ReportingTrimesterOfYear,aType) ||
				Enum.Equals(AttributeType.ReportingWeeks,aType) ||
				Enum.Equals(AttributeType.ReportingWeekOfHalfYear,aType) ||
				Enum.Equals(AttributeType.ReportingWeekOfMonth,aType) ||
				Enum.Equals(AttributeType.ReportingWeekOfQuarter,aType) ||
				Enum.Equals(AttributeType.ReportingWeekOfTrimester,aType) ||
				Enum.Equals(AttributeType.ReportingWeekOfYear,aType) ||
				Enum.Equals(AttributeType.ReportingYears,aType) ||
				Enum.Equals(AttributeType.Seconds,aType) ||
				Enum.Equals(AttributeType.TenDayOfHalfYear,aType) ||
				Enum.Equals(AttributeType.TenDayOfMonth,aType) ||
				Enum.Equals(AttributeType.TenDayOfQuarter,aType) ||
				Enum.Equals(AttributeType.TenDayOfTrimester,aType) ||
				Enum.Equals(AttributeType.TenDayOfYear,aType) ||
				Enum.Equals(AttributeType.TenDays,aType) ||
				Enum.Equals(AttributeType.Trimesters,aType) ||
				Enum.Equals(AttributeType.TrimesterOfYear,aType) ||
				Enum.Equals(AttributeType.UndefinedTime,aType) ||
				Enum.Equals(AttributeType.WeekOfYear,aType) ||
				Enum.Equals(AttributeType.Weeks,aType) ||
				Enum.Equals(AttributeType.WinterSummerSeason,aType) ||
				Enum.Equals(AttributeType.Years,aType);
	}

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