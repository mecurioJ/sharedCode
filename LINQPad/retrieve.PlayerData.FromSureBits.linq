<Query Kind="Program">
  <Connection>
    <ID>6fda4b70-e1fd-42f7-944b-42e1ffe82f6b</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>DraftKit</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <NuGetReference Version="9.0.1">Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
</Query>

void Main()
{
	var sequence = new[]{
	"http://api.suredbits.com/nfl/v0/players/Williams/Mike"
	, "http://api.suredbits.com/nfl/v0/players/O'Neal"
	};
	//new System.Net.WebClient().DownloadString("http://api.suredbits.com/nfl/v0/players/McCaffrey/Christian");
	List<String> Inserts = new List<String>();
	
	foreach (var element in sequence)
	{
	var jPlayer = (JArray)JsonConvert.DeserializeObject(new System.Net.WebClient().DownloadString(element));
	
	
		for (int i = 0; i < jPlayer.Count; i++)
		{
			String.Format(@"INSERT INTO [dbo].[Player]([ProfileId],[gsisId],[FirstName],[LastName],[FullName],[BirthDate],[College],[gsisName],[ProfileUrl],[HeightInInches],[WeightInPounds])VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}', {9},{10})",		
			jPlayer[i]["profileId"].ToString(),
			jPlayer[i]["playerId"].ToString(),
			jPlayer[i]["firstName"].ToString(),
			jPlayer[i]["lastName"].ToString(),
			jPlayer[i]["fullName"].ToString(),
			jPlayer[i]["birthDate"].ToString(),
			jPlayer[i]["college"].ToString(),
			jPlayer[i]["gsisName"].ToString(),
			jPlayer[i]["profileUrl"].ToString(),
			jPlayer[i]["height"].ToString(),
			jPlayer[i]["weight"].ToString()
	).Dump();
			
		}
	
	
			
	}
	

	
}

// Define other methods and classes here
