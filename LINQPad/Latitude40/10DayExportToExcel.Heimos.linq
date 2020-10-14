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
	using(HeimosEntities ctx = new HeimosEntities())
	{
		ctx.Inventories.Dump();
	}
	
//	WeekBLL.GetWeek(DateTime.Parse("09/15/2014")).Dump();
//	
//	
//	
//	InventoryDAL.GetTenDayModelByShipWeek(new[]{DateTime.Parse("09/15/2014")})
//	.InjectStickWeek()
//	.Dump();
//	
	InventoryBLL.GetExportForExcel(DateTime.Parse("09/15/2014")).Select(t => t.ToExcelModel()).Dump();

}

// Define other methods and classes here

public static class ExtensionTest
{

public static IEnumerable<ProductionModel> InjectStickWeek(this IEnumerable<ProductionModel> iem)
{
	
	foreach(var i in iem)
	{
		i.StickWeek = WeekBLL.GetStickWeek(i.ShipWeek,LeadTimeBLL.GetLeadTimeInWeeks(i.MaterialGuid,i.ShipWeek));
	}
	return iem;
}

}