<Query Kind="Program" />

void Main()
{
	var Today = DateTime.Today;
	var FirstDayPriorMonth = DateTime.Parse("2014-03-01 00:00:00.000");
	var LastDayPriorMonth = DateTime.Parse("2014-03-31 00:00:00.000");
	var FirstDayPriorQuarter = DateTime.Parse("2014-01-01 00:00:00.000");
	var LastDayPriorQuarter = DateTime.Parse("2014-03-31 00:00:00.000");
	var FirstDayCurrentMonth = DateTime.Parse("2014-04-01 00:00:00.000");
	var LastDayCurrentMonth = DateTime.Parse("2014-04-30 23:59:59.997");
	//
	
	string.Format("{0}-{1}.xlsx",LastDayPriorMonth.Year,LastDayPriorMonth.Month).Dump();
	
	//Quarters don't match = -1
	//Quarters Match = 0
	Int32.Parse("2").CompareTo(Int32.Parse("2")).Dump();
	
	//are we past the last day of the quarter?
	//1 earlier
	//0 Same Day
	//-1 later
	LastDayPriorQuarter.CompareTo(Today).Dump();
	
	
	
	
}

// Define other methods and classes here
