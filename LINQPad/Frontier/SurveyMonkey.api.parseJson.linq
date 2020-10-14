<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
</Query>

void Main()
{
	var source = (JObject)JsonConvert.DeserializeObject(File.ReadAllText("C:\\Temp\\167111222.SurveyDetails.json"));

	source.SelectTokens("$...Answers.Choices")
		.Select(
			ac => ac.Select(
				cc =>
				new
				{
					SurveyID = "",
					sid = cc.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Value<String>("Id"),
					QuestionID = cc.Parent.Parent.Parent.Parent.Parent.Value<String>("Id"),
					ChoiceID = cc.Value<String>("Id"),
					ChoiceDescr = cc.Value<String>("Description"),
					ChoiceIsNA = cc.Value<String>("IsNa"),
					ChoiceIsVisible = cc.Value<String>("Visible"),
					ChoicePosition  = cc.Value<String>("Position"),
					ChoiceText = cc.Value<String>("Text"),
					ChoiceWeight = cc.Value<String>("Weight")
				}
			)
		).Dump();

	IEnumerable<DimHeading> DimHeadings = 
	source["Pages"]
		.SelectMany(pg => pg["Questions"].Select(
			pgq => new DimHeading(pgq,source["Id"].ToString()))
		);


	DimDefinition DimDefinition = new DimDefinition(source);
	
	IEnumerable<DimRow> DimRows = source["Pages"]
		.SelectMany(pg => pg["Questions"].SelectMany(
				pgq => pgq.SelectTokens("Answers..Rows")
				.Where(r => r.HasValues).Children()
				.Where(rc => !String.IsNullOrEmpty(rc.Value<String>("Text")))
				.Select(ri => new DimRow(ri, source["Id"].ToString(), pgq["Id"].ToString()))
	));





/*
	In the DB, we only care about the following things in relation
	to a Survey...:
	DimChoice
	**DimDefinition
	**DimHeading
	DimQuestion
	**DimRow
*/

}

// Define other methods and classes here

public class DimDefinition
{
	
	public DimDefinition(){}
	public DimDefinition(JObject source)
	{
		SurveyID = long.Parse(source["Id"].ToString());
		SurveyTitle = source["Title"].ToString();
		SurveyNickname = source["Nickname"].ToString();
		SurveyCategory = source["Category"].ToString();
		SurveyPageCount = int.Parse(source["PageCount"].ToString());
		SurveyQuestionCount = int.Parse(source["QuestionCount"].ToString());
		SurveyLanguage = source["Language"].ToString();
		SurveyFooter = bool.Parse(source["Footer"].ToString());
		CustomVariable_ADT = source["CustomVariables"]["ADT"].ToString();
		CustomVariable_ArriveAir = source["CustomVariables"]["ArriveAir"].ToString();
		CustomVariable_CEM = source["CustomVariables"]["CEM"].ToString();
		CustomVariable_DDT = source["CustomVariables"]["DDT"].ToString();
		CustomVariable_DFN = source["CustomVariables"]["DFN"].ToString();
		CustomVariable_DPA = source["CustomVariables"]["DPA"].ToString();
		CustomVariable_DisCountDen = source["CustomVariables"]["DisCountDen"].ToString();
		CustomVariable_EODT = source["CustomVariables"]["EODT"].ToString();
		CustomVariable_LTY = source["CustomVariables"]["LTY"].ToString();
		CustomVariable_NPNR = source["CustomVariables"]["NPNR"].ToString();
		CustomVariable_ORIG = source["CustomVariables"]["ORIG"].ToString();
		CustomVariable_PNR = source["CustomVariables"]["PNR"].ToString();
		CustomVariable_SSDT = source["CustomVariables"]["SSDT"].ToString();
		CustomVariable_Zip = source["CustomVariables"]["Zip"].ToString();
		AnalyzeURL = source["AnalyzeUrl"].ToString();
		CollectURL = source["CollectUrl"].ToString();
		EditURL = source["EditUrl"].ToString();
		PreviewURL = source["Preview"].ToString();
		SummaryURL = source["SummaryUrl"].ToString();
		CreatedDate = DateTime.Parse(source["DateCreated"].ToString());
		ModifiedDate = DateTime.Parse(source["DateModified"].ToString());
	}

	public long SurveyID {get;set;}
	public string SurveyTitle  {get;set;}
	public string SurveyNickname  {get;set;}
	public string SurveyCategory  {get;set;}
	public int SurveyPageCount  {get;set;}
	public int SurveyQuestionCount  {get;set;}
	public string SurveyLanguage  {get;set;}
	public bool SurveyFooter  {get;set;}
	public string CustomVariable_ADT  {get;set;}
	public string CustomVariable_ArriveAir  {get;set;}
	public string CustomVariable_CEM  {get;set;}
	public string CustomVariable_DDT  {get;set;}
	public string CustomVariable_DFN  {get;set;}
	public string CustomVariable_DPA  {get;set;}
	public string CustomVariable_DisCountDen  {get;set;}
	public string CustomVariable_EODT  {get;set;}
	public string CustomVariable_LTY  {get;set;}
	public string CustomVariable_NPNR  {get;set;}
	public string CustomVariable_ORIG { get; set; }
	public string CustomVariable_PNR { get; set; }
	public string CustomVariable_SSDT { get; set; }
	public string CustomVariable_Zip { get; set; }
	public string AnalyzeURL { get; set; }
	public string CollectURL { get; set; }
	public string EditURL { get; set; }
	public string PreviewURL { get; set; }
	public string SummaryURL { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime ModifiedDate { get; set; }
	public long CollectorID { get; set; }
	public string SurveyPath { get; set; }
}

public class DimRow
{
	
	public DimRow()
	{
		
	}
	
	public DimRow(JToken rowItem, string surveyId, string questionId)
	{
						SurveyId = surveyId;
						QuestionID = questionId;
						RowID = rowItem.Value<String>("Id");
						RowPosition = rowItem.Value<String>("Position");
						RowText = rowItem.Value<String>("Text");
						RowIsVisible = rowItem.Value<String>("Visible");
						RowIsRequired = rowItem.Value<String>("Required");
						RowType = rowItem.Value<String>("Type");
	}
	public String SurveyId { get; set; }
	public String QuestionID { get; set; }
	public String RowID { get; set; }
	public String RowIsRequired { get; set; }
	public String RowIsVisible { get; set; }
	public String RowPosition { get; set; }
	public String RowText { get; set; }
	public String RowType { get; set; }

 }
 
 public class DimHeading
 {
	public DimHeading()
	{
		
	}
	
	public DimHeading(JToken HeadingItem, string surveyId)
	{
		SurveyID = surveyId; 
		QuestionID = HeadingItem["Id"].ToString();
		SubType = HeadingItem["Subtype"].ToString();
		Family = HeadingItem["Family"].ToString();
		Heading = HeadingItem["Headings"].First()["Heading"].ToString();
		HeadingIsVisible = HeadingItem["Visible"].ToString();
	}

	public String SurveyID {get; set;}
	public String QuestionID  {get; set;}
	public String SubType {get; set;}
	public String Family {get; set;}
	public String Heading {get; set;}
	public String HeadingIsVisible {get; set;}
	public String HeadingURL {get; set;}

}