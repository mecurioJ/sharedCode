<Query Kind="Program" />

void Main()
{
	var ETLPath = @"C:\Users\joey.filichia\source\Projects\EDW\f9azwcsqlbi03\";
	
	XNamespace DTS = "www.microsoft.com/SqlServer/Dts";
	XNamespace SqlTask = "www.microsoft.com/sqlserver/dts/tasks/sqltask";
	var dirI = new DirectoryInfo(ETLPath).GetFiles("*.dtsx").SelectMany(xf => XElement.Parse(File.ReadAllText(xf.FullName))
		.Elements().Where(tt => tt.Descendants(SqlTask + "SqlTaskData").Count() > 1).Select(dts => new
		{
			FileName = xf.FullName,			
			Executables = dts.Parent.Elements(DTS + "Executables").Elements(DTS + "Executable").Descendants(SqlTask + "SqlTaskData").Attributes(SqlTask + "SqlStatementSource").Select(v => 
			new {
				Command = v.Value,
				IsStoredProc = v.Parent.Attributes().Where(an => an.Name.LocalName.Contains("IsStoredProc")).FirstOrDefault(),
				ConnectionString = dts.Parent.Elements(DTS + "ConnectionManagers").Elements(DTS + "ConnectionManager")
					.Where(at => at.Attribute(DTS + "DTSID").Value.Equals(v.Parent.Attribute(SqlTask + "Connection").Value)).Descendants(DTS + "ConnectionManager").FirstOrDefault().Attribute(DTS + "ConnectionString").Value
				//.Where(ttx => ttx.Attribute(DTS + "DTSID").Value.Equals(v.Parent.Attribute(SqlTask+"Connection").Value))
			})	
			
		}).Where(t => t.Executables.Any(cm => cm.ConnectionString.Contains("Initial Catalog=EDW")))
	)
	.SelectMany(t => t.Executables.Select(sp => new {
		t.FileName,
		sp.Command,		
		IsProc = sp.IsStoredProc == null
			? String.Empty
			: sp.IsStoredProc.Value,
		sp.ConnectionString
	}))
	.Dump();


//	var dtsx = XElement.Load(ETLPath + ETLFile);
//	dtsx.Elements().Select(dts => new{
//		Connections = dts.Parent.Elements(DTS + "ConnectionManagers").Elements(DTS + "ConnectionManager").Elements(DTS + "ObjectData"),
//		Executables = dts.Parent.Elements(DTS + "Executables").Elements(DTS + "Executable")
//	}).Dump();
//	
//	dtsx.Elements(DTS + "Executables").Elements(DTS + "Executable").Dump();
//	dtsx.Elements(DTS + "ConnectionManagers").Elements(DTS + "ConnectionManager").Elements(DTS + "ObjectData").Dump();
//	dtsx.Elements().Dump();
//	dtsx.Elements(DTS+"Executables")
//	.Elements(DTS+"Executable").Select(ex => new{
//		//Attributes = ex.Attributes().Select(a => new{a.Name.LocalName,a.Value}).ToDictionary(d => d.LocalName,d => d.Value),
//		//Variables = ex.Element(DTS+"Variables"),
//		PipeLine = ex.Elements(DTS+"ObjectData").Elements("pipeline")
//			.Elements("components")
//				.Elements("component").Select(cmp => new{
//					Attributes = cmp.Attributes().Select(a => new{a.Name.LocalName,a.Value} as dynamic),
//					Properties = cmp.Elements("properties").Elements("property").Select(p => new{
//						Attributes = p.Attributes().Select(a => new{a.Name.LocalName,a.Value}).ToDictionary(d => d.LocalName,d => d.Value),
//						Value = p.Value
//					}),
//					Inputs = cmp.Elements("inputs").Elements("input").Select(inp => new{
//						Attributes = inp.Attributes().Select(a => new{a.Name.LocalName,a.Value}as dynamic),
//						InputColumns = inp.Elements("inputColumns").Select(inc => new{
//							Attributes = inc.Attributes().Select(a => new{a.Name.LocalName,a.Value}).ToDictionary(d => d.LocalName,d => d.Value),
//							Elements = inc.Elements()
//						}),
//						//Elements = inp.Elements()
//					}),
//					Outputs =  cmp.Elements("outputs").Elements("output").Select(inp => new{
//						Attributes = inp.Attributes().Select(a => new{a.Name.LocalName,a.Value}).ToDictionary(d => d.LocalName,d => d.Value),
//						Elements = inp.Elements()
//					}),
//				
//				})
//	})
//	.Dump();
	
	//dtsx.Dump();
	
	//var Connections = ConnectionManager.ConnectionManagers(dtsx.Element(DTS+"ConnectionManagers")).Dump();
//	var ExecutionOrder = EventOrder.OrderedPrecedent(dtsx.Elements().Where(xn => xn.Name.LocalName.Equals("PrecedenceConstraints")).First())
//	.Select(eo => new{
////		eo.ObjectName,
////		eo.Order,
//		From = eo.From.Replace(" ",String.Empty).Replace("Package\\",String.Empty),
//		To = eo.To.Replace(" ",String.Empty).Replace("Package\\",String.Empty)
//		})
//	.Dump();
	
//	var Variables = dtsx.Element(DTS+"Variables");
//	var Tasks = dtsx.Element(DTS+"Executables");
	
	
	//dtsx.Element(DTS+"Property").Dump();
	//dtsx.Element(DTS+"DesignTimeProperties").Dump();
//	
//	.Elements(DTS+"Executable").Select(li => new{
//		Variables = li.Element(DTS+"Variables"),
//		//LoggingOptions = li.Element(DTS+"LoggingOptions"),
//		SQLTask = li.Element(DTS+"ObjectData").Elements().Where(xn => !xn.Name.LocalName.Equals("pipeline")).Select(att => new KeyValuePair<String,String>(att.Name.LocalName, att.Value)),
//		PipelineTask = li.Element(DTS+"ObjectData").Elements().Where(xn => xn.Name.LocalName.Equals("pipeline"))
//			.Select(p => new{
//				Components = p.Elements("components").Elements("component").Select(t1 => new{
//					Attributes = t1.Attributes().Select(att => new KeyValuePair<String,String>(att.Name.LocalName, att.Value)),
//					Children = t1.Elements().Count()
//				}),
////				Paths = p.Elements("paths").Elements("path").Select(a => 
////				new{
////					refId = a.Attribute("refId").Value,
////					name =  a.Attribute("name").Value,
////					startId =  a.Attribute("startId").Value,
////					endId =  a.Attribute("endId").Value,
////					})
//			})
//	}).Dump();
	
	//Executable
	//components
	//paths
}

