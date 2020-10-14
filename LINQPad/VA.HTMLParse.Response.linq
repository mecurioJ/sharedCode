<Query Kind="Program">
  <NuGetReference>HtmlDexterityPack</NuGetReference>
  <Namespace>HtmlAgilityPack</Namespace>
</Query>

void Main()
{
	var sourceString = "<img src='Resources/info.png' align=\"middle\">5/24/2017 11:00:42 AM - Test started<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Testing SQL Repository Access<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Test Suceeded! Version: 6.41.3482.1937, Data Source: 'vhacdwdwhsql28.vha.med.va.gov' Database: 'PyramidRepository' using:<b>SQL Authenticaiton</b><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Router Server Address: http://vhacdwdwhbir01.vha.med.va.gov:1210/Router.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM -  Router Server Available: <span style='color: green; '>True</span> ~  Id: 05880fa4-329d-429f-8347-a3e3695f2a31 ~  IsActive: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>NT AUTHORITY\\SYSTEM</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Router Server Address: http://vhacdwdwhbir02.vha.med.va.gov:1210/Router.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM -  Router Server Available: <span style='color: green; '>True</span> ~  Id: e6545f86-5e3b-417a-bc2f-f96be893bf22 ~  IsActive: <span style='color: green; '>True</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>NT AUTHORITY\\SYSTEM</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Primary Application Server: 65c3e0ab-db2b-4922-9b6d-0dc4a0f5fb52<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Application Server Address: http://vhacdwdwhbia03.vha.med.va.gov:3535/AppServer.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM -  Application Server Available: <span style='color: green; '>True</span> ~  Id: 65c3e0ab-db2b-4922-9b6d-0dc4a0f5fb52 ~  IsPrimary: <span style='color: green; '>True</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>VHA06\\VHASBYVrbskL</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Sending ping to an Application Server via Router: e2c75408-ef36-45c7-b0c7-71cbccea1cbf<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Test Suceeded!<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Application Server Address: http://vhacdwdwhbia04.vha.med.va.gov:3535/AppServer.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM -  Application Server Available: <span style='color: green; '>True</span> ~  Id: 543707ce-d73f-46a6-b379-70be837e3d48 ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>NT AUTHORITY\\SYSTEM</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Sending ping to an Application Server via Router: 1c3de8ca-9a05-485f-a144-035c4a7eead8<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Test Suceeded!<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Application Server Address: http://vhacdwdwhbia07.vha.med.va.gov:3535/AppServer.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM -  Application Server Available: <span style='color: green; '>True</span> ~  Id: 9c474cd0-8d9e-4b10-adb5-7e77447acf0b ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>NT AUTHORITY\\SYSTEM</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Sending ping to an Application Server via Router: 73ec6f89-1b51-40b6-b59f-866bf914b38e<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Test Suceeded!<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Application Server Address: http://vhacdwdwhbia05.vha.med.va.gov:3535/AppServer.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM -  Application Server Available: <span style='color: green; '>True</span> ~  Id: 5501a4dd-7191-401f-86c8-8f959d152109 ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>VHA23\\VHAHOTCloseS</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Sending ping to an Application Server via Router: 25a08cb0-200a-4f19-bdb2-da142e98f040<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Test Suceeded!<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Application Server Address: http://vhacdwdwhbia06.vha.med.va.gov:3535/AppServer.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM -  Application Server Available: <span style='color: green; '>True</span> ~  Id: 52b1a6dc-e246-41ad-96b4-91b60ba3faec ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>DVA\\vhacolaughj</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Sending ping to an Application Server via Router: 68d4c707-8fb2-43cb-be60-6863ee201ce5<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Test Suceeded!<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Application Server Address: http://vhacdwdwhbia01.vha.med.va.gov:3535/AppServer.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM -  Application Server Available: <span style='color: green; '>True</span> ~  Id: 33303793-f2b3-4f65-8c30-a04a554ecc54 ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>VHA17\\VHASTXNELSOJD</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Sending ping to an Application Server via Router: 5d93877d-612f-4c18-9b65-6976d1e370e4<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Test Suceeded!<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:42 AM - Application Server Address: http://vhacdwdwhbia02.vha.med.va.gov:3535/AppServer.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM -  Application Server Available: <span style='color: green; '>True</span> ~  Id: b354f4b2-cbdd-4bfa-abb9-c808b65439ce ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>VHA07\\VHACHAGlaziC</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Sending ping to an Application Server via Router: 7d8d5ec8-561a-41de-832d-1ef25270e956<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Test Suceeded!<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Application Server Address: http://vhacdwdwhbia08.vha.med.va.gov:3535/AppServer.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM -  Application Server Available: <span style='color: green; '>True</span> ~  Id: 18653674-d336-4315-8d99-d8a44571ebe7 ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>VHA22\\VHALASCROSBC</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Sending ping to an Application Server via Router: cfa4927d-07f9-49bf-b265-f7c887189410<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Test Suceeded!<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Primary Publisher Server: 8a72ddd0-f621-4c3a-8c5c-2d6e52c3a213<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Publisher Address: http://vhacdwdwhbia02.vha.med.va.gov:1406/Publisher.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Publisher Available: <span style='color: green; '>True</span> ~  Id: 8a72ddd0-f621-4c3a-8c5c-2d6e52c3a213 ~  IsPrimary: <span style='color: green; '>True</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>NT AUTHORITY\\SYSTEM</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Publisher Address: http://vhacdwdwhbia01.vha.med.va.gov:1406/Publisher.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Publisher Available: <span style='color: green; '>True</span> ~  Id: 208b1302-aa61-4cf2-b148-4029c23763a9 ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>NT AUTHORITY\\SYSTEM</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Publisher Address: http://vhacdwdwhbia08.vha.med.va.gov:1406/Publisher.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Publisher Available: <span style='color: green; '>True</span> ~  Id: cf9e85e9-2b5d-4eb9-9e1f-77d36952a2c4 ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>NT AUTHORITY\\SYSTEM</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Publisher Address: http://vhacdwdwhbia07.vha.med.va.gov:1406/Publisher.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Publisher Available: <span style='color: green; '>True</span> ~  Id: db4c88ba-9561-46bb-9b9f-79331498ad9e ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>NT AUTHORITY\\SYSTEM</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:43 AM - Publisher Address: http://vhacdwdwhbia03.vha.med.va.gov:1406/Publisher.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:44 AM - Publisher Available: <span style='color: green; '>True</span> ~  Id: b16b49b3-56ba-4c75-8df3-80dd8a59ad64 ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>NT AUTHORITY\\SYSTEM</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:44 AM - Publisher Address: http://vhacdwdwhbia06.vha.med.va.gov:1406/Publisher.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:44 AM - Publisher Available: <span style='color: green; '>True</span> ~  Id: 070795b5-8373-4063-9ab9-85446881579c ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>NT AUTHORITY\\SYSTEM</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:44 AM - Publisher Address: http://vhacdwdwhbia04.vha.med.va.gov:1406/Publisher.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:44 AM - Publisher Available: <span style='color: green; '>True</span> ~  Id: bca2d747-931b-42a3-90ea-8c1602eb8a44 ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>NT AUTHORITY\\SYSTEM</span><hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:44 AM - Publisher Address: http://vhacdwdwhbia05.vha.med.va.gov:1406/Publisher.svc<hr><img src='Resources/good.png' align=\"middle\">5/24/2017 11:00:44 AM - Publisher Available: <span style='color: green; '>True</span> ~  Id: 4675696f-002d-4312-9979-93aaebea2ca8 ~  IsPrimary: <span style='color: brown; '>False</span> ~  Version: <span style='color: blue; '>6.41.3482.1937</span> ~  Account: <span style='color: purple; '>NT AUTHORITY\\SYSTEM</span><hr><img src='Resources/info.png' align=\"middle\">5/24/2017 11:00:44 AM - Test ended";
	String[] lineBreak = new String[]{"<hr>"};
	var indexer = 1;
	
	
	
	
//	sourceString.Split(lineBreak,StringSplitOptions.None).Dump();
	
	var results = sourceString.Split(lineBreak,StringSplitOptions.None).ToList();
	
	var SQLTest = results.Skip(1).Take(2);//.Aggregate((p,n) => String.Format("{0}{1}",p,n));
	var RouterServers = results.Skip(3).Take(4);
	var ApplicationServers = results.Skip(7).Take(33);
	var PublicationServers = results.Skip(40).Take(17);
	
	
	
	SQLTest.Select(st => new PingParse(st)).Dump();
	
	
	RouterServers.Select(st => new PingParse(st)).Dump();
	
	ApplicationServers.Select(st => new PingParse(st)).Dump();
	PublicationServers.Select(st => new PingParse(st)).Dump();
	
	
	
	
	//results.Skip(7).TakeWhile(x => x.index >= 18 && x.index <= 50 ).Dump();
	// 18 to 50
	//The lines are HTML Content, split into testing.
	
//	var itemList = ParseHTML.ParseItems(sourceString);
//	itemList.Select(
//		li => new{
//			li.Indexer,
//			li.PriorIndexer,
//			MessageDetails = li.MessageDetails.Where(cc => li.ServerType.Equals("Pyramid Repository")).FirstOrDefault(), 
//			li.TimeStamp,
//			li.TestResult,
//			li.ServerType,
//			ServerAddress = String.IsNullOrEmpty(li.ServerAddress) ?
//					li.PriorIndexer < 0 
//						? String.Empty 
//						: itemList.Where(i => i.Indexer.Equals(li.PriorIndexer)).SingleOrDefault().ServerAddress
//				: li.ServerAddress,
//			li.Status,
//			ServerId = li.ServerDetails.Where(k => k.Key.Equals("Id")).FirstOrDefault().Value,
//			ServerAvailable = li.ServerDetails.Where(k => k.Key.Contains("Available")).FirstOrDefault().Value,
//			ServerIsActive = li.ServerDetails.Where(k => k.Key.Equals("IsActive")).FirstOrDefault().Value,
//			ServerIsPrimary = li.ServerDetails.Where(k => k.Key.Equals("IsPrimary")).FirstOrDefault().Value,
//			ServerVersion = li.ServerDetails.Where(k => k.Key.Equals("Version")).FirstOrDefault().Value,
//			LastIdentity = li.ServerDetails.Where(k => k.Key.Equals("Account")).FirstOrDefault().Value,
//			RouterUsedForAppServerPing = li.ServerDetails.Where(k => k.Key.Contains("Sending ping to ")).FirstOrDefault().Value,
//			PrimaryApplicationServer = li.ServerDetails.Where(k => k.Key.Equals("Primary Application Server")).FirstOrDefault().Value,
//			li.Message
//		}
//	)
//	//.GroupBy(s => s.ServerType)
//	.Dump()
//	;
	
}



