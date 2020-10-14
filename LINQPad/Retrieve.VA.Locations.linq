<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>TopSoft.ExcelExport</NuGetReference>
  <Namespace>DocumentFormat.OpenXml</Namespace>
  <Namespace>DocumentFormat.OpenXml.AdditionalCharacteristics</Namespace>
  <Namespace>DocumentFormat.OpenXml.Bibliography</Namespace>
  <Namespace>DocumentFormat.OpenXml.CustomProperties</Namespace>
  <Namespace>DocumentFormat.OpenXml.CustomXmlDataProperties</Namespace>
  <Namespace>DocumentFormat.OpenXml.CustomXmlSchemaReferences</Namespace>
  <Namespace>DocumentFormat.OpenXml.Drawing</Namespace>
  <Namespace>DocumentFormat.OpenXml.Drawing.ChartDrawing</Namespace>
  <Namespace>DocumentFormat.OpenXml.Drawing.Charts</Namespace>
  <Namespace>DocumentFormat.OpenXml.Drawing.Diagrams</Namespace>
  <Namespace>DocumentFormat.OpenXml.Drawing.LegacyCompatibility</Namespace>
  <Namespace>DocumentFormat.OpenXml.Drawing.LockedCanvas</Namespace>
  <Namespace>DocumentFormat.OpenXml.Drawing.Pictures</Namespace>
  <Namespace>DocumentFormat.OpenXml.Drawing.Spreadsheet</Namespace>
  <Namespace>DocumentFormat.OpenXml.Drawing.Wordprocessing</Namespace>
  <Namespace>DocumentFormat.OpenXml.EMMA</Namespace>
  <Namespace>DocumentFormat.OpenXml.ExtendedProperties</Namespace>
  <Namespace>DocumentFormat.OpenXml.InkML</Namespace>
  <Namespace>DocumentFormat.OpenXml.Math</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office.ActiveX</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office.ContentType</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office.CoverPageProps</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office.CustomDocumentInformationPanel</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office.CustomUI</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office.CustomXsn</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office.Drawing</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office.Excel</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office.LongProperties</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office.MetaAttributes</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office.Word</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.CustomUI</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Drawing</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Drawing.Charts</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Drawing.Diagram</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Drawing.LegacyCompatibility</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Drawing.Pictures</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Drawing.Slicer</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Excel</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Excel.Drawing</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.ExcelAc</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Ink</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.PowerPoint</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Word</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Word.Drawing</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Word.DrawingCanvas</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Word.DrawingGroup</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2010.Word.DrawingShape</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.Drawing</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.Drawing.Chart</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.Drawing.TimeSlicer</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.Excel</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.ExcelAc</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.PowerPoint</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.Theme</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.WebExtension</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.WebExtentionPane</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.Word</Namespace>
  <Namespace>DocumentFormat.OpenXml.Office2013.Word.Drawing</Namespace>
  <Namespace>DocumentFormat.OpenXml.Packaging</Namespace>
  <Namespace>DocumentFormat.OpenXml.Presentation</Namespace>
  <Namespace>DocumentFormat.OpenXml.Spreadsheet</Namespace>
  <Namespace>DocumentFormat.OpenXml.Validation</Namespace>
  <Namespace>DocumentFormat.OpenXml.VariantTypes</Namespace>
  <Namespace>DocumentFormat.OpenXml.Vml</Namespace>
  <Namespace>DocumentFormat.OpenXml.Vml.Office</Namespace>
  <Namespace>DocumentFormat.OpenXml.Vml.Presentation</Namespace>
  <Namespace>DocumentFormat.OpenXml.Vml.Spreadsheet</Namespace>
  <Namespace>DocumentFormat.OpenXml.Vml.Wordprocessing</Namespace>
  <Namespace>DocumentFormat.OpenXml.Wordprocessing</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>TopSoft.ExcelExport</Namespace>
  <Namespace>TopSoft.ExcelExport.Attributes</Namespace>
  <Namespace>TopSoft.ExcelExport.Entity</Namespace>
  <Namespace>TopSoft.ExcelExport.Styles</Namespace>