// Define other methods and classes here



public class EventOrder
{
	private static readonly XNamespace DTS = "www.microsoft.com/SqlServer/Dts";
	public EventOrder(){}
	public EventOrder(XElement at)
	{
		refId = at.Attribute(DTS+"refId").Value;
		CreationName = at.Attribute(DTS+"CreationName").Value;
		DTSID = new Guid(at.Attribute(DTS+"DTSID").Value);
		From = at.Attribute(DTS+"From").Value;
		LogicalAnd = at.Attribute(DTS+"LogicalAnd").Value;
		ObjectName = at.Attribute(DTS+"ObjectName").Value;
		Order = String.IsNullOrEmpty(at.Attribute(DTS+"ObjectName").Value.Replace("Constraint",String.Empty).Trim())
				? 0
				: Convert.ToInt32(at.Attribute(DTS+"ObjectName").Value.Replace("Constraint",String.Empty).Trim());
		To = at.Attribute(DTS+"To").Value;
	}
	
	public static List<EventOrder> OrderedPrecedent (XElement eo)
	{
		return eo.Elements(DTS+"PrecedenceConstraint").Select(at => new EventOrder(at)).OrderBy(o => o.Order).ToList();
	}
	public String refId { get; set; }
	public String CreationName { get; set; }
	public Guid DTSID { get; set; }
	public String From { get; set; }
	public String LogicalAnd { get; set; }
	public String ObjectName { get; set; }
	public int Order { get; set; }
	public String To { get; set; }
}

public class ConnectionManager
{
	private static XNamespace DTS = "www.microsoft.com/SqlServer/Dts";
	public ConnectionManager(){}
	public ConnectionManager(XElement cnMgr)
	{
		refId = cnMgr.Attribute(DTS+"refId").Value;
		CreationName = cnMgr.Attribute(DTS+"CreationName").Value;
		DelayValidation = cnMgr.Attribute(DTS+"DelayValidation").Value;
		DTSID = cnMgr.Attribute(DTS+"DTSID").Value;
		ObjectName = cnMgr.Attribute(DTS+"ObjectName").Value;
		ConnectionString = cnMgr.Element(DTS+"ObjectData").Element(DTS+"ConnectionManager").Attribute(DTS+"ConnectionString").Value;
		Attributes = cnMgr.Attributes().Select(xAtt => new KeyValuePair<String,String>(xAtt.Name.LocalName,xAtt.Value)).ToList();
		NestedItems = cnMgr.Element(DTS+"ObjectData").Elements(DTS+"ConnectionManager").Select(xAtt => new KeyValuePair<String,String>(xAtt.Name.LocalName,xAtt.Value)).ToList();
		
	}
	
	public static List<ConnectionManager> ConnectionManagers (XElement ConnMgr)
	{
		return ConnMgr.Elements(DTS+"ConnectionManager").Select(cnMgr => new ConnectionManager(cnMgr)).ToList();
	}
	

	public String refId {get;set;}
	public String CreationName {get;set;}
	public String DelayValidation {get;set;}
	public String DTSID {get;set;}
	public String ObjectName {get;set;}
	public String ConnectionString {get;set;}
	public List<KeyValuePair<String,String>> Attributes {get;set;}
	public List<KeyValuePair<String,String>> NestedItems {get;set;}
	
}