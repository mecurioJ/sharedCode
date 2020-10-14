<Query Kind="Program">
  <Connection>
    <ID>359f551a-465b-4231-b045-807385f318cd</ID>
    <Persist>true</Persist>
    <Server>JOEYSURFACEPRO</Server>
    <Database>LSPOG</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <NuGetReference>LinqToExcel</NuGetReference>
  <Namespace>LinqToExcel</Namespace>
  <Namespace>LinqToExcel.Attributes</Namespace>
  <Namespace>LinqToExcel.Domain</Namespace>
  <Namespace>LinqToExcel.Extensions</Namespace>
  <Namespace>LinqToExcel.Logging</Namespace>
  <Namespace>LinqToExcel.Query</Namespace>
  <Namespace>Remotion</Namespace>
  <Namespace>Remotion.BridgeInterfaces</Namespace>
  <Namespace>Remotion.Collections</Namespace>
  <Namespace>Remotion.Context</Namespace>
  <Namespace>Remotion.Data.Linq</Namespace>
  <Namespace>Remotion.Data.Linq.Clauses</Namespace>
  <Namespace>Remotion.Data.Linq.Clauses.Expressions</Namespace>
  <Namespace>Remotion.Data.Linq.Clauses.ExpressionTreeVisitors</Namespace>
  <Namespace>Remotion.Data.Linq.Clauses.ResultOperators</Namespace>
  <Namespace>Remotion.Data.Linq.Clauses.StreamedData</Namespace>
  <Namespace>Remotion.Data.Linq.Collections</Namespace>
  <Namespace>Remotion.Data.Linq.EagerFetching</Namespace>
  <Namespace>Remotion.Data.Linq.EagerFetching.Parsing</Namespace>
  <Namespace>Remotion.Data.Linq.Parsing</Namespace>
  <Namespace>Remotion.Data.Linq.Parsing.ExpressionTreeVisitors</Namespace>
  <Namespace>Remotion.Data.Linq.Parsing.ExpressionTreeVisitors.MemberBindings</Namespace>
  <Namespace>Remotion.Data.Linq.Parsing.ExpressionTreeVisitors.TreeEvaluation</Namespace>
  <Namespace>Remotion.Data.Linq.Parsing.Structure</Namespace>
  <Namespace>Remotion.Data.Linq.Parsing.Structure.IntermediateModel</Namespace>
  <Namespace>Remotion.Data.Linq.Transformations</Namespace>
  <Namespace>Remotion.Data.Linq.Utilities</Namespace>
  <Namespace>Remotion.Design</Namespace>
  <Namespace>Remotion.Implementation</Namespace>
  <Namespace>Remotion.Logging</Namespace>
  <Namespace>Remotion.Logging.BridgeInterfaces</Namespace>
  <Namespace>Remotion.Mixins</Namespace>
  <Namespace>Remotion.Mixins.BridgeInterfaces</Namespace>
  <Namespace>Remotion.Reflection</Namespace>
  <Namespace>Remotion.Reflection.TypeDiscovery</Namespace>
  <Namespace>Remotion.Utilities</Namespace>
</Query>

