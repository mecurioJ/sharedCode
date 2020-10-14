<Query Kind="Program">
  <Connection>
    <ID>fb112041-3f78-4cec-8506-30cb177ff82a</ID>
    <Persist>true</Persist>
    <Server>bvaserver</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>mjfilichia</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA6CZ+pDyS6k64q01uTaqaNgAAAAACAAAAAAAQZgAAAAEAACAAAAA/wi/zetbMyBcbTDQcJa/gchVwzrv9FC7aCpfPJFeukQAAAAAOgAAAAAIAACAAAADwUHL4QemeWAFnHnWc5Nl4dq34Y1ZQIlhv1Q7XNezy3hAAAACHfed97lYQTFtsyLoxXCdwQAAAAMtENCa7nAZ60KBSi+Ndh0DX85mnTro+1cyXsLtwOGeaWIdJVwwWeKf3e2DoAa5dBIoNpjtd8jcQPhHqou/2DyI=</Password>
    <Database>PowerSchoolStage</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference Relative="Libs\Laan.Sql\Laan.Sql.Parser.dll">C:\Users\mjfil\Dropbox\LinqPad\Libs\Laan.Sql\Laan.Sql.Parser.dll</Reference>
  <Namespace>Laan.Sql.Parser</Namespace>
  <Namespace>Laan.Sql.Parser.Entities</Namespace>
  <Namespace>Laan.Sql.Parser.Exceptions</Namespace>
  <Namespace>Laan.Sql.Parser.Expressions</Namespace>
  <Namespace>Laan.Sql.Parser.Parsers</Namespace>
</Query>

/*
SELECT             # Start with a select
    (          # Parentheses in a regex mean "put (capture) the stuff 
               #     in between into the Groups array" 
       [^)]    # Any character that is not a ')' character
       *       # Zero or more occurrences of the aforementioned "non ')' char"
    )          # Close the capturing group
;             # "Ends with a ';' character"
		
		*/
		
void Main()
{
	var filterName = "TN_EDFI_DISCIPLINE_BODY";

	var extracts = new DirectoryInfo(@"C:\Workspaces\PowerSchool\Reporting\Extracts\").GetFiles().Where(fil => fil.Name.ToLower().Contains("body"))
	.Where(fil => !fil.Name.Equals("046_extract_body.sql"))
	.Select(itm => new{
		Name = itm.Name.Replace(itm.Extension,String.Empty),
		Queries = Regex.Matches(File.ReadAllText(itm.FullName), @"SELECT ([^;]*);").Cast<Match>().Select(v => v.Value),
		XmlQueries = Regex.Matches(File.ReadAllText(itm.FullName), @"SELECT XML([^;]*);").Cast<Match>().Select(v => v.Value),
		//Tables = GetPossibleTables(itm.FullName)
	})
	.Where(fltr => (
		fltr.Name.Equals("TN_EDFI_TRANSCRIPT_BODY")
		//|| 
//		fltr.Name.Equals("TN_EDFI_GRADE_BODY")
//		|| fltr.Name.Equals("TN_EDFI_GRADEBOOK_BODY")
//		|| fltr.Name.Equals("TN_EDFI_TRANSCRIPT_BODY")
//		|| fltr.Name.Equals("AZ_EDFI_TRANSCRIPT_BODY")
		))
	.Dump();
	
	
	
}

// Define other methods and classes here

public static IEnumerable<String> GetPossibleTables(string filename)
{
	return File.ReadAllLines(filename)
	.Where(li => (li.Contains("FROM") || li.Contains("JOIN")))
	.Where(li => !li.ToLower().Contains("cst_"))
	.Select(la => la
		.Replace("AND EXISTS (SELECT 1 ",String.Empty)
		.Replace("AND NOT EXISTS (SELECT 1 ",String.Empty)
		.Replace("OR EXISTS (SELECT 1 ",String.Empty)
		.Replace("OR NOT EXISTS (SELECT 1 ",String.Empty)
		.Replace("FROM ",String.Empty)
		.Replace("LEFT JOIN ",String.Empty)
		.Replace("JOIN ",String.Empty)
		.Replace("( SELECT 1 ",String.Empty)
		.Replace(",",string.Empty)
		.Trim().ToLower()
		).Where(fi => !fi.ToLower().Equals("from") )
		.Where(fi => !fi.ToLower().Equals("dual"))
		.Where(fi => !fi.ToLower().Equals("dual;"))
		.Where(fi => !fi.ToLower().StartsWith("dual"))
		.Where(fi => !fi.ToLower().StartsWith("select"))
		.Where(fi => !fi.ToLower().StartsWith("(select"))
		.Where(fi => !fi.ToLower().StartsWith("table("))
		//.Select(t => t.Split(' ')[0])
		.Distinct();
}