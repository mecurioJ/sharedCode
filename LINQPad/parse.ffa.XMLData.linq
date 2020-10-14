<Query Kind="Program" />

void Main()
{
	var pathName = @"D:\projects\FFA\XMLData\";
	var fileName = @"DST_WK_1_YR_2015.xml";
	XNamespace nm ="http://www.yahooapis.com/v1/base.rng";
	
	List<Player> QBs = new List<Player>();
	
	XElement Players = new XElement("Players");
	
	var results = XElement.Load(pathName+fileName).Element("results").Elements("tr")
		.Select(td => td.Elements("td").Select(v => v.Value)
		.ToArray()).Where(cc => cc.Count() > 2)
		.Select(result => new XElement("Player",
					new XAttribute("SeasonYear",2015),
					new XAttribute("CompetitionWeek",1),
					new XAttribute("PlayerName",result[0]),//.Split(',').First()),
					new XAttribute("Team",result[0]),//.Split(',').Last().Trim()),
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
//					new XAttribute("TotalReceptions",Convert.ToDecimal(result[13].ToString())),
//					new XAttribute("TotalReceivingYards",Convert.ToDecimal(result[14].ToString())),
//					new XAttribute("ReceivingTouchdowns",Convert.ToDecimal(result[15].ToString())),
//					new XAttribute("TotalReceivingTwoPointConversions",Convert.ToDecimal(result[16].ToString())),
//					new XAttribute("Fumbles",Convert.ToDecimal(result[17].ToString())),
//					new XAttribute("FumblesForTouchdown",Convert.ToDecimal(result[18].ToString()))
//		
		))
		.Dump();
//		.Select(t => {
//			var nameParse = t[i][0].Split(',');
//		XElement Player = new XElement("Player");
//		Player.Add(
//			new XAttribute("SeasonYear",2015),
//			new XAttribute("CompetitionWeek",1),
//			new XAttribute("PlayerName",nameParse.First()),
//			new XAttribute("Team",nameParse.Last()),
//			new XAttribute("Opponent",t[i][1]),
//			new XAttribute("HomeOrAway",results[i][1].Contains("@") ? "Away" : "Home"),
//			new XAttribute("PassingAttempts",Convert.ToDecimal(results[i][3].ToString())),
//			new XAttribute("PassingCompletions",Convert.ToDecimal(results[i][4].ToString())),
//			new XAttribute("TotalPassingYards",Convert.ToDecimal(results[i][5].ToString())),
//			new XAttribute("PassingTouchdowns",Convert.ToDecimal(results[i][6].ToString())),
//			new XAttribute("TotalInterceptions",Convert.ToDecimal(results[i][7].ToString())),
//			new XAttribute("TotalPassingTwoPointConversions",Convert.ToDecimal(results[i][8].ToString())),
//			new XAttribute("RushingAttempts",Convert.ToDecimal(results[i][9].ToString())),
//			new XAttribute("TotalRushingYards",Convert.ToDecimal(results[i][10].ToString())),
//			new XAttribute("RushingTouchdowns",Convert.ToDecimal(results[i][11].ToString())),
//			new XAttribute("TotalRushingTwoPointConversions",Convert.ToDecimal(results[i][12].ToString())),
//			new XAttribute("TotalReceptions",Convert.ToDecimal(results[i][13].ToString())),
//			new XAttribute("TotalReceivingYards",Convert.ToDecimal(results[i][14].ToString())),
//			new XAttribute("ReceivingTouchdowns",Convert.ToDecimal(results[i][15].ToString())),
//			new XAttribute("TotalReceivingTwoPointConversions",Convert.ToDecimal(results[i][16].ToString())),
//			new XAttribute("Fumbles",Convert.ToDecimal(results[i][17].ToString())),
//			new XAttribute("FumblesForTouchdown",Convert.ToDecimal(results[i][18].ToString()))
//		);
//		
		
		//.Dump();
		/*
	for (int i = 0; i < results.Count(); i++)
	{
		var nameParse = results[i][0].Split(',');
		XElement Player = new XElement("Player");
		Player.Add(
			new XAttribute("SeasonYear",2015),
			new XAttribute("CompetitionWeek",1),
			new XAttribute("PlayerName",nameParse.First()),
			new XAttribute("Team",nameParse.Last()),
			new XAttribute("Opponent",results[i][1]),
			new XAttribute("HomeOrAway",results[i][1].Contains("@") ? "Away" : "Home"),
			new XAttribute("PassingAttempts",Convert.ToDecimal(results[i][3].ToString())),
			new XAttribute("PassingCompletions",Convert.ToDecimal(results[i][4].ToString())),
			new XAttribute("TotalPassingYards",Convert.ToDecimal(results[i][5].ToString())),
			new XAttribute("PassingTouchdowns",Convert.ToDecimal(results[i][6].ToString())),
			new XAttribute("TotalInterceptions",Convert.ToDecimal(results[i][7].ToString())),
			new XAttribute("TotalPassingTwoPointConversions",Convert.ToDecimal(results[i][8].ToString())),
			new XAttribute("RushingAttempts",Convert.ToDecimal(results[i][9].ToString())),
			new XAttribute("TotalRushingYards",Convert.ToDecimal(results[i][10].ToString())),
			new XAttribute("RushingTouchdowns",Convert.ToDecimal(results[i][11].ToString())),
			new XAttribute("TotalRushingTwoPointConversions",Convert.ToDecimal(results[i][12].ToString())),
			new XAttribute("TotalReceptions",Convert.ToDecimal(results[i][13].ToString())),
			new XAttribute("TotalReceivingYards",Convert.ToDecimal(results[i][14].ToString())),
			new XAttribute("ReceivingTouchdowns",Convert.ToDecimal(results[i][15].ToString())),
			new XAttribute("TotalReceivingTwoPointConversions",Convert.ToDecimal(results[i][16].ToString())),
			new XAttribute("Fumbles",Convert.ToDecimal(results[i][17].ToString())),
			new XAttribute("FumblesForTouchdown",Convert.ToDecimal(results[i][18].ToString()))
		);
		Players.Add(Player);
	}
	
	Players.Dump();
	*/
	//plyr.Dump();	
	

	
}

public class Player
{
	public int SeasonYear {get;set;}
	public int CompetitionWeek {get;set;}
	public string PlayerName {get;set;}
	public string Team {get;set;}
	public string Opponent {get;set;}
	public string HomeOrAway {get;set;}
	public dynamic PassingAttempts {get;set;}
	public dynamic PassingCompletions {get;set;}
	public dynamic PassingTouchdowns {get;set;}
	public dynamic TotalPassingYards {get;set;}
	public dynamic TotalInterceptions {get;set;}
	public dynamic TotalPassingTwoPointConversions {get;set;}
	public dynamic RushingAttempts {get;set;}	
	public dynamic TotalRushingYards {get;set;}
	public dynamic RushingTouchdowns {get;set;}
	public dynamic TotalRushingTwoPointConversions {get;set;}
	public dynamic TotalReceptions {get;set;}
	public dynamic TotalReceivingYards  {get;set;}
	public dynamic ReceivingTouchdowns  {get;set;}
	public dynamic TotalReceivingTwoPointConversions {get;set;}
	public dynamic Fumbles {get;set;}
	public dynamic FumblesForTouchdown {get;set;}
	
}

// Define other methods and classes here
