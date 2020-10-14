<Query Kind="Program">
  <Connection>
    <ID>0f3a4d8a-4daa-45bd-ad1e-aa2479f35629</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Projects\Workspaces\Heimos\Latitude.Heimos\Latitude.Heimos.Web\bin\Latitude.Heimos.DAL.dll</CustomAssemblyPath>
    <CustomTypeName>Latitude.Heimos.DAL.HeimosEntities</CustomTypeName>
    <AppConfigPath>C:\SyncFiles\Dropbox\LinqPad\Latitude40\Latitude.Heimos.DAL.dll.config</AppConfigPath>
  </Connection>
  <Reference Relative="..\Libs\EntityFramework.6.1.1\lib\net45\EntityFramework.dll">C:\SyncFiles\Dropbox\LinqPad\Libs\EntityFramework.6.1.1\lib\net45\EntityFramework.dll</Reference>
  <Reference Relative="..\Libs\EntityFramework.6.1.1\lib\net45\EntityFramework.SqlServer.dll">C:\SyncFiles\Dropbox\LinqPad\Libs\EntityFramework.6.1.1\lib\net45\EntityFramework.SqlServer.dll</Reference>
  <Reference Relative="..\Libs\Latitude40\Latitude.Heimos.Application.dll">C:\SyncFiles\Dropbox\LinqPad\Libs\Latitude40\Latitude.Heimos.Application.dll</Reference>
  <Reference Relative="..\Libs\Latitude40\Latitude.Heimos.B2B.dll">C:\SyncFiles\Dropbox\LinqPad\Libs\Latitude40\Latitude.Heimos.B2B.dll</Reference>
  <Reference Relative="..\Libs\Latitude40\Latitude.Heimos.DAL.dll">C:\SyncFiles\Dropbox\LinqPad\Libs\Latitude40\Latitude.Heimos.DAL.dll</Reference>
  <Reference Relative="..\Libs\Latitude40\Latitude.Heimos.dll">C:\SyncFiles\Dropbox\LinqPad\Libs\Latitude40\Latitude.Heimos.dll</Reference>
  <Reference Relative="..\Libs\Latitude40\Latitude.Heimos.Entities.dll">C:\SyncFiles\Dropbox\LinqPad\Libs\Latitude40\Latitude.Heimos.Entities.dll</Reference>
  <Reference Relative="..\Libs\Latitude40\Latitude.Heimos.Utilities.dll">C:\SyncFiles\Dropbox\LinqPad\Libs\Latitude40\Latitude.Heimos.Utilities.dll</Reference>
  <Reference Relative="..\Libs\Latitude40\Latitude.Heimos.Web.dll">C:\SyncFiles\Dropbox\LinqPad\Libs\Latitude40\Latitude.Heimos.Web.dll</Reference>
  <Namespace>ExportToExcel</Namespace>
  <Namespace>Latitude.Heimos</Namespace>
  <Namespace>Latitude.Heimos.Application</Namespace>
  <Namespace>Latitude.Heimos.Application.BLL</Namespace>
  <Namespace>Latitude.Heimos.Application.BLL.Acknowledgments</Namespace>
  <Namespace>Latitude.Heimos.Application.Protocols</Namespace>
  <Namespace>Latitude.Heimos.Application.Protocols.Interfaces</Namespace>
  <Namespace>Latitude.Heimos.B2B</Namespace>
  <Namespace>Latitude.Heimos.B2B.AvailabilityService</Namespace>
  <Namespace>Latitude.Heimos.B2B.Controllers</Namespace>
  <Namespace>Latitude.Heimos.B2B.Models</Namespace>
  <Namespace>Latitude.Heimos.DAL</Namespace>
  <Namespace>Latitude.Heimos.DAL.Extensions</Namespace>
  <Namespace>Latitude.Heimos.Entities</Namespace>
  <Namespace>Latitude.Heimos.Models</Namespace>
  <Namespace>Latitude.Heimos.Models.Extensions</Namespace>
  <Namespace>Latitude.Heimos.Utilities</Namespace>
  <Namespace>Latitude.Heimos.Utilities.ErrorHandler</Namespace>
  <Namespace>Latitude.Heimos.Utilities.OpenXML</Namespace>
  <Namespace>Latitude.Heimos.Utilities.PDFTools</Namespace>
  <Namespace>Latitude.Heimos.Web</Namespace>
  <Namespace>Latitude.Heimos.Web.Controllers</Namespace>
  <Namespace>Latitude.Heimos.Web.Models</Namespace>
  <Namespace>Vergosity.Actions</Namespace>
