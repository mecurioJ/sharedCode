<Query Kind="Program" />

void Main()
{
//	var file1 = new List<KeyValuePair<String,double>>(){
//		new KeyValuePair<String,double>("20130316_180440",GetJulianDate("03/16/2013 18:04:40")),
//		new KeyValuePair<String,double>("20130316_180911",GetJulianDate("03/16/2013 18:09:11")),
//		new KeyValuePair<String,double>("20130316_180923",GetJulianDate("03/16/2013 18:09:23")),
//		new KeyValuePair<String,double>("20130316_181133",GetJulianDate("03/16/2013 18:11:33"))};
//	
//	file1.Dump();
	Math.Round(GetJulianDate(DateTime.Parse("11/17/2014"))).Dump();
//	var JulianDate = GetJulianDate().Dump();
//	var StartTime = DateTime.Parse("03/16/2013 16:00:00");
//	var EndTime = DateTime.Parse("03/17/2013 01:00:00");
//	var TimeSpanPointer = new TimeSpan(EndTime.Subtract(StartTime).Ticks).TotalMinutes;
}

// Define other methods and classes here
public static double GetJulianDate()
{
	return GetJulianDate(DateTime.Now);
}

public static double GetJulianDate(DateTime date)
{
	return date.ToOADate() + 2415018.5;
}

public static double GetJulianDate(String DateLiteral)
{
	return DateTime.Parse(DateLiteral).ToOADate() + 2415018.5;
}