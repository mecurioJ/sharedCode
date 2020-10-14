<Query Kind="Program">
  <Connection>
    <ID>57b8e721-62f2-4f6a-88ff-6c40878b38a8</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>SNLBanker_ApplicationDB</Database>
  </Connection>
</Query>

protected static XNamespace engine = "http://schemas.microsoft.com/analysisservices/2003/engine";
protected static XNamespace engine200 = "http://schemas.microsoft.com/analysisservices/2010/engine/200";
protected static XNamespace engine300 = "http://schemas.microsoft.com/analysisservices/2011/engine/300";
protected static XNamespace engine2 = "http://schemas.microsoft.com/analysisservices/2003/engine/2";
protected static XNamespace xs = "http://www.w3.org/2001/XMLSchema";
protected static XNamespace xsi="http://www.w3.org/2001/XMLSchema-instance";
protected static XNamespace msData = "urn:schemas-microsoft-com:xml-msdata";
protected static XNamespace diffGram = "urn:schemas-microsoft-com:xml-diffgram-v1";

void Main()
{

			
	var dsViews = DataSourceViews(XElement.Load(@"c:\projects\DataSourceViews.xml"));
	dsViews.Dump();
//		DataSourceViews.Descendants(engine+"DataSourceViews")
//		.Elements(engine+"DataSourceView")
//		.Select(dsv => new DataSourceViewSource(dsv))
//		.Select(dsX => new DataSourceViewDetail(dsX))
//		.ToDictionary(t => t.DataSourceViewID, t => t).Dump();
}

// Define other methods and classes here
public String GetElementValue(XElement xe)
{
	return String.Empty;
}

public Dictionary<String, DataSourceViewDetail> DataSourceViews(XElement Source)
{
	return Source.Descendants(engine+"DataSourceViews")
	.Elements(engine+"DataSourceView")
	.Select(dsv => new DataSourceViewSource(dsv))
	.Select(dsX => new DataSourceViewDetail(dsX))
	.ToDictionary(t => t.DataSourceViewID, t => t);
}

public class DataSourceViewDetail
{	
	public DataSourceViewDetail(DataSourceViewSource dsX){
		DataSourceViewID = dsX.DataSourceViewID;
		Name = dsX.Name;
		StandAloneTables = 
			dsX.Schema.Tables.Cast<DataTable>().Select(tn => tn.TableName)
			.Except(dsX.Schema.Relations.Cast<DataRelation>().Select(dr => dr.ParentTable.TableName))
			.Except(dsX.Schema.Relations.Cast<DataRelation>().Select(dr => dr.ChildTable.TableName)).ToArray();
		Tables = dsX.Schema.Tables.Cast<DataTable>().Select(tn => tn.TableName).ToArray();
		Relations = dsX.Schema.Relations.Cast<DataRelation>().Select(dr => new RelationDetail(dr)).ToDictionary(d => d.Name,d=> d);
	}
	public string DataSourceViewID {get;set;}
	public string Name {get;set;}
	public string[] StandAloneTables {get;set;}
	public string[] Tables {get;set;}
	public Dictionary<string,RelationDetail> Relations {get;set;}
}

public class DataSourceViewSource
{
	public DataSourceViewSource() {}
	public DataSourceViewSource(XElement dsv) 
	{
	Func<XElement, DataSet> DataViewSchema = schema => {
			//var r = XElement.Parse(schema);
			var ds = new DataSet();
			ds.ReadXmlSchema(schema.CreateReader());
			return ds;
			};
		Name = dsv.Element(engine+"Name").Value;
		DataSourceViewID = dsv.Element(engine+"ID").Value;
		Schema = DataViewSchema(dsv.Element(engine+"Schema"));
	}
	
	public string DataSourceViewID {get;set;}
	public string Name {get;set;}
	public DataSet Schema {get;set;}
}

public class RelationDetail
{
	public RelationDetail(){}
	public RelationDetail(DataRelation dr)
	{
	Name = dr.RelationName;
			ParentTableName = dr.ParentTable.TableName;
			ParentTableColumns = dr.ParentTable.Columns.Cast<DataColumn>().Select(pc => new KeyValuePair<String,String>(
				pc.ColumnName, 
				pc.DataType.ToString()
				)).ToDictionary(t => t.Key, t => t.Value);
			ParentRelationColumns = dr.ParentColumns.Cast<DataColumn>().Select(pc => pc.ColumnName).ToArray();
			ChildTableName = dr.ChildTable.TableName;
			ChildTableColumns = dr.ChildTable.Columns.Cast<DataColumn>().Select(pc => new KeyValuePair<String,String>(
				pc.ColumnName, 
				pc.DataType.ToString()
				)).ToDictionary(t => t.Key, t => t.Value);
			ChildRelationColumns = dr.ChildColumns.Cast<DataColumn>().Select(cc => cc.ColumnName).ToArray();
	}
	

	public String Name {get;set;}
	public String ParentTableName {get;set;}
	public Dictionary<string,string> ParentTableColumns {get;set;}
	public string[] ParentRelationColumns {get;set;}
	public String ChildTableName {get;set;}
	public Dictionary<string,string> ChildTableColumns {get;set;}
	public string[] ChildRelationColumns {get;set;}
}