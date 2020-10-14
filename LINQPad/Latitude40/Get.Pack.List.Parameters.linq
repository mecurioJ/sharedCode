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
	DateTime? shipWeek = DateTime.Parse("11/03/2014");
	DateTime? shipDate = DateTime.Parse("11/09/2014");
	String shipMethod = null;
	bool? isLateOrder = null;
	String[] trucks = null;

			
			

//                source.ForEach(s => s.LineItems.ForEach(li =>
//                {
//                    var boxCount = li.ShipmentBoxes.Count();
//
//                    if (li.ShipmentBoxes.Count() > 1)
//                    {   
//                        li.BoxNumbers = RangeItem.BuildRange(li.ShipmentBoxes.OrderBy(t => t).Select(int.Parse).ToArray())
//                           .Select(r => r.Eval).Aggregate((p, n) => String.Format("{0}{1}", p, n))
//                           .Replace(",-", "-").Replace(",,", ",").Replace("--", "-");
//                    }
//                    else
//                    {
//                        li.BoxNumbers = "1";
//                    }
//                    
//
//                }));

            //source.Where(pl => pl.OrderDetail.OrderNumber.Equals(1000775)).Dump();
			
			
	
//	List<PackListReportModel> PackListReports;
//
//            using (HeimosEntities context = new HeimosEntities())
//            {
//                IQueryable<PackListBoxes> shipmentBoxes = context.ShipmentBoxes.Include("ShipmentBoxContents")
//                    .SelectMany(sb => sb.ShipmentBoxContents.Select(sbc => new PackListBoxes
//                    {
//                        ShipmentGuid = sb.ShipmentGuid,
//                        MaterialGuid = sbc.MaterialGuid,
//                        BoxNumber = sb.BoxNumber,
//                        Quantity = sbc.Quantity
//                    }));
//
//                PackListReports = new List<PackListReportModel>(context.Orders
//                    .Include("Broker")
//                    .Include("ShipTo")
//                    .Include("ShipTo.Region")
//                    .Include("ShipTo.Country")
//                    .Include("Customer")
//                    .Include("Customer.Region")
//                    .Include("Customer.Country")
//                    .Include("Salesman")
//                    .Include("ShipMethod")
//                    .Include("Shipments")
//
//                    .Include("Shipments.ShipmentLines")
//                    .Include("Shipments.ShipmentLines.Material")
//                    .Include("Shipments.ShipmentLines.Material.Variety")
//                    .Include("Shipments.ShipmentLines.Material.Variety.Species")
//                    .Include("Shipments.ShipmentLines.Material.Form")
//
//                    .Include("Shipments.ShipmentBoxes")
//                    //.Include("Shipments.ShipmentBoxes.ShipmentBoxContents")
//                    .Where(pl => pl.Shipments.Any())
//                    .SelectMany(pl => pl.Shipments.Select(sh => new PackListReportModel
//                    {
//                        OrderDetail = new PackListOrderDetail
//                        {
//                            DeliveryWeekDate = pl.ShipWeek.Value,
//                            OrderNumber = pl.OrderNo,
//                            BrokerPO = pl.BrokerPO,
//                            CustomerPO = pl.CustomerPO,
//                            ShippingMethod = pl.ShipMethod.Name + (pl.Shipments.Select(s => s.ProtectedRegion).FirstOrDefault()
//                                ? " (Protected)"
//                                : String.Empty),
//                            ShippingMethodGuid = pl.ShipMethodGuid,
//                            ShippingInstructions = pl.ShippingInstructions,
//                            SalesRep = pl.Salesman.Name + " - " + pl.Salesman.Phone,
//                            BillTo = pl.Broker.Name,
//                            ShipmentGuid = sh.Guid,
//                            LateOrder = pl.LateOrder,
//                            ShipDate = sh.ShipDate
//                        },
//                        SoldTo = new PackListSoldTo
//                        {
//                            SoldToCode = pl.Customer.Code,
//                            SoldToName = pl.Customer.Name,
//                            SoldToStreet = pl.Customer.Street,
//                            SoldToCity = pl.Customer.City,
//                            SoldToState = pl.Customer.Region.Code,
//                            SoldToPostal = pl.Customer.PostalCode,
//                            SoldToPhone = pl.Customer.Phone
//                        },
//                        ShipTo = new PackListShipTo
//                        {
//                            ShipToCode = pl.ShipTo.Code,
//                            ShipToName = pl.ShipTo.Name,
//                            ShipToStreet = pl.ShipTo.Street,
//                            ShipToCity = pl.ShipTo.City,
//                            ShipToState = pl.ShipTo.Region.Code,
//                            ShipToPostal = pl.ShipTo.PostalCode,
//                            ShipToPhone = pl.ShipTo.Phone
//                        },
//                        LineItems = sh.ShipmentLines.Select(sl => new PackListLineItems
//                        {
//                            ShipmentGuid = sh.Guid,
//                            Breeder = sl.Material.Variety.Supplier.Name,
//                            ShipmentNumber = sh.ShipmentNumber,
//                            SequenceWithinOrder = sh.SequenceWithinOrder,
//                            SequenceWithinWeek = sh.SequenceWithinWeek,
//                            SellingMultiple = sl.Material.Form.SellingMultiple ?? 1,
//                            DropNumber = sh.DropNumber,
//                            Quantity = sl.Quantity, //Can sum in Report as Total Cuttings
//                            VarietyCode = sl.Material.Variety.Code,
//                            VarietyName = sl.Material.Variety.Name,
//                            SpeciesCode = sl.Material.Variety.Species.Code,
//                            SpeciesName = sl.Material.Variety.Species.Name,
//                            FormCode = sl.Material.Form.Code,
//                            FormName = sl.Material.Form.Name,
//                            ShipmentBoxes = sh.ShipmentBoxes
//                                //.Where(sb => sb.ShipmentGuid.Equals(sh.Guid) && sl.MaterialGuid.Equals(sl.MaterialGuid))
//                                .Where(sb => sb.ShipmentBoxContents.Any(sbc => sbc.MaterialGuid.Equals(sl.MaterialGuid)))
//                                .Select(num => num.BoxNumber.ToString()).ToList(),
//                            TotalBoxes = sh.ShipmentBoxes.Count(sb => sb.ShipmentGuid.Equals(sh.Guid)),
//                            Liners = int.MinValue,
//                            //Parameter List for filtering...
//                            ShipWeek = sh.Order.ShipWeek,
//                            LateOrder = sh.Order.LateOrder,
//                            ShipDate = sh.ShipDate,
//                            ShipMethodGuid = sh.Order.ShipMethodGuid,
//                            TruckNumber = sh.TruckNumber
//                        }).OrderBy(li => li.VarietyCode)
//                    })))
//                    //Filter by ShipWeek
//                    .Where(p1 => (shipWeek.HasValue)
//                        ? shipWeek.Value.Date.Equals(p1.OrderDetail.DeliveryWeekDate.Date)
//                        : p1.OrderDetail.DeliveryWeekDate.Equals(p1.OrderDetail.DeliveryWeekDate))
//                    //Filter by ShipDay
//                    .Where(p1 => (shipDate.HasValue)
//                        ? p1.LineItems.Where(t => t.ShipDate.HasValue).Any(li => shipDate.Value.Date.Equals(li.ShipDate.Value.Date))
//                        : p1.LineItems.Any())
//                    //Filter by Shipping Method
//                    .Where(p1 => (!String.IsNullOrEmpty(shipMethod))
//                        ? Guid.Parse(shipMethod).Equals(p1.OrderDetail.ShippingMethodGuid)
//                        : p1.OrderDetail.ShippingMethodGuid.Equals(p1.OrderDetail.ShippingMethodGuid))
//                    //Filter by Late Order
//                    .Where(p1 => (!isLateOrder.HasValue) || ((isLateOrder.Value)
//                        ? p1.OrderDetail.LateOrder
//                        : !p1.OrderDetail.LateOrder))
//                    //Filter by Truck Number
//                    .Where(p1 => (trucks != null && (trucks.Any()))
//                        ? p1.LineItems.Select(li => li.TruckNumber).Intersect(trucks).Any()
//                        : p1.LineItems.Any()
//                        )
//                    .ToList();
//            }
//			
//		PackListReports.ForEach(pl =>
//            {
// 				pl.OrderDetail.WeekNumber = WeekBLL.FormattedWeekString(pl.OrderDetail.DeliveryWeekDate);
//
//                foreach (PackListLineItems item in pl.LineItems)
//                {
//                    item.Liners = Math.Round(OrderBLL.CuttingsToTrays(item.Quantity, item.SellingMultiple), 2);
//                    item.BoxNumbers = item.ShipmentBoxes.OrderBy(box => Int32.Parse(box)).Aggregate((p, n) => String.Format("{0},{1}", p, n));
//                }
//                pl.OrderDetail.TotalCuttings = pl.LineItems.Select(li => li.Quantity).Sum();
//                pl.OrderDetail.TotalLiners = pl.LineItems.Select(li => li.Liners).Sum();
//                pl.OrderDetail.TotalBoxes = pl.LineItems.Select(li => li.TotalBoxes).LastOrDefault();
//                pl.OrderDetail.SequenceWithinWeek = pl.LineItems.Select(li => li.SequenceWithinWeek).FirstOrDefault();
//            });
//
//		PackListReports.ToArray()
//		//.Where(pl => pl.OrderDetail.OrderNumber.Equals(1000775))
//		.ForEach(s => s.LineItems.ForEach(li =>
//                {
//                    var boxCount = li.ShipmentBoxes.Count();
//
//                    if (li.ShipmentBoxes.Count() > 1)
//                    {   
//                        li.BoxNumbers = RangeItem.BuildRange(li.ShipmentBoxes.OrderBy(t => t).Select(int.Parse).ToArray())
//                           .Select(r => r.Eval).Aggregate((p, n) => String.Format("{0}{1}", p, n))
//                           .Replace(",-", "-").Replace(",,", ",").Replace("--", "-");
//                    }
//                    else
//                    {
//                        li.BoxNumbers = "1";
//                    }
//                    
//
//                }));
//
//		
	
	//Latitude.Heimos.ReportingService.Data.ReportDataSource.GetPackList(null, null, string.Empty,null, new[]{""}).Dump();
}

// Define other methods and classes here