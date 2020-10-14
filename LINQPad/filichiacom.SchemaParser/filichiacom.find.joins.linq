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
	var schemaDir = @"C:\Users\joeyf\SkyDrive\SSAS";
	SchemaParser.oDataBase OlapData = new SchemaParser.oDataBase(XElement.Load(schemaDir+@"\BankerSchema.xml"));
	
	DateTime TimeStamp = DateTime.Now.Dump();
	
            int databaseId = 11; 
            String cubeName = "DDA";
			
			IEnumerable<SchemaParser.oMeasureGroupMeasure> CountMeasures =  
			OlapData
			//olapData
			.Cubes.Where(cNm => cNm.Name.Equals(cubeName))
            .SelectMany(c => c.MeasureGroups.SelectMany(msr => msr.Measures))
            .Where(msr => msr.AggregateFunction.Contains("Count"));
			
            IEnumerable<String> filterList = new[]{""};
            
			IEnumerable<String> columnList = new[]{
"[Organization].[Organization].[All Organizations]"	,
"[Measures].[Product Count End of Period]"	,
"[Measures].[Product Count]"	,
"[Measures].[Current Balance]"	,
"[Products].[Products].[Product Name]"	,
"[Customers].[Customer Names].[Customer Name]"	,
"[Organization].[Organization Region].[Branch]"	,
"[Accounts].[Accounts]"	,
"[Measures].[Note Rate x Balance]"	,
"[Risk Rating].[Risk Rating].[All Risk Rating]"	,
					};
					
            IEnumerable<String> rowList = new[]{
"[Risk Rating].[Risk Rating].[All Risk Rating]"	,
"[Participation].[Participation Codes].[All Participation Codes]"	,
"[Customers].[Officer Portfolio].[Officer Name]"	,
"[Products].[Products].[Product Family]"	,
			};

            #region Build Source objects

            //get expressions from tables
            var dataTableExpressions = SchemaParser.GetDataSourceViewExpressions(OlapData);
            var dataTableDefinitions = SchemaParser.GetDataSourceViewDefinitions(OlapData);




            var ExpressionSet =
                dataTableExpressions.SelectMany(
                    dsEx => dsEx.ExpressionTree.Where(tree => !String.IsNullOrEmpty(tree.Expression)))
                                    .Select(tree => new {tree.TableName, tree.ColumnName, tree.Expression});

            //Get the DataSourceViewId from the current Cube
            String DataSourceViewId = OlapData.Cubes.First(cub => cub.Name.Equals(cubeName)).Source;

            //Build a lookup for the Table Joins
            ILookup<string, string> cubeSourceTablePkLookup = PrimaryKeyLookup(cubeName);

            //Build a set of Data Relation Objects
            IEnumerable<string> sourceSet =
                filterList.Union(columnList.Union(rowList)).Where(ss => !String.IsNullOrEmpty(ss)).ToArray();

            //Build a Temp Object to hold the contents of the members
            IEnumerable<SchemaParser.TableColumn> measureSet = SchemaParser.BuildMeasureSet(OlapData, sourceSet,
                                                                                            cubeName).ToArray();
			
			
			
			
