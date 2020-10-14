<Query Kind="Program">
  <NuGetReference>LinqToExcel</NuGetReference>
  <Namespace>LinqToExcel</Namespace>
  <Namespace>LinqToExcel.Attributes</Namespace>
  <Namespace>LinqToExcel.Domain</Namespace>
  <Namespace>LinqToExcel.Extensions</Namespace>
  <Namespace>LinqToExcel.Query</Namespace>
</Query>

void Main()
{
	var excel = new ExcelQueryFactory(@"C:\Workspaces\BrightView\BrightViewDBs\Scripts\ETLFromEdfi\Import Ed-Fi Core Types.Data.xls");
	var worksheetStruct = excel.GetWorksheetNames()
		.Select(nm => new{
			Insert = String.Format("INSERT INTO edfi.{0} ({1})",nm, excel.Worksheet(nm).Select(col => col.ColumnNames).First().Aggregate((p,n) => String.Format("{0},{1}",p,n))),
			Data = excel.Worksheet(nm)
			.Select(col => (
				!nm.Equals("IdentificationDocumentUseType")
				? String.Format("SELECT {0},'{1}','{2}' UNION ALL",col[0].Value.ToString(),col[1].Value.ToString().Replace("'","''"),col[2].Value.ToString().Replace("'","''"))
				: String.Format("SELECT {0},'{1}' UNION ALL",col[0].Value.ToString(),col[1].Value.ToString().Replace("'","''"))
				))
		})
		.Select(sql => String.Format("{1}{0}{2}",
			Environment.NewLine,
			sql.Insert,
			sql.Data.ToList().Aggregate((p,n) => String.Format("{1}{0}{2}",Environment.NewLine,p,n))
			.Substring(0,(sql.Data.ToList().Aggregate((p,n) => String.Format("{1}{0}{2}",Environment.NewLine,p,n)).Length - 9)
			)))
		.Dump()
	;
}

// Define other methods and classes here

public class WorksheetMapping
{
	public int Id {get;set;}
	public String CodeValue {get;set;}
	public String Description {get;set;}
}

 