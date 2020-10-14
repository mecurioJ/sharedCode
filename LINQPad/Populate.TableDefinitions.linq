<Query Kind="Program">
  <Connection>
    <ID>0799912f-9f83-4973-96a1-f8730f5f7db7</ID>
    <Persist>true</Persist>
    <Server>sqlsass</Server>
    <Database>ECSDMTools</Database>
  </Connection>
</Query>

void Main()
{
	//db.Tables.Count.Dump();

	DataSet as400Set = new DataSet();
	as400Set.ReadXml(@"B:\Projects\ECSDM\SSIS\as400Def.xml");
	
	var tableDefs = as400Set.Tables["Tables"];
	var viewDefs =  as400Set.Tables["Views"];
	var colDefs =  as400Set.Tables["Columns"];
	
	var tableRows = tableDefs.Rows.Cast<DataRow>().Select(dr => new Table{
			TABLE_CAT = dr.Field<String>("TABLE_CAT"),
			REMARKS= dr.Field<String>("REMARKS"),
			TABLE_NAME = dr.Field<String>("TABLE_NAME"),
			TABLE_SCHEM = dr.Field<String>("TABLE_SCHEM"),
			TABLE_TYPE = dr.Field<String>("TABLE_TYPE")
		});
		
	var viewRows = viewDefs.Rows.Cast<DataRow>().Select(dr => new Views{
			TABLE_CAT = dr.Field<String>("TABLE_CAT"),
			REMARKS= dr.Field<String>("REMARKS"),
			TABLE_NAME = dr.Field<String>("TABLE_NAME"),
			TABLE_SCHEM = dr.Field<String>("TABLE_SCHEM"),
			TABLE_TYPE = dr.Field<String>("TABLE_TYPE")
		});
	
	var colRows = colDefs.Rows.Cast<DataRow>().Select(dr => new CPDATA_Columns{
			TABLE_CAT = dr.Field<String>("TABLE_CAT"),
			TABLE_SCHEM = dr.Field<String>("TABLE_SCHEM"),
			TABLE_NAME = dr.Field<String>("TABLE_NAME"),
			COLUMN_NAME = dr.Field<String>("COLUMN_NAME"),
			DATA_TYPE = dr.Field<String>("DATA_TYPE"),
			TYPE_NAME = dr.Field<String>("TYPE_NAME"),
			COLUMN_SIZE = dr.Field<String>("COLUMN_SIZE"),
			BUFFER_LENGTH = dr.Field<String>("BUFFER_LENGTH"),
			DECIMAL_DIGITS = dr.Field<String>("DECIMAL_DIGITS"),
			NUM_PREC_RADIX = dr.Field<String>("NUM_PREC_RADIX"),
			NULLABLE = dr.Field<String>("NULLABLE"),
			REMARKS = dr.Field<String>("REMARKS"),
			COLUMN_DEF = dr.Field<String>("COLUMN_DEF"),
			SQL_DATA_TYPE = dr.Field<String>("SQL_DATA_TYPE"),
			SQL_DATETIME_SUB = dr.Field<String>("SQL_DATETIME_SUB"),
			CHAR_OCTET_LENGTH = dr.Field<String>("CHAR_OCTET_LENGTH"),
			ORDINAL_POSITION = dr.Field<String>("ORDINAL_POSITION"),
			IS_NULLABLE = dr.Field<String>("IS_NULLABLE")
	});
	
	Table.InsertAllOnSubmit(tableRows);
	Views.InsertAllOnSubmit(viewRows);
	CPDATA_Columns.InsertAllOnSubmit(colRows);
	
	SubmitChanges();
	

	
	
}
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