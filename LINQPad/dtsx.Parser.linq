<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Attunity.SqlServer.CDCControlTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.AnalysisServices.AppLocal.Core.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.AnalysisServices.AppLocal.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.AnalysisServices.AppLocal.Tabular.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.ASTasks.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.BulkInsertTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.DataProfilingTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.DmQueryTask.dll</Reference>
  <Reference Relative="ManagedDTS\Microsoft.SqlServer.DTSPipelineWrap.dll">C:\Users\joey.filichia\source\GitHub\Linqpad\ManagedDTS\Microsoft.SqlServer.DTSPipelineWrap.dll</Reference>
  <Reference>C:\Windows\Microsoft.NET\assembly\GAC_32\Microsoft.SqlServer.DTSRuntimeWrap\v4.0_14.0.0.0__89845dcd8080cc91\Microsoft.SqlServer.DTSRuntimeWrap.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.ExecProcTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.ExpressionTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.FileSystemTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.FtpTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.IntegrationService.HadoopTasks.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.MaintenancePlanTasks.dll</Reference>
  <Reference>C:\Windows\Microsoft.NET\assembly\GAC_MSIL\Microsoft.SqlServer.ManagedDTS\v4.0_14.0.0.0__89845dcd8080cc91\Microsoft.SqlServer.ManagedDTS.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.Management.CollectorTasks.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.MSMQTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.ScriptTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.SendMailTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.SQLTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.TransferDatabasesTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.TransferErrorMessagesTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.TransferJobsTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.TransferLoginsTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.TransferObjectsTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.TransferSqlServerObjectsTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.TransferStoredProceduresTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.WebServiceTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.WMIDRTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.WMIEWTask.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\140\DTS\Tasks\Microsoft.SqlServer.XmlTask.dll</Reference>
  <GACReference>Microsoft.SqlServer.ExecPackageTaskWrap, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>Microsoft.SqlServer.Dts.Pipeline.Wrapper</Namespace>
  <Namespace>Microsoft.SqlServer.Dts.Runtime</Namespace>
  <Namespace>Microsoft.SqlServer.Dts.Runtime.Wrapper</Namespace>
  <Namespace>Microsoft.SqlServer.Dts.Tasks.ExecutePackageTask</Namespace>
  <Namespace>Microsoft.SqlServer.Dts.Tasks.ExecuteProcess</Namespace>
  <Namespace>Microsoft.SqlServer.Dts.Tasks.ScriptTask</Namespace>
</Query>

