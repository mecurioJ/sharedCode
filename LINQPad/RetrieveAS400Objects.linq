<Query Kind="Program">
  <Namespace>System.Data.Odbc</Namespace>
</Query>

void Main()
{
	String xmlSchemaLoc = @"B:\Projects\ECSDM\SSIS\";
	
	DataTable Tables;
	DataTable Views;
	DataTable Columns;
	
	DataSet SchemaSet = new DataSet("Schema");
	
	String oConn = "Dsn=QSDN_172.16.8.219;uid=brightview;pwd=bright123";
	OdbcConnection as400Conn = new OdbcConnection(oConn);
	
	as400Conn.Open();
	
	
	
	Tables = as400Conn.GetSchema("Tables");
	
	Tables.Rows.Dump();
//	Tables.TableName = "Tables";
//	
//	Views = as400Conn.GetSchema("Views");
//	Views.TableName = "Views";
//	
//	Columns = as400Conn.GetSchema("Columns");
//	Columns.TableName = "Columns";
//	
//	
//	SchemaSet.Tables.Add(Tables);
//	SchemaSet.Tables.Add(Views);
//	SchemaSet.Tables.Add(Columns);
//	as400Conn.Close();
//	
//	SchemaSet.Tables["Tables"].Rows.Count.Dump();
//	SchemaSet.Tables["Views"].Rows.Count.Dump();
//	
//	SchemaSet.Tables["Columns"].Rows.Count.Dump();
//	SchemaSet.WriteXml(xmlSchemaLoc+"as400Def.xml");
	
	//SchemaSet.WriteXmlSchema(xmlSchemaLoc+"as400Def.xml");
}

// Define other methods and classes here