<Query Kind="Program">
  <Namespace>System.Xml.Schema</Namespace>
</Query>

protected static XNamespace schema = "http://www.w3.org/2001/XMLSchema"; 
protected static XNamespace annotation = "http://www.w3.org/2001/XMLSchema" ;
protected static XNamespace appinfo = "http://www.w3.org/2001/XMLSchema" ;
protected static XNamespace DataSource = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace Connections = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace Connection = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace Tables = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace TableAdapter = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace MainSource = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace DbSource = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace InsertCommand = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace DbCommand = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace CommandText = "urn:schemas-microsoft-com:xml-msdatasource"; 
protected static XNamespace Parameters = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace Parameter = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace SelectCommand = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace Mappings = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace Mapping = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace Sources = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace UpdateCommand = "urn:schemas-microsoft-com:xml-msdatasource" ;
protected static XNamespace element = "http://www.w3.org/2001/XMLSchema" ;
protected static XNamespace complexType = "http://www.w3.org/2001/XMLSchema" ;
protected static XNamespace choice = "http://www.w3.org/2001/XMLSchema" ;
protected static XNamespace sequence = "http://www.w3.org/2001/XMLSchema" ;
protected static XNamespace simpleType = "http://www.w3.org/2001/XMLSchema"; 
protected static XNamespace restriction = "http://www.w3.org/2001/XMLSchema"; 
protected static XNamespace maxLength = "http://www.w3.org/2001/XMLSchema" ;


void Main()
{
	
	FileInfo[] fileList = new DirectoryInfo(@"C:\Projects\SVN\trunk").GetFiles("*.xsd", SearchOption.AllDirectories);
	List<DataSetSelectItem> DataSetItems = new List<DataSetSelectItem>();
	foreach(var fi in fileList)
	{
		
		XDocument xsd = XDocument.Load(fi.FullName);
		DataSetItems.AddRange(
		xsd.Descendants().Where(xn => xn.Name.Namespace.Equals(CommandText))
		.Select(d => new {
		FileName = fi.FullName,
		ParentParent = d.Parent.Parent.Attributes().Where(xn => xn.Name.LocalName.Equals("DbObjectName")).Select(V => V.Value).FirstOrDefault(),
		//d.Parent.Parent.Attribute("DbObjectName").Name.NamespaceName,
		Parent = d.Parent.Name.LocalName,
		d.Name.LocalName,
		d.Value
		}).Where(ln => ln.Parent.Equals("SelectCommand") &
		ln.LocalName.Equals("DbCommand")).Where(v => !String.IsNullOrEmpty(v.Value))
		.Select(dt => new DataSetSelectItem
		{
			FileName = dt.FileName,
			DataObjectName = dt.ParentParent,
			CommandType = dt.LocalName,
			CommandText = dt.Value
		}));
	}
	
	DataSetItems.GroupBy(di => di.FileName)
	//.Where(fi => fi.Key.Contains("Discrepant"))
	.Dump();

}

// Define other methods and classes here

class DataSetSelectItem
{
	public String FileName {get;set;}
	public String DataObjectName {get;set;}
	public String CommandType {get;set;}
	public String CommandText {get;set;}
}