<Query Kind="Program">
  <Reference>&lt;ProgramFilesX86&gt;\Microsoft.NET\ADOMD.NET\110\Microsoft.AnalysisServices.AdomdClient.dll</Reference>
  <Reference>&lt;ProgramFilesX86&gt;\Microsoft SQL Server\110\SDK\Assemblies\Microsoft.AnalysisServices.DLL</Reference>
  <Reference Relative="..\..\SkyDrive\Assemblies\SchemaParser\bin\Release\SchemaParser.dll">C:\Users\joeyf\SkyDrive\Assemblies\SchemaParser\bin\Release\SchemaParser.dll</Reference>
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>filichiacom</Namespace>
  <Namespace>Microsoft.AnalysisServices</Namespace>
  <Namespace>Microsoft.AnalysisServices.AdomdClient</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
	 AdomdConnection MetaDataConnection = new AdomdConnection(Utils.ConnectionString(ServerName, CatalogName));
      if (MetaDataConnection.State != ConnectionState.Open)
      {
          MetaDataConnection.Open();
      }
	  var Restrictions = new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection();
	  
	  	var xmlRestrictions = 
						;
		var SchemaObjects = 
		new filichiacom.SchemaParser.oDataBase(
			XDocument.Parse(
			MetaDataConnection.GetSchemaDataSet(
							AdomdSchema.sSchemaList[AdomdSchemaGuid.XmlMetadata].Id,
							new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection()
							{
								new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("DatabaseID",CatalogName),
								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CubeID","DDA"),
								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("MeasureGroupID","FACT DDA"),
								
							}).Tables[0].Rows[0].Field<String>("METADATA")).Elements()
												.First());
											
//		MetaDataConnection.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.Members].Id,
//												new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection()
//							{
//								new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CATALOG_NAME",CatalogName),
//								//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("LEVEL_UNIQUE_NAME","[Account Status].[Account Status].[Account Type]"),
//								new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("MEMBER_UNIQUE_NAME","[Account Status].[Account Status].[Account Type].&[Deposits]"),
//								new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CUBE_NAME","COD"),
//								
//							}).Dump();
										
  var DSVObjects = SchemaObjects.DataSourceViews.Select(dsv => new DataSourceViewSummary(dsv));

#region Build Cube Objects
		//hierarchyAndLevels.Dump();
	var Cubes = MetaDataConnection
					.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.Cubes].Id,Restrictions)
					.Tables[0]
					.Rows
					.Cast<DataRow>()
					.Select(row => new{
						CubeName = row.Field<String>("CUBE_NAME"),
						CubeCaption = row.Field<String>("CUBE_CAPTION"),
						Description = row.Field<String>("DESCRIPTION"),
						IsDimensionCube = String.Empty,
						IsLinkable = row.Field<Boolean>("IS_LINKABLE"),
						CubeType = row.Field<String>("CUBE_TYPE"),
						IsWriteEnabled = row.Field<Boolean>("IS_WRITE_ENABLED"),
						LastSchemaUpdate = row.Field<DateTime>("LAST_SCHEMA_UPDATE"),
						LastUpdateDate = row.Field<DateTime>("LAST_DATA_UPDATE"),
						BaseCubeName = row.Field<String>("BASE_CUBE_NAME")
					})
					.Select(cub => {
	  					Restrictions.Clear();
						Restrictions.Add("CUBE_NAME",cub.CubeName);
						
						
						
							var CubeDataSource = 
														
							SchemaObjects.DataSourceViews.Where(dsv => dsv.ID.Equals(
							//CubeDataSource
							XDocument.Parse(
							MetaDataConnection
								.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.XmlMetadata].Id,
								new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection()
									{
										new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("DatabaseID",CatalogName),
										new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CubeID",
											SchemaObjects.Cubes.Where(cb => cb.Name.Equals(cub.CubeName)).FirstOrDefault().ID),
										//new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("MeasureGroupID","FACT DDA"),
										
									})
								.Tables[0]
								.Rows[0].Field<String>("METADATA")).Elements().First()
								.Element(filichiacom.Namespaces.engine+"Source")
								.Value
							)).Select(dsv => new{
									DataSourceViewName = dsv.Name,
									DataSourceViewId = dsv.ID,
									DataSourceTables = dsv.Schema.Tables.Cast<DataTable>().Select(dt => new{
										dt.TableName,
										PrimaryKey = dt.PrimaryKey.Cast<DataColumn>().Select(dc => dc.ColumnName),
										Columns = dt.Columns.Cast<DataColumn>().Select(dc => new{dc.ColumnName, Expression = dc.ExtendedProperties["ComputedColumnExpression"] ?? String.Empty}),
										DbTableName = dt.ExtendedProperties["DbTableName"],
										FriendlyName = dt.ExtendedProperties["FriendlyName"],
										RelatesTo = dt.ParentRelations.Cast<DataRelation>().Select(rt => new{
											ParentColumns = rt.ParentColumns.Cast<DataColumn>().Select(dc => new{
												dc.Table.TableName,
												dc.ColumnName, 
												Expression = dc.ExtendedProperties["ComputedColumnExpression"] ?? String.Empty}),
											ChildColumns = rt.ChildColumns.Cast<DataColumn>().Select(dc => new{
												dc.Table.TableName,
												dc.ColumnName, 
												Expression = dc.ExtendedProperties["ComputedColumnExpression"] ?? String.Empty})
											}),
										RelatesFrom = dt.ChildRelations.Cast<DataRelation>().Select(rt => new{
											ParentColumns = rt.ParentColumns.Cast<DataColumn>().Select(dc => new{
												dc.Table.TableName,
												dc.ColumnName, 
												Expression = dc.ExtendedProperties["ComputedColumnExpression"] ?? String.Empty}),
											ChildColumns = rt.ChildColumns.Cast<DataColumn>().Select(dc => new{
												dc.Table.TableName,
												dc.ColumnName, 
												Expression = dc.ExtendedProperties["ComputedColumnExpression"] ?? String.Empty}).Select(col => new{col.TableName,col.ColumnName,col.Expression})
											})
									})
								});
						
						
#region Add Dimensions to Output						
						var dims = MetaDataConnection
										.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.Dimensions].Id,Restrictions)
										.Tables[0]
										.Rows
										.Cast<DataRow>()
										.Select(dRow => new{
											CubeName = dRow.Field<String>("CUBE_NAME"),
											UniqueName = dRow.Field<String>("DIMENSION_UNIQUE_NAME"),
											Name = dRow.Field<String>("DIMENSION_UNIQUE_NAME"),
											Caption = dRow.Field<String>("DIMENSION_CAPTION"),
											Description = dRow.Field<String>("DESCRIPTION"),
											MasterName = dRow.Field<String>("DIMENSION_MASTER_NAME"),
											DimensionType = dRow.Field<System.Int16>("DIMENSION_TYPE"),
											DimensionOrdinal = dRow.Field<UInt32>("DIMENSION_ORDINAL"),
											KeyAttributeCardinality = dRow.Field<UInt32>("DIMENSION_CARDINALITY"),
											IsWriteEnabled = dRow.Field<Boolean>("IS_READWRITE"),
											IsVisible = dRow.Field<Boolean>("DIMENSION_IS_VISIBLE"),
											
										}).Select(dim => {
										
											var HierRestrict = new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection();
											HierRestrict.Add("CUBE_NAME", dim.CubeName);
											HierRestrict.Add("DIMENSION_UNIQUE_NAME", dim.UniqueName);
											
											var HierarchyRows = MetaDataConnection
																.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.Hierarchies].Id,
																HierRestrict
																).Tables[0].Rows.Cast<DataRow>().Select(hRow => new{
																	CubeName = hRow.Field<String>("CUBE_NAME"),
																	DimensionUniqueName =  hRow.Field<String>("DIMENSION_UNIQUE_NAME"),
																	HierarchyUniqueName =  hRow.Field<String>("HIERARCHY_UNIQUE_NAME"),
																	HierarchyName = hRow.Field<String>("HIERARCHY_NAME"),
																	HierarchyCaption = hRow.Field<String>("HIERARCHY_CAPTION"),
																	Description = hRow.Field<String>("DESCRIPTION"),
																	AllMember = hRow.Field<String>("ALL_MEMBER"),
																	DefaultMember = hRow.Field<String>("DEFAULT_MEMBER"),
																	DisplayFolder = hRow.Field<String>("HIERARCHY_DISPLAY_FOLDER"),
																	HierarchyOrdinal = hRow.Field<Object>("HIERARCHY_ORDINAL"),
																	HierarchyCardinality = hRow.Field<Object>("HIERARCHY_CARDINALITY"),
																	IsWriteEnabled = hRow.Field<Boolean>("IS_READWRITE"),
																	IsVisible = hRow.Field<Boolean>("HIERARCHY_IS_VISIBLE"),
																	DimensionType = 
																		(hRow.Field<object>("DIMENSION_TYPE") != null)
																		? System.Enum.Parse(typeof(eDimensionType),hRow.Field<object>("DIMENSION_TYPE").ToString())
																		: eDimensionType.MsNotDefined,
																	Structure = 
																		(hRow.Field<object>("STRUCTURE") != null)
																		? System.Enum.Parse(typeof(eHierarchyStructure),hRow.Field<object>("STRUCTURE").ToString())
																		: eHierarchyStructure.Network,
																	HierarchyOrigin = 
																		(hRow.Field<object>("HIERARCHY_ORIGIN") != null)
																		? System.Enum.Parse(typeof(eHierarchyOrigin),hRow.Field<object>("HIERARCHY_ORIGIN").ToString())
																		: eHierarchyOrigin.SystemInternal,
																	InstanceSelection = 
																		(hRow.Field<object>("INSTANCE_SELECTION") != null)
																		? System.Enum.Parse(typeof(eInstanceSelection),hRow.Field<object>("INSTANCE_SELECTION").ToString())
																		: eInstanceSelection.None,
																	GroupingBehavior = 
																		(hRow.Field<object>("GROUPING_BEHAVIOR") != null)
																		? System.Enum.Parse(typeof(GroupingBehavior),hRow.Field<object>("GROUPING_BEHAVIOR").ToString())
																		: GroupingBehavior.DiscourageGrouping,
																	
																	
																}).Select(hier => {
																			var mdLevel = MetaDataConnection
																				.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.Levels].Id,
																				new Microsoft.AnalysisServices.AdomdClient.AdomdRestrictionCollection()
																					{
																						new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("CUBE_NAME",hier.CubeName),
																						new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("DIMENSION_UNIQUE_NAME",hier.DimensionUniqueName),
																						new Microsoft.AnalysisServices.AdomdClient.AdomdRestriction("HIERARCHY_UNIQUE_NAME",hier.HierarchyUniqueName)
																					}
																				)
																				.Tables[0]
																				.Rows
																				.Cast<DataRow>()
																				.Select(lvl => new{
																					CubeName = lvl.Field<String>("CUBE_NAME"),
																					DimensionUniqueName = lvl.Field<String>("DIMENSION_UNIQUE_NAME"),
																					HierarchyUniqueName = lvl.Field<String>("HIERARCHY_UNIQUE_NAME"),
																					LevelName = lvl.Field<String>("LEVEL_UNIQUE_NAME"),
																					LevelCaption = lvl.Field<String>("LEVEL_CAPTION"),
																					Description = lvl.Field<String>("DESCRIPTION"),
																					LevelNumber = lvl.Field<Object>("LEVEL_NUMBER"),
																					LevelCardinality = lvl.Field<Object>("LEVEL_CARDINALITY"),
																					LevelType = (lvl.Field<Object>("LEVEL_TYPE") != null)
																								? System.Enum.Parse(typeof(eLevelType),lvl.Field<int>("LEVEL_TYPE").ToString())
																								: eLevelType.Regular,
																					isVisible = lvl.Field<Boolean>("LEVEL_IS_VISIBLE"),
																					AttributeHierarchyName = lvl.Field<String>("LEVEL_ATTRIBUTE_HIERARCHY_NAME"),
																					KeyCardinality = lvl.Field<Object>("LEVEL_KEY_CARDINALITY"),
																					Origin = lvl.Field<Object>("LEVEL_ORIGIN"),
																		LevelDef = SchemaParser.GetHierarchyLevelDefinitions(SchemaObjects).Where(hh =>
																			hh.DimensionName.Equals(dim.Caption)
																			&& hh.HierarchyName.Equals(hier.HierarchyName)
																			&& hh.LevelName.Equals(lvl.Field<String>("LEVEL_CAPTION"))),
																				});
																			return new{
																				hier.CubeName,
																				hier.DimensionUniqueName,
																				hier.HierarchyUniqueName,
																				hier.HierarchyName,
																				hier.HierarchyCaption,
																				hier.Description,
																				hier.AllMember,
																				hier.DefaultMember,
																				hier.DisplayFolder,
																				hier.HierarchyOrdinal,
																				hier.HierarchyCardinality,
																				hier.IsWriteEnabled,
																				hier.IsVisible,
																				hier.DimensionType,
																				hier.Structure,
																				hier.HierarchyOrigin,
																				hier.InstanceSelection,
																				hier.GroupingBehavior,
																				LevelList = mdLevel,
																			};
																});
											return new{
												dim.CubeName,
												dim.UniqueName,
												dim.Name,
												dim.Caption,
												dim.Description,
												dim.MasterName,
												dim.DimensionType,
												dim.DimensionOrdinal,
												dim.KeyAttributeCardinality,
												dim.IsWriteEnabled,
												dim.IsVisible,
												Hierarchies = HierarchyRows
											};
										})
										;
