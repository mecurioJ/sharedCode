<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.ConnectionInfo.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.Management.Sdk.Sfc.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.RegSvrEnum.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.ServiceBrokerEnum.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.Smo.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.SqlEnum.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.WmiEnum.dll</Reference>
  <Namespace>Microsoft.SqlServer.Management.Common</Namespace>
  <Namespace>Microsoft.SqlServer.Management.Smo</Namespace>
</Query>

void Main()
{
	
	var relSet = GetRelationWorksheet(@"P:\GitHub\Ed-Fi-Samples\Sample Interchange XML\");
	
	relSet
	//.Where(ws => ws.SchemaName.Equals("edfi_79"))
	//.Select(itm => {return "Select top 10 * from " + itm.SchemaName +'.'+ itm.ParentTableName;}).Distinct()
	
	.Dump();
	
}

public static List<RelationDefinition> GetRelationWorksheet(String filePath)
{
	System.IO.DirectoryInfo dir = new DirectoryInfo(filePath);
	List<RelationDefinition> RelationWorksheet = new List<RelationDefinition>();
	
	int i = (int)'A';
	foreach(var fi in dir.GetFiles("*.xml").Cast<FileInfo>())
	{
		if(i == 91) { i = 97;}
		
		var SchemaName = String.Format("edfi_{0}",i+1);
		var FileName = fi.FullName;
		
		try
		{	 
			DataSet ds = new DataSet();
			ds.ReadXml(XDocument.Load(fi.FullName).CreateReader());
			
			RelationWorksheet.AddRange(
			ds.Relations.Cast<DataRelation>().Select(rel => new RelationDefinition{
				SchemaName = String.Format("edfi_{0}",i+1),
				FileName = fi.FullName,
				ShortName = fi.Name.Replace(fi.Extension,""),
				ParentTableName = rel.ParentKeyConstraint.Table.TableName,
				ChildTableName = rel.ChildKeyConstraint.Table.TableName,
				ParentColumns = rel.ParentColumns.Cast<DataColumn>().Select(col => String.Format("{0}.{1}",col.Table.TableName, col.ColumnName) ).SingleOrDefault(),
				ChildColumns = rel.ChildColumns.Cast<DataColumn>().Select(col => String.Format("{0}.{1}",col.Table.TableName, col.ColumnName) ).SingleOrDefault(),
				 
			}));
				
				
		}
		catch (Exception ex)
		{
			fi.FullName.Dump();
			ex.Message.Dump();
			//throw;
		}
		
			i = i+1;
	}
	
	return RelationWorksheet;
	
}


public class RelationDefinition
{
	public String SchemaName {get;set;}
	public String FileName {get;set;}
	public String ShortName {get;set;}
	public String ParentTableName {get;set;}
	public String ChildTableName {get;set;}
	public String ParentColumns {get;set;}
	public String ChildColumns {get;set;}
	
}


public class TableContent
{
	public String TableName {get;set;}
	public Microsoft.SqlServer.Management.Smo.Table SqlTable {get;set;}
	public IEnumerable<Microsoft.SqlServer.Management.Smo.Column> SqlColumns {get;set;}
	public String TableScript {get;set;}
	public IEnumerable<DataColumn> SourceColumns {get;set;}
	public IEnumerable<DataRow> SourceRows {get;set;}
}

public static Microsoft.SqlServer.Management.Smo.DataType EvalDataType(System.Type type)
{
		if (type == typeof(Int64)) 
			return Microsoft.SqlServer.Management.Smo.DataType.BigInt;
//		if (type ==  typeof(Byte[])) 
//			return Microsoft.SqlServer.Management.Smo.DataType.Binary;
		if (type ==  typeof(Boolean)) 
			return Microsoft.SqlServer.Management.Smo.DataType.Bit;
		if (type ==  typeof(DateTime)) 
			return Microsoft.SqlServer.Management.Smo.DataType.DateTime;
//		if (type ==  typeof(DateTimeOffset)) 
//			return Microsoft.SqlServer.Management.Smo.DataType.DateTimeOffset;
		if (type ==  typeof(Decimal)) 
			return Microsoft.SqlServer.Management.Smo.DataType.Decimal(10,4);
		if (type ==  typeof(Double)) 
			return Microsoft.SqlServer.Management.Smo.DataType.Numeric(10,4);
		if (type ==  typeof(Int32)) 
			 return Microsoft.SqlServer.Management.Smo.DataType.Int;
		if (type ==  typeof(Single)) 
			 return Microsoft.SqlServer.Management.Smo.DataType.Real;
		if (type ==  typeof(Int16)) 
			 return Microsoft.SqlServer.Management.Smo.DataType.SmallInt;
//		if (type ==  typeof(TimeSpan)) 
//			 return Microsoft.SqlServer.Management.Smo.DataType.Time;
		if (type ==  typeof(Byte)) 
			 return Microsoft.SqlServer.Management.Smo.DataType.TinyInt;
		if (type ==  typeof(Guid)) 
			 return Microsoft.SqlServer.Management.Smo.DataType.UniqueIdentifier;
		if (type ==  typeof(String)) 
			 return Microsoft.SqlServer.Management.Smo.DataType.VarCharMax;
		return Microsoft.SqlServer.Management.Smo.DataType.Variant;
}

// Define other methods and classes here