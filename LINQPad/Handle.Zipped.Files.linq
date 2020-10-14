<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>MoreLinq.Extensions</Namespace>
  <Namespace>System.IO.Compression</Namespace>
  <Namespace>System.IO.Compression.FileSystem</Namespace>
</Query>

void Main()
{
	//Get the compressed files
	var SourcePath = @"G:\SkyLedger\Source\";
	var DestinationPath = @"D:\Projects\Frontier\Skyledger\Data\";

	//Get the Source Directories
	foreach (var name in new DirectoryInfo(SourcePath).GetDirectories().Select(d => d.Name))
	{
		Directory.CreateDirectory($@"D:\Projects\Frontier\Skyledger\Data\{name}");
	}


	var SourceDir = new DirectoryInfo(SourcePath);
	var compressed = SourceDir.GetFiles(@"*.zip", new EnumerationOptions() {RecurseSubdirectories = true})
	.Select(
		c => new
		{
			Destination = c.DirectoryName.Replace(SourcePath,DestinationPath),
			ZipName = c.Name,
			YearMonth = Regex.Match(c.Name,@"_\d{6}").Value,
			Contents = ZipFile.OpenRead(c.FullName)
		}
	);

	//var doodah = compressed.SelectMany(item => item.Contents.Entries);
	//compressed.Dump();

	foreach (var item in compressed)
	{
		foreach (var Entry in item.Contents.Entries)
		{
			string destinationName = @$"{item.Destination}\{item.YearMonth}_{Entry.Name}";
					
			
			if(File.Exists(destinationName))
			{
				//are the files the same	
				if (Entry.GetHashCode().Equals(File.Open(destinationName, FileMode.Open).GetHashCode()))
					return;
				
				int nextVal = 
					Regex.Match(destinationName,@"\(\d\)").Success
					? int.Parse(Regex.Match(destinationName,@"\(\d\)").Value.Replace("(",string.Empty).Replace(")",string.Empty))+1
					: 1;

				Entry.ExtractToFile($"{destinationName}({nextVal})");
					
				}
				else
				{
					Entry.ExtractToFile(destinationName);		
				}
		}
	}
	
	
	
}

// Define other methods, classes and namespaces here