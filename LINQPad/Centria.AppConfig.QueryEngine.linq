<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
	var Directory = @"B:\Projects\Plus\Centria\PDAPWPF\PDAPWPF\";
	var FileName = @"App.Config";
	
	var config = XElement.Load(Directory+FileName);
	
!String.IsNullOrEmpty(tl.RevealValue) ? float.Parse(tl.RevealValue) : 0f

	
	config.Descendants("PanelAttributeTruthKeys")
	.Elements("add")
//	.SelectMany(a => a.Attributes().Select(xn => xn.Name.LocalName)).Distinct()
	.Select(ti => new{
		key = ti.Attribute("Key").Value
	
	})
	.Dump();
	
	
}

// Define other methods and classes here