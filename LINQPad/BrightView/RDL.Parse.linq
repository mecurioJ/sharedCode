<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
	var stringPath = @"C:\Projects\RDLs";
	var ReportsLoad = 
	System.IO.Directory.GetFiles(stringPath).Select(fi => new FileInfo(fi))
	.Where(fi => fi.Extension.Equals(".rdl"))
	.Select(fi => new{
		fi.Name,fi.FullName,fi.Extension
	});
	
	//XElement xReports = new XElement("Reports");
	
	List<XElement> reportList = new List<XElement>();
	
	
	
	reportList.Add(new XElement("Reports",ReportsLoad.Select(x => XElement.Load(x.FullName))));
	XElement Reports = new XElement("ReportItems",reportList);
	
	Reports.Elements().SelectMany(xn => xn.Elements(rDef+"Report").Select(xn1 => new Report(xn1)))
	.GroupBy(grp => grp.ReportID).Dump();
}


public static XNamespace rd = @"http://schemas.microsoft.com/SQLServer/reporting/reportdesigner";
public static XNamespace rDef = @"http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition";
public static XNamespace asq = @"http://schemas.microsoft.com/AnalysisServices/QueryDefinition";

public static String Parse(XAttribute attribute)
{
	return (attribute != null) ? attribute.Value : String.Empty;
}

public static String Parse(XElement element)
{
	return (element != null) ? element.Value : String.Empty;
}

public class DataSource
{
	public DataSource(){}
	
	public DataSource(XElement element)
	{
		Name = Parse(element.Attribute("Name"));
		DataSourceReference = Parse(element.Element(rDef+"DataSourceReference"));
		DataSourceId = Parse(element.Element(rd+"DataSourceID"));
		SecurityType = Parse(element.Element(rd+"SecurityType"));
	}
	
	public String Name {get;set;}
	public String DataSourceReference {get;set;}
	public String DataSourceId {get;set;}
	public String SecurityType {get;set;}
}

public class DataSet
{
	public DataSet(){}
	public DataSet(XElement element)
	{
		Name = Parse(element.Attribute("Name"));
		Fields = element.Descendants(rDef+"Field").Select(f => new Field(f));
		Query = element.Descendants(rDef+"Query").Select(q => new Query(q));
	}
	public String Name {get;set;}
	public IEnumerable<Field> Fields {get;set;}
	public IEnumerable<Query> Query {get;set;}
}

public class Field
{
	public Field(){}
	public Field (XElement element)
	{
		Name = Parse(element.Attribute("Name"));
		Type = Parse(element.Element(rd+"TypeName"));
	}
	public String Name {get;set;}
	public String Type {get;set;}
}


public class Query
{
	public Query(){}
	public Query(XElement element)
	{
		Name = Parse(element.Element(rDef+"DataSourceName"));
		Text = Parse(element.Element(rDef+"CommandText"));
		Parameters = element.Descendants(rDef+"QueryParameter").Select(qp => new QueryParameter(qp));
	}
	public String Name {get;set;}
	public String Text {get;set;}
	public IEnumerable<QueryParameter> Parameters {get;set;}
}

public class QueryParameter
{
	public QueryParameter(){}
	public QueryParameter (XElement element)
	{
		Name = Parse(element.Attribute("Name"));
		Type = Parse(element.Element(rDef+"Value"));
		//Parse(qp.Attribute("Name")),Parse(qp.Element(rDef+"Value"))
	}
	public String Name {get;set;}
	public String Type {get;set;}
}

public class ReportCell
{
	public ReportCell(){}
	public ReportCell(XElement element)
	{
		Name = Parse(element.Elements().FirstOrDefault().Attributes("Name").FirstOrDefault());
		Value = Parse(element.Descendants(rDef+"Value").FirstOrDefault());
		
	}
	public String Name {get;set;}
	public String Value {get;set;}
	
}

public class ReportParameter
{
	public ReportParameter (){}
	public ReportParameter (XElement element)
	{
		Name = Parse(element.Attribute("Name"));
		DataType = Parse(element.Element(rDef+"DataType"));
		AllowBlank = Parse(element.Element(rDef+"AllowBlank"));
		Prompt = Parse(element.Element(rDef+"Prompt"));
	}
	public String Name {get;set;}
	public String DataType {get;set;}
	public String AllowBlank {get;set;}
	public String Prompt {get;set;}
}
public class Report 
{
	public Report(){}
	public Report(XElement load)
	{
		DataSources = load.Descendants(rDef+"DataSource").Select(t => new DataSource(t));
		DataSets =  load.Descendants(rDef+"DataSet").Select(t => new DataSet(t));
		Body =   load.Descendants(rDef+"Body").Descendants(rDef+"CellContents").Select(cl => new ReportCell(cl));
		ReportParameters =   load.Descendants(rDef+"ReportParameters").Elements(rDef+"ReportParameter").Select(p => new ReportParameter(p));
		ReportID =   Parse(load.Descendants(rd+"ReportID").FirstOrDefault());
	}
	
	public IEnumerable<DataSource> DataSources {get;set;}
	public IEnumerable<DataSet>  DataSets {get;set;}
	public IEnumerable<ReportCell> Body {get;set;}
	public IEnumerable<ReportParameter> ReportParameters {get;set;}
	public String ReportID {get;set;}
	
}