<Query Kind="Program" />

void Main()
{
	var path = $@"D:\Projects\Frontier\oooi\integrator\";


	OOOIFolder = path;

	OOOI_OUT = $@"DEP_OUT";
	OOOI_OFF = $@"DEP_OFF";
	OOOI_ON = $@"ARR_ON";
	OOOI_IN = $@"ARR_IN";
	OOOI_RTN = $@"Returns";

	OOOISource = $@"\Source\";
	OOOIArchive = $@"\Archive\";

	string OOOI_OUT_Path = $"{OOOIFolder}{OOOI_OUT}{OOOIArchive}";
	string OOOI_OFF_Path = $"{OOOIFolder}{OOOI_OFF}{OOOIArchive}";
	string OOOI_ON_Path = $"{OOOIFolder}{OOOI_ON}{OOOIArchive}";
	string OOOI_IN_Path = $"{OOOIFolder}{OOOI_IN}{OOOIArchive}";
	string OOOI_RTN_Path = $"{OOOIFolder}{OOOI_RTN}{OOOIArchive}";

	DirectoryInfo OOOI_OUT_Dir = new DirectoryInfo(OOOI_OUT_Path);
	DirectoryInfo OOOI_OFF_Dir = new DirectoryInfo(OOOI_OFF_Path);
	DirectoryInfo OOOI_ON_Dir = new DirectoryInfo(OOOI_ON_Path);
	DirectoryInfo OOOI_IN_Dir = new DirectoryInfo(OOOI_IN_Path);
	DirectoryInfo OOOI_RTN_Dir = new DirectoryInfo(OOOI_RTN_Path);

	var OOOI_OUT_Files = OOOI_OUT_Dir.GetFiles().Select(fi => new OOOIFile(fi));
	var OOOI_OFF_Files = OOOI_OFF_Dir.GetFiles().Select(fi => new OOOIFile(fi));
	var OOOI_ON_Files = OOOI_ON_Dir.GetFiles().Select(fi => new OOOIFile(fi));
	var OOOI_IN_Files = OOOI_IN_Dir.GetFiles().Select(fi => new OOOIFile(fi));
	var OOOI_RTN_Files = OOOI_RTN_Dir.GetFiles().Select(fi => new OOOIFile(fi));

	var oooiFiles = OOOI_OUT_Files.Union(OOOI_OFF_Files).Union(OOOI_ON_Files).Union(OOOI_IN_Files).Union(OOOI_RTN_Files);


	
	var inboundFiles = oooiFiles.Where(s => s.SMI.Equals("RTN"));
		
	inboundFiles.Dump();
	
}


public static String OOOISource { get; set; }
public static String OOOIFolder { get; set; }
public static String OOOIArchive { get; set; }

public static String OOOI_OUT { get; set; }
public static String OOOI_OFF { get; set; }
public static String OOOI_ON { get; set; }
public static String OOOI_IN { get; set; }
public static String OOOI_RTN { get; set; }

public static class EventType
{
	public static String Parse(string smi, string eventRow)
	{
		string retVal = null;
		switch(smi)
		{
			case "ARR":
				retVal = eventRow.Contains("/ON")
				? "ON"
				: eventRow.Contains("/IN")
					? "IN"
					: null;
				break;
			case "DEP":
				retVal = eventRow.Contains("/OT")
				? "OUT"
				: eventRow.Contains("/OF")
					? "OFF"
					: null;
				break;
			case "RTN":
				retVal = eventRow.Contains("/OT") && eventRow.Contains("/RI")
				? "RTN"
				: null;
				break;
		}
		return retVal;
	}
}

// Define other methods, classes and namespaces here
public class OOOIFile
{
	public OOOIFile()
	{
	}
	public OOOIFile(InboundFile contents)
	{
		APUFileName = contents.FileName;
		MessageBody = contents.FullText;
		FileDate = contents.FileDate;
		SendToAddress = contents.FileContents[0].Split(' ')[1].Trim();
		SentFromAddress = contents.FileContents[1].Split(' ')[0].Trim();
		TimeStamp = contents.FileContents[1].Split(' ')[1].Trim();
		SMI = contents.FileContents[2].Substring(1, 3).Trim();
		ReportTypeDefintion = ParseType(contents.FileContents[2].Substring(1, 3).Trim(), contents.FileContents[3]).Trim();
		FlightIdentification = contents.FileContents[3].Split(' ')[1].Split('/')[0].Trim();
		AircraftIdentification = contents.FileContents[3].Split(' ')[2].Split('/')[0].Trim();
		OriginStation = Regex.Match(contents.FileContents[3].Split(' ')[3].Split('/')[0].Trim(), "[A-Z]{4}").Success
						? contents.FileContents[3].Split(' ')[3].Split('/')[0].Trim()
						: string.Empty;
		DestinationStation = Regex.Match(contents.FileContents[4].Split(' ')[3].Split('/')[0].Trim(), "[A-Z]{4}").Success
						? contents.FileContents[3].Split(' ')[4].Split('/')[0].Trim()
						: string.Empty;
		Time = contents.FileContents[3].Split(' ')[5].Split('/')[0].Trim();
		FuelOnBoard = GetFuelOnBoard(SMI, ReportTypeDefintion, contents.FileContents).Trim();
		DataDownlinkStation = contents.FileContents[4].Split(' ')[2].Trim();
		FileContents = contents.FileContents;
	}


