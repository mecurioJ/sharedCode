<Query Kind="Program" />

void Main()
{
	var sourceSet = new[]{
	"[Measures].[Current Balance]",
	"[Products].[Products].[Product Name]",
	"[Customers].[Customer Names].[Customer Name]",
	"[Organization].[Organization Region].[Branch]",
	"[Accounts].[Accounts]",
	"[Measures].[Note Rate x Balance]",
	"[Risk Rating].[Risk Rating].[All Risk Rating]",
	"[Customers].[Class].[Class Name].&[V]",
	"[Customers].[Officer Portfolio].[Officer Name]",
	"[Products].[Products].[Product Family]"
	};
	
	sourceSet.Where(s => s.Contains("&")).Dump();
}

// Define other methods and classes here
