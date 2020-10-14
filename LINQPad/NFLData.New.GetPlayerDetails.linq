<Query Kind="Program">
  <Connection>
    <ID>5b90285f-2fed-476e-b27a-efa0d187e91f</ID>
    <Persist>true</Persist>
    <Server>jfinternal</Server>
    <SqlSecurity>true</SqlSecurity>
    <Database>NFLData</Database>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAuzmBG7GJnECmX1Z2peYMpwAAAAACAAAAAAAQZgAAAAEAACAAAABP5NXDqvPRZVK3WwvfhhMrRp41e+BYS4Sv7Sey6Cn3OAAAAAAOgAAAAAIAACAAAABXUcrVTBgtYGp832N/eGaoqF+O+Cc7RWTal1vtsbuc5hAAAADYMqlWycEmA3R7qpCYyT8HQAAAAPTMolL70hR3VFuL6KLeu7Hv30lR1C/XCcWZDAkAbcQrXQK2TFA4HU9S2RImlmsKZYVRYayCY2UwIfPG3nHfyGs=</Password>
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
	foreach (var pp in Players)
	{
		if (String.IsNullOrEmpty(pp.Title))
		{
			HtmlWeb hWeb = new HtmlWeb();
			HtmlDocument hdoc = hWeb.Load(pp.ProfileUrl);
			if (hdoc != null && hdoc.DocumentNode.SelectNodes("//div[@class='player-info']/p") != null)
			{
				var PlayerBio = hdoc.DocumentNode.SelectNodes("//div[@class='player-info']/p")
				.SelectMany(p => p.InnerText
				.Replace("&nbsp;", string.Empty)
				.Trim().Split('\t').Where(x => !string.IsNullOrWhiteSpace(x))).ToArray();

				pp.Title = hdoc.DocumentNode.Descendants("title").FirstOrDefault().InnerText;
				pp.Value1 = PlayerBio.Length >= 1 ? PlayerBio[0] : null;
				pp.Value2 = PlayerBio.Length >= 2 ? PlayerBio[1] : null;
				pp.Value3 = PlayerBio.Length >= 3 ? PlayerBio[2] : null;
				pp.Value4 = PlayerBio.Length >= 4 ? PlayerBio[3] : null;
				pp.Value5 = PlayerBio.Length >= 5 ? PlayerBio[4] : null;
				pp.Value6 = PlayerBio.Length >= 6 ? PlayerBio[5] : null;
				pp.Value7 = PlayerBio.Length >= 7 ? PlayerBio[6] : null;
				pp.Value8 = PlayerBio.Length >= 8 ? PlayerBio[7] : null;
				pp.Value9 = PlayerBio.Length >= 9 ? PlayerBio[8] : null;
				pp.Value10 = PlayerBio.Length >= 10 ? PlayerBio[9] : null;
				pp.Value11 = PlayerBio.Length >= 11 ? PlayerBio[10] : null;
				pp.Value112 = PlayerBio.Length >= 12 ? PlayerBio[11] : null;
				this.SubmitChanges();
			}
		}
	}


}

// Define other methods and classes here
