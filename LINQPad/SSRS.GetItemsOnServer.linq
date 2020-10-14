<Query Kind="Program">
  <Reference Relative="Libs\ExecutionService.dll">C:\SyncDrives\Dropbox\LinqPad\Libs\ExecutionService.dll</Reference>
  <Reference Relative="Libs\ReportService.dll">C:\SyncDrives\Dropbox\LinqPad\Libs\ReportService.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
</Query>

void Main()
{
	var creds = System.Net.CredentialCache.DefaultCredentials;
	
	ReportExecutionService rsExSvc = new ReportExecutionService();
	ReportingService2010 rsSvc = new ReportingService2010();

	rsExSvc.Url = "http://den-sp-01/ReportServer/ReportExecution2005.asmx?WSDL";
	rsSvc.Url = "http://den-sp-01/ReportServer/ReportService2010.asmx?wsdl";
	rsExSvc.Credentials = rsSvc.Credentials = new System.Net.NetworkCredential("jfilichia","iem!adm1n","emllc.loc");
	
	var testReport = "/ESWReporting/ClipstoneSales";
	
	//rsSvc.GetItemReferences(testReport,"").Dump();
	Subscription[] sub = rsSvc.ListSubscriptions(testReport).Dump();
	
	rsSvc.SetSubscriptionProperties
	
	
	
//	rsSvc.Credentials.Dump();
//	
//	//how to set subscription properties
//	//rsSvc.SetSubscriptionProperties("",new ExtensionSettings(),"","","",new ParameterValue[]);
//	
//	rsSvc.ListChildren(@"/",true)
//	.Select(rc => new{
//		catItem = rc,
//		itemParameters = rc.TypeName.Equals("Report")
//			? rsSvc.GetItemParameters(rc.Path,null, false,null, null)
//			: null,
//		Subscription = rc.TypeName.Equals("Report")
//			? rsSvc.ListSubscriptions(rc.Path)
//			: null
//	})
//	.Dump();
	
	
	
}

// Define other methods and classes here