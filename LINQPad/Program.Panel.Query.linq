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
		//pps.Panel.Equals("S500")
		pps.Panel.Contains("S118")
		//pps.DefinitionDescription.Contains("collection")
	)
	.Select( pnl => new{
		pnl.Panel,
		pnl.DefinitionDescription,
		pnl.Location,
		pnl.ElementLength,
		pnl.Description,
		pnl.PromptTitle,
		pnl.PromptFile,
		pnl.PromptRecord,
		pnl.PromptElement,
		pnl.ElementType,
		pnl.RequiredField,
		pnl.LevelNumber,
		pnl.CopyName,
		pnl.PhysicalFile,
		pnl.Record,
		pnl.ExternalFieldName
	})
	.ToArray()
	.GroupBy(gp => gp.Panel)
	.Dump();
}

// Define other methods and classes here