void Main()
{
	var etlPath = @"P:\Projects\DataDiscovery\ETL\EdFi.Etl.Dashboard\";
	var etlExtension = ".dtsx";
	
	var Packages = XElement.Load(@"C:\Users\joey.filichia\source\repos\OND\Files\OND PNR Sampling q3.dtsx")
//	new DirectoryInfo(etlPath).GetFiles()
//	.Where(et => et.Extension.Equals(etlExtension))
//	.Where(et => et.Name.Contains("SchoolCourseGradeMetrics"))
//	.Select(fi => XElement.Load(fi.FullName))
	.Descendants().Where(xn => xn.Name.LocalName.Equals("component"))
	.Select(
		cmp => new{
			Namespace = cmp.Parent.Name.Namespace,
			RefId = cmp.Attributes("refId").FirstOrDefault().Value,			
			Name = cmp.Attributes("name").FirstOrDefault().Value,
			ComponentClass = cmp.Attributes("componentClassID").FirstOrDefault().Value,
			Inputs = cmp.Elements("inputs").Elements().Select(
				ts => new
				{
					refId = ts.Attributes().Where(xn => xn.Name.LocalName.Equals("refId")).Select(t => t.Value).FirstOrDefault(),
					name = ts.Attributes().Where(xn => xn.Name.LocalName.Equals("name")).Select(t => t.Value).FirstOrDefault(),
					description = ts.Attributes().Where(xn => xn.Name.LocalName.Equals("description")).Select(t => t.Value).FirstOrDefault(),
					errorOrTruncationOperation = ts.Attributes().Where(xn => xn.Name.LocalName.Equals("errorOrTruncationOperation")).Select(t => t.Value).FirstOrDefault(),
					errorRowDisposition = ts.Attributes().Where(xn => xn.Name.LocalName.Equals("errorRowDisposition")).Select(t => t.Value).FirstOrDefault(),
					hasSideEffects = ts.Attributes().Where(xn => xn.Name.LocalName.Equals("hasSideEffects")).Select(t => t.Value).FirstOrDefault(),
					hasInputColumns = ts.Element("inputColumns").Elements().Any(),
					hasExternalMetaDataColumns = ts.Element("externalMetadataColumns").Elements().Any(),
					ExternalMetaDataColumns = ts.Element("externalMetadataColumns").Elements().Select(
						col => new
						{
							refId = col.Attributes().Where(xn => xn.Name.LocalName.Equals("refId")).Select(t => t.Value).FirstOrDefault(),
							DataType = col.Attributes().Where(xn => xn.Name.LocalName.Equals("dataType")).Select(t => t.Value).FirstOrDefault(),
							length = col.Attributes().Where(xn => xn.Name.LocalName.Equals("length")).Select(t => t.Value).FirstOrDefault(),
							precision = col.Attributes().Where(xn => xn.Name.LocalName.Equals("precision")).Select(t => t.Value).FirstOrDefault(),
							name = col.Attributes().Where(xn => xn.Name.LocalName.Equals("name")).Select(t => t.Value).FirstOrDefault()
						}
					),
					Columns = ts.Element("inputColumns").Elements().Select(
						col => new {
							refId = col.Attributes().Where(xn => xn.Name.LocalName.Equals("refId")).Select(t => t.Value).FirstOrDefault(),
							cachedDataType = col.Attributes().Where(xn => xn.Name.LocalName.Equals("cachedDataType")).Select(t => t.Value).FirstOrDefault(),
							cachedName = col.Attributes().Where(xn => xn.Name.LocalName.Equals("cachedName")).Select(t => t.Value).FirstOrDefault(),
							cachedPrecision = col.Attributes().Where(xn => xn.Name.LocalName.Equals("cachedPrecision")).Select(t => t.Value).FirstOrDefault(),
							lineageId = col.Attributes().Where(xn => xn.Name.LocalName.Equals("lineageId")).Select(t => t.Value).FirstOrDefault()
						}
					)
				}
			),
			Outputs = cmp.Elements("outputs").Elements(),
			Connections = cmp.Elements("connections"),
			Properties = cmp.Elements("properties").Select(
				ptp => ptp.Elements()
			),
		}
	)
	.Dump();
	
//	
//	//Packages.Count().Dump();
//	
//	Packages
//	.FirstOrDefault()
//	.Executables
//	//.Cast<Microsoft.SqlServer.Dts.Runtime.TaskHost>()
//	.Cast<Executable>()
////	.Where( t => t.GetType().ToString().Equals("Microsoft.SqlServer.Dts.Runtime.TaskHost"))
////	.Select(t => (Microsoft.SqlServer.Dts.Runtime.TaskHost) t )
//	.Select( th => new{
//		th.Name,
//		th.Description,
//		th.CreationName, 
//		pipeline = ((MainPipe)th.InnerObject).ComponentMetaDataCollection.Cast<IDTSComponentMetaData100>()
//			.Select(md => new{
//				Name = md.Name,
//				OutputCollection = md.OutputCollection.Cast<IDTSOutput100>().SelectMany(oc => OutputColumn.GetOutputColumns(oc)),
//				InputCollection = md.InputCollection.Cast<IDTSInput100>().SelectMany(ic => InputColumn.GetInputColumns(ic))
//			}),
//		th.Properties	
//	})
//	
//	.Dump();
}

public class PackageItem
{
	public PackageItem () {}
	
	public PackageItem (String PackageBody)
	{
		Microsoft.SqlServer.Dts.Runtime.Package pkg = new Microsoft.SqlServer.Dts.Runtime.Package();
		pkg.LoadFromXML(PackageBody,null);
		
		Name = pkg.Name;
		ConnectionManagers = pkg.Connections.Cast<ConnectionManager>();
		PrecedenceConstraints = pkg.PrecedenceConstraints.Cast<Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint>();
		Tasks = pkg.Executables.Cast<Microsoft.SqlServer.Dts.Runtime.TaskHost>().Select(th => new Task(th));
	}
	
