<Query Kind="Program">
  <NuGetReference>HtmlAgilityPack</NuGetReference>
  <NuGetReference>morelinq</NuGetReference>
  <NuGetReference>NReco.PivotData</NuGetReference>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>MoreLinq</Namespace>
  <Namespace>NReco.PivotData</Namespace>
</Query>

void Main()
{

	//max is 902, incrementing by 40
	List<String> Items = new List<String>();
	String ESPNUrl = "http://games.espn.com/ffl/tools/projections?startIndex=";
	int increment = 40;
	int StartIndex = 0;
	StringWriter tw = new StringWriter();
	XElement PlayerData;
	do
	{
		new HtmlWeb().LoadHtmlAsXml(String.Format("{0}{1}",ESPNUrl,StartIndex),new XmlTextWriter(tw));
		Items.Add(tw.ToString());
		StartIndex = StartIndex+increment;
		tw = new StringWriter();
	} while(XElement.Parse(Items.Last()).Descendants("table").Where(xa => xa.Attributes("id").Where(xn => xn.Value.Equals("playertable_0")).Any()).Elements("tr").Skip(2).Any());
	
	Items.Count().Dump();
	
	Items.SelectMany(pt => XElement.Parse(pt).Descendants("table").Where(xa => xa.Attributes("id").Where(xn => xn.Value.Equals("playertable_0")).Any())
		.SelectMany(dt => dt.Elements("tr").Skip(2).Select(tr => tr.Elements("td").Select(cell => cell.Value).ToArray()))
		.Select(p => 
			new{
				Rank = p[0],
				Player = p[1].Replace("&nbsp;",",").Split(',')[0],
				Team = p[1].Replace("&nbsp;",",").Split(',')[1].Equals("D/ST")
						? String.Empty
						: p[1].Replace("&nbsp;",",").Split(',')[1],
				Position = p[1].Replace("&nbsp;",",").Split(',')[1].Equals("D/ST")
						? p[1].Replace("&nbsp;",",").Split(',')[1]
						: p[1].Replace("&nbsp;",",").Split(',')[2],
				
				//p[1].Replace("&nbsp;",",").Split(',')[2],
//				Player = p[1].Split(new[]{", ","&nbsp;"},StringSplitOptions.None)[0],
//				Team = p[1].Split(new[]{", ","&nbsp;"},StringSplitOptions.None)[1],
//				Position = p[1].Split(new[]{", ","&nbsp;"},StringSplitOptions.None)[2],
//				Remarks = p[1].Split(new[]{", ","&nbsp;"},StringSplitOptions.None).Count() > 4 ?  p[1].Split(new[]{", ","&nbsp;"},StringSplitOptions.None)[4] : String.Empty,
//				Completions = p[2].Split('/')[0],
//				Attempts = p[2].Split('/')[1],
				Passing = p[2],
				PassingYards = p[3],
				PassingTD = p[4],
				Interceptions = p[5],
				TotalRushes = p[6],
				TotalRushingYars = p[7],
				TotalRushingTD = p[8],
				TotalReceptions = p[9],
				TotalReceptionYards = p[10],
				TotalReceptionTD = p[11],
				TotalPoints = p[12],
			}
		
		)
		).Where(p => p.Position.Equals("D/ST")).OrderBy(p => p.Player).Select(p => p.Player.Split(' ')[0]).Dump();
	
//	using (StringWriter tw = new StringWriter())
//	{
//		do
//		{
//		String.Format("",ESPNUrl,StartIndex);
//		
//		} while ();
//	}
	
	//PlayerData.Descendants("table").Where(xa => xa.Attributes("id").Where(xn => xn.Value.Equals("playertable_0")).Any()).Elements("tr").Skip(2).Any()
	
//	tw = 
//	new HtmlWeb().LoadHtmlAsXml("http://games.espn.com/ffl/tools/projections?&startIndex=900",new XmlTextWriter(tw));
//	PlayerData = XElement.Parse(tw.ToString());
//	
//	
//	
//	.Dump();
	
	
	
	
	
//	
//	
//	
	
//	
//	var PlayerData = 
//	.Dump();
	
	//var PlayerData = hDoc.DocumentNode.SelectNodes("/html[1]/body[1]/div[2]/table[1]/tr[1]/td[1]/div[3]/div[1]/div[1]/div[1]/div[3]/div[1]/div[1]/div[2]/div[3]/div[1]/table[1]").FirstOrDefault();
	
	//PlayerData.InnerHtml.Dump();
	
	
	
}

// Define other methods and classes here