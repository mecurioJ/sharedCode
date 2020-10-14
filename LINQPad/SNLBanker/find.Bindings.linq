<Query Kind="Program" />

void Main()
{
	var joins = new JoinTable{
		MeasureGroup = new SourceCacheColumnBinding[]{
			new SourceCacheColumnBinding {DimensionId ="FACT DDA", AttributeId ="[Measure]", TableId ="FACT_DDA", ColumnId ="dimAccountID", Type=SourceColumnType.Key	},
new SourceCacheColumnBinding {DimensionId ="FACT DDA", AttributeId ="[Measure]", TableId ="FACT_DDA", ColumnId ="ProductID", Type=SourceColumnType.Key}
		},
		Dimension = new SourceCacheColumnBinding[]{	
		new SourceCacheColumnBinding {DimensionId ="Dim Accounts", AttributeId ="Dim Accounts", TableId ="dim_Accounts", ColumnId ="dimAccountID", Type=SourceColumnType.Key	},
		new SourceCacheColumnBinding {DimensionId ="Dim Products", AttributeId ="Product Category", TableId ="dim_Products", ColumnId ="ProductID", Type=SourceColumnType.Key	},
		new SourceCacheColumnBinding {DimensionId ="Dim Accounts", AttributeId ="Dim Accounts", TableId ="dim_Accounts", ColumnId ="dimAccountID", Type=SourceColumnType.Key	},
		new SourceCacheColumnBinding {DimensionId =null, AttributeId =null, TableId ="FACT_DDA", ColumnId ="dimAccountID", Type=SourceColumnType.Key		},
		new SourceCacheColumnBinding {DimensionId =null, AttributeId =null, TableId ="FACT_DDA", ColumnId ="ProductID", Type=SourceColumnType.Key	},
		new SourceCacheColumnBinding {DimensionId =null, AttributeId =null, TableId ="dim_AccountCustomers", ColumnId ="dimAccountID", Type=SourceColumnType.Key}
			
		}
	};
	
	
            var LeftTables = joins.MeasureGroup;
            var RightTables = joins.Dimension;
			
			 var joinBase = LeftTables.Join(
                RightTables.Where(rt => !String.IsNullOrEmpty(rt.DimensionId)).Distinct(),
                left => left.ColumnId,
                right => right.ColumnId,
                (left, right) => new 
                    {
                        LeftTable = left.TableId,
                        LeftColumns = Enumerable.Repeat(left.ColumnId,1),
                        RightTable = right.TableId,
                        RightColumns = Enumerable.Repeat(right.ColumnId,1),
                        Type = "Left"
                    }
                ).Where(tbl => !tbl.LeftTable.Equals(tbl.RightTable));
	
	joins.Dump();
	joinBase.Dump();
	
}

// Define other methods and classes here

public class JoinTable 
{
	public IEnumerable<SourceCacheColumnBinding> MeasureGroup {get;set;}
	public IEnumerable<SourceCacheColumnBinding> Dimension {get;set;}
}

public class SourceCacheColumnBinding
{
	public String DimensionId {get;set;}
    public String AttributeId {get;set;}
    public String TableId {get;set;}
    public String ColumnId {get;set;}
    public SourceColumnType Type {get;set;}
    
}

public enum SourceColumnType
    {
        Key = 0,
        Name = 1,
        Value = 2
    }