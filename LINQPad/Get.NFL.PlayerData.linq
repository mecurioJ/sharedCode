<Query Kind="Program">
  <Connection>
    <ID>6fda4b70-e1fd-42f7-944b-42e1ffe82f6b</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>DraftKit</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference Relative="Libs\spire.xls_7.10.79\NET4.0\Spire.Common.dll">&lt;MyDocuments&gt;\OneDrive\LinqPad\Libs\spire.xls_7.10.79\NET4.0\Spire.Common.dll</Reference>
  <Reference Relative="Libs\spire.xls_7.10.79\NET4.0\Spire.License.dll">&lt;MyDocuments&gt;\OneDrive\LinqPad\Libs\spire.xls_7.10.79\NET4.0\Spire.License.dll</Reference>
  <Reference Relative="Libs\spire.xls_7.10.79\NET4.0\Spire.Pdf.dll">&lt;MyDocuments&gt;\OneDrive\LinqPad\Libs\spire.xls_7.10.79\NET4.0\Spire.Pdf.dll</Reference>
  <Reference Relative="Libs\spire.xls_7.10.79\NET4.0\Spire.XLS.dll">&lt;MyDocuments&gt;\OneDrive\LinqPad\Libs\spire.xls_7.10.79\NET4.0\Spire.XLS.dll</Reference>
  <NuGetReference Version="1.4.9.5">HtmlAgilityPack</NuGetReference>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>Spire.CompoundFile.XLS</Namespace>
  <Namespace>Spire.CompoundFile.XLS.Net</Namespace>
  <Namespace>Spire.Xls</Namespace>
  <Namespace>Spire.Xls.Calculation</Namespace>
  <Namespace>Spire.Xls.Charts</Namespace>
  <Namespace>Spire.Xls.Collections</Namespace>
  <Namespace>Spire.Xls.Core</Namespace>
  <Namespace>Spire.Xls.Core.Converter.Exporting.EMF</Namespace>
  <Namespace>Spire.Xls.Core.Converter.Spreadsheet.ExcelStyle</Namespace>
  <Namespace>Spire.Xls.Core.Converter.Spreadsheet.PivotTable</Namespace>
  <Namespace>Spire.Xls.Core.Interface</Namespace>
  <Namespace>Spire.Xls.Core.Interfaces</Namespace>
  <Namespace>Spire.Xls.Core.Parser.Biff_Records</Namespace>
  <Namespace>Spire.Xls.Core.Parser.Biff_Records.Charts</Namespace>
  <Namespace>Spire.Xls.Core.Parser.Biff_Records.Formula</Namespace>
  <Namespace>Spire.Xls.Core.Parser.Biff_Records.MsoDrawing</Namespace>
  <Namespace>Spire.Xls.Core.Parser.Biff_Records.ObjRecords</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Charts</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Collections</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Parser.Biff_Records.Charts</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.PivotTables</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Security</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Shapes</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Sorting</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Tables</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.TemplateMarkers</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.XmlReaders</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.XmlSerialization</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.XmlSerialization.Charts</Namespace>
  <Namespace>Spire.Xls.License</Namespace>
</Query>

