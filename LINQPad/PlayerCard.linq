<Query Kind="Program">
  <Connection>
    <ID>6fda4b70-e1fd-42f7-944b-42e1ffe82f6b</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>DraftKit</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	CustomProjections
	.Join(ByeWeeks,
		p => p.DetailsTeam,
		b => b.Team,
		(p,b) => new PlayerCard () {
			PlayerId = p.DetailsPlayerId,
			StatId = Players.Where(pp => pp.ProfileId.Equals(p.DetailsPlayerId)).FirstOrDefault().GsisId,
			PlayerName = p.DetailsPlayer,
			Position = p.DetailsPosition,
			Experience = p.DetailsExperience,
			Team = b.Team,
			TeamName = b.TeamName,
			ByeWeek = b.ByeWeek
		}
	)
	.Dump();
}

// Define other methods and classes here

public class PlayerCard
{
	public String PlayerId { get; set; }
	public String StatId { get; set; }
	public String PlayerName { get; set; }
	public String Position { get; set; }
	public int? Experience { get; set; }
	public String Team { get; set; }
	public String TeamName { get; set; }
	public int? ByeWeek { get; set; }
}

