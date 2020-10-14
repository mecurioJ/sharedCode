<Query Kind="Program" />

void Main()
{	
	(
		new Movie{
			Name="Captain America",
			StartTime= DateTime.Parse("04/05/2014 10:20 AM"),
			Duration="2:08"
			}
	).Dump();
}

// Define other methods and classes here
class Movie
{
	public String Name {get;set;}
	public DateTime StartTime {get;set;}
	public String Duration {get;set;}
	public DateTime EndTime { get{ return  StartTime.AddMinutes(15).Add(TimeSpan.Parse(Duration)); }}
}