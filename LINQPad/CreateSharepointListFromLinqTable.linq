<Query Kind="Program">
  <Connection>
    <ID>7a4e7d40-0157-4052-965f-ef86408a5ee1</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>cfbstats</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <NuGetReference>morelinq</NuGetReference>
  <NuGetReference>ServiceStack.Text</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
	

	//--XElement 
	var serializedFinals = 
	ServiceStack.Text.XmlSerializer.SerializeToString(
Teams.Select(tm => 
	new TeamConference{
		TeamName = tm.Name,
		ConferenceName = Conferences
			.Where(cnf => cnf.ConferenceCode.Equals(tm.ConferenceCode))
			.Where(cnf => cnf.Season.Equals(tm.Season)).FirstOrDefault().Name
	}).Distinct().ToArray()
		);
	
	XElement DataRows = new XElement("Rows");
	
	XElement.Parse(serializedFinals).Elements().ForEach(t => {
		XElement rowItem = new XElement("Row");
			t.Elements().ForEach(a => {
				var fieldItem = new XElement("Field", new XAttribute("Name",a.Name.LocalName));
				fieldItem.Value = a.Value;
				rowItem.Add(fieldItem);
			});
		DataRows.Add(rowItem);
	});
	
	DataRows.ToString().Dump();
	
}

// Define other methods and classes here
public class TeamConference
{
	public String TeamName {get;set;}
	public String ConferenceName {get;set;}
	
}

public class PersonList
{
	public String FirstName {get;set;}
	public String MiddleName {get;set;}
	public String LastName {get;set;}
	public String FullNameFL {get;set;}
	public String FullNameLF {get;set;}
}