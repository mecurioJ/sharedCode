<Query Kind="Program">
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
	SchemaParser.oDataBase OlapData = new SchemaParser.oDataBase(XElement.Load(@"c:\Projects\SSAS\BankerSchema.xml"));
	var tableName = "dbo_dim_Part_Placed_Code";
	var tableColumns = new []{
		"Orig_Direct_Indirect_ID",
		"Participation_Display"
	};

	SchemaParser.GetExpressionDefinition(OlapData,  "dbo_dim_Part_Placed_Code", "Participation_Display").Dump();
}
// Define other methods and classes here


        public string FixTableName(string tableId)
        {
            // TODO: What if the schema name isn't "dbo" but something else?
            return tableId.StartsWith("dbo_") ? tableId.Substring(4) : tableId;
        }
		
		
        private ILookup<string, string> PrimaryKeyLookup(string cubeName)
        {
			SchemaParser.oDataBase OlapData = new SchemaParser.oDataBase(XElement.Load(@"c:\Projects\SSAS\BankerSchema.xml"));
            return OlapData.Cubes
                           .Where(cub => cub.Name.Equals(cubeName))
                           .SelectMany(cub => OlapData.DataSourceViews.Where(dsv => dsv.ID.Equals(cub.Source)).SelectMany(dsv => dsv.Schema.Tables.Cast<DataTable>().SelectMany(tbl => 
                                                                                                                                                                                tbl.PrimaryKey.Cast<DataColumn>().Select(dCol => new{
                                                                                                                                                                                    dCol.Table.TableName,
                                                                                                                                                                                    dCol.ColumnName
                                                                                                                                                                                })))).ToLookup(pk => pk.TableName, pk => pk.ColumnName);
        }