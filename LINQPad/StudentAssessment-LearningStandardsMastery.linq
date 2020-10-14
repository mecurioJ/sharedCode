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
	var filePath = @"P:\GitHub\Ed-Fi-Samples\Sample Interchange XML\";
	//var fileName = @"StudentAssessment-StateAssessment.xml";
	//var fileName = @"StudentAssessment-LearningStandardsMastery.xml";
	
	System.IO.DirectoryInfo dir = new DirectoryInfo(filePath);
	
	Server BVAServer2014 = new Server(new ServerConnection(){
		ServerInstance = @"bvaserver", 
		LoginSecure = false, 
		Login = "sa", 
		Password = "ob1-w4n"
		});
	
	SqlConnection sqlConn = new SqlConnection(@"Server=bvaserver;Database=Interchange;User Id=sa;Password=ob1-w4n");
	
	Database Interchange = BVAServer2014.Databases["Interchange"];
	
	List<String> Inserts = new List<String>();
	
	int i = (int)'A';
	foreach(var fi in dir.GetFiles())
	{
		if(i == 91) { i = 97;}
		
		String.Format("edfi_{0} : {1}",i,fi.FullName).Dump();
		char schemaId = (char)i;
		
		Microsoft.SqlServer.Management.Smo.Schema sch = new Schema(Interchange,String.Format("edfi_{0}",i));
		
		if(Interchange.Schemas[String.Format("edfi_{0}",i)] == null)
		{
//			("Schema Create:"+sch.Name).Dump();
//			sch.Create();
		}
		
		try
		{	 
			DataSet ds = new DataSet();
			ds.ReadXml(XDocument.Load(fi.FullName).CreateReader());
			
			foreach(DataTable dt in ds.Tables)
			{
				
				Microsoft.SqlServer.Management.Smo.Table sqlTable = new Microsoft.SqlServer.Management.Smo.Table(Interchange,dt.TableName,sch.Name);
				List<String> columnNames = new List<String>();
				
				
				
				
				foreach(DataColumn dc in dt.Columns)
				{
					Microsoft.SqlServer.Management.Smo.Column cc = new Column(sqlTable,dc.ColumnName,EvalDataType(dc.DataType));
					sqlTable.Columns.Add(cc);
					columnNames.Add(dc.ColumnName);
				}
				
//				sqlTable.Create();
//				
//				
//				
////				get the table
////				
////				Connect to the DB
//				sqlConn.Open();
				
				
//				sqlConn.Close();
				foreach (DataRow row in dt.Rows)
				{
					Inserts.Add(String.Format("INSERT INTO [{0}].[{1}]({2})VALUES({3})"
					,sch.Name
					,sqlTable.Name
					,String.Join(",",dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray())
					,String.Join(",",row.ItemArray.Select(ia => String.Format("'{0}'",ia.ToString().Replace("'","''"))))
					));
				}
				
				
				//sqlTable.Script()[0].Dump();
				//columnNames.Aggregate((p,n) => String.Format("{0},{1}",p,n)).Dump();
				
				Inserts.Add(@"'/*"+sqlTable.Name+"--------------------------------------------------------------------------------------------------------*/'");
			}
				
				
		}
		catch (Exception ex)
		{
			fi.FullName.Dump();
			ex.Message.Dump();
			//throw;
		}
		
		Inserts.Add(@"'/*"+fi.FullName+"--------------------------------------------------------------------------------------------------------*/'");
		i = i+1;
	}
	

	//System.IO.File.WriteAllLines(@"P:\GitHub\Ed-Fi-Samples\Sample Interchange XML\Data.Inserts.sql",Inserts.ToArray());
	
	
	
	
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