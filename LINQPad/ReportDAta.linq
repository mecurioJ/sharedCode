<Query Kind="Program" />

void Main()
{
	XDocument reports = XDocument.Load(@"c:\projects\jatsic\infoportal Tools\xReports.xml");
	
	reports.Descendants("ReportItem").Dump();
}

// Define other methods and classes here
