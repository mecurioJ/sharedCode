<Query Kind="Program">
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	MakeRequests();
	//https://developer.yahoo.com/yql/console/?q=select%20*%20from%20html%20where%20url%3D%27http%3A%2F%2Fwww.footballdb.com%2Ffantasy-football%2Findex.html%3Fpos%3DQB%26yr%3D2015%26wk%3D1%26rules%3D1%27%20and%20xpath%3D%27%2F%2Ftable%2F*%5Bcontains(.%2C%22player%22)%5D%2F%2Fa%27&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys#h=select+*+from+html+where+url%3D%27http%3A%2F%2Fwww.footballdb.com%2Ffantasy-football%2Findex.html%3Fpos%3DQB%26yr%3D2015%26wk%3D1%26rules%3D1%27+and+xpath%3D%27%2F%2Ftable%5B%40class%3D%22statistics%22%5D%27
	
}


private void MakeRequests()
{
	HttpWebResponse response;
	var Positions = new []{
	//"QB", "RB", "WR", "TE"
	//,"RB%2CWR%2CTE", 
	//"K"
	 "DST"
	};
	int Year = 2015;
XElement Players = new XElement("Players");
	
	
	//QB, RB, WR, TE,RB%2CWR%2CTE, K, DST
	//string Position = "QB";
//	string Position = "RB";
//	string Position = "WR";
//	string Position = "DST";
//	string Position = "DST";
//	string Position = "DST";
//	string Position = "DST";
foreach(String Position in Positions)
	{

//List<XElement> Players = new List<XElement>();
	for (int Week = 1; Week < 18; Week++)
	{
		String urlPath = 
		string.Format(
		"https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20html%20where%20url%3D'http%3A%2F%2Fwww.footballdb.com%2Ffantasy-football%2Findex.html%3Fpos%3D{0}%26yr%3D{1}%26wk%3D{2}%26rules%3D1'%20%20and%20xpath%20%3D'%2F%2Ftable%5B%40class%3D%22statistics%22%5D%2Ftbody%2Ftr'&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys",
		Position,
		Year,
		Week
		);
		
		XElement xElm;
	
		if (Request_query_yahooapis_com(urlPath, out response))
		{
			using (Stream stream = response.GetResponseStream())
			{
				xElm = XElement.Parse(new StreamReader(stream, Encoding.UTF8).ReadToEnd());
				Players.Add(
				xElm.Element("results").Elements("tr")
					.Select(td => td.Elements("td").Select(v => v.Value)
					.ToArray()).Where(cc => cc.Count() > 2).Select(result => new XElement("Player",
								new XAttribute("SeasonYear",Year),
								new XAttribute("CompetitionWeek",Week),
								new XAttribute("Position",Position),
								new XAttribute("PlayerName",result[0].Split(',').First()),
								new XAttribute("Team",result[0].Split(',').Last().Trim()),
								new XAttribute("Opponent",result[1]),
								new XAttribute("HomeOrAway",result[1].Contains("@") ? "Away" : "Home"),					
					new XAttribute("Sacks",Convert.ToDecimal(result[3].ToString())),
					new XAttribute("Interceptions",Convert.ToDecimal(result[4].ToString())),
					new XAttribute("Safeties",Convert.ToDecimal(result[5].ToString())),
					new XAttribute("FumblesRecovered",Convert.ToDecimal(result[6].ToString())),
					new XAttribute("BlockedKicks",Convert.ToDecimal(result[7].ToString())),
					new XAttribute("Touchdowns",Convert.ToDecimal(result[8].ToString())),
					new XAttribute("PointsAgainst",Convert.ToDecimal(result[9].ToString())),
					new XAttribute("PassingYardsAllowed",Convert.ToDecimal(result[10].ToString())),
					new XAttribute("RushingYardsAllowed",Convert.ToDecimal(result[11].ToString())),
					new XAttribute("TotalYardsAllowed",Convert.ToDecimal(result[12].ToString()))					
//					new XAttribute("ExtraPointsAttempted",Convert.ToDecimal(result[3].ToString())),
//					new XAttribute("ExtraPointsMade",Convert.ToDecimal(result[4].ToString())),
//					new XAttribute("FieldGoalsAttempted",Convert.ToDecimal(result[5].ToString())),
//					new XAttribute("FieldGoalsMade",Convert.ToDecimal(result[6].ToString())),
//					new XAttribute("LongerThan50",Convert.ToDecimal(result[7].ToString()))
//								new XAttribute("PassingAttempts",Convert.ToDecimal(result[3].ToString())),
//								new XAttribute("PassingCompletions",Convert.ToDecimal(result[4].ToString())),
//								new XAttribute("TotalPassingYards",Convert.ToDecimal(result[5].ToString())),
//								new XAttribute("PassingTouchdowns",Convert.ToDecimal(result[6].ToString())),
//								new XAttribute("TotalInterceptions",Convert.ToDecimal(result[7].ToString())),
//								new XAttribute("TotalPassingTwoPointConversions",Convert.ToDecimal(result[8].ToString())),
//								new XAttribute("RushingAttempts",Convert.ToDecimal(result[9].ToString())),
//								new XAttribute("TotalRushingYards",Convert.ToDecimal(result[10].ToString())),
//								new XAttribute("RushingTouchdowns",Convert.ToDecimal(result[11].ToString())),
//								new XAttribute("TotalRushingTwoPointConversions",Convert.ToDecimal(result[12].ToString())),
//								new XAttribute("TotalReceptions",Convert.ToDecimal(result[13].ToString())),
//								new XAttribute("TotalReceivingYards",Convert.ToDecimal(result[14].ToString())),
//								new XAttribute("ReceivingTouchdowns",Convert.ToDecimal(result[15].ToString())),
//								new XAttribute("TotalReceivingTwoPointConversions",Convert.ToDecimal(result[16].ToString())),
//								new XAttribute("Fumbles",Convert.ToDecimal(result[17].ToString())),
//								new XAttribute("FumblesForTouchdown",Convert.ToDecimal(result[18].ToString()))
					
					)));
				
				//xElm.Save(string.Format(@"D:\projects\FFA\XMLData\{0}_WK_{1}_YR_{2}.xml",Position, Week, Year));
			}
			response.Close();
		}	
	}
	
	}
	
	Players.Save(String.Format(@"D:\projects\FFA\XMLData\{0}_DST.xml",2015));
	
}

private bool Request_query_yahooapis_com(String urlPath, out HttpWebResponse response)
{
	response = null;

	try
	{
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPath);

		request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:48.0) Gecko/20100101 Firefox/48.0";
		request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
		request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
		request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
		request.Headers.Set(HttpRequestHeader.Cookie, @"BX=badcrnlb6i1o5&b=4&d=xCNqv4tpYEKTADYoxcNDuyY0mLU-&s=16&i=p1Vf7Nzqll7DK.0z4MER");
		request.Headers.Add("DNT", @"1");
		request.KeepAlive = true;
		request.Headers.Add("Upgrade-Insecure-Requests", @"1");
		request.Headers.Set(HttpRequestHeader.CacheControl, "max-age=0");

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
// Define other methods and classes here