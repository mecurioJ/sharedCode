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
	AHLPPs.Where(aa => aa
	.HLPPCOPY.Contains("TYPE")
	//.HLPPFILE.Contains("scr")
	//.HLPDESC.Contains("transcr")
	//.HLPKPANEL.Equals("g103")
	//g103
	//s521
	)
	.Select(hlp => new{
		District= hlp.HLPKDIST,
		Panel= hlp.HLPKPANEL,
		Location= hlp.HLPKLOCN,
		Description= hlp.HLPDESC,
		
		ElementType= hlp.HLPTYPE,
		ElementLength= hlp.HLPLENG,
		
		RequiredField= hlp.HLPREQ,
		LevelNumber= hlp.HLPPLVL,
		CopyName= hlp.HLPPCOPY,
		Picture= hlp.HLPPPIC,
		PhysicalFile= hlp.HLPPFILE,
		Record= hlp.HLPPRCD,
		
		PromptProgram= hlp.HLPPRMPR,
		PromptFile= hlp.HLPPRMF,
		PromptRecord= hlp.HLPPRMR,
		PromptElement= hlp.HLPPRME,
		PromptTitle= hlp.HLPPRMT,
		
		ExternalFieldName= hlp.HLPEXNM,
		ExternalRequired= hlp.HLPEXRQ,
		ExternalReferenceDocument= hlp.HLPEXRF,
		Protect= hlp.HLPPROT,
		TextAttached= hlp.HLPTXT,
	})
	.Dump();
	//ProgramPanelSearch.Where(pps => pps.Panel.Equals("g116")).Dump();
}

// Define other methods and classes here