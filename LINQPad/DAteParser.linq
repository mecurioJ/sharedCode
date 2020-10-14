<Query Kind="Program" />

void Main()
{
	var datePattern = "([0-9]{2}[A-Za-z]{2,4}[0-9]{2})|([0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-)";
	
	var test1 = "COMBINED_IROP_REPORT_31Mar20.csv";
	var test2 ="2020-02-05-07-00-alertRaisedCountByAlertName-All Ports.csv";
	
	var date1 = Regex.Match(test1, datePattern).Value;
	var date2 = Regex.Match(test2, datePattern).Value;
	
	GetFileDate(date1).ToString().Dump();
	GetFileDate(date2).Dump();

	Regex.Replace(
					Regex.Replace(
						Regex.Replace(
							test2,
							"[0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-",
							string.Empty
						),
						"-All Ports.csv",
						string.Empty
					),
					@"(for\s|_)[0-9]{2}[A-Za-z]{2,4}[0-9]{2}.csv",
					string.Empty
				).ToString().Trim().Dump();


	var dateToCheck = "2020-02-23-07-00-";
	GetFileDate(dateToCheck).Dump();
	GetFileDate("25May20").Dump();
}

// Define other methods, classes and namespaces here
public DateTime GetFileDate(string dateToCheck)
{
	DateTime result = DateTime.MinValue;
	if (Regex.Match(dateToCheck, "[0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-").Success)
	{
		var sourceDate = dateToCheck.Substring(0, 10);
		var sourceTime = dateToCheck.Substring(11, 5);
		result = DateTime.Parse($"{sourceDate} {sourceTime.Replace("-", ":")}:00");
	} else if(Regex.Match(dateToCheck,"[0-9]{2}[A-Za-z]{2,4}[0-9]{2}").Success)
	{
		result = DateTime.Parse(dateToCheck);
	}
	
	return result;
}