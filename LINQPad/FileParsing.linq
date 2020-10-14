<Query Kind="Program">
  <NuGetReference>CsvHelper</NuGetReference>
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>CsvHelper</Namespace>
  <Namespace>CsvHelper.Configuration</Namespace>
  <Namespace>CsvHelper.Configuration.Attributes</Namespace>
  <Namespace>CsvHelper.Expressions</Namespace>
  <Namespace>CsvHelper.TypeConversion</Namespace>
  <Namespace>MoreLinq</Namespace>
  <Namespace>MoreLinq.Extensions</Namespace>
</Query>

void Main()
{
	var pathing = @"\\goaspen\Department_Shares\Business Intelligence\Coupa\Inbound\";
	var lookupValuesFilter = @"sox___user_access_review___lookup_values_*.csv";
	var approvalChainsFilter = @"sox___user_access_review___approval_chains_*.csv";
	var userDataFilter = @"sox___user_access_review___user_data_*.csv";
	
	DirectoryInfo di = new DirectoryInfo(pathing);
	//	var lookupValues = di.GetFiles(lookupValuesFilter).Select(lv => new CsvReader(File.OpenText(lv.FullName),).Read());
	var approvalChains = di.GetFiles(approvalChainsFilter).Select(lv => { 
		var dt = new DataTable();
		dt.Load(new CsvDataReader(new CsvReader(File.OpenText(lv.FullName))));
		return dt;
		});
//	var userData = di.GetFiles(userDataFilter).SelectMany(lv => new CsvParser(File.OpenText(lv.FullName)).Read()).Distinct();
//	lookupValues.Dump();
	approvalChains.Dump();
}

// Define other methods and classes here
