<Query Kind="Statements">
  <Connection>
    <ID>a2472a43-66c6-4da0-8876-061e2e7455c0</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>swiftk_SNLv92</Database>
  </Connection>
</Query>

XElement.Parse(Reportings.Where(rpt => rpt.ReportID.Equals(Guid.Parse("f736b5dd-440f-49c9-b803-fef0713256c0"))).FirstOrDefault().ReportData)
.Descendants().Where(cbr => cbr.Name.LocalName == "cubereportbuilder")
.Dump();
