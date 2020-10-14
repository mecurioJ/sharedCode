<Query Kind="Program">
  <NuGetReference>Azure.Storage.Files.DataLake</NuGetReference>
  <NuGetReference>Microsoft.Azure.DataLake.Store</NuGetReference>
  <NuGetReference>Microsoft.Azure.Storage.Blob</NuGetReference>
  <NuGetReference>Microsoft.Azure.Storage.Common</NuGetReference>
  <NuGetReference>Microsoft.Azure.Storage.File</NuGetReference>
  <NuGetReference>Microsoft.Rest.ClientRuntime.Azure.Authentication</NuGetReference>
  <NuGetReference>ServiceStack.Text</NuGetReference>
  <Namespace>Azure.Storage</Namespace>
  <Namespace>Azure.Storage.Files.DataLake</Namespace>
  <Namespace>Azure.Storage.Files.DataLake.Models</Namespace>
  <Namespace>Azure.Storage.Sas</Namespace>
  <Namespace>Microsoft.Azure.DataLake.Store</Namespace>
  <Namespace>Microsoft.Azure.Storage</Namespace>
  <Namespace>Microsoft.Rest</Namespace>
  <Namespace>Microsoft.Rest.Azure.Authentication</Namespace>
  <Namespace>ServiceStack</Namespace>
  <Namespace>ServiceStack.Memory</Namespace>
  <Namespace>ServiceStack.Text</Namespace>
  <Namespace>ServiceStack.Text.Common</Namespace>
  <Namespace>ServiceStack.Text.Controller</Namespace>
  <Namespace>ServiceStack.Text.Json</Namespace>
  <Namespace>ServiceStack.Text.Jsv</Namespace>
  <Namespace>ServiceStack.Text.Pools</Namespace>
  <Namespace>ServiceStack.Text.Support</Namespace>
</Query>

protected static String AccountName;
protected static String AccountKey;
protected static Uri ServiceUri;
protected static DataLakeServiceClient Client;
protected static DataLakeFileSystemClient FileSystemClient;

void Main()
{
	
	//spark.conf.set("fs.azure.account.key.f9datalakebiv2.dfs.core.windows.net","oLkT31cSX4cGXVQgTk+qa2PfJXWIg+5soXqhpZ2CYc0yuHpX7ySJeFs8tEkXa/rhU2f56MKV9g01I3udJ//8fw==")

	AccountName = "f9datalakebiv2";
    AccountKey = "oLkT31cSX4cGXVQgTk+qa2PfJXWIg+5soXqhpZ2CYc0yuHpX7ySJeFs8tEkXa/rhU2f56MKV9g01I3udJ//8fw==";
    ServiceUri = new Uri("https://f9datalakebiv2.blob.core.windows.net");

	GetDataLakeServiceClient(ref Client, AccountName, AccountKey);
	FileSystemClient = Client.GetFileSystemClient("integrator");
	//Console.WriteLine(FileSystemClient.GetPaths().Count());
	
	var firstPath = 	FileSystemClient.GetPaths().FirstOrDefault();
	var file = FileSystemClient.GetFileClient(firstPath.Name);
	var fileContent = new StreamReader(file.Read().Value.Content).ReadToEnd();

	var content = JsonSerializer.DeserializeFromString(fileContent,typeof(IntegratorRaw)).ConvertTo<IntegratorRaw>();

	var payload = content.Payload.ReadLines().ToArray();
	var flightIdentification = payload[1].Substring(3, 6);
	var aircraftIdentification = payload[1].Substring(13, 6);
	var DataDownlinkStation = payload[2].Substring(7, 3);
	var Value1 = payload[3].Substring(3,6);
	LINQPad.Extensions.Dump(new {
		flightIdentification,
		aircraftIdentification,
		DataDownlinkStation,
		Value1,
		payload
	});
	//
	//JObject.Parse(fileContent).Dump();
	//	

}
public class IntegratorRaw
{
	public String Priority { get; set; }
	public String[] Receivers { get; set; }
	public String Sender { get; set; }
	public String DateTimeGroup { get; set; }
	public String SMI { get; set; }
	public String Payload { get; set; }	
}

        protected static void GetDataLakeServiceClient(ref DataLakeServiceClient dataLakeServiceClient,
            string accountName, string accountKey)
{
            StorageSharedKeyCredential sharedKeyCredential =
                new StorageSharedKeyCredential(accountName, accountKey);

            string dfsUri = "https://" + accountName + ".dfs.core.windows.net";

            dataLakeServiceClient = new DataLakeServiceClient
				(new Uri(dfsUri), sharedKeyCredential);
}