// Define other methods and classes here

//Router Server 
//Application Server
//Publisher Server
//SQL Server

public class PingParse
{
	public String Status { get; set; }
	public DateTime ReportedDateTime { get; set; }
	public String ReportedOutcome { get; set; }
	
	
	public PingParse()
	{
		
	}
	
	public PingParse(string st)
	{
		Status = InterpretStatus(st);
		ReportedDateTime = InterpretDate(st);
		ReportedOutcome = InterpretOutcome(st);
	}
	
	private static String InterpretStatus(string st)
	{
		return st.Contains("good.png")
					? "Good"
					: st.Contains("warning.png")
					 	? "Warning"
						: st.Contains("bad.png")
							? "Bad"
						: st.Contains("info.png")
							? "Info"
							: String.Empty;
	}
	
	private static DateTime InterpretDate(string st)
	{
		return DateTime.Parse(st.Split(
		new[]{
		@"<img src='Resources/blank.png' align=""middle"">",
		@"<img src='Resources/good.png' align=""middle"">",
		@"<img src='Resources/warning.png' align=""middle"">",
		@"<img src='Resources/bad.png' align=""middle"">",
		@"<img src='Resources/info.png' align=""middle"">"
		},StringSplitOptions.None)[1].Split(new[]{" - "},StringSplitOptions.None)[0]);
	}
	
