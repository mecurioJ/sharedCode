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
	
            //get expressions from tables
            var dataTableExpressions = SchemaParser.GetDataSourceViewExpressions(OlapData);
            var dataTableDefinitions = SchemaParser.GetDataSourceViewDefinitions(OlapData);

            System.Diagnostics.Debug.WriteLine(String.Format("Sql05CubeSourceMeta entry time: {0}", DateTime.Now));

            //Get the DataSourceViewId from the current Cube
            String DataSourceViewId = OlapData.Cubes.First(cub => cub.Name.Equals(cubeName)).Source;

            System.Diagnostics.Debug.WriteLine(String.Format("Sql05CubeSourceMeta DataSourceViewId time: {0}", DateTime.Now));

            //Build a lookup for the Table Joins
            ILookup<string, string> cubeSourceTablePkLookup = PrimaryKeyLookup(cubeName);

            //Build a set of Data Relation Objects
            IEnumerable<string> sourceSet = filterList.Union(columnList.Union(rowList)).Where(ss => !String.IsNullOrEmpty(ss)).ToArray();

            //Build a Temp Object to hold the contents of the members
            IEnumerable<SchemaParser.TableColumn> measureSet = SchemaParser.BuildMeasureSet(OlapData, sourceSet,
                                                                                            cubeName).ToArray();

            System.Diagnostics.Debug.WriteLine(String.Format("Sql05CubeSourceMeta measureSet time: {0}", DateTime.Now));

            //Build a definition of the members that are not measures...
            IEnumerable<SchemaParser.MemberItemDefinition> memberItemDefinitions = SchemaParser.GetMemberItemDefinitions(
                    OlapData,
                    SchemaParser.GetMeasureDefinitions(OlapData).Where(cNm => cNm.CubeName.Equals(cubeName)),
                    SchemaParser.BuildMemberSet(sourceSet),
                    SchemaParser.GetHierarchySet(OlapData, cubeName),
                    SchemaParser.GetRelationSet(OlapData,DataSourceViewId)).ToArray();

            System.Diagnostics.Debug.WriteLine(String.Format("Sql05CubeSourceMeta memberDefinitions time: {0}", DateTime.Now));

            //Get the names of the tables
            IEnumerable<string> MemberTables = memberItemDefinitions.Select(mDef => SchemaParser.GetDbTableName(OlapData, FixTableName(mDef.TableName)))
                .Distinct()
                .ToArray();

            System.Diagnostics.Debug.WriteLine(String.Format("Sql05CubeSourceMeta MemberTables time: {0}", DateTime.Now));

            //Build a list to hold the primary filter items
            SchemaParser.MemberItemDefinition[] filterPrimary =
                memberItemDefinitions.Where(r => r.DataType.Equals("Integer") || r.DataType.Equals("BigInt"))
                .ToArray();

            //built an array to hold the joins...
            var joins = filterPrimary
                .SelectMany(r => r.Relations
                    .Select(rel =>
                        new 
                            {
                                Right = rel.ChildColumns.Select(cc => new { cc.TableName, cc.ColumnName }), 
                                Left = rel.ParentColumns.Select(cc => new { cc.TableName, cc.ColumnName })
                            })
                )
            .Select(tb =>
            new
            {
                LeftTable = SchemaParser.GetDbTableName(OlapData, FixTableName(tb.Left.First().TableName)),
                LeftColumn = tb.Left.First().ColumnName,
                RightTable = SchemaParser.GetDbTableName(OlapData, FixTableName(tb.Right.First().TableName)),
                RightColumn = tb.Right.First().ColumnName,
            }).Distinct()
            .ToArray();



            System.Diagnostics.Debug.WriteLine(String.Format("Sql05CubeSourceMeta Joins time: {0}", DateTime.Now));

            //Get all tables with a direct join to the measures
            var MeasureJoins = joins.Where(MeasureJoin => measureSet.Select(ms => ms.TableName).Contains(MeasureJoin.RightTable)).ToArray();
            System.Diagnostics.Debug.WriteLine(String.Format("Sql05CubeSourceMeta MeasureJoins time: {0}", DateTime.Now));
            
            //Get all the tables that joins were not found for.
            var MissingTables = MemberTables.Except(MeasureJoins.Select(mj => mj.LeftTable)).ToArray();
            System.Diagnostics.Debug.WriteLine(String.Format("Sql05CubeSourceMeta MissingTables time: {0}", DateTime.Now));

            //Get the tables for linking the missing tables to known joins
            var linkingTables = joins.Where(js => MissingTables.Contains(js.LeftTable)).Select(js => js.RightTable).ToArray();
            System.Diagnostics.Debug.WriteLine(String.Format("Sql05CubeSourceMeta linkingTables time: {0}", DateTime.Now));

            //get the joins for the tables that are missing and attach them to the objects...
            var MissingJoins = joins.Where(js => linkingTables.Contains(js.RightTable)).ToArray();
            System.Diagnostics.Debug.WriteLine(String.Format("Sql05CubeSourceMeta MissingJoins time: {0}", DateTime.Now));
            
            var JoinSet = MissingJoins.Union(MeasureJoins).Select(js => new TableJoin
            {

                LeftTable = FixTableName(js.LeftTable),
                LeftColumns = new[] { js.LeftColumn },
                RightTable = FixTableName(js.RightTable),
                RightColumns = new[] { js.RightColumn }
            }).ToArray();
            System.Diagnostics.Debug.WriteLine(String.Format("Sql05CubeSourceMeta JoinSet time: {0}", DateTime.Now));



            var memberDefinitions = memberItemDefinitions.SelectMany(tbl =>
                {
                    var listSet1 = tbl.ExpressionDefinition.ToList();
                    if (!tbl.ExpressionDefinition.Any())
                    {
                        listSet1.Add(tbl.ColumnName);
                    }
                    return listSet1.Select(li => new
                        {
                            Caption = li,
                            DisplayColumn = li,
                            TableName = SchemaParser.GetDbTableName(OlapData, FixTableName(tbl.TableName)),
                            Value = tbl.Value,
                            IsKeyColumn = tbl.IsPrimaryKey,
                            CanFilter = tbl.DataType.Equals("Integer") || tbl.DataType.Equals("BigInt")
                        });
                }
                ).ToArray();

            var KeyColumnArray = memberItemDefinitions
                .Where(miDef => miDef.IsPrimaryKey && miDef.DataType.Equals("Integer") || miDef.DataType.Equals("BigInt"))
                .DistinctBy(dc => String.Format("{0}.{1}", dc.TableName, dc.ColumnName))
                .Select(tt => new
                    {
                        TableName = SchemaParser.GetDbTableName(OlapData, FixTableName(tt.TableName)), 
                        tt.ColumnName
                    });

            var TableLookup = memberDefinitions
                .Select(ts => new
                    {
                        TableName = FixTableName(ts.TableName),
                        KeyColumn = KeyColumnArray
                            .Where(tn => tn.TableName.Equals(ts.TableName))
                            .Select(dcol => dcol.ColumnName)
                            .FirstOrDefault() ?? String.Empty,
                        Caption = ts.DisplayColumn,
                        ts.DisplayColumn,
                        Value = (ts.IsKeyColumn && ts.CanFilter) ? ts.Value : null
                    }).Distinct()
                    .ToLookup(tbl => tbl.TableName);

            var RelationalTables = TableLookup.Select(tbl => new Table
            {
                Name = tbl.Key,
                Columns = tbl.Select(cc => new TableColumn()
                {
                    Caption = cc.Caption,
                    DisplayColumn = cc.DisplayColumn,
                    KeyColumn = cc.KeyColumn,
                    TableName = cc.TableName,
                    Value = cc.Value
                })
            }).ToList();

            System.Diagnostics.Debug.WriteLine(String.Format("Sql05CubeSourceMeta RelationalTables time: {0}", DateTime.Now));

            



            var RelationalSet = new []
                {
                    new RelationalBuilderContext()
                        {
                            DatabaseId = databaseId,
                            Tables = RelationalTables,
                            Joins = JoinSet.OrderByDescending(ob => ob.LeftTable).ToArray()

                        }, 

                };

            System.Diagnostics.Debug.WriteLine(String.Format("Sql05CubeSourceMeta RelationalSet time: {0}", DateTime.Now));

            return RelationalSet;
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