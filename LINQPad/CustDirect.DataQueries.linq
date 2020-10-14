<Query Kind="Program">
  <Connection>
    <ID>09686bde-e36a-4ae0-807b-641bd9369c65</ID>
    <Persist>true</Persist>
    <Server>den-sp-01</Server>
    <Database>Subscription</Database>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.DataSetExtensions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>System.Data.Sql</Namespace>
  <Namespace>System.Data.SqlClient</Namespace>
</Query>

void Main()
{	
	DataTable Customers = new DataTable();
	DataTable CustomerFilter = new DataTable();
	DataTable Subscribers = new DataTable();
	
	SqlDataAdapter sDa;
	
	sDa = new SqlDataAdapter("Exec [Email].[sp_SelectCustomersAll]",this.Connection.ConnectionString);
	sDa.Fill(Customers);
	
	sDa = new SqlDataAdapter("Exec [Email].[sp_SelectCustomerFiltersAll]",this.Connection.ConnectionString);
	sDa.Fill(CustomerFilter);
	
	sDa = new SqlDataAdapter("Exec [Email].[sp_SelectSubscribersAll]",this.Connection.ConnectionString);
	sDa.Fill(Subscribers);
	
	var custFilters = 
	CustomerFilter.Rows.Cast<DataRow>()
                .Select(dr => new System.Tuple<String, String, String, bool?>(
                    dr.Field<String>("EmailAddress"),
                    dr.Field<String>("EntityId"),
                    dr.Field<String>("CustId"),
                    dr.Field<bool?>("isActiveRelation")
                    )).ToList();
	
	custFilters.Dump();
}

// Define other methods and classes here

