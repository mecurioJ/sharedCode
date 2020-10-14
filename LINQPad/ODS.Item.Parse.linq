<Query Kind="Program" />

void Main()
{
	//var PackageName = @"Course";
	var ETLPath = @"F:\FromDrive\Workspaces\Github\Ed-Fi-Alliance\Ed-Fi-Core\Etl\EdFi.Etl.EdFi\";
	
	var FileItems = new List<EtlItem>();
	
	foreach(var fi in new DirectoryInfo(ETLPath).GetFiles("*.dtsx"))
	{
		FileItems.AddRange(EtlItem.Items(XElement.Load(fi.FullName),fi.Name.Replace(fi.Extension,String.Empty)));
	}
	
	var dtsxItems = FileItems
	.Where(fi => (fi.Task != null))
//	.Where(fi => fi.SourceFile.Equals("StaffAssociation"))
	.Select(fi => new{
	fi.SourceFile,
		fi.Task,
		fi.Item,
		fi.SqlCommand,
		fi.OpenRowset
	})
	//.Where(dst => !String.IsNullOrEmpty(dst.OpenRowset))
	.Select(rr => new{
		rr.SourceFile,
		rr.Task,
		rr.Item,
		rr.OpenRowset,
		SqlCommand = String.IsNullOrEmpty(rr.SqlCommand)
			? String.Empty
			: rr.SqlCommand.Replace("\r"," ").Replace("\n"," ").Replace("  "," ").Replace("  "," "),
		ViewQuery = String.IsNullOrEmpty(rr.OpenRowset)
			? String.Empty
			: String.Format("select * from [DataOutbound].{0}", rr.OpenRowset.Replace("].[","_"))
			
	}).Distinct();
	
	
	dtsxItems//.Where(sf => sf.SourceFile.Equals(PackageName))
	.Select(v => new{
		v.SourceFile,
		Table = v.OpenRowset,
		//ViewQuery = v.ViewQuery.Replace("*","count(*)")
	})	
	.Distinct().Dump();
	
	
}

// Define other methods and classes here

public class EtlItem
{	
	public String SourceFile {get;set;}
	public String Task {get;set;}
	public String Item {get;set;}
	public String InputRef {get;set;}
	public String OutputRef {get;set;}
	public String SqlCommand {get;set;}
	public String SqlCommandParam {get;set;}
	public String OpenRowset {get;set;}
	
	internal static XNamespace DTS = "www.microsoft.com/SqlServer/Dts";
	
	public static IEnumerable<EtlItem> Items (XElement dtsx, string ETLFile)
	{
		return dtsx.Elements(DTS+"Executables").Elements(DTS+"Executable")
	.Select(
		ex => new{
			Variables = ex.Element(DTS+"Variables"),
			ObjectData = ex.Elements().Where(xn => xn.Name.LocalName.Equals("ObjectData")),
			Executables = ex.Elements().Where(xn => xn.Name.LocalName.Equals("Executables")),
			PrecedenceConstraints = ex.Elements().Where(xn => xn.Name.LocalName.Equals("PrecedenceConstraints")),
		}
	)
	.SelectMany(ob => 
		ob.ObjectData
		.Elements("pipeline").Elements("components")
		.Elements("component")
		.Select(cmp => new{
			properties = cmp.Elements("properties").Elements("property").Select(
				att => new{
					Name = att.Attribute("name").Value,
					DataType = att.Attribute("dataType").Value,
					IsExceptionHandler = att.ToString().Contains("EdFiException"),
					att.Value
				}
			),
			inputs = cmp.Elements("inputs").Elements("input").Select(
					input => new{
						RefId = input.Attribute("refId").Value,
						Name = input.Attribute("name").Value,
					}
			),
			outputs = cmp.Element("outputs").Elements("output").Select(
				output => new{
						RefId = output.Attribute("refId").Value,
						Name = output.Attribute("name").Value,
				}
			),
			
		})
		.Where(tt => tt.properties != null)
		.Where(tt => !tt.properties.Where(p => p.IsExceptionHandler).Any())
		.Select(
			summa => new{
				SourceFile = ETLFile.Replace(".dtsx",""),
				Task = summa.inputs.Select(ins => ins.RefId)
					.Where(tt => !tt.Contains("1."))
					.Where(tt => !tt.Contains("2."))
					.Select(tt => tt.Split('\\')).Select(pk => pk[1]).FirstOrDefault(),
				Item = summa.inputs.Select(ins => ins.RefId)
					.Where(tt => !tt.Contains("1."))
					.Where(tt => !tt.Contains("2."))
					.Select(tt => tt.Split('\\')).Select(pk => pk[2].Split('.')[0]).FirstOrDefault(),
				InputRef = summa.inputs.Select(ins => ins.RefId)
					.Where(tt => !tt.Contains("1."))
					.Where(tt => !tt.Contains("2.")).FirstOrDefault(),
				OutputRef = summa.outputs.Select(ons => ons.RefId)
					.Where(tt => !tt.Contains("Error Output"))
					.Where(tt => !tt.Contains("No Match Output")).FirstOrDefault(),
				SqlCommand = summa.properties.Where(nm => nm.Name.Equals("SqlCommand")).Select(sc => sc.Value).FirstOrDefault(),
				SqlCommandParam = summa.properties.Where(nm => nm.Name.Equals("SqlCommandParam")).Select(sc => sc.Value).FirstOrDefault(),
				OpenRowset = summa.properties.Where(nm => nm.Name.Equals("OpenRowset")).Select(sc => sc.Value).FirstOrDefault(),
			}
		)
		).Distinct()
		.Select( ei => new EtlItem(){
			SourceFile = ei.SourceFile,
			Task = ei.Task,
			Item = ei.Item,
			InputRef = ei.InputRef,
			OutputRef = ei.OutputRef,
			SqlCommand = ei.SqlCommand,
			SqlCommandParam = ei.SqlCommandParam,
			OpenRowset =ei.OpenRowset
		});
	}
}