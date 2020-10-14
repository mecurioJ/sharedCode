<Query Kind="Program">
  <Connection>
    <ID>41ee6e4d-2ab0-4fe9-88c0-159fb65d6ef3</ID>
    <Persist>true</Persist>
    <Server>localhost</Server>
    <Database>ODS</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	var ctx = this.Connection;
	
	//Create Data Tables
	DataTable Source = new DataTable();
	
	//Accquire Data
	new SqlDataAdapter(string.Format(@"SELECT [Entity],[CustID],[CustName],[SOStatus],[WOStatus],[OrderDate],[OrderDateFmt],[RvJobRdyDate]
      ,[RvJobRdyDateFmt],[JobRdyDate],[JobRdyDateFmt],[WOWantDate],[WOWantDateFmt],[EstCompDate]
      ,[EstCompDateFmt],[AccountMngr],[ProjectMngr],[PlanNumber],[Territory],[CustPORef],[Subdivision]
      ,[LotNumber],[ShipToAddr],[ShipToCity],[ShipToState],[ShipToCounty],[ShipToZipCode],[SalesOrder]
      ,[WorkOrder],[WOPartID],[WOPartDesc],[WOProdCode],[WOProdCodeDesc],[WOProdCodeGrp],[COCommCode]
      ,[InvcdDllr],[MasonSF],[SalesPerFoot],[InstLabDolEmp],[InstLabDolSub],[InstLabDolOther],[InstLabDollars]
      ,[InstLabPerFoot],[InstLabPcnt],[MatDollars],[MatPerFoot],[MatPercent],[JobMarginDol],[JobMarginPct]
      ,[InvoicedQuantity],[IssStnQty],[IssStnQty_Flat],[IssStnQty_Corner],[IssStnQty_Accy],[IssStnMatDol]
	 ,[IssStnLabDol],[IssStnOnHDol],[IssStnTotDol],[IssInsMatDol],[WOProdSetup],[WOProdQty_Flat],[WOProdQty_Corner]
      ,[WOProdQty_Accy],[MasonSFPaid],[LastPaidDate],[LastIssueDate],[FirstIssueDate],[Installer],[LastComment]
      ,[commentAt],[hasJIMCompletion],[NextTruckDate],[ShipVia]
  FROM [dw].JobCostOpen"),Connection.ConnectionString).Fill(Source);
  
  
  //Source.Columns.Cast<DataColumn>().Select(c => c.ColumnName).Aggregate((p,n) => String.Format("{0},{1}",p,n)).Dump();
  Source.Columns.Cast<DataColumn>().Select(c => String.Format("Inserted.[{0}] AS new_{0}",c.ColumnName)).Aggregate((p,n) => String.Format("{0},{1}",p,n)).Dump();
}

// Define other methods and classes here
