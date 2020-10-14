<Query Kind="Program">
  <Connection>
    <ID>69f30433-6c96-4ae6-a156-9c0a6d6b4f75</ID>
    <Persist>true</Persist>
    <Server>JFMobile</Server>
    <Database>NFL</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <NuGetReference>HtmlAgilityPack</NuGetReference>
  <NuGetReference Version="9.0.1">Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
{
	var game = GameData.ToArray().Last();
	
	//game.RawData.Dump();
	
	var jGame = JObject.Parse(game.RawData);
	
	var ScoreBoard = 
		jGame[game.GameId.ToString()].SelectTokens("home.score").Select(t => new Score(t)).Union(jGame[game.GameId.ToString()].SelectTokens("away.score").Select(t => new Score(t)));
	ScoreBoard.Dump();
	

	var homer = jGame[game.GameId.ToString()].SelectToken("home.stats");
	//var visitor = jGame[game.GameId.ToString()].SelectToken("away.stats");
	
	
	//var Drives = Drive.Parse(jGame[game.GameId.ToString()]["drives"]);
	//Drives.Dump();
	
	//Drives.SelectMany(d => d.Plays.SelectMany( p=> p.PlayStats.Select(ps => ps.StatId))).Distinct().Dump();
	
#region PassingStats
	var PassingStats = homer.SelectToken("passing").Select(
		t => t.Select(pss => new {
			GameId = pss.Path.Split('.').First(),
			Date = new DateTime(
				int.Parse(pss.Path.Split('.').First().Substring(0,8).Substring(0,4)),
				int.Parse(pss.Path.Split('.').First().Substring(0,8).Substring(4,2)),
				int.Parse(pss.Path.Split('.').First().Substring(0,8).Substring(6,2))),
			PlayerNFLId = pss.Path.Split('.').Last(),
			Name = pss.SelectToken("name").Value<String>(),
			Attempts = pss.SelectToken("att").Value<int>(),
			Completions = pss.SelectToken("cmp").Value<int>(),
			TotalYards = pss.SelectToken("yds").Value<int>(),
			PassingTD = pss.SelectToken("tds").Value<int>(),
			Interceptions = pss.SelectToken("ints").Value<int>(),
			TwoPointAttempts = pss.SelectToken("twopta").Value<int>(),
			TwoPointMade = pss.SelectToken("twoptm").Value<int>()
		}));
#endregion
#region RushingStats				
var RushingStats = 
	homer.SelectToken("rushing").SelectMany(
	t => t.Select(pss => new{
		GameId = pss.Path.Split('.').First(),
		Date = pss.Path.Split('.').First().Substring(0,8),
		PlayerNFLId = pss.Path.Split('.').Last(),
		Name = pss.SelectToken("name").Value<String>(),
		Attempts = pss.SelectToken("att").Value<int>(),
		TotalYards = pss.SelectToken("yds").Value<int>(),
		RushingTD = pss.SelectToken("tds").Value<int>(),
		LongestRush = pss.SelectToken("lng").Value<int>(),
		LongestTD = pss.SelectToken("lngtd").Value<int>(),
		TwoPointAttempts = pss.SelectToken("twopta").Value<int>(),
		TwoPointMade = pss.SelectToken("twoptm").Value<int>()
		}));
#endregion		
#region ReceivingStats 		
var ReceivingStats = 		
	homer.SelectToken("receiving").SelectMany(
	t => t.Select(pss => new{
		GameId = pss.Path.Split('.').First(),
		Date = pss.Path.Split('.').First().Substring(0,8),
		PlayerNFLId = pss.Path.Split('.').Last(),
		Name = pss.SelectToken("name").Value<String>(),
		Receptions = pss.SelectToken("rec").Value<int>(),
		TotalYards = pss.SelectToken("yds").Value<int>(),
		RecievingTD = pss.SelectToken("tds").Value<int>(),
		LongestReception = pss.SelectToken("lng").Value<int>(),
		LongestReceptionTD = pss.SelectToken("lngtd").Value<int>(),
		TwoPointAttempts = pss.SelectToken("twopta").Value<int>(),
		TwoPointMade = pss.SelectToken("twoptm").Value<int>()
		}));
#endregion		
#region fumbleStats		
	var fumbleStats = homer.SelectToken("fumbles").SelectMany(
	t => t.Select(pss => new{
		GameId = pss.Path.Split('.').First(),
		Date = pss.Path.Split('.').First().Substring(0,8),
		PlayerNFLId = pss.Path.Split('.').Last(),
		Name = pss.SelectToken("name").Value<String>(),
		Fumbles = pss.SelectToken("tot").Value<int>(),
		FumblesRecovered = pss.SelectToken("rcv").Value<int>(),
		TotalFumblesRecovered = pss.SelectToken("trcv").Value<int>(),
		TotalYards = pss.SelectToken("yds").Value<int>(),
		TotalLost = pss.SelectToken("lost").Value<int>(),
		}));
#endregion		
#region kickingStats	
var kickingStats = homer.SelectToken("kicking").SelectMany(
	t => t.Select(pss => new{
		GameId = pss.Path.Split('.').First(),
		Date = pss.Path.Split('.').First().Substring(0,8),
		PlayerNFLId = pss.Path.Split('.').Last(),
		Name = pss.SelectToken("name").Value<String>(),
		FieldGoalsMade = pss.SelectToken("fgm").Value<int>(),
		FieldGoalsAttempted = pss.SelectToken("fga").Value<int>(),
		FieldGoalYards = pss.SelectToken("fgyds").Value<int>(),
		TotalFieldGoalPoints = pss.SelectToken("totpfg").Value<int>(),
		ExtraPointsMade = pss.SelectToken("xpmade").Value<int>(),
		ExtraPointsMissed = pss.SelectToken("xpmissed").Value<int>(),
		ExtraPointsA = pss.SelectToken("xpa").Value<int>(),
		ExtraPointsB = pss.SelectToken("xpb").Value<int>(),
		TotalExtraPoints = pss.SelectToken("xptot").Value<int>(),
		}));	
#endregion		
#region PuntingStats		
var PuntingStats = homer.SelectToken("punting").SelectMany(
	t => t.Select(pss => new{
		GameId = pss.Path.Split('.').First(),
		Date = pss.Path.Split('.').First().Substring(0,8),
		PlayerNFLId = pss.Path.Split('.').Last(),
		Name = pss.SelectToken("name").Value<String>(),
		TotalPunts = pss.SelectToken("pts").Value<int>(),
		TotalYards = pss.SelectToken("yds").Value<int>(),
		AverageDistance = pss.SelectToken("avg").Value<int>(),
		Inside20 = pss.SelectToken("i20").Value<int>(),
		Longest = pss.SelectToken("lng").Value<int>(),
		}));
#endregion		
#region KickReturnStats
var KickReturnStats = homer.SelectToken("kickret").SelectMany(
	t => t.Select(pss => new{
		GameId = pss.Path.Split('.').First(),
		Date = pss.Path.Split('.').First().Substring(0,8),
		PlayerNFLId = pss.Path.Split('.').Last(),
		Name = pss.SelectToken("name").Value<String>(),
		Returns = pss.SelectToken("ret").Value<int>(),
		AverageYards = pss.SelectToken("avg").Value<int>(),
		ReturnForTD = pss.SelectToken("tds").Value<int>(),
		Longest = pss.SelectToken("lng").Value<int>(),
		LongestForTD = pss.SelectToken("lngtd").Value<int>(),
		}));	
#endregion
#region	PuntReturnStats	
var PuntReturnStats = homer.SelectToken("puntret").SelectMany(
	t => t.Select(pss => new{
		GameId = pss.Path.Split('.').First(),
		Date = pss.Path.Split('.').First().Substring(0,8),
		PlayerNFLId = pss.Path.Split('.').Last(),
		Name = pss.SelectToken("name").Value<String>(),
		Returns = pss.SelectToken("ret").Value<int>(),
		AverageYards = pss.SelectToken("avg").Value<int>(),
		ReturnForTD = pss.SelectToken("tds").Value<int>(),
		Longest = pss.SelectToken("lng").Value<int>(),
		LongestForTD = pss.SelectToken("lngtd").Value<int>(),
		}));	
#endregion	
#region TeamStats

var TeamStats = homer.SelectToken("defense").SelectMany(
	t => t.Select(pss => new {
		PlayerNFLId = pss.Path.Split('.').Last(),
		Name = pss.SelectToken("name").Value<String>(),
		Tackles = pss.SelectToken("tkl").Value<int>(),
		Assists = pss.SelectToken("ast").Value<int>(),
		Sacks = pss.SelectToken("sk").Value<int>(),
		Interceptions = pss.SelectToken("int").Value<int>(),
		ForcedFumbles = pss.SelectToken("ffum").Value<int>(),
		}));
		
#endregion

PassingStats.Dump();
RushingStats.Dump();
ReceivingStats.Dump();
fumbleStats.Dump();
kickingStats.Dump();
PuntingStats.Dump();
KickReturnStats.Dump();
PuntReturnStats.Dump();		
TeamStats.Dump();

homer.SelectTokens("team").Select(tm => new TeamStats(tm)).Dump();
		
	
//	var scrsummary = jGame[game.GameId.ToString()]["scrsummary"].Dump();
}

