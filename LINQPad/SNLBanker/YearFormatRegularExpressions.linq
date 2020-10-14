<Query Kind="Program" />

void Main()
{
	var strQuarter = "2009 - Q2";
	var strMonth = "2009 - June";
	var strYear = "2009";
	var strDate = "06-15-2009";
	
	var QuarterPattern = @"\d{2,4}\s*-\s*Q\d";
	var MonthPattern =  @"\d{2,4}\s*-\s*\w{3,10}";
	var YearPattern = @"\d{4}\Z";
	var DatePattern = @"\d{2}\s*-\s*\d{2}\s*-\s*\d{2,4}";
	
	
	var matches = Regex.Match(strMonth,YearPattern).Success.Dump();
	
	DateTime.Parse(strDate).Day.Dump();
}

// Define other methods and classes here
