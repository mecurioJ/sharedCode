<Query Kind="Program" />

void Main()
{
	var sourcePath = @"D:\Projects\Frontier\Skyledger\Data\";
	var destinationPath = @"F:\";
	var Folder = @"SVCSREarnOverTime";
	

	//does the destination exist?
	var push = $@"{destinationPath}{Folder}\";
	var pull = $"{sourcePath}{Folder}";
	
	if(!Directory.Exists(push))
		{
			Directory.CreateDirectory(push);
		}
	


	var Files = new DirectoryInfo(@$"{pull}").GetFileSystemInfos($"*.csv", SearchOption.AllDirectories);

	var splitter = "_";

	var remap = Files.Select(
		fi => new
		{
			NewFileName =
			push +
			fi.FullName.Split(@"\")[6].Split(splitter).Last().Replace($"{Folder}.",string.Empty) + 
						"_" + 
						Regex.Replace(
						fi.Name.Replace(".csv", string.Empty), 
						@"^SkyLedgerReconciliationExtract_FrontierAirlines_Credit|SkyLedgerReconciliationExtract_FrontierAirlines|INC000000808869|Frontier|ExtractsByPeriod_"
						, fi.LastWriteTimeUtc.ToString("yyyyMMddHHmmss")) +
						fi.Extension,
			OldFileName = fi.FullName,
			fi.Extension,
			indc = fi.FullName.Split(@"\")[6].Split(splitter).Last(),
			dirc = fi.FullName,
			WriteDAte = fi.LastWriteTimeUtc.ToString("yyyyMMddHHmmss")
		}
	)
	.OrderBy(x => x.NewFileName)
	.Dump()
	;

	

	foreach (var fi in remap)
	{   
		File.Copy(@$"{fi.OldFileName}", @$"{fi.NewFileName}",true);
	}
	
}

// Define other methods, classes and namespaces here