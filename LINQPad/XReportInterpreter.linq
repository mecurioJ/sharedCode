<Query Kind="Program" />

void Main()
{

	XNamespace ssrs = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices"; 
	XNamespace rDef = "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition"; 
	XNamespace rDsgn = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner"; 
	XNamespace ssas = "http://schemas.microsoft.com/AnalysisServices/QueryDefinition"; 

	String xDirectory = @"C:\Projects\JATSIC\InfoPortal Tools\";
	String xReports = @"xReports.xml";
	
	var xReportItems = XElement.Load(xDirectory+xReports)
		.Elements("ReportItem")
		//Children
		.Select(ri => new{
			Name = ri.Element("Name").Value,
			Definition = ri.Element("Definition").Elements(rDef+"Report")
				.Select(rd => new{
					DataSources = rd.Elements(rDef+"DataSources").SelectMany(dsr => dsr.Descendants().Select(dsi => new{dsi.Name.LocalName,dsi.Value}).ToList()),
					DataSets = rd.Descendants(rDef+"DataSet"),
					Body = rd.Elements(rDef+"Body"),
					ReportParameters = rd.Elements(rDef+"ReportParameters"),
					items = rd.Elements().Where(rn => rn.Name.Namespace.Equals(rDef) 
						&& !rn.Name.LocalName.Equals("DataSources")
						&& !rn.Name.LocalName.Equals("DataSets")
						&& !rn.Name.LocalName.Equals("Body")
						&& !rn.Name.LocalName.Equals("ReportParameters")
						&& !rn.Name.LocalName.Equals("Width")
						&& !rn.Name.LocalName.Equals("Page")
						)
				})
		})
	//.SelectMany(re => re.Elements())
		//.Where(rnm => rnm.Name.Equals("Name") || rnm.Name.Equals("Definition"))
	.Dump();
	
	
	
}

// Define other methods and classes here
