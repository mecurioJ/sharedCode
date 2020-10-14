<Query Kind="Program">
  <Connection>
    <ID>521b3469-fa41-4506-a815-9397b9f8cbd4</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>BrightViewDemo</Database>
    <NoPluralization>true</NoPluralization>
    <ExcludeRoutines>true</ExcludeRoutines>
  </Connection>
</Query>

void Main()
{

	Dictionary<String,String> Schools = InstructionalHierarchy.Where(ih => ih.DistrictName.Contains("Middletown")).Select(sch => new{sch.SchoolCode,sch.SchoolName}).ToDictionary(k => k.SchoolCode, k => k.SchoolName);
	var collectionData = MathTestingByGender.Where(md => Schools.Select(k => k.Key).Contains(md.EntityCode));

	XDocument cxml = new XDocument(){ Declaration = new XDeclaration("1.0","UTF-8",null)};
	XNamespace xnBase = "http://schemas.microsoft.com/collection/metadata/2009";
	XNamespace xnP = "http://schemas.microsoft.com/livelabs/pivot/collection/2009";
/*
 	String 	
	LongString 	
	Number 	
	DateTime 	
	Link 	
	Item
*/
	
	XDocument xd = XDocument.Load(@"c:\projects\pass summit 2012.cxml");
	xd.Descendants(xnBase+"FacetCategory").FirstOrDefault()
	.Attributes().Select(att => new{
		att.Name,
		att.Name.LocalName,
		att.Name.Namespace,
		att.Name.NamespaceName,
		att.Value
	})
	//.Dump()
	;

	
	XElement Collection = new XElement(
		xnBase+"Collection",
		new XAttribute("Name","MathTesting"), 
		new XAttribute("SchemaVersion","1"),
		new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
		new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
		new XAttribute(XNamespace.Xmlns + "P", "http://schemas.microsoft.com/livelabs/pivot/collection/2009")
		);
	
	XElement FacetCategories = new XElement(xnBase+"FacetCategories",
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","EntityCode"),
			new XAttribute("Type","String"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false"),
			new XElement(xnBase+"Extension",
				new XElement(xnP+"SortOrder", new XAttribute("Name","EntityCode"),
					collectionData.Select(mt => new XElement(xnP+"SortValue",new XAttribute("Value",mt.EntityCode))).Distinct()
			))
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","EntityName"),
			new XAttribute("Type","String"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false"),
			new XElement(xnBase+"Extension",
				new XElement(xnP+"SortOrder", new XAttribute("Name","EntityName"),
					collectionData.Select(mt => new XElement(xnP+"SortValue",new XAttribute("Value",mt.EntityName))).Distinct()
			))
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","YearKey"),
			new XAttribute("Type","Number"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false")
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","SchoolYear"),
			new XAttribute("Type","String"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false"),
			new XElement(xnBase+"Extension",
				new XElement(xnP+"SortOrder", new XAttribute("Name","SchoolYear"),
					collectionData.Select(mt => new XElement(xnP+"SortValue",new XAttribute("Value",mt.SchoolYear))).Distinct()
			))
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","DemographicGroup"),
			new XAttribute("Type","String"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false"),
			new XElement(xnBase+"Extension",
				new XElement(xnP+"SortOrder", new XAttribute("Name","DemographicGroup"),
					collectionData.Select(mt => new XElement(xnP+"SortValue",new XAttribute("Value",mt.DemographicGroup))).Distinct()
			))
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","TotalTested"),
			new XAttribute("Type","Number"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false")
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","NumberScoringAtLevel1"),
			new XAttribute("Type","Number"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false")
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","NumberScoringAtLevel2"),
			new XAttribute("Type","Number"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false")
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","NumberScoringAtLevel3"),
			new XAttribute("Type","Number"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false")
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","NumberScoringAtLevel4"),
			new XAttribute("Type","Number"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false")
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","PercentScoringAtLevel1"),
			new XAttribute("Type","Number"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false")
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","PercentScoringAtLevel2"),
			new XAttribute("Type","Number"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false")
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","PercentScoringAtLevel3"),
			new XAttribute("Type","Number"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false")
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","PercentScoringAtLevel4"),
			new XAttribute("Type","Number"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false")
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","SumOfStudentScores"),
			new XAttribute("Type","Number"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false")
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","ComputedMeanStudentScores"),
			new XAttribute("Type","Number"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false")
			),
		new XElement(xnBase+"FacetCategory",
			new XAttribute("Name","GradeLevel"),
			new XAttribute("Type","Number"),
			new XAttribute(xnP+"IsFilterVisible","false"),
			new XAttribute(xnP+"IsWordWheelVisible","false"),
			new XAttribute(xnP+"IsMetaDataVisible","false"),
			new XElement(xnBase+"Extension",
				new XElement(xnP+"SortOrder", new XAttribute("Name","GradeLevel"),
					MathTestingByGender.Where(md => Schools.Select(k => k.Key).Contains(md.EntityCode))
					.Select(mt => new XElement(xnP+"SortValue",new XAttribute("Value",mt.GradeLevel))).Distinct()
			))
			)
	);
	
	
	XElement Items = new XElement(xnBase+"Items", 
		new XAttribute("ImgBase",@"PASS Summit 2012\DZImages.xml"),
					MathTestingByGender.Where(md => Schools.Select(k => k.Key).Contains(md.EntityCode))
					.Select(mt => 
						new XElement("Item",
							new XAttribute("Img",""),
							new XAttribute("Id",""),
							new XAttribute("Name",""),
							new XElement("Facets",
								new XElement("Facet",new XAttribute("Name","EntityCode"),new XElement("String", new XAttribute("Value", mt.EntityCode))),
								new XElement("Facet",new XAttribute("Name","EntityName"),new XElement("String", new XAttribute("Value", mt.EntityName))),
								new XElement("Facet",new XAttribute("Name","YearKey"),new XElement("Number", new XAttribute("Value", mt.YearKey))),
								new XElement("Facet",new XAttribute("Name","SchoolYear"),new XElement("String", new XAttribute("Value", mt.SchoolYear))),
								new XElement("Facet",new XAttribute("Name","DemographicGroup"),new XElement("String", new XAttribute("Value", mt.DemographicGroup))),
								new XElement("Facet",new XAttribute("Name","TotalTested"),new XElement("Number", new XAttribute("Value", mt.TotalTested))),
								new XElement("Facet",new XAttribute("Name","NumberScoringAtLevel1"),new XElement("Number", new XAttribute("Value", mt.NumberScoringAtLevel1))),
								new XElement("Facet",new XAttribute("Name","NumberScoringAtLevel2"),new XElement("Number", new XAttribute("Value", mt.NumberScoringAtLevel2))),
								new XElement("Facet",new XAttribute("Name","NumberScoringAtLevel3"),new XElement("Number", new XAttribute("Value", mt.NumberScoringAtLevel3))),
								new XElement("Facet",new XAttribute("Name","NumberScoringAtLevel4"),new XElement("Number", new XAttribute("Value", mt.NumberScoringAtLevel4))),
								new XElement("Facet",new XAttribute("Name","PercentScoringAtLevel1"),new XElement("Number", new XAttribute("Value", mt.PercentScoringAtLevel1))),
								new XElement("Facet",new XAttribute("Name","PercentScoringAtLevel2"),new XElement("Number", new XAttribute("Value", mt.PercentScoringAtLevel2))),
								new XElement("Facet",new XAttribute("Name","PercentScoringAtLevel3"),new XElement("Number", new XAttribute("Value", mt.PercentScoringAtLevel3))),
								new XElement("Facet",new XAttribute("Name","PercentScoringAtLevel4"),new XElement("Number", new XAttribute("Value", mt.PercentScoringAtLevel4))),
								new XElement("Facet",new XAttribute("Name","SumOfStudentScores"),new XElement("Number", new XAttribute("Value", mt.SumOfStudentScores))),
								new XElement("Facet",new XAttribute("Name","ComputedMeanStudentScores"),new XElement("Number", new XAttribute("Value", mt.ComputedMeanStudentScores))),
								new XElement("Facet",new XAttribute("Name","GradeLevel"),new XElement("Number", new XAttribute("Value", mt.GradeLevel)))
							)
						)
					)
	);
	
	Collection.Add(FacetCategories);
	Collection.Add(Items);
	cxml.Add(Collection);
	cxml.Dump();

	/*
	MathTestingByEthnicity.Where(md => Schools.Select(k => k.Key).Contains(md.EntityCode)).Dump();
	MathTestingByEthnicityAndGender.Where(md => Schools.Select(k => k.Key).Contains(md.EntityCode)).Dump();
	MathTestingByGender.Where(md => Schools.Select(k => k.Key).Contains(md.EntityCode)).Dump();
	*/
	
}

// Define other methods and classes here

public String CountyCode{get{return InstructionalHierarchy.Where(ih => ih.DistrictName.Contains("Middletown")).First().CountyCode;}}

public String CountyName{get{return InstructionalHierarchy.Where(ih => ih.DistrictName.Contains("Middletown")).First().CountyName;}}

public String DistrictCode{get{return InstructionalHierarchy.Where(ih => ih.DistrictName.Contains("Middletown")).First().DistrictCode;}}

public String DistrictName {get{return InstructionalHierarchy.Where(ih => ih.DistrictName.Contains("Middletown")).First().DistrictName;}}
