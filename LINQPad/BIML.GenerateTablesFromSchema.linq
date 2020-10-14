<Query Kind="Program">
  <Connection>
    <ID>8c1c2690-b1e8-4c43-b8bb-e9b9c3f40af9</ID>
    <Persist>true</Persist>
    <Server>sqlsass</Server>
    <SqlSecurity>true</SqlSecurity>
    <Database>ECSDMTools</Database>
    <UserName>mjfilichia</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAABVavVz32YkWhd6P74RyWeAAAAAACAAAAAAAQZgAAAAEAACAAAADIS53j6PDEYFFYKrqYh/d8HVQ6Rzv05gHO55i+cyPpLAAAAAAOgAAAAAIAACAAAABlEvho5IsufJ7tbaoUyl6n6/PcumSlyoe7HB2OcpxKghAAAABh5H9ypuWx5ayQa3GALZcPQAAAANbn22It4GQvcCmeNd2O74pBHmFB5lww1nCUUZ9zqRKZ1OvEdrbcS3LN1rOZO2T8+dqGed+JXQTRJQGSGNJ71xE=</Password>
  </Connection>
</Query>

void Main()
{
	var XDoc = new XElement("Tables");
	
	
	
	var Source = 
	Columns
	.Where(cl => !cl.DATA_TYPE.Contains("NULL"))
	.Select(
		cl => new{
			
			cl.TABLE_NAME,
			Element = EvalBIMLColumn(
				cl.COLUMN_NAME,
				cl.DATA_TYPE,
				cl.NUMERIC_PRECISION,
				cl.NUMERIC_PRECISION_RADIX,
				cl.NUMERIC_SCALE,
				((int)cl.LENGTH).ToString()
			)}
	);
	
	var biml = Source.GroupBy(tt => tt.TABLE_NAME, tt => tt.Element)
	.Select(xl => new XElement("Table",
		new XAttribute("Name",xl.Key),
		new XAttribute("SchemaName","as400"),
		//new XAttribute("ConnectionName","sqlsass.ECSDM_Staging1"),
		new XElement("Columns",xl)));
		
		
	var bimlHeader = new XElement("Biml",
		//new XAttribute("xmlns",new[]{@"http://schemas.varigence.com/biml.xsd"}),
		new XElement("Connections",
			new XElement("OleDBConnection",
				new XAttribute("Name", "Source"),
				new XAttribute("ConnectionString", "Data Source=172.16.8.219;User ID=brightview;Initial Catalog=S102187C;Provider=IBMDA400.DataSource.1;Persist Security Info=True;")
			),
			new XElement("OleDBConnection",
				new XAttribute("Name", new[]{"Target"}),
				new XAttribute("ConnectionString", new[]{"Data Source=sqlsass;Initial Catalog=ECSDM_Staging;Provider=SQLNCLI11.1;Integrated Security=SSPI;Auto Translate=False;"})
			)
		),
		new XElement("Databases",
			new XElement("OleDBConnection",
				new XAttribute("Name", new[]{"Source"}),
				new XAttribute("ConnectionString", new[]{"Data Source=172.16.8.219;User ID=brightview;Initial Catalog=S102187C;Provider=IBMDA400.DataSource.1;Persist Security Info=True;"})
			),
			new XElement("OleDBConnection",
				new XAttribute("Name", new[]{"Target"}),
				new XAttribute("ConnectionString", new[]{"Data Source=sqlsass;Initial Catalog=ECSDM_Staging;Provider=SQLNCLI11.1;Integrated Security=SSPI;Auto Translate=False;"})
			)),
		new XElement("Schemas",
			new XElement("OleDBConnection",
				new XAttribute("Name", new[]{"CPDATA"}),
				new XAttribute("ConnectionName", new[]{"Source"})
			),
			new XElement("OleDBConnection",
				new XAttribute("Name", new[]{"ECSDM_Staging"}),
				new XAttribute("ConnectionName", new[]{"Target"})
			))
		
	);	
		
	bimlHeader.Dump();		
		
		
//	.GroupBy(tt => tt.TABLE_NAME, tt => new{
//			 columnName = EvalBIMLColumn(
//			 	tt.COLUMN_NAME,
//				tt.DATA_TYPE,
//				tt.NUMERIC_PRECISION,
//				tt.NUMERIC_PRECISION_RADIX, 
//				tt.NUMERIC_SCALE,
//				((int)tt.LENGTH).ToString()),
//			 etc = String.Format("{0} {1} {2}",tt.ORDINAL_POSITION.Equals(1) ? String.Empty: ",", tt.COLUMN_NAME, tt.DataConversion)
//		})
//	.Select(k => new XElement("Table",
//		new XAttribute("Name",k.Key),
//		new XAttribute("SchemaName","as400"),
//		//new XAttribute("ConnectionName","sqlsass.ECSDM_Staging1"),
//		new XElement("Columns",k.Select(x => x.columnName))))
//	.Count().Dump();
	
	
	
	//XDoc.Save(@"\\etlsvr\e$\Districts\Middletown\Data Analysis\AS400.To.Staging\AS400.To.Staging\Tables.biml");
	
	
}

public XElement EvalBIMLColumn(String columnName, string dataType, string precision, string radix, string scale, string len)
{
	var col = new XElement("Column",
					new XAttribute("Name",columnName));
		var DataType = EvalBIMLType(dataType);
		
		col.Add(new XAttribute("DataType",DataType));
		
		if(DataType.Equals("String"))
		{
			col.Add(new XAttribute("Length",len));
		}
		
		if(DataType.Equals("Decimal"))
		{
			col.Add(new XAttribute("Precision",radix));
			col.Add(new XAttribute("Scale",scale));
		}
	
	return col;
}

// Define other methods and classes here
public string EvalBIMLType(string dType)
{
	return 		dType.Trim().Contains("CHAR")
					? "String"
					: dType.Trim().Contains("DECIMAL")
						? "Decimal"
						: dType.Trim().Contains("FLOAT")
							? "Double"
							: dType.Trim().Contains("INTEGER")
								? "Int32"
								: dType.Trim().Contains("DATE")
									? "Date"
									: dType.Trim().Contains("BIGINT")
										? "Int64"
										: dType.Trim().Contains("TIMESTMP")
											? "DateTime2"
											: dType.Trim().Contains("TIME")
												? "Time"
												: dType.Trim().Contains("NUMERIC")
													? "Decimal"
													: dType.Trim().Contains("SMALLINT")
														? "Int16"
														: dType;
}

public string EvalDataType(string dType, string precision, string radix, string scale, string len)
{
	return 				dType.Trim().Contains("VARCHAR")
				? String.Format("nvarchar({0})",len)
				: dType.Trim().Contains("CHAR")
					? String.Format("nvarchar({0})",len)
					: dType.Trim().Contains("DECIMAL")
						? String.Format("numeric({1},{2})",dType,radix,scale)
						: dType.Trim().Contains("FLOAT")
							? String.Format("float({1})",dType,precision)
							: dType.Trim().Contains("INTEGER")
								? "int"
								: dType.Trim().Contains("DATE")
									? "date"
									: dType.Trim().Contains("BIGINT")
										? "bigint"
										: dType.Trim().Contains("TIMESTMP")
											? "datetime2"
											: dType.Trim().Contains("TIME")
												? "time"
												: dType;
}
