<Query Kind="Program" />

void Main()
{
	var directoryPath = @"D:\Projects\Frontier\APU\Data\";
	
	DateTime dt;
	
	
	new DirectoryInfo(directoryPath).GetFiles().Select(f => new{
		theName = f.FullName,
		contents = File.ReadAllLines(f.FullName).First().ToString()	
	}).Where(t => t.contents.EndsWith("4U2020")).Dump();
	
	var Source = new DirectoryInfo(directoryPath).GetFiles().Select(f => File.ReadAllLines(f.FullName).First().Split(','));		
	var Parse1 = Source.Select(
		apu => new {
			Carrier = apu[0],
			FlightNumber = apu[1],
			TailNumber = apu[2],
			MessageReceiptStation = apu[3],
			MessageReceiptTimestamp = apu[4],
			AircraftType = apu[5],
			StopTime = apu[6],
			OriginStation = apu[7],
			DestinationStation = apu[8],
			APUStartTime = apu[9],
			APUStopTime = apu[10],
			FileDate = apu[11].ToString().Trim()
		});
				
		Parse1.Where(fd => fd.FileDate.Length < 8).Dump();

		//,
		//	day = apu[11].ToString().Substring(0, 2),
		//	month = apu[11].ToString().Substring(2, 2),
		//	year = apu[11].ToString().Substring(4, 4)
		//.Where(dd => !DateTime.TryParse(string.Format("{0}-{1}-{2}"
		//	, dd.month
		//	, dd.day
		//	, dd.year
		//)
		//, out dt))
		
	
	
	
}

// Define other methods, classes and namespaces here