public class home
{
	public object Score {get;set;}
	public String Abbreviation {get;set;}
	public int TimeOuts {get;set;}
	public object Stats {get;set;}
}

public class Score
{
	public String GameId {get;set;}
	public String Date {get;set;}
	public String Team {get;set;}
	public int TimeOuts {get;set;}
	public int First {get;set;}
	public int Second {get;set;}
	public int Third {get;set;}
	public int Fourth {get;set;}
	public int OverTime {get;set;}
	public int Final {get;set;}
	
	public Score (JToken gameScore)
	{
		GameId = gameScore.Parent.Parent.Parent.Parent.Path;
		Date = gameScore.Parent.Parent.Parent.Parent.Path.Substring(0,8);
		Team = gameScore.Parent.Parent.SelectToken("abbr").Value<String>();
		TimeOuts = gameScore.Parent.Parent.SelectToken("to").Value<int>();
		First = gameScore["1"].Value<int>();
		Second = gameScore["2"].Value<int>();
		Third = gameScore["3"].Value<int>();
		Fourth = gameScore["4"].Value<int>();
		OverTime = gameScore["5"].Value<int>();
		Final = gameScore["T"].Value<int>();
	}
}

public class TeamStats
{
	public String GameId {get;set;}
	public String Date {get;set;}
	public String Team {get;set;}
	public int TotalFirstDowns {get;set;}
	public int TotalYards {get;set;}
	public int PassingYards {get;set;}
	public int RushingYards {get;set;}
	public int Penalties {get;set;}
	public int PenaltyYards {get;set;}
	public int Turnovers {get;set;}
	public int Punts {get;set;}
	public int TotalPuntYards {get;set;}
	public int AveragePuntYards {get;set;}
	public String TotalTimeOfPossesion {get;set;}
	
