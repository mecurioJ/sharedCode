<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.ConnectionInfo.dll</Reference>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SQL Server\120\SDK\Assemblies\Microsoft.SqlServer.Smo.dll</Reference>
  <GACReference>IBM.Data.DB2.iSeries, Version=12.0.0.0, Culture=neutral, PublicKeyToken=9cdb2ebfb1f93a26</GACReference>
  <GACReference>Microsoft.SqlServer.Management.Sdk.Sfc, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>IBM.Data.DB2.iSeries</Namespace>
  <Namespace>Microsoft.SqlServer.Management.Common</Namespace>
  <Namespace>Microsoft.SqlServer.Management.Smo</Namespace>
</Query>

void Main()
{
	Server sqlsass = new Server(new ServerConnection("sqlsass"));
	Database ECSDM_Staging = sqlsass.Databases["ECSDM_Staging"];
	
	//db.Tables.Count.Dump();

	DataSet as400Set = new DataSet();
	as400Set.ReadXml(@"B:\Projects\ECSDM\SSIS\as400Def.xml");
	
	//var tableName = "OLDCIMS";
	
	
	var tables = 
	as400Set.Tables["Columns"].Rows.Cast<DataRow>()
	//.Where(dr => dr.Field<String>("TABLE_NAME").ToUpper().Equals(tableName))
	.Where(dr => TableFilter.Contains(dr.Field<String>("TABLE_NAME").ToUpper()))
	.Select(dr => new{
		Schema = dr.Field<String>("TABLE_SCHEM"),
		TableName = dr.Field<String>("TABLE_NAME"),
		ColumnName = dr.Field<String>("COLUMN_NAME"),
		Remark = dr.Field<String>("Remarks"),
		ExtendedProperty = String.Format(@"EXEC sp_addextendedproperty @name = N'MS_Description', @value = '{0}',@level0type = N'Schema', @level0name = '{1}',@level1type = N'Table',  @level1name = '{2}', @level2type = N'Column', @level2name = '{3}';",
			dr.Field<String>("Remarks").Trim(),dr.Field<String>("TABLE_SCHEM").ToString(),dr.Field<String>("TABLE_NAME"),dr.Field<String>("COLUMN_NAME").ToString()),
		Catalog = dr.Field<String>("TABLE_CAT"),
		Table = dr.Field<String>("TABLE_NAME"),
		Alias = String.Format("{0} AS {1}",
			dr.Field<String>("COLUMN_NAME"),
			dr.Field<String>("Remarks")
			.Replace(" ",String.Empty)
			.Replace("\"",String.Empty)
			.Replace("/Ule",String.Empty)
			),
		Name = dr.Field<String>("COLUMN_NAME"),
		SqlDataType = OdbcConvert(dr.Field<String>("DATA_TYPE"),dr.Field<String>("TYPE_NAME"),dr.Field<String>("SQL_DATA_TYPE")),
		SmoDataType = SmoDataType(dr.Field<String>("DATA_TYPE"),dr.Field<String>("COLUMN_SIZE"),dr.Field<String>("DECIMAL_DIGITS"),dr.Field<String>("NUM_PREC_RADIX")),
		ColumnDataType = OdbConvert(dr.Field<String>("DATA_TYPE"),dr.Field<String>("COLUMN_SIZE"),dr.Field<String>("DECIMAL_DIGITS"),dr.Field<String>("NUM_PREC_RADIX")).Name,
		ColumnSize = dr.Field<String>("COLUMN_SIZE"),
		Scale = dr.Field<String>("DECIMAL_DIGITS"),
		Precision = dr.Field<String>("NUM_PREC_RADIX"),
		Nullable = dr.Field<String>("NULLABLE"),
		MetaData = dr.Field<String>("REMARKS"),
		IsNullable = dr.Field<String>("IS_NULLABLE")
	})
	.Where(w => !String.IsNullOrEmpty(w.Remark))
	.GroupBy(g => new{g.TableName,g.Schema}, g => new{g.Name, g.ColumnDataType, g.SmoDataType})
	.Dump()
	;
	
//	tables.Select(tb => new {tb.Key.Item3, ColumnCount = tb.Select(nm => nm.Name).Count()})
//	.Where(r => r.ColumnCount > 100)
//	.Dump();
//	
	
	foreach(var t in tables)
	{
		Table tb = new Table(ECSDM_Staging,t.Key.TableName,t.Key.Schema);
		foreach (var column in t.Select(col => new Column(tb,col.Name, col.SmoDataType)))
		{
			tb.Columns.Add(column);
		}
		
		tb.Create();
		
	}
	
	
	
}


