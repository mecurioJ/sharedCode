<Query Kind="Program">
  <Connection>
    <ID>90cadb01-e28a-41eb-a3c5-d3c0dab3b7c8</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>MSD_Sandbox</Database>
  </Connection>
</Query>

void Main()
{
//	DomainView.Select(dv => new{
//		dv.DomainName,dv.RecordName,dv.RowPosition,dv.SequenceAppearance,dv.LevelCol,dv.CopyName,
//		dv.Picture,dv.FullDescription,dv.RequiredField,dv.ExternalItem,dv.TextField,
//		//dv.Panel,dv.Location,
//		dv.PartIdentifier
//	})
//	.Where(dd => dd.CopyName.Equals(dd.DomainName)).Distinct()
//	.OrderBy(o => o.DomainName);
	
	
	DomainView.Select(dv => new{
		dv.DomainName,dv.RecordName,dv.RowPosition,dv.SequenceAppearance,dv.LevelCol,dv.CopyName,
		dv.Picture,dv.FullDescription,dv.RequiredField,dv.ExternalItem,dv.TextField,
		dv.Panel,dv.Location,
		dv.PartIdentifier
	})
	.OrderBy(odr => odr.LevelCol)
	.GroupBy(gg => gg.Panel)
	
	.Dump();
}

// Define other methods and classes here
//C131 -- Warehouse
//F635 -- Finance
