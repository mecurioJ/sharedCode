<Query Kind="Program">
  <NuGetReference>CsvHelper</NuGetReference>
  <Namespace>CsvHelper</Namespace>
  <Namespace>CsvHelper.Configuration</Namespace>
  <Namespace>CsvHelper.Configuration.Attributes</Namespace>
  <Namespace>CsvHelper.Expressions</Namespace>
  <Namespace>CsvHelper.TypeConversion</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>


void Main()
{
	#region user modifiable variables
	//Define the path where the file comes from
	var NetPath = @"\\goaspen\Department_Shares\Business Intelligence\Coupa\Archive";
	
	//retrieve the file paths into something usable	
	var userDataFile = @"sox___user_access_review___user_data_20200309_060132Z.csv";
	var approvalChainFile = @"sox___user_access_review___approval_chains_20200309_060125Z.csv";
	var lookupValuesFile = @"sox___user_access_review___lookup_values_20200309_060131Z.csv";
	#endregion
	
	#region end object instanciation
	//create entities for loading in code
	List<appChainParse> AppChain = new List<appChainParse>();
	List<User> Users = new List<User>();
	List<LookupValue> LookupValues = new List<LookupValue>();
	#endregion
	
	#region object logic
	//parse and extract custom approval chains
	using (var csv = new CsvReader(new StreamReader($"{NetPath}\\{approvalChainFile}"),CultureInfo.InvariantCulture))
	{
		var appChain = csv.GetRecords<appChainSource>().ToList();
		
		AppChain = appChain//.Where(ac => ac.Approvers.Contains(","))
			.SelectMany(
				ac => ac.Approvers.Split(',').Select(
						t1 => new appChainParse
						{
							Name = ac.Name.Trim(),
							Type = ac.Type.Trim(),
							Approver = t1.Split('(').FirstOrDefault().Replace("(",string.Empty).Replace(")",string.Empty).Trim(),
							CustomApprovalAmount = t1.Split('(').LastOrDefault().Replace("(",string.Empty).Replace(")",string.Empty)
						}
						
					)
			).ToList();
	}

	//parse and extract users
	using (var csv = new CsvReader(new StreamReader($"{NetPath}\\{userDataFile}"), CultureInfo.InvariantCulture))
	{
		Users = csv.GetRecords<User>().ToList();
	}

	//parse and extract Lookup Values
	//Note that ParentExternalRefCode is the GL Code, while ExternalRefNumber is the Cost Center
	using (var csv = new CsvReader(new StreamReader($"{NetPath}\\{lookupValuesFile}"), CultureInfo.InvariantCulture))
	{
		LookupValues = csv.GetRecords<LookupValue>().ToList();
	}

	#endregion

	using (var csv = new CsvWriter(new StreamWriter(@"C:\Users\joey.filichia\source\Projects\Coupa\approvalChains.csv"), CultureInfo.InvariantCulture))
	{
		csv.WriteRecords(AppChain);
	}

}

public class appChainSource
{
	public string Name { get; set; }
	public string Type { get; set; }
	public string Approvers { get; set; }
}

public class appChainParse
{
	public string Name { get; set; }
	public string Type { get; set; }
	public string Approver { get; set; }
	public string CustomApprovalAmount {get; set;}

}

public class User
{
	[Index(0)]
	public string FullName { get; set; }
	[Index(1)]
	public string Login { get; set; }
	[Index(2)]
	public string Email { get; set; }
	[Index(3)]
	public string Active { get; set; }
	[Index(4)]
	public string InvoiceApprovalLimitAmount { get; set; }
	[Index(5)]
	public string RequisitionApprovalLimitAmount { get; set; }
	[Index(6)]
	public string Approver { get; set; }
}

public class LookupValue
{
	[Index(0)]
	public string Name { get; set; }
	[Index(7)]
	public string GLCode { get; set; }
	[Index(4)]
	public string CostCenter { get; set; }
	[Index(1)]
	public string Active { get; set; }
	[Index(2)]
	public string Lookup { get; set; }
	[Index(3)]
	public string Description { get; set; }
	[Index(4)]
	public string ExternalRefNum { get; set; }
	[Index(5)]
	public string ExternalRefCode { get; set; }
	[Index(6)]
	public string ChartOfAccounts { get; set; }
	[Index(7)]
	public string ParentExternalRefCode { get; set; }
	[Index(8)]
	public string Default { get; set; }
	[Index(9)]
	public string CostCenterManager { get; set; }
	[Index(10)]
	public string CostCenterDirector { get; set; }
	[Index(11)]
	public string CostCenterVP { get; set; }
	[Index(12)]
	public string CostCenterSVP { get; set; }
	[Index(13)]
	public string CostCenterCFO { get; set; }
	[Index(14)]
	public string CostCenterCEO { get; set; }
	[Index(15)]
	public string RequiresCostCenterManagerApproval { get; set; }
	[Index(16)]
	public string RequiresCostCenterDirectorApproval { get; set; }
	[Index(17)]
	public string RequiresCostCenterVPApproval { get; set; }
	[Index(18)]
	public string RequiresCostCenterSVPApproval { get; set; }
	[Index(19)]
	public string RequiresCostCenterCFOApproval { get; set; }
	[Index(20)]
	public string RequiresCostCenterCEOApproval { get; set; }
	[Index(21)]
	public string GLTypeforCostCenter302 { get; set; }
}

// Define other methods, classes and namespaces here