public static List<String> TableFilter
{
	get{
		return new List<String>(new[]{"PGMDBASE"});
	}
}

public Microsoft.SqlServer.Management.Smo.DataType SmoDataType(String dataType, string columnSize, string scale, string precision)
{

	switch(dataType)
	{
		//1 CHAR 1 SQL_CHAR SqlDataType.Char
		case "1":
			return DataType.NVarChar(int.Parse(columnSize));
			break;
		//3 DECIMAL 3 SQL_DECIMAL SqlDataType.Decimal
		case "3":
			return DataType.Decimal(int.Parse(scale),int.Parse(precision));
			break;
		//-2 CHAR() FOR BIT DATA -2 SQL_BINARY SqlDataType.Binary	
		case "-2":
			return DataType.Binary(int.Parse(columnSize));
			break;
		//2 NUMERIC 2  SQL_NUMERIC SqlDataType.Numeric
		case "2":
			return DataType.Numeric(int.Parse(scale),int.Parse(precision));
			break;		
		//4 INTEGER 4 SQL_INTEGER SqlDataType.Int
		case "4":
			return DataType.Int;
			break;
		//12 VARCHAR 12 SQL_VARCHAR SqlDataType.VarChar
		case "12":
			return DataType.VarChar(int.Parse(columnSize));
			break;
		//91 DATE 9 SQL_TYPE_DATE SqlDataType.Date
		case "91":
			return DataType.Date;
			break;
		//92 TIME 9 SQL_TYPE_TIME SqlDataType.Time
		case "92":
			return DataType.Time(int.Parse(columnSize));
			break;
		//93 TIMESTAMP 9 SQL_TYPE_TIMESTAMP SqlDataType.DateTime2
		case "93":
			return DataType.DateTime2(int.Parse(scale));
			break;
		//7 REAL 7 SQL_REAL SqlDataType.Real
		case "7":
			return DataType.Real;
			break;
		//5 SMALLINT 5 SQL_SMALLINT  SqlDataType.SmallInt
		case "5":
			return DataType.SmallInt;
			break;
		//-5 BIGINT -5  SQL_BIGINT SqlDataType.BigInt		
		case "-5":
			return DataType.BigInt;
			break;
		
	}
	return DataType.Variant;
}

public DataType OdbConvert(String dataType, string columnSize, string scale, string precision)
{
	switch(dataType)
	{
		//1 CHAR 1 SQL_CHAR SqlDataType.Char
		case "1":
			return DataType.NVarChar(int.Parse(columnSize));
			break;
		//3 DECIMAL 3 SQL_DECIMAL SqlDataType.Decimal
		case "3":
			return DataType.Decimal(int.Parse(scale),int.Parse(precision));
			break;
		//-2 CHAR() FOR BIT DATA -2 SQL_BINARY SqlDataType.Binary	
		case "-2":
			return DataType.Binary(int.Parse(columnSize));
			break;
		//2 NUMERIC 2  SQL_NUMERIC SqlDataType.Numeric
		case "2":
			return DataType.Numeric(int.Parse(scale),int.Parse(precision));
			break;		
		//4 INTEGER 4 SQL_INTEGER SqlDataType.Int
		case "4":
			return DataType.Int;
			break;
		//12 VARCHAR 12 SQL_VARCHAR SqlDataType.VarChar
		case "12":
			return DataType.VarChar(int.Parse(columnSize));
			break;
		//91 DATE 9 SQL_TYPE_DATE SqlDataType.Date
		case "91":
			return DataType.Date;
			break;
		//92 TIME 9 SQL_TYPE_TIME SqlDataType.Time
		case "92":
			return DataType.Time(int.Parse(columnSize));
			break;
		//93 TIMESTAMP 9 SQL_TYPE_TIMESTAMP SqlDataType.DateTime2
		case "93":
			return DataType.DateTime2(int.Parse(scale));
			break;
		//7 REAL 7 SQL_REAL SqlDataType.Real
		case "7":
			return DataType.Real;
			break;
		//5 SMALLINT 5 SQL_SMALLINT  SqlDataType.SmallInt
		case "5":
			return DataType.SmallInt;
			break;
		//-5 BIGINT -5  SQL_BIGINT SqlDataType.BigInt		
		case "-5":
			return DataType.BigInt;
			break;
		
	}
	return DataType.Variant;



}

