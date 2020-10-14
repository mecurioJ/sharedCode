<Query Kind="Program">
  <Connection>
    <ID>1e5485fa-aac2-4b53-8481-ce2675973975</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>SNLBanker_SampleDW</Database>
  </Connection>
  <Reference Relative="..\..\SkyDrive\Assemblies\SchemaParser\bin\Release\SchemaParser.dll">C:\Users\joeyf\SkyDrive\Assemblies\SchemaParser\bin\Release\SchemaParser.dll</Reference>
  <GACReference>Microsoft.AnalysisServices.AdomdClient, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>filichiacom</Namespace>
  <Namespace>Microsoft.AnalysisServices.AdomdClient</Namespace>
  <Namespace>MoreLinq</Namespace>
  <Namespace>SchemaParser = filichiacom.SchemaParser</Namespace>
</Query>

void Main()
{
	var schemaDir = @"C:\SNLBanker\Banker Support\CBLayout";
	var xmlaSchema = 
	XElement.Load(schemaDir+@"\BankingCustomerIntelligence_JHLIVE.xmla");
	
	
	xmlaSchema.Dump();
	
	//var OlapData = new filichiacom.SchemaParser.oDataBase(xmlaSchema);
	
	//xmlaSchema.Dump();
}
// Define other methods and classes here
