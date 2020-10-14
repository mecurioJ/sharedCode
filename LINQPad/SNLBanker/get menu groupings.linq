<Query Kind="Program">
  <Connection>
    <ID>57b8e721-62f2-4f6a-88ff-6c40878b38a8</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>SNLBanker_ApplicationDB</Database>
  </Connection>
</Query>

void Main()
{
	Menus.Select(mnu => new{
		ParentMenu = mnu.Parent.Name,
		mnu.Name,
		Cube = mnu.Reporting.Cube.CubeName,
		Url = String.Format("http://localhost/skweb94/{0}",mnu.Url)
	})
	.GroupBy(p => p.ParentMenu)
	.Dump();
}

// Define other methods and classes here
