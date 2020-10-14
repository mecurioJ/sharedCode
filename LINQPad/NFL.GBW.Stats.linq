<Query Kind="Program">
  <Connection>
    <ID>359f551a-465b-4231-b045-807385f318cd</ID>
    <Persist>true</Persist>
    <Server>JOEYSURFACEPRO</Server>
    <Database>NFL</Database>
    <ShowServer>true</ShowServer>
  </Connection>
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
	var game = SourceTexts.ToArray().Last();
	var jGame = JObject.Parse(game.JsonText);
	var GameKey = jGame.First.Path.Dump();
	
	//get the NFL Key for the Game
	
	var Passing = Stats.PassingStats(jGame[GameKey],GameKey).Dump();
	var Rushing = Stats.RushingStats(jGame[GameKey],GameKey).Dump();
	var Receiving = Stats.ReceivingStats(jGame[GameKey],GameKey).Dump();
	var Fumbles = Stats.FumbleStats(jGame[GameKey],GameKey).Dump();
	var token = jGame[GameKey];
	var gameKey = GameKey;
//			"00-0025437":{"name":"C.Henry","att":6,"yds":23,"tds":0,"lng":16,"twopta":0,"twoptm":0},
	

	
}


public class Stats
{

public static IEnumerable<Passing> PassingStats (JToken token, string gameKey)
	{
		return token.SelectToken("home.stats.passing")
	.Children().Children()
	.Select(
		ply => new Passing(){
			PlayerKey = ply.Path.Split('.').Last(),
			GameKey = gameKey,
			Team = token.SelectToken("home.abbr").Value<String>(),
			Name = ply.SelectToken("name").Value<String>(),
			Attempts = ply.SelectToken("att").Value<int>(),
			Completions = ply.SelectToken("cmp").Value<int>(),
			PassingYards = ply.SelectToken("yds").Value<int>(),
			PassingTouchdowns = ply.SelectToken("tds").Value<int>(),
			Interceptions = ply.SelectToken("ints").Value<int>(),
			TwoPointAttempts = ply.SelectToken("twopta").Value<int>(),
			TwoPointsCompleted = ply.SelectToken("twoptm").Value<int>(),
		}
	).Union(
	token.SelectToken("away.stats.passing")
	.Children().Children()
	.Select(
		ply => new Passing() {
			PlayerKey = ply.Path.Split('.').Last(),
			GameKey = gameKey,
			Name = ply.SelectToken("name").Value<String>(),
			Team = token.SelectToken("away.abbr").Value<String>(),
			Attempts = ply.SelectToken("att").Value<int>(),
			Completions = ply.SelectToken("cmp").Value<int>(),
			PassingYards = ply.SelectToken("yds").Value<int>(),
			PassingTouchdowns = ply.SelectToken("tds").Value<int>(),
			Interceptions = ply.SelectToken("ints").Value<int>(),
			TwoPointAttempts = ply.SelectToken("twopta").Value<int>(),
			TwoPointsCompleted = ply.SelectToken("twoptm").Value<int>(),
		}
	));
	}
public static IEnumerable<Rushing> RushingStats (JToken token, string gameKey)
	{
		return token.SelectToken("home.stats.rushing")
	.Children()
	.Children()
	.Select(
		ply => new Rushing(){
			PlayerKey = ply.Path.Split('.').Last(),
			GameKey = gameKey,
			Name = ply.SelectToken("name").Value<String>(),
			Team = token.SelectToken("home.abbr").Value<String>(),
			Attempts = ply.SelectToken("att").Value<int>(),
			RushingYards = ply.SelectToken("yds").Value<int>(),
			RushingTouchdowns = ply.SelectToken("tds").Value<int>(),
			Longest = ply.SelectToken("lng").Value<int>(),
			TwoPointAttempts = ply.SelectToken("twopta").Value<int>(),
			TwoPointsCompleted = ply.SelectToken("twoptm").Value<int>(),
		}
	).Union(
	token.SelectToken("away.stats.rushing")
	.Children()
	.Children()
	.Select(
		ply => new Rushing() {
			PlayerKey = ply.Path.Split('.').Last(),
			GameKey = gameKey,
			Name = ply.SelectToken("name").Value<String>(),
			Team = token.SelectToken("away.abbr").Value<String>(),
			Attempts = ply.SelectToken("att").Value<int>(),
			RushingYards = ply.SelectToken("yds").Value<int>(),
			RushingTouchdowns = ply.SelectToken("tds").Value<int>(),
			Longest = ply.SelectToken("lng").Value<int>(),
			TwoPointAttempts = ply.SelectToken("twopta").Value<int>(),
			TwoPointsCompleted = ply.SelectToken("twoptm").Value<int>(),
		}
	));
	}
public static IEnumerable<Receiving> ReceivingStats (JToken token, string gameKey)
	{
		return token.SelectToken("home.stats.receiving")
	.Children()
	.Children()
	.Select(
		ply => new Receiving(){
			PlayerKey = ply.Path.Split('.').Last(),
			GameKey = gameKey,
			Name = ply.SelectToken("name").Value<String>(),
			Team = token.SelectToken("home.abbr").Value<String>(),
			Receptions = ply.SelectToken("rec").Value<int>(),
			ReceivingYards = ply.SelectToken("yds").Value<int>(),
			ReceivingTouchdowns = ply.SelectToken("tds").Value<int>(),
			Longest = ply.SelectToken("lng").Value<int>(),
			TwoPointAttempts = ply.SelectToken("twopta").Value<int>(),
			TwoPointsCompleted = ply.SelectToken("twoptm").Value<int>(),
		}
	).Union(
	token.SelectToken("away.stats.receiving")
	.Children()
	.Children()
	.Select(
		ply => new Receiving() {
			PlayerKey = ply.Path.Split('.').Last(),
			GameKey = gameKey,
			Name = ply.SelectToken("name").Value<String>(),
			Team = token.SelectToken("away.abbr").Value<String>(),
			Receptions = ply.SelectToken("rec").Value<int>(),
			ReceivingYards = ply.SelectToken("yds").Value<int>(),
			ReceivingTouchdowns = ply.SelectToken("tds").Value<int>(),
			Longest = ply.SelectToken("lng").Value<int>(),
			TwoPointAttempts = ply.SelectToken("twopta").Value<int>(),
			TwoPointsCompleted = ply.SelectToken("twoptm").Value<int>(),
		}
	));
	}

public static IEnumerable<Fumble> FumbleStats (JToken token, string gameKey)
	{
		return token.SelectToken("home.stats.fumbles")
	.Children()
	.Children()
	.Select(
		ply => new Fumble(){
			PlayerKey = ply.Path.Split('.').Last(),
			GameKey = gameKey,
			Name = ply.SelectToken("name").Value<String>(),
			Team = token.SelectToken("home.abbr").Value<String>(),
			Total = ply.SelectToken("tot").Value<int>(),
			Recovered = ply.SelectToken("rcv").Value<int>(),
			TeamRecovered = ply.SelectToken("trcv").Value<int>(),
			Yards = ply.SelectToken("yds").Value<int>(),
			Lost = ply.SelectToken("lost").Value<int>()
		}
	).Union(
	token.SelectToken("away.stats.fumbles")
	.Children()
	.Children()
	.Select(
		ply => new Fumble() {
			//"00-0021379":{"name":"P.Ramsey","tot":1,"rcv":1,"trcv":1,"yds":0,"lost":0},
			PlayerKey = ply.Path.Split('.').Last(),
			GameKey = gameKey,
			Name = ply.SelectToken("name").Value<String>(),
			Team = token.SelectToken("away.abbr").Value<String>(),
			Total = ply.SelectToken("tot").Value<int>(),
			Recovered = ply.SelectToken("rcv").Value<int>(),
			TeamRecovered = ply.SelectToken("trcv").Value<int>(),
			Yards = ply.SelectToken("yds").Value<int>(),
			Lost = ply.SelectToken("lost").Value<int>()
		}
	));
	}
	
public class Passing
{
	public String PlayerKey {get;set;}
	public String GameKey {get;set;}
	public String Team  {get;set;}
	public String Name  {get;set;}
	public int Attempts  {get;set;}
	public int Completions  {get;set;}
	public int PassingYards  {get;set;}
	public int PassingTouchdowns  {get;set;}
	public int Interceptions {get;set;}
	public int TwoPointAttempts  {get;set;}
	public int TwoPointsCompleted  {get;set;}
	
	
}

public class Rushing
{
	public String PlayerKey {get;set;}
	public String GameKey {get;set;}
	public String Team  {get;set;}
	public String Name  {get;set;}
	public int Attempts  {get;set;}
	public int RushingYards  {get;set;}
	public int RushingTouchdowns  {get;set;}
	public int Longest {get;set;}
	public int TwoPointAttempts  {get;set;}
	public int TwoPointsCompleted  {get;set;}
}

public class Receiving
{
	public String PlayerKey {get;set;}
	public String GameKey {get;set;}
	public String Team  {get;set;}
	public String Name  {get;set;}
	public int Receptions  {get;set;}
	public int ReceivingYards  {get;set;}
	public int ReceivingTouchdowns  {get;set;}
	public int Longest {get;set;}
	public int TwoPointAttempts  {get;set;}
	public int TwoPointsCompleted  {get;set;}
	
}

public class Fumble
{
	public String PlayerKey {get;set;}
	public String GameKey {get;set;}
	public String Team  {get;set;}
	public String Name  {get;set;}
	public int Total  {get;set;}
	public int Recovered  {get;set;}
	public int TeamRecovered  {get;set;}
	public int Yards  {get;set;}
	public int Lost  {get;set;}
	
}


}
/*
{
	"score":{"1":14,"2":7,"3":0,"4":0,"5":0,"T":21},
	"abbr":"TEN",
	"to":2,
	"stats":{
		"fumbles":{
			"00-0027095":{"name":"G.McRath","tot":0,"rcv":0,"trcv":1,"yds":0,"lost":0}
		},
		"kicking":{
			"00-0020962":{"name":"R.Bironas","fgm":0,"fga":0,"fgyds":0,"totpfg":0,"xpmade":3,"xpmissed":0,"xpa":3,"xpb":0,"xptot":3}},
		"punting":{"00-0026550":{"name":"A.Trapasso","pts":6,"yds":249,"avg":40.29999923706055,"i20":2,"lng":51}},
		"kickret":{
			"00-0027066":{"name":"R.Mouton","ret":2,"avg":21,"tds":0,"lng":24},
			"00-0027136":{"name":"J.McCourty","ret":2,"avg":14,"tds":0,"lng":21}},
		"puntret":{
			"00-0025515":{"name":"C.Davis","ret":3,"avg":5,"tds":0,"lng":7},
			"00-0027066":{"name":"R.Mouton","ret":1,"avg":9,"tds":0,"lng":9}},
		"defense":{
			"00-0026274":{"name":"S.Keglar","tkl":5,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0027066":{"name":"R.Mouton","tkl":4,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0022814":{"name":"D.Ball","tkl":3,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0009056":{"name":"J.Kearse","tkl":3,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0023543":{"name":"V.Fuller","tkl":3,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0023648":{"name":"K.Vickerson","tkl":3,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0022100":{"name":"D.Nickey","tkl":2,"ast":1,"sk":0,"int":0,"ffum":0},
			"00-0027038":{"name":"S.Marks","tkl":2,"ast":0,"sk":1,"int":0,"ffum":0},
			"00-0026243":{"name":"W.Hayes","tkl":2,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0024690":{"name":"L.Birdine","tkl":2,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0027095":{"name":"G.McRath","tkl":2,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0019648":{"name":"K.Bulluck","tkl":2,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0020382":{"name":"K.Vanden Bosch","tkl":1,"ast":0,"sk":1,"int":0,"ffum":0},
			"00-0019733":{"name":"N.Harper","tkl":1,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0024429":{"name":"C.Finnegan","tkl":1,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0021230":{"name":"D.Thornton","tkl":1,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0021219":{"name":"C.Hope","tkl":1,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0023622":{"name":"J.Haye","tkl":1,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0021286":{"name":"D.Faggins","tkl":1,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0023988":{"name":"T.Davis","tkl":1,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0026958":{"name":"N.Schommer","tkl":1,"ast":0,"sk":0,"int":0,"ffum":0},
			"00-0024418":{"name":"L.Ramsey","tkl":0,"ast":1,"sk":0,"int":0,"ffum":0},
			"00-0027136":{"name":"J.McCourty","tkl":0,"ast":0,"sk":0,"int":1,"ffum":0},
			"00-0025406":{"name":"M.Griffin","tkl":0,"ast":0,"sk":0,"int":1,"ffum":0}
			},
			"team":{"totfd":14,"totyds":271,"pyds":198,"ryds":73,"pen":14,"penyds":132,"trnovr":2,"pt":6,"ptyds":249,"ptavg":40.29999923706055,"top":"32:58"}},
	"players":null
}
*/