<Query Kind="Program">
  <Connection>
    <ID>0632ee6f-09a5-46b6-a92c-d018e80c48ad</ID>
    <Server>den-sql01</Server>
    <Database>JIM</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	String FolderPath = @"K:\RDLs\";
	String ReportPath = @"zzz-CSAP PDF Proof of Concept.rdl";
	XNamespace rdl = "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition";
	XElement xe = XElement.Load(FolderPath+ReportPath);
	xe.Elements().Where(xn => xn.Name.LocalName.Equals("DataSets"))
		//.Select(xn => xn.Name.LocalName).ToList()
		.Dump();
}

// Define other methods and classes here
