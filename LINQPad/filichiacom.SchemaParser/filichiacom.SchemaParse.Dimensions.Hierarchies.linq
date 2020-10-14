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
            int databaseId = 11; 
            String cubeName = "Loans";
            IEnumerable<String> filterList = new[]{"[Occupancy].[Occupancy Codes].&[1]","[Department].[Department Codes].&[5]","[Credit Line].[Credit Line Code].&[3]"};
            IEnumerable<String> columnList = new[]{"[Organization].[Organization].[Charter].&[CHARTER WEST]","[Measures].[Current Balance]"};
            IEnumerable<String> rowList = new[]{"[Risk Rating].[Risk Rating].[Risk Group].&[Negative]"};
			
			var ItemSet = 
			filterList.Union(columnList.Union(rowList))
			//Exclude any items that do not have a set value for expression
			.Where(li => li.Contains("&"))
			//Break out items into Member and Value
			.Select(li => Regex.Split(li,".&"))
			.Select(sp => new{
				Member = sp[0].ToString()
								.Replace("[",String.Empty)
								.Replace("]",String.Empty).Split('.'), 
				Value = sp[1].ToString()
								.Replace("[",String.Empty)
								.Replace("]",String.Empty)})
			//Split filter Values into their constiutent components					
			.Select(mem => new{
				Dimension = mem.Member[0],
				Hierarchy = mem.Member[1],
				Level = (mem.Member.Count() > 2) ? mem.Member[2].ToString() : String.Empty ,
				mem.Value
			});
			
			 //get expressions from tables
            var dataTableExpressions = SchemaParser.GetDataSourceViewExpressions(OlapData);
            var dataTableDefinitions = SchemaParser.GetDataSourceViewDefinitions(OlapData);

			//build a list object that gets the dimensions/hierarchies/levels and values
            
            //Build a lookup for the Table Joins
            var cubeSourceTablePkLookup = 
            OlapData.Cubes
                .Where(cub => cub.Name.Equals(cubeName))
                .SelectMany(cub => OlapData.DataSourceViews.Where(dsv => dsv.ID.Equals(cub.Source)).SelectMany(dsv => dsv.Schema.Tables.Cast<DataTable>().SelectMany(tbl => 
                tbl.PrimaryKey.Cast<DataColumn>().Select(dCol => new{
                    dCol.Table.TableName,
                    dCol.ColumnName
                })))).ToLookup(pk => pk.TableName, pk => pk.ColumnName);
    
            //Build a Temp Object to hold the contents of the members
            var memberSet = new[]{
			new	{ Dimension = "Customer", Hierarchy = "Customer Names", Level = "Customer Name", Value = "" },
			new	{ Dimension = "Accounts", Hierarchy = "Accounts", Level = "Account", Value = "" },
			new	{ Dimension = "Products", Hierarchy = "Products", Level = "Product", Value = "" }
			};
//			filterList.Union(columnList.Union(rowList))
//            .Where(ms => ms.Contains(".&"))
//            .Select(ms => Regex.Split(ms,".&"))
//            .Select(spx => new{
//                Member = spx[0]
//                            .Replace("[",String.Empty)
//                            .Replace("]",String.Empty)
//                            .Split('.'),
//                Values = spx[1]
//            }).Select(mem => new{
//                Dimension = mem.Member[0],
//                Hierarchy = mem.Member[1],
//                Level = (mem.Member.Count() > 2)
//                        ? mem.Member[2]
//                        : String.Empty,
//                Value = mem.Values
//                            .Replace("[",String.Empty)
//                            .Replace("]",String.Empty)
//    
//            });

            //find any members that are measures, they require special handling
            var measureSet = SchemaParser.GetMeasureDefinitions(OlapData)
                                         .Where(cub => cub.CubeName.Equals(cubeName))
                                         .Where(ms => filterList.Union(columnList.Union(rowList))
										 .Where(li => li.IndexOf('.') > 0)
                                                                .Select(item => item
                                                                                    .Replace("[", String.Empty)
                                                                                    .Replace("]", String.Empty)
                                                                                    .Split('.')[1])
                                                                .Contains(ms.MeasureName))
                                         .Select(mDef => new
                                             {
                                                 TableName = mDef.TableName,
                                                 ColumnName = mDef.ColumnName
                                             });

            //Build a list of the hierarchy sources from the DataSourceViewSchemas
            var dimensionHierarchy = 
            OlapData.Cubes.Where(cub => cub.Name.Equals(cubeName))
            .SelectMany(cub => 
                cub.Dimensions//.Where(cDim => memberSet.Select(ms => ms.Dimension).Contains(cDim.Name))
                .SelectMany(dSrc => 
                    OlapData.Dimensions.Where(oDim => oDim.ID.Equals(dSrc.DimensionID))
                        .SelectMany(oDim => oDim.Attributes.SelectMany(oAtt => oAtt.AttributeRelationships.Select(arr => 
                                    new{
                                        Dimension = dSrc.Name,
                                        Hierarchy = arr.Name,
                                        Level = String.Empty,
                                        ID = arr.AttributeID,
                                        DataSourceViewDefintion = dataTableDefinitions.Where(dsvDef => dsvDef.DataSourceViewId.Equals(oDim.Source)),
                                        SourceAttribute = oDim.Attributes.Where(oat => oat.ID.Equals(arr.AttributeID))
                                    }))
                        )));

            //Build a list of the hierarchies and Level sources from the DataSourceViewSchemas
            var dimensionHierarchyLevel = 
            OlapData.Cubes.Where(cub => cub.Name.Equals(cubeName))
            .SelectMany(cub => 
                cub.Dimensions//.Where(cDim => memberSet.Select(ms => ms.Dimension).Contains(cDim.Name))
                .SelectMany(dSrc => 
                    OlapData.Dimensions.Where(oDim => oDim.ID.Equals(dSrc.DimensionID))
                        .SelectMany(oDim => 
                            oDim.Hierarchies.SelectMany(oHier => oHier.Levels.Select(oLvl => new{
                                    Dimension = dSrc.Name,
                                    Hierarchy = oHier.Name,
                                    Level = oLvl.Name,
                                    oLvl.ID,
                                    DataSourceViewDefintion = dataTableDefinitions.Where(dsvDef => dsvDef.DataSourceViewId.Equals(oDim.Source)),
                                    SourceAttribute = oDim.Attributes.Where(oat => oat.ID.Equals(oLvl.SourceAttributeID))
                                })))
                    ));
