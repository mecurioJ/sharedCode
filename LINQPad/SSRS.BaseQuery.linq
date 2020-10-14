<Query Kind="Program">
  <Reference Relative="Libs\ReportingServices.dll">C:\SyncDrives\Dropbox\LinqPad\Libs\ReportingServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Namespace>ReportingServices</Namespace>
</Query>

void Main()
{
	var SSRSurl = @"http://DEN-SP-01/ReportServer/ReportService2010.asmx";
	var reportPath = "/ESWReporting/ClipstoneSales";
	
	
	ReportingService2010 ssrs = new ReportingService2010();
	ssrs.Url = SSRSurl;
	ssrs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
	
	
	
	ReportingServices.CatalogItem ClipStone = ssrs.ListChildren("/",true).ToList().Where(ci => ci.Path.Equals(reportPath)).FirstOrDefault();
	
	ClipStone.Dump();
	
	

	
	
}

// Define other methods and classes here
