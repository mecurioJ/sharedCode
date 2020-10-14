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
  <Reference Relative="..\Libs\Latitude40\Latitude.Heimos.ReportingService.dll">C:\SyncFiles\Dropbox\LinqPad\Libs\Latitude40\Latitude.Heimos.ReportingService.dll</Reference>
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
            DateTime? shipWeek = DateTime.Parse("11/03/2014 07:00 AM");
            DateTime? shipDate = DateTime.Parse("11/08/2014 07:00 AM");
            String shipMethod = null;
            bool? isLateOrder = null;
            //String[] trucks = null;

//            DateTime? interceptShipWeek = (shipWeek.HasValue) ? shipWeek.Value.Date : (DateTime?) null;
//            DateTime? interceptShipDate = (shipDate.HasValue) ? shipDate.Value.Date : (DateTime?)null;
//            Guid? interceptShipMethod = (!String.IsNullOrEmpty(shipMethod)) ? Guid.Parse(shipMethod) : (Guid?) null;
//            Boolean? interceptLateOrder = (isLateOrder.HasValue) ? isLateOrder.Value : (Boolean?) null;

	            using (HeimosEntities context = new HeimosEntities())
            {
				
                 context.OrderLines
                            .Include("Order")
                            .Include("Order.Shipments")
                            .Include("Order.Shipments.ShipmentLines")
                            .Include("Material")
                            .Include("Material.Variety")
                            .Include("Material.Variety.Species")
                            .Include("Material.Variety.Species.Cultivar")
                            .Include("Material.Form")
                    .Where(ol => ol.Order.ShipWeek.HasValue)
                    .Where(ol => ol.MaterialGuid.HasValue)
                    .Select(ol => new PickListModel
                    {
                        ShipWeek = ol.Order.ShipWeek.Value.ToString(),
                        CultivarCode = ol.Material.Variety.Species.Cultivar.Code,
                        CultivarName = ol.Material.Variety.Species.Cultivar.Name,
                        FormCode = ol.Material.Form.Code,
                        FormName = ol.Material.Form.Name,
                        VarietyCode = ol.Material.Variety.Code,
                        VarietyName = ol.Material.Variety.Name,
                        MaterialGuid = ol.MaterialGuid.Value,
                        QtyOrdered = ol.QtyOrdered.Value,
                        SellingMultiple = ol.Material.Form.SellingMultiple.Value,
                        //Parameters not displayed
						ShipWeekDate = ol.Order.ShipWeek.Value,
                        IsLateOrder = ol.Order.LateOrder,
                        ShipMethodName = ol.Order.ShipMethod.Name,
                        ShipMethodGuid = ol.Order.ShipMethod.Guid,
                        ShipDates = ol.Order.Shipments.Where(sh => sh.ShipDate.HasValue).Select(sh => sh.ShipDate),
                        TruckNumbers = ol.Order.Shipments.Where(sh => sh.ShipDate.HasValue).Select(sh => sh.TruckNumber)
                    }).AsEnumerable()
                    .Where(p1 => (shipWeek.HasValue)
                        ? shipWeek.Value.Date.Equals(p1.ShipWeekDate.Value.Date)
                        : true)
                    //Filter by ShipDay
                    .Where(p1 => (shipDate.HasValue)
                        ? p1.ShipDates.Where(sd => sd.Value.Date.Equals(shipDate.Value.Date)).Any()
                        : true)
                    //Filter by Shipping Method
                    .Where(p1 => (!String.IsNullOrEmpty(shipMethod))
                        ? Guid.Parse(shipMethod).Equals(p1.ShipMethodGuid)
                        : true)
                    //Filter by Late Order
                    .Where(p1 => (!isLateOrder.HasValue) || ((isLateOrder.Value)
                        ? p1.IsLateOrder
                        : !p1.IsLateOrder))
                    //Filter by Truck Number
//                    .Where(p1 => (trucks != null && (trucks.Any()))
//                        ? p1.LineItems.Select(li => li.TruckNumber).Intersect(trucks).Any()
//                        : p1.LineItems.Any()
//                        )
                    .ToList()
					.Dump();
            }
}

// Define other methods and classes here