measureSet.Dump();
			var olapData = OlapData;


            //Build a definition of the members that are not measures...
            var memberItemDefinitions = SchemaParser.GetMemberItemDefinitions(
                OlapData,
                SchemaParser.GetMeasureDefinitions(OlapData).Where(cNm => cNm.CubeName.Equals(cubeName)),
                SchemaParser.BuildMemberSet(sourceSet),
                SchemaParser.GetHierarchySet(OlapData, cubeName),
                SchemaParser.GetRelationSet(OlapData, DataSourceViewId)).ToArray();

            //Get the names of the tables
            var memberTables = memberItemDefinitions
                .Select(mDef => SchemaParser.GetDbTableName(OlapData, FixTableName(mDef.TableName)))
                .Distinct()
                .ToArray();
			
			//memberItemDefinitions.Dump();


            #endregion
            
            #region Build Context Joins

            //built an array to hold the joins...
            var joins = memberItemDefinitions
                //Removed the filtering function to ensure we get all joins. Since the expression parser
                //will handle expressions instead of columns, this will be handled with its implementation
                //.Where(r => r.DataType.Equals("Integer") || r.DataType.Equals("BigInt"))
                .ToArray()
                .SelectMany(r => r.Relations
                                  .Select(rel =>
                                          new
                                              {
                                                  Right =
                                              rel.ChildColumns.Select(cc => new {cc.TableName, cc.ColumnName}),
                                                  Left =
                                              rel.ParentColumns.Select(cc => new {cc.TableName, cc.ColumnName})
                                              })
                )
                .Select(tb =>
                        new
                            {
                                LeftTable =
                            SchemaParser.GetDbTableName(OlapData, FixTableName(tb.Left.First().TableName)),
                                LeftColumn = tb.Left.First().ColumnName,
                                RightTable =
                            SchemaParser.GetDbTableName(OlapData, FixTableName(tb.Right.First().TableName)),
                                RightColumn = tb.Right.First().ColumnName,
                            })
                .Distinct()
                .ToArray();


            //Get all tables with a direct join to the measures
            var measureJoins =
                joins.Where(MeasureJoin => measureSet.Select(ms => ms.TableName).Contains(MeasureJoin.RightTable))
                     .ToArray();
            //Get all the tables that joins were not found for.
            var missingTables = memberTables.Except(measureJoins.Select(mj => mj.LeftTable)).ToArray();
            //Get the tables for linking the missing tables to known joins
            var linkingTables =
                joins.Where(js => missingTables.Contains(js.LeftTable)).Select(js => js.RightTable).ToArray();

            //get the joins for the tables that are missing and attach them to the objects...
            var joinSet = joins
                .Where(js => linkingTables.Contains(js.RightTable))
                .Union(measureJoins)
                .Select(js => new //TableJoin
                    {

                        LeftTable = FixTableName(js.LeftTable),
                        LeftColumns = new[] {js.LeftColumn},
                        RightTable = FixTableName(js.RightTable),
                        RightColumns = new[] {js.RightColumn}
                    }).ToArray();

	//joinSet.Dump();
            #endregion
            
            #region Build Context tables

            //Get a simplified list of member definitions, defined similar to the 
            //object they will populate
            var memberDefinitions = memberItemDefinitions.Select(tbl => new
            {
				tbl.Dimension,
				tbl.Hierarchy,
				tbl.Level,
                Caption = CountMeasures.Any(msr => msr.Name.Contains(tbl.Hierarchy))
								? tbl.Hierarchy
								: tbl.ColumnName,
                DisplayColumn = CountMeasures.Any(msr => msr.Name.Contains(tbl.Hierarchy))
								? tbl.Hierarchy
								: tbl.ColumnName,
                TableName = SchemaParser.GetDbTableName(OlapData, FixTableName(tbl.TableName)),
                Value =  CountMeasures.Any(msr => msr.Name.Contains(tbl.Hierarchy))
                            ? "1"
                            : tbl.Value,
                CanFilter = !String.IsNullOrEmpty(tbl.Value),
                IsKeyColumn = tbl.IsPrimaryKey,
                Expression = CountMeasures.Any(msr => msr.Name.Contains(tbl.Hierarchy))
                            ? "1"
                            : tbl.ExpressionStatement
            })
            //.DistinctBy(tbl => new { tbl.TableName, tbl.DisplayColumn })
            .ToArray();
			
			memberDefinitions.Dump();

            //Get an array of Key Columns
            var keyColumnArray = memberItemDefinitions
                .Where(
                    miDef => miDef.IsPrimaryKey && miDef.DataType.Equals("Integer") || miDef.DataType.Equals("BigInt"))
                .DistinctBy(dc => String.Format("{0}.{1}", dc.TableName, dc.ColumnName))
                .Select(tt => new
                    {
                        TableName = SchemaParser.GetDbTableName(OlapData, FixTableName(tt.TableName)),
                        tt.ColumnName
                    });

            //Create a Dictionary of table definitions of the members
            var tableLookup = memberDefinitions
                .Select(ts => new
                    {
                        TableName = FixTableName(ts.TableName),
                        KeyColumn = keyColumnArray
                                        .Where(tn => tn.TableName.Equals(ts.TableName))
                                        .Select(dcol => dcol.ColumnName)
                                        .FirstOrDefault() ?? String.Empty,
                        Caption = ts.Caption,

                        ts.DisplayColumn,
                        Value = (ts.IsKeyColumn && ts.CanFilter) ? ts.Value : null,
                        Expression = ts.Expression

                    }).Distinct()
                .ToLookup(tbl => tbl.TableName);

            //create a final defintion of table objects to use in the context
            var relationalTables = tableLookup.Select(tbl =>
                                                      new //Table
                                                          {
                                                              Name = tbl.Key,
                                                              Columns = tbl.Select(cc => new //TableColumn()
                                                                  {
                                                                      Caption = cc.Caption,
                                                                      DisplayColumn = cc.DisplayColumn,
                                                                      //if the TableLookup Key Column is missing, populate it with values from the PK Lookup Table
                                                                      KeyColumn = String.IsNullOrEmpty(cc.KeyColumn)
                                                                                      ? cubeSourceTablePkLookup[
                                                                                          "dbo_" + cc.TableName]
                                                                                            .FirstOrDefault()
                                                                                      : cc.KeyColumn,
                                                                      TableName = cc.TableName,
                                                                      Value = cc.Value,
                                                                      Expression = cc.Expression

                                                                  })
                                                          }).ToList();

            #endregion

            #region Build Final Context 

            var relationalSet = new[]
                {
                    new //RelationalBuilderContext()
                        {
                            DatabaseId = databaseId,
                            Tables = relationalTables,
                            Joins = joinSet.ToArray()
                        },

                };

            #endregion
            
			relationalSet.Dump();
			
            //return relationalSet;
			
			
}
// Define other methods and classes here
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
		

	public sealed class TopologicalSort<T> where T : IEquatable<T>
	{
	
		private Dictionary<T, NodeInfo> Nodes = new Dictionary<T, NodeInfo>();

		
		public bool Edge(T nodeKey)
		{
			if (nodeKey == null)
				return false;

			if (!Nodes.ContainsKey(nodeKey))
				Nodes.Add(nodeKey, new NodeInfo());

			return true;
		}
		
		public bool Edge(T successor, T predecessor)
		{
			// make sure both nodes are there
			if (!Edge(successor)) return false;
			if (!Edge(predecessor)) return false;

			// if successor == predecessor (cycle) fail
			if (successor.Equals(predecessor)) return false;

			var successorsOfPredecessor = Nodes[predecessor].Successors;

			// if the Edge is already there, keep silent
			if (!successorsOfPredecessor.Contains(successor))
			{
				// add the sucessor to the predecessor's successors
				successorsOfPredecessor.Add(successor);

				// increment predecessorrCount of successor
				Nodes[successor].PredecessorCount++;
			}
			return true;

		}
		//-------------------------------------------------------------------------
		public bool Sort(out Queue<T> sortedQueue)
		{
			sortedQueue = new Queue<T>(); // create, even if it stays empty

			var outputQueue = new Queue<T>(); // with predecessorCount == 0


			foreach( KeyValuePair<T, NodeInfo> kvp in Nodes ) 
				if( kvp.Value.PredecessorCount == 0 )
						outputQueue.Enqueue(kvp.Key) ;

			T nodeKey;
			NodeInfo nodeInfo;

			while (outputQueue.Count != 0)
			{
				nodeKey = outputQueue.Dequeue();

				sortedQueue.Enqueue(nodeKey); // add it to sortedQueue

				nodeInfo = Nodes[nodeKey]; // get successors of nodeKey

				Nodes.Remove(nodeKey);	// remove it from Nodes

				foreach (T successor in nodeInfo.Successors)
					if (--Nodes[successor].PredecessorCount == 0)
						outputQueue.Enqueue(successor);

				nodeInfo.Clear();

			}

			// outputQueue is empty here
			if (Nodes.Count == 0)
				return true;	// if there are no nodes left in Nodes, return true

			// there is at least one cycle
			CycleInfo(sortedQueue); // get one cycle in sortedQueue
			return false; // and fail

		}

		public void Clear()
		{
			foreach (NodeInfo nodeInfo in Nodes.Values)
				nodeInfo.Clear();

			Nodes.Clear();
		}

		
		public void CycleInfo(Queue<T> cycleQueue)
		{
			cycleQueue.Clear(); 

			foreach (NodeInfo nodeInfo in Nodes.Values)
				nodeInfo.ContainsCycleKey = nodeInfo.CycleWasOutput = false;

			T cycleKey = default(T);
			bool cycleKeyFound = false;

			NodeInfo successorInfo;

			foreach (KeyValuePair<T, NodeInfo> kvp in Nodes)
			{
				foreach (T successor in kvp.Value.Successors)
				{
					successorInfo = Nodes[successor];

					if (!successorInfo.ContainsCycleKey)
					{
						successorInfo.CycleKey = kvp.Key;
						successorInfo.ContainsCycleKey = true;

						if (!cycleKeyFound)
						{
							cycleKey = kvp.Key;
							cycleKeyFound = true;
						}
					}
				}
				kvp.Value.Clear();
			}

			if( !cycleKeyFound )
				throw new Exception("program error: !cycleKeyFound");

			NodeInfo cycleNodeInfo;
			while (!(cycleNodeInfo = Nodes[cycleKey]).CycleWasOutput)
			{
				if (!cycleNodeInfo.ContainsCycleKey)
					throw new Exception("program error: nodeInfo.ContainsCycleKey");

				cycleQueue.Enqueue(cycleKey);
				cycleNodeInfo.CycleWasOutput = true;
				cycleKey = cycleNodeInfo.CycleKey;

			}


		}
		
		private class NodeInfo
		{
			// for construction
			public int PredecessorCount;
			public List<T> Successors = new List<T>();

			// for Cycles in case the sort fails
			public T CycleKey;
			public bool ContainsCycleKey;
			public bool CycleWasOutput;

			// Clear NodeInfo
			public void Clear()
			{
				Successors.Clear();
			}

		}

	}