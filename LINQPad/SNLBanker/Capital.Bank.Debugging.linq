<Query Kind="Program" />

protected static String ServerName = @"BURDCSNLBSQLDEV\SNLBANKERDEV";
protected static String CatalogName = @"BankingCustomerIntelligence_JHLive";

void Main()
{
									

	using(AdomdConnection connection = new AdomdConnection(Utils.ConnectionString(ServerName,CatalogName)))
	{
		if (connection.State != ConnectionState.Open)
			connection.Open();
		
		//[Customers].[Customer Geography].[State]
		// [Customers].[Customer Geography].[County]
		//[Customers].[Customer Geography].[City]
			var StateRows = connection.GetSchemaDataSet( "MDSCHEMA_MEMBERS", new AdomdRestrictionCollection()
                                {
                                    new AdomdRestriction("CATALOG_NAME", CatalogName),
                                    new AdomdRestriction("CUBE_NAME", "Loans"),
                                    new AdomdRestriction("DIMENSION_UNIQUE_NAME", "[Customers]"),									
                                    new AdomdRestriction("HIERARCHY_UNIQUE_NAME", "[Customers].[Customer Geography]"),										
                                    new AdomdRestriction("LEVEL_UNIQUE_NAME", "[Customers].[Customer Geography].[State]"),
                                })
                            .Tables[0].Rows.Cast<DataRow>();

			var CountyRows =  connection.GetSchemaDataSet( "MDSCHEMA_MEMBERS", new AdomdRestrictionCollection()
                                {
                                    new AdomdRestriction("CATALOG_NAME", CatalogName),
                                    new AdomdRestriction("CUBE_NAME", "Loans"),
                                    new AdomdRestriction("DIMENSION_UNIQUE_NAME", "[Customers]"),									
                                    new AdomdRestriction("HIERARCHY_UNIQUE_NAME", "[Customers].[Customer Geography]"),									
                                    new AdomdRestriction("LEVEL_UNIQUE_NAME", "[Customers].[Customer Geography].[County]"),	
                                })
                            .Tables[0].Rows.Cast<DataRow>();
							
			var CityRows =  connection.GetSchemaDataSet( "MDSCHEMA_MEMBERS", new AdomdRestrictionCollection()
                                {
                                    new AdomdRestriction("CATALOG_NAME", CatalogName),
                                    new AdomdRestriction("CUBE_NAME", "Loans"),
                                    new AdomdRestriction("DIMENSION_UNIQUE_NAME", "[Customers]"),									
                                    new AdomdRestriction("HIERARCHY_UNIQUE_NAME", "[Customers].[Customer Geography]"),										
                                    new AdomdRestriction("LEVEL_UNIQUE_NAME", "[Customers].[Customer Geography].[City]"),
                                })
                            .Tables[0].Rows.Cast<DataRow>();
							
StateRows.Union(CountyRows.Union(CityRows)).Select(dr => new
														{
HierarchyUniqueName = dr.Field<String>("HIERARCHY_UNIQUE_NAME"),
LevelUniqueName = dr.Field<String>("LEVEL_UNIQUE_NAME"),
LevelSortOrder = dr.Field<UInt32>("LEVEL_NUMBER"),
MemberSortOrder = dr.Field<UInt32>("MEMBER_ORDINAL"),
MemberName = dr.Field<String>("MEMBER_NAME"),
MemberCaption = dr.Field<String>("MEMBER_CAPTION"),
MemberUniqueName = dr.Field<String>("MEMBER_UNIQUE_NAME"),
ParentUniqueName = dr.Field<String>("PARENT_UNIQUE_NAME"),
MemberKey = dr.Field<String>("MEMBER_KEY"),
														}).ToArray()
.Dump()
														;
		
		if (connection.State != ConnectionState.Closed)
			connection.Close();	
	}
}

// Define other methods and classes here
private class Utils
{
	public static String ConnectionString(String ServerName, String InitialCatalog)
	{
			return String.Format("Data Source={0};Application Name={1}; Initial Catalog={2}",
			ServerName,
			"SNLBanker",
			InitialCatalog
			);
	}
}

// Define other methods and classes here
