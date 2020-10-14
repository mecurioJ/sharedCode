<Query Kind="Program">
  <Connection>
    <ID>1e5485fa-aac2-4b53-8481-ce2675973975</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>SNLBanker_SampleDW</Database>
  </Connection>
  <Reference Relative="..\..\SkyDrive\Assemblies\SchemaParser\bin\Release\SchemaParser.dll">C:\Users\joeyf\SkyDrive\Assemblies\SchemaParser\bin\Release\SchemaParser.dll</Reference>
  <GACReference>Microsoft.AnalysisServices.AdomdClient, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>filichiacom</Namespace>
  <Namespace>Microsoft.AnalysisServices.AdomdClient</Namespace>
  <Namespace>MoreLinq</Namespace>
  <Namespace>SchemaParser = filichiacom.SchemaParser</Namespace>
</Query>

void Main()
{
	var schemaDir = @"C:\Users\joeyf\SkyDrive\SSAS";
	SchemaParser.oDataBase OlapData = new SchemaParser.oDataBase(XElement.Load(schemaDir+@"\BankerSchema.xml"));
	
	var Tables = OlapData.DataSourceViews
                .SelectMany(sch => sch.Schema.Tables.Cast<DataTable>()
				.Select(tbl => new {
					CubeDataSource = sch.Name,
					 tbl.TableName,
					 DbTableName = tbl.ExtendedProperties["DbTableName"] != null
					 	? "dbo_" + tbl.ExtendedProperties["DbTableName"].ToString()
						: String.Empty
				}))
				.Distinct()
				.ToLookup(tbl => new{tbl.CubeDataSource,tbl.TableName})
				;
	Tables.Dump();
}
// Define other methods and classes here

public class HierarchySetItem
{
	public String Dimension {get;set;}
	public String Hierarchy  {get;set;}
	public String Level {get;set;} 
	public String Value {get;set;} 
	public Object Hierarchies {get;set;} 
}

public class DataDefinitionItem
{

	public String DataType {get;set;}
	public String TableName {get;set;}
	public String ColumnName {get;set;}
	public String OriginalName {get;set;}
	public IEnumerable<String[]> ExpressionDefinition {get;set;}
}


        public string FixTableName(string tableId)
        {
            // TODO: What if the schema name isn't "dbo" but something else?
            return tableId.StartsWith("dbo_") ? tableId.Substring(4) : tableId;
        }
		
		
        private ILookup<string, string> PrimaryKeyLookup(string cubeName)
        {
		
			var schemaDir = @"C:\Users\joeyf\SkyDrive\SSAS\";
		
			SchemaParser.oDataBase OlapData = new SchemaParser.oDataBase(XElement.Load(schemaDir+@"BankerSchema.xml"));
            return OlapData.Cubes
                           .Where(cub => cub.Name.Equals(cubeName))
                           .SelectMany(cub => OlapData.DataSourceViews.Where(dsv => dsv.ID.Equals(cub.Source)).SelectMany(dsv => dsv.Schema.Tables.Cast<DataTable>().SelectMany(tbl => 
                                                                                                                                                                                tbl.PrimaryKey.Cast<DataColumn>().Select(dCol => new{
                                                                                                                                                                                    dCol.Table.TableName,
                                                                                                                                                                                    dCol.ColumnName
                                                                                                                                                                                })))).ToLookup(pk => pk.TableName, pk => pk.ColumnName);
        }