<Query Kind="Statements" />

var DimensionsTuples = new List<String[]> {new[]{"Customers","[Customers]","[Customers].[Customer Names].[Customer Name].&[15823]"},
new[]{"Accounts","[Accounts]","[Accounts].[Accounts].[All Accounts]"},
new[]{"Calendar Period","[Calendar Period]","[Calendar Period].[Calendar Period].[Date].&[142]"},
new[]{"Products","[Products]","[Products].[Products].[Product Type].&[CDs]"},
new[]{"Products","[Products]","[Products].[Products].[Product Type].&[Deposits]"},};

var MemberDefinition = "[Products].[Products].[Product Family].&[10]&[Deposits]";
var MemberParentDefinition = "[Products].[Products].[Product Type].&[Deposits]";

//
var measureUniqueName = "[Measures].[Current Balance]";


var ExtractText = @"[\]?&\.\[]";
var SplitValue = @"[\[{&}\]\.]";
var MeasureParse = @"\].\[";


Regex.Split(measureUniqueName.Substring(1,measureUniqueName.Length-2),MeasureParse).Last().Dump();

String[] memberProperties = Regex.Split(MemberDefinition,@"\.&");
String[] memberValues = memberProperties[1].Replace("[",String.Empty).Replace("]",String.Empty).Split('&');
String[] memberParent = Regex.Split(MemberParentDefinition,@"\.&");

if(memberValues.Count() > 1)
{
	memberParent.Dump();
}