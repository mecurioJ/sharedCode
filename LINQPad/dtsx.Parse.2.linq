<Query Kind="Program" />

void Main()
{
	var ETLPath = @"C:\Users\joey.filichia\source\Workspaces\Business Intelligence\";
	var ODS = @"EdFi\";
	var Dashboard = @"EdFi.Etl.Dashboard\";
	//var ETLFile = "StudentInformation.dtsx";
	//var ETLFile = "StudentIndicator.dtsx";
	//var dtsx = XElement.Load(ETLPath+Dashboard+ETLFile);

	//var DtsId = "{78b79994-4cef-4bf7-af0d-c7be3c0688a8}";

	//var FilterPackage = "StudentParent";

	var PackageList = new DirectoryInfo(ETLPath).GetFiles("*.dtsx", SearchOption.AllDirectories);
	PackageList.Select(fi => new
	{
		Path = fi.FullName,
		fi.Name,
		fi.DirectoryName,
		Body = XElement.Parse(File.ReadAllText(fi.FullName))
		
		})
		.Select(t => new{
			t.Name,
			Encrypted = t.Body.HasElements ? t.Body.Elements().First().Name.LocalName.Equals("EncryptionMethod") : false,
			FirstElement = t.Body.HasElements ? t.Body.Elements().First().Name.LocalName : string.Empty,
			Connections = t.Body.HasElements ? t.Body.Descendants().Where(nm => nm.Name.LocalName.Contains("connections")) : new List<XElement>(),
			//Tasks = t.Body.HasElements ? t.Body.Descendants().Where(nm => nm.Name.LocalName.Contains("tasks")) : new List<XElement>(),
			//SqlCommand = t.Body.HasElements ? t.Body.Descendants().Where(nm => nm.Name.LocalName.Contains("components")).Descendants().SelectMany(a => a.Attributes().Where(xn => xn.Value.Equals("SqlCommand")).Select(p => p.Parent)) : new List<XElement>(),			
			//Components = t.Body.HasElements ? t.Body.Descendants().Where(nm => nm.Name.LocalName.Contains("components")) : new List<XElement>(),
		})
	.Where(e => !e.Encrypted)
	.Where(e => e.Connections.Attributes().Where(x => x.Name.LocalName.Equals("connectionManagerRefId")).Where(x => x.Value.Contains("EDW")).Any())
	//.Where(xe => xe.Elements().Where(xn => xn.Name.LocalName.Equals("EncryptedData")).Any())
	.Dump();
	//var Packages = PackageList.Select(fi => new XElement(File.ReadAllText(fi.FullName))).ToList();
	//var details = Packages.Dump();
//	
//	.Select(tk => new{
//		tk.Name,
//		tk.Connections,
//		TaskItem = tk.Tasks//.Where(dId => dId.DtsId.Equals(DtsId))
//			.Select(ti => new{
//				ti.Description,
//				ti.DtsId,
//				ti.ExecutableType,
//				ti.Name,
//				Components = ti.Components.Where(cp => !cp.Name.Contains("Exception"))
//					.Select(tCmp => new{
//						tCmp.RefId,
//						tCmp.Name,
//						tCmp.TaskDescription,
//						tCmp.TaskType,
//						Properties = tCmp.Properties.Select(tkp => new{
//							tkp.Name,
//							tkp.DataType,
//							tkp.Description,
//							//tkp.TypeConverter,
//							//tkp.ContainsId,
//							tkp.Value,
//							tkp.HasChildren,
//							//tkp.Source
//						}),
//						Inputs = tCmp.Inputs == null
//							? new List<TaskInput>()
//							: tCmp.Inputs,
//						Outputs = tCmp.Outputs.Where(nm => !nm.Name.Contains("Error Output")).Select(tOut => new{
//							tOut.RefId,
//							tOut.IsSorted,
//							tOut.Name,
//							tOut.ExternalMetadataColumns,
//							tOut.OutputColumns
//						}),
//						tCmp.Connections,
//						//tCmp.Children
//					}),
//				Variables = ti.Variables.Elements().Select(e => e.Value),
//				ti.Attributes,
//				ti.Source
//			}),
//		tk.FileName
//	});
//	
//	var Components = 	details.SelectMany(dt => dt.TaskItem.SelectMany(ti => ti.Components));
//	
//	Components.Select(nm => new{nm.Name,nm.Outputs}).Dump();
	

	
	
//	Packages
//	.Select(pk => new{
//		pk.Name,
//		Tasks = pk.Tasks.Select(tk => new{
//			SqlCommands = tk.Components.SelectMany(pr => pr.Properties.Where(nm => nm.Name.Equals("SqlCommand"))).Select(v => v.Value),
//			tk.Attributes
//		})
//	})
	//.Dump();

	
//	var package = new Package( XElement.Load(ETLPath+Dashboard+ETLFile));
	//package.Dump();
	
	
}