</Query>

void Main()
{
	
	var shipWeek = DateTime.Parse("11/3/2014");
	
    List<PackListReportModel> source = ReportingDAL.GetPackList();
	source.Where(s => s.OrderDetail.DeliveryWeekDate.Equals(shipWeek)).Dump();
	
	
	Latitude.Heimos.DAL.ShipmentDAL
//	//.GetShipmentByShipWeek
	.GetShipment()
	.Where(s => s.ShipDate.HasValue)
//		//.GetShipmentByShipWeek()
//	//shipments
	.Select(s => new{
	ShipMethods = Latitude.Heimos.DAL.ShipMethodDAL.GetShipMethod(),
	shipDate =s.ShipDate,
	shipDateFmt = (s.ShipDate == null) ? String.Empty : WeekBLL.FormattedWeekString(s.ShipDate),
	shipWeek = s.Order.ShipWeek,
	shipWeekFmt = (s.Order.ShipWeek == null) ? String.Empty : WeekBLL.FormattedWeekString(s.Order.ShipWeek),
	
	
	s.Order.ShipMethod.Name
	}).OrderBy(odr => odr.shipWeek)
	//.Distinct()
	.Dump();
}

// Define other methods and classes here

/*

	//String shipmentGuid =  String.Empty;
	DateTime? shipWeek =  
//		DateTime.Parse("11/3/2014");
		null;
	DateTime? shipDate =  
//		DateTime.Parse("11/8/2014");
		null;
	String shipMethod = 
		String.Empty;
		//"9fe2cbd1-ae21-4f7b-871a-582001826647";
		//"84c9c875-c0bb-4085-a76e-401857337200";
	
//9fe2cbd1-ae21-4f7b-871a-582001826647 "FedEx" 
//84c9c875-c0bb-4085-a76e-401857337200 "Truck"
	
	bool? isLateOrder = null;
	
	var source = ReportingBLL.PackList();
	
	source.Count().Dump();
	
	//Filter by ShipWeek
	source = source.Where(p1 => {
		return (shipWeek.HasValue)
		? shipWeek.Value.Equals(p1.OrderDetail.DeliveryWeekDate)
		: p1.OrderDetail.DeliveryWeekDate.Equals(p1.OrderDetail.DeliveryWeekDate);
	}).ToArray();
	
	source.Count().Dump();
	
	
	
	//Filter by ShipDate
	source = source
		//filter the remaining records
		.Where(p1 => {
			return(shipDate.HasValue)
			? p1.LineItems.Where(t => t.ShipDate.HasValue).Any(li => shipDate.Value.Equals(li.ShipDate.Value))
			: p1.LineItems.Any();
		}).ToArray();

	source.Count().Dump();
	
		
	//Filter by DeliveryMethod
	source = source.Where(p1 => {
		return (!String.IsNullOrEmpty(shipMethod))
		? Guid.Parse(shipMethod).Equals(p1.OrderDetail.ShippingMethodGuid)
		: p1.OrderDetail.ShippingMethodGuid.Equals(p1.OrderDetail.ShippingMethodGuid);
	}).ToArray();
	
	//Filter by LateOrder
	//late order
	source.Where(p1 => (!isLateOrder.HasValue) || ((isLateOrder.Value)
                        ? p1.OrderDetail.LateOrder
                        : !p1.OrderDetail.LateOrder)).ToArray()
	.Dump();
	
	
	source.Dump();
	*/