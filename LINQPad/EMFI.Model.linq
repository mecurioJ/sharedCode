<Query Kind="Program">
  <Connection>
    <ID>0632ee6f-09a5-46b6-a92c-d018e80c48ad</ID>
    <Persist>true</Persist>
    <Server>den-sql01</Server>
    <Database>EMCO</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	LSA_TABLEs.Select(rl => new{
	TableSpace1 = string.Format("{0}_{1}",rl.DATASPACE_NAME,rl.TABLE_NAME),
	rl.DOCUMENTATION
	});
	
	//LSA_DOMAINs.Dump();
	
	var Relationships = 
	LSA_RELATIONSHIPs.Select(rl => new{
		TableSpace1 = string.Format("{0}_{1}",rl.DATASPACE_NAME,rl.TABLE_NAME),
		TableSpace2 = string.Format("{0}_{1}",rl.DATASPACE_NAME,rl.RELATIONSHIP_NAME),
		Columns = new[]{rl.COLUMN_1,rl.COLUMN_2,rl.COLUMN_3,rl.COLUMN_4,rl.COLUMN_5}}).ToList();
	
	LSA_COLUMNs.Select(col => new{
		TableName = String.Format("{0}_{1}",col.DATASPACE_NAME,col.TABLE_NAME),
		ColumnName = col.COLUMN_NAME,
		Ordinal = col.COLUMN_NO,
		PrimaryKey = (col.PRIMARY_KEY>0),
		DomainName = col.DOMAIN_NAME,
		EnumValues = col.ENUM_NAME,
		DataType = col.DATA_TYPE,
		Length = col.MAX_LENGTH,
		Precision = col.NUMERIC_PREC,
		Scale = col.NUMERIC_SCALE,
		Default = col.DEFAULT_VALUE,
		Documentation = col.DOCUMENTATION
	}).ToLookup(tt => tt.TableName, tt => new{
		tt.ColumnName,
		tt.Ordinal,
		tt.PrimaryKey,
		tt.DomainName,
		tt.EnumValues,
		tt.DataType,
		tt.Length,
		tt.Precision,
		tt.Scale,
		tt.Default,
		tt.Documentation,
		Parent = Relationships.Where(rel => rel.TableSpace1.Equals(tt.TableName)).Where(col => col.Columns.Contains(tt.ColumnName)).Select(tbl => tbl.TableSpace2),
		Child = Relationships.Where(rel => rel.TableSpace2.Equals(tt.TableName)).Where(col => col.Columns.Contains(tt.ColumnName)).Select(tbl => tbl.TableSpace1)
	}).Dump();
	
}

// Define other methods and classes here