	public PackageItem (Microsoft.SqlServer.Dts.Runtime.Package pkg)
	{
		Name = pkg.Name;
		ConnectionManagers = pkg.Connections.Cast<ConnectionManager>();
		PrecedenceConstraints = pkg.PrecedenceConstraints.Cast<Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint>();
		Tasks = pkg.Executables.Cast<Microsoft.SqlServer.Dts.Runtime.TaskHost>().Select(th => new Task(th));
	}
	public String Name {get;set;}
	public IEnumerable<ConnectionManager> ConnectionManagers {get;set;}
	public IEnumerable<Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint> PrecedenceConstraints {get;set;}
	public IEnumerable<Task> Tasks {get;set;}
}

public class Task
{
	public Task () {}
	public Task (Microsoft.SqlServer.Dts.Runtime.TaskHost th) 
	{
		Id = th.ID ?? th.Name;
		Name = th.Name;
		Identifier = th.CreationName;
		Type = th.Description;
		Items = new TaskItems{
				pipeLine = th.CreationName.Equals("SSIS.Pipeline.4")
						? ((MainPipe)th.InnerObject).ComponentMetaDataCollection.Cast<IDTSComponentMetaData100>().Select(md => new PipeLineItem(md))
						: null,
				SQLTask  = th.CreationName.Contains("ExecuteSQLTask")
				? ((Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask.ExecuteSQLTask)th.InnerObject)
				: null
			};
	}
	public String Id {get;set;}
	public String Name {get;set;}
	public String Identifier {get;set;}
	public String Type {get;set;}
	public TaskItems Items {get;set;}
}

public class TaskItems
{
	public IEnumerable<PipeLineItem> pipeLine {get;set;}
	public Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask.ExecuteSQLTask SQLTask {get;set;}
}

public class PipeLineItem
{
	//https://technet.microsoft.com/en-us/library/microsoft.sqlserver.dts.pipeline.wrapper.idtscomponentmetadata100(v=sql.120).aspx
	public PipeLineItem () {}
	public PipeLineItem (IDTSComponentMetaData100 md)
	{
		AreInputColumnsValid = md.AreInputColumnsValid;
		Description = md.Description;
		ID =md.ID;
		Name = md.Name;
		ComponentType= Component(md.ComponentClassID).Elements().Where(xn => xn.Name.LocalName.Equals("CreationName")).First().Value;
		IdentificationString = md.IdentificationString;
		UsesDispositions = md.UsesDispositions;
		CustomPropertyCollection = md.CustomPropertyCollection.Cast<IDTSCustomProperty100>().Select(cpc => new TaskCustomProperty(cpc));
		InputCollection = md.InputCollection.Cast<IDTSInput100>().SelectMany(ic => InputColumn.GetInputColumns(ic));
		OutputCollection = md.OutputCollection.Cast<IDTSOutput100>().SelectMany(oc => OutputColumn.GetOutputColumns(oc));
		RuntimeConnectionCollection = md.RuntimeConnectionCollection.Cast<IDTSRuntimeConnection100>().Select(rcc => new RuntimeConnection(rcc));		
	}
	
	public bool AreInputColumnsValid {get;set;}
	public String Description {get;set;}
	public int ID {get;set;}
	public String Name {get;set;}
	public String ComponentType {get;set;}
	public String IdentificationString {get;set;}
	public bool UsesDispositions {get;set;}
	public IEnumerable<TaskCustomProperty> CustomPropertyCollection {get;set;}
	public IEnumerable<InputColumn> InputCollection {get;set;}
	public IEnumerable<OutputColumn> OutputCollection {get;set;}
	public IEnumerable<RuntimeConnection> RuntimeConnectionCollection {get;set;}	
}


