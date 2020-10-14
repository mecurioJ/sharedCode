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

	
	DateTime? shipWeek = DateTime.Parse("10/27/2014");
	DateTime? shipDate = null;//DateTime.Parse("10/27/2014");
	string shipMethod = null;
	//string shipMethod = "9fe2cbd1-ae21-4f7b-871a-582001826647"; //FedEx
	//string shipMethod = "84c9c875-c0bb-4085-a76e-401857337200"; //Truck
	bool? isLateOrder = null;
	string[] trucks = null;
	
 List<BoxContentLabelModel> contents = new List<BoxContentLabelModel>();

            if (shipWeek.HasValue)
            {
                List<Latitude.Heimos.Entities.Shipment> shipmentList = ShipmentDAL.GetShipmentByShipWeek(shipWeek.Value, true)
                    //Filter by ShipDay
                    .Where(p1 => (shipDate.HasValue)
                        ? shipDate.Value.Date.Equals(p1.ShipDate.Value.Date)
                        : true)
                    //Filter by Shipping Method
                    .Where(p1 => (!String.IsNullOrEmpty(shipMethod))
                        ? Guid.Parse(shipMethod).Equals(p1.Order.ShipMethodGuid)
                        : p1.Order.ShipMethodGuid.Equals(p1.Order.ShipMethodGuid))
                    //Filter by Late Order
                    .Where(p1 => (!isLateOrder.HasValue) || ((isLateOrder.Value)
                        ? p1.Order.LateOrder
                        : !p1.Order.LateOrder))
                    .ToList()
                    /*                  
                    //Filter by Truck Number
                    .Where(p1 => (trucks != null && (trucks.Any()))
                        ? p1.LineItems.Select(li => li.TruckNumber).Intersect(trucks).Any()
                        : p1.LineItems.Any()
                        )
                    .ToList();
                     */
                    ;

//shipmentList.Dump();
                foreach (
                    List<BoxContentLabelModel> boxLabels in
                        shipmentList.Select(shipment => shipment.ShipmentBoxes.Select(sb => new BoxContentLabelModel
                        {
                            ShipmentGuid = shipment.Guid,
                            OrderNumber = shipment.Order.OrderNo,
                            FormCode = shipment.Form.Code,
                            FormName = shipment.Form.Name,
                            CultivarName = shipment.Cultivar.Name,
                            CultivarCode = shipment.Cultivar.Code,
                            ShipMethod = shipment.Order.ShipMethod.Name,
                            BoxNumber = sb.BoxNumber,
							ShipToName = shipment.Order.ShipTo.Name,
							ShipToStreet = shipment.Order.ShipTo.Street,
							ShipToCity = shipment.Order.ShipTo.City,
							ShipToState = shipment.Order.ShipTo.Region.Name,
							ShipToPostalCode = shipment.Order.ShipTo.PostalCode,
							ShipToPhone = shipment.Order.ShipTo.Phone,
                            BoxQuantity = sb.ShipmentBoxContent.Select(q => q.Quantity).Sum(),
                            BoxPctFull = (((double)sb.ShipmentBoxContent.Select(q => q.Quantity).Sum() / (double)sb.SoldCuttingCapacity) * 100),
                            BoxContentLineItems = sb.ShipmentBoxContent.Select(sbC => new BoxContentItemModel
                            {
                                FormCode = sbC.Material.Form.Code,
                                Quantity = sbC.Quantity,
                                VarietyCode = sbC.Material.VarietyCode,
                                VarietyName = sbC.Material.VarietyName
                            })
                        }).OrderBy(box => box.BoxNumber).ToList()))
                {
                    boxLabels.ForEach(bl => bl.TotalBoxes = boxLabels.Select(bx => bx.BoxNumber).LastOrDefault());
                    contents.AddRange(boxLabels);
                }
            }


		contents.Dump();		
					
					
				

}