public struct TaskItem
{
	public int SortOrder;
	public String Name;
	public String Id;
	
	public TaskItem(int sortOrder, String name, String id)
	{
		SortOrder = sortOrder;
		Name = name;
		Id = id;
	}
}


public class Utilties
{
	public static String ParseAttribute(XElement xa, String attributeName)
	{
		return xa.Attributes(attributeName).Any()
			? xa.Attribute("sortKeyPosition").Value
			: String.Empty;
	}
}

public static XNamespace DTS = "www.microsoft.com/SqlServer/Dts";

public class PackageFile
{
	public PackageFile (){}
	public PackageFile (XElement dtsx)
	{
		Name = 	dtsx.Attribute(DTS+"ObjectName").Value;
		Connections = PackageConnectionManager.PackageConnectionManagers(dtsx);
		Tasks = PackageTask.PackageTasks(dtsx);
		PackageItems = dtsx.Elements();
	}
	
	public PackageFile (String fullName)
	{
		var dtsx = XElement.Load(fullName);
		//Name = 	dtsx.HasElements().Attribute(DTS+"ObjectName").Value;
		Connections = PackageConnectionManager.PackageConnectionManagers(dtsx);
		Tasks = PackageTask.PackageTasks(dtsx);
		FileName = fullName;
		PackageItems = dtsx.Elements();
	}
	
	
	public String Name {get;set;}
	public IEnumerable<PackageConnectionManager> Connections {get;set;}
	public IEnumerable<PackageTask> Tasks {get;set;}
	public String FileName {get;set;}
	public IEnumerable<XElement> PackageItems {get;set;}
}

public class PackageTask
{
	
	public PackageTask(){}
	public PackageTask(XElement ex)
	{
		Description = ex.Attribute(DTS+"Description").Value;
		DtsId= ex.Attribute(DTS+"DTSID").Value;
		Name = ex.Attribute(DTS+"ObjectName").Value;
		ExecutableType= ex.Attribute(DTS+"ExecutableType").Value;
		Attributes = ex.Attributes().Select(an => new{Key = an.Name.LocalName,an.Value});
		Variables = ex.Elements(DTS+"Variables").Any()
					? ex.Elements(DTS+"Variables").FirstOrDefault().Elements()
					: null;
		Components = TaskComponent.TaskComponents(ex);
		Source = ex;
		//ex.Elements(DTS+"ObjectData").Elements("pipeline").Elements("components").Elements().Select(cmp => new TaskComponent(cmp)),
	}
	
	public static IEnumerable<PackageTask> PackageTasks (XElement dtsx)
	{
		return dtsx.Elements(DTS+"Executables").Elements(DTS+"Executable").Select(ex => new PackageTask(ex));
	}
	
	public String Description {get;set;}
	public String DtsId {get;set;}
	public String ExecutableType {get;set;}
	public String Name  {get;set;}
	public IEnumerable<TaskComponent> Components {get;set;}
	public IEnumerable<XElement> Variables {get;set;}
	public dynamic Attributes {get;set;}
	public XElement Source {get;set;}
}

public class TaskComponent
{
	public TaskComponent (){}
	public TaskComponent (XElement cmp)
	{
		RefId = cmp.Attribute("refId").Value;
		Name = cmp.Attribute("name").Value;
		TaskDescription = cmp.Attribute("description").Value;
		TaskType = cmp.Attribute("contactInfo").Value.Split(';')[0];
		Properties = TaskProperty.TaskProperties(cmp);
		Inputs = TaskInput.TaskInputs(cmp);
		Outputs = TaskOutput.TaskOutputs(cmp);
		Connections = TaskConnectionManager.TaskConnectionManagers(cmp);
		Children = cmp.Elements();
	}
	public static IEnumerable<TaskComponent> TaskComponents(XElement ex)
	{
		return ex.Elements(DTS+"ObjectData").Elements("pipeline").Elements("components").Elements().Select(cmp => new TaskComponent(cmp));
	}
	
