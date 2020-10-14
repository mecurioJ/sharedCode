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
	var CVBase = 
	SeasonStats
	.ToArray()
	.Where(sts => new[]{"TE","QB","RB","WR"}.Contains(sts.Pos))
	//.Where(sts => sts.Season.Equals(2016))
	.Select(gid => new{
			gid.Id,
			gid.Name,
			gid.Pos,
			gid.Week,
			gid.Team,
			gid.Season,
			GamePoints = ((float)(gid.Passing_yds)*(1d/25d))+ ((float)(gid.Rushing_yds)*(1d/10d))
			+((float)(gid.Receiving_yds)*(1d/10d))+(((float)gid.Passing_tds)*4d)
			+(((float)gid.Rushing_tds)*6d)+(((float)gid.Receiving_tds)*6d)
			+((float)gid.Fumbles_lost*-2d)+((float)gid.Passing_twoptm*2d)
			+((float)gid.Receiving_twoptm*2d)+((float)gid.Rushing_twoptm*2d)
			})
	.GroupBy(ply => Tuple.Create(ply.Id, ply.Season), ply => ply.GamePoints)
	.Select(g => new{PlayerId = g.Key.Item1, Season = g.Key.Item2, Points = g.Sum(), sd = StdDev(g), Mean = g.Average(), CV = (StdDev(g)/g.Average()) })
	;

	var pSet = CustomProjections
	.Join(ByeWeeks,
		p => p.DetailsTeam,
		b => b.Team,
		(p,b) => new {
			PlayerId = p.DetailsPlayerId,
			StatId = Players.Where(pp => pp.ProfileId.Equals(p.DetailsPlayerId)).FirstOrDefault().GsisId,
			PlayerName = p.DetailsPlayer,
			Position = p.DetailsPosition,
			Experience = p.DetailsExperience,
			Team = b.Team,
			TeamName = b.TeamName,
			ByeWeek = b.ByeWeek,
			AverageDraftPosition = p.AverageDraftPositionMean,
			OverallRank = p.OverallRankMean,
			PositionRank = p.PositionRankMean,
			ECRRank = p.ECRRankMean,
			ECRPositionRank = p.ECRPositionRankMean,
			Points = p.PointsMean,
			Floor = p.PointsFloorMean,
			ValueOverReplacement = p.ValueOverReplacementMean,
			Risk = p.RiskMean
		}
	).ToArray();
	
	
	pSet.Select(ccp => new{
		ccp.PlayerId,
		ccp.PlayerName,
		ccp.Position,
		ccp.Team,
		ccp.Experience,
		ccp.TeamName,
		ccp.ByeWeek,
		
		CVV2010 = CVBase.Where(sn => sn.Season.HasValue && sn.Season.Value.Equals(2010)).Where(k => k.PlayerId.Equals(ccp.StatId)).Select(cv => cv.CV).FirstOrDefault(),
		CVV2011 = CVBase.Where(sn => sn.Season.HasValue && sn.Season.Value.Equals(2011)).Where(k => k.PlayerId.Equals(ccp.StatId)).Select(cv => cv.CV).FirstOrDefault(),
		CVV2012 = CVBase.Where(sn => sn.Season.HasValue && sn.Season.Value.Equals(2012)).Where(k => k.PlayerId.Equals(ccp.StatId)).Select(cv => cv.CV).FirstOrDefault(),
		CVV2013 = CVBase.Where(sn => sn.Season.HasValue && sn.Season.Value.Equals(2013)).Where(k => k.PlayerId.Equals(ccp.StatId)).Select(cv => cv.CV).FirstOrDefault(),
		CVV2014 = CVBase.Where(sn => sn.Season.HasValue && sn.Season.Value.Equals(2014)).Where(k => k.PlayerId.Equals(ccp.StatId)).Select(cv => cv.CV).FirstOrDefault(),
		CVV2015 = CVBase.Where(sn => sn.Season.HasValue && sn.Season.Value.Equals(2015)).Where(k => k.PlayerId.Equals(ccp.StatId)).Select(cv => cv.CV).FirstOrDefault(),
		CVV2016 = CVBase.Where(sn => sn.Season.HasValue && sn.Season.Value.Equals(2016)).Where(k => k.PlayerId.Equals(ccp.StatId)).Select(cv => cv.CV).FirstOrDefault(),
	})
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


    public static double StdDev(IEnumerable<double> values)
    {
       double ret = 0;
       int count = values.Count();
       if (count  > 1)
       {
          //Compute the Average
          double avg = values.Average();

          //Perform the Sum of (value-avg)^2
          double sum = values.Sum(d => (d - avg) * (d - avg));

          //Put it all together
          ret = Math.Sqrt(sum / count);
       }
       return ret;
    }