public class InputColumn
{
	public InputColumn (){}
	public InputColumn (string collectionName, string collectionType, IDTSInputColumn100 icc)
	{
		CollectionName  =  collectionName;
		Name  =  icc.Name;
		IdentificationString  =  icc.IdentificationString;
		MappedColumnID  =  icc.MappedColumnID;
		//DataType  =  icc.DataType.ToString();
		Length  = icc.Length ;
		Precision  =  icc.Precision;
		Scale  =  icc.Scale;
		LineageID  =  icc.LineageID;
		CodePage  = icc.CodePage ;
		ComparisonFlags  = icc.ComparisonFlags ;
		CustomPropertyCollection  = icc.CustomPropertyCollection.Cast<IDTSCustomProperty100>().Select(cpc => new ColumnCustomProperty(cpc)) ;
		Description  = icc.Description ;
		ErrorOrTruncationOperation  = icc.ErrorOrTruncationOperation ;
		ErrorRowDisposition  = icc.ErrorRowDisposition.ToString() ;
		ExternalMetadataColumnID  = icc.ExternalMetadataColumnID ;
		IsAssociatedWithOutputColumn  = icc.IsAssociatedWithOutputColumn ;
		IsValid  = icc.IsValid ;
		SortKeyPosition  = icc.SortKeyPosition ;
		TruncationRowDisposition  = icc.TruncationRowDisposition.ToString() ;
		UpstreamComponentName  = icc.UpstreamComponentName ;
		UsageType  = icc.UsageType.ToString() ;
		CollectionType  = collectionType ;
	}
	public static IEnumerable<InputColumn> GetInputColumns(IDTSInput100 ic)
	{
		return ic.InputColumnCollection.Cast<IDTSInputColumn100>().Select(icc => new InputColumn(ic.Name,ic.ObjectType.ToString(),icc));
	}
	
	public String CollectionName {get;set;}
	public String Name {get;set;}
	public String IdentificationString {get;set;}
	public int MappedColumnID {get;set;}
	public String DataType {get;set;}
	public int Length {get;set;}
	public int Precision {get;set;}
	public int Scale {get;set;}
	public int LineageID {get;set;}
	public int CodePage {get;set;}
	public int ComparisonFlags {get;set;}
	public IEnumerable<ColumnCustomProperty> CustomPropertyCollection {get;set;}
	public String Description {get;set;}
	public String ErrorOrTruncationOperation {get;set;}
	public String ErrorRowDisposition {get;set;}
	public int ExternalMetadataColumnID {get;set;}
	public bool IsAssociatedWithOutputColumn {get;set;}
	public bool IsValid {get;set;}
	public int SortKeyPosition {get;set;}
	public String TruncationRowDisposition {get;set;}
	public String UpstreamComponentName {get;set;}
	public String UsageType {get;set;}
	public String CollectionType {get;set;}
}

public class OutputColumn
{
	//https://technet.microsoft.com/en-us/library/microsoft.sqlserver.dts.pipeline.wrapper.idtsoutputcollection100(v=sql.120).aspx
	//https://msdn.microsoft.com/en-us/library/microsoft.sqlserver.dts.pipeline.wrapper.idtsoutput100%28v=sql.120%29.aspx
	
	public OutputColumn () {}
	public OutputColumn (String collectionName, String collectionType, IDTSOutputColumn100 occ) 
	{
		CollectionName =  collectionName;
		Name =  occ.Name;
		IdentificationString =  occ.IdentificationString;
		MappedColumnID =  occ.MappedColumnID;
		DataType = occ.DataType.ToString() ;
		Length =  occ.Length;
		Precision =  occ.Precision;
		Scale =  occ.Scale;
		LineageID = occ.LineageID ;
		CodePage =  occ.CodePage;
		ComparisonFlags =  occ.ComparisonFlags;
		CustomPropertyCollection =  occ.CustomPropertyCollection.Cast<IDTSCustomProperty100>().Select(cpc => new ColumnCustomProperty(cpc));
		Description = occ.Description ;
		ErrorOrTruncationOperation = occ.ErrorOrTruncationOperation ;
		ErrorRowDisposition = occ.ErrorRowDisposition.ToString() ;
		ExternalMetadataColumnID = occ.ExternalMetadataColumnID ;
		ObjectType =  occ.ObjectType.ToString();
		SortKeyPosition =  occ.SortKeyPosition;
		SpecialFlags = occ.SpecialFlags;
		TruncationRowDisposition =  occ.TruncationRowDisposition.ToString();
		CollectionType =  collectionType;
	}
	
	public static IEnumerable<OutputColumn> GetOutputColumns(IDTSOutput100 oc)
	{
		return oc.OutputColumnCollection.Cast<IDTSOutputColumn100>().Select(occ => new OutputColumn(oc.Name,oc.ObjectType.ToString(),occ));
	}
	public String CollectionName {get;set;}
	public String Name {get;set;}
	public String IdentificationString {get;set;}
	public int MappedColumnID {get;set;}
	public String DataType {get;set;}
	public int Length {get;set;}
	public int Precision {get;set;}
	public int Scale {get;set;}
	public int LineageID {get;set;}
	public int CodePage {get;set;}
	public int ComparisonFlags {get;set;}
	public IEnumerable<ColumnCustomProperty> CustomPropertyCollection {get;set;}
	public String Description {get;set;}
	public String ErrorOrTruncationOperation {get;set;}
	public String ErrorRowDisposition {get;set;}
	public int ExternalMetadataColumnID {get;set;}
	public String ObjectType {get;set;}
	public int SortKeyPosition {get;set;}
	public int SpecialFlags {get;set;}
	public String TruncationRowDisposition {get;set;}
	public String CollectionType {get;set;}
	
}