	public String RefId {get;set;}
	public String Name {get;set;}
	public String TaskDescription {get;set;}
	public String TaskType {get;set;}
	public IEnumerable<TaskProperty> Properties {get;set;}
	public IEnumerable<TaskInput> Inputs {get;set;}
	public IEnumerable<TaskOutput> Outputs {get;set;}
	public IEnumerable<TaskConnectionManager> Connections {get;set;}
	public IEnumerable<XElement> Children {get;set;}
}

public class TaskOutput
{
	public TaskOutput(){}
	
	public TaskOutput(XElement outt)
	{
		RefId = outt.Attribute("refId").Value;
		IsSorted = outt.Attributes("isSorted").Any()
					? outt.Attribute("isSorted").Value
					: String.Empty;
		Name = outt.Attribute("name").Value;
		ExternalMetadataColumns = outt.Descendants("externalMetadataColumns").FirstOrDefault().Elements();
		OutputColumns = TaskOutputColumn.TaskOutputColumns(outt);
	}
	
	public static IEnumerable<TaskOutput> TaskOutputs(XElement cmp)
	{
		return cmp.Elements("outputs").Elements("output").Select(outt => new TaskOutput(outt));
	}
	
	public String RefId {get;set;}
	public String IsSorted {get;set;}
	public String Name {get;set;}
	public IEnumerable<XElement> ExternalMetadataColumns {get;set;}
	public IEnumerable<TaskOutputColumn> OutputColumns {get;set;}
}

public class TaskInput
{
	public TaskInput (){}
	public TaskInput (XElement inpt)
	{
		RefId = inpt.Attribute("refId").Value;
		HasSideEffects = inpt.Attribute("hasSideEffects").Value;
		Name = inpt.Attribute("name").Value;
		InputColumns = TaskInputColumn.TaskInputColumns(inpt);
		ExternalMetadataColumn = inpt.Elements("externalMetadataColumn");
	}
	
	public static IEnumerable<TaskInput> TaskInputs (XElement cmp)
	{
		return cmp.Elements("inputs").Elements("input").Select(inpt => new TaskInput(inpt));
	}
	
	public String RefId {get;set;}
	public String HasSideEffects {get;set;}
	public String Name {get;set;}
	public IEnumerable<TaskInputColumn>  InputColumns {get;set;}
	public IEnumerable<XElement> ExternalMetadataColumn {get;set;}
}

public class TaskInputColumn
{
	public TaskInputColumn(){}
	public TaskInputColumn(XElement inpta)
	{
		RefId = inpta.Attribute("refId").Value;
		CachedDataType = inpta.Attribute("cachedDataType").Value;
		CachedName = inpta.Attributes("cachedName").Any()
					? inpta.Attribute("cachedName").Value
					: String.Empty;
		CachedSortKeyPosition = inpta.Attributes("cachedSortKeyPosition").Any()
					? inpta.Attribute("cachedSortKeyPosition").Value
					: String.Empty;
		LineageId = inpta.Attribute("lineageId").Value;
		CachedLength = inpta.Attributes("cachedLength").Any()
					? inpta.Attribute("cachedLength").Value
					: String.Empty;
	}
	
	public static IEnumerable<TaskInputColumn> TaskInputColumns(XElement inpt)
	{
		return inpt.Elements("inputColumns").Elements("inputColumn").Select(inpta => new TaskInputColumn(inpta));
	}
	
	public String RefId {get;set;}
	public String CachedDataType {get;set;}
	public String CachedName {get;set;}
	public String CachedSortKeyPosition {get;set;}
	public String LineageId {get;set;}
	public String CachedLength {get;set;}
}

public class TaskOutputColumn
{
	public TaskOutputColumn (){}
	public TaskOutputColumn (XElement outta)
	{
		RefId = outta.Attribute("refId").Value;
		DataType = outta.Attribute("dataType").Value;
		Length = outta.Attributes("length").Any()
					? outta.Attribute("length").Value
					: String.Empty;
		LineageId = outta.Attribute("lineageId").Value;
		Name = outta.Attribute("name").Value;
		SortKeyPosition = outta.Attributes("sortKeyPosition").Any()
					? outta.Attribute("sortKeyPosition").Value
					: String.Empty;
	}
	
