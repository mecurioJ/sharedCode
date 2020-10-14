<Query Kind="Program">
  <Connection>
    <ID>aeb12ee6-bf95-4264-b81f-290a8698202a</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>LSPOG</Database>
  </Connection>
  <NuGetReference>LinqToExcel</NuGetReference>
  <Namespace>LinqToExcel</Namespace>
  <Namespace>LinqToExcel.Attributes</Namespace>
  <Namespace>LinqToExcel.Domain</Namespace>
  <Namespace>LinqToExcel.Extensions</Namespace>
  <Namespace>LinqToExcel.Logging</Namespace>
  <Namespace>LinqToExcel.Query</Namespace>
</Query>

void Main()
{

		var di = new DirectoryInfo(@"D:\Proiects\LSPOG\Rev2\Rev2\Archives").GetFiles("*.xlsb").Select(fi => new{fi.FullName, fi.LastWriteTime});
		
		
		foreach (var fi in di)
		{
			var cld = 
			new ExcelQueryFactory(fi.FullName).Worksheet("CL30D")
			//.ToArray().Where(rw => !String.IsNullOrEmpty(rw[1].Value.ToString()))
			.Select(rw => new CL30D{
				TicketNumber = rw[0].Value.ToString(),
				OpenDate = rw[1].Value.ToString(),
				DaysOpen = rw[2].Value.ToString(),
				ClosedDate = rw[3].Value.ToString(),
				GroupName = rw[4].Value.ToString(),
				SiteName = rw[5].Value.ToString(),
				VISN = rw[6].Value.ToString(),
				Priority = rw[7].Value.ToString(),
				Status = rw[8].Value.ToString(),
				TransferCount = rw[9].Value.ToString(),
				TicketType = rw[10].Value.ToString(),
				SnapshotDate = fi.LastWriteTime.ToShortDateString()
			})
			.ToArray()
			.Where(op => !String.IsNullOrEmpty(op.OpenDate));
			
			
			CL30Ds.InsertAllOnSubmit(cld.ToArray());
			SubmitChanges();
			//cld.Dump();
		}
		
		
//			di.GetFiles("*.xlsb").Select(fi => new{fi.FullName, fi.LastWriteTime}).Select(
//			ti => new TicketSheet(ti.FullName,ti.LastWriteTime)
//			).Count();
	

}
//
//public class OP30D
//{
//	public String TicketNumber {get;set;}
//	public String OpenDate {get;set;}
//	public String GroupName {get;set;}
//	public String SiteName {get;set;}
//	public String VISN {get;set;}
//	public String Priority {get;set;}
//	public String Status {get;set;}
//	public DateTime SnapshotDate {get;set;}
//	
//}
//
//public class CL30D
//{
//	public String TicketNumber {get;set;}
//	public String OpenDate {get;set;}
//	public String GroupName {get;set;}
//	public String DaysOpen {get;set;}
//	public String ClosedDate {get;set;}
//	public String SiteName {get;set;}
//	public String VISN {get;set;}
//	public String Priority {get;set;}
//	public String Status {get;set;}
//	public String TransferCount {get;set;}
//	public String TicketType {get;set;}	
//	public DateTime SnapshotDate {get;set;}
//}
//
//public static int ParseInt(object obj)
//	{
//		return String.IsNullOrEmpty(obj.ToString())
//			? 0
//			: obj.Cast<int>();
//	}
//public static double ParseDouble(object obj)
//	{
//		return String.IsNullOrEmpty(obj.ToString())
//			? 0d
//			: obj.Cast<double>();
//	}
//
//public class SiteSummary
//{
//	public String VISN {get;set;}
//public String SiteName {get;set;}
//public int GT90Days {get;set;}
//public int GT61LT90Days {get;set;}
//public int GT31LT60Days {get;set;}
//public int GT15LT30Days {get;set;}
//public int GT04LT15Days {get;set;}
//public int GT0LT4Days {get;set;}
//public int AllGT14 {get;set;}
//public int ALLGT30 {get;set;}
//public int ALLGT60 {get;set;}
//public int PriorGT30 {get;set;}
//public int WKTrend {get;set;}
//public int YTDTrend {get;set;}
//public int Pri1Inc {get;set;}
//public int Pri1Req {get;set;}
//public int Pri2Inc {get;set;}
//public int Pri2Req {get;set;}
//public int OpTickets {get;set;}
//public int OpTicketsGT4 {get;set;}
//public int CLTicketsGT4 {get;set;}
//public int TotalTicketsGT4 {get;set;}
//public int TotalOpCl30D {get;set;}
//public int Op30D {get;set;}
//public int CL30D {get;set;}
//public double Backlog {get;set;}
//public double OPADO {get;set;}
//public double BounceRate {get;set;}
//public double Efficiency {get;set;}
//public double MTTRAll {get;set;}
//public double MTTRInc {get;set;}
//public double MTTRReq {get;set;}
//public DateTime SnapshotDate {get;set;}
//}
//
//
//public class VISNSummary
//{
//	public String VISN {get;set;}
//public int GT90Days {get;set;}
//public int GT61LT90Days {get;set;}
//public int GT31LT60Days {get;set;}
//public int GT15LT30Days {get;set;}
//public int GT04LT15Days {get;set;}
//public int GT0LT4Days {get;set;}
//public int AllGT14 {get;set;}
//public int ALLGT30 {get;set;}
//public int ALLGT60 {get;set;}
//public int PriorGT30 {get;set;}
//public int WKTrend {get;set;}
//public int YTDTrend {get;set;}
//public int Pri1Inc {get;set;}
//public int Pri1Req {get;set;}
//public int Pri2Inc {get;set;}
//public int Pri2Req {get;set;}
//public int OpTickets {get;set;}
//public int OpTicketsGT4 {get;set;}
//public int CLTicketsGT4 {get;set;}
//public int TotalTicketsGT4 {get;set;}
//public int TotalOpCl30D {get;set;}
//public int Op30D {get;set;}
//public int CL30D {get;set;}
//public double Backlog {get;set;}
//public double OPADO {get;set;}
//public double BounceRate {get;set;}
//public double Efficiency {get;set;}
//public double MTTRAll {get;set;}
//public int UnAIncGT14 {get;set;}
//public int UnAIncGT30 {get;set;}
//public int UnAInc {get;set;}
//public DateTime SnapshotDate {get;set;}
//}
//
//public class RegionSummary
//{
//	public String RegionName {get;set;}
//	public int GT90Days {get;set;}
//	public int GT61LT90Days {get;set;}
//	public int GT31LT60Days {get;set;}
//	public int GT15LT30Days {get;set;}
//	public int GT04LT15Days {get;set;}
//	public int GT0LT4Days {get;set;}
//	public int AllGT14 {get;set;}
//	public int ALLGT30 {get;set;}
//	public int ALLGT60 {get;set;}
//	public int PriorGT30 {get;set;}
//	public int WKTrend {get;set;}
//	public int YTDTrend {get;set;}
//	public int Pri1Inc {get;set;}
//	public int Pri1Req {get;set;}
//	public int Pri2Inc {get;set;}
//	public int Pri2Req {get;set;}
//	public int OpTickets {get;set;}
//	public int OpTicketsGT4 {get;set;}
//	public int UnAInc {get;set;}
//	public int Op30D {get;set;}
//	public int Cl30D {get;set;}
//	public double Backlog {get;set;}
//	public double OPADO {get;set;}
//	public double BounceRate {get;set;}
//	public double Efficiency {get;set;}
//	public double MTTRAll {get;set;}
//	public int UnAIncGT14 {get;set;}
//	public int UnAIncGT30 {get;set;}
//	public DateTime SnapshotDate {get;set;}
//}
//
////public class OP30D
////{
////	public String TicketNumber {get;set;}
////	public DateTime OpenDate {get;set;}
////	public String GroupName {get;set;}
////	public String SiteName {get;set;}
////	public String VISN {get;set;}
////	public int Priority {get;set;}
////	public String Status {get;set;}
////	public DateTime SnapshotDate {get;set;}
////	
////}
////
////public class CL30D
////{
////	public String TicketNumber {get;set;}
////	public DateTime OpenDate {get;set;}
////	public String GroupName {get;set;}
////	public int DaysOpen {get;set;}
////	public DateTime ClosedDate {get;set;}
////	public String SiteName {get;set;}
////	public String VISN {get;set;}
////	public int Priority {get;set;}
////	public String Status {get;set;}
////	public int TransferCount {get;set;}
////	public String TicketType {get;set;}	
////	public DateTime SnapshotDate {get;set;}
////}
//
//public class TicketDetail
//{
//	public String VISN {get;set;}
//	public String SiteName {get;set;}
//	public String TicketNumber {get;set;}
//	public DateTime OpenDate {get;set;}
//	public int Priority {get;set;}
//	public String GroupName {get;set;}
//	public String Assignee {get;set;}
//	public String Status {get;set;}	
//	public int DaysOpen {get;set;}
//	public int TXCount {get;set;}
//	public int TXGroupCount {get;set;}
//	public int TXAssigneeCount {get;set;}
//	public String Summary {get;set;}	
//	public String Type {get;set;}	
//	public DateTime SnapshotDate {get;set;}
//}
//
//
//
//// Define other methods and classes here
///*
//CL30D 
//DATA 
//OP30D
//Main  
//Pivot Data
//Ticket Pivot 
//*/