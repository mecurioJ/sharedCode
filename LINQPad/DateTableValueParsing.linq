<Query Kind="Program" />

void Main()
{
	var DateTables = new[]{
	new { TableName = "dim_Date", KeyColumn = "DateKey", Caption = "Date", DataType = "Integer", DisplayColumn = "DateKey", Value = "04-19-2011", Expression = "" },
	new { TableName = "dim_Date", KeyColumn = "Quarter", Caption = "Quarter", DataType = "Integer", DisplayColumn = "Quarter", Value = "2011 - Q1", Expression = "" },
		new { TableName = "dim_Date", KeyColumn = "Year", Caption = "Month", DataType = "Integer", DisplayColumn = "Year", Value = "2011", Expression = "" },
		new { TableName = "dim_Date", KeyColumn = "Month", Caption = "Month", DataType = "Integer", DisplayColumn = "Month", Value = "2011 - April", Expression = "" },
		new { TableName = "dim_Date", KeyColumn = "MonthNameEnglish", Caption = "Month", DataType = "WChar", DisplayColumn = "MonthNameEnglish", Value = "2011 - April", Expression = "CAST(Year AS char(4)) + ' - ' + MonthName" },
	};
	
		List<System.Tuple<string,string,string,string,string,string>> DateTuples = new List<System.Tuple<string,string,string,string,string,string>>();
	foreach(var t in DateTables)
	{
		DateTime ParsedValue;
	
		t.Value.Dump();
		var isParsed = DateTime.TryParse(t.Value,out ParsedValue);
		
		switch(t.KeyColumn)
		{
			case "Year":
			DateTuples.Add(System.Tuple.Create(
				t.TableName,
				"Year",
				"Year",
				"WChar",
				"Year",
				ParsedValue.Year.ToString().Length < 4
				? t.Value
				: ParsedValue.Year.ToString()
				));
			break;
			case "Month":
			DateTuples.Add(System.Tuple.Create(
				t.TableName,
				"Month",
				"Month",
				"Integer",
				"Month",
				ParsedValue.Month.ToString()
				));
			break;
			case "Quarter":
			DateTuples.Add(System.Tuple.Create(
				t.TableName,
				"Quarter",
				"Quarter",
				"Integer",
				"Quarter",
				t.Value.Split('-').Last().TrimStart().Remove(0,1)
				));
			break;
			case "DateKey":
			DateTuples.Add(System.Tuple.Create(
				t.TableName,
				"Year",
				"Year",
				"WChar",
				"Year",
				ParsedValue.Year.ToString()
				));
			DateTuples.Add(System.Tuple.Create(
				t.TableName,
				"Month",
				"Month",
				"Integer",
				"Month",
				ParsedValue.Month.ToString()
				));
			DateTuples.Add(System.Tuple.Create(
				t.TableName,
				"Day",
				"Day",
				"Integer",
				"Day",
				ParsedValue.Day.ToString()
				));
			break;
		};
		
		
	}
	
	DateTuples.Select(st => new
                    {
                        TableName = st.Item1,
                        KeyColumn = st.Item2,
                        Caption = st.Item3,
                        DataType = st.Item4,
                        DisplayColumn = st.Item5,
                        Value = st.Item6,
                        Expression = String.Empty

                    }).Dump();
}

// Define other methods and classes here