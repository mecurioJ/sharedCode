<Query Kind="Program" />

void Main()
{
	var dir = new DirectoryInfo(@"D:\Projects\Frontier\CreditShell").GetFiles("*.csv");
	var files = 
	dir.Select(fi => new{
		fi.Name,
		contents = File.ReadAllLines(fi.FullName)
			.Where(li => !String.IsNullOrEmpty(li))
			//Remove the Rows Affected lines
			.Except(File.ReadAllLines(fi.FullName).Where(li => Regex.IsMatch(li,@"\([0-9]{1,10}\srows affected\)")))
			//only get rows with data
			.Where(li => Regex.Matches(li,@"\w,").Any())
	});
	
	files.Select(fi => new{
		fi.Name,
		FirstRow = fi.contents.FirstOrDefault().Split(',').Length,
		Data = fi.contents.Skip(1),
		InvalidData = fi.contents.Skip(1).Select(li => li.Split(',').Length).Distinct()		
	}).Dump();
}

// Define other methods, classes and namespaces here