	private static String InterpretOutcome(string st)
	{
		return st.Split(
		new[]{
		@"<img src='Resources/blank.png' align=""middle"">",
		@"<img src='Resources/good.png' align=""middle"">",
		@"<img src='Resources/warning.png' align=""middle"">",
		@"<img src='Resources/bad.png' align=""middle"">",
		@"<img src='Resources/info.png' align=""middle"">"
		},StringSplitOptions.None)[1].Split(new[]{" - "},StringSplitOptions.None)[1];
	}
}

public class ParseHTML
{
	
	private static List<String> _messageDetails = new List<String>();

	public int Indexer {get;set;}
	public int PriorIndexer {get;set;}
	public DateTime TimeStamp {get;set;}
	public String TestResult {get;set;}
	public String Status {get;set;}
	public String ServerType {get;set;}
	public String ServerAddress {get;set;}
	public String StatusMessage {get;set;}
	public String Message {get;set;}
	public String ServerId {get;set;}
	
	public List<String> MessageDetails {get;set;}
	public List<KeyValuePair<String,String>> ServerDetails {get;set;}
	
	protected static String timeStampPattern = "(?<Month>\\d{1,2})/(?<Day>\\d{1,2})/(?<Year>(?:\\d{4}|\\d{2})) (?<Hour>(1[012]|[1-9])):(?<Minute>([0-5][0-9])):(?<Second>([0-5][0-9])) (?<AMPM>(AM|PM))";
	protected static String imgIcon = "(?<=<img\\s+[^>]*?src=(?<q>['\"\"]))(?<url>.+?)(?=\\k<q>)";
	protected static String SendingPing = "Sending ping to\\s.*\\:";
	protected static String TestSucceed = "Test Suceeded";
	protected static String TestFailed = "Test Failed";
	protected static String[] lineBreak = new String[]{"<hr>"};
	protected static String[] appServerAddress = new String[]{"Application Server Address:","Router Server Address:","Publisher Address:"};
	
	
	
