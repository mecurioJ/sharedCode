<Query Kind="Program">
  <Connection>
    <ID>2e945996-a0e3-45cf-bf5c-fcb323818428</ID>
    <Persist>true</Persist>
    <Server>sqlsass</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>mjfilichia</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAEvzdJpREcE2SHJAaAsA5LAAAAAACAAAAAAAQZgAAAAEAACAAAACVWTL+nI9l33p3eZu1olArD7eUZms78xew/kDsXHrGdgAAAAAOgAAAAAIAACAAAABgQH13K/Rql0kaSuvNkoHH1ty7LiXpvelClBhSBm6gRhAAAACZELVvLAOsB7PradG9Dip3QAAAABrdMaSbdVkTcC5GY2xobPZRW8m5NmvxBh1py3i/eyg3iWtl56feCembfvaLx3m+E1pMPw+ahR9OuYxoa90BXPs=</Password>
    <Database>ECSDMTools</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
		Tables.Join(Columns,
		tt => tt.TABLE_NAME,
		cc => cc.TABLE_NAME,
		(tt,cc) => new{
			TableName = tt.TABLE_NAME,
			TableMetaData = tt.REMARKS,
			ColumnName = cc.COLUMN_NAME,
			ColumnMeta = cc.REMARKS,
			cc.ORDINAL_POSITION
		})
		.Where(tt => tt.TableName.Contains("SCRRP"))
		//.Where(tt => tt.ColumnName.Contains("stu"))
		//INGRLV
		.ToArray()
		//.Where(tn => tn.ColumnMeta.Contains("house"))
		//.Where(tn => tn.TableMetaData.Contains("house"))
		.Dump();
}

// Define other methods and classes here