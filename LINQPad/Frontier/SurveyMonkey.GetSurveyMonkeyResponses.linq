<Query Kind="Program">
  <NuGetReference>ServiceStack.Text</NuGetReference>
  <NuGetReference>SurveyMonkeyApi</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>ServiceStack.Text</Namespace>
  <Namespace>SurveyMonkey</Namespace>
  <Namespace>SurveyMonkey.Containers</Namespace>
  <Namespace>SurveyMonkey.ProcessedAnswers</Namespace>
  <Namespace>SurveyMonkey.RequestSettings</Namespace>
</Query>

void Main()
{
//ProdToken : 
var token = "Tpt.p.YeYZ9Nk25cfnMBz7kYdiMtuvtXM4DyNRAhQYuxGCFlEg-VRZuk3gvIYjLBgjnsoNKb2pZr.b.qzFVIXHjshxHvW.piBcaUAGXdK3K1Uba8f.oKiIbehw1n1lQd";
//var token = "2ivTqZYSLeqZEm10ay-LK3uFaKKuzqkTm1lO7D5Zc8yEUXARRGjoiJE5LTFNeb.JTAScSIwaWeP7r2vHIL8xCu5pMAt47MWAOUmJjBjPX3VJQ7nvH7BzYUC080vVXflD";

var surveyId = 274530266;
var startCreatedAt = DateTime.Parse("2020-02-13");
var endModifiedAt = DateTime.Parse("2020-05-13");
var fileLoc = @$"D:\Projects\Frontier\SurveyMonkeyGen2\{surveyId}.02132020.05132020.csv";

List<SurveyMonkey_Responses> responses = new List<SurveyMonkey_Responses>();

using(var api = new SurveyMonkeyApi(token))
{
		responses = api.GetSurveyResponseDetailsList(surveyId, new GetResponseListSettings()
		{
			StartCreatedAt = startCreatedAt
								,
			EndModifiedAt = endModifiedAt
		}).SelectMany(r => r.Questions.SelectMany(q => q.Answers.Select(
				a => new SurveyMonkey_Responses
				{
					AnswerText = a.Text,
					ArriveAirPort = String.Empty,
					ArriveDateTime = r.CustomVariables.ContainsKey("ADT") ? r.CustomVariables["ADT"] : string.Empty,
					ChoiceID = a.ChoiceId.ToString(),
					CustEmail = r.CustomVariables.ContainsKey("CEM") ? r.CustomVariables["CEM"] : string.Empty,
					DateModified = r.DateModified.ToString(),
					DepartAirport = r.CustomVariables.ContainsKey("DPA") ? r.CustomVariables["DPA"] : string.Empty,
					DepartDateTime = r.CustomVariables.ContainsKey("DDT") ? r.CustomVariables["DDT"] : string.Empty,
					DepartureFlightNumber = r.CustomVariables.ContainsKey("DFN") ? r.CustomVariables["DFN"] : string.Empty,
					DisCountDen = r.CustomVariables.ContainsKey("DisCountDen") ? r.CustomVariables["DisCountDen"] : string.Empty,
					EmailOpenDateTime = r.CustomVariables.ContainsKey("EODT") ? r.CustomVariables["EODT"] : string.Empty,
					LoyaltyProg = r.CustomVariables.ContainsKey("LTY") ? r.CustomVariables["LTY"] : string.Empty,
					NumPassOnPNR = r.CustomVariables.ContainsKey("NPNR") ? r.CustomVariables["NPNR"] : string.Empty,
					OrigAirport = r.CustomVariables.ContainsKey("ORIG") ? r.CustomVariables["ORIG"] : string.Empty,
					OtherID = a.OtherId.ToString(),
					PNR = r.CustomVariables.ContainsKey("PNR") ? r.CustomVariables["PNR"] : string.Empty,
					PageID = String.Empty,
					QuestionID = q.Id.ToString(),
					ResponseID = r.Id.ToString(),
					RowID = a.RowId.ToString(),
					SurveyID = r.SurveyId.ToString(),
					SurveySentDateTime = r.CustomVariables.ContainsKey("SSDT") ? r.CustomVariables["SSDT"] : string.Empty,
					Zip = r.CustomVariables.ContainsKey("Zip") ? r.CustomVariables["Zip"] : string.Empty,
				}
			))).ToList();
	}

	var fileContents = CsvSerializer.SerializeToCsv(responses.Cast<SurveyMonkey_Responses>());
	byte[] textByteArray = Encoding.UTF8.GetBytes(fileContents);
	File.WriteAllBytes(fileLoc,textByteArray);


}

	

public class SurveyMonkey_Responses
{
	public String AnswerText { get; set; }
	public String ArriveAirPort { get; set; }
	public String ArriveDateTime { get; set; }
	public String ChoiceID { get; set; }
	public String CustEmail { get; set; }
	public String DateModified { get; set; }
	public String DepartAirport { get; set; }
	public String DepartDateTime { get; set; }
	public String DepartureFlightNumber { get; set; }
	public String DisCountDen { get; set; }
	public String EmailOpenDateTime { get; set; }
	public String LoyaltyProg { get; set; }
	public String NumPassOnPNR { get; set; }
	public String OrigAirport { get; set; }
	public String OtherID { get; set; }
	public String PNR { get; set; }
	public String PageID { get; set; }
	public String QuestionID { get; set; }
	public String ResponseID { get; set; }
	public String RowID { get; set; }
	public String SurveyID { get; set; }
	public String SurveySentDateTime { get; set; }
	public String Zip { get; set; }
	public String CreateDate { get; set; }
}