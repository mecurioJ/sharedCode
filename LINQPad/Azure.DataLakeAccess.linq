<Query Kind="Program">
  <NuGetReference>Azure.Storage.Files.DataLake</NuGetReference>
  <NuGetReference>Microsoft.Azure.DataLake.Store</NuGetReference>
  <NuGetReference>Microsoft.Azure.Storage.Blob</NuGetReference>
  <NuGetReference>Microsoft.Azure.Storage.Common</NuGetReference>
  <NuGetReference>Microsoft.Azure.Storage.File</NuGetReference>
  <NuGetReference>Microsoft.Rest.ClientRuntime.Azure.Authentication</NuGetReference>
  <Namespace>Azure.Storage</Namespace>
  <Namespace>Azure.Storage.Files.DataLake</Namespace>
  <Namespace>Azure.Storage.Files.DataLake.Models</Namespace>
  <Namespace>Azure.Storage.Sas</Namespace>
  <Namespace>Microsoft.Azure.DataLake.Store</Namespace>
  <Namespace>Microsoft.Azure.Storage</Namespace>
  <Namespace>Microsoft.Rest</Namespace>
  <Namespace>Microsoft.Rest.Azure.Authentication</Namespace>
</Query>

void Main()
{

	String v1TennantId = "77ead82d-8a2e-4bc2-b8b3-2f8e0d161f2d";
	String v1AppId = "18e13c9a-aa73-4709-91fe-40daa2712ae5";
	String v1ClientSecret = "V0/8JYMgWz28wo?RrgXnWcfkslbiRa=:";
	String v1FQDN = "f9datalakebi.azuredatalakestore.net";

	string accountName = "f9datalakebiv2";
	string accountKey = "oLkT31cSX4cGXVQgTk+qa2PfJXWIg+5soXqhpZ2CYc0yuHpX7ySJeFs8tEkXa/rhU2f56MKV9g01I3udJ//8fw==";
	Uri serviceUri = new Uri("https://f9datalakebiv2.blob.core.windows.net");

	//System.Uri ADL_TOKEN_AUDIENCE = new System.Uri(@"https://datalake.azure.net/");

	var v1Client = AdlsClient.CreateClient(
				v1FQDN,
				GetCreds_SPI_SecretKey(
					v1TennantId, 
					new System.Uri(@"https://datalake.azure.net/"), 
					v1AppId, 
					v1ClientSecret)
				);
				
	var directories = v1Client.EnumerateDirectory("/Data/Integrator/OOOI/").OrderByDescending(d => d.LastModifiedTime);	
	//var td = v1Client.EnumerateDirectory(directories.First().FullName).First();
	//td.Dump();
	directories.Dump();
	
	//var SourceFile = v1Client.GetReadStream(td.FullName);
	//var sourceFileName = td.Name;

//
//	
//	//get a security key
//	StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(accountName, accountKey);
//	// Get a reference to a service Client
//	DataLakeServiceClient serviceClient = new DataLakeServiceClient(serviceUri, sharedKeyCredential);
//	//get a reference to a filesystem client
//	DataLakeFileSystemClient filesystem = serviceClient.GetFileSystemClient("customercare/ivr_bpo");
//	//create a file
//	DataLakeFileClient file = filesystem.CreateFile(sourceFileName);
	
	//write to the file
//	file.Append(SourceFile, 0);
//	file.Flush(SourceFile.Length);
//
//	//compare the file
//	PathProperties props = file.GetProperties();
//	SourceFile.Length.Equals(props.ContentLength).Dump();


}

// Define other methods, classes and namespaces here
public static ServiceClientCredentials GetCreds_SPI_SecretKey(
	   string tenant,
	   Uri tokenAudience,
	   string clientId,
	   string secretKey)
{
	SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

	var serviceSettings = ActiveDirectoryServiceSettings.Azure;
	serviceSettings.TokenAudience = tokenAudience;

	ServiceClientCredentials creds = ApplicationTokenProvider.LoginSilentAsync(
	 tenant,
	 clientId,
	 secretKey,
	 serviceSettings).GetAwaiter().GetResult();
	return creds;
}