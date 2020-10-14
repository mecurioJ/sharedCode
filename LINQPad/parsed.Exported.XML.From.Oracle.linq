<Query Kind="Program">
  <NuGetReference>Soltys.ChangeCase</NuGetReference>
  <Namespace>Soltys.ChangeCase</Namespace>
</Query>

void Main()
{
	var dirPath = @"\\bvaserver\nas\Projects\PowerSchool Schemas\";
	var xtn = ".xml";
	var fileName = "PSSIS_Student_Discip_Summary";
	
	var schemaLoc = new DirectoryInfo(dirPath).GetFiles().Select(
		fi => 
		new{
			name = fi.Name.Replace(fi.Extension,string.Empty),
			sqlQuery = XElement.Load(fi.FullName)
		.Elements("DATA_RECORD").Select(dr => new{
		ColumnName = dr.Element("ColumnName").Value,
		Ordinal = dr.Element("ColID").Value,
		isPrimaryKey = (dr.Elements().Where(xn => xn.Name.LocalName.Equals("PK")).Count() > 0),
		isNullable =  (dr.Element("Null").Value.Equals("Y")),
		DataType = dr.Element("DataType").Value
		.Replace(" Char)",")")
		.Replace("VARCHAR2","NVARCHAR")
		.Replace("NUMBER (","NUMERIC (")
		.Replace(" (","(")
		.Replace("TIMESTAMP(3)","DATETIME")
		.Replace("TIMESTAMP","DATETIME2")
		.Replace("NUMERIC(3)","TINYINT")
		.Replace("NUMERIC(10)","INT")
		.Replace("NUMERIC(19)","BIGINT")
		
		,
		//source = dr
	})
	.OrderBy(col => col.Ordinal)
	.Select(col => string.Format("{2} {0} {1}",
	col.ColumnName,
	col.DataType,
	Int32.Parse(col.Ordinal).Equals(1) ? String.Empty : ","
	)).Aggregate((p,n) => String.Format("{0} {1}",p,n))
		}
	).Select(t => String.Format("CREATE TABLE [ps_inbound].[{0}]({1});",t.name,t.sqlQuery))
	
	.Dump();
	
//	var pathConcat = string.Format(@"{0}\{1}{2}",dirPath,fileName,xtn);
//	
//	XElement tableDef = XElement.Load(pathConcat);
//	
//	var tableDeftoSql = 
//	tableDef.Elements("DATA_RECORD").Select(dr => new{
//		ColumnName = dr.Element("ColumnName").Value.TitleCase(),
//		Ordinal = dr.Element("ColID").Value,
//		isPrimaryKey = (dr.Elements().Where(xn => xn.Name.LocalName.Equals("PK")).Count() > 0),
//		isNullable =  (dr.Element("Null").Value.Equals("Y")),
//		DataType = dr.Element("DataType").Value
//		.Replace(" Char)",")")
//		.Replace("VARCHAR2","VARCHAR")
//		.Replace("NUMBER (","NUMERIC (")
//		.Replace(" (","(")
//		.Replace("TIMESTAMP(3)","DATETIME")
//		.Replace("TIMESTAMP","DATETIME2")
//		.Replace("NUMERIC(3)","TINYINT")
//		.Replace("NUMERIC(10)","INT")
//		.Replace("NUMERIC(19)","BIGINT")
//		
//		,
//		//source = dr
//	})
//	.OrderBy(col => col.Ordinal)
//	.Select(col => string.Format("{2} {0} {1}",
//	col.ColumnName,
//	col.DataType,
//	Int32.Parse(col.Ordinal).Equals(1) ? String.Empty : ","
//	)).Aggregate((p,n) => String.Format("{0} {1}",p,n));
//	
//	String.Format("CREATE TABLE [ps_inbound].[{0}]({1})",fileName,tableDeftoSql).Dump();
}

// Define other methods and classes her
// Define other methods and classes here