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
	//Get Today's Date Id
	var todayId = DimDates.Where(dt => dt.SQL_DATE.Equals(DateTime.Today)).FirstOrDefault().DATE_ID;
	
	//Get the connection
	var ctx = this.Connection;
	
	//Create Data Tables
	DataTable Source = new DataTable();
	DataTable Destination = new DataTable();
	
	//Accquire Data
	new SqlDataAdapter(string.Format(@"SELECT [username],[id],[Entity],[ReceivedDate],[RequestedInstallDate]
		,[DaysOut],[EnteredAt],[EnteredBy],[AccountManager],[AMName],[Estimator],[CustomerID],[CustomerName]
		,[Subdivision],[ProjectManager],[Address],[City],[State],[TotalAmt],[ReasonCode],[ReasonStr],[note],
		[ResolvedAt],[ResolvedBy],[CanceledAt],[CanceledBy],[SalesOrderID],[LotNumber],[Status],[ResolveDays],[LastComment]
		FROM [dw].[DiscrepantOrders] where DATE_ID = {0}",todayId),Connection.ConnectionString).Fill(Source);
	
	new SqlDataAdapter(@"SELECT [username],[id],[Entity],[ReceivedDate],[RequestedInstallDate]
		,[DaysOut],[EnteredAt],[EnteredBy],[AccountManager],[AMName],[Estimator],[CustomerID],[CustomerName]
		,[Subdivision],[ProjectManager],[Address],[City],[State],[TotalAmt],[ReasonCode],[ReasonStr],[note],
		[ResolvedAt],[ResolvedBy],[CanceledAt],[CanceledBy],[SalesOrderID],[LotNumber],[Status],[ResolveDays],[LastComment]
		FROM [Order].[DiscrepantOrders]",Connection.ConnectionString).Fill(Destination);
		
	//Cast results into Dynamic Classes
	var sourceRows = Source.Rows.Cast<DataRow>().Select(dr => new{
		username = dr.Field<String>("username"), 
		id = dr.Field<Int32?>("id"), 
		Entity = dr.Field<String>("Entity"), 
		ReceivedDate = dr.Field<DateTime?>("ReceivedDate"), 
		RequestedInstallDate = dr.Field<DateTime?>("RequestedInstallDate"), 
		//DaysOut = dr.Field<Int32>("DaysOut"), 
		EnteredAt = dr.Field<DateTime?>("EnteredAt"), 
		EnteredBy = dr.Field<String>("EnteredBy"), 
		AccountManager = dr.Field<String>("AccountManager"), 
		AMName = dr.Field<String>("AMName"), 
		Estimator = dr.Field<String>("Estimator"), 
		CustomerID = dr.Field<String>("CustomerID"), 
		CustomerName = dr.Field<String>("CustomerName"), 
		Subdivision = dr.Field<String>("Subdivision"), 
		ProjectManager = dr.Field<String>("ProjectManager"), 
		Address = dr.Field<String>("Address"), 
		City = dr.Field<String>("City"), 
		State = dr.Field<String>("State"), 
		TotalAmt = dr.Field<Decimal?>("TotalAmt"), 
		ReasonCode = dr.Field<Int32?>("ReasonCode"), 
		ReasonStr = dr.Field<String>("ReasonStr"), 
		note = dr.Field<String>("note"), 
		ResolvedAt = dr.Field<DateTime?>("ResolvedAt"), 
		ResolvedBy = dr.Field<String>("ResolvedBy"), 
		CanceledAt = dr.Field<DateTime?>("CanceledAt"), 
		CanceledBy = dr.Field<String>("CanceledBy"), 
		SalesOrderID = dr.Field<String>("SalesOrderID"), 
		LotNumber = dr.Field<String>("LotNumber"), 
		Status = dr.Field<String>("Status"), 
		ResolveDays = dr.Field<Int32?>("ResolveDays"), 
		LastComment = dr.Field<String>("LastComment")
	});
	
	var destRows = Destination.Rows.Cast<DataRow>().Select(dr => new{
		username = dr.Field<String>("username"), 
		id = dr.Field<Int32?>("id"), 
		Entity = dr.Field<String>("Entity"), 
		ReceivedDate = dr.Field<DateTime?>("ReceivedDate"), 
		RequestedInstallDate = dr.Field<DateTime?>("RequestedInstallDate"), 
		//DaysOut = dr.Field<Int32>("DaysOut"), 
		EnteredAt = dr.Field<DateTime?>("EnteredAt"), 
		EnteredBy = dr.Field<String>("EnteredBy"), 
		AccountManager = dr.Field<String>("AccountManager"), 
		AMName = dr.Field<String>("AMName"), 
		Estimator = dr.Field<String>("Estimator"), 
		CustomerID = dr.Field<String>("CustomerID"), 
		CustomerName = dr.Field<String>("CustomerName"), 
		Subdivision = dr.Field<String>("Subdivision"), 
		ProjectManager = dr.Field<String>("ProjectManager"), 
		Address = dr.Field<String>("Address"), 
		City = dr.Field<String>("City"), 
		State = dr.Field<String>("State"), 
		TotalAmt = dr.Field<Decimal?>("TotalAmt"), 
		ReasonCode = dr.Field<Int32?>("ReasonCode"), 
		ReasonStr = dr.Field<String>("ReasonStr"), 
		note = dr.Field<String>("note"), 
		ResolvedAt = dr.Field<DateTime?>("ResolvedAt"), 
		ResolvedBy = dr.Field<String>("ResolvedBy"), 
		CanceledAt = dr.Field<DateTime?>("CanceledAt"), 
		CanceledBy = dr.Field<String>("CanceledBy"), 
		SalesOrderID = dr.Field<String>("SalesOrderID"), 
		LotNumber = dr.Field<String>("LotNumber"), 
		Status = dr.Field<String>("Status"), 
		ResolveDays = dr.Field<Int32?>("ResolveDays"), 
		LastComment = dr.Field<String>("LastComment")
	});
	
	//find any changes in the records
	var AlteredRecords = sourceRows.Where(sr => !destRows.Contains(sr));
	
	//find any new records
	var NewRecords = AlteredRecords.Where(sr => !destRows.Select(dr => dr.id).Contains(sr.id)).Dump();
	
	//find any updated records
	var DeltaRecords = AlteredRecords.Except(NewRecords).Dump();
	
	AlteredRecords.Dump();
	NewRecords.Dump();
	DeltaRecords.Dump();
}

// Define other methods and classes here
