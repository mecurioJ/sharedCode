<Query Kind="Program" />

void Main()
{
	var tbr = XElement.Load(@"E:\OurHouse\Visual 7\VISUAL_NET72_Build_18_8_508_886\MSI\SourceDir\Reports\Ledger\FinancialReportTrialBal1.rpx");
	tbr.Elements("Sections").Elements("Section")
	.Descendants("Control")
	.Select(ctl => new{
		Type = ctl.Attribute("Type").Value,
		DataField = (ctl.Attribute("DataField") != null) ? ctl.Attribute("DataField").Value : String.Empty,
		Caption =  (ctl.Attribute("Caption") != null) ? ctl.Attribute("Caption").Value : String.Empty,
		Text =  (ctl.Attribute("Text") != null) ? ctl.Attribute("Text").Value : String.Empty,
		OutputFormat =  (ctl.Attribute("OutputFormat") != null) ? ctl.Attribute("OutputFormat").Value : String.Empty,
		Attribs = ctl.Attributes().Select(at => new{Name = at.Name.LocalName, at.Value})
		
	})
	
	.Dump();
	
}

// Define other methods and classes here
