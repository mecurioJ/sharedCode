<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
</Query>

void Main()
{
	var FileSet = Directory.GetFiles(@"R:\Projects\OraPowerSchool\OraPowerSchool\Scripts\TN\");
	
	FileSet.Select(fi => new{
	fileName = fi,
	fileLines = File.ReadAllLines(fi).ToList()
		.Where(ln => ln.ToLower().Contains("from "))
		.Where(ln => !ln.ToLower().Contains("--"))
		.Where(ln => !ln.ToLower().Contains("cst_"))
		.Where(ln => !ln.ToLower().Contains("from dual"))
		.Where(ln => !ln.ToLower().Contains("v_"))
		.Where(ln => !ln.ToLower().Contains("from virtualtablesdata"))
		.Distinct()
	})
	.Where(tn => tn.fileLines.Count() > 0)
	.Where(tn => tn.fileName.Contains("EDFI"))
	.SelectMany(fi => fi.fileLines).Distinct()
	.Select(li => li.Trim())
	.OrderBy(li => li).Distinct()
	.Dump();
/*
	var plSQlSourceLines = File
		.ReadAllLines(@"R:\Projects\OraPowerSchool\OraPowerSchool\Scripts\TN\TN_EDFI_COMMON_BODY.sql")
		.ToList();
		
	plSQlSourceLines
	.Where(ln => ln.ToLower().Contains("from "))
	.Where(ln => !ln.ToLower().Contains("--"))
	.Where(ln => !ln.ToLower().Contains("cst_"))
	.Where(ln => !ln.ToLower().Contains("from dual"))
	.Where(ln => !ln.ToLower().Contains("v_"))
	.Where(ln => !ln.ToLower().Contains("from virtualtablesdata"))
	.Distinct()
	.Dump();
*/	
	
	
	
	
}

// Define other methods and classes here