memberSet.Dump();
var memberSetDimensions = 
memberSet.SelectMany(
	ms => OlapData.Dimensions.Where(oDim => oDim.Name.Equals(ms.Dimension))
			.SelectMany(oDim => oDim.Hierarchies.Where(hi => hi.Name.Equals(ms.Hierarchy))
								.SelectMany(oHr => oHr.Levels.Where(lvl => lvl.Name.Equals(ms.Level))
									.SelectMany(lvl => oDim.Attributes.Where(oAtt => oAtt.ID.Equals(lvl.SourceAttributeID))
								)).Union(oDim.Attributes.Where(oAtt => oAtt.Name.Equals(ms.Hierarchy)))
								.Select(sAtt => new{
								ms.Dimension,
								ms.Hierarchy,
								ms.Level,
                                DataSourceViewDefintion = dataTableDefinitions
									.Where(dsvDef => dsvDef.DataSourceViewId.Equals(oDim.Source))
									.SelectMany(dsvDef => dsvDef.Relations.Where(
										rel => sAtt.KeyColumns.SelectMany(src => src.Source.Select(tbl => tbl.TableName).Distinct()).Contains(rel.Parent.TableName)
												&& !rel.Child.TableName.ToLower().Contains("_stage_")
												//&& rel.Child.TableName.Equals("dbo_FACT_Global")
											|| sAtt.KeyColumns.SelectMany(src => src.Source.Select(tbl => tbl.TableName).Distinct()).Contains(rel.Child.TableName)
												&& !rel.Parent.TableName.ToLower().Contains("_stage_"))),
						TableName = sAtt.KeyColumns.SelectMany(src => src.Source.Select(tbl => tbl.TableName)).FirstOrDefault(),
						PrimaryKeyColumn = sAtt.KeyColumns.SelectMany(src => src.Source.SelectMany(tbl => cubeSourceTablePkLookup[tbl.TableName])).FirstOrDefault(),
                    TableColumns =
                        sAtt.KeyColumns.SelectMany(src =>
                            //need to specifically handle Expressions here...
                            src.Source.Select(cc => new { cc.TableName, cc.ColumnName })
                            )
                            //This should be handled when the measureSet is Defined.
                        .Union(measureSet)
                        .Union(sAtt.NameColumn.SelectMany(src => 
                            //need to specifically handle Expressions here...
                            src.Source.Select(cc => new { cc.TableName, cc.ColumnName })
                        
                        )),
                    ms.Value,
								//sAtt
								})))
								.Dump()
								;            


            //Build the Relational Source Items
            var relationTables = 
            memberSet.SelectMany(ms =>
            dimensionHierarchy.Union(dimensionHierarchyLevel)
                .Where(hl => hl.Dimension.Equals(ms.Dimension))
                .Where(hl => hl.Hierarchy.Equals(ms.Hierarchy))
                .Where(hl => hl.Level.Equals(ms.Level))
                .SelectMany(hl => hl.SourceAttribute.Select(sAtt => new
                {
                    DimensionName = ms.Dimension,
                    HierarchyName = ms.Hierarchy,
                    LevelName = ms.Level,
                    DataSourceViewRelations = hl.DataSourceViewDefintion.SelectMany(dsvDef => dsvDef.Relations.Where(
                        rel => sAtt.KeyColumns.SelectMany(src => src.Source.Select(tbl => tbl.TableName).Distinct()).Contains(rel.Parent.TableName)
                                && !rel.Child.TableName.ToLower().Contains("_stage_")
                                //&& rel.Child.TableName.Equals("dbo_FACT_Global")
                            || sAtt.KeyColumns.SelectMany(src => src.Source.Select(tbl => tbl.TableName).Distinct()).Contains(rel.Child.TableName)
                                && !rel.Parent.TableName.ToLower().Contains("_stage_")
                    )),
                    TableName = sAtt.KeyColumns.SelectMany(src => src.Source.Select(tbl => tbl.TableName)).FirstOrDefault(),
                    PrimaryKeyColumn = sAtt.KeyColumns.SelectMany(src => src.Source.SelectMany(tbl => cubeSourceTablePkLookup[tbl.TableName])).FirstOrDefault(),
                    //Need to handle Expression columns here
                    TableColumns =
                        sAtt.KeyColumns.SelectMany(src =>
                            //need to specifically handle Expressions here...
                            src.Source.Select(cc => new { cc.TableName, cc.ColumnName })
                            )
                            //This should be handled when the measureSet is Defined.
                        .Union(measureSet)
                        .Union(sAtt.NameColumn.SelectMany(src => 
                            //need to specifically handle Expressions here...
                            src.Source.Select(cc => new { cc.TableName, cc.ColumnName })
                        
                        )),
                    ms.Value
                })));