	public TeamStats (JToken tm)
	{
		GameId = tm.Parent.Parent.Parent.Parent.Path;
		Date = tm.Parent.Parent.Parent.Parent.Path.Substring(0,8);
		Team = tm.Parent.Parent.Parent.Parent.SelectToken("abbr").Value<String>();
		TotalFirstDowns = tm.Value<int>("totfd");
		TotalYards = tm.Value<int>("totyds");
		PassingYards = tm.Value<int>("pyds");
		RushingYards = tm.Value<int>("ryds");
		Penalties = tm.Value<int>("pen");
		PenaltyYards = tm.Value<int>("penyds");
		Turnovers = tm.Value<int>("trnovr");
		Punts = tm.Value<int>("pt");
		TotalPuntYards = tm.Value<int>("ptyds");
		AveragePuntYards = tm.Value<int>("ptavg");
		TotalTimeOfPossesion = tm.Value<String>("top");
	}
}

public class Drive
{
	public String Team {get;set;}
	public int Quarter {get;set;}
	public bool RedZone {get;set;}
	public int TotalFirstDowns {get;set;}
	public String DriveResult {get;set;}
	public int TotalPenaltyYards {get;set;}
	public int TotalYardsGained {get;set;}
	public int TotalNumberOfPlays {get;set;}
	public String TotalTimeOfPossesion {get;set;}
	public IEnumerable<Play> Plays {get;set;}
	public int DriveStartQuarter {get;set;}
	public String DriveStartClock {get;set;}
	public String DriveStartYardLine {get;set;}
	public String DriveStartTeam {get;set;}
	public int DriveEndQuarter {get;set;}
	public String DriveEndClock {get;set;}
	public String DriveEndYardLine {get;set;}
	public String DriveEndTeam {get;set;}
	