</Query>

void Main()
{	
	String eX;
	XElement results; 
//	String StateName = "FL";
	List<TMSDomain> TmsDomains = new List<TMSDomain>();
	
	foreach (var StateName in States)
	{
		MakeRequests(out eX, StateName);
		
		results = XElement.Parse(eX);
		XNamespace nsi = results.Name.Namespace;
		
		TmsDomains.AddRange(results.Element("results").Elements("tr").Where(c => c.Elements().Count() > 3)
					.Select(c => c.Elements("td").Select(v => v.Value.Trim()).ToArray())
					.Select(a => new TMSDomain(a))
					);
	}
	
	StreamWriter sr = new StreamWriter(@"d:\LocationSelect.sql");
	sr.WriteLine("STA6AID\tName\tAddress\tPhone");
	foreach( var ln in TmsDomains.Select(t => string.Format("SELECT '{0}','{1}','{2}','{3}' UNION \r\n",
		t.StationID,
		t.Name,
		t.Address
			.Replace("\r\n"," ")
			.Replace("\t",String.Empty)
			.Replace("                       "," ")
			,
		t.Phone)))
	{
	sr.WriteLine(ln);
	}
	
	sr.Close();
	
}


private string[] States
{
	get {
		return new []{"VI",
"AS",
"VT",
"RI",
"RI",
"CT",
"CT",
"NJ",
"NJ",
"DE",
"DE",
"MA",
"MA",
"AK",
"HI",
"PI",
"GU",
"PR",
"ME",
"NH",
"VT",
"NY",
"MI",
"PA",
"WV",
"OH",
"IN",
"KY",
"VA",
"MD",
"MD",
"DC",
"NC",
"SC",
"GA",
"FL",
"WI",
"IL",
"TN",
"AL",
"MS",
"LA",
"AR",
"MO",
"MN",
"WA",
"ID",
"OR",
"CA",
"NV",
"AZ",
"TX",
"OK",
"IA",
"NE",
"KS",
"UT",
"NM",
"CO",
"SD",
"ND",
"WY",
"MT"};
	
	}
}

public class TMSDomain : ExcelRow
{
	[CellData("A")]
	public String StationID {get;set;}
	[CellData("B")]
	public String Name {get;set;}
	[CellData("C")]
	public String Address {get;set;}
	[CellData("D")]
	public String Phone {get;set;}
	
	public TMSDomain(){}
	public TMSDomain(String[] a)
	{
		StationID = a[0].ToString();
		Name = a[1].ToString();
		Address = a[2].ToString();
		Phone = a[3].ToString();
	}
}

// Define other methods and classes here

// Define other methods and classes here
private void MakeRequests(out String xElm, string StateName)
{
	xElm = String.Empty;
	HttpWebResponse response;

	if (Request_query_yahooapis_com(out response, StateName))
	{
		
			xElm = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
	
		response.Close();
	}
}

private bool Request_query_yahooapis_com(out HttpWebResponse response, string state)
{
	response = null;

	try
	{
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
		String.Format(
		//"https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20html%20where%20url%3D'https%3A%2F%2Fwww.valu.va.gov%2FMap%2FInteractiveMap%3Fstate%3D{0}'%20and%20xpath%3D'%2F%2Fdiv%5B%40class%3D%22%3FstateData%22%5D%2F%2Ftable%2Ftbody%2Ftr'"
		"https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20html%20where%20url%3D'http%3A%2F%2Fwww.va.gov%2Fdirectory%2Fguide%2Ffac_list_by_state.cfm%3FState%3D{0}%26dnum%3DAll%26isflash%3D0'%20and%20xpath%3D'%2F%2Ftable%2Ftbody%2Ftr'&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys"
		, state
		)
		);

		request.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
		request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
		request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; Touch; rv:11.0) like Gecko";
		request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");

		response = (HttpWebResponse)request.GetResponse();
	}
	catch (WebException e)
	{
		if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		else return false;
	}
	catch (Exception)
	{
		if(response != null) response.Close();
		return false;
	}

	return true;
}