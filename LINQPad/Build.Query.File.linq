<Query Kind="Program">
  <Connection>
    <ID>0799912f-9f83-4973-96a1-f8730f5f7db7</ID>
    <Persist>true</Persist>
    <Server>sqlsass</Server>
    <Database>ECSDMTools</Database>
  </Connection>
  <NuGetReference>MoreLinq.Portable</NuGetReference>
  <NuGetReference>StringExtensionsLibrary</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>StringExtensionLibrary</Namespace>
</Query>

void Main()
{
	//Table.Where(tb => tb.REMARKS.Contains("Basic Student")).Dump();
	
	String TableFilter = "PGMDBASE";
	
	var TableDescriptions = Table.Select(t => new TableDescription{Schema = t.TABLE_SCHEM,TableName = t.TABLE_NAME, Description = t.REMARKS})
	.ToList();
	
	var ViewBuilder = 
	CPDATA_Columns
	.Where(cl => cl.TABLE_NAME.Equals(TableFilter))
	.Select(c => new QueryBuilder{
		Schema = c.TABLE_SCHEM,
		TableName = c.TABLE_NAME,
		ColumnName = c.COLUMN_NAME,
		ColumnCaption = c.REMARKS
	})
	.ToList();
	
	
	
	ViewBuilder
	.Join(TableDescriptions,
	v => v.SchemaTable,
	t => t.SchemaTable,
	(v,t) => new{
		v.SchemaTable,
		t.Description,
		ColumnAlias = v.ColumnAlias.RemoveChars(new[]{'"'})
	})
	.GroupBy(g => new{g.SchemaTable,g.Description}, g=> g.ColumnAlias)
	.Select(
		sql => String.Format("--{2}{3}SELECT {0} FROM {1} ",
		sql.Aggregate((p,n) => String.Format("{0}{2}, {1}",p,n,Environment.NewLine)),
		sql.Key.SchemaTable,
		sql.Key.Description,
		Environment.NewLine
		))
	.Dump();
	
}
public class TableDescription
{
	public String Schema {get;set;}
	public String TableName {get;set;}
	public String Description {get;set;}
	
	public String SchemaTable
	{
		get{
			return String.Format("{0}.{1}",Schema,TableName);
		}
	}
}

public class QueryBuilder 
{
	public String Schema {get;set;}
	public String TableName {get;set;}
	public String ColumnName {get;set;}
	public String ColumnCaption {get;set;}
	
	public String SchemaTable
	{
		get{
			return String.Format("{0}.{1}",Schema,TableName);
		}
	}
	
	public String ColumnAlias
	{
		get{
			return String.Format("[{0}] as [{1}]",ColumnName,CleanCaption(ColumnCaption));
		}
	}
	
	private String CleanCaption(String caption)
	{
		return caption
					.Trim()
					.Replace(" ","_")
					.Replace("-","_")
					.Replace("/","_")
					.Replace("1st","First")
					;
	}
}
// Define other methods and classes here