#endregion						
	  					Restrictions.Clear();
						Restrictions.Add("CUBE_NAME",cub.CubeName);
						var Measures = MetaDataConnection
							.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.Measures].Id,Restrictions)
							.Tables[0];
						
						var view = new DataView(Measures,null, "MEASUREGROUP_NAME, MEASURE_CAPTION", DataViewRowState.CurrentRows);
						
						var measuresList = view.ToTable().Rows.Cast<DataRow>().Select(mRow => new{
							CubeName = mRow.Field<String>("CUBE_NAME"),
							MeasureUniqueName = mRow.Field<String>("MEASURE_UNIQUE_NAME"),
							MeasureName = mRow.Field<String>("MEASURE_NAME"),
							MeasureCaption = mRow.Field<String>("MEASURE_CAPTION"),
							Description = mRow.Field<String>("DESCRIPTION"),
							MeasureAggregator = (mRow.Field<Object>("MEASURE_AGGREGATOR") != null)
												? System.Enum.Parse(typeof(eMeasureAggregator),mRow.Field<Object>("MEASURE_AGGREGATOR").ToString())
												: eMeasureAggregator.None,
							DataType = (mRow.Field<ushort>("DATA_TYPE") != null)
										? System.Enum.Parse(typeof(eDataType),mRow.Field<Object>("DATA_TYPE").ToString())
										: eDataType.DBTYPE_IUNKNOWN,
							NumericPrecision = mRow.Field<ushort>("NUMERIC_PRECISION"),
							NumericScale = mRow.Field<short>("NUMERIC_SCALE"),
							Expression = mRow.Field<String>("EXPRESSION") ?? String.Empty,
							IsVisible = mRow.Field<Boolean>("MEASURE_IS_VISIBLE"),							
							MeasureGroupName = mRow.Field<String>("MEASUREGROUP_NAME") ?? String.Empty,							
							DisplayFolder = mRow.Field<String>("MEASURE_DISPLAY_FOLDER"),							
							FormatString = mRow.Field<String>("DEFAULT_FORMAT_STRING") ?? String.Empty,	
							measureDef = SchemaParser.GetMeasureDefinitions(SchemaObjects).Where(mdef => 
								mdef.MeasureName.Equals(mRow.Field<String>("MEASURE_NAME"))
								&& mdef.CubeName.Equals(mRow.Field<String>("CUBE_NAME"))
								&& mdef.MeasureGroupName.Equals(mRow.Field<String>("MEASUREGROUP_NAME"))
								)
							
						}).Select(ml => {
						
							return new {
								ml.CubeName,
								ml.MeasureUniqueName,
								ml.MeasureName,
								ml.MeasureCaption,
								ml.Description,
								ml.MeasureAggregator,
								ml.DataType,
								ml.NumericPrecision,
								ml.NumericScale,
								ml.Expression,
								ml.IsVisible,
								ml.MeasureGroupName,
								ml.DisplayFolder,
								ml.FormatString,
								ml.measureDef
							};						});
						
	
						
						return new{
							cub.CubeName,
							cub.CubeCaption,
							cub.Description,
							cub.IsDimensionCube,
							cub.IsLinkable,
							cub.CubeType,
							cub.IsWriteEnabled,
							cub.LastSchemaUpdate,
							cub.LastUpdateDate,
							cub.BaseCubeName,
							Dimensions = dims,
							Measures = measuresList
						
						};
					}
					
					);
#endregion					

//Cubes.SelectMany(cub => cub.Dimensions.SelectMany(dim => dim.Hierarchies.SelectMany(lvl => lvl.LevelList))).Dump();

//dbo_dim_Account_Status
/*
  Cubes
  	//.Where(cub => cub.CubeName.Equals("COD"))
	.SelectMany(c => c
		.Dimensions.SelectMany(d => 
		{
			var DimensionUniqueName = d.UniqueName;
			var DimensionName = d.Name;
			return d.Hierarchies
				.SelectMany(h => h.LevelList)
				.Select(lvl => new {
				lvl.CubeName,
				DataSourceViewId = SchemaObjects.Cubes.Where(cNm => cNm.Name.Equals(lvl.CubeName)).First().Source,
				DimensionName,
				lvl.DimensionUniqueName,
				lvl.HierarchyUniqueName,
				lvl.LevelType,
				lvl.LevelName,
				lvl.LevelCaption,
				lvl.Description,
				lvl.LevelCardinality,
				lvl.isVisible,
				lvl.AttributeHierarchyName,
				lvl.KeyCardinality,
				Origin = (lvl.Origin != null)
										? System.Enum.Parse(typeof(eHierarchyOrigin),lvl.Origin.ToString())
										: null,
				
				//lvl.LevelDef,
				IsAllMember = String.IsNullOrEmpty(lvl.AttributeHierarchyName),
				AttributeDefinitions = SchemaParser.GetAttributeDefinitions(SchemaObjects)
								.Where(dsv => dsv.DimensionName.Equals(DimensionName.Replace("[",String.Empty).Replace("]",String.Empty)) && dsv.AttributeName.Equals(lvl.AttributeHierarchyName))
								.Select(cls => new{
									KeyTable = cls.KeyColumn.Select(tbl => tbl.TableName).Distinct(),
									KeyColumns = cls.KeyColumn.Select(col => col.ColumnName).Distinct(),
									NameTable = cls.NameColumn.Select(tbl => tbl.TableName).Distinct(),
									NameColumns = cls.NameColumn.Select(col => col.ColumnName).Distinct()
									})
									.SelectMany(tbl => {
										var dsvId = SchemaObjects.Cubes.Where(cNm => cNm.Name.Equals(lvl.CubeName)).First().Source;
										var SourceTables = DSVObjects.Where(dsv => dsv.ID.Equals(dsvId)).SelectMany(dsv => dsv.Tables);
										return SourceTables
											.Where(sTbl => tbl.KeyTable.Union(tbl.NameTable).Contains(sTbl.TableName))
											.Select(sTbl => new {
												KeyTable = tbl.KeyTable,
												KeyColumns = tbl.KeyColumns,
												NameTable = tbl.NameTable,
												NameColumns = tbl.NameColumns,
												DbSchemaName = sTbl.DbSchemaName,
												FriendlyName = sTbl.FriendlyName,
												Columns = sTbl.Columns.Where(sCol => tbl.KeyColumns.Union(tbl.NameColumns).Contains(sCol.ColumnName)),
												PrimaryKeys = sTbl.PrimaryKeys,
												//sTbl.ParentRelations,
												//sTbl.ChildRelations
											});})});}))
	.SelectMany(t1 => t1.AttributeDefinitions.Where(aDef => aDef.Columns.Where(aCol => !String.IsNullOrEmpty(aCol.ColumnExpression)).Any())
		.Select(t2 => new{
			t1.CubeName,
//			t1.DataSourceViewId,
			t1.DimensionName,
			t1.DimensionUniqueName,
			t1.HierarchyUniqueName,
			t1.LevelName,
			t1.LevelCaption,
			t1.Description,
			t1.LevelCardinality,
			t1.isVisible,
			t1.AttributeHierarchyName,
			t1.KeyCardinality,
			t1.Origin,
			t1.IsAllMember,
//			t2.KeyTable,
//			t2.KeyColumns,
//			t2.NameTable,
//			t2.NameColumns,
			t2.DbSchemaName,
			t2.FriendlyName,
			Columns = t2.Columns.Union(t2.PrimaryKeys).Distinct().Select(cc => new{
				IsPrimaryKey = t2.PrimaryKeys.Where(pk => 
					pk.ColumnName.Equals(cc.ColumnName)
					&& pk.DbColumnName.Equals(cc.DbColumnName)
					&& pk.DataType.Equals(cc.DataType)).Any(),
				cc.ColumnName,
				cc.DbColumnName,
				ColumnExpression =
					cc.ColumnExpression
						.Replace("\r\n"," ")
						.Replace("  "," ")
				,
				cc.DataType
			}).DistinctBy(nm => nm.DbColumnName)
			.Where(tt => !String.IsNullOrEmpty(tt.ColumnExpression))
			.Select(tm => new{
			t1.CubeName,
			t1.DimensionName,
//			t1.DimensionUniqueName,
			t1.HierarchyUniqueName,
			t1.LevelName,
			t1.AttributeHierarchyName,
//			SourceTableName = t2.NameTable.First(),
//			SourceFriendlyName = t2.FriendlyName, 
//			SourceColumnName = tm.ColumnName,
//			SourceFriendlyColumn = tm.DbColumnName,
//				tm.ColumnExpression,
//				tm.DataType,
//			t1.DataSourceViewId,
			t1.LevelCaption,
//			t1.Description,
//			t1.LevelCardinality,
//			t1.isVisible,
//			t1.KeyCardinality,
			t1.Origin,
//			t1.IsAllMember,
//			t2.KeyTable,
//			t2.KeyColumns,
//			t2.NameColumns,
//			t2.DbSchemaName,
			
			})

		})
	
	).SelectMany(dCol => dCol.Columns)
	.Where(ogn => ogn.Origin.Equals(eHierarchyOrigin.UserDefined))
	.OrderBy(cDb => System.Tuple.Create(cDb.CubeName,cDb.HierarchyUniqueName,cDb.LevelName))
	.Dump()
	;

*/

var DataSourceLookup = 

DSVObjects.SelectMany(t1 => t1.Tables.SelectMany(t2 => t2.Columns.Union(t2.PrimaryKeys).Select(t3 => new{
	t1.ID,
	t1.Name,
	t2.TableName,
	t2.DbTableName,
	t2.FriendlyName,
	t2.TableType,
	t2.DbSchemaName,
	IsPrimaryKey = t2.PrimaryKeys.Where(pk => 
		pk.ColumnName.Equals(t3.ColumnName)
		&& pk.DbColumnName.Equals(t3.DbColumnName)
		&& pk.ColumnExpression.Equals(t3.ColumnExpression)
		&& pk.DataType.Equals(t3.DataType)
		).Any(),
	t3.ColumnName,
	t3.DbColumnName,
	t3.ColumnExpression,
	t3.DataType
})));

