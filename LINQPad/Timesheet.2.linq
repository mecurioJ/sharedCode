<Query Kind="Program">
  <Connection>
    <ID>f268594a-d7ea-477c-8f43-f7e6c4adfcdc</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>Consulting</Database>
  </Connection>
</Query>

void Main()
{
	Timesheets.Where(e => e.EntryDate.Value > DateTime.Parse("10/06/2014"))
	.ToLookup(d => d.EntryDate.Value.ToShortDateString(), d=> new{hours = d.EndTime.Value.Subtract(d.StartTime.Value), d.TaskDetails, d.TaskItem, d.TFSId, d.OnTimeId})
	.Select(k => new{k.Key, tHOur = k.Select(t => new{t.hours,t.OnTimeId})})
	
	.Dump();
}

// Define other methods and classes here