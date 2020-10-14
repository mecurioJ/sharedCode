<Query Kind="Program">
  <Connection>
    <ID>6fda4b70-e1fd-42f7-944b-42e1ffe82f6b</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>NFLRaw</Database>
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
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	
	List<ScheduleData> seasons = new List<LINQPad.User.ScheduleData>();
	for (int i = 1971; i < 2019; i++)
	{

		seasons.AddRange(GetSchedule("PRE", i));
		seasons.AddRange(GetSchedule("REG", i));
		if (i < 2018)
		{
			seasons.AddRange(GetSchedule("POST", i));
		}
	}
	
	//seasons.Dump();
	//save the data...submit the changes
	ScheduleData.InsertAllOnSubmit(seasons);
	this.SubmitChanges();
	
	
	
}// Define other methods and classes here

public ScheduleData[] GetSchedule(string scheduleType, int season)
{
	HtmlWeb web = new HtmlWeb();
	HtmlDocument hDoc = web.Load(string.Format(@"http://www.nfl.com/schedules/{0}/{1}", season, scheduleType));
	
	var selectNode = hDoc.DocumentNode.SelectNodes("//div[contains(@class,'schedules-list-content')]");
	if (selectNode == null)
	{
		return null;
	}

	return selectNode
		.Select(a => a.Attributes.Where(n => n.Name.Contains("data"))
			.Select(v => new { v.Name, v.Value })
			.ToLookup(l => l.Name, l => l.Value)
		)
		.Select(
			g => new ScheduleData
			{
				GameId = long.Parse(g["data-gameid"].FirstOrDefault()).ToString(),
				AwayAbbreviation = g["data-away-abbr"].FirstOrDefault(),
				HomeAbbreviation = g["data-home-abbr"].FirstOrDefault(),
				AwayMascot = g["data-away-mascot"].FirstOrDefault(),
				HomeMascot = g["data-home-mascot"].FirstOrDefault(),
				GameState = g["data-gamestate"].FirstOrDefault(),
				//GameType = g["data-gametype"].FirstOrDefault() , 
				GameCenterUrl = g["data-gc-url"].FirstOrDefault(),
				LocalTime = g["data-localtime"].FirstOrDefault(),
				ShareId = g["data-shareid"].FirstOrDefault(),
				GameSite = g["data-site"].FirstOrDefault()
			}
		).ToArray();
}