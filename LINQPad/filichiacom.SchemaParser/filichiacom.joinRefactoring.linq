<Query Kind="Program">
  <Reference Relative="..\..\SkyDrive\Assemblies\SchemaParser\bin\Release\SchemaParser.dll">C:\Users\joeyf\SkyDrive\Assemblies\SchemaParser\bin\Release\SchemaParser.dll</Reference>
  <GACReference>Microsoft.AnalysisServices.AdomdClient, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>filichiacom</Namespace>
  <Namespace>Microsoft.AnalysisServices.AdomdClient</Namespace>
  <Namespace>MoreLinq</Namespace>
  <Namespace>SchemaParser = filichiacom.SchemaParser</Namespace>
</Query>

void Main()
{
	SchemaParser.oDataBase OlapData = new SchemaParser.oDataBase(XElement.Load(@"c:\Projects\SSAS\BankerSchema.xml"));
	
	DateTime TimeStamp = DateTime.Now.Dump();
	
            int databaseId = 11; 
            String cubeName = "Loans";
            IEnumerable<String> filterList = 
			new[]{String.Empty};
            IEnumerable<String> columnList = new[]{
			"[Measures].[Current Balance]",
			"[Products].[Products].[Product Name]",
			"[Customers].[Customer Names].[Customer Name]",
			"[Organization].[Organization Region].[Branch]",
			"[Accounts].[Accounts]",
			"[Organization].[Organization].[Charter].&[CHARTER SOUTH]",
					};
            IEnumerable<String> rowList = new[]{
			"[Customers].[Officer Portfolio].[Officer Name]",
			"[Products].[Products].[Product Family]",
			"[Measures].[Note Rate]",
			"[Risk Rating].[Risk Rating].[All Risk Rating]",
			"[Participation].[Participation Codes].&[-1]",
			};
			//Get the DataSourceViewId from the current Cube
			String DataSourceViewId = OlapData.Cubes.Where(cub => cub.Name.Equals(cubeName)).First().Source;
			
            //get expressions from tables
            var dataTableExpressions = SchemaParser.GetDataSourceViewExpressions(OlapData);
            var dataTableDefinitions = SchemaParser.GetDataSourceViewDefinitions(OlapData);

            //Build a lookup for the Table Joins
            var cubeSourceTablePkLookup = PrimaryKeyLookup(cubeName);
			
			var Joins = new []{
				new { LeftTable = "dbo_dim_Products", LeftColumn = "ProductID", RightTable = "dbo_FACT_Global", RightColumn = "ProductID" },
				new { LeftTable = "dbo_dim_Products", LeftColumn = "ProductID", RightTable = "dbo_FACT_Loans", RightColumn = "ProductID" },
				new { LeftTable = "dbo_dim_Products", LeftColumn = "ProductID", RightTable = "dbo_FACT_RPM_GLOBAL_View", RightColumn = "ProductID" },
				new { LeftTable = "dbo_dim_Products", LeftColumn = "ProductID", RightTable = "dbo_FACT_Global_WaivedHist", RightColumn = "ProductID" },
				new { LeftTable = "dbo_dim_Customer", LeftColumn = "CustomerID", RightTable = "dbo_dim_AccountCustomers", RightColumn = "dimCustomerID" },
				new { LeftTable = "dbo_dim_Branch", LeftColumn = "BranchID", RightTable = "dbo_FACT_Global", RightColumn = "BranchID" },
				new { LeftTable = "dbo_dim_Branch", LeftColumn = "BranchID", RightTable = "dbo_FACT_Loans", RightColumn = "BranchID" },
				new { LeftTable = "dbo_dim_Branch", LeftColumn = "Bank_Number", RightTable = "dbo_STAGE_RBC", RightColumn = "Bank_Number" },
				new { LeftTable = "dbo_dim_Branch", LeftColumn = "BranchID", RightTable = "dbo_FACT_RPM_GLOBAL_View", RightColumn = "BranchID" },
				new { LeftTable = "dbo_dim_Branch", LeftColumn = "BranchID", RightTable = "dbo_FACT_Global_WaivedHist", RightColumn = "BranchID" },
				new { LeftTable = "dbo_dim_Accounts", LeftColumn = "dimAccountID", RightTable = "dbo_dim_AccountCustomers", RightColumn = "dimAccountID" },
				new { LeftTable = "dbo_dim_Accounts", LeftColumn = "dimAccountID", RightTable = "dbo_FACT_Loans", RightColumn = "dimAccountID" },
				new { LeftTable = "dbo_dim_Accounts", LeftColumn = "dimAccountID", RightTable = "dbo_FACT_Global", RightColumn = "dimAccountID" },
				new { LeftTable = "dbo_dim_Accounts", LeftColumn = "dimAccountID", RightTable = "dbo_FACT_RPM_GLOBAL_View", RightColumn = "dimAccountID" },
				new { LeftTable = "dbo_dim_RiskRating", LeftColumn = "Risk_Rate_ID", RightTable = "dbo_FACT_Loans", RightColumn = "Risk_Rate_ID" },
				new { LeftTable = "dbo_dim_Loan_Participation_Status", LeftColumn = "Orig_Direct_Indirect_ID", RightTable = "dbo_FACT_Loans", RightColumn = "Orig_Direct_Indirect_ID" },
			};
			var MeasureJoinList = new[]{
				new { LeftTable = "dbo_dim_Products", LeftColumn = "ProductID", RightTable = "dbo_FACT_Loans", RightColumn = "ProductID" },
				new { LeftTable = "dbo_dim_Branch", LeftColumn = "BranchID", RightTable = "dbo_FACT_Loans", RightColumn = "BranchID" },
				new { LeftTable = "dbo_dim_Accounts", LeftColumn = "dimAccountID", RightTable = "dbo_FACT_Loans", RightColumn = "dimAccountID" },
				new { LeftTable = "dbo_dim_RiskRating", LeftColumn = "Risk_Rate_ID", RightTable = "dbo_FACT_Loans", RightColumn = "Risk_Rate_ID" },
				new { LeftTable = "dbo_dim_Loan_Participation_Status", LeftColumn = "Orig_Direct_Indirect_ID", RightTable = "dbo_FACT_Loans", RightColumn = "Orig_Direct_Indirect_ID" },
			};
			var MissingTableList = new[]{"dbo_dim_Customer"};
			var LinkingTableList = new[]{"dbo_dim_AccountCustomers"};
			
			var LinkingTable = 	Joins.Where(js => MissingTableList.Contains(js.LeftTable)).Select(js => js.RightTable).Dump();
			var MissingJoins = Joins.Where(js => LinkingTable.Contains(js.RightTable)).Dump();
			
			MissingJoins.Union(MeasureJoinList).Dump();
			
			
			DateTime.Now.Subtract(TimeStamp).TotalSeconds.Dump();
}
// Define other methods and classes here