void Main()
{
	HtmlWeb web = new HtmlWeb();	
	List<PlayerData> pdList = new List<PlayerData>();
	
	
	foreach (var player in Players.ToArray()
	//.Where(pd => String.IsNullOrEmpty(pd.ImageUrl))
	.Where(p => !p.Position.Equals("DST"))
	.Select(pd => new{
		PlayerName = pd.Player.Replace(" ","").ToLower()
		,pd.PlayerId
		, ProfilePage = pd.ProfileUrl
	}))
	{
		var hDoc = web.Load(player.ProfilePage);
		String ImageUrl = String.Empty;
		String PlayerInfo = String.Empty;
		var split = new []{"&nbsp;"};		
		//------------------------------------//
		DateTime BirthDate;
		String PageTitle;
		String BirthCity;
		String Height;
		String Weight;
		String School;
		String Seasons;
		
		List<HtmlNode> Content = new List<HtmlNode>();
		
		try
		{	        
			
			if (hDoc.DocumentNode.Descendants("div")
			.Where(ttt => ttt.Attributes.Contains("class") &&  ttt.Attributes["class"].Value.Equals("player-info")).Any ())
			{
		
			Content = 	hDoc.DocumentNode.Descendants("div")
			.Where(ttt => ttt.Attributes.Contains("class") &&  ttt.Attributes["class"].Value.Equals("player-info")).First()
			.Descendants("p").ToList();
				
			var playerData = new PlayerData(){
				PageTitle = hDoc.DocumentNode.Descendants("Title").First().InnerText,				
				ImageUrl = hDoc.DocumentNode.Descendants("div")
								.Where(ttt => ttt.Attributes.Contains("class") &&  ttt.Attributes["class"].Value.Equals("player-photo"))
								.First().Descendants("img")
								.First().Attributes["src"].Value,				
				
				BirthDate = DateTime.Parse(
				Content
				.Where(pt => Regex.Match(pt.InnerText.Trim(),@"\d{0,2}\/\d{0,2}\/\d{0,4}").Success)
				.Select(pt => Regex.Match(pt.InnerText.Trim(),@"\d{0,2}\/\d{0,2}\/\d{0,4}").Value)
				.Distinct().SingleOrDefault()),				
				BirthCity = Regex.Replace(Content.Where(pt =>pt.InnerText.Trim().Contains("Born:")).SingleOrDefault().InnerText,@"(Born: \d{0,2}\/\d{0,2}\/\d{0,4})?",String.Empty).Trim(),
				Height = Regex.Match(Content.Where(pt =>pt.InnerText.Trim().Contains("Height:")).SingleOrDefault().InnerText,@"\d-\d{1,2}").Value.Trim(),
				Weight = Regex.Match(Content.Where(pt =>pt.InnerText.Trim().Contains("Weight:")).SingleOrDefault().InnerText,@"\d{1,3}").Value.Trim(),
				School = Content.Where(pt =>pt.InnerText.Trim().Contains("College:")).SingleOrDefault().InnerText.Replace("College:",String.Empty).Trim(),
				Seasons = Regex.Match(Content.Where(pt =>pt.InnerText.Trim().Contains("Experience:")).SingleOrDefault().InnerText,@"\d{1,3}").Value.Trim(),
			};
			pdList.Add(playerData);
			playerData.Dump();
			}
			
			
		
			
//							hDoc.DocumentNode.Descendants("div")
//							.Where(ttt => ttt.Attributes.Contains("class") &&  ttt.Attributes["class"].Value.Equals("player-info")).First().Descendants("p")
//							.Select(
//								pt => pt.InnerText.Trim().Split(split,StringSplitOptions.None).Where(tx => tx.Contains("Experience:")).Any()
//										? Regex.Match(pt.InnerText.Trim().Split(split,StringSplitOptions.None).Where(tx => tx.Contains("Experience:")).First(),@"\d{1,3}").Value
//										: String.Empty)
//							.Dump();
			
			
		}
		catch (Exception ex)
		{
			player.ProfilePage.Dump();
			Content.Select(c => c.InnerText).Dump();
			throw;
		}
//		String.Format("SELECT '{0}','{1}' UNION",
//			player.PlayerId,
//			ImageUrl,
//			player.ProfilePage
//			).Dump();

	};
	
//	Players
//		.Where(ply => !ply.ProfileID.Equals(0))
//		.ToArray()
//		.Join(
//			PlayerImageUrls.ToList().Where(pi => Regex.Match(pi.ImageUrl,@"^http://www\.nfl\.com/player/*/*/").Success),
//			t1 => t1.ProfileID,
//			t2 => t2.Profile_id,
//			(t1,t2) => new {t1.FirstName, t1.LastName, t1.ProfileID, t1.Player_id}
//		).Dump();

	
//	
//	foreach (var player in Players
//		.Where(ply => !ply.ProfileID.Equals(0))
//		.ToArray()
//		.Join(
//			PlayerImageUrls.ToList().Where(pi => Regex.Match(pi.ImageUrl,@"^http://www\.nfl\.com/player/*/*/").Success),
//			t1 => t1.ProfileID,
//			t2 => t2.Profile_id,
//			(t1,t2) => new {t1.FirstName, t1.LastName, t1.ProfileID, t1.Player_id}
//		))	
//	{
//	
//		var profileUrl = 
//		string.Format("http://www.nfl.com/player/{0}{1}/{2}/profile",
//			player.FirstName.ToLower().Replace(".","").Replace("'",""),
//			player.LastName.ToLower().Replace(".","").Replace("'",""),
//			player.ProfileID
//			);
//		
//		var hDoc = web.Load(profileUrl);
//	    var PlayerPhoto = hDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[3]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]");
//		hDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[3]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/img[1]").Attributes["src"].Value.Dump();
//		
//		string ImageUrl = String.Empty;
//		
//		try
//		{	        
//		
//			ImageUrl = hDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[3]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/img[1]").Attributes["src"].Value;
//		}
//		catch (Exception ex)
//		{
//			ImageUrl = profileUrl;
//			throw;
//		}
//		String.Format("UPDATE Util.PlayerImageUrl SET ImageUrl = '{1}' WHERE profile_id = '{0}'",
//			player.ProfileID,
//			ImageUrl
//			).Dump();	
//			
//	}



//			var position = Regex.Split(hDoc.DocumentNode.SelectNodes("/html[1]/head[1]/title[1]").SingleOrDefault().InnerText.Split(',')[1].Substring(1,5),@"\s")[0];
//			String.Format("UPDATE nfldb.player SET nfldb.player.position = '{0}' WHERE nfldb.player.player_id = '{1}'",position,player.Player_id).Dump();

//	foreach(Players pp in Players.Where(ppk => ppk.ProfileHtmlContent == null).Where(ppk => !ppk.PlayPlayersKey.Equals("XX-0000001")))
//	{
//		//load the HTML Content
//		var hDoc = web.Load(pp.ProfilePage);
//		
//		pp.ProfileHtmlContent = hDoc.DocumentNode.OuterHtml;
//		
//		SubmitChanges(ConflictMode.ContinueOnConflict);
//	}
	
//	HtmlWeb web = new HtmlWeb();
//	var hDoc = web.Load("http://www.nfl.com/players/profile?id=00-0019310");
//	
//	hDoc.DocumentNode.OuterHtml.Dump();
//	
//	
//	var Name = hDoc.DocumentNode.SelectNodes("/html[1]/head[1]/meta[9]/@content[1]").SingleOrDefault().Attributes["content"].Value;
//	var Id = hDoc.DocumentNode.SelectNodes("/html[1]/head[1]/meta[10]/@content[1]").SingleOrDefault().Attributes["content"].Value;
//	var ProfileDiv = hDoc.DocumentNode.SelectSingleNode("html[1]/body[1]/div[2]/div[3]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/@id[1]");
//	
//	var PlayerData = hDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[3]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/p[1]").InnerText.Trim();
//	var TeamNumber = hDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[3]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/p[2]").InnerText.Trim().Split(':');
//	var Vitals = hDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[3]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/p[3]").InnerText.Trim().Replace("&nbsp;",":").Split(':');
//	var Birthdate = hDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[3]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/p[4]").InnerText.Trim().Split(':').Last();
//	var College = hDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[3]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/p[5]").InnerText.Trim().Split(':').Last();
//	var Experience = hDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[3]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/p[6]").InnerText.Trim().Split(':').Last();
//	var HighSchool = hDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[3]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/p[7]").InnerText.Trim().Split(':').Last();
//	
//	PlayerData.Dump();
//	TeamNumber.Dump();
//	Vitals.Dump();
//	Birthdate.Dump();
//	College.Dump();
//	Experience.Dump();
//	HighSchool.Dump();

	/*
	for (int i = 1971; i < 2017; i++)
	{

	HtmlWeb web = new HtmlWeb();
//	HtmlDocument hDoc = web.Load(string.Format(@"http://www.nfl.com/schedules/{0}/REG",i));
//	HtmlDocument hDoc = web.Load(string.Format(@"http://www.nfl.com/schedules/{0}/PRE",i));
	HtmlDocument hDoc = web.Load(string.Format(@"http://www.nfl.com/schedules/{0}/POST",i));
	var schedules = 
	hDoc.DocumentNode.SelectNodes("//div[contains(@class,'schedules-list-content')]")
		.Select(a => a.Attributes.Where(n => n.Name.Contains("data"))
			.Select(v => new {v.Name,v.Value})
			.ToLookup(l => l.Name, l => l.Value)
		)
		.Select(
			g => new ScheduleData {
				GameId = long.Parse(g["data-gameid"].FirstOrDefault()).ToString(),
				AwayAbbreviation = g["data-away-abbr"].FirstOrDefault(),
				HomeAbbreviation = g["data-home-abbr"].FirstOrDefault(),
				AwayMascot = g["data-away-mascot"].FirstOrDefault(),
				HomeMascot = g["data-home-mascot"].FirstOrDefault(), 
				GameState = g["data-gamestate"].FirstOrDefault(),
				//GameType = g["data-gametype"].FirstOrDefault() , 
				GameCenterUrl = g["data-gc-url"].FirstOrDefault(),
				LocalTime= g["data-localtime"].FirstOrDefault(), 
				ShareId = g["data-shareid"].FirstOrDefault(),
				GameSite= g["data-site"].FirstOrDefault(),
				ScheduleType_sk = 3
			}
		).ToArray();
		//schedules.Dump();
		
//		ScheduleData.InsertAllOnSubmit(schedules);
//		SubmitChanges();
		
	}
	*/

	/*
	data-gameid 2016102700 
data-away-abbr JAX 
data-home-abbr TEN 
data-away-mascot Jaguars 
data-home-mascot Titans 
data-gamestate PRE 
data-gametype REG 
data-gc-url http://www.nfl.com/gamecenter/2016102700/2016/REG8/jaguars@titans 
data-localtime 19:25:00 
data-shareid sb-42lp6270 
data-site Nissan Stadium 

*/
	
	
//	
//	var tBodies = new List<HtmlNode>();
//	
//	foreach(var tbody in hDoc.DocumentNode.SelectNodes("//table"))
//	{
//		var thisNode = tbody.SelectSingleNode("tbody");
//		
//		if(thisNode.InnerHtml.Contains("Data Element"))
//		{			
//			tBodies.Add(thisNode);
//		}
//	}
//	
//	
//	var tables = tBodies.Select(
//		tb => tb
//			.SelectNodes("tr")
//			.Select(tr => tr.SelectNodes("th|td"))
//			.Select(cell => cell.Select(cc => new TableCell { Name = cc.Name, Text = cc.InnerText } )
//			.Where(cc => cc.Name.Equals("td"))
//			)
//	);
//	
//	var dataObjects = new List<List<FieldMatrix>>();
//	
//	for (int t = 0; t < tables.Count(); t++)
//	{
//		var currTable = tables.ElementAt(t);
//		var dataTable = new List<FieldMatrix>();
//
//		for (int row = 0; row < currTable.Count(); row++)
//		{
//			var currRow = currTable.ElementAt(row);
//			if(currRow.Count() > 3)
//			{
//				var fm = new FieldMatrix()
//				{
//					DataElement = currRow.ElementAt(0).Text,
//					Description = currRow.ElementAt(1).Text,
//					Optional = currRow.ElementAt(2).Text,
//					CampusDatabaseField = currRow.ElementAt(3).Text,
//					CampusGUILocation = currRow.ElementAt(4).Text.Replace("&gt;",">")
//				};
//				dataTable.Add(fm);
//			}
//			
//		}
//		dataObjects.Add(dataTable);
//		
//	}
//	
//	
//#region CellList
//
//	var cellList = new []{
//	new KeyValuePair<int,String>(0,"A"),
//	new KeyValuePair<int,String>(1,"B"),
//	new KeyValuePair<int,String>(2,"C"),
//	new KeyValuePair<int,String>(3,"D"),
//	new KeyValuePair<int,String>(4,"E"),
//	new KeyValuePair<int,String>(5,"F"),
//	new KeyValuePair<int,String>(6,"G"),
//	new KeyValuePair<int,String>(7,"H"),
//	new KeyValuePair<int,String>(8,"I"),
//	new KeyValuePair<int,String>(9,"J"),
//	new KeyValuePair<int,String>(10,"K"),
//	new KeyValuePair<int,String>(11,"L"),
//	new KeyValuePair<int,String>(12,"M"),
//	new KeyValuePair<int,String>(13,"N"),
//	new KeyValuePair<int,String>(14,"O"),
//	new KeyValuePair<int,String>(15,"P"),
//	new KeyValuePair<int,String>(16,"Q"),
//	new KeyValuePair<int,String>(17,"R"),
//	new KeyValuePair<int,String>(18,"S"),
//	new KeyValuePair<int,String>(19,"T"),
//	new KeyValuePair<int,String>(20,"U"),
//	new KeyValuePair<int,String>(21,"V"),
//	new KeyValuePair<int,String>(22,"W"),
//	new KeyValuePair<int,String>(23,"X"),
//	new KeyValuePair<int,String>(24,"Y"),
//	new KeyValuePair<int,String>(25,"Z"),
//	new KeyValuePair<int,String>(26,"AA"),
//	new KeyValuePair<int,String>(27,"AB"),
//	new KeyValuePair<int,String>(28,"AC"),
//	new KeyValuePair<int,String>(29,"AD"),
//	new KeyValuePair<int,String>(30,"AE"),
//	new KeyValuePair<int,String>(31,"AF"),
//	new KeyValuePair<int,String>(32,"AG"),
//	new KeyValuePair<int,String>(33,"AH"),
//	new KeyValuePair<int,String>(34,"AI"),
//	new KeyValuePair<int,String>(35,"AJ"),
//	new KeyValuePair<int,String>(36,"AK"),
//	new KeyValuePair<int,String>(37,"AL"),
//	new KeyValuePair<int,String>(38,"AM"),
//	new KeyValuePair<int,String>(39,"AN"),
//	new KeyValuePair<int,String>(40,"AO"),
//	new KeyValuePair<int,String>(41,"AP"),
//	new KeyValuePair<int,String>(42,"AQ"),
//	new KeyValuePair<int,String>(43,"AR"),
//	new KeyValuePair<int,String>(44,"AS"),
//	new KeyValuePair<int,String>(45,"AT"),
//	new KeyValuePair<int,String>(46,"AU"),
//	new KeyValuePair<int,String>(47,"AV"),
//	new KeyValuePair<int,String>(48,"AW"),
//	new KeyValuePair<int,String>(49,"AX"),
//	new KeyValuePair<int,String>(50,"AY"),
//	new KeyValuePair<int,String>(51,"AZ"),
//	new KeyValuePair<int,String>(52,"BA"),
//	new KeyValuePair<int,String>(53,"BB"),
//	new KeyValuePair<int,String>(54,"BC"),
//	new KeyValuePair<int,String>(55,"BD"),
//	new KeyValuePair<int,String>(56,"BE"),
//	new KeyValuePair<int,String>(57,"BF"),
//	new KeyValuePair<int,String>(58,"BG"),
//	new KeyValuePair<int,String>(59,"BH"),
//	new KeyValuePair<int,String>(60,"BI"),
//	new KeyValuePair<int,String>(61,"BJ"),
//	new KeyValuePair<int,String>(62,"BK"),
//	new KeyValuePair<int,String>(63,"BL"),
//	new KeyValuePair<int,String>(64,"BM"),
//	new KeyValuePair<int,String>(65,"BN"),
//	new KeyValuePair<int,String>(66,"BO"),
//	new KeyValuePair<int,String>(67,"BP"),
//	new KeyValuePair<int,String>(68,"BQ"),
//	new KeyValuePair<int,String>(69,"BR"),
//	new KeyValuePair<int,String>(60,"BS"),
//	new KeyValuePair<int,String>(71,"BT"),
//	new KeyValuePair<int,String>(72,"BU"),
//	new KeyValuePair<int,String>(73,"BV"),
//	new KeyValuePair<int,String>(74,"BW"),
//	new KeyValuePair<int,String>(75,"BX"),
//	new KeyValuePair<int,String>(76,"BY"),
//	new KeyValuePair<int,String>(77,"BZ"),
//	new KeyValuePair<int,String>(78,"CA"),
//	new KeyValuePair<int,String>(79,"CB"),
//	new KeyValuePair<int,String>(80,"CC"),
//	new KeyValuePair<int,String>(81,"CD"),
//	new KeyValuePair<int,String>(82,"CE"),
//	new KeyValuePair<int,String>(83,"CF"),
//	new KeyValuePair<int,String>(84,"CG"),
//	new KeyValuePair<int,String>(85,"CH"),
//	new KeyValuePair<int,String>(86,"CI"),
//	new KeyValuePair<int,String>(87,"CJ"),
//	new KeyValuePair<int,String>(88,"CK"),
//	new KeyValuePair<int,String>(89,"CL"),
//	new KeyValuePair<int,String>(90,"CM"),
//	new KeyValuePair<int,String>(91,"CN"),
//	new KeyValuePair<int,String>(92,"CO"),
//	new KeyValuePair<int,String>(93,"CP"),
//	new KeyValuePair<int,String>(94,"CQ"),
//	new KeyValuePair<int,String>(95,"CR"),
//	new KeyValuePair<int,String>(96,"CS"),
//	new KeyValuePair<int,String>(97,"CT"),
//	new KeyValuePair<int,String>(98,"CU"),
//	new KeyValuePair<int,String>(99,"CV"),
//	new KeyValuePair<int,String>(100,"CW"),
//	new KeyValuePair<int,String>(101,"CX"),
//	new KeyValuePair<int,String>(102,"CY"),
//	new KeyValuePair<int,String>(103,"CZ")
//	};
//#endregion
//	
//	Workbook wb = new Workbook();
//	wb.LoadFromFile(@"D:\BrightView Analytics\Data Import Templates\TestRun.xlsx");
//	
//	for (int sheet = 0; sheet < dataObjects.Count(); sheet++)
//	{
//		//get each item in the list...
//		var workSheet = dataObjects.ElementAt(sheet);
//		var sht = wb.Worksheets.Add(string.Format("Sheet {0}", sheet));
//		
//		if(dataObjects.ElementAt(sheet).Count() > 0)
//		{
//		//workSheet.Select(t => t.DataElement).Aggregate((p,n) => String.Format("{0},{1}",p,n)).Replace("Â",String.Empty).Dump();
//		
//		//get each field Matrix Item in the list
//		for (int field = 0; field < workSheet.Count(); field++)
//		{
//			field.Dump();
//			var cellName = cellList[field].Value;
//			var cell = sht.Range[String.Format("{0}1",cellName)];
//			var cellContent = workSheet.ElementAt(field);
//			cell.Text = cellContent.DataElement.Replace("Â",String.Empty);
//			cell.Comment.Text = cellContent.Description.Replace("Â",String.Empty);	        
//			cell.Comment.AutoSize = true;
//			
//			//var current = workSheet.ElementAt(field).
//		}
//		}
//	}
//	
//	wb.SaveToFile(@"D:\BrightView Analytics\TempWork\ICMappingSheets.v1.xlsx");
	
	
	//dataObjects.Count().Dump();
	
}// Define other methods and classes here

public class PlayerData
{
	public DateTime BirthDate {get;set;}
	public String ImageUrl {get;set;}
	public String PageTitle {get;set;}
	public String BirthCity {get;set;}
	public String Height {get;set;}
	public String Weight {get;set;}
	public String School {get;set;}
	public String Seasons {get;set;}
}

public class TableCell 
{
	public String Name {get;set;}
	public String Text {get;set;}
}

public class FieldMatrix
{
	public String DataElement {get;set;}
	public String Description {get;set;}
	public String Optional {get;set;}
	public String CampusDatabaseField {get;set;}
	public String CampusGUILocation {get;set;}
}