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
TypeDefinitionLookup
//.Where(tdl => new[]{"CA","GY"}.Contains(tdl.CodeValue))
//STGSGRTYP
.Where(tdl => tdl.RelatedType.Equals("ART")) //DLC //ART
//.Where(tdl => tdl.CodeValue.Contains("10018"))

	.Select(sch => new{
		sch.ApplSchoolYear,
		sch.RelatedType,
		sch.CodeValue,
		sch.Description,
		sch.ExternalDescription,
		sch.ExternalValue,
		sch.VariableContent
	}).Distinct()
	.GroupBy(gg => gg.RelatedType)
	.Dump();
//	TypeDefinitionLookup
//	.Select(sch => new{
//		sch.ApplSchoolYear,
//		sch.CodeValue,
//		sch.Description,
//		sch.ExternalDescription,
//		sch.ExternalValue
//		
//	}).Distinct()
//	.Dump();
}

// Define other methods and classes here