	public static IEnumerable<TaskOutputColumn> TaskOutputColumns(XElement outt)
	{
		return outt.Elements("outputColumns").Elements("outputColumn").Select(outta => new TaskOutputColumn(outta));
	}
	
	public String RefId {get;set;}
	public String DataType {get;set;}
	public String Length {get;set;}
	public String LineageId {get;set;}
	public String Name {get;set;}
	public String SortKeyPosition {get;set;}
}

public class TaskConnectionManager
{
	public TaskConnectionManager(){}
	public TaskConnectionManager(XElement an)
	{
		RefId =  an.Attributes("refId").Any()
				? an.Attribute("refId").Value
				: String.Empty;
		ConnectionManagerID = an.Attributes("nameconnectionManagerID").Any()
				? an.Attribute("connectionManagerID").Value
				: String.Empty;
		ConnectionManagerRefId = an.Attributes("connectionManagerRefId").Any()
				? an.Attribute("connectionManagerRefId").Value
				: String.Empty;
		Description = an.Attributes("description").Any()
				? an.Attribute("description").Value
				: String.Empty;
		Name = an.Attributes("name").Any()
				? an.Attribute("name").Value
				: String.Empty;
		Source = an;
	}
	
	public static IEnumerable<TaskConnectionManager> TaskConnectionManagers (XElement cmp)
	{
		return cmp.Elements("connections").Elements("connection").Select(an => new TaskConnectionManager(an));
	}
	
	public String RefId {get;set;}
	public String ConnectionManagerID {get;set;}
	public String ConnectionManagerRefId {get;set;}
	public String Description {get;set;}
	public String Name {get;set;}
	public XElement Source {get;set;}
}

public class TaskProperty
{
	public TaskProperty (){}
	public TaskProperty (XElement pr)
	{
		Name = pr.Attributes("name").Any()
				? pr.Attribute("name").Value
				: String.Empty;
		DataType = pr.Attributes("dataType").Any()
				? pr.Attribute("dataType").Value
				: String.Empty;
		Description = pr.Attributes("description").Any()
				? pr.Attribute("description").Value
				: String.Empty;
		TypeConverter = pr.Attributes("typeConverter").Any()
				? pr.Attribute("typeConverter").Value
				: String.Empty;
		ContainsId = pr.Attributes("containsID").Any()
				? pr.Attribute("containsID").Value
				: String.Empty;
		Value = pr.Value ?? String.Empty;
		HasChildren = pr.HasElements;
		Source = pr;
	}
	
	public static IEnumerable<TaskProperty> TaskProperties(XElement cmp)
	{
		return cmp.Elements("properties").Elements("property").Select(pr => new TaskProperty(pr));
	}
	public String Name {get;set;}
	public String DataType {get;set;}
	public String Description {get;set;}
	public String TypeConverter {get;set;}
	public String ContainsId {get;set;}
	public String Value {get;set;}
	public bool HasChildren {get;set;}
	public XElement Source {get;set;}
}

public class PackageConnectionManager
{
	public PackageConnectionManager(){}
	public PackageConnectionManager(XElement cm){
			RefId = cm.Attribute(DTS+"refId").Value;
			CreationName = cm.Attribute(DTS+"CreationName").Value;
			DTSID = cm.Attribute(DTS+"DTSID").Value;
			ObjectName = cm.Attribute(DTS+"ObjectName").Value;
			ConnectionString = cm.Element(DTS+"ObjectData").Element(DTS+"ConnectionManager").Attribute(DTS+"ConnectionString").Value;
	}
	
	
	
	public static IEnumerable<PackageConnectionManager> PackageConnectionManagers(XElement dtsx)
	{
		return dtsx.Elements(DTS+"ConnectionManagers").Elements(DTS+"ConnectionManager").Select( cm => new PackageConnectionManager(cm));
	}
	
	public String RefId {get;set;}
	public String CreationName {get;set;}
	public String DTSID {get;set;}
	public String ObjectName {get;set;}
	public String ConnectionString {get;set;}
}
// Define other methods and classes here