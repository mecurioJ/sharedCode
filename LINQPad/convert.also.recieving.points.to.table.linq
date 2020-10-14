<Query Kind="Statements" />

var PollDate = @"01/05/2003";
var PollWeek = "Final";
var addTeams = @"Florida 120, Texas Tech 80, Oklahoma State 73, OKLAHOMAST 73, Boston College 52, Colorado State 44, LSU 38, South Florida 27, Wisconsin 15, Minnesota 4, Arkansas 3, Air Force 2, Hawaii 2, Purdue 2, North Texas 1, Fresno State 1";
addTeams.Split(',')
.Select(a => a.Trim())
.Select(b => {
	var itembreak = b.LastIndexOf(" ");
	var totalLen = b.Length;
	return new{
		PollDate,
		PollWeek,
		Ranking = String.Empty,
		Team = b.Substring(0,itembreak).Trim(),
		Record = String.Empty,
		Votes = b.Substring(itembreak).Trim()
	};
})
.Dump();