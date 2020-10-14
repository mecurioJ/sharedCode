<Query Kind="Program">
  <NuGetReference>Azure.Identity</NuGetReference>
  <NuGetReference>Azure.Storage.Blobs</NuGetReference>
  <NuGetReference>Azure.Storage.Files.DataLake</NuGetReference>
  <NuGetReference>Microsoft.Azure.DataLake.Store</NuGetReference>
  <Namespace>Azure.Core</Namespace>
  <Namespace>Azure.Identity</Namespace>
  <Namespace>Azure.Storage</Namespace>
  <Namespace>Azure.Storage.Blobs</Namespace>
  <Namespace>Azure.Storage.Blobs.Models</Namespace>
  <Namespace>Azure.Storage.Files.DataLake</Namespace>
  <Namespace>Azure.Storage.Files.DataLake.Models</Namespace>
  <Namespace>Azure.Storage.Sas</Namespace>
</Query>

void Main()
{

	var ConnectionString = "BlobEndpoint=https://f9datalakebiv2.blob.core.windows.net/;QueueEndpoint=https://f9datalakebiv2.queue.core.windows.net/;FileEndpoint=https://f9datalakebiv2.file.core.windows.net/;TableEndpoint=https://f9datalakebiv2.table.core.windows.net/;SharedAccessSignature=sv=2019-10-10&ss=bfqt&srt=sco&sp=rwdlacupx&se=2200-05-12T09:17:21Z&st=2020-05-13T01:17:21Z&spr=https&sig=PsPCj7AlwOU7qDblIauzsjNj9UoCq00eLYYL66O8s3w%3D";

var SASToken = "?sv=2019-10-10&ss=bfqt&srt=sco&sp=rwdlacupx&se=2200-05-12T09:17:21Z&st=2020-05-13T01:17:21Z&spr=https&sig=PsPCj7AlwOU7qDblIauzsjNj9UoCq00eLYYL66O8s3w%3D";

var BlobServiceSAS = "https://f9datalakebiv2.blob.core.windows.net/?sv=2019-10-10&ss=bfqt&srt=sco&sp=rwdlacupx&se=2200-05-12T09:17:21Z&st=2020-05-13T01:17:21Z&spr=https&sig=PsPCj7AlwOU7qDblIauzsjNj9UoCq00eLYYL66O8s3w%3D";

var FileServiceSAS = "https://f9datalakebiv2.file.core.windows.net/?sv=2019-10-10&ss=bfqt&srt=sco&sp=rwdlacupx&se=2200-05-12T09:17:21Z&st=2020-05-13T01:17:21Z&spr=https&sig=PsPCj7AlwOU7qDblIauzsjNj9UoCq00eLYYL66O8s3w%3D";

var QueueServiceSAS = "https://f9datalakebiv2.queue.core.windows.net/?sv=2019-10-10&ss=bfqt&srt=sco&sp=rwdlacupx&se=2200-05-12T09:17:21Z&st=2020-05-13T01:17:21Z&spr=https&sig=PsPCj7AlwOU7qDblIauzsjNj9UoCq00eLYYL66O8s3w%3D";

var TableServiceSAS = "https://f9datalakebiv2.table.core.windows.net/?sv=2019-10-10&ss=bfqt&srt=sco&sp=rwdlacupx&se=2200-05-12T09:17:21Z&st=2020-05-13T01:17:21Z&spr=https&sig=PsPCj7AlwOU7qDblIauzsjNj9UoCq00eLYYL66O8s3w%3D";


	string accountName = "f9datalakebiv2";
	string accountKey = "oLkT31cSX4cGXVQgTk+qa2PfJXWIg+5soXqhpZ2CYc0yuHpX7ySJeFs8tEkXa/rhU2f56MKV9g01I3udJ//8fw==";
	Uri serviceUri = new Uri(BlobServiceSAS);


	StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(accountName, accountKey);
	DataLakeServiceClient service = new DataLakeServiceClient(serviceUri, sharedKeyCredential);
	
	DAtaLake

}

// Define other methods, classes and namespaces here