	public OOOIFile(FileInfo theFile) : this(new InboundFile(theFile))
	{

	}

	protected static String ParseType(string smi, string eventRow)
	{
		string retVal = null;
		switch (smi)
		{
			case "ARR":
				retVal = eventRow.Contains("/ON")
				? "ON"
				: eventRow.Contains("/IN")
					? "IN"
					: null;
				break;
			case "DEP":
				retVal = eventRow.Contains("/OT")
				? "OUT"
				: eventRow.Contains("/OF")
					? "OFF"
					: null;
				break;
			case "RTN":
				retVal = eventRow.Contains("/OT") && eventRow.Contains("/RI")
				? "RTN"
				: null;
				break;
		}
		return retVal;
	}

	protected String GetFuelOnBoard(string smi, string messageType, string[] contents)
	{

		var switcher = Tuple.Create(smi, messageType);
		string returnVal = string.Empty;
		int index1 = 0;
		int index2 = 0;

		var OUT = Tuple.Create("DEP", "OUT").Equals(switcher);
		var OFF = Tuple.Create("DEP", "OFF").Equals(switcher);
		var ON = Tuple.Create("ARR", "ON").Equals(switcher);
		var IN = Tuple.Create("ARR", "IN").Equals(switcher);
		var RTN = Tuple.Create("RTN", "RTN").Equals(switcher);

		if (OUT)
		{
			var eval = contents[3];
			index1 = eval.IndexOf("/FB") + 3;
			index2 = eval.IndexOf("/BF");
			returnVal = eval.Substring(index1, index2 - index1).Trim();
		}
		else if (OFF)
		{
			var eval = contents[5].Split(',')[0].Replace("-", string.Empty).Trim();
			returnVal = eval;
		}
		else if (ON)
		{
			var eval = contents[5].Split(',')[0].Replace("-", string.Empty).Trim();
			returnVal = eval;
		}
		else if (IN)
		{
			var eval = contents[3];
			index1 = eval.IndexOf("/FB") + 3;
			index2 = eval.IndexOf("/LA");
			returnVal = eval.Substring(index1, index2 - index1).Trim();
		}
		else if (RTN)
		{
			var eval = contents[3].Split(' ').Last();
			returnVal = eval;
		}

		return returnVal;
	}

	protected String GetMessageType(string contents)
	{
		String returnVal = string.Empty;

		if (contents.Contains(MessageType.OUT.MatchValue))
		{
			returnVal = MessageType.OUT.ReturnValue;
		}
		else if (contents.Contains(MessageType.OFF.MatchValue))
		{
			returnVal = MessageType.OFF.ReturnValue;
		}
		else if (contents.Contains(MessageType.ON.MatchValue))
		{
			returnVal = MessageType.ON.ReturnValue;
		}
		else if (contents.Contains(MessageType.IN.MatchValue))
		{
			returnVal = MessageType.IN.ReturnValue;
		}
		else if (contents.Contains(MessageType.RTN.MatchValue))
		{
			returnVal = MessageType.RTN.ReturnValue;
		}

		return returnVal;
	}

	protected sealed class MessageType
	{
		public String MatchValue { get; set; }
		public String ReturnValue { get; set; }

		public static MessageType OUT = new MessageType() { MatchValue = "/OT", ReturnValue = "OUT" };
		public static MessageType OFF = new MessageType() { MatchValue = "/OF", ReturnValue = "OFF" };
		public static MessageType ON = new MessageType() { MatchValue = "/ON", ReturnValue = "ON" };
		public static MessageType IN = new MessageType() { MatchValue = "/IN", ReturnValue = "IN" };
		public static MessageType RTN = new MessageType() { MatchValue = "/RI", ReturnValue = "RTN" };

	}

	public String APUFileName { get; set; }
	public DateTime FileDate { get; set; }
	public String SendToAddress { get; set; }
	public String SentFromAddress { get; set; }
	public String TimeStamp { get; set; }
	public String SMI { get; set; }
	public String ReportTypeDefintion { get; set; }
	public String FlightIdentification { get; set; }
	public String AircraftIdentification { get; set; }
	public String OriginStation { get; set; }
	public String DestinationStation { get; set; }
	public String Time { get; set; }
	public String FuelOnBoard { get; set; }
	public String DataDownlinkStation { get; set; }
	public String MessageBody { get; set; }
	public String[] FileContents { get; set; }
}

public class InboundFile
{
	public String FileName { get; set; }
	public DateTime FileDate { get; set; }
	public String FullName { get; set; }
	public String FullText { get; set; }
	public String[] FileContents { get; set; }

	public InboundFile() { }
	public InboundFile(FileInfo theFile)
	{
		FileName = theFile.Name;
		FileDate = theFile.LastWriteTime;
		FullName = theFile.FullName;
		FullText = File.ReadAllText(theFile.FullName).TrimStart();
		FileContents = File.ReadAllLines(theFile.FullName).Skip(1).ToArray();
	}
}