<Query Kind="Program" />

void Main()
{
	XNamespace rdl = "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdesigner";
	XNamespace rDef = "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition";
	XNamespace qDef = "http://schemas.microsoft.com/AnalysisServices/QueryDefinition";
	var ReportsLocation = @"F:\Projects\JATSIC\InfoPortal Tools\RDLs";
	var ReportName = @"Graph Widget - Student Absence by Reasons.rdl";
	
	var rdls = new DirectoryInfo(ReportsLocation).GetFiles("*.rdl",SearchOption.AllDirectories)
	//.Where(fi => fi.Name.Contains("Student Attendance"))
	.Select(fi => new{
		fileName = fi.Name,
		rdlContent = XDocument.Load(fi.FullName).Descendants(rDef+"DataSet").Select(x => 
		new{
			CommandType = x.Descendants().Where(xn => xn.Name.LocalName.Equals("CommandType"))
			.Where(ct => ct.Value.Equals("MDX")).Any()
			,Name = x.Attribute("Name").Value,
			CommandText = x.Elements(rDef+"Query").Select(e => e.Element(rDef+"CommandText").Value).FirstOrDefault()
		}).Where(ct => ct.CommandType)
	})
	//.SelectMany(w => w.rdlContent.Select(r => r.CommandText))
	.Distinct()
	.Where(s => s.rdlContent.Where(ct => ct.CommandType).Any())
	//.Where(w => w.rdlContent.Select(txt => txt.CommandText.Contains("[Student]")).Any())
	.Dump();
	
	
}

// Define other methods and classes here
