<Query Kind="Program" />

void Main()
{
	DirectoryInfo source = new DirectoryInfo(@"P:\Projects\DataDiscovery\ETL\DataDiscovery\Dashboard.Revised");
	DirectoryInfo destin = new DirectoryInfo(@"P:\Projects\DataDiscovery\ETL\DataDiscovery\Dashboard SQL Scripts");
	var filter = @"StaffInformation";
	var bimls = new DirectoryInfo(@"P:\Projects\DataDiscovery\ETL\DataDiscovery\Dashboard.Revised").GetFiles("*.biml").Select(
		fi => XElement.Load(fi.FullName)).SelectMany(scgm => DirectInput.GetInputs(scgm))
		.Where(pkg => pkg.Package.Equals(filter))		
		//.Dump();
		;
		
		bimls.Dump();
		
//		foreach(var b in bimls)
//		{
//			
//			var fileName = String.Format(@"P:\Projects\DataDiscovery\ETL\DataDiscovery\Dashboard SQL Scripts\{0}\{1}.sql",b.Package,
//				b.ParentName
//					.Replace(" ",String.Empty)
//					.Replace("&","And")				
//					.Replace("\"","_")				
//					.Replace("|","_")				
//				);
//			fileName.Dump();
//			File.AppendAllLines(fileName,new[]{
//				String.Format("/*Package : {0}*/",b.Package),
//				String.Format("/*DataFlow: {0}*/",b.DataFlow),
//				String.Format("/*Task: {0}*/",b.ParentName),
//				String.Format("/*Connection: {0}*/",b.ConnectionName),
//				b.SQLCode
//			});
//			
//			
//		}
	
}

// Define other methods and classes here

public class DirectInput
{
	public String Package {get;set;}
	public String DataFlow {get;set;}
	public String ParentType {get;set;}
	public String ParentName {get;set;}
	public String ConnectionName  {get;set;}
	public String SQLCode {get;set;}
	
	public static IEnumerable<DirectInput> GetInputs(XElement scgm)
	{
		XNamespace biml = scgm.Name.Namespace;
		return scgm.Descendants(biml+"DirectInput").Select(di => (DirectInput)di);
	}
	
	public static explicit operator DirectInput (XElement di)
	{
		XNamespace biml = di.Name.Namespace;
	
		return new DirectInput{
			Package = di.Ancestors(biml+"Package").Attributes("Name").FirstOrDefault().Value,
			DataFlow = di.Ancestors(biml+"Dataflow").Where(a => a.HasAttributes).SelectMany(a => a.Attributes("Name").Select(v => v.Value)).FirstOrDefault(),
			ParentType = di.Parent.Name.LocalName,
			ParentName = di.Parent.Attribute("Name").Value,
			ConnectionName = di.Parent.Attributes().Where(xn => xn.Name.LocalName.Equals("ConnectionName")).Select(v => v.Value).FirstOrDefault(),
			SQLCode = di.Value
			
		};
	}
}