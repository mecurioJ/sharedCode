<Query Kind="Program">
  <Connection>
    <ID>f82670ce-0e3b-4779-83f3-24ad947d8dd1</ID>
    <Server>.</Server>
    <Database>StateData</Database>
  </Connection>
</Query>

void Main()
{	
var thing = 
	new Attendance()
	.GetType().GetProperties()
		.Select(x => new{
			AccessModifier = "public",
			PropertyType = x.PropertyType.ToString()
				.Replace("System.","")
				.Replace("Nullable`1[Decimal]","Decimal?")
				.Replace("Nullable`1[Boolean]","Boolean?"),
				x.Name,
				Auto = "{get;set;}",
				thinx = x.GetMethod.DeclaringType.ToString().Replace("LINQPad.User.","")
			})
			.Select(sf => string.Format("{3}.{1}, ",
				sf.AccessModifier,
				sf.Name
					.Replace("ENTITY_CD","EntityCd")
					.Replace("GROUP_CODE","GroupCode")
					.Replace("ENTITY_NAME","EntityName")
					.Replace("GROUP_NAME","GroupName")
					.Replace("IsNewYorkCitySchool_District","IsNewYorkCitySchoolDistrict"),
				LowerFirstLetter(sf.Name
					.Replace("ENTITY_CD","EntityCd")
					.Replace("GROUP_CODE","GroupCode")
					.Replace("ENTITY_NAME","EntityName")
					.Replace("GROUP_NAME","GroupName")),
				sf.thinx,
				sf.Auto,
				Environment.NewLine
				)).Aggregate((p,n) => String.Format("{0} {1}",p,n))
				.Dump();
	
}

// Define other methods and classes here

public String LowerFirstLetter(string value)
{
	return String.Format("{0}{1}",value.Substring(0,1).ToLower(),value.Substring(1,value.Length-1));
}