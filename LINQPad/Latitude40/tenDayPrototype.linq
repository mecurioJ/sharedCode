<Query Kind="Program">
  <Connection>
    <ID>0f3a4d8a-4daa-45bd-ad1e-aa2479f35629</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Projects\Workspaces\Heimos\Latitude.Heimos\Latitude.Heimos.Web\bin\Latitude.Heimos.DAL.dll</CustomAssemblyPath>
    <CustomTypeName>Latitude.Heimos.DAL.HeimosEntities</CustomTypeName>
    <AppConfigPath>C:\Projects\Workspaces\Heimos\Latitude.Heimos\Latitude.Heimos.DAL\bin\Debug\Latitude.Heimos.DAL.dll.config</AppConfigPath>
  </Connection>
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
	var ShipWeeks = new[]{
		DateTime.Parse("11/24/2014"), 
		DateTime.Parse("9/8/2014"),
		DateTime.Parse("9/8/2014").AddDays(-7)
		};
	
	//Inventories.Dump();
	
	var shipWeek = ShipWeeks[1];
	
	var Guid = new[]{
		System.Guid.Parse("21f32f77-8c20-4ad7-957e-004816276120"),
		System.Guid.Parse("d34d5c3d-44fc-4a18-92f1-002860552863")
		};
	
	var first = InventoryDAL.GetTenDayModelByShipWeek(new[]{shipWeek}).FirstOrDefault();
	
	//var first = InventoryBLL.GetTenDayPageModel(ShipWeeks);
	//var first = InventoryBLL.GetTenDayPageModel(ShipWeeks,Guid);
	
	
	//Get the value of next week's data and prior weeks data
	
	first.Dump();
	var priorNext = InventoryDAL.GetPriorNextWeekCuttingsforTenDay(first);
	//first.CuttingsPulledFromPriorWeek = 
	
	priorNext.Where(k => k.Key.Equals(first.ShipWeek.AddDays(-7))).SelectMany(k => k.Where(m => m.MaterialGuid.Equals(first.MaterialGuid)))
	.Select(ds => new KeyValuePair<DateTime,int>(ds.ShipWeek,ds.CuttingsPushedToNextWeek)).FirstOrDefault().Dump();
	
	priorNext.Where(k => k.Key.Equals(first.ShipWeek.AddDays(7))).SelectMany(k => k.Where(m => m.MaterialGuid.Equals(first.MaterialGuid)))
	.Select(ds => new KeyValuePair<DateTime,int>(ds.ShipWeek,ds.CuttingsPushedToPriorWeek)).FirstOrDefault().Dump();
	
	//first.CuttingsPulledFromNextWeek = 
	priorNext.Where(k => k.Key.Equals(first.ShipWeek.AddDays(7))).Dump();
	
		
		
		



	
	
	//IEnumerable<Latitude.Heimos.DAL.Inventory> InventorySet = _getTenDayPriorNextWeekCuttings();
	
//	using (HeimosEntities context = new HeimosEntities())
//	{	
//		InventorySet = context.Inventories
//		.Include("Material")
//		.Where(i => WeekRange.Contains(i.ShipWeek)).ToList();
//	}
//	
//	InventorySet.Dump();
	
	
}	

// Define other methods and classes here

    public class TenDayPriorNextWeekCutting
    {
        public Guid InventoryGuid { get; set; }
        public Guid MaterialGuid { get; set; }
        public DateTime ShipWeekCurrent { get; set; }
        public DateTime ShipWeekPrior { get; set; }
        public DateTime ShipWeekNext { get; set; }
        public int CuttingsPulledFromPriorWeek { get; set; }
        public int CuttingsPulledFromNextWeek { get; set; }
        public int OverageCuttings { get; set; }
    }