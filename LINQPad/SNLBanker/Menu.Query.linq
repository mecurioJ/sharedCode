<Query Kind="Statements">
  <Connection>
    <ID>a2472a43-66c6-4da0-8876-061e2e7455c0</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>swiftk_SNLv92</Database>
  </Connection>
</Query>

Menus.Select(m => new{
	Parent = m.Parent.Name,
	ReportName = m.Reporting.ReportName ?? String.Empty,
	foo = m.Reporting.OutputType,
	m.Reporting.CubeID,
	m.Reporting.Cube.CubeName,
	m.Reporting.Cube.CubeLabel,
	m.Name,
	Url = String.Format("http://localhost/skweb93/{0}",	m.Url)
}).Where(r => r.CubeID != null)
.GroupBy(f => f.foo)
.Dump();