Cubes.SelectMany(cub => cub.Dimensions.SelectMany(dim => dim.Hierarchies.Where(hry => hry.HierarchyOrigin.Equals(eHierarchyOrigin.UserDefined))))
.SelectMany(itm => itm.LevelList.SelectMany(lvl => SchemaParser.GetAttributeDefinitions(SchemaObjects).Where(so => so.AttributeID.Equals(lvl.LevelDef.Select(lDef => lDef.LevelSourceAttributeId).FirstOrDefault()))
		.Select(atr => new{
			lvl.CubeName,
			lvl.DimensionUniqueName,
			lvl.HierarchyUniqueName,
			lvl.LevelName,
			lvl.AttributeHierarchyName,
			lvl.LevelCaption,
			ExpectedReportDisplay = String.Empty
//			Origin = (lvl.Origin != null)
//						? System.Enum.Parse(typeof(eHierarchyOrigin),lvl.Origin.ToString())
//						: null,
//			lvl.LevelType,
//			LevelDef = lvl.LevelDef.Select(lDef => lDef.LevelSourceAttributeId).FirstOrDefault(),
//			atr.DataSourceViewId,
//			atr.DimensionName,
//			atr.DimensionID,
//			atr.AllAttributeName,
//			atr.AttributeID,
//			atr.AttributeType,
//			Columns = atr.KeyColumn.Union(atr.NameColumn)
//			.SelectMany(
//				cc => DataSourceLookup.Where(dsvO => 
//					dsvO.ID.Equals(SchemaObjects.Cubes.Where(cNm => cNm.Name.Equals(lvl.CubeName)).First().Source)
//					&& dsvO.TableName.Contains(cc.TableName) 
//					&& dsvO.ColumnName.Equals(cc.ColumnName))
//					.Select(sd => new{
//						sd.TableName, 
//						sd.DbTableName, 
//						sd.FriendlyName, 
//						sd.TableType, 
//						sd.DbSchemaName, 
//						sd.IsPrimaryKey, 
//						sd.ColumnName, 
//						sd.DbColumnName, 
//						sd.ColumnExpression, 
//						sd.DataType 
//					})).Distinct()
		})))
		//.GroupBy(g => g.CubeName)
		//.Select(k => new{k.Key, Dimensions = k.GroupBy(gg => gg.DimensionUniqueName)})
.DistinctBy(d => new{d.CubeName,d.LevelName})
.Dump();
	  
	//AdomdSchemaGuid.Columns will allow you to return the type of "column" from the cube
	//AdomdSchemaGuid.Connections wll get you the current connection information...
	//AdomdSchemaGuid.Functions will return a list of objects tha are valid commands
	//AdomdSchemaGuid.Keywords will return a list of objects that are reserved key words...
	//AdomdSchemaGuid.Literals will return a list of valid literal values
	//AdomdSchemaGuid.MemberProperties will return a list of objects and the properties associated with the members
	//AdomdSchemaGuid.MemoryUsage will return a list of items that are or have used memory for the application
	//AdomdSchemaGuid.ProviderTypes will return a list of data types that are valid in the cube.
	//AdomdSchemaGuid.Sessions will return a list of all open sessions
	//AdomdSchemaGuid.Tables will return a list of items with their definition of Olap Type
	//AdomdSchemaGuid.XmlaProperties seems relevant, just don't know why...
	//var DataSources =MetaDataConnection.GetSchemaDataSet(AdomdSchema.sSchemaList[AdomdSchemaGuid.XmlaProperties].Id,Restrictions).Tables[0];
	

  
  
	if (MetaDataConnection.State != ConnectionState.Closed)
	{
		MetaDataConnection.Close();
	}
	
}

public class AttributeDefinition
{
	public IEnumerable<String> KeyTable {get;set;}
	public IEnumerable<String> KeyColumns {get;set;}
	public IEnumerable<String> NameTable {get;set;}
	public IEnumerable<String> NameColumns {get;set;}
	public String DbSchemaName {get;set;}
	public String FriendlyName {get;set;}
	public IEnumerable<DataColumnSummary> Columns {get;set;} 
	public IEnumerable<DataColumnSummary> PrimaryKeys {get;set;} 
}

public class DataSourceViewSummary
{
	public DataSourceViewSummary (){}
	public DataSourceViewSummary (SchemaParser.oDataSourceView dsv)
	{
		ID = dsv.ID;
		Name = dsv.Name;
		Tables = dsv.Schema.Tables.Cast<DataTable>().Select(tbl => new DataTableSummary() {
			TableName = tbl.TableName,
			DbTableName = tbl.ExtendedProperties["DbTableName"].ToString(),
			FriendlyName = tbl.ExtendedProperties["FriendlyName"].ToString(),
			TableType = tbl.ExtendedProperties["TableType"].ToString(),
			DbSchemaName = tbl.ExtendedProperties["DbSchemaName"].ToString(),
			Columns = tbl.Columns.Cast<DataColumn>().Select(tCol => new DataColumnSummary(tCol)),
			PrimaryKeys = tbl.PrimaryKey.Cast<DataColumn>().Select(tCol => new DataColumnSummary(tCol)),
			ParentRelations = tbl.ParentRelations.Cast<DataRelation>().Select(tParent => new
			{
				Parent = tParent.ParentColumns.Select(pCol => new RelationColumnSummary(pCol)),
				Child = tParent.ChildColumns.Select(pCol => new RelationColumnSummary(pCol)),
			}),
			ChildRelations = tbl.ChildRelations.Cast<DataRelation>().Select(tParent => new{
				Parent = tParent.ParentColumns.Select(pCol => new RelationColumnSummary(pCol)),
				Child = tParent.ChildColumns.Select(pCol => new RelationColumnSummary(pCol)),
			})});
		
	}
	public String ID {get;set;}
	public String Name {get;set;}
	public IEnumerable<DataTableSummary> Tables {get;set;}
}

public class DataTableSummary
{
	public String TableName {get;set;}
	public String DbTableName {get;set;}
	public String FriendlyName {get;set;}
	public String TableType {get;set;}
	public String DbSchemaName {get;set;}
	public IEnumerable<DataColumnSummary> Columns {get;set;}
	public IEnumerable<DataColumnSummary> PrimaryKeys {get;set;}
	public object ParentRelations {get;set;}
	public object ChildRelations  {get;set;}
}

public class RelationColumnSummary : DataColumnSummary
{
	public RelationColumnSummary (){}
	public RelationColumnSummary (DataColumn tCol)
	{
		TableName = tCol.Table.TableName;
		ColumnName = tCol.ColumnName;
		DbColumnName =  tCol.ExtendedProperties["DbColumnName"].ToString();
		ColumnExpression = (tCol.ExtendedProperties["ComputedColumnExpression"] != null)
							? tCol.ExtendedProperties["ComputedColumnExpression"].ToString()
							: String.Empty;
		DataType = tCol.DataType.ToString();
	}

	public String TableName {get;set;}
	
}

public class DataColumnSummary
{
	public DataColumnSummary (){}
	public DataColumnSummary (DataColumn tCol)
	{
			ColumnName = tCol.ColumnName;
			DbColumnName =  tCol.ExtendedProperties["DbColumnName"].ToString();
		ColumnExpression = (tCol.ExtendedProperties["ComputedColumnExpression"] != null)
							? tCol.ExtendedProperties["ComputedColumnExpression"].ToString()
							: String.Empty;
			DataType = tCol.DataType.ToString();
		
	}
	
	public String ColumnName {get;set;}
	public String DbColumnName {get;set;}
	public String ColumnExpression {get;set;}
	public String DataType {get;set;}
}