	public static List<Drive> Parse(JToken rawDrives)
	{
		List<Drive> Drives = new List<Drive>();
		
		for (int i = 1; i < rawDrives["crntdrv"].Value<int>(); i++)
		{
			var driveDetail = rawDrives[i.ToString()];
			Drives.Add(
			new Drive () {
			Team = driveDetail["posteam"].Value<String>(),
			Quarter = driveDetail["qtr"].Value<int>(),
				RedZone = driveDetail["redzone"].Value<bool>(),
				TotalFirstDowns = driveDetail["fds"].Value<int>(),
				DriveResult = driveDetail["result"].Value<String>(),
				TotalPenaltyYards = driveDetail["penyds"].Value<int>(),
				TotalYardsGained =  driveDetail["ydsgained"].Value<int>(),
				TotalNumberOfPlays = driveDetail["numplays"].Value<int>(), 
				TotalTimeOfPossesion = driveDetail["postime"].Value<String>(),
				DriveStartQuarter = driveDetail["start"]["qtr"].Value<int>(),
				DriveStartClock = driveDetail["start"]["time"].Value<String>(),
				DriveStartYardLine = driveDetail["start"]["yrdln"].Value<String>(),
				DriveStartTeam = driveDetail["start"]["team"].Value<String>(),
				DriveEndQuarter = driveDetail["end"]["qtr"].Value<int>(),
				DriveEndClock = driveDetail["end"]["time"].Value<String>(),
				DriveEndYardLine = driveDetail["end"]["yrdln"].Value<String>(),
				DriveEndTeam = driveDetail["end"]["team"].Value<String>(),
				Plays = driveDetail["plays"].SelectMany(pp => pp.Select(play => new Play{
						PlayId = Convert.ToInt32(pp.Path.Split('.').Last()),
						ScoringPlay = Convert.ToBoolean(play["sp"].Value<int>()),
						AccumulatedYards = play["ydsnet"].Value<int>(),
						Quarter = play["qtr"].Value<int>(),
						Down = play["down"].Value<int>(),
						Time = play["time"].Value<String>(),
						YardLine= play["yrdln"].Value<String>(),
						YardsToGo= play["ydstogo"].Value<int>(),
						Team= play["posteam"].Value<String>(),
						Description= play["desc"].Value<String>(),
						Note= play["note"].Value<String>(),
						PlayStats = play["players"].SelectMany(
							pr => pr.SelectMany(stt => JArray.Parse(stt.ToString()).Select(
								plyr => new PlayStat(plyr)))).OrderBy(s => s.Sequence)
						}))
				
			});
			
		}
		
		return Drives;
		
	}
}

public class Play
{
	public int PlayId {get;set;}
	public bool ScoringPlay {get;set;}
	public int Quarter {get;set;}
	public int Down {get;set;}
	public String Time {get;set;}
	public String YardLine {get;set;}
	public int YardsToGo {get;set;}
	public int AccumulatedYards {get;set;}
	public String Team {get;set;}
	public String Description {get;set;}
	public String Note {get;set;}
	public IEnumerable<PlayStat> PlayStats {get;set;}
}

public class PlayStat
{
	public int Sequence {get;set;}
	public String Team {get;set;}
	public String PlayerName {get;set;}
	public StatInfo Stats {get;set;}
	public int StatId {get;set;}
	public int Yards {get;set;}
	
	public PlayStat (JToken plyr)
	{
		Sequence = plyr["sequence"].Value<int>();
		Team = plyr["clubcode"].Value<String>();
		PlayerName = plyr["playerName"].Value<String>();
		StatId = plyr["statId"].Value<int>();
		//Stats = PlayerStat(plyr["statId"].Value<int>());
		Yards = plyr["yards"].Value<int>();
	}
}


public class DefenseStats
{
}