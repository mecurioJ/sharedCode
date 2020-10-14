<Query Kind="Program">
  <Connection>
    <ID>edc58089-4bf4-4236-a966-50366b182d8d</ID>
    <Persist>true</Persist>
    <Server>BVAServer</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>mjfilichia</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA1VarxQc6hk+Tu0JSfzcrxwAAAAACAAAAAAAQZgAAAAEAACAAAAA6lPggwnOrAL71K0HsUgX0NkgJrDKRoF7IEgdc/uz75QAAAAAOgAAAAAIAACAAAADuRhx7lBaAwjLke+g7gE22pXz1689CneIo5c7BfItIqRAAAABMkYLzaaBt0xU1JIkfvfL/QAAAALYnMGUnszGe1BSQszVd9+jq+FnWEJH3F9wOfDDQGYFtz9L/IjJqm5AIFJp8YUPhgvys4HWH3hOs3XbfEmZKl+E=</Password>
    <Database>MiddletownDataDefinitions</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	var fileName = @"p:\MiddletownDataDefinitionObjects.csv";
	var fileLines = new List<RowItem>();
	
	using(StreamReader sr = new StreamReader(fileName))
	{
		String line;
		while((line = sr.ReadLine())!= null)
		{
			fileLines.Add(new RowItem(line));
		}
	}
	
	fileLines.RemoveAt(0);
	
	
	
	fileLines
	.Select(li => new{
		li.Key,
		li.POSN,
		li.SEQ,
		li.LEVEL,
		li.SuggestedType,
		li.DESCRIPTION,
		li.RCD,
		/*
		li.COPY,
		li.File,li.RCD,
		li.PICTURE,li.REQ,li.EXT,li.TXT,li.UPDATE,
		li.PRT,
		*/
		li.PNL,
		li.LOCN
	})
	.Where(ff => ff.LEVEL.Contains("01"))	
	//.Where(ff => ff.DESCRIPTION.Contains("Assessment")).Dump();
	//.Where(li => li.Key.Equals("ABS_ABS"))
	.Select(d => new{d.RCD,d.DESCRIPTION}).Distinct()
	.Dump();
	
	
	
	
	
	
}

// Define other methods and classes here
public class RowItem
{
	
	
	private String String = @"X\(\d+\)";
	private String SignedFloat = @"^S9\(\d+\)V9*";
	private String UnSignedFloat = @"9\(\d+\)V9*";
	private String SignedNumeric = @"^S9\(\d+\)[^v9]";
	private String UnSignedNumeric = @"^9\(\d+\)";
	private String ArrayList = @"\bOCCURS";
	private String Redefine = @"\bREDEFINE";
	private String RedefineVariant = @"\bREDF";

	public RowItem (String fileLines)
	{
		Key = String.Format("{0}_{1}"
			, String.IsNullOrEmpty(fileLines.Substring(4,5))
			? fileLines.Substring(4,5)
			: fileLines.Substring(30,16).Substring(0,3)
			,String.IsNullOrEmpty(fileLines.Substring(9,4))
			? fileLines.Substring(9,4)
			: fileLines.Substring(30,16).Substring(0,3)
		);
		
		File = String.IsNullOrEmpty(fileLines.Substring(4,5))
			? fileLines.Substring(4,5)
			: fileLines.Substring(30,16).Substring(0,3);
			
		RCD = String.IsNullOrEmpty(fileLines.Substring(9,4))
			? fileLines.Substring(9,4)
			: fileLines.Substring(30,16).Substring(0,3);
			
		POSN = fileLines.Substring(13,5);
		SEQ = fileLines.Substring(18,4);
		LEVEL = fileLines.Substring(22,8);
		COPY = fileLines.Substring(30,16);
		PICTURE = fileLines.Substring(46,16);
		SuggestedType = Regex.Match(fileLines.Substring(46,16),String).Success
			? typeof(System.String).ToString()
			: 	Regex.Match(fileLines.Substring(46,16),SignedNumeric).Success
				? typeof(System.Double).ToString()
				: Regex.Match(fileLines.Substring(46,16),SignedFloat).Success || Regex.Match(fileLines.Substring(46,16),UnSignedFloat).Success
					? typeof(System.Decimal).ToString()
					: Regex.Match(fileLines.Substring(46,16),UnSignedNumeric).Success
						? typeof(System.UInt32).ToString()
			 			: Regex.Match(fileLines.Substring(46,16),ArrayList).Success
							? typeof(ArrayList).ToString()
							: typeof(System.Object).ToString();
		DESCRIPTION = fileLines.Substring(62,31);
		REQ = fileLines.Substring(93,4);
		EXT = fileLines.Substring(97,4);
		TXT = fileLines.Substring(101,4);
		PNL = fileLines.Substring(105,5);
		LOCN = fileLines.Substring(110,5);
		PRT = fileLines.Substring(115,4);
		UPDATE = fileLines.Substring(119);
	}
	public String Key {get;set;}
	public String File {get;set;}
	public String RCD {get;set;}
	public String POSN {get;set;}
	public String SEQ {get;set;}
	public String LEVEL {get;set;}
	public String COPY {get;set;}
	public String PICTURE {get;set;}
	public String SuggestedType {get;set;}
	public String DESCRIPTION {get;set;}
	public String REQ {get;set;}
	public String EXT {get;set;}
	public String TXT {get;set;}
	public String PNL {get;set;}
	public String LOCN {get;set;}
	public String PRT {get;set;}
	public String UPDATE {get;set;}
}