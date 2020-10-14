<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>


IEnumerable<String> filterList = new[]{"[Occupancy].[Occupancy Codes].&[1]","[Department].[Department Codes].&[5]","[Credit Line].[Credit Line Code].&[3]"};
IEnumerable<String> columnList = new[]{"[Organization].[Organization].[Charter].&[CHARTER WEST]","[Measures].[Current Balance]"};
IEnumerable<String> rowList = new[]{"[Risk Rating].[Risk Rating].[Risk Group].&[Negative]"};


filterList.Union(columnList.Union(rowList))
//Exclude any items that do not have a set value for expression
.Where(li => li.Contains("&"))
//Break out items into Member and Value
.Select(li => Regex.Split(li,".&"))
.Select(sp => new{
	Member = sp[0].ToString()
					.Replace("[",String.Empty)
					.Replace("]",String.Empty).Split('.'), 
	Value = sp[1].ToString()
					.Replace("[",String.Empty)
					.Replace("]",String.Empty)})
//Split filter Values into their constiutent components					
.Select(mem => new{
	Dimension = mem.Member[0],
	Hierarchy = mem.Member[1],
	Level = (mem.Member.Count() > 2) ? mem.Member[2].ToString() : String.Empty ,
	mem.Value
})					
.Dump();