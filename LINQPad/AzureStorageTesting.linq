<Query Kind="Program">
  <Connection>
    <ID>f82670ce-0e3b-4779-83f3-24ad947d8dd1</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>StateData</Database>
  </Connection>
  <Reference Relative="Libs\Microsoft.Data.Edm.5.6.0\lib\net40\Microsoft.Data.Edm.dll">C:\SyncDrives\Dropbox\LinqPad\Libs\Microsoft.Data.Edm.5.6.0\lib\net40\Microsoft.Data.Edm.dll</Reference>
  <Reference Relative="Libs\Microsoft.Data.OData.5.6.0\lib\net40\Microsoft.Data.OData.dll">C:\SyncDrives\Dropbox\LinqPad\Libs\Microsoft.Data.OData.5.6.0\lib\net40\Microsoft.Data.OData.dll</Reference>
  <Reference Relative="Libs\Microsoft.Data.Services.Client.5.6.0\lib\net40\Microsoft.Data.Services.Client.dll">C:\SyncDrives\Dropbox\LinqPad\Libs\Microsoft.Data.Services.Client.5.6.0\lib\net40\Microsoft.Data.Services.Client.dll</Reference>
  <Reference Relative="Libs\Microsoft.WindowsAzure.ConfigurationManager.1.8.0.0\lib\net35-full\Microsoft.WindowsAzure.Configuration.dll">C:\SyncDrives\Dropbox\LinqPad\Libs\Microsoft.WindowsAzure.ConfigurationManager.1.8.0.0\lib\net35-full\Microsoft.WindowsAzure.Configuration.dll</Reference>
  <Reference Relative="Libs\WindowsAzure.Storage.4.1.0\lib\net40\Microsoft.WindowsAzure.Storage.dll">C:\SyncDrives\Dropbox\LinqPad\Libs\WindowsAzure.Storage.4.1.0\lib\net40\Microsoft.WindowsAzure.Storage.dll</Reference>
  <Reference Relative="Libs\Newtonsoft.Json.5.0.6\lib\net45\Newtonsoft.Json.dll">C:\SyncDrives\Dropbox\LinqPad\Libs\Newtonsoft.Json.5.0.6\lib\net45\Newtonsoft.Json.dll</Reference>
  <Reference Relative="Libs\System.Spatial.5.6.0\lib\net40\System.Spatial.dll">C:\SyncDrives\Dropbox\LinqPad\Libs\System.Spatial.5.6.0\lib\net40\System.Spatial.dll</Reference>
  <Namespace>Microsoft.Data.Edm</Namespace>
  <Namespace>Microsoft.Data.Edm.Annotations</Namespace>
  <Namespace>Microsoft.Data.Edm.Csdl</Namespace>
  <Namespace>Microsoft.Data.Edm.EdmToClrConversion</Namespace>
  <Namespace>Microsoft.Data.Edm.Evaluation</Namespace>
  <Namespace>Microsoft.Data.Edm.Expressions</Namespace>
  <Namespace>Microsoft.Data.Edm.Library</Namespace>
  <Namespace>Microsoft.Data.Edm.Library.Annotations</Namespace>
  <Namespace>Microsoft.Data.Edm.Library.Expressions</Namespace>
  <Namespace>Microsoft.Data.Edm.Library.Values</Namespace>
  <Namespace>Microsoft.Data.Edm.Validation</Namespace>
  <Namespace>Microsoft.Data.Edm.Values</Namespace>
  <Namespace>Microsoft.Data.OData</Namespace>
  <Namespace>Microsoft.Data.OData.Atom</Namespace>
  <Namespace>Microsoft.Data.OData.Metadata</Namespace>
  <Namespace>Microsoft.Data.OData.Query</Namespace>
  <Namespace>Microsoft.Data.OData.Query.SemanticAst</Namespace>
  <Namespace>Microsoft.WindowsAzure</Namespace>
  <Namespace>Microsoft.WindowsAzure.Storage</Namespace>
  <Namespace>Microsoft.WindowsAzure.Storage.Auth</Namespace>
  <Namespace>Microsoft.WindowsAzure.Storage.Table</Namespace>
  <Namespace>Microsoft.WindowsAzure.Storage.Table.DataServices</Namespace>
  <Namespace>Microsoft.WindowsAzure.Storage.Table.Protocol</Namespace>
  <Namespace>Microsoft.WindowsAzure.Storage.Table.Queryable</Namespace>
  <Namespace>System.Data.Services.Client</Namespace>
  <Namespace>System.Data.Services.Common</Namespace>
</Query>

void Main()
{

	var StoreName = "brightviewstore";
	var PrimaryAccessKey = "glrnnzuEVfe1l/F3i4TaC5ktxzKAtbFkSVq0kBQVFZIGZpaNxc7IYD1EPX+Rf05gQioemhe+arFyKZBwT8VaaA==";
	var uri = new System.Uri("http://table.core.windows.net/"); 
//	
//	var storageAccountInfo = new CloudStorageAccount(new StorageCredentials(StoreName,PrimaryAccessKey),true);
//	
//	CloudTableClient tableClient = storageAccountInfo.CreateCloudTableClient();
//	
//	
//	CloudTable table = tableClient.GetTableReference("StateGrouping");
//	
//	table.Exists().Dump();
//	table.CreateIfNotExists();
//	table.Exists().Dump();
//	StateGroupingEntity sge = new StateGroupingEntity(1m,"Total Public");
//	
//	TableOperation insertOp = TableOperation.Insert(sge);
//	
//	//table.Execute(insertOp);
//	
//	TableQuery<StateGroupingEntity> query = new TableQuery<StateGroupingEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey",
//	QueryComparisons.Equal,"StateGrouping"));
//	
//	table.ExecuteQuery(query).ToList().Dump();
	
}





public class StateGroupingEntity : TableEntity
{
	public StateGroupingEntity(){}

	public StateGroupingEntity(decimal groupCode, String groupName)
	{
		PartitionKey = "StateGrouping";
		RowKey = GroupCode.ToString();
		GroupCode = groupCode;
		GroupName = groupName;
	}
	
	
	public StateGroupingEntity(decimal groupCode)
	{
		PartitionKey = "StateGrouping";
		RowKey = GroupCode.ToString();
	}
	
	public decimal GroupCode {get;set;}
	public String GroupName {get;set;}
}