protected static String ServerName = ".";
//protected static String ServerName = "LT-JFILICHA";
protected static String CatalogName = "BankingCustomerIntelligence_JHLive";
// Define other methods and classes here
    public enum eDataType
    {
        DBTYPE_ARRAY = 0x2000,
        DBTYPE_BOOL = 11,
        DBTYPE_BSTR = 8,
        DBTYPE_BYREF = 0x4000,
        DBTYPE_BYTES = 0x80,
        DBTYPE_CY = 6,
        DBTYPE_DATE = 7,
        DBTYPE_DBDATE = 0x85,
        DBTYPE_DBTIME = 0x86,
        DBTYPE_DBTIMESTAMP = 0x87,
        DBTYPE_DECIMAL = 14,
        DBTYPE_EMPTY = 0,
        DBTYPE_ERROR = 10,
        DBTYPE_GUID = 0x48,
        DBTYPE_I1 = 0x10,
        DBTYPE_I2 = 2,
        DBTYPE_I4 = 3,
        DBTYPE_I8 = 20,
        DBTYPE_IDISPATCH = 9,
        DBTYPE_IUNKNOWN = 13,
        DBTYPE_NULL = 1,
        DBTYPE_NUMERIC = 0x83,
        DBTYPE_R4 = 4,
        DBTYPE_R8 = 5,
        DBTYPE_RESERVED = 0x8000,
        DBTYPE_STR = 0x81,
        DBTYPE_UDT = 0x84,
        DBTYPE_UI1 = 0x11,
        DBTYPE_UI2 = 0x12,
        DBTYPE_UI4 = 0x13,
        DBTYPE_UI8 = 0x15,
        DBTYPE_VARIANT = 12,
        DBTYPE_VECTOR = 0x1000,
        DBTYPE_WSTR = 130
    }
    public enum eDimensionType
    {
        Unknown,
        Time,
        Measure,
        Other,
        MsNotDefined,
        Quantitative,
        Accounts,
        Customers,
        Products,
        Scenario,
        Utility,
        Currency,
        Rates,
        Channel,
        Promotion,
        Organization,
        BillOfMaterials,
        Geography
    }
    public enum eHierarchyOrigin
    {
        Key = 6,
        ParentChild = 3,
        SystemEnabled = 2,
        SystemInternal = 4,
        UserDefined = 1,
        UserDefinedSystemInternal = 5
    }
	public enum eHierarchyStructure
    {
        FullyBalanced,
        RaggedBalanced,
        Unbalanced,
        Network
    }
	public enum eInstanceSelection
    {
        None,
        DropDown,
        List,
        FilteredList,
        MandatoryFilter
    }
	public enum eLevelType
    {
        Account = 0x1014,
        All = 1,
        BomResource = 0x1012,
        Calculated = 2,
        Channel = 0x1061,
        Company = 0x1042,
        CurrencyDestination = 0x1052,
        CurrencySource = 0x1051,
        Customer = 0x1021,
        CustomerGroup = 0x1022,
        CustomerHousehold = 0x1023,
        GeoCity = 0x2006,
        GeoContinent = 0x2001,
        GeoCountry = 0x2003,
        GeoCounty = 0x2005,
        GeoPoint = 0x2008,
        GeoPostalCode = 0x2007,
        GeoRegion = 0x2002,
        GeoStateOrProvince = 0x2004,
        OrgUnit = 0x1011,
        Person = 0x1041,
        Product = 0x1031,
        ProductGroup = 0x1032,
        Promotion = 0x1071,
        Quantitative = 0x1013,
        Regular = 0,
        Representative = 0x1062,
        Reserved1 = 8,
        Scenario = 0x1015,
        Time = 4,
        TimeDays = 0x204,
        TimeHalfYears = 0x24,
        TimeHours = 0x304,
        TimeMinutes = 0x404,
        TimeMonths = 0x84,
        TimeQuarters = 0x44,
        TimeSeconds = 0x804,
        TimeUndefined = 0x1004,
        TimeWeeks = 260,
        TimeYears = 20,
        Utility = 0x1016
    }
	public enum eMeasureAggregator
    {
        Avg = 5,
        AvgChildren = 10,
        ByAccount = 15,
        Calculated = 0xff,
        Count = 2,
        Dst = 8,
        FirstChild = 11,
        FirstNonEmpty = 13,
        LastChild = 12,
        LastNonEmpty = 14,
        Max = 4,
        Min = 3,
        None = 9,
        Std = 7,
        Sum = 1,
        Unknown = 0,
        Var = 6
    }
	[Flags]
    public enum ePropertyType
    {
        MDPROP_BLOB = 8,
        MDPROP_CELL = 2,
        MDPROP_MEMBER = 1,
        MDPROP_SYSTEM = 4
    }
	public enum eRelationCardinality
    {
        One,
        Many
    }
	public enum eServerVersion
    {
        Unknown,
        Shiloh,
        Yukon,
        Katmai
    }


	public interface IMdObject
	{
		string Caption { get; }
		string Name { get; }
		string UniqueName { get; }
	}	

    public class MdObjectList<T> : IEnumerable<T>, IEnumerable where T: IMdObject
    {
        private List<T> mList;
        private SortedList<string, T> mNameList;
        private SortedList<string, T> mUniqueNameList;

        public MdObjectList()
        {
            this.mList = new List<T>();
            this.mUniqueNameList = new SortedList<string, T>();
            this.mNameList = new SortedList<string, T>();
        }

        public void Add(T pMdObject)
        {
            this.mList.Add(pMdObject);
            this.mUniqueNameList.Add(pMdObject.UniqueName, pMdObject);
            this.mNameList.Add(pMdObject.UniqueName, pMdObject);
        }

        public T GetByName(string pName)
        {
            return this.mNameList[pName];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.mList.GetEnumerator();
        }

        public List<string> GetUNameList()
        {
            return new List<string>(this.mUniqueNameList.Keys);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.mList.GetEnumerator();
        }

        public bool TryGetValue(string pUniqueName, out T oValue)
        {
            return this.mUniqueNameList.TryGetValue(pUniqueName, out oValue);
        }

        public int Count
        {
            get
            {
                return this.mList.Count;
            }
        }

        public T this[string pUniqueName]
        {
            get
            {
                return this.mUniqueNameList[pUniqueName];
            }
        }

        public T this[int pIndex]
        {
            get
            {
                return this.mList[pIndex];
            }
        }
    }
	
    public class MdProperty : IMdObject
    {
        private string mAttributeHierarchyName;
        private eRelationCardinality mCardinality;
        private int mContentType;
        private string mCubeName;
        private eDataType mDataType;
        private string mDescription;
        private string mDimensionUniqueName;
        private string mHierarchyUniqueName;
        private bool mIsVisible;
        private string mLevelUniqueName;
        private int mOrigin;
        private string mPropertyCaption;
        private string mPropertyName;
        private ePropertyType mPropertyType;

        public MdProperty(DataRow pRow)
        {
            this.mCubeName = (string) pRow["CUBE_NAME"];
            this.mDimensionUniqueName = (string) pRow["DIMENSION_UNIQUE_NAME"];
            this.mHierarchyUniqueName = (string) pRow["HIERARCHY_UNIQUE_NAME"];
            this.mLevelUniqueName = (string) pRow["LEVEL_UNIQUE_NAME"];
            this.mPropertyName = (string) pRow["PROPERTY_NAME"];
            this.mPropertyCaption = (string) pRow["PROPERTY_CAPTION"];
            this.mDescription = (pRow["DESCRIPTION"] is DBNull) ? string.Empty : ((string) pRow["DESCRIPTION"]);
            this.mPropertyType = (ePropertyType) ((short) pRow["PROPERTY_TYPE"]);
            this.mDataType = (eDataType) ((ushort) pRow["DATA_TYPE"]);
            this.mContentType = (pRow["PROPERTY_CONTENT_TYPE"] is DBNull) ? 0 : ((short) pRow["PROPERTY_CONTENT_TYPE"]);
            this.mOrigin = (ushort) pRow["PROPERTY_ORIGIN"];
            this.mAttributeHierarchyName = (pRow["PROPERTY_ATTRIBUTE_HIERARCHY_NAME"] is DBNull) ? string.Empty : ((string) pRow["PROPERTY_ATTRIBUTE_HIERARCHY_NAME"]);
            this.mCardinality = (((pRow["PROPERTY_CARDINALITY"] is DBNull) ? string.Empty : ((string) pRow["PROPERTY_CARDINALITY"])) == "MANY") ? eRelationCardinality.Many : eRelationCardinality.One;
            this.mIsVisible = (bool) pRow["PROPERTY_IS_VISIBLE"];
        }

        public MdProperty(string pCubeName, string pDimensionUniqueName, string pHierarchyUniqueName, string pLevelUniqueName, string pName, string pCaption, string pDescription, ePropertyType pPropertyType, int pDataType, int pContentType, int pOrigin, string pAttributeHierarchyName, eRelationCardinality pCardinality, bool pIsVisible)
        {
            this.mCubeName = pCubeName;
            this.mDimensionUniqueName = pDimensionUniqueName;
            this.mHierarchyUniqueName = pHierarchyUniqueName;
            this.mLevelUniqueName = pLevelUniqueName;
            this.mPropertyName = pName;
            this.mPropertyCaption = pCaption;
            this.mDescription = pDescription;
            this.mPropertyType = pPropertyType;
            this.mDataType = (eDataType) pDataType;
            this.mContentType = pContentType;
            this.mOrigin = pOrigin;
            this.mAttributeHierarchyName = pAttributeHierarchyName;
            this.mCardinality = pCardinality;
            this.mIsVisible = pIsVisible;
        }

        public MdProperty SetName(string pName)
        {
            return new MdProperty(this.mCubeName, this.mDimensionUniqueName, this.mHierarchyUniqueName, this.mLevelUniqueName, pName, pName, string.Empty, ePropertyType.MDPROP_SYSTEM, 130, this.mContentType, this.mOrigin, this.mAttributeHierarchyName, this.mCardinality, this.mIsVisible);
        }

        public override string ToString()
        {
            return this.mPropertyCaption;
        }

        public string AttributeHierarchyName
        {
            get
            {
                return this.mAttributeHierarchyName;
            }
        }

        public string Caption
        {
            get
            {
                return this.mPropertyCaption;
            }
        }

        public eRelationCardinality Cardinality
        {
            get
            {
                return this.mCardinality;
            }
        }

        public int ContentType
        {
            get
            {
                return this.mContentType;
            }
        }

        public string CubeName
        {
            get
            {
                return this.mCubeName;
            }
        }

        public eDataType DataType
        {
            get
            {
                return this.mDataType;
            }
        }

        public string Description
        {
            get
            {
                return this.mDescription;
            }
        }

        public string DimensionUniqueName
        {
            get
            {
                return this.mDimensionUniqueName;
            }
        }

        public string HierarchyUniqueName
        {
            get
            {
                return this.mHierarchyUniqueName;
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.mIsVisible;
            }
        }

        public string LevelUniqueName
        {
            get
            {
                return this.mLevelUniqueName;
            }
        }

        public string Name
        {
            get
            {
                return this.mPropertyName;
            }
        }

        public int Origin
        {
            get
            {
                return this.mOrigin;
            }
        }

        public ePropertyType PropertyType
        {
            get
            {
                return this.mPropertyType;
            }
        }

        public string UniqueName
        {
            get
            {
                return this.mPropertyName;
            }
        }
    }
	
    public class MdMeasure : IMdObject
    {
        private string mCubeName;
        private int mDataType;
        private string mDescription;
        private string mDisplayFolder;
        private string mExpression;
        private string mFormatString;
        private bool mIsVisible;
        private eMeasureAggregator mMeasureAggregator;
        private string mMeasureCaption;
        private string mMeasureGroupName;
        private string mMeasureName;
        private string mMeasureUniqueName;
        private int mNumericPrecision;
        private int mNumericScale;

        public MdMeasure(DataRow pRow)
        {
            this.mCubeName = (string) pRow["CUBE_NAME"];
            this.mMeasureUniqueName = (string) pRow["MEASURE_UNIQUE_NAME"];
            this.mMeasureName = (string) pRow["MEASURE_NAME"];
            this.mMeasureCaption = (string) pRow["MEASURE_CAPTION"];
            this.mDescription = (pRow["DESCRIPTION"] is DBNull) ? string.Empty : ((string) pRow["DESCRIPTION"]);
            this.mMeasureAggregator = (eMeasureAggregator) ((int) pRow["MEASURE_AGGREGATOR"]);
            this.mDataType = (ushort) pRow["DATA_TYPE"];
            this.mNumericPrecision = (ushort) pRow["NUMERIC_PRECISION"];
            this.mNumericScale = (short) pRow["NUMERIC_SCALE"];
            this.mExpression = pRow["EXPRESSION"] as string;
            this.mIsVisible = (bool) pRow["MEASURE_IS_VISIBLE"];
            this.mMeasureGroupName = ((pRow.Table.Columns.IndexOf("MEASUREGROUP_NAME") < 0) || (pRow["MEASUREGROUP_NAME"] is DBNull)) ? string.Empty : ((string) pRow["MEASUREGROUP_NAME"]);
            this.mDisplayFolder = ((pRow.Table.Columns.IndexOf("MEASURE_DISPLAY_FOLDER") < 0) || (pRow["MEASURE_DISPLAY_FOLDER"] is DBNull)) ? string.Empty : ((string) pRow["MEASURE_DISPLAY_FOLDER"]);
            this.mFormatString = ((pRow.Table.Columns.IndexOf("DEFAULT_FORMAT_STRING") < 0) || (pRow["DEFAULT_FORMAT_STRING"] is DBNull)) ? string.Empty : ((string) pRow["DEFAULT_FORMAT_STRING"]);
        }

        public MdMeasure(string pCubeName, string pUniqueName, string pName, string pCaption, string pDescription, eMeasureAggregator pMeasureAggregator, int pDataType, int pNumericPrecision, int pNumericScale, string pExpression, bool pIsVisible, string pMeasureGroupName, string pDisplayFolder, string pFormatString)
        {
            this.mCubeName = pCubeName;
            this.mMeasureUniqueName = pUniqueName;
            this.mMeasureName = pName;
            this.mMeasureCaption = pCaption;
            this.mDescription = pDescription;
            this.mMeasureAggregator = pMeasureAggregator;
            this.mDataType = pDataType;
            this.mNumericPrecision = pNumericPrecision;
            this.mNumericScale = pNumericScale;
            this.mExpression = pExpression;
            this.mIsVisible = pIsVisible;
            this.mMeasureGroupName = pMeasureGroupName;
            this.mDisplayFolder = pDisplayFolder;
            this.mFormatString = pFormatString;
        }

        public override string ToString()
        {
            return this.mMeasureCaption;
        }

        public string Caption
        {
            get
            {
                return this.mMeasureCaption;
            }
        }

        public string CubeName
        {
            get
            {
                return this.mCubeName;
            }
        }

        public int DataType
        {
            get
            {
                return this.mDataType;
            }
        }

        public string Description
        {
            get
            {
                return this.mDescription;
            }
        }

        public string DisplayFolder
        {
            get
            {
                return this.mDisplayFolder;
            }
        }

        public string Expression
        {
            get
            {
                return this.mExpression;
            }
        }

        public string FormatString
        {
            get
            {
                return this.mFormatString;
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.mIsVisible;
            }
        }

        public eMeasureAggregator MeasureAggregator
        {
            get
            {
                return this.mMeasureAggregator;
            }
        }

        public string MeasureGroupName
        {
            get
            {
                return this.mMeasureGroupName;
            }
        }

        public string Name
        {
            get
            {
                return this.mMeasureName;
            }
        }

        public int NumericPrecision
        {
            get
            {
                return this.mNumericPrecision;
            }
        }

        public int NumericScale
        {
            get
            {
                return this.mNumericScale;
            }
        }

        public string UniqueName
        {
            get
            {
                return this.mMeasureUniqueName;
            }
        }
    }
	
	public class IMdObjectCaptionComparer<T> : IComparer<T> where T: IMdObject
    {
        int IComparer<T>.Compare(T x, T y)
        {
            return string.Compare(x.Caption, y.Caption, true);
        }
    }
	
    public class MdMeasureGroupDimension
    {
        private string mCubeName;
        private eRelationCardinality mDimensionCardinality;
        private string mDimensionGranularity;
        private string mDimensionUniqueName;
        private bool mIsDimensionVisible;
        private bool mIsFactDimension;
        private eRelationCardinality mMeasureGroupCardinality;
        private string mMeasureGroupName;

        public MdMeasureGroupDimension(DataRow pRow)
        {
            this.mCubeName = (string) pRow["CUBE_NAME"];
            this.mMeasureGroupName = (string) pRow["MEASUREGROUP_NAME"];
            this.mMeasureGroupCardinality = (((string) pRow["MEASUREGROUP_CARDINALITY"]) == "MANY") ? eRelationCardinality.Many : eRelationCardinality.One;
            this.mDimensionUniqueName = (string) pRow["DIMENSION_UNIQUE_NAME"];
            this.mDimensionCardinality = (((string) pRow["DIMENSION_CARDINALITY"]) == "MANY") ? eRelationCardinality.Many : eRelationCardinality.One;
            this.mIsDimensionVisible = (bool) pRow["DIMENSION_IS_VISIBLE"];
            this.mIsFactDimension = (bool) pRow["DIMENSION_IS_FACT_DIMENSION"];
            this.mDimensionGranularity = (string) pRow["DIMENSION_GRANULARITY"];
        }

        public MdMeasureGroupDimension(string pCubeName, string pMeasureGroupName, eRelationCardinality pMeasureGroupCardinality, string pDimensionUniqueName, eRelationCardinality pDimensionCradinality, bool pIsDimensionVisible, bool pIsFactDimension, string pDimensionGranularity)
        {
            this.mCubeName = pCubeName;
            this.mMeasureGroupName = pMeasureGroupName;
            this.mMeasureGroupCardinality = pMeasureGroupCardinality;
            this.mDimensionUniqueName = pDimensionUniqueName;
            this.mDimensionCardinality = pDimensionCradinality;
            this.mIsDimensionVisible = pIsDimensionVisible;
            this.mIsFactDimension = pIsFactDimension;
            this.mDimensionGranularity = pDimensionGranularity;
        }

        public override string ToString()
        {
            return (this.mMeasureGroupName + " - " + this.mDimensionUniqueName);
        }

        public string CubeName
        {
            get
            {
                return this.mCubeName;
            }
        }

        public eRelationCardinality DimensionCardinality
        {
            get
            {
                return this.mDimensionCardinality;
            }
        }

        public string DimensionGranularity
        {
            get
            {
                return this.mDimensionGranularity;
            }
        }

        public string DimensionUniqueName
        {
            get
            {
                return this.mDimensionUniqueName;
            }
        }

        public bool IsDimensionVisible
        {
            get
            {
                return this.mIsDimensionVisible;
            }
        }

        public bool IsFactDimension
        {
            get
            {
                return this.mIsFactDimension;
            }
        }

        public eRelationCardinality MeasureGroupCardinality
        {
            get
            {
                return this.mMeasureGroupCardinality;
            }
        }

        public string MeasureGroupName
        {
            get
            {
                return this.mMeasureGroupName;
            }
        }
    }
	
    public class MdMeasureGroup : IMdObject
    {
        private string mCubeName;
        private string mDescription;
        private bool mIsWriteEnabled;
        private string mMeasureGroupCaption;
        private List<MdMeasureGroupDimension> mMeasureGroupDimensionList;
        private string mMeasureGroupName;

        public MdMeasureGroup(DataRow pRow)
        {
            this.mCubeName = (string) pRow["CUBE_NAME"];
            this.mMeasureGroupName = (string) pRow["MEASUREGROUP_NAME"];
            this.mMeasureGroupCaption = (string) pRow["MEASUREGROUP_CAPTION"];
            this.mDescription = (string) pRow["DESCRIPTION"];
            this.mIsWriteEnabled = (bool) pRow["IS_WRITE_ENABLED"];
            this.mMeasureGroupDimensionList = new List<MdMeasureGroupDimension>();
        }

        public MdMeasureGroup(string pCubeName, string pName, string pCaption, string pDescription, bool pIsWriteEnabled)
        {
            this.mCubeName = pCubeName;
            this.mMeasureGroupName = pName;
            this.mMeasureGroupCaption = pCaption;
            this.mDescription = pDescription;
            this.mIsWriteEnabled = pIsWriteEnabled;
            this.mMeasureGroupDimensionList = new List<MdMeasureGroupDimension>();
        }

        public override string ToString()
        {
            return this.mMeasureGroupCaption;
        }

        public string Caption
        {
            get
            {
                return this.mMeasureGroupCaption;
            }
        }

        public string CubeName
        {
            get
            {
                return this.mCubeName;
            }
        }

        public string Description
        {
            get
            {
                return this.mDescription;
            }
        }

        public List<MdMeasureGroupDimension> Dimensions
        {
            get
            {
                return this.mMeasureGroupDimensionList;
            }
        }

        public bool IsWriteEnabled
        {
            get
            {
                return this.mIsWriteEnabled;
            }
        }

        public string Name
        {
            get
            {
                return this.mMeasureGroupName;
            }
        }

        public string UniqueName
        {
            get
            {
                return this.mMeasureGroupName;
            }
        }
    }
	
    public class MdSet : IMdObject
    {
        private string mCubeName;
        private string mDescription;
        private string mDimensions;
        private string mDisplayFolder;
        private string mExpression;
        private int mScope;
        private string mSetCaption;
        private string mSetName;
        private string mSetUniqueName;

        public MdSet(DataRow pRow)
        {
            this.mCubeName = (string) pRow["CUBE_NAME"];
            this.mSetName = (string) pRow["SET_NAME"];
            this.mSetUniqueName = "[" + this.mSetName + "]";
            this.mSetCaption = (string) pRow["SET_CAPTION"];
            this.mDescription = (pRow["DESCRIPTION"] is DBNull) ? string.Empty : ((string) pRow["DESCRIPTION"]);
            this.mScope = (int) pRow["SCOPE"];
            this.mExpression = pRow["EXPRESSION"] as string;
            this.mDimensions = (string) pRow["DIMENSIONS"];
            this.mDisplayFolder = ((pRow.Table.Columns.IndexOf("SET_DISPLAY_FOLDER") < 0) || (pRow["SET_DISPLAY_FOLDER"] is DBNull)) ? string.Empty : ((string) pRow["SET_DISPLAY_FOLDER"]);
        }

        public override string ToString()
        {
            return this.mSetCaption;
        }

        public string Caption
        {
            get
            {
                return this.mSetCaption;
            }
        }

        public string CubeName
        {
            get
            {
                return this.mCubeName;
            }
        }

        public string Description
        {
            get
            {
                return this.mDescription;
            }
        }

        public string Dimensions
        {
            get
            {
                return this.mDimensions;
            }
        }

        public string DisplayFolder
        {
            get
            {
                return this.mDisplayFolder;
            }
        }

        public string Expression
        {
            get
            {
                return this.mExpression;
            }
        }

        public string Name
        {
            get
            {
                return this.mSetName;
            }
        }

        public int Scope
        {
            get
            {
                return this.mScope;
            }
        }

        public string UniqueName
        {
            get
            {
                return this.mSetUniqueName;
            }
        }
    }
	
    public class MdKpi : IMdObject
    {
        private string mAnnotations;
        private string mCubeName;
        private string mKpiCaption;
        private string mKpiCurrentTimeMember;
        private string mKpiDescription;
        private string mKpiDisplayFolder;
        private string mKpiGoal;
        private string mKpiName;
        private string mKpiParentKpiName;
        private string mKpiStatus;
        private string mKpiStatusGraphic;
        private string mKpiTrend;
        private string mKpiTrendGraphic;
        private string mKpiUniqueName;
        private string mKpiValue;
        private string mKpiWeight;
        private string mMeasureGroupName;

        public MdKpi(DataRow pRow)
        {
            this.mCubeName = (string) pRow["CUBE_NAME"];
            this.mKpiUniqueName = "";
            this.mKpiName = (string) pRow["KPI_NAME"];
            this.mKpiCaption = (string) pRow["KPI_CAPTION"];
            this.mKpiDescription = (pRow["KPI_DESCRIPTION"] is DBNull) ? string.Empty : ((string) pRow["KPI_DESCRIPTION"]);
            this.mKpiValue = (string) pRow["KPI_VALUE"];
            this.mKpiGoal = (string) pRow["KPI_GOAL"];
            this.mKpiStatus = (string) pRow["KPI_STATUS"];
            this.mKpiTrend = (string) pRow["KPI_TREND"];
            this.mKpiStatus = (string) pRow["KPI_STATUS"];
            this.mKpiTrendGraphic = (string) pRow["KPI_TREND_GRAPHIC"];
            this.mKpiStatusGraphic = (string) pRow["KPI_STATUS_GRAPHIC"];
            this.mKpiWeight = (string) pRow["KPI_WEIGHT"];
            this.mKpiCurrentTimeMember = (pRow["KPI_CURRENT_TIME_MEMBER"] is DBNull) ? string.Empty : ((string) pRow["KPI_CURRENT_TIME_MEMBER"]);
            this.mKpiParentKpiName = (string) pRow["KPI_PARENT_KPI_NAME"];
            this.mAnnotations = (string) pRow["ANNOTATIONS"];
            this.mMeasureGroupName = (pRow["MEASUREGROUP_NAME"] is DBNull) ? string.Empty : ((string) pRow["MEASUREGROUP_NAME"]);
            this.mKpiDisplayFolder = (pRow["KPI_DISPLAY_FOLDER"] is DBNull) ? string.Empty : ((string) pRow["KPI_DISPLAY_FOLDER"]);
        }

        public override string ToString()
        {
            return this.mKpiCaption;
        }

        public string Annotations
        {
            get
            {
                return this.mAnnotations;
            }
        }

        public string Caption
        {
            get
            {
                return this.mKpiCaption;
            }
        }

        public string CubeName
        {
            get
            {
                return this.mCubeName;
            }
        }

        public string CurrentTimeMember
        {
            get
            {
                return this.mKpiCurrentTimeMember;
            }
        }

        public string Description
        {
            get
            {
                return this.mKpiDescription;
            }
        }

        public string DisplayFolder
        {
            get
            {
                return this.mKpiDisplayFolder;
            }
        }

        public string Goal
        {
            get
            {
                return this.mKpiGoal;
            }
        }

        public string MeasureGroupName
        {
            get
            {
                return this.mMeasureGroupName;
            }
        }

        public string Name
        {
            get
            {
                return this.mKpiName;
            }
        }

        public string ParentKpiName
        {
            get
            {
                return this.mKpiParentKpiName;
            }
        }

        public string Status
        {
            get
            {
                return this.mKpiStatus;
            }
        }

        public string StatusGraphic
        {
            get
            {
                return this.mKpiStatusGraphic;
            }
        }

        public string Trend
        {
            get
            {
                return this.mKpiTrend;
            }
        }

        public string TrendGraphic
        {
            get
            {
                return this.mKpiTrendGraphic;
            }
        }

        public string UniqueName
        {
            get
            {
                return this.mKpiUniqueName;
            }
        }

        public string Value
        {
            get
            {
                return this.mKpiValue;
            }
        }

        public string Weight
        {
            get
            {
                return this.mKpiWeight;
            }
        }
    }
		
    public class MdLevel : IMdObject
    {
        private string mAttributeHierarchyName;
        private string mCubeName;
        private string mDescription;
        private string mDimensionUniqueName;
        private string mHierarchyUniqueName;
        private bool mIsVisible;
        private int mKeyCardinality;
        private string mLevelCaption;
        private int mLevelCardinality;
        private string mLevelName;
        private int mLevelNumber;
        private eLevelType mLevelType;
        private string mLevelUniqueName;
        private int mOrigin;
        private MdObjectList<MdProperty> mPropertyList;

        public MdLevel(DataRow pRow)
        {
            this.mCubeName = (string) pRow["CUBE_NAME"];
            this.mDimensionUniqueName = (string) pRow["DIMENSION_UNIQUE_NAME"];
            this.mHierarchyUniqueName = (string) pRow["HIERARCHY_UNIQUE_NAME"];
            this.mLevelUniqueName = (string) pRow["LEVEL_UNIQUE_NAME"];
            this.mLevelName = (string) pRow["LEVEL_NAME"];
            this.mLevelCaption = (string) pRow["LEVEL_CAPTION"];
            this.mDescription = (pRow["DESCRIPTION"] is DBNull) ? string.Empty : ((string) pRow["DESCRIPTION"]);
            this.mLevelNumber = (int) ((uint) pRow["LEVEL_NUMBER"]);
            this.mLevelCardinality = (int) ((uint) pRow["LEVEL_CARDINALITY"]);
            this.mLevelType = (eLevelType) ((int) pRow["LEVEL_TYPE"]);
            this.mIsVisible = (bool) pRow["LEVEL_IS_VISIBLE"];
            this.mAttributeHierarchyName = ((pRow.Table.Columns.IndexOf("LEVEL_ATTRIBUTE_HIERARCHY_NAME") < 0) || (pRow["LEVEL_ATTRIBUTE_HIERARCHY_NAME"] is DBNull)) ? string.Empty : ((string) pRow["LEVEL_ATTRIBUTE_HIERARCHY_NAME"]);
            this.mKeyCardinality = (pRow.Table.Columns.IndexOf("LEVEL_KEY_CARDINALITY") < 0) ? 0 : ((ushort) pRow["LEVEL_KEY_CARDINALITY"]);
            this.mOrigin = (pRow.Table.Columns.IndexOf("LEVEL_KEY_CARDINALITY") < 0) ? 0 : ((ushort) pRow["LEVEL_ORIGIN"]);
            this.mPropertyList = new MdObjectList<MdProperty>();
        }

        public MdLevel(string pCubeName, string pDimensionUniqueName, string pHierarchyUniqueName, string pUniqueName, string pName, string pCaption, string pDescription, int pNumber, int pCardinality, eLevelType pType, bool pIsVisible, string pAttributeHierarchyName, int pKeyKardinality, int pOrigin)
        {
            this.mCubeName = pCubeName;
            this.mDimensionUniqueName = pDimensionUniqueName;
            this.mHierarchyUniqueName = pHierarchyUniqueName;
            this.mLevelUniqueName = pUniqueName;
            this.mLevelName = pName;
            this.mLevelCaption = pCaption;
            this.mDescription = pDescription;
            this.mLevelNumber = pNumber;
            this.mLevelCardinality = pCardinality;
            this.mLevelType = pType;
            this.mIsVisible = pIsVisible;
            this.mAttributeHierarchyName = pAttributeHierarchyName;
            this.mKeyCardinality = pKeyKardinality;
            this.mOrigin = pOrigin;
            this.mPropertyList = new MdObjectList<MdProperty>();
        }

        public override string ToString()
        {
            return this.mLevelCaption;
        }

        public string AttributeHierarchyName
        {
            get
            {
                return this.mAttributeHierarchyName;
            }
        }

        public string Caption
        {
            get
            {
                return this.mLevelCaption;
            }
        }

        public int Cardinality
        {
            get
            {
                return this.mLevelCardinality;
            }
        }

        public string CubeName
        {
            get
            {
                return this.mCubeName;
            }
        }

        public string Description
        {
            get
            {
                return this.mDescription;
            }
        }

        public string DimensionUniqueName
        {
            get
            {
                return this.mDimensionUniqueName;
            }
        }

        public string HierarchyUniqueName
        {
            get
            {
                return this.mHierarchyUniqueName;
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.mIsVisible;
            }
        }

        public int KeyKardinality
        {
            get
            {
                return this.mKeyCardinality;
            }
        }

        public eLevelType LevelType
        {
            get
            {
                return this.mLevelType;
            }
        }

        public string Name
        {
            get
            {
                return this.mLevelName;
            }
        }

        public int Number
        {
            get
            {
                return this.mLevelNumber;
            }
        }

        public int Origin
        {
            get
            {
                return this.mOrigin;
            }
        }

        public MdObjectList<MdProperty> Properties
        {
            get
            {
                return this.mPropertyList;
            }
        }

        public string UniqueName
        {
            get
            {
                return this.mLevelUniqueName;
            }
        }
    }
		
    public class MdHierarchy : IMdObject
    {
        private string mAllMember;
        private string mCubeName;
        private string mDefaultMember;
        private string mDescription;
        private eDimensionType mDimensionType;
        private string mDimensionUniqueName;
        private string mDisplayFolder;
        private int mGroupingBehaviour;
        private string mHierarchyCaption;
        private int mHierarchyCardinality;
        private string mHierarchyName;
        private int mHierarchyOrdinal;
        private eHierarchyOrigin mHierarchyOrigin;
        private string mHierarchyUniqueName;
        private eInstanceSelection mInstanceSelection;
        private bool mIsVisible;
        private bool mIsWriteEnabled;
        private MdObjectList<MdLevel> mLevelList;
        private eHierarchyStructure mStructure;
        private string mUniqueCaption;

        public MdHierarchy(DataRow pRow)
        {
            this.mCubeName = (string) pRow["CUBE_NAME"];
            this.mDimensionUniqueName = (string) pRow["DIMENSION_UNIQUE_NAME"];
            this.mHierarchyUniqueName = (string) pRow["HIERARCHY_UNIQUE_NAME"];
            this.mHierarchyName = (pRow["HIERARCHY_NAME"] is DBNull) ? string.Empty : ((string) pRow["HIERARCHY_NAME"]);
            this.mHierarchyCaption = (string) pRow["HIERARCHY_CAPTION"];
            this.mDescription = (pRow["DESCRIPTION"] is DBNull) ? string.Empty : ((string) pRow["DESCRIPTION"]);
            this.mAllMember = (pRow["ALL_MEMBER"] is DBNull) ? string.Empty : ((string) pRow["ALL_MEMBER"]);
            this.mDefaultMember = (string) pRow["DEFAULT_MEMBER"];
            this.mDisplayFolder = ((pRow.Table.Columns.IndexOf("HIERARCHY_DISPLAY_FOLDER") < 0) || (pRow["HIERARCHY_DISPLAY_FOLDER"] is DBNull)) ? string.Empty : ((string) pRow["HIERARCHY_DISPLAY_FOLDER"]);
            this.mHierarchyOrdinal = (int) ((uint) pRow["HIERARCHY_ORDINAL"]);
            this.mHierarchyCardinality = (int) ((uint) pRow["HIERARCHY_CARDINALITY"]);
            this.mIsWriteEnabled = (bool) pRow["IS_READWRITE"];
            this.mIsVisible = (pRow.Table.Columns.IndexOf("HIERARCHY_IS_VISIBLE") < 0) || ((bool) pRow["HIERARCHY_IS_VISIBLE"]);
            this.mDimensionType = (eDimensionType) ((short) pRow["DIMENSION_TYPE"]);
            this.mStructure = (eHierarchyStructure) ((short) pRow["STRUCTURE"]);
            this.mHierarchyOrigin = (pRow.Table.Columns.IndexOf("HIERARCHY_ORIGIN") < 0) ? eHierarchyOrigin.UserDefined : ((eHierarchyOrigin) ((ushort) pRow["HIERARCHY_ORIGIN"]));
            this.mInstanceSelection = ((pRow.Table.Columns.IndexOf("INSTANCE_SELECTION") < 0) || (pRow["INSTANCE_SELECTION"] is DBNull)) ? eInstanceSelection.None : ((eInstanceSelection) ((ushort) pRow["INSTANCE_SELECTION"]));
            this.mGroupingBehaviour = (pRow.Table.Columns.IndexOf("GROUPING_BEHAVIOR") < 0) ? 0 : ((ushort) pRow["GROUPING_BEHAVIOR"]);
            this.mLevelList = new MdObjectList<MdLevel>();
        }

        public MdHierarchy(string pCubeName, string pDimensionUniqueName, string pUniqueName, string pName, string pCaption, string pDescription, string pAllMember, string pDefaultMember, string pDisplayFolder, int pOrdinal, int pCardinality, bool pIsWriteEnabled, bool pIsVisible, eDimensionType pType, eHierarchyStructure pStructure, eHierarchyOrigin pOrigin, eInstanceSelection pInstanceSelection, int pGroupingBehaviour)
        {
            this.mCubeName = pCubeName;
            this.mDimensionUniqueName = pDimensionUniqueName;
            this.mHierarchyUniqueName = pUniqueName;
            this.mHierarchyName = pName;
            this.mHierarchyCaption = pCaption;
            this.mDescription = pDescription;
            this.mAllMember = pAllMember;
            this.mDefaultMember = pDefaultMember;
            this.mDisplayFolder = pDisplayFolder;
            this.mHierarchyOrdinal = pOrdinal;
            this.mHierarchyCardinality = pCardinality;
            this.mIsWriteEnabled = pIsWriteEnabled;
            this.mIsVisible = pIsVisible;
            this.mDimensionType = pType;
            this.mStructure = pStructure;
            this.mHierarchyOrigin = pOrigin;
            this.mInstanceSelection = pInstanceSelection;
            this.mGroupingBehaviour = pGroupingBehaviour;
            this.mLevelList = new MdObjectList<MdLevel>();
        }

        public void SetCaption(string pCaption)
        {
            this.mHierarchyCaption = pCaption;
        }

        public void SetUniqueCaption(string pString)
        {
            this.mUniqueCaption = pString;
        }

        public override string ToString()
        {
            return this.mUniqueCaption;
        }

        public string AllMember
        {
            get
            {
                return this.mAllMember;
            }
        }

        public string Caption
        {
            get
            {
                return this.mHierarchyCaption;
            }
        }

        public int Cardinality
        {
            get
            {
                return this.mHierarchyCardinality;
            }
        }

        public string CubeName
        {
            get
            {
                return this.mCubeName;
            }
        }

        public string DefaultMember
        {
            get
            {
                return this.mDefaultMember;
            }
        }

        public string Description
        {
            get
            {
                return this.mDescription;
            }
        }

        public string DimensionUniqueName
        {
            get
            {
                return this.mDimensionUniqueName;
            }
        }

        public string DisplayFolder
        {
            get
            {
                return this.mDisplayFolder;
            }
        }

        public int GroupingBehaviour
        {
            get
            {
                return this.mGroupingBehaviour;
            }
        }

        public eInstanceSelection InstanceSelection
        {
            get
            {
                return this.mInstanceSelection;
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.mIsVisible;
            }
        }

        public bool IsWriteEnabled
        {
            get
            {
                return this.mIsWriteEnabled;
            }
        }

        public MdObjectList<MdLevel> Levels
        {
            get
            {
                return this.mLevelList;
            }
        }

        public string Name
        {
            get
            {
                return this.mHierarchyName;
            }
        }

        public int Ordinal
        {
            get
            {
                return this.mHierarchyOrdinal;
            }
        }

        public eHierarchyOrigin Origin
        {
            get
            {
                return this.mHierarchyOrigin;
            }
        }

        public eHierarchyStructure Structure
        {
            get
            {
                return this.mStructure;
            }
        }

        public eDimensionType Type
        {
            get
            {
                return this.mDimensionType;
            }
        }

        public string UniqueCaption
        {
            get
            {
                return this.mUniqueCaption;
            }
        }

        public string UniqueName
        {
            get
            {
                return this.mHierarchyUniqueName;
            }
        }
    }
	
    public class MdDimension : IMdObject
    {
        private string mCubeName;
        private string mDescription;
        private string mDimensionCaption;
        private string mDimensionName;
        private int mDimensionOrdinal;
        private eDimensionType mDimensionType;
        private string mDimensionUniqueName;
        private MdObjectList<MdHierarchy> mHierarchyList;
        private bool mIsVisible;
        private bool mIsWriteEnabled;
        private int mKeyAttributeCardinality;
        private string mMasterName;

        public MdDimension(DataRow pRow)
        {
            this.mCubeName = (string) pRow["CUBE_NAME"];
            this.mDimensionUniqueName = (string) pRow["DIMENSION_UNIQUE_NAME"];
            this.mDimensionName = (string) pRow["DIMENSION_NAME"];
            this.mDimensionCaption = (string) pRow["DIMENSION_CAPTION"];
            this.mDescription = (pRow["DESCRIPTION"] is DBNull) ? string.Empty : ((string) pRow["DESCRIPTION"]);
            this.mMasterName = (pRow.Table.Columns.IndexOf("DIMENSION_MASTER_NAME") < 0) ? string.Empty : ((string) pRow["DIMENSION_MASTER_NAME"]);
            this.mDimensionType = (eDimensionType) ((short) pRow["DIMENSION_TYPE"]);
            this.mDimensionOrdinal = (int) ((uint) pRow["DIMENSION_ORDINAL"]);
            this.mKeyAttributeCardinality = (int) ((uint) pRow["DIMENSION_CARDINALITY"]);
            this.mIsWriteEnabled = (bool) pRow["IS_READWRITE"];
            this.mIsVisible = (bool) pRow["DIMENSION_IS_VISIBLE"];
            this.mHierarchyList = new MdObjectList<MdHierarchy>();
        }

        public MdDimension(string pCubeName, string pUniqueName, string pName, string pCaption, string pDescription, string pMasterName, eDimensionType pType, int pOrdinal, int pCardinality, bool pIsWriteEnabled, bool pIsVisible)
        {
            this.mCubeName = pCubeName;
            this.mDimensionUniqueName = pUniqueName;
            this.mDimensionName = pName;
            this.mDimensionCaption = pCaption;
            this.mDescription = pDescription;
            this.mMasterName = pMasterName;
            this.mDimensionType = pType;
            this.mDimensionOrdinal = pOrdinal;
            this.mKeyAttributeCardinality = pCardinality;
            this.mIsWriteEnabled = pIsWriteEnabled;
            this.mIsVisible = pIsVisible;
            this.mHierarchyList = new MdObjectList<MdHierarchy>();
        }

        public override string ToString()
        {
            return this.mDimensionCaption;
        }

        public string Caption
        {
            get
            {
                return this.mDimensionCaption;
            }
        }

        public int Cardinality
        {
            get
            {
                return this.mKeyAttributeCardinality;
            }
        }

        public string CubeName
        {
            get
            {
                return this.mCubeName;
            }
        }

        public string Description
        {
            get
            {
                return this.mDescription;
            }
        }

        public eDimensionType DimensionType
        {
            get
            {
                return this.mDimensionType;
            }
        }

        public MdObjectList<MdHierarchy> Hierarchies
        {
            get
            {
                return this.mHierarchyList;
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.mIsVisible;
            }
        }

        public bool IsWriteEnabled
        {
            get
            {
                return this.mIsWriteEnabled;
            }
        }

        public string MasterName
        {
            get
            {
                return this.mMasterName;
            }
        }

        public string Name
        {
            get
            {
                return this.mDimensionName;
            }
        }

        public int Ordinal
        {
            get
            {
                return this.mDimensionOrdinal;
            }
        }

        public string UniqueName
        {
            get
            {
                return this.mDimensionUniqueName;
            }
        }
    }
	
    public class MdCube : IMdObject
    {
        private string mBaseCubeName;
        private string mCubeCaption;
        private string mCubeName;
        private string mDescription;
        private MdObjectList<MdDimension> mDimensionList;
        private bool mIsDimensionCube;
        private bool mIsLinkable;
        private bool mIsVitual;
        private bool mIsWriteEnabled;
        private MdObjectList<MdKpi> mKpiList;
        private DateTime mLastDataUpdate;
        private DateTime mLastSchemaUpdate;
        private MdObjectList<MdMeasureGroup> mMeasureGroupList;
        private MdObjectList<MdMeasure> mMeasureList;
        private MdObjectList<MdSet> mSetList;

        public MdCube(DataRow pRow)
        {
            this.mCubeName = (string) pRow["CUBE_NAME"];
            this.mCubeCaption = (pRow.Table.Columns.IndexOf("CUBE_CAPTION") >= 0)
                                    ? ((string) pRow["CUBE_CAPTION"])
                                    : this.mCubeName;
            this.mDescription = (pRow["DESCRIPTION"] is DBNull) ? string.Empty : ((string) pRow["DESCRIPTION"]);
            this.mIsDimensionCube = (pRow.Table.Columns.IndexOf("CUBE_SOURCE") >= 0) &&
                                    (((ushort) pRow["CUBE_SOURCE"]) == 2);
            this.mIsLinkable = (bool) pRow["IS_LINKABLE"];
            this.mIsVitual = pRow["CUBE_TYPE"].ToString().ToUpper() == "VIRTUAL CUBE";
            this.mIsWriteEnabled = (bool) pRow["IS_WRITE_ENABLED"];
            this.mLastSchemaUpdate = (DateTime) pRow["LAST_SCHEMA_UPDATE"];
            this.mLastDataUpdate = (DateTime) pRow["LAST_DATA_UPDATE"];
            this.mBaseCubeName = (pRow.Table.Columns.IndexOf("BASE_CUBE_NAME") >= 0)
                                     ? ((pRow["BASE_CUBE_NAME"] is DBNull)
                                            ? string.Empty
                                            : ((string) pRow["BASE_CUBE_NAME"]))
                                     : string.Empty;
//            this.mDimensionList = new MdObjectList<MdDimension>();
//            this.mMeasureList = new MdObjectList<MdMeasure>();
//            this.mMeasureGroupList = new MdObjectList<MdMeasureGroup>();
//            this.mSetList = new MdObjectList<MdSet>();
//            this.mKpiList = new MdObjectList<MdKpi>();
        }

        public MdCube(
			string pName, 
			string pCaption, 
			string pDescription, 
			bool pIsDimensionCube, 
			bool pIsLinkable, 
			bool pIsWriteEnabled, 
			DateTime pLastSchemaUpdate, 
			DateTime pLastDataUpdate, 
			string pBaseCubeName
			)
        {
            this.mCubeName = pName;
            this.mCubeCaption = pCaption;
            this.mDescription = pDescription;
            this.mIsDimensionCube = pIsDimensionCube;
            this.mIsLinkable = pIsLinkable;
            this.mIsWriteEnabled = pIsWriteEnabled;
            this.mLastSchemaUpdate = pLastSchemaUpdate;
            this.mLastDataUpdate = pLastDataUpdate;
            this.mBaseCubeName = pBaseCubeName;
//            this.mDimensionList = new MdObjectList<MdDimension>();
//            this.mMeasureList = new MdObjectList<MdMeasure>();
//            this.mMeasureGroupList = new MdObjectList<MdMeasureGroup>();
//            this.mSetList = new MdObjectList<MdSet>();
//            this.mKpiList = new MdObjectList<MdKpi>();
        }

        public MdObjectList<MdDimension> Dimensions
        {
			get;set;
//            get
//            {
//                return this.mDimensionList;
//            }
//			
//			set
//			{
//				mDimensionList = value;
//			}
        }

        public MdObjectList<MdKpi> Kpis
        {
		get;set;
//            get
//            {
//                return this.mKpiList;
//            }
//			
//			set
//			{
//				mKpiList = value;
//			}
        }

        public MdObjectList<MdMeasureGroup> MeasureGroups
        {
		get;set;
//            get
//            {
//                return this.mMeasureGroupList;
//            }
//			
//			set
//			{
//				mMeasureGroupList = value;
//			}
        }

        public MdObjectList<MdMeasure> Measures
        {
		get; set;
//            get
//            {
//                return this.mMeasureList;
//            }
//			
//			set
//			{
//				mMeasureList = value;
//			}
        }

        public MdObjectList<MdSet> Sets
        {
		get; set;
//            get
//            {
//                return this.msetlist;
//            }
//			
//			set
//			{
//				msetlist = value;
//			}
        }

        public override string ToString()
        {
            return this.mCubeCaption;
        }

        public string BaseCubeName
        {
            get
            {
                return this.mBaseCubeName;
            }
        }

        public string Caption
        {
            get
            {
                return this.mCubeCaption;
            }
        }

        public string Description
        {
            get
            {
                return this.mDescription;
            }
        }
        public bool IsDimensionCube
        {
            get
            {
                return this.mIsDimensionCube;
            }
        }

        public bool IsLinkable
        {
            get
            {
                return this.mIsLinkable;
            }
        }

        public bool IsPespective
        {
            get
            {
                return !string.IsNullOrEmpty(this.mBaseCubeName);
            }
        }

        public bool IsVirtual
        {
            get
            {
                return this.mIsVitual;
            }
        }

        public bool IsWriteEnabled
        {
            get
            {
                return this.mIsWriteEnabled;
            }
        }

        public DateTime LastDataUpdate
        {
            get
            {
                return this.mLastDataUpdate;
            }
        }

        public DateTime LastSchemaUpdate
        {
            get
            {
                return this.mLastSchemaUpdate;
            }
        }

        public string Name
        {
            get
            {
                return this.mCubeName;
            }
        }


        public string UniqueName
        {
            get
            {
                return this.mCubeName;
            }
        }
    }
	
	
    public class MdSchemaShared
    {
        public const string cPropKEY0 = "KEY0";
        public const string cPropMEMBER_VALUE = "MEMBER_VALUE";
        public const string cPropNAME = "NAME";
    }
