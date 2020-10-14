<Query Kind="Program">
  <Connection>
    <ID>2e945996-a0e3-45cf-bf5c-fcb323818428</ID>
    <Persist>true</Persist>
    <Server>sqlsass</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>mjfilichia</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAEvzdJpREcE2SHJAaAsA5LAAAAAACAAAAAAAQZgAAAAEAACAAAACVWTL+nI9l33p3eZu1olArD7eUZms78xew/kDsXHrGdgAAAAAOgAAAAAIAACAAAABgQH13K/Rql0kaSuvNkoHH1ty7LiXpvelClBhSBm6gRhAAAACZELVvLAOsB7PradG9Dip3QAAAABrdMaSbdVkTcC5GY2xobPZRW8m5NmvxBh1py3i/eyg3iWtl56feCembfvaLx3m+E1pMPw+ahR9OuYxoa90BXPs=</Password>
    <Database>ECSDM_Staging</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{

	TypeDefinitionLookup.Where(tdl => tdl.RelatedType.Contains("tsd"))
	.Select(tsd => new{
		tsd.CodeValue,
		tsd.Description,
		AcademicSubjectTypeId = ParseDescription(tsd.Description)
	}).ToArray()
	//.Select(tsd => String.Format("WHEN '{0}' THEN {1}",tsd.CodeValue.Trim(), tsd.AcademicSubjectTypeId))
	//.Where(ast => ast.AcademicSubjectTypeId == null)
	.Dump();
}

// Define other methods and classes here
public int? ParseDescription(string desc)
{
	int? result = 31;
	
	if(	desc.Contains("Reading")
				|| desc.Contains("READING")
				|| desc.Contains("READ") 
				) {result = 1;}
	if(	desc.Contains("ELA") 
				) {result = 2;}
	if(	desc.Contains("Math")
				|| desc.Contains("MATH")
				|| desc.Contains("Algebra")
				|| desc.Contains("Geometry")
				|| desc.Contains("Alg 2/Trig")
				|| desc.Contains(" MTH")
				) {result = 3;}
	if(	desc.Contains("Writing")
				|| desc.Contains("WRITING") 
				) {result = 4;}			
	if(	desc.Contains(" SS") 
				|| desc.Contains("AMERI HIST & SOC STUDIES") 
				|| desc.Contains("Global Studies") 
				|| desc.Contains("US History&Gov't") 
				|| desc.Contains("US Hist & Gov't") 
				|| desc.Contains("Global History") 
				|| desc.Contains("Global Hist") 
				|| desc.Contains(" Global ") 
				|| desc.Contains("U.S. HISTORY") 
				|| desc.Contains(" US History ") 
				|| desc.Contains("WORLD HISTORY") 
				|| desc.Contains("EUROPEAN HIST & WRLD") 
				
				) {result = 5;}
	if(	desc.Contains("Science")
				|| desc.Contains("Physics")
				|| desc.Contains("PHYSICS")
				|| desc.Contains("Phy Set")
				|| desc.Contains("Chemistry")
				|| desc.Contains("Earth Sci")
				|| desc.Contains("SCI")
				|| desc.Contains("Sci")
				|| desc.Contains("CHEMISTRY")
				|| desc.Contains("BIOLOGY")
				|| desc.Contains("Biology")
				) {result = 6;}				
	if(	desc.Contains(" Eng") 
				|| desc.Contains("-Eng-")
				|| desc.Contains("LITERATURE")
				|| desc.Contains("ENGLISH")
				|| desc.Contains("NYSESLAT")
				|| desc.Contains("NYSITELL")
				) {result = 8;}			
	if(	desc.Contains("Living Environment") 
				) {result = 10;}
	if(	desc.Contains("French")
				|| desc.Contains("German")
				|| desc.Contains("Hebrew")
				|| desc.Contains("Italian")
				|| desc.Contains("Latin")
				|| desc.Contains("LATIN")
				|| desc.Contains("HEBREW")
				|| desc.Contains("FRENCH")
				|| desc.Contains("CHINESE")
				|| desc.Contains("GERMAN")
				|| desc.Contains("ITALIAN")
				|| desc.Contains("JAPANESE")
				|| desc.Contains("KOREAN")
				|| desc.Contains("GREEK")
				|| desc.Contains("Greek")
				|| desc.Contains("Spanish")
				|| desc.Contains("SPANISH")
				) {result = 12;}
	return result;
}