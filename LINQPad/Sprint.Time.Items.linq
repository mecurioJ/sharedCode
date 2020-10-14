<Query Kind="Program">
  <Connection>
    <ID>57b8e721-62f2-4f6a-88ff-6c40878b38a8</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>SNLBanker_ApplicationDB</Database>
  </Connection>
</Query>

void Main()
{
	
	
	DateTime StartOfSprint = DateTime.Parse("07/24/2013");
	DateTime EndOfSprint = DateTime.Parse("08/17/2013");
	
	int WorkingDays = 0;
	int VacationDays = 1;
	int LengthofDays = 6;
	int PreviousAllocHours = 31;
	int calendarDays = EndOfSprint.Subtract(StartOfSprint).Days;
	
	for(var i = StartOfSprint; i < EndOfSprint; i = i.AddDays(1))
	{
		
		if (!((int)i.DayOfWeek == (int) DayOfWeek.Saturday || (int)i.DayOfWeek == (int) DayOfWeek.Sunday))
			WorkingDays++;
	}
	
	(((WorkingDays-VacationDays) * LengthofDays) - PreviousAllocHours).Dump();
	
	
}

// Define other methods and classes here
