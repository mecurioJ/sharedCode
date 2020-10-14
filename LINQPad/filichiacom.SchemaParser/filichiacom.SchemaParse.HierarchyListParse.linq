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
			
			            //Build a lookup for the Table Joins
            var cubeSourceTablePkLookup = 
            OlapData.Cubes
                .Where(cub => cub.Name.Equals(cubeName))
                .SelectMany(cub => OlapData.DataSourceViews.Where(dsv => dsv.ID.Equals(cub.Source)).SelectMany(dsv => dsv.Schema.Tables.Cast<DataTable>().SelectMany(tbl => 
                tbl.PrimaryKey.Cast<DataColumn>().Select(dCol => new{
                    dCol.Table.TableName,
                    dCol.ColumnName
                })))).ToLookup(pk => pk.TableName, pk => pk.ColumnName);

//We need the following items			
            var HierachyList = new[]{
			//Customer Name
			"[Customers].[Customer Names].[Customer Name]",
			//Account Number
			"[Accounts].[Accounts]",
			//Product Family
			"[Products].[Products].[Product Family]",
			//Product Name
			"[Products].[Products].[Product ID]",
			//Current Balance
			"[Measures].[Current Balance]",
			//Officer Name			
			"[Customers].[Officer Portfolio].[Officer Name]",
			//Branch Name
			"[Organization].[Organization].[Branch]",
			}
			.Select(li => li.Replace("[",String.Empty).Replace("]",String.Empty).Split('.'))
			.Select(li => new{
				Dimension = li[0],
				Hierarchy = li[1],
				Level = (li.Count() > 2) ? li[2] : String.Empty
				});
				
			var SourceItems = 
			HierachyList.Select(li => new{
				li.Dimension,
				li.Hierarchy,
				li.Level,
				SourceDimension = OlapData.Dimensions.Where(oDim => oDim.Name.Equals(li.Dimension))
					.Select(oDim => new{
						Attributes = oDim.Attributes.Where(oAtt => oAtt.Name.Equals(li.Hierarchy)),
						Hierarchies = oDim.Hierarchies.Where(ohL => ohL.Name.Equals(li.Hierarchy))
						})
			}).SelectMany(fi => fi.SourceDimension.Select(sdI => new{
				fi.Dimension,
				fi.Hierarchy,
				fi.Level,
				sdI.Attributes,
				LevelAttributes = sdI.Hierarchies.SelectMany(hry => hry.Levels.SelectMany(lvl =>OlapData.Dimensions.Where(oDim => oDim.Name.Equals(fi.Dimension))
						.SelectMany(oDim => oDim.Attributes.Where(oAtt => oAtt.ID.Equals(lvl.SourceAttributeID)))))
				})).Select(fi => {
				return new{
					fi.Dimension,
					fi.Hierarchy,
					AttributeSource = (fi.Attributes.Any())
					? fi.Attributes
					: (fi.LevelAttributes.Where(lAtt => lAtt.Name.Equals(fi.Level)).Any())
						? fi.LevelAttributes.Where(lAtt => lAtt.Name.Equals(fi.Level))
						: fi.LevelAttributes};
					
					})
			.SelectMany(fin => fin.AttributeSource.Select(aSrc => new{
				AppHierarchyName = String.Format("[{0}].[{1}]",fin.Dimension,fin.Hierarchy),
				fin.Dimension,
				fin.Hierarchy,
					aSrc.Name,
					aSrc.ID,
					KeyColumns = aSrc.KeyColumns.SelectMany(col => col.Source.SelectMany(tSrc => 
						cubeSourceTablePkLookup[tSrc.TableName].Select(pk => new{
							WillPassFilter = tSrc.ColumnName.Equals(pk),
							tSrc.TableName,
							tSrc.ColumnName,
							KeyColumn = pk,
						}))),
					NameColumns = aSrc.NameColumn.SelectMany(col => col.Source.SelectMany(tSrc => 
						cubeSourceTablePkLookup[tSrc.TableName].Select(pk => new{
							tSrc.TableName,
							tSrc.ColumnName,
							KeyColumn = pk
						}))),
					aSrc.AttributeRelationships
				})).Dump();
			
			
			
			/*
			OlapData.Dimensions.Where(oDim => HierachyList.Select(li => li.Dimension).Contains(oDim.Name))
			.Select(oDim => new{
				oDim.Name,
				oDim.Attributes,
				oDim.Hierarchies
				})
			.Dump();
			*/
}
// Define other methods and classes here


        public string FixTableName(string tableId)
        {
            // TODO: What if the schema name isn't "dbo" but something else?
            return tableId.StartsWith("dbo_") ? tableId.Substring(4) : tableId;
        }