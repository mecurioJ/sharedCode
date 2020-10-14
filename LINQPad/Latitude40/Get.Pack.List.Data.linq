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
	
	using(HeimosEntities context = new HeimosEntities())
	{
	
		var shipmentBoxes = context.ShipmentBoxes.Include("ShipmentBoxContents")
			.SelectMany(sb => sb.ShipmentBoxContents.Select(sbc => new PackListBoxes{
				ShipmentGuid = sb.ShipmentGuid,
				MaterialGuid = sbc.MaterialGuid,
				BoxNumber = sb.BoxNumber,
				Quantity = sbc.Quantity
			}));
	
		var PackListReports = context.Orders
			.Include("Broker")
			.Include("ShipTo")
			.Include("ShipTo.Region")
			.Include("ShipTo.Country")
			.Include("Customer")
			.Include("Customer.Region")
			.Include("Customer.Country")
			.Include("Salesman")
			.Include("ShipMethod")
			.Include("Shipments")
			.Include("Shipments.ShipmentLines")
			.Include("Shipments.ShipmentLines.Material")
			.Include("Shipments.ShipmentLines.Material.Variety")
			.Include("Shipments.ShipmentLines.Material.Variety.Species")
			.Include("Shipments.ShipmentLines.Material.Form")
			.Include("Shipments.ShipmentBoxes")
			//.Include("Shipments.ShipmentBoxes.ShipmentBoxContents")
			.Where(pl => pl.Shipments.Any())
			.SelectMany(pl => pl.Shipments.Select(sh => new PackListReportModel{
				OrderDetail = new PackListOrderDetail{
					DeliveryWeek = pl.ShipWeek.Value,
					OrderNumber = pl.OrderNo,
					BrokerPO = pl.BrokerPO,
					CustomerPO = pl.CustomerPO,
					ShippingMethod = pl.ShipMethod.Name  + (pl.Shipments.Select(s => s.ProtectedRegion).FirstOrDefault() 
						? " (Protected)"
						: String.Empty),
					ShippingInstructions = pl.ShippingInstructions,
					SalesRep = pl.Salesman.Name +" - "+ pl.Salesman.Phone,
					BillTo = pl.Broker.Name
				},
				SoldTo = new PackListSoldTo{
					SoldToCode = pl.Customer.Code,
					SoldToName = pl.Customer.Name,
					SoldToStreet = pl.Customer.Street,
					SoldToCity = pl.Customer.City,
					SoldToState = pl.Customer.Region.Name,
					SoldToPostal = pl.Customer.PostalCode,
					SoldToPhone = pl.Customer.Phone
				},
				ShipTo = new PackListShipTo{
					ShipToCode = pl.ShipTo.Code,
					ShipToName = pl.ShipTo.Name,
					ShipToStreet = pl.ShipTo.Street,
					ShipToCity = pl.ShipTo.City,
					ShipToState = pl.ShipTo.Region.Name,
					ShipToPostal = pl.ShipTo.PostalCode,
					ShipToPhone = pl.ShipTo.Phone
				},
				LineItems = sh.ShipmentLines.Select(sl => new PackListLineItems{
				
					Breeder = sl.Material.Variety.Supplier.Name,
					ShipmentNumber = sh.ShipmentNumber,
					ShipDate = sh.ShipDate,
					SequenceWithinOrder = sh.SequenceWithinOrder,
					SequenceWithinWeek = sh.SequenceWithinWeek,
					TruckNumber = sh.TruckNumber,
					DropNumber = sh.DropNumber,
					Quantity = sl.Quantity, //Can sum in Report as Total Cuttings
					VarietyCode = sl.Material.Variety.Code,
					VarietyName = sl.Material.Variety.Name,
					SpeciesCode = sl.Material.Variety.Species.Code,
					SpeciesName = sl.Material.Variety.Species.Name,
					FormCode = sl.Material.Form.Code,
					FormName = sl.Material.Form.Name,
					ShipmentBoxes = shipmentBoxes.Where(sb => sb.ShipmentGuid.Equals(sh.Guid) && sb.MaterialGuid.Equals(sl.MaterialGuid))
						.Select(num => num.BoxNumber.ToString()).ToList(),
					TotalBoxes = shipmentBoxes.Where(sb => sb.ShipmentGuid.Equals(sh.Guid)).Count(),
					Liners = int.MinValue //Can sum int Report as Total Boxes
						}).ToList()
					}))
			;
		PackListReports.Dump();

	}
	
	
	
}



public class PackListReportModel
{
	public PackListOrderDetail OrderDetail {get;set;}
	public PackListSoldTo SoldTo {get;set;}
	public PackListShipTo ShipTo {get;set;}
	public List<PackListLineItems> LineItems {get;set;}
}

