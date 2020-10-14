<Query Kind="Program">
  <Reference>C:\Projects\SchemaParser\SchemaParser\bin\Debug\SchemaParser.dll</Reference>
  <Namespace>SchemaParser</Namespace>
</Query>

void Main()
{
	String currentCubeName	= "DDA";
		String leftJoinTable = "dbo_FACT_DDA";
		
		List<String> MemberListUniqueNames= new List<String> {"[Calendar Period].[Calendar Period].[Date].&[142]","[Products].[Products].[Product Type].&[CDs]","[Products].[Products].[Product Type].&[Deposits]","[Customers].[Customer Names].[Customer Name].&[15823]","[Accounts].[Accounts].[All Accounts]"};
		
		SchemaParser.SchemaParser.oDataBase olapData = new SchemaParser.SchemaParser.oDataBase(XElement.Load(@"c:\Projects\SSAS\BankerSchema.xml"));
        IEnumerable<SchemaParser.SchemaParser.DataSourceViewDefinition> dataSourceViewDef = SchemaParser.SchemaParser.GetDataSourceViewDefinitions(olapData);
		
		
		dataSourceViewDef.SelectMany(dDef => dDef.Relations.Where( dRel => dRel.Parent.TableName.Equals(leftJoinTable) || dRel.Child.TableName.Equals(leftJoinTable))).Dump();
		
		
		
		
		//pointer to the measures table source
		var MeasureTable = leftJoinTable;
		
		//find all the objects that are dimensions:
		olapData.Cubes.SelectMany(cDim => cDim.Dimensions.SelectMany(dimz => 
			olapData.Dimensions.Where(oDim => oDim.ID.Contains(dimz.DimensionID))
			.SelectMany( dimO => dimO.Attributes.SelectMany(att => 
						att.KeyColumns.SelectMany(akc => akc.Source.Select(tc => new{ 
							CubeName = cDim.Name,
							CubeId = cDim.ID,
							DimensionName = dimO.Name,
							DimensionId = dimO.ID,
							SourceName = dimz.Name,
							SourceId = dimz.ID,
							HierarchyUniqueNameStyle = dimz.HierarchyUniqueNameStyle,
							AttributeName = att.Name,
							AttributeId = att.ID,
							TableName = tc.TableName,
							ColumnName = tc.ColumnName,
							AttributeDataType = akc.DataType,
							Hierarchies = dimO.Hierarchies.ToList(),
							akc.Format})))
					))).Dump();
		
		//Get the current cuble
        var CurrentCube = olapData.Cubes.Where(cub => cub.Name.Contains(currentCubeName)).First();
		
		//Create a matrix of information about the items
       var JoinSources =
               MemberListUniqueNames.Select(li => new MemberListUniqueName
               {
                   Dimension = li.Split('.')[0].Replace("[", String.Empty).Replace("]", String.Empty),
                   Hierarchy = li.Split('.')[1].Replace("[", String.Empty).Replace("]", String.Empty),
                   Attribute = li.Split('.')[2].Replace("[", String.Empty).Replace("]", String.Empty),
               }).Distinct();
	
		//Narrow down the list of dimension . hierarchy. attribute
		var DimensionHash = new HashSet<String>(JoinSources.Select(js => js.Dimension));
		var AttributeHash = new HashSet<String>(JoinSources.Select(js => js.Attribute));
		var HierarchyHash = new HashSet<String>(JoinSources.Select(js => js.Hierarchy));
		
		//narrow down the dimensions
		var dimensionFilter = CurrentCube.Dimensions.Where(Dim => DimensionHash.Contains(Dim.Name)).Select(cDim => olapData.Dimensions.Where(dm => dm.ID.Equals(cDim.DimensionID)));
		
		//generate the source for the joins
		var SourceData = 
		dimensionFilter
			.SelectMany(df => df
					.SelectMany(dAtt => dAtt.Attributes
						.Where(att => AttributeHash.Contains(att.Name) || HierarchyHash.Contains(att.Name))
						.SelectMany(att => att.KeyColumns.SelectMany(kc => 
						kc.Source.Select(src => new{
						Selector = att.Name,
						DimensionID = dAtt.ID,
						DimensionName = dAtt.Name,
						AttributeID = att.ID,
						AttributeName = att.Name,
						src.TableName,
						src.ColumnName,
						Relations = dataSourceViewDef.SelectMany(Rel => Rel.Relations.Where(r => 
												r.Parent.TableName.Contains(src.TableName)
												|| r.Child.TableName.Contains(src.TableName)
												))
						})
						))));
		
		//Get a disinct list of the tables in the source, then add in the 
		//  table for the left hand side of the join.
		List<String> Tables = SourceData.Select(sd => sd.TableName).ToList();
		Tables.Add(leftJoinTable);
		
		//build a matrix of results
		var RelationsMatrix = 
		SourceData.SelectMany(sd => sd.Relations.Select(rel => new{
			Left = new{
				DimensionId = sd.DimensionID,
				AttributeId = sd.AttributeID,
				TableId = rel.Parent.TableName,
				ColumnId = rel.Parent.ColumnName,
				},
			Right = new{
				DimensionId = sd.DimensionID,
				AttributeId = sd.AttributeID,
				TableId = rel.Child.TableName,
				ColumnId = rel.Child.ColumnName,
				}
		})).ToLookup(k => new []{k.Left.TableId,k.Right.TableId});

	
	//get a matrix of known and orphaned relationships.
	var JoinSetKnown = 
	Tables.Select(tbl => new{
		tbl,
		IsOrphan = !RelationsMatrix.Where(mtx => mtx.Key[0].Contains(MeasureTable) || mtx.Key[1].Contains(MeasureTable)).Where(mtx => mtx.Key[0].Equals(tbl) || mtx.Key[1].Equals(tbl)).Any(),
		RelationToMeasure = RelationsMatrix.Where(mtx => mtx.Key[0].Contains(MeasureTable) || mtx.Key[1].Contains(MeasureTable)).Where(mtx => mtx.Key[0].Equals(tbl) || mtx.Key[1].Equals(tbl))
								.SelectMany(r => r.Select(rel => new{rel.Left,rel.Right}))
	});
	
	//get all tables that are not orphans
	var knownTables = JoinSetKnown.Where(js => !js.IsOrphan).Select(t => t.tbl);
	
	//get all tables that are orphans
	var orphanTables = JoinSetKnown.Where(js => js.IsOrphan).Select(t => t.tbl);
	
	//Find a path for the orphan tables to join
	var OrphanSet = 
	JoinSetKnown.Where(js => !js.tbl.Equals(MeasureTable)).Where(js => js.IsOrphan).SelectMany(
	oh => RelationsMatrix
			.Where(mtx => mtx.Key[0].Contains(oh.tbl) || mtx.Key[1].Contains(oh.tbl))
			.SelectMany(rm => RelationsMatrix
				.Where(mtx => mtx.Key[0].Contains(rm.Key[0])|| mtx.Key[0].Contains(rm.Key[1]) || mtx.Key[1].Contains(rm.Key[0]) || mtx.Key[1].Contains(rm.Key[1]))
				)
			.Where(rx => knownTables.Contains(rx.Key[0]) || knownTables.Contains(rx.Key[1]))
			.First()
			
	).ToList();
	
	//add the Orphan Table back in
	OrphanSet.Add(RelationsMatrix.Where(mtx => mtx.Key[0].Contains(orphanTables.First()) || mtx.Key[1].Contains(orphanTables.First()))
					.SelectMany(r => r.Select(rel => new{rel.Left,rel.Right}))
					.First());
								
	
	//join them all together.
	JoinSetKnown.SelectMany(k => k.RelationToMeasure).Union(OrphanSet);
	
	

	
	
			
				
}

// Define other methods and classes here
public class MemberListUniqueName
{
	public String Dimension{get;set;}
	public String Hierarchy{get;set;}
	public String Attribute{get;set;}
}
        public class RelationPath
        {
            public String DataSourceViewId { get; set; }
            public String ParentTable { get; set; }
            public String NavigateFrom { get; set; }
            public String NavigateTo { get; set; }
            public String ChildTable { get; set; }
        }