public class Utils
{

        public static String ConnectionString(String ServerName)
        {
            return ConnectionString(ServerName, null);
        }

        public static String ConnectionString(String ServerName, String InitialCatalog)
        {
            return !String.IsNullOrEmpty(InitialCatalog)
                       ? string.Format("Data Source={0};Application Name=CubeParser v{1}; Initial Catalog={2}",
                                       ServerName,
                                       System.Reflection.Assembly.GetExecutingAssembly().GetName().Version, InitialCatalog)
                       : string.Format("Data Source={0};Application Name=CubeParser v{1};",
                                       ServerName,
                                       System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
        }
}

private class AdomdSchema
    {
        private System.Guid mGuid;
        private string mId;
        private string mName;
        public static Dictionary<System.Guid, AdomdSchema> sSchemaList = new Dictionary<System.Guid, AdomdSchema>();

        static AdomdSchema()
        {
            sSchemaList.Add(AdomdSchemaGuid.Actions, new AdomdSchema("MDSCHEMA_ACTIONS", AdomdSchemaGuid.Actions, "Actions"));
            sSchemaList.Add(AdomdSchemaGuid.Catalogs, new AdomdSchema("DBSCHEMA_CATALOGS", AdomdSchemaGuid.Catalogs, "Catalogs"));
            sSchemaList.Add(AdomdSchemaGuid.Columns, new AdomdSchema("DBSCHEMA_COLUMNS", AdomdSchemaGuid.Columns, "Columns"));
            sSchemaList.Add(AdomdSchemaGuid.Connections, new AdomdSchema("DISCOVER_CONNECTIONS", AdomdSchemaGuid.Connections, "Connections"));
            sSchemaList.Add(AdomdSchemaGuid.Cubes, new AdomdSchema("MDSCHEMA_CUBES", AdomdSchemaGuid.Cubes, "Cubes"));
            sSchemaList.Add(AdomdSchemaGuid.DataSources, new AdomdSchema("DISCOVER_DATASOURCES", AdomdSchemaGuid.DataSources, "DataSources"));
            sSchemaList.Add(AdomdSchemaGuid.DBConnections, new AdomdSchema("DISCOVER_DB_CONNECTIONS", AdomdSchemaGuid.DBConnections, "DBConnections"));
            sSchemaList.Add(AdomdSchemaGuid.Dimensions, new AdomdSchema("MDSCHEMA_DIMENSIONS", AdomdSchemaGuid.Dimensions, "Dimensions"));
            sSchemaList.Add(AdomdSchemaGuid.DimensionStat, new AdomdSchema("DISCOVER_DIMENSION_STAT", AdomdSchemaGuid.DimensionStat, "DimensionStat"));
            sSchemaList.Add(AdomdSchemaGuid.Enumerators, new AdomdSchema("DISCOVER_ENUMERATORS", AdomdSchemaGuid.Enumerators, "Enumerators"));
            sSchemaList.Add(AdomdSchemaGuid.Functions, new AdomdSchema("MDSCHEMA_FUNCTIONS", AdomdSchemaGuid.Functions, "Functions"));
            sSchemaList.Add(AdomdSchemaGuid.Hierarchies, new AdomdSchema("MDSCHEMA_HIERARCHIES", AdomdSchemaGuid.Hierarchies, "Hierarchies"));
            sSchemaList.Add(AdomdSchemaGuid.InputDataSources, new AdomdSchema("MDSCHEMA_INPUT_DATASOURCES", AdomdSchemaGuid.InputDataSources, "InputDataSources"));
            sSchemaList.Add(AdomdSchemaGuid.Instances, new AdomdSchema("DISCOVER_INSTANCES", AdomdSchemaGuid.Instances, "Instances"));
            sSchemaList.Add(AdomdSchemaGuid.Jobs, new AdomdSchema("DISCOVER_JOBS", AdomdSchemaGuid.Jobs, "Jobs"));
            sSchemaList.Add(AdomdSchemaGuid.Keywords, new AdomdSchema("DISCOVER_KEYWORDS", AdomdSchemaGuid.Keywords, "Keywords"));
            sSchemaList.Add(AdomdSchemaGuid.Kpis, new AdomdSchema("MDSCHEMA_KPIS", AdomdSchemaGuid.Kpis, "Kpis"));
            sSchemaList.Add(AdomdSchemaGuid.Levels, new AdomdSchema("MDSCHEMA_LEVELS", AdomdSchemaGuid.Levels, "Levels"));
            sSchemaList.Add(AdomdSchemaGuid.Literals, new AdomdSchema("DISCOVER_LITERALS", AdomdSchemaGuid.Literals, "Literals"));
            sSchemaList.Add(AdomdSchemaGuid.Locations, new AdomdSchema("DISCOVER_LOCATIONS", AdomdSchemaGuid.Locations, "Locations"));
            sSchemaList.Add(AdomdSchemaGuid.Locks, new AdomdSchema("DISCOVER_LOCKS", AdomdSchemaGuid.Locks, "Locks"));
            sSchemaList.Add(AdomdSchemaGuid.MasterKey, new AdomdSchema("DISCOVER_MASTER_KEY", AdomdSchemaGuid.MasterKey, "MasterKey"));
            sSchemaList.Add(AdomdSchemaGuid.MeasureGroups, new AdomdSchema("MDSCHEMA_MEASUREGROUPS", AdomdSchemaGuid.MeasureGroups, "MeasureGroups"));
            sSchemaList.Add(AdomdSchemaGuid.MeasureGroupDimensions, new AdomdSchema("MDSCHEMA_MEASUREGROUP_DIMENSIONS", AdomdSchemaGuid.MeasureGroupDimensions, "MeasureGroupDimensions"));
            sSchemaList.Add(AdomdSchemaGuid.Measures, new AdomdSchema("MDSCHEMA_MEASURES", AdomdSchemaGuid.Measures, "Measures"));
            sSchemaList.Add(AdomdSchemaGuid.MemberProperties, new AdomdSchema("MDSCHEMA_PROPERTIES", AdomdSchemaGuid.MemberProperties, "MemberProperties"));
            sSchemaList.Add(AdomdSchemaGuid.Members, new AdomdSchema("MDSCHEMA_MEMBERS", AdomdSchemaGuid.Members, "Members"));
            sSchemaList.Add(AdomdSchemaGuid.MemoryGrant, new AdomdSchema("DISCOVER_MEMORYGRANT", AdomdSchemaGuid.MemoryGrant, "MemoryGrant"));
            sSchemaList.Add(AdomdSchemaGuid.MemoryUsage, new AdomdSchema("DISCOVER_MEMORYUSAGE", AdomdSchemaGuid.MemoryUsage, "MemoryUsage"));
            sSchemaList.Add(AdomdSchemaGuid.MiningColumns, new AdomdSchema("DMSCHEMA_MINING_COLUMNS", AdomdSchemaGuid.MiningColumns, "MiningColumns"));
            sSchemaList.Add(AdomdSchemaGuid.MiningFunctions, new AdomdSchema("DMSCHEMA_MINING_FUNCTIONS", AdomdSchemaGuid.MiningFunctions, "MiningFunctions"));
            sSchemaList.Add(AdomdSchemaGuid.MiningModelContent, new AdomdSchema("DMSCHEMA_MINING_MODEL_CONTENT", AdomdSchemaGuid.MiningModelContent, "MiningModelContent"));
            sSchemaList.Add(AdomdSchemaGuid.MiningModelContentPmml, new AdomdSchema("DMSCHEMA_MINING_MODEL_CONTENT_PMML", AdomdSchemaGuid.MiningModelContentPmml, "MiningModelContentPmml"));
            sSchemaList.Add(AdomdSchemaGuid.MiningModels, new AdomdSchema("DMSCHEMA_MINING_MODELS", AdomdSchemaGuid.MiningModels, "MiningModels"));
            sSchemaList.Add(AdomdSchemaGuid.MiningServiceParameters, new AdomdSchema("DMSCHEMA_MINING_SERVICE_PARAMETERS", AdomdSchemaGuid.MiningServiceParameters, "MiningServiceParameters"));
            sSchemaList.Add(AdomdSchemaGuid.MiningServices, new AdomdSchema("DMSCHEMA_MINING_SERVICES", AdomdSchemaGuid.MiningServices, "MiningServices"));
            sSchemaList.Add(AdomdSchemaGuid.MiningStructureColumns, new AdomdSchema("DMSCHEMA_MINING_STRUCTURE_COLUMNS", AdomdSchemaGuid.MiningStructureColumns, "MiningStructureColumns"));
            sSchemaList.Add(AdomdSchemaGuid.MiningStructures, new AdomdSchema("DMSCHEMA_MINING_STRUCTURES", AdomdSchemaGuid.MiningStructures, "MiningStructures"));
            sSchemaList.Add(AdomdSchemaGuid.PartitionDimensionStat, new AdomdSchema("DISCOVER_PARTITION_DIMENSION_STAT", AdomdSchemaGuid.PartitionDimensionStat, "PartitionDimensionStat"));
            sSchemaList.Add(AdomdSchemaGuid.PartitionStat, new AdomdSchema("DISCOVER_PARTITION_STAT", AdomdSchemaGuid.PartitionStat, "PartitionStat"));
            sSchemaList.Add(AdomdSchemaGuid.PerformanceCounters, new AdomdSchema("DISCOVER_PERFORMANCE_COUNTERS", AdomdSchemaGuid.PerformanceCounters, "PerformanceCounters"));
            sSchemaList.Add(AdomdSchemaGuid.ProviderTypes, new AdomdSchema("DBSCHEMA_PROVIDER_TYPES", AdomdSchemaGuid.ProviderTypes, "ProviderTypes"));
            sSchemaList.Add(AdomdSchemaGuid.SchemaRowsets, new AdomdSchema("DISCOVER_SCHEMA_ROWSETS", AdomdSchemaGuid.SchemaRowsets, "SchemaRowsets"));
            sSchemaList.Add(AdomdSchemaGuid.Sessions, new AdomdSchema("DISCOVER_SESSIONS", AdomdSchemaGuid.Sessions, "Sessions"));
            sSchemaList.Add(AdomdSchemaGuid.Sets, new AdomdSchema("MDSCHEMA_SETS", AdomdSchemaGuid.Sets, "Sets"));
            sSchemaList.Add(AdomdSchemaGuid.Tables, new AdomdSchema("DBSCHEMA_TABLES", AdomdSchemaGuid.Tables, "Tables"));
            sSchemaList.Add(AdomdSchemaGuid.TablesInfo, new AdomdSchema("DBSCHEMA_TABLES_INFO", AdomdSchemaGuid.TablesInfo, "TablesInfo"));
            sSchemaList.Add(AdomdSchemaGuid.TraceColumns, new AdomdSchema("DISCOVER_TRACE_COLUMNS", AdomdSchemaGuid.TraceColumns, "TraceColumns"));
            sSchemaList.Add(AdomdSchemaGuid.TraceDefinitionProviderInfo, new AdomdSchema("DISCOVER_TRACE_DEFINITION_PROVIDERINFO", AdomdSchemaGuid.TraceDefinitionProviderInfo, "TraceDefinitionProviderInfo"));
            sSchemaList.Add(AdomdSchemaGuid.TraceEventCategories, new AdomdSchema("DISCOVER_TRACE_EVENT_CATEGORIES", AdomdSchemaGuid.TraceEventCategories, "TraceEventCategories"));
            sSchemaList.Add(AdomdSchemaGuid.Traces, new AdomdSchema("DISCOVER_TRACES", AdomdSchemaGuid.Traces, "Traces"));
            sSchemaList.Add(AdomdSchemaGuid.Transactions, new AdomdSchema("DISCOVER_TRANSACTIONS", AdomdSchemaGuid.Transactions, "Transactions"));
            sSchemaList.Add(AdomdSchemaGuid.XmlaProperties, new AdomdSchema("DISCOVER_PROPERTIES", AdomdSchemaGuid.XmlaProperties, "XmlaProperties"));
            sSchemaList.Add(AdomdSchemaGuid.XmlMetadata, new AdomdSchema("DISCOVER_XML_METADATA", AdomdSchemaGuid.XmlMetadata, "XmlMetadata"));
        }

        public AdomdSchema(string pId, System.Guid pGuid, string pName)
        {
            this.mId = pId;
            this.mGuid = pGuid;
            this.mName = pName;
        }

        public override string ToString()
        {
            return this.mName;
        }

        public System.Guid Guid
        {
            get
            {
                return this.mGuid;
            }
        }

        public string Id
        {
            get
            {
                return this.mId;
            }
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
        }
    }