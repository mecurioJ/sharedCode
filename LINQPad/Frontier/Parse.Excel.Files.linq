<Query Kind="Program">
  <NuGetReference>ExcelDataReader</NuGetReference>
  <NuGetReference>ExcelDataReader.DataSet</NuGetReference>
  <Namespace>ExcelDataReader</Namespace>
  <Namespace>ExcelDataReader.Core.NumberFormat</Namespace>
  <Namespace>ExcelDataReader.Exceptions</Namespace>
  <Namespace>ExcelDataReader.Log</Namespace>
  <Namespace>ExcelDataReader.Log.Logger</Namespace>
  <Namespace>System.IO.Compression</Namespace>
</Query>

ExcelDataSetConfiguration dsConfig = new ExcelDataSetConfiguration
{
	UseColumnDataType = true,
	FilterSheet = (r, index) => r.VisibleState == ((index > 27) ? "hidden" : "visible"),
	ConfigureDataTable = _ => new ExcelDataTableConfiguration
	{
		UseHeaderRow = true
	}
};

void Main()
{
	var path = @"D:\Projects\Reporting Standards\Reporting Standards 5.25.2020.xlsx";
	//must have all 28 tables
	//must have all columns per table
	List<KeyValuePair<String,DataSet>> FileContents = new List<KeyValuePair<String,DataSet>>();
	
	
	

	foreach (FileInfo file in new DirectoryInfo(@"D:\Projects\Frontier\Allied\").GetFiles("*.xls"))
	{

		//build the DataSet
		var workbookDataSet = ExcelReaderFactory.CreateReader(File.Open(file.FullName, FileMode.Open, FileAccess.Read)).AsDataSet(dsConfig);
		
		//rename the tables and columns:
		foreach (DataTable table in workbookDataSet.Tables)
		{
			table.TableName = ToPascalCase(table.TableName);
			foreach (DataColumn column in table.Columns)
			{
				column.ColumnName = ToPascalCase(column.ColumnName.ToLower());
			}
		}
		
		
		FileContents.Add(new KeyValuePair<String,DataSet>(file.Name,workbookDataSet));
	}
	
//	FileContents.Select(dt => dt.Value.Tables.Cast<DataTable>().Select(
//		drs => drs.Rows.Cast<DataRow>().Select(
//			dr => new {
//				CustNo = dr["CustNo"].ToString()
//			}
//			)
//	)).Dump();

	FileContents.SelectMany(ct => ct.Value.Tables.Cast<DataTable>().Select(tb => new {
		//FileName = ct.Key,
		TableName = tb.TableName,
		ColumnNames = tb.Columns.Cast<DataColumn>().Select(col => $@"Output0Buffer.{col.ColumnName} = dr[""{col.ColumnName}""].ToString();")//.Aggregate((p, n) => $"{p},{n}"),
		//FileData = tb.Rows.Cast<DataRow>().Select(fd => fd.ItemArray.Select((r, i) => new {col = i, Length = r.ToString().Length} ))
	})).First().ColumnNames
	.Dump();
	
	
}
public class TableContents
{
	public String TableName {get;set;}
	public String ColumnNames {get;set;}
}



public static string ToPascalCase(string the_string)
{
	// If there are 0 or 1 characters, just return the string.
	if (the_string == null) return the_string;
	if (the_string.Length < 2) return the_string.ToUpper();

	// Split the string into words.
	string[] words = the_string.Split(
		new char[] { },
		StringSplitOptions.RemoveEmptyEntries);

	// Combine the words.
	string result = "";
	foreach (string word in words)
	{
		result +=
			word.Substring(0, 1).ToUpper() +
			word.Substring(1);
	}

	return
								Regex.Replace(
								Regex.Replace(
									Regex.Replace(result
		.Replace("(",string.Empty)
		.Replace(")", string.Empty)
		.Replace("-", string.Empty)
										.Replace("%", "pct")
                                    ,"<=","LT")
                                , ">=", "GT")
                                , @"[^A-Za-z|0-9|\s]", string.Empty)
								.Replace(" ", string.Empty)
		;
}