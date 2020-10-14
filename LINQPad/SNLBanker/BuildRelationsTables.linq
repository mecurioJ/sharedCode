<Query Kind="Program">
  <Reference Relative="..\..\SkyDrive\Assemblies\SchemaParser\bin\Release\SchemaParser.dll">C:\Users\joeyf\SkyDrive\Assemblies\SchemaParser\bin\Release\SchemaParser.dll</Reference>
  <Namespace>filichiacom</Namespace>
  <Namespace>SchemaParser = filichiacom.SchemaParser</Namespace>
</Query>

void Main()
{
	SchemaParser.oDataBase OlapData = new SchemaParser.oDataBase(XElement.Load(@"c:\Projects\SSAS\BankerSchema.xml"));
	
	//[Calendar Period].[Calendar Period]
	//[Responsibility].[Opened by Officer]
	//[Measures].[Product Count]
	
	
	int databaseId;
	String cubeName = "Global"; 
	String measureGroupName = 
//		"Global Facts";
		"Products";
//		"Customers";
	IEnumerable<String> columnList = 
		//new[]{"[Measures].[Current Balance]"}; 
		new[]{"[Measures].[Product Count]"}; 
		//new[]{"[Measures].[Customer Coun]"}; 
	IEnumerable<String> rowList = 
		new[]{"[Responsibility].[Opened by Officer].&[AJM: Ezra Patel]"};
	IEnumerable<String> filterList = 
		new[]{"[Calendar Period].[Calendar Period].[Date].&[142]"};


	//get expressions from tables
            var dataTableExpressions = SchemaParser.GetDataSourceViewExpressions(OlapData);
			var dataTableDefinitions = SchemaParser.GetDataSourceViewDefinitions(OlapData);

			
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
	        var memberSet = filterList.Union(columnList.Union(rowList))
	        .Where(ms => ms.Contains(".&"))
	        .Select(ms => Regex.Split(ms,".&"))
	        .Select(spx => new{
		        Member = spx[0]
					        .Replace("[",String.Empty)
					        .Replace("]",String.Empty)
					        .Split('.'),
		        Values = spx[1]
	        }).Select(mem => new{
		        Dimension = mem.Member[0],
		        Hierarchy = mem.Member[1],
		        Level = (mem.Member.Count() > 2)
				        ? mem.Member[2]
				        : String.Empty,
		        Value = mem.Values
					        .Replace("[",String.Empty)
					        .Replace("]",String.Empty)
	
	        });


            //find any members that are measures, they require special handling
            var measureSet = SchemaParser.GetMeasureDefinitions(OlapData)
                                         .Where(cub => cub.CubeName.Equals(cubeName))
                                         .Where(ms => filterList.Union(columnList.Union(rowList))
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
                                    .Select(tbl => new 
                                        {
                                            TableName = tbl.TableName,
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
                            new 
                            {
                                LeftTable = (rel.Child.TableName),
                                LeftColumns = new[] { rel.Child.ColumnName },
                                RightTable = (rel.Parent.TableName),
                                RightColumns = new[] { rel.Parent.ColumnName }
                            })).Where(lt => TableSet.Select(k => k.Key).Contains(lt.LeftTable) && TableSet.Select(k => k.Key).Contains(lt.RightTable)).Distinct().ToArray();
//cubeSourceTablePkLookup.Dump();
memberSet.Dump();
measureSet.Dump();
//dimensionHierarchy.Dump();
//dimensionHierarchyLevel.Dump();
//relationTables.Dump();
KeyColumns.Dump();
ExpressionColumns.Dump();
TableSet.Dump();
JoinSet.Dump();
}


	/*
	var tableNames = 
	
	//new[]{"dbo_dim_Accounts","dbo_dim_Date","dbo_dim_RespCode"};
	new[]{"dbo_FACT_Global","dbo_dim_Date","dbo_dim_RespCode"};
	
	//var mGroups = OlapData.Cubes.Where(cNm => cNm.Name.Equals(CubeName)).SelectMany(m => m.MeasureGroups.Where(mg => mg.Name.Equals("Products"))).Dump();
	            var multi =
                SchemaParser.GetDataSourceViewDefinitions(OlapData)
                            .Where(dsv => dsv.DataSourceViewId.Equals(OlapData.Cubes.Where(cNm => cNm.Name.Equals(CubeName)).FirstOrDefault().Source)).SelectMany(pJoin => pJoin.Relations.Where(prnt => tableNames.Contains(prnt.Parent.TableName)))
                             .Union(SchemaParser.GetDataSourceViewDefinitions(OlapData)
                                                .Where(dsv => dsv.DataSourceViewId.Equals(OlapData.Cubes.Where(cNm => cNm.Name.Equals(CubeName)).FirstOrDefault().Source)).SelectMany(pJoin => pJoin.Relations.Where(prnt => tableNames.Contains(prnt.Child.TableName))))
                             .Where(tbl => !tbl.Child.TableName.ToLower().Contains("_stage_"))
                             .Select(jSet => new { ParentTable = jSet.Parent.TableName, ChildTable = jSet.Child.TableName })
                             .OrderBy(p => p.ParentTable).Select(row => new
                    {
                        row,
                        childNodeCount = (
                                             from countrow in SchemaParser.GetDataSourceViewDefinitions(OlapData)
                                                                          .Where(dsv => dsv.DataSourceViewId.Equals(OlapData.Cubes.Where(cNm => cNm.Name.Equals(CubeName)).FirstOrDefault().Source)).SelectMany(pJoin => pJoin.Relations.Where(prnt => tableNames.Contains(prnt.Parent.TableName)))
                                                                           .Union(SchemaParser.GetDataSourceViewDefinitions(OlapData)
                                                                                              .Where(dsv => dsv.DataSourceViewId.Equals(OlapData.Cubes.Where(cNm => cNm.Name.Equals(CubeName)).FirstOrDefault().Source)).SelectMany(pJoin => pJoin.Relations.Where(prnt => tableNames.Contains(prnt.Child.TableName))))
                                                                           .Where(tbl => !tbl.Child.TableName.ToLower().Contains("_stage_"))
                                                                           .Select(jSet => new { ParentTable = jSet.Parent.TableName, ChildTable = jSet.Child.TableName })
                                                                           .OrderBy(p => p.ParentTable)
                                             where countrow.ChildTable == row.ChildTable
                                             select countrow.ChildTable
                                         ).Count()
                    }).Select(@t => new {@t.row.ChildTable, @t.childNodeCount});
	
	multi.Distinct().Where(m => m.childNodeCount >= tableNames.Count()).Dump();
	*/
	//var ChildJoins =  DataSourceDef.SelectMany(pJoin => pJoin.Relations.Where(prnt => tableNames.Contains(prnt.Child.TableName))).Dump();

// Define other methods and classes here