public class HierarchySetItem
{
	public String Dimension {get;set;}
	public String Hierarchy  {get;set;}
	public String Level {get;set;} 
	public String Value {get;set;} 
	public Object Hierarchies {get;set;} 
}

public class DataDefinitionItem
{

	public String DataType {get;set;}
	public String TableName {get;set;}
	public String ColumnName {get;set;}
	public String OriginalName {get;set;}
	public IEnumerable<String[]> ExpressionDefinition {get;set;}
}


        public string FixTableName(string tableId)
        {
            // TODO: What if the schema name isn't "dbo" but something else?
            return tableId.StartsWith("dbo_") ? tableId.Substring(4) : tableId;
        }
		
		
        private ILookup<string, string> PrimaryKeyLookup(string cubeName)
        {
			SchemaParser.oDataBase OlapData = new SchemaParser.oDataBase(XElement.Load(@"c:\Projects\SSAS\BankerSchema.xml"));
            return OlapData.Cubes
                           .Where(cub => cub.Name.Equals(cubeName))
                           .SelectMany(cub => OlapData.DataSourceViews.Where(dsv => dsv.ID.Equals(cub.Source)).SelectMany(dsv => dsv.Schema.Tables.Cast<DataTable>().SelectMany(tbl => 
                                                                                                                                                                                tbl.PrimaryKey.Cast<DataColumn>().Select(dCol => new{
                                                                                                                                                                                    dCol.Table.TableName,
                                                                                                                                                                                    dCol.ColumnName
                                                                                                                                                                                })))).ToLookup(pk => pk.TableName, pk => pk.ColumnName);
        }