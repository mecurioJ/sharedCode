<Query Kind="Program">
  <Connection>
    <ID>6fda4b70-e1fd-42f7-944b-42e1ffe82f6b</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>NFLRaw</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	
		using (var w = new WebClient())
		{
			
			foreach (var sd in ScheduleData.ToArray().Where(gtd => Int32.Parse(gtd.GameId) >= 2009080950))
				{
					String RawData = String.Empty;
				try
				{
					RawData = w.DownloadString(String.Format("http://www.nfl.com/liveupdate/game-center/{0}/{0}_gtd.json", sd.GameId));
				}
				catch (Exception)
				{
					//sd.GameCenterData = String.Empty;
				}
				sd.GameId.Dump();
				sd.GameCenterData = RawData;
				SubmitChanges();
			}
		
		}

/*
	foreach (var element in ScheduleData//.Where(s => s.Season.Equals(2007)).Where(g => g.GameData == null)
	)
	{
		var game = 
		String.Format("http://www.nfl.com/liveupdate/game-center/{0}/{0}_gtd.json", element.GameId);
		using (var w = new WebClient())
		{
			try
			{	        
				var gameData = w.DownloadString(game);
				element.GameData = gameData;
				//element.Dump();
				
			}
			catch (Exception ex)
			{
				Console.WriteLine("{0} has no data", element.GameId);
				//throw;
			}
		}
		//game.Dump();
	}
	
	SubmitChanges();
	Console.WriteLine("Load Complete");
	*/
//	using (var w = new WebClient())
//	{
//		var gameData = w.DownloadString("http://www.nfl.com/liveupdate/game-center/1971091900/1971091900_gtd.json");
//		gameData.Dump();
//		
//	}
}

// Define other methods and classes here