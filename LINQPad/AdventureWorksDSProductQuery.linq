<Query Kind="Program">
  <Connection>
    <ID>7a4e7d40-0157-4052-965f-ef86408a5ee1</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>AdventureWorksDW2012</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	var SqlQuery = @"SELECT distinct Category.EnglishProductCategoryName
	,Category.EnglishProductSubcategoryName
	,EnglishProductName
	,Color
	,Size
	,SizeRange
	,Weight
	,ProductLine
	,ISNULL(StandardCost,0.00) StandardCost
	,ISNULL(ListPrice,0.00) ListPrice
	,DealerPrice
	,ModelName
	,EnglishDescription
FROM DimProduct
JOIN (
	SELECT Category.ProductCategoryKey
		,subCategory.ProductSubcategoryKey
		,Category.EnglishProductCategoryName
		,subCategory.EnglishProductSubcategoryName
	FROM DimProductSubcategory subCategory
	JOIN DimProductCategory Category ON subCategory.ProductCategoryKey = Category.ProductCategoryKey
	GROUP BY Category.ProductCategoryKey
		,subCategory.ProductSubcategoryKey
		,Category.EnglishProductCategoryName
		,subCategory.EnglishProductSubcategoryName
	) Category ON DimProduct.ProductSubcategoryKey = Category.ProductSubcategoryKey";
	SqlConnection sqlConn = ((SqlConnection) this.Connection);
	SqlCommand sqlCmd = new SqlCommand(SqlQuery);
	sqlCmd.Connection = sqlConn;
	sqlCmd.CommandType =  CommandType.Text;
	
	DataTable dt = new DataTable("ProductGen");
	SqlDataAdapter da = new SqlDataAdapter(SqlQuery,sqlConn.ConnectionString);
	
	sqlConn.Open();
	da.Fill(dt);
	sqlConn.Close();
	var ColumnSet = dt.Columns.Cast<DataColumn>().Select(dc => new{dc.ColumnName,DataType = dc.DataType.Name});

	dt.Rows.Cast<DataRow>().Select(dr => new Product{
			EnglishProductCategoryName = dr.Field<String>("EnglishProductCategoryName"), 
			EnglishProductSubcategoryName = dr.Field<String>("EnglishProductSubcategoryName"), 
			EnglishProductName = dr.Field<String>("EnglishProductName"), 
			Color = dr.Field<String>("Color"), 
			Size = dr.Field<String>("Size"), 
			SizeRange = dr.Field<String>("SizeRange"), 
			Weight = dr.Field<Double?>("Weight"), 
			ProductLine = dr.Field<String>("ProductLine"), 
			StandardCost = dr.Field<Decimal?>("StandardCost"), 
			ListPrice = dr.Field<Decimal?>("ListPrice"), 
			DealerPrice = dr.Field<Decimal?>("DealerPrice"), 
			ModelName = dr.Field<String>("ModelName"), 
			EnglishDescription = dr.Field<String>("EnglishDescription"), 
	}).ToList().Dump();
	
	ColumnSet.Select(cs => string.Format("public {0} {1} {2}",cs.DataType,cs.ColumnName,"{ get; set;}")).Dump();
	ColumnSet.Select(cs => string.Format("{1} = dr.Field<{0}>(\"{1}\"),",cs.DataType,cs.ColumnName,"{ get; set;}")).Dump();
	
	
	
}

// Define other methods and classes here


public class Product
{
	public String EnglishProductCategoryName { get; set;} 
	public String EnglishProductSubcategoryName { get; set;} 
	public String EnglishProductName { get; set;} 
	public String Color { get; set;} 
	public String Size { get; set;} 
	public String SizeRange { get; set;} 
	public Double? Weight { get; set;} 
	public String ProductLine { get; set;} 
	public Decimal? StandardCost { get; set;} 
	public Decimal? ListPrice { get; set;} 
	public Decimal? DealerPrice { get; set;} 
	public String ModelName { get; set;} 
	public String EnglishDescription { get; set;} 

}