// Define other methods and classes here
public SqlDataType OdbcConvert(String dataType, String typeName, String sqlDataType)
{
	return
	Tuple.Create(dataType,typeName,sqlDataType).Equals(Tuple.Create("1","CHAR","1"))
		? SqlDataType.Char
		: Tuple.Create(dataType,typeName,sqlDataType).Equals(Tuple.Create("3","DECIMAL","3"))
			? SqlDataType.Decimal
			: Tuple.Create(dataType,typeName,sqlDataType).Equals(Tuple.Create("-2","CHAR() FOR BIT DATA","-2"))
				? SqlDataType.Binary
				: Tuple.Create(dataType,typeName,sqlDataType).Equals(Tuple.Create("2","NUMERIC","2"))
					? SqlDataType.Numeric
					: Tuple.Create(dataType,typeName,sqlDataType).Equals(Tuple.Create("4","INTEGER","4"))
						? SqlDataType.Int
						: Tuple.Create(dataType,typeName,sqlDataType).Equals(Tuple.Create("12","VARCHAR","12"))
							? SqlDataType.VarChar
							: Tuple.Create(dataType,typeName,sqlDataType).Equals(Tuple.Create("91","DATE","9"))
								? SqlDataType.Date
								: Tuple.Create(dataType,typeName,sqlDataType).Equals(Tuple.Create("92","TIME","9"))
									? SqlDataType.Time
									: Tuple.Create(dataType,typeName,sqlDataType).Equals(Tuple.Create("93","TIMESTAMP","9"))
										? SqlDataType.DateTime2
										: Tuple.Create(dataType,typeName,sqlDataType).Equals(Tuple.Create("7","REAL","7"))
											? SqlDataType.Real
											: Tuple.Create(dataType,typeName,sqlDataType).Equals(Tuple.Create("5","SMALLINT","5"))
												? SqlDataType.SmallInt
												: Tuple.Create(dataType,typeName,sqlDataType).Equals(Tuple.Create("-5","BIGINT","-5"))
													? SqlDataType.BigInt
													: SqlDataType.Variant;
	
}
/*
var Tables = new List<String>(new []{"XXNY02P","SCAFP","OLDCIMS","SHMTP","SCAFP_BKP","SSPCP",
		"SSBTP","SHRHP","SCAVP","SIEPP","HLUSP","SDEVP","SSSBP","SLKRP","HCNTP","XNYTCHNP","XNYTCHP",
		"SMODP","SSSTP","BEDSSBSRP","SSCRP","SCLKP","BEDSSENRP","HQTWP_B01","BEHAVE","SNTEP","SSTGP",
		"SSTMP","SCLRP","SABSP","SPCMP","HEEVP","SSCSP","SPCNP","SCLTP","ABRRP","SPCRP","SPCRPTMP",
		"SCMSP","HGNIP","SENRP","SACHP","SSCXP","SCOMP","HGN2P","SACHP14JUN","HGN2PBK","SACHP15FEB",
		"SPCSP","SSTPP","SPDMP","SPDMP0501","SCONP","ACSAP","SSDSP","SADMP","SPDSP","SPDSPTMP","SCRLP",
		"SCRLP0615","SSYRP","HPC1P","SCRLP0615B","SADRP","STAIP","SSFRP","STAVP","HSPSP","SPKGP","SCROP",
		"ADEFCAL","SFSNP","SSGPP","SATHP","STCHP","STCHPBK","HJB1P","SCRQP","SCRQP0617","SSHRP",
		"SGCHP","HEIDP","HCERP","HCERPV1","HCERPV2","SCRRP","SBELP","ADEFPCP","HJB2P","HTHSP","SBRSP",
		"STPFP","SCRTP","SGPAP","SGPAP_BKP","STRBP","HPRAP","SRAVP","HJTLCSV","SSACP","SGRDP","SBSRP",
		"LCAFP","LCAFP_BKP","LCAFP_BKP2","STUFAM","SBS2P","SBUSP","SCTMP","XXNY01P"});
*/


/*

1 CHAR 1 SQL_CHAR SqlDataType.Char
3 DECIMAL 3 SQL_DECIMAL SqlDataType.Decimal
-2 CHAR() FOR BIT DATA -2 SQL_BINARY SqlDataType.Binary
2 NUMERIC 2  SQL_NUMERIC SqlDataType.Numeric
4 INTEGER 4 SQL_INTEGER SqlDataType.Int
12 VARCHAR 12 SQL_VARCHAR SqlDataType.VarChar
91 DATE 9 SQL_TYPE_DATE SqlDataType.Date
92 TIME 9 SQL_TYPE_TIME SqlDataType.Time
93 TIMESTAMP 9 SQL_TYPE_TIMESTAMP SqlDataType.DateTime2
7 REAL 7 SQL_REAL SqlDataType.Real
5 SMALLINT 5 SQL_SMALLINT  SqlDataType.SmallInt
-5 BIGINT -5  SQL_BIGINT SqlDataType.BigInt
*/