public class PackListBoxes
{
	public Guid ShipmentGuid {get;set;}
	public Guid MaterialGuid {get;set;}
	public int BoxNumber {get;set;}
	public int Quantity {get;set;}
}

public class PackListLineItems
{
	public String Breeder {get;set;}
	public String ShipmentNumber {get;set;}
	public DateTime? ShipDate {get;set;}
	public int SequenceWithinOrder {get;set;}
	public int SequenceWithinWeek {get;set;}
	public String TruckNumber {get;set;}
	public String DropNumber {get;set;}
	public int Quantity {get;set;}
	public String VarietyCode {get;set;}
	public String VarietyName {get;set;}
	public String SpeciesCode {get;set;}
	public String SpeciesName {get;set;}
	public String FormCode {get;set;}
	public String FormName {get;set;}
	public List<String> ShipmentBoxes  {get;set;}
	public String BoxNumbers {get;set;}
	public int TotalBoxes {get;set;}
	public int Liners {get;set;}
}

public class PackListOrderDetail
{
	public DateTime DeliveryWeek {get;set;}
	public int OrderNumber {get;set;}
	public String BrokerPO {get;set;}
	public String CustomerPO {get;set;}
	public String ShippingMethod {get;set;}
	public String ShippingInstructions {get;set;}
	public String SalesRep {get;set;}
	public String BillTo {get;set;}
}

public class PackListSoldTo
{
	public int SoldToCode {get;set;}
	public String SoldToName {get;set;}
	public String SoldToStreet {get;set;}
	public String SoldToCity {get;set;}
	public String SoldToState {get;set;}
	public String SoldToPostal {get;set;}
	public String SoldToPhone {get;set;}
}

public class PackListShipTo
{
	public String ShipToCode {get;set;}
	public String ShipToName {get;set;}
	public String ShipToStreet {get;set;}
	public String ShipToCity {get;set;}
	public String ShipToState {get;set;}
	public String ShipToPostal {get;set;}
	public String ShipToPhone {get;set;}
}


// Define other methods and classes here

public static IEnumerable<DateTime> ShipWeeks
{
	get{
		return new []{
			DateTime.Parse("11/3/2014 12:00:00 AM"), 
			DateTime.Parse("11/3/2014 12:00:00 AM"), 
			DateTime.Parse("10/27/2014 12:00:00 AM"), 
			DateTime.Parse("11/24/2014 12:00:00 AM"), 
			DateTime.Parse("11/17/2014 12:00:00 AM"), 
			DateTime.Parse("8/18/2014 12:00:00 AM"), 
			DateTime.Parse("12/15/2014 12:00:00 AM"), 
			DateTime.Parse("1/5/2015 12:00:00 AM"), 
			DateTime.Parse("6/9/2014 12:00:00 AM"), 
			DateTime.Parse("6/30/2014 12:00:00 AM"), 
			DateTime.Parse("12/29/2014 12:00:00 AM"), 
			DateTime.Parse("12/1/2014 12:00:00 AM"), 
			DateTime.Parse("10/13/2014 12:00:00 AM"), 
			DateTime.Parse("11/10/2014 12:00:00 AM"), 
			DateTime.Parse("6/23/2014 12:00:00 AM"), 
			DateTime.Parse("9/29/2014 12:00:00 AM"), 
			DateTime.Parse("10/20/2014 12:00:00 AM"), 
			DateTime.Parse("2/16/2015 12:00:00 AM"), 
			DateTime.Parse("5/19/2014 12:00:00 AM"), 
			DateTime.Parse("12/8/2014 12:00:00 AM"), 
			DateTime.Parse("5/26/2014 12:00:00 AM"), 
			DateTime.Parse("5/12/2014 12:00:00 AM"), 
			DateTime.Parse("6/16/2014 12:00:00 AM"), 
			DateTime.Parse("10/6/2014 12:00:00 AM"), 
			DateTime.Parse("1/19/2015 12:00:00 AM"), 
			DateTime.Parse("1/12/2015 12:00:00 AM"), 
			DateTime.Parse("7/21/2014 12:00:00 AM")
		}.ToArray();
	}
}

