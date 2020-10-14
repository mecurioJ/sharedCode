<Query Kind="Program" />

void Main()
{
	XElement Parameters = XElement.Parse(@"<ParameterValues><ParameterValue><Name>Entity</Name><Value>EM11</Value></ParameterValue><ParameterValue><Name>Subdivision</Name><Value>COTTONWOOD COVE</Value></ParameterValue><ParameterValue><Name>Subdivision</Name><Value>EAGLE CREST </Value></ParameterValue><ParameterValue><Name>Subdivision</Name><Value>INDEPENDENCE TH</Value></ParameterValue><ParameterValue><Name>Subdivision</Name><Value>INDEPENDENCE TOWNHOMES</Value></ParameterValue><ParameterValue><Name>Subdivision</Name><Value>MUSTANG HOLLOW</Value></ParameterValue><ParameterValue><Name>Subdivision</Name><Value>PARRY FARMS </Value></ParameterValue><ParameterValue><Name>Subdivision</Name><Value>VILLAGES AT ROSE CREST (SF)</Value></ParameterValue><ParameterValue><Name>Subdivision</Name><Value>VILLAGES AT ROSECREST</Value></ParameterValue><ParameterValue><Name>Customer</Name><Value>CUT00115</Value></ParameterValue></ParameterValues>");
	
	XElement Settings = XElement.Parse(@"<ParameterValues><ParameterValue><Name>TO</Name><Value>Jfilichia@estoneworks.com</Value></ParameterValue><ParameterValue><Name>ReplyTo</Name><Value>Jfilichia@estoneworks.com</Value></ParameterValue><ParameterValue><Name>IncludeReport</Name><Value>True</Value></ParameterValue><ParameterValue><Name>RenderFormat</Name><Value>EXCELOPENXML</Value></ParameterValue><ParameterValue><Name>Subject</Name><Value>@ReportName was executed at @ExecutionTime</Value></ParameterValue><ParameterValue><Name>IncludeLink</Name><Value>True</Value></ParameterValue><ParameterValue><Name>Priority</Name><Value>NORMAL</Value></ParameterValue></ParameterValues>");
	
	var Subscription = new{
		Settings = Settings.Descendants("ParameterValue").Select(xn => 
			new KeyValuePair<string,string>(xn.Element("Name").Value,xn.Element("Value").Value)).ToDictionary(k=> k.Key, k=> k.Value),
			
		Parameters = Parameters.Descendants("ParameterValue").Select(pn => new ReportParameter()	
		{
		Name = pn.Element("Name").Value,
		Value = pn.Element("Value").Value
		})
	}.Dump();
}

// Define other methods and classes here

class Subscription
{
	public String To {get;set;}
	public String ReplyTo {get;set;}
	public String IncludeReport {get;set;}
	public String RenderFormat {get;set;}
	public String Subject {get;set;}
	public String IncludeLink {get;set;}
	public String Priority {get;set;}
}

struct ReportParameter
{
	public String Name;
	public String Value;
}