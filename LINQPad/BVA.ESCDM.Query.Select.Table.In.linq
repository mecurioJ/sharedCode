<Query Kind="Program">
  <Connection>
    <ID>2e945996-a0e3-45cf-bf5c-fcb323818428</ID>
    <Persist>true</Persist>
    <Server>sqlsass</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>mjfilichia</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAEvzdJpREcE2SHJAaAsA5LAAAAAACAAAAAAAQZgAAAAEAACAAAACVWTL+nI9l33p3eZu1olArD7eUZms78xew/kDsXHrGdgAAAAAOgAAAAAIAACAAAABgQH13K/Rql0kaSuvNkoHH1ty7LiXpvelClBhSBm6gRhAAAACZELVvLAOsB7PradG9Dip3QAAAABrdMaSbdVkTcC5GY2xobPZRW8m5NmvxBh1py3i/eyg3iWtl56feCembfvaLx3m+E1pMPw+ahR9OuYxoa90BXPs=</Password>
    <Database>ECSDM_Staging</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	var tableSelect = "SCAVP";
	var tablesRemaining = @"
	'SCAVP','SCRRP','SCROP','SCRTP','SDEVP','XXNY02P','XXNY01P','HEIDP','HEEVP','SFSNP','HGNIP',
	'SGRDP','SGCHP','SHMTP','SHRHP','SIEPP','HJB1P','HJB2P','SLKRP','XNYTCHNP','XNYTCHP',
	'HPRAP','HPC1P','OLDCIMS','SRAVP','SSCRP','SSCXP','BEHAVE','SSGPP'";


	var queryData = String.Format(@"select DISTINCT t1.TABLE_NAME as [Table] , t1.REMARKS as TableDescription
    , t2.COLUMN_NAME as [Column], REPLACE(t3.HLPDESC,' ','') as ColumnDetail, t2.COLUMN_NAME +' AS ['+ 
	   REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
	   REPLACE(LTRIM(RTRIM(ISNULL(t3.HLPDESC,t2.Remarks))),' ','')
	   ,'&','And'),'#',''),'''',''),'(',''),')',''),'/',''),',','_'),'-','_')+']' as Alias
    , t2.REMARKS as Description, t3.HLPDESC as HelpDescription
  from ECSDMTools.CPDATA.[Table] t1 INNER JOIN ECSDMTools.CPDATA.Columns t2 ON t1.TABLE_NAME = t2.TABLE_NAME LEFT JOIN CPDATA.AHLPP t3 on  t2.COLUMN_NAME = RTRIM(LTRIM(t3.HLPPCOPY))
WHERE  t1.TABLE_NAME in (select tt.TABLE_NAME from INFORMATION_SCHEMA.TABLES tt) and 
--t1.TABLE_NAME ='{0}'
t1.TABLE_NAME IN ({1})
",
tableSelect,
tablesRemaining
); 

var DetailTable = 
ExecuteQueryDynamic(queryData)
.Where(q => !q.Alias.Contains("Filler"))
.Select(
	q => new DetailRow(){
		Table = q.Table,
		Alias = q.Alias,
		Column = q.Column,
		ColumnDetail = q.ColumnDetail,
		Description = q.Description,
		HelpDescription = q.HelpDescription,
		TableDescription = q.TableDescription
	}).ToList();
	
	DetailTable
	.GroupBy(k => Tuple.Create(k.Table,k.TableDescription), k => k.Alias)
	.Dump();
	
	DetailTable
	.OrderBy(ord => ord.Alias)
	.GroupBy(k => Tuple.Create(k.Table,k.TableDescription), k => k.Alias)
	.Select(tt => 
		String.Format("CREATE VIEW DataMerge.Base{1} AS SELECT {2} from CPDATA.{0}",
			tt.Key.Item1,
			tt.Key.Item2
				.Replace(" ",String.Empty)
				.Replace("(LANSA)",String.Empty),
			tt.ToArray().Aggregate((p,n)=> String.Format("{0},{1}",p,n))
			)
		)
	
	.Dump();

}

// Define other methods and classes here

public class DetailRow
{
	public String Table {get;set;}
	public String TableDescription {get;set;}
	public String ColumnDetail {get;set;}
	public String Column {get;set;}
	public String Alias {get;set;}
	public String Description {get;set;}
	public String HelpDescription {get;set;}
}