public static DataSet OrderSet
{
	get{
			//Reporting DataSet
	DataSet PackListSet = new DataSet("PackListSet");
	
	//Order Detail Table
	DataTable OrderTable = new DataTable("OrderTable");
	OrderTable.Columns.AddRange(new []{
		new DataColumn("OrderGuid",typeof(String)), //Guid
		new DataColumn("SellerCode",typeof(String)), //We are going to use the information from the screenshot here.
		new DataColumn("SellerName",typeof(String)), //We are going to use the information from the screenshot here.
		new DataColumn("SellerStreet",typeof(String)), //We are going to use the information from the screenshot here.
		new DataColumn("SellerCity",typeof(String)), //We are going to use the information from the screenshot here.
		new DataColumn("SellerState",typeof(String)), //We are going to use the information from the screenshot here.
		new DataColumn("SellerPostalCode",typeof(String)), //We are going to use the information from the screenshot here.
		new DataColumn("SellerCountry",typeof(String)), //We are going to use the information from the screenshot here.
		new DataColumn("SellerPhoneNumber",typeof(String)), //We are going to use the information from the screenshot here.
		new DataColumn("SellerFaxNumber",typeof(String)), //We are going to use the information from the screenshot here.
		new DataColumn("SellerWATSNumber",typeof(String)), //We are going to use the information from the screenshot here.

		new DataColumn("BillTo",typeof(String)), //Customer?
		new DataColumn("SalesRep",typeof(String)), //SalesmanGuid
				
		new DataColumn("DeliveryWeekDate",typeof(DateTime)), //Don't know where this is coming from
		new DataColumn("DeliveryWeekFormatted",typeof(String)), //Don't know where this is coming from
		new DataColumn("OrderNumber",typeof(String)), //OrderNo
		new DataColumn("BrokerPO",typeof(String)), //BrokerPO
		new DataColumn("CustomerPO",typeof(String)), // CustomerPO
		new DataColumn("ShippingMethod",typeof(String)), //need to include or join the ShipTo Here...
		
		
	});
	OrderTable.PrimaryKey = new DataColumn[]{OrderTable.Columns["OrderGuid"]};
	
	//Ship To Table	
	DataTable ShipToTable = new DataTable("ShipToTable");
	ShipToTable.Columns.AddRange(new []{
		new DataColumn("OrderGuid",typeof(String)),
		new DataColumn("Code",typeof(String)),
		new DataColumn("Name",typeof(String)),
		new DataColumn("Street",typeof(String)),
		new DataColumn("City",typeof(String)),
		new DataColumn("State",typeof(String)),
		new DataColumn("PostalCode",typeof(String)),
		new DataColumn("Country",typeof(String)),
		new DataColumn("PhoneNumber",typeof(String)),
	});
	
	
	//Sold to Table
	DataTable SoldToTable = new DataTable("SoldToTable");
	SoldToTable.Columns.AddRange(new []{
		new DataColumn("OrderGuid",typeof(String)),
		new DataColumn("Code",typeof(String)),
		new DataColumn("Name",typeof(String)),
		new DataColumn("Street",typeof(String)),
		new DataColumn("City",typeof(String)),
		new DataColumn("State",typeof(String)),
		new DataColumn("PostalCode",typeof(String)),
		new DataColumn("Country",typeof(String)),
		new DataColumn("PhoneNumber",typeof(String)),
	});
	
	
	//Order Lines Table
	DataTable OrderLinesTable = new DataTable("OrderLinesTable");
	OrderLinesTable.Columns.AddRange(new[]{
		new DataColumn("OrderGuid",typeof(String)),
		new DataColumn("Breeder",typeof(string)),
		new DataColumn("Form",typeof(string)),
		new DataColumn("Species",typeof(string)),
		new DataColumn("Variety",typeof(string)),
		new DataColumn("Description",typeof(string)),
		new DataColumn("Cuttings",typeof(int)),
		new DataColumn("Liners",typeof(int)),
		new DataColumn("BoxNumbers",typeof(string))
		});
	
	PackListSet.Tables.AddRange(new[]{
		OrderTable,
		ShipToTable,
		SoldToTable,
		OrderLinesTable
	});
	
	
	DataRelation navOrderToShipTo = new DataRelation("OrderToShipTo",
		PackListSet.Tables["OrderTable"].Columns["OrderGuid"],
		PackListSet.Tables["ShipToTable"].Columns["OrderGuid"]
		);


	DataRelation navOrderToSoldTo = new DataRelation("OrderToSoldTo",
		PackListSet.Tables["OrderTable"].Columns["OrderGuid"],
		PackListSet.Tables["SoldToTable"].Columns["OrderGuid"]
		);		


	DataRelation navOrderToOrderLines = new DataRelation("OrderToOrderLines",
		PackListSet.Tables["OrderTable"].Columns["OrderGuid"],
		PackListSet.Tables["OrderLinesTable"].Columns["OrderGuid"]
		);			
		
	PackListSet.Relations.AddRange(new[]{navOrderToShipTo,navOrderToSoldTo,navOrderToOrderLines});
	
	return PackListSet;
	
	}
}