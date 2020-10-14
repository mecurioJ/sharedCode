<Query Kind="Program">
  <Connection>
    <ID>0f3a4d8a-4daa-45bd-ad1e-aa2479f35629</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Projects\Workspaces\Heimos\Latitude.Heimos\Latitude.Heimos.DAL\bin\Debug\Latitude.Heimos.DAL.dll</CustomAssemblyPath>
    <CustomTypeName>Latitude.Heimos.DAL.HeimosEntities</CustomTypeName>
    <AppConfigPath>C:\Projects\Workspaces\Heimos\Latitude.Heimos\Latitude.Heimos.DAL\bin\Debug\Latitude.Heimos.DAL.dll.config</AppConfigPath>
  </Connection>
</Query>

//15:00 Start

void Main()
{
	Suppliers
		.Join(PurchaseOrders,su => su.Guid,po => po.SupplierGuid, (su,po) => new{su,po})
	.Dump();

	//PurchaseOrders.Dump();
}

// Define other methods and classes here
