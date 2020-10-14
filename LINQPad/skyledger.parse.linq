<Query Kind="Program">
  <Namespace>System.IO.Compression</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

void Main()
{
	var Sourcedir = @"F:\CreditShell\";
	var fileTypes = new string[]{"CSHFX","CSHUR_CF","CSHUR_CS","CSHXR","PMTPM_CF","PMTPM_CS"};
	
	//var zip = ZipFile.OpenRead(@$"{Sourcedir}{file}");
	//zip.Entries.First().ExtractToFile(@$"{Destindir}\202002_{zip.Entries.First().FullName.Split('_').Last()}",true);
	
	//Get all files:
	//$"*{}.*"
	
	var CSHFXFiles = new DirectoryInfo(Sourcedir).GetFiles();
	var FileList = new DirectoryInfo(Sourcedir).GetFiles();

	var FileItems = FileList.Select(fi => new CreditShell(new FileItem(fi)));
	
	FileItems.Dump();
	
	/*
	//parse the file to exclude seperator line
	var items = FileList.Select((r, i) => new {idx=i,Row=r}).Select(fl => new {
		fl.idx,
		fl.Row.Name,
		TotalLines = File.ReadAllLines(fl.Row.FullName).Count(),
		WhiteSpace = File.ReadAllLines(fl.Row.FullName).Where(li => String.IsNullOrEmpty(li)).Count(),
		RowCount = File.ReadAllLines(fl.Row.FullName).Where(li => Regex.IsMatch(li,@$"\([0-9]*\srows\saffected\)",RegexOptions.IgnoreCase))
		.Select(li => Int32.Parse(li.Replace(" rows affected)",string.Empty).Replace("(",string.Empty))).FirstOrDefault(),
		Break = File.ReadAllLines(fl.Row.FullName).Where(li => li.StartsWith("--")),
		Headers = File.ReadAllLines(fl.Row.FullName).Take(1)	
	});
	
	items.Dump();
		
	

//	var contents = File.ReadAllLines(@"D:\Projects\Frontier\CreditShell\ATL_ER\202002_ERExtract.csv").Where(li => Regex.Match(li, @"[a-zA-Z](?!-)*,").Success)
//	.Select((r, i) => new {idx=i,Row=r});
//	
//	contents.Count().Dump();
//	
//	var frags = contents.Where(t => t.Row.Split(',').Length < 15);
//
//	var correct = contents.Except(frags).Select(r => r.Row).ToList();
//
//	correct.Count().Dump();
//	
//	using (var frag = frags.GetEnumerator())	
//	{
//		while (frag.MoveNext())
//		{
//			//get the current and the next one...
//			string firstPart = frag.Current.Row;
//			frag.MoveNext();
//			var secondPart = $"{firstPart}{frag.Current.Row}";
//			correct.Add(secondPart);
//		}
//	}


	//correct.Select(k => k.Split(',').Length).Distinct().Dump();



	//.Where(li => li.Split(',').Length == 13 || li.Split(',').Length == 2)
	//.Select(li => li.Split(','))
	//.Select((itm, i) => new
	//	{
	//		idx = i,
	//		LedgerKey = itm[0],
	//		FlightDate = itm[1],
	//		GDSRecLoc = itm[2],
	//		BookingDate = itm[3],
	//		LegOrigin = itm[4],
	//		LegDest = itm[5],
	//		SegmentOrigin = itm[6],
	//		SegmentDest = itm[7],
	//		FareClass = itm[8],
	//		ModifiedAgent = itm[9],
	//		BookingAgent = itm[10],
	//		AirlineCode = itm[11],
	//		//ScheduledDeparture = itm[12],
	//		//FlightNumber = itm[13],
	//		//HostAmount  = itm[14],
	//	}
	//
	//)
	//.Dump();
	//we need to find the first row that has what looks like headers...
	
	
	*/
	
}

// Define other methods, classes and namespaces here
public class FileItem
{

	public FileItem() {}
	public FileItem(FileInfo fi) {
		Name = fi.Name;
		Contents = File.ReadAllLines(fi.FullName);
	}
	public string Name {get; set;}
	public string[] Contents {get;set;}
}

public class CreditShellType
{
	public String Type {get;set;}
	public int Columns{get;set;}
}

public class CreditShell
{
	public string Name { get; set; }
	public string[] Header { get; set; }
	public int RowsActual { get; set; }
	public Dictionary<Int32, String[]> RowContents { get; set; }
	public CreditShellType CSType {get; set;}

	protected readonly CreditShellType CSHFX = new CreditShellType() {Type = "CSHFX", Columns = 41};
	protected readonly CreditShellType CSHUR_CF = new CreditShellType() {Type = "CSHUR_CF", Columns = 41};
	protected readonly CreditShellType CSHUR_CS = new CreditShellType() {Type = "CSHUR_CS", Columns = 41};
	protected readonly CreditShellType CSHXR = new CreditShellType() {Type = "CSHXR", Columns = 41};
	protected readonly CreditShellType PMTPM_CF = new CreditShellType {Type = "PMTPM_CF", Columns = 35};
	protected readonly CreditShellType PMTPM_CS = new CreditShellType {Type = "PMTPM_CS", Columns = 35};

	public CreditShell() {}
	public CreditShell(FileItem fi)
	{
		Name = fi.Name;
		Header = fi.Contents.Take(1).ToArray();
		RowContents = fi.Contents.Skip(1)
			.Where(cr => !cr.StartsWith("---"))
			.Where(cr => !String.IsNullOrEmpty(cr))
			.Where(cr => !Regex.IsMatch(cr, @"\([0-9]*\srows\saffected\)"))
			.Select((r, i) => new { idx = i, Row = r.Split(',') }).ToDictionary(d => d.idx, d => d.Row);
		RowsActual = RowContents.Count();
		CSType = FindType(fi.Name);
	}
	
	private CreditShellType FindType(string fileName)
	{
		CreditShellType retVal = new CreditShellType();
		
		retVal = 
			fileName.Contains(CSHFX.Type)
			? CSHFX
			: fileName.Contains(CSHUR_CF.Type)
			? CSHUR_CF
			: fileName.Contains(CSHUR_CS.Type)
			? CSHUR_CS
			: fileName.Contains(CSHXR.Type)
			? CSHXR
			: fileName.Contains(PMTPM_CF.Type)
			? PMTPM_CF
			: fileName.Contains(PMTPM_CS.Type)
			? PMTPM_CS
			: retVal;


		return retVal;
	}

}