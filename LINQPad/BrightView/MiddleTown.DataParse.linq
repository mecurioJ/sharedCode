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
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
//http://support.microsoft.com/kb/142369
	
	DataDefinitions.Select(dd => new{
		Level = String.Concat(dd.LEVEL,dd.LEVEL2),
		Name = dd.DESCRIPTION,
		PIC = String.Concat(dd.PICTURE,dd.PICTURE2),
		InternalName = String.Concat(dd.COPYNAME,dd.COPYNAME2),
		Record = dd.RCD,
		dd.POSN,
		dd.SEQ,
		dd.REQ,
		dd.EXT,
		dd.TXT,
		dd.PNL,
		dd.LOCN,
		spacer = Convert.ToInt32(String.Concat(dd.LEVEL,dd.LEVEL2))
			})
	.Where(pnl => pnl.PNL.StartsWith("S"))
	.Dump();

}

// Define other methods and classes here
const String PICNum8Bit = "^9\(\d\)";