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
	
	var tubes = Latitude.Heimos.ReportingService.Data.ReportDataSource.GetTubeLabels(shipWeek,shipDate,shipMethod,isLateOrder,trucks);
	
	tubes
	                .OrderBy(t => System.Tuple.Create(
                    t.CultivarCode,
                    t.FormCode,
                    (t.TubeLabelContents.Count().Equals(1) && 
						t.TubeLabelContents.FirstOrDefault().Quantity.Equals(t.TubeCapacity)) ? 1 : 2,
                    //t.TubeLabelContents.FirstOrDefault().SpeciesName, 
                    t.TubeLabelContents.FirstOrDefault().VarietyCode))
	.Dump();
	
/*
CultivarCode
FormCode
TubeLineItems.Count = 1 and TubeIsCompletelyFull
StickWeekDate
SpeciesName
VarietyCode
 
 */
	
//	List<Latitude.Heimos.Entities.Shipment> shipments = new List<Latitude.Heimos.Entities.Shipment>();
//	
//	
//	        using (HeimosEntities context = new HeimosEntities())
//            {
//				shipments = 
//                context.Shipments.Include("Order")
//                .Include("ShipmentBoxes")
//                .Include("ShipmentBoxes.ShipmentBoxContents")
//                .Include("ShipmentBoxes.ShipmentBoxContents.Material")
//                .Include("ShipmentBoxes.ShipmentBoxContents.Material.Variety")
//                .Include("ShipmentBoxes.ShipmentBoxContents.Material.Variety.Species")
//                .Where(o => (excludeUndefinedShipDates ? o.ShipDate.HasValue : true)
//                    && ((o.Order.ShipWeek ?? DateTime.MinValue) == shipWeek)
//					).Distinct().ToList()
//					.ToModelEntityList()
//					;
//					
//					shipments.Dump();
//            }
			
}

// Define other methods and classes here