	public static IEnumerable<ParseHTML> ParseItems(String sourceString)
	{
		return sourceString.Split(lineBreak,StringSplitOptions.None).Select((li, indexer) => new ParseHTML(li,indexer));
	}
	
	public ParseHTML(String li, int indexer)
	{
		Indexer = indexer;
		PriorIndexer = --indexer;
		TimeStamp = DateTime.Parse(Regex.Match(li,timeStampPattern).Value);
		TestResult = Regex.Match(li,TestSucceed).Success ? "Passed" : Regex.Match(li,TestFailed).Success ? "Failed" : String.Empty;
		StatusMessage = li.Substring(
			(Regex.Match(li,timeStampPattern).Index+Regex.Match(li,timeStampPattern).Length),
			(li.Length - (Regex.Match(li,timeStampPattern).Index+Regex.Match(li,timeStampPattern).Length))
			).Replace(" - ",string.Empty).Trim();
		ServerType = 
			Regex.Match(li,"Router Server").Success ? "Router Server" :
			Regex.Match(li,"Application Server").Success ? "Application Server" :
			Regex.Match(li,"Publisher Server").Success ? "Publisher Server" :
			Regex.Match(li,"Publisher Address").Success ? "Publisher Server" :
			Regex.Match(li,"Publisher Available").Success ? "Publisher Server" :
			Regex.Match(li,"SQL Repository").Success ? "Pyramid Repository" :
			Regex.Match(li,"PyramidRepository").Success ? "Pyramid Repository" :
			"";
		ServerAddress =  li.Split(appServerAddress,StringSplitOptions.None).Count() == 2
			? li.Split(appServerAddress,StringSplitOptions.None)[1]
			: String.Empty;
		Status = 
			Regex.Match(li.Substring(0,Regex.Match(li,timeStampPattern).Index),imgIcon).Value == "Resources/blank.png" ? "Blank" :
			Regex.Match(li.Substring(0,Regex.Match(li,timeStampPattern).Index),imgIcon).Value == "Resources/good.png" ? "Good" :
			Regex.Match(li.Substring(0,Regex.Match(li,timeStampPattern).Index),imgIcon).Value == "Resources/warning.png" ? "Warning" :
			Regex.Match(li.Substring(0,Regex.Match(li,timeStampPattern).Index),imgIcon).Value == "Resources/bad.png" ? "Bad" :
			"Info";
		Message = li;
		
		MessageDetails = StatusMessage.Split('~').ToList().Select(t => Regex.Replace(Regex.Replace(t,"<.*?>",""),"</.*?>","").Trim()).ToList();
		ServerDetails = StatusMessage.Split('~').ToList().Select(t => Regex.Replace(Regex.Replace(t,"<.*?>",""),"</.*?>","").Trim()).ToList()
						.Where(cc => cc.Contains(":") && !cc.Contains("http") && !ServerType.Equals("Pyramid Repository")) 
						.Select(k => new KeyValuePair<String,String>(k.Split(':')[0].Trim(),k.Split(':')[1].Trim())).ToList();
	}
}