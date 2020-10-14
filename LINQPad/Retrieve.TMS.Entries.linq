<Query Kind="Program">
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	String eX;
	XElement results; 
	//String StateName = "Florida";
	List<TMSDomain> TmsDomains = new List<TMSDomain>();
	
	foreach (var StateName in States)
	{
		MakeRequests(out eX, StateName);
		
		if (!string.IsNullOrEmpty(eX))		
		{
			results = XElement.Parse(eX);
			XNamespace nsi = results.Name.Namespace;
			try
			{	
				TmsDomains.AddRange(results.Element("results").Elements("tr").Where(c => c.Elements().Count() > 4)
				.Select(c => c.Elements("td").Select(v => v.Value.Trim()).ToArray())
				.Select(a => new TMSDomain(a, StateName)));	        
			}
			catch (Exception ex)
			{
				String.Format(
				"!!{0}--{1}!!"
				, StateName
				, ex.InnerException
				).Dump();
			
			}
		}
		else
		{
			StateName.Dump();
		}
		
	}
	
	
	StreamWriter sr = new StreamWriter(@"d:\TMSEntrySelect.sql");
	foreach( var ln in TmsDomains.Select(t => string.Format("SELECT '{0}','{1}','{2}','{3}' UNION \r\n",
		t.StateName,
		t.Name,
		t.Organization,
		t.Domain)))
	{
	sr.WriteLine(ln);
	}
	
	sr.Close();
	
	
}

public class TMSDomain
{
	public String StateName {get;set;}
	public String Name {get;set;}
	public String Organization {get;set;}
	public String Domain {get;set;}
	
	public TMSDomain(){}
	public TMSDomain(String[] a, String stateName)
	{
		Name = a[0].ToString();
		StateName = stateName;
		Organization = a[1].ToString();
		Domain = a[2].ToString();
	}
}

private string[] States
{
	get {
		return new []{"Washington",
"Oregon",
"California",
"Idaho",
"Nevada",
"Utah",
"Arizona",
"Montana",
"Wyoming",
"Colorado",
"New%2520Mexico",
"North%2520Dakota",
"South%2520Dakota",
"Nebraska",
"Kansas",
"Oklahoma",
"Texas",
"Minnesota",
"Iowa",
"Missouri",
"Arkansas",
"Louisiana",
"Wisconsin",
"Illinois",
"Kentucky",
"Tennessee",
"Mississippi",
"Alabama",
"Georgia",
"Florida",
/*
https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20html%20where%20url%3D'https%3A%2F%2Fwww.valu.va.gov%2FMap%2FInteractiveMap%3Fstate%3DNorth%2520Dakota'%20and%20xpath%3D'%2F%2Fdiv%5B%40class%3D%22%3FstateData%22%5D%2F%2Ftable%2Ftbody%2Ftr'&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys
*/
"Puerto%2520Rico",
"South%2520Carolina",
"North%2520Carolina",
"Virginia",
"Michigan",
"Indiana",
"Ohio",
"West%2520Virginia",
"Pennsylvania",
"New%2520York",
"Maryland",
"Delaware",
"New%2520Jersey",
"Connecticut",
"Rhode%2520Island",
"Massachusetts",
"Vermont",
"New%2520Hampshire",
"Maine",
"Alaska",
"Hawaii",
"New%2520Jersey",
"Vermont",
"New%2520Hampshire",
"Massachusetts",
"DC"};
	
	}
}

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
		"https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20html%20where%20url%3D'https%3A%2F%2Fwww.valu.va.gov%2FMap%2FInteractiveMap%3Fstate%3D{0}'%20and%20xpath%3D'%2F%2Fdiv%5B%40class%3D%22%3FstateData%22%5D%2F%2Ftable%2Ftbody%2Ftr'&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys"
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