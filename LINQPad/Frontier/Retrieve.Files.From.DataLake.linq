<Query Kind="Program">
  <NuGetReference>Microsoft.Azure.Management.DataLake.Store</NuGetReference>
  <NuGetReference>Microsoft.IdentityModel.Clients.ActiveDirectory</NuGetReference>
  <NuGetReference>Microsoft.Rest.ClientRuntime.Azure.Authentication</NuGetReference>
  <Namespace>Microsoft.Azure.Management.DataLake.Store</Namespace>
  <Namespace>Microsoft.IdentityModel.Clients.ActiveDirectory</Namespace>
  <Namespace>Microsoft.Rest.Azure.Authentication</Namespace>
</Query>

void Main()
{

	// 1. Set Synchronization Context
	SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

	// 2. Create credentials to authenticate requests as an Active Directory application
	var clientCredential = new ClientCredential(clientId, clientSecret);
	var creds = ApplicationTokenProvider.LoginSilentAsync(tenantId, clientCredential).Result;

	// 3. Initialise Data Lake Store File System Client
	adlsFileSystemClient = new DataLakeStoreFileSystemManagementClient(creds);

	//adlsFileSystemClient.FileSystem.DownloadFolder(
	//			adlsAccountName,
	//			@"/Data/Integrator/APURunTime",
	//			@"D:\Projects\Frontier\APU\Data\");

	adlsFileSystemClient.FileSystem.Delete(
				adlsAccountName,
				@"/Data/Integrator/APURunTime/dfd.rep050.2002161907400170.txt");
				

	//	// 4. iterate, move and archive...
//	foreach (string selector in selectors)
//	{
//		Console.Write(selector);
//		Console.Write("------------------------------------------------------------------------");
//		//Get the files that have the identifier in them
//		foreach (var fi in new DirectoryInfo(localPath).GetFiles($"*{selector}.txt-*").ToList())
//		{
//			//move the file to ADL
//			adlsFileSystemClient.FileSystem.UploadFile(
//				adlsAccountName,
//				fi.FullName,
//				Path.Combine($"/Data/CustomerCare/IVR_BPO/{selector}/", fi.Name),
//				1,
//				false,
//				true);
//			//Move the file to the archive folder
//			File.Move(fi.FullName, $"{fi.DirectoryName}\\Archive\\{fi.Name}");
//
//			//print out the name of the file just moved...
//			Console.WriteLine($"    --{fi.Name}");
//		}
//
//	}

}

// Define other methods and classes here

// Data Lake Store File System Management Client
private static DataLakeStoreFileSystemManagementClient adlsFileSystemClient;

// Portal > Azure AD > App Registrations > App > Application ID (aka Client ID)
private static string clientId = "18e13c9a-aa73-4709-91fe-40daa2712ae5";

// Portal > Azure AD > App Registrations > App > Settings > Keys (aka Client Secret)
private static string clientSecret = "V0/8JYMgWz28wo?RrgXnWcfkslbiRa=:";

// Portal > Azure AD > Properties > Directory ID (aka Tenant ID)
private static string tenantId = "77ead82d-8a2e-4bc2-b8b3-2f8e0d161f2d";

// Name of the Azure Data Lake Store
private static string adlsAccountName = "f9datalakebi";

//network path location:
private static string localPath = @"\\goaspen\Department_Shares\Business Intelligence\Customer Care - CR.RES";

private static string[] selectors = new[] { "CSAT_col", "CSAT_mc", "WRAP_col", "WRAP_mc", "IVR_col", "IVR_mc", "IVR_cd", "IVR_cp" };