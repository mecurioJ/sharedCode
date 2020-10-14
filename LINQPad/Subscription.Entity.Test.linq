<Query Kind="Program">
  <Connection>
    <ID>2f022393-0a3d-4f8b-8ca6-5092c7898e30</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\CustDirect.dll</CustomAssemblyPath>
    <CustomTypeName>CustDirect.Data.SubscriptionEntities</CustomTypeName>
    <AppConfigPath>C:\Projects\SVN\trunk\CustDirect\CustDirect\Web.config</AppConfigPath>
    <DisplayName>SubscriptionEntities</DisplayName>
  </Connection>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\Antlr3.Runtime.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\CustDirect.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\EntityFramework.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\Microsoft.Data.Edm.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\Microsoft.Data.OData.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\Microsoft.Web.Infrastructure.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\Microsoft.Web.Mvc.FixedDisplayModes.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\Newtonsoft.Json.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\System.Net.Http.Formatting.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\System.Spatial.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\System.Web.Helpers.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\System.Web.Http.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\System.Web.Http.OData.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\System.Web.Http.WebHost.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\System.Web.Mvc.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\System.Web.Optimization.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\System.Web.Razor.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\System.Web.WebPages.Deployment.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\System.Web.WebPages.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\System.Web.WebPages.Razor.dll</Reference>
  <Reference>C:\Projects\SVN\trunk\CustDirect\CustDirect\bin\WebGrease.dll</Reference>
  <Namespace>CustDirect</Namespace>
  <Namespace>CustDirect.Controllers</Namespace>
  <Namespace>CustDirect.Data</Namespace>
  <Namespace>CustDirect.Models</Namespace>
</Query>

void Main()
{
	SubscriptionEntities db = new SubscriptionEntities();
	
	
	//(db.CustomerFilters.Find(new[]{"mjfilichia@hotmail.com","CRM10317","EMAT3"})
	
	//.Dump();
	
	(db.Subscribers.Find("mjfilichia@hotmail.co") == null ).Dump();
	
	//var list = Customer.CustomerList("EMAT3").Select(li => new{li.CustId, li.CustName}).ToList();
	
	//Newtonsoft.Json.JsonConvert.SerializeObject(list).Dump();
	
	
}

// Define other methods and classes here