public class TaskCustomProperty
{
	//https://technet.microsoft.com/en-us/library/microsoft.sqlserver.dts.pipeline.wrapper.idtscustomproperty100(v=sql.120).aspx
	public TaskCustomProperty () {}
	public TaskCustomProperty (IDTSCustomProperty100 cpc) 
	{
		Name  = cpc.Name ;
		Description = cpc.Description;
		IdentificationString  =  cpc.IdentificationString;
		ExpressionType = cpc.ExpressionType.ToString();
		TypeConverter  = cpc.TypeConverter ;
		ContainsID = cpc.ContainsID;
		EncryptionRequired = cpc.EncryptionRequired;
		ID  =  cpc.ID;
		ObjectType  = ((DTSObjectType)cpc.ObjectType).ToString();
		State  = cpc.State.ToString() ;
		UITypeEditor  = cpc.UITypeEditor ;
		PropertyValue = cpc.Value ;
	}
	
	public int ID  {get;set;}
	public String Name  {get;set;}
	public String Description {get;set;}
	public String IdentificationString  {get;set;}
	public String TypeConverter  {get;set;}
	public String ExpressionType {get;set;}
	public dynamic PropertyValue {get;set;}
	public String ObjectType  {get;set;}
	public bool ContainsID {get;set;}
	public bool EncryptionRequired {get;set;}
	public String State  {get;set;}
	public String UITypeEditor  {get;set;}
	
}

public class ColumnCustomProperty
{
	public ColumnCustomProperty(){}
	public ColumnCustomProperty(IDTSCustomProperty100 cpc)
	{
		Name  = cpc.Name ;
		Description = cpc.Description;
		IdentificationString  =  cpc.IdentificationString;
		ExpressionType = cpc.ExpressionType.ToString();
		TypeConverter  = cpc.TypeConverter ;
		ContainsID = cpc.ContainsID;
		EncryptionRequired = cpc.EncryptionRequired;
		ID  =  cpc.ID;
		ObjectType  = cpc.ObjectType.ToString() ;
		State  = cpc.State.ToString() ;
		UITypeEditor  = cpc.UITypeEditor ;
		Value = cpc.Value ;
	}
	public bool ContainsID {get;set;}
	public String Description {get;set;}
	public bool EncryptionRequired {get;set;}
	public String ExpressionType {get;set;}
	public int ID  {get;set;}
	public String IdentificationString  {get;set;}
	public String Name  {get;set;}
	public String ObjectType  {get;set;}
	public String State  {get;set;}
	public String TypeConverter  {get;set;}
	public String UITypeEditor  {get;set;}
	public dynamic Value {get;set;}
}

public class RuntimeConnection
{
	//https://technet.microsoft.com/en-us/library/microsoft.sqlserver.dts.pipeline.wrapper.idtsruntimeconnection100(v=sql.120).aspx
	public RuntimeConnection() {}
	public RuntimeConnection(IDTSRuntimeConnection100 rcc) 
	{
		ConnectionManagerId = rcc.ConnectionManagerID;
		Name = rcc.Name;
		ObjectType = ((DTSObjectType)rcc.ObjectType).ToString();
	}
	public String ConnectionManagerId {get;set;}
	public String Name {get;set;}
	public String ObjectType {get;set;}
}

public static XElement Component(String componentClassId)
	{
			XElement DTSbits = XElement.Parse(File.ReadAllText(@"C:\Workspaces\DTSXExplorer\IO22GSHK.XML"));
	
	return DTSbits
	.Descendants().Where(xn => xn.Name.LocalName.Equals("CreationName"))
	.Where(xv => xv.Value.Equals(componentClassId))
	.Select(xp => xp.Parent).FirstOrDefault();
	}


// Define other methods and classes here