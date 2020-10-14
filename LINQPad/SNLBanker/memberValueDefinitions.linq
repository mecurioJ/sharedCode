<Query Kind="Program" />

void Main()
{
	var memberValueDefinitions = new[]{
new {
		DimensionUniqueName = "Customers",
		HierarchyUniqueName = "Customer Geography",
		MemberCaption = "Boulder",
		MemberKey = String.Empty,
		MemberName = "Boulder",
		MemberUniqueName = "[Customers].[Customer Geography].[City].&[CO]&[Boulder]&[Boulder]",
		ParentUniqueName = "[Customers].[Customer Geography].[County].&[CO]&[Boulder]",
		},
		new {
		DimensionUniqueName = "Customers",
		HierarchyUniqueName = "Customer Geography",
		MemberCaption = "CO",
		MemberKey = "CO",
		MemberName = "CO",
		MemberUniqueName = "[Customers].[Customer Geography].[State].&[CO]",
		ParentUniqueName = "[Customers].[Customer Geography].[All Customers]",
		},
		new {
		DimensionUniqueName = "Customers",
		HierarchyUniqueName = "Customer Geography",
		MemberCaption = "Boulder",
		MemberKey = String.Empty,
		MemberName = "Boulder",
		MemberUniqueName = "[Customers].[Customer Geography].[County].&[CO]&[Boulder]",
		ParentUniqueName = "[Customers].[Customer Geography].[State].&[CO]",
		}
};
memberValueDefinitions.Where(nm => !String.IsNullOrEmpty(nm.MemberUniqueName)).Select(nm => {
	var findValue = nm.MemberUniqueName.IndexOf(".&");
	var UniqueName = nm.MemberUniqueName.Substring(0,findValue);
	return UniqueName;
	
}).Dump();
}

// Define other methods and classes here
