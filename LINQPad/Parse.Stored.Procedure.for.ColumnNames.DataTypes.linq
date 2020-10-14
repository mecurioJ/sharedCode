<Query Kind="Program">
  <Connection>
    <ID>f29406c1-faa9-4088-af94-a403c2212cd1</ID>
    <Persist>true</Persist>
    <Server>den-sql01</Server>
    <Database>ESW_Intranet</Database>
  </Connection>
</Query>

void Main()
{
	//string ProcString = @"exec Orders.sp_GetJIMSubscription 'rstone@estoneworks.com'";
	//("Acct_Budget_Summary '" & EntityID & "', '" & BUD_YEAR & "', '" & BUD_PERIOD & "', '" & BUD_TYPE & "'")
	// BudgetTypes are ARAGE, JOBCOST, PROD, CASH, SALES
//("Acct_Cash_Collection_Summary  '" & EntityID & "', '" & RunDate & "'") 
	//string ProcString = @"EXEC dbo.GetClipstoneSalesByDate '03/01/2014','03/31/2014'";
	string ProcString = @"EXEC JobCost_Open 'EMAT2','EMGA','', '', 'rptJobCostOpn', 'all', 'AccountMngr','','','','', 'dtl','NoList','NONE',''";


	DataTable ProcedureResult = new DataTable();
	DataTable ProcedureSource = new DataTable();
	
	SqlDataAdapter sqlda = new SqlDataAdapter(
	//String.Format(@"SET FMTONLY ON; {0} SET FMTONLY OFF;",ProcString), (SqlConnection)Connection);
	String.Format(@"{0}",ProcString), (SqlConnection)Connection);
		
	sqlda.Fill(ProcedureSource);
	
	//ProcedureSource.Dump();
	
	foreach(DataColumn dc in ProcedureSource.Columns)
	{
		ProcedureResult.Columns.Add(new DataColumn(dc.ColumnName,dc.DataType, dc.MaxLength.ToString()));
	}
	
	//Get Schema output
	ProcedureResult.Columns.Cast<DataColumn>().Select(col => new {
		col.Ordinal, 
		col.ColumnName,
		DataType = col.DataType.Name
			.ToString(),
		
		}).Dump();
	
	Console.WriteLine("	private DataTable SourceDt;");
	Console.WriteLine("	SourceDt = ((DataSet)ReadOnlyVariables[\"{0}\"].Value).Tables[0];","GetClipstoneSalesByDate");
	Console.WriteLine("	foreach (DataRow dr in SourceDt.Rows)");
	Console.WriteLine("	{");
	Console.WriteLine("		JobCostOpenBuffer.AddRow();");
	ProcedureResult.Columns.Cast<DataColumn>().Select(col => 
	string.Format(
		"{1}.{0} = dr[\"{0}\"].ToString();",
		col.ColumnName,
		"JobCostOpenBuffer"
		)
	).Dump();
	Console.WriteLine("	}");
	
    //    SourceDt = ((DataSet)ReadOnlyVariables["DiscrepantOrders"].Value).Tables[0];
	//foreach (DataRow dr in SourceDt.Rows)
    //    	{
   	//         DiscrepantOrdersBuffer.AddRow();
	//		}
		
//	ProcedureResult.Columns.Cast<DataColumn>().Select(col => 
//	string.Format(
//		"{1}.{0} = dr[\"{0}\"].ToString();",
//		col.ColumnName,
//		"GetClipstoneSalesByDateBuffer"
//		)
//	).Dump();


}

// Define other methods and classes here