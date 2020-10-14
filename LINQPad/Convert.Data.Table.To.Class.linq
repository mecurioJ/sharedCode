<Query Kind="Program">
  <Connection>
    <ID>f268594a-d7ea-477c-8f43-f7e6c4adfcdc</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>Consulting</Database>
  </Connection>
</Query>

void Main()
{
	Func<String,String> getYear = delegate(string s) { 
		var strArry = s.Split('_'); 
		return String.Format("{0}/{1}",strArry[0],strArry[1]);
	};
	
	
	
	getYear.Invoke("48_14_Available").Dump();
	
	

	DataSet ds = new DataSet();
	ds.ReadXml(XDocument.Load(@"C:\Projects\Heimos.Local\RCAvailabilityDisplay.xml").CreateReader());
	//ds.Dump();
	ds.Tables.Cast<DataTable>().FirstOrDefault().Columns.Cast<DataColumn>()
	.Select(dc => new{
		Name = dc.ColumnName,
		Type = dc.DataType.Name.ToString()
	}).Select(cl => String.Format("public {0} {1} {2}",cl.Type,cl.Name,"{ get; set;}"))
	.Dump();
	
	
//	ds.Tables.Cast<DataTable>().FirstOrDefault().Rows.Cast<DataRow>()
//            .Select(dr => String.Format("<tr><td>{0}</td></tr>",dr.ItemArray.Aggregate((p,n) => String.Format("{0}</td><td>{1}",p,n))))
//			.Aggregate((p,n) => String.Format("{0}{1}",p,n))
//	.Dump();
	
	}

// Define other methods and classes here

public class RCAvailabilityModel
{
	public String Supplier { get; set;} 
	public String Species { get; set;} 
	public String Variety { get; set;} 
	public String Name { get; set;} 
	public WeekDetail Week_1 {get;set;}
	public WeekDetail Week_2 {get;set;}
	public WeekDetail Week_3 {get;set;}
	public WeekDetail Week_4 {get;set;}
	public WeekDetail Week_5 {get;set;}
	public WeekDetail Week_6 {get;set;}
	public WeekDetail Week_7 {get;set;}
	public WeekDetail Week_8 {get;set;}
	public WeekDetail Week_9 {get;set;}
	public WeekDetail Week_10 {get;set;}
}

public class WeekDetail
{
	public String ShipWeek {get;set;}
	public int Available {get;set;}
	public int OnOrder {get;set;}
	public int Remaining {get;set;}
}