dimensionHierarchy.Union(dimensionHierarchyLevel).Where(hl => hl.Dimension.Equals("Credit Line")).Count().Dump();
relationTables.Dump();

            var KeyColumns = relationTables.Select(tbl => new
            {
                tbl.TableName,
                tbl.PrimaryKeyColumn,
                tbl.Value
            }).Distinct();


            //Get the Defintion of any related ComputedColumn Expressions
            var ExpressionColumns = relationTables.SelectMany(tbl =>
                tbl.TableColumns.SelectMany(tc => SchemaParser.GetExpressionDefinition(OlapData, tc.TableName, tc.ColumnName).SelectMany(parse => parse.ExpressionTree.Select(tree => new { SourceColumn = tc.ColumnName, tree.TableName, tree.ColumnName })).Distinct())
                ).ToLookup(k => k.SourceColumn, k => new { k.TableName, k.ColumnName });


            var TableSet = relationTables.SelectMany(tbl => tbl.TableColumns
                                                        .Where(
                                                            tc =>
                                                            !ExpressionColumns[tc.ColumnName].Any())
                        ).Distinct()
                                    .Union(ExpressionColumns.SelectMany(k => k))
                                    .OrderBy(k => k.TableName)
                                    .Select(tbl => new //TableColumn
                                        {
                                            TableName = FixTableName(tbl.TableName),
                                            DisplayColumn = tbl.ColumnName,
                                            Caption = tbl.ColumnName,
                                            KeyColumn =
                                                        KeyColumns.Where(
                                                            kc => kc.TableName.Equals(tbl.TableName))
                                                                .Select(kc => kc.PrimaryKeyColumn)
                                                                .FirstOrDefault(),
                                            Value = KeyColumns
                                                        .Where(kc => kc.TableName.Equals(tbl.TableName))
                                                        .Where(
                                                            kc =>
                                                            kc.PrimaryKeyColumn.Equals(tbl.ColumnName))
                                                        .Select(kc => kc.Value)
                                                        .FirstOrDefault() 
                                        }).ToLookup(tbl => tbl.TableName);

            var JoinSet = relationTables.SelectMany(tbl => tbl.DataSourceViewRelations.Select(rel =>
                            new //TableJoin
                            {
                                LeftTable = FixTableName(rel.Child.TableName),
                                LeftColumns = new[] { rel.Child.ColumnName },
                                RightTable = FixTableName(rel.Parent.TableName),
                                RightColumns = new[] { rel.Parent.ColumnName }
                            })).Where(lt => TableSet.Select(k => k.Key).Contains(lt.LeftTable) && TableSet.Select(k => k.Key).Contains(lt.RightTable)).Distinct().ToArray();

            var RelationalSet = new []//RelationalBuilderContext[]
                {
                    new //RelationalBuilderContext()
                        {
                            DatabaseId = databaseId,
                            Tables = TableSet.Select(tbl => new //Table
                                {
                                    Name = tbl.Key,
                                    Columns = tbl
                                }
                            
                            ).ToArray(),
                            Joins = JoinSet.DistinctBy(js => String.Format("{0}.{1}",js.LeftTable, js.RightTable))
                        }, 
                };
}
// Define other methods and classes here


        public string FixTableName(string tableId)
        {
            // TODO: What if the schema name isn't "dbo" but something else?
            return tableId.StartsWith("dbo_") ? tableId.Substring(4) : tableId;
        }