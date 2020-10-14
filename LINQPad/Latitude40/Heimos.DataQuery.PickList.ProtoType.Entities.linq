<Query Kind="Program">
  <Connection>
    <ID>0f3a4d8a-4daa-45bd-ad1e-aa2479f35629</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Projects\Workspaces\Heimos\Latitude.Heimos\Latitude.Heimos.Web\bin\Latitude.Heimos.DAL.dll</CustomAssemblyPath>
    <CustomTypeName>Latitude.Heimos.DAL.HeimosEntities</CustomTypeName>
    <AppConfigPath>C:\Projects\Workspaces\Heimos\Latitude.Heimos\Latitude.Heimos.DAL\bin\Debug\Latitude.Heimos.DAL.dll.config</AppConfigPath>
  </Connection>
</Query>

void Main()
{
	Orders
	.Select(o => new{
		o.CustomerPO,
		CustomerCode = o.Customer.Code,
		CustomerName = o.Customer.Name,
		CustomerStreet = o.Customer.Street,
		CustomerCity = o.Customer.City,
		CustomerState = o.Customer.Region.Code,
		CustomerPostalCode = o.Customer.PostalCode,
		CustomerCountryCode = o.Customer.Country.Code,
		CustomerCountry = o.Customer.Country.Name,
		//--------------------------------------------//
		ShipToCode = o.ShipTo.Code,
		ShipToName = o.ShipTo.Customer.Name,
		ShipToStreet = o.ShipTo.Street,
		ShipToCity = o.ShipTo.City,
		ShipToState = o.ShipTo.Region.Code,
		ShipoToPostalCode = o.ShipTo.PostalCode,
		//--------------------------------------------//
		o.OrderNo,
		o.BrokerPO,
		o.Salesman.Name,
		o.Salesman.Phone,
		BillTo = o.Broker.Name,
		//o.Broker.Salesmen,
		//Broker = BillTo
		//BrokerSalesMan = SalesRep
		o.ShipWeek,
		ShipMethodType = o.ShipMethod.Name
		
		//Breeder, form, species, Description, Cuttings, Liners, Num Boxes
	})
	.Dump();
	
}

// Define other methods and classes here