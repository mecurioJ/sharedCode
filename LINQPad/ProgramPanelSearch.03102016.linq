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
	ProgramPanelSearch
	.Where(pps => 
//		pps.DefinitionDescription.Contains("Calendar")
//		pps.Description.Contains("Calendar")
//		pps.PromptRecord.Contains("SUS")
		//pps.PanelDescription.Contains("grade")
		//pps.PromptFile.Contains("pkg")
		//pps.CopyName.Contains("ktype")
		pps.Panel.Equals("S176") //s203 //s306 //s313 //s316 //s491
	)
	.GroupBy(pnl => pnl.Panel)
	.Dump();
}

// Define other methods and classes here