void Main()
{
//	var xlsxFileName = @"D:\Proiects\LSPOG\Rev2\RD1 All Open Tickets Report 2017.06.19.xlsx";
//	var region = "RD1";
//	var snapshotDate = "06/19/2017";

	DirectoryInfo dir = new DirectoryInfo(@"D:\Proiects\LSPOG\Rev2\");
	
	var files = 	dir.GetFiles("VACO*.xlsx",SearchOption.AllDirectories);
	
	String region;
	String snapshotDate;
	String xlsxFileName;
	
	
	foreach (var fi in files)
	{
		xlsxFileName = fi.FullName;
		region = "VACO"; //fi.Name.Substring(0,4);
		snapshotDate = DateTime.Parse(Regex.Match(fi.Name,"[0-9]{4}.[0-9]{2}.[0-9]{2}").Value.Replace(".","/")).ToShortDateString();
		
		
		ExcelQueryFactory file = new ExcelQueryFactory(xlsxFileName);
		
		var Open30 = Conversions.OP30DRows(xlsxFileName, region, snapshotDate);
		
		var Closed30 = 	Conversions.CL30DRows(xlsxFileName, region, snapshotDate);
		var DataAll = Conversions.DataRows(xlsxFileName, region, snapshotDate);
		
		new{
		xlsxFileName,
		region,
		snapshotDate,
		Open30 = Open30.Count(),
		Closed30 = Closed30.Count(),
		DataAll = DataAll.Count(),
		}.Dump();
		
		Data.InsertAllOnSubmit(DataAll);
		CL30Ds.InsertAllOnSubmit(Closed30);
		OP30Ds.InsertAllOnSubmit(Open30);
		
		SubmitChanges();
		
		
	}
	
	

	
	


	


	
}

// Define other methods and classes here

	public static int ParseCellValue(Cell x)
	{
		return Regex.IsMatch(x.ToString(),"[0-9]")
			? int.Parse(x.ToString())
			: 0;
	}
	
	
	public static  DateTime ParseCellValueDt(Cell x)
	{
		return Regex.IsMatch(x.ToString(),"\\b(?<month>\\d{1,2})/(?<day>\\d{1,2})/(?<year>\\d{2,4})\\b")
			? DateTime.Parse(x.ToString())
			: DateTime.MinValue;
	}

public class Conversions
{

public static IEnumerable<CL30D> CL30DRows (string xlsxFileName, string region, string snapshotDate)
{
	ExcelQueryFactory file = new ExcelQueryFactory(xlsxFileName);
	
	return file.Worksheet("CL30D")
	.ToList()
	.Select(cl => new CL30D()
	{
		Region = region,
		SnapshotDate = DateTime.Parse(snapshotDate),
		VISN = cl.ColumnNames.Contains("VISN")
			? cl["VISN"].Value.ToString()  
			: cl["District"].Value.ToString(),  
		Site = cl.ColumnNames.Contains("Site")
			? cl["Site"].Value.ToString()
			: cl["Org"].Value.ToString(),
		TicketNumber = cl.ColumnNames.Contains("Ticket #")
			? cl["Ticket #"].Value.ToString()
			: cl["Ref Num"].Value.ToString(), 
		OpenDate = ParseCellValueDt(cl["Open Date"]),
		DaysOpen = ParseCellValue(cl["Days Open"]),  
		CloseDate = ParseCellValueDt(cl["Close Date"]),  
		GroupName = cl["Group Name"].Value.ToString(),  
		Priority = cl["Priority"].Value.ToString(),  
		Status = cl["Status"].Value.ToString(),  
		TxCount = ParseCellValue(cl["Tx Count"]),  
		TicketType = cl["Type"].Value.ToString(),  
	});
}

public static IEnumerable<OP30D> OP30DRows (string xlsxFileName, string region, string snapshotDate)
{
	ExcelQueryFactory file = new ExcelQueryFactory(xlsxFileName);
	
	return file.Worksheet("OP30D")
	.ToList()
	.Select(cl => new OP30D(){
		Region = region,
		SnapshotDate = DateTime.Parse(snapshotDate),
		VISN = cl.ColumnNames.Contains("VISN")
			? cl["VISN"].Value.ToString()  
			: cl["District"].Value.ToString(),  
		Site = cl.ColumnNames.Contains("Site")
			? cl["Site"].Value.ToString()
			: cl["Org"].Value.ToString(),
		TicketNumber = cl.ColumnNames.Contains("Ticket #")
			? cl["Ticket #"].Value.ToString()
			: cl["Ref Num"].Value.ToString(),  
		OpenDate = ParseCellValueDt(cl["Open Date"]), 
		GroupName = cl["Group Name"].Value.ToString(), 
		Priority = cl["Priority"].Value.ToString(),
		Status = cl["Status"].Value.ToString()
	
	});
}

public static IEnumerable<Data> DataRows (string xlsxFileName, string region, string snapshotDate)
	{
		ExcelQueryFactory file = new ExcelQueryFactory(xlsxFileName);
	
		IEnumerable<Data> dataRows = file.WorksheetNoHeader("DATA")
		.Select(x => new dataTicketItem(x, region,snapshotDate))
		.ToList().Where(dti => dti.IsValid)
		.Select(dd => new Data()
		{
			Region = dd.Region,
			SnapshotDate = dd.SnapshotDate,
			VISN = dd.VISN,
			Site = dd.Site,
			TicketNumber = dd.TicketNumber,
			OpenDate = DateTime.Parse(dd.OpenDate),
			Priority = dd.Priority,
			GroupName = dd.GroupName,
			Assignee = dd.Assignee,
			Status = dd.Status,
			DaysOpen = dd.DaysOpen,
			TxTotalCount = dd.TxTotalCount, 
			TxGroupCount = dd.TxGroupCount,
			TxAssigneeCount = dd.TxAssigneeCount,
			Summary = dd.Summary,
			TicketType = dd.TicketType
			
		}).ToList()
		;
		return dataRows;
	}

	public static int ParseCellValue(Cell x)
	{
		return Regex.IsMatch(x.ToString(),"[0-9]")
			? int.Parse(x.ToString())
			: 0;
	}
	
	
	public static  DateTime ParseCellValueDt(Cell x)
	{
		return Regex.IsMatch(x.ToString(),"\\b(?<month>\\d{1,2})/(?<day>\\d{1,2})/(?<year>\\d{2,4})\\b")
			? DateTime.Parse(x.ToString())
			: DateTime.MinValue;
	}
}	

public class dataTicketItem
{
	

	public dataTicketItem(){}
	
	public dataTicketItem(RowNoHeader x, string region, string snapShotDate)
	{
		IsValid = Regex.IsMatch(x[4].Value.ToString(),"\\b(?<month>\\d{1,2})/(?<day>\\d{1,2})/(?<year>\\d{2,4})\\b");
		Region = region; //"RD1";
		SnapshotDate = DateTime.Parse(snapShotDate);
		VISN = x[1].Value.ToString();
		Site = x[2].Value.ToString();
		TicketNumber = x[3].Value.ToString();
		OpenDate = x[4].Value.ToString();
		Priority = x[5].Value.ToString();
		GroupName = x[6].Value.ToString();
		Assignee = x[7].Value.ToString();
		Status = x[8].Value.ToString();
		DaysOpen = ParseCellValue(x[9]);
		TxTotalCount =  ParseCellValue(x[10]);
		TxAssigneeCount = ParseCellValue(x[11]);
		TxGroupCount = ParseCellValue(x[12]);
		Summary = x[13].Value.ToString();
		TicketType = x[14].Value.ToString();
	}
	
	private int ParseCellValue(Cell x)
	{
		return Regex.IsMatch(x.ToString(),"[0-9]")
			? int.Parse(x.ToString())
			: 0;
	}

	public bool IsValid {get;set;}
	public String Region { get; set; }
	public DateTime SnapshotDate { get; set; }
	public String VISN { get; set; }
	public String Site { get; set; }
	public String TicketNumber { get; set; }
	public String OpenDate { get; set; }
	public String Priority { get; set; }
	public String GroupName { get; set; }
	public String Assignee { get; set; }
	public String Status { get; set; }
	public int DaysOpen { get; set; }
	public int TxTotalCount { get; set; }
	public int TxGroupCount { get; set; }
	public int TxAssigneeCount { get; set; }
	public String Summary { get; set; }
	public String TicketType { get; set; }
}
