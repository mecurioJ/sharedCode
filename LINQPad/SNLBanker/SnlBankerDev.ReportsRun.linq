<Query Kind="Statements">
  <Connection>
    <ID>50f7294a-f1f5-498f-a121-56c5aa23b1b9</ID>
    <Persist>true</Persist>
    <Server>snlbankerdev1</Server>
    <Database>SNLBanker_Dev</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

ReportLoads.Select(rl => new{
	rl.User.Username,
	rl.User.FirstName,
	rl.User.LastName,
	rl.LoadDate,
	rl.LoadID,
	rl.Success,
	rl.Reporting.CubeID,
	rl.Reporting.DatabaseID,
	rl.Reporting.ReportName
}).Where(wrl => wrl.FirstName.Equals("Michael"))
.GroupBy(g => g.ReportName)
.Select(k => k.Key)
.Dump();
