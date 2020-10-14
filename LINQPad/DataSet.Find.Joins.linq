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
	String Schema = "edfi_80";
	DataSet ds = new DataSet();
	//ds.ReadXml(XDocument.Load(@"P:\GitHub\Ed-Fi-Samples\Sample Interchange XML\EducationOrganization.xml").CreateReader());
	ds.ReadXml(XDocument.Load(@"P:\GitHub\Ed-Fi-Samples\Sample Interchange XML\EducationOrganization.xml").CreateReader());
	ds.Tables.Cast<DataTable>()
		.Select(dt => new{
			dt.TableName,
			WildCard = String.Format("SELECT * FROM {0}.{1}",Schema ,dt.TableName),
			//ColumnNames = dt.Columns.Cast<DataColumn>().Select(cl => cl.ColumnName),
			ChildRelation = dt.ChildRelations.Cast<DataRelation>().Select(ch => new{
				
				JoinSyntax = String.Format("JOIN {0} ON {1}.{2} = {0}.{3}",
					Schema + "."  + ch.ChildTable.TableName,
					Schema + "."  + dt.TableName,
					ch.ParentColumns.First().ColumnName,
					ch.ChildColumns.First().ColumnName
					),
				//SelectSyntax = "SELECT "+ch.ChildTable.Columns.Cast<DataColumn>().Select(cl => cl.ColumnName).Aggregate((p,n) => String.Format("{0}, {1}",p,n)) + " FROM " + Schema + "."  + ch.ChildTable.TableName,
			}),
			SelectSyntax = "SELECT "+dt.Columns.Cast<DataColumn>().Select(cl => cl.ColumnName).Aggregate((p,n) => String.Format("{0},{1}",p,n))+ " FROM " + Schema + "." + dt.TableName,
		}).Dump();
	//ds.Dump();
	
}

// Define other methods and classes here