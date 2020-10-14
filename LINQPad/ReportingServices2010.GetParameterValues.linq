<Query Kind="Program">
  <Reference Relative="Libs\ReportingServices.dll">C:\SyncDrives\Dropbox\LinqPad\Libs\ReportingServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Namespace>ReportingServices</Namespace>
</Query>

void Main()
{	
	var ReportServerPath = "http://localhost/reportserver/reportservice2010.asmx";
	var itemPath = "/CustomReports/JobOpenCost";
	
	List<IEnumerable<ParameterValue>> Parameters = new List<IEnumerable<ParameterValue>>();
	
	//Connect To Service
	ReportingService2010 service2010 = new ReportingService2010
           {
               Url = ReportServerPath,
               Credentials = System.Net.CredentialCache.DefaultCredentials
           };
		   
	//Get Report Parameters:
    List<ItemParameter> ItemParameters = service2010
		.GetItemParameters(itemPath,null,true,null,null).Cast<ItemParameter>().ToList();
	
	//find Entity (only one)
	var tti = ItemParameters.Where(prm => prm.Name.Equals("Entity")).Select(vv => new{
		vv.Name,
		ValidValues = vv.ValidValues.Select(lv => new KeyValuePair<String,String>(lv.Label,lv.Value))
	}).FirstOrDefault();
	
	//load each entity into an anonymous Type
	foreach (var e in tti.ValidValues)
	{
		//Create a parameter value to pass back to service
		ParameterValue pv = new ParameterValue(){Name = tti.Name, Label = e.Key, Value = e.Value};
		
		//Get the customers for each entity
		var itemSet = service2010.GetItemParameters(itemPath,null,true,new[]{pv},null).Cast<ItemParameter>().Where(ci => ci.Name.Equals("Customer")).Select(vv => new{
						vv.Name,
						ValidValues = vv.ValidValues.Select(lv => new KeyValuePair<String,String>(lv.Label,lv.Value)).Select(cc => new ParameterValue
						{
							Name = vv.Name,
							Label = cc.Key,
							Value = cc.Value
						})
					}).SelectMany(tt => tt.ValidValues);
		
					
		//Get subdivisions for each customer (this can be a multi-select)
		foreach (var val in itemSet)
		{
		
		//Create a parameter value to pass back to service
			var pvs = new ParameterValue[]{pv,val};
			
			//Add to collection
			Parameters.Add(
			pvs.Union(service2010.GetItemParameters(itemPath,null,true,pvs,null).Cast<ItemParameter>().Where(ip => ip.Name.Equals("Subdivision")).SelectMany(div => div.ValidValues.Select(cc => new ParameterValue
						{
							Name = div.Name,
							Label = cc.Label,
							Value = cc.Value
						}))));
		}			
	}
	
}

