<Query Kind="Statements">
  <NuGetReference>CsvHelper</NuGetReference>
  <NuGetReference>SurveyMonkeyApi</NuGetReference>
  <Namespace>CsvHelper</Namespace>
  <Namespace>CsvHelper.Configuration</Namespace>
  <Namespace>CsvHelper.Configuration.Attributes</Namespace>
  <Namespace>CsvHelper.Expressions</Namespace>
  <Namespace>CsvHelper.TypeConversion</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>SurveyMonkey</Namespace>
  <Namespace>SurveyMonkey.Containers</Namespace>
  <Namespace>SurveyMonkey.ProcessedAnswers</Namespace>
  <Namespace>SurveyMonkey.RequestSettings</Namespace>
</Query>

//var token = "BnD3DkfZSZDQfqb4xeoLqUkNw2-YVvZ8-gGxrZknFVhBbV-tWsLK3zZZoaoGFLG1cU48z8Nn7SCnaP6SpYI5X5RmR1cTY-QGWUmstdXrN8aEXNzMakSZWvAk2AtTUo6d";

var token = "2ivTqZYSLeqZEm10ay-LK3uFaKKuzqkTm1lO7D5Zc8yEUXARRGjoiJE5LTFNeb.JTAScSIwaWeP7r2vHIL8xCu5pMAt47MWAOUmJjBjPX3VJQ7nvH7BzYUC080vVXflD";


using(var api = new SurveyMonkeyApi(token))
{
	
	
	api.ApiRequestsMade.Dump();

	var g = api.GetSurveyResponseDetailsList(167111222, new GetResponseListSettings() {
		  StartCreatedAt = new DateTime(2019,7,09)
		, EndModifiedAt = new DateTime(2019,8,05)
	})
	.SelectMany(r => r.Questions.SelectMany(q => q.Answers.Select(
			a => new
			{
				AnswerText = a.Text,			
				ArriveAirPort = String.Empty,
				ArriveDateTime = r.CustomVariables.ContainsKey("ADT") ? r.CustomVariables["ADT"] : string.Empty,
				a.ChoiceId,
				r.CollectorId,
				CustEmail = r.CustomVariables.ContainsKey("CEM") ? r.CustomVariables["CEM"] : string.Empty,
				r.DateModified,
				DepartAirport = r.CustomVariables.ContainsKey("DPA") ? r.CustomVariables["DPA"] : string.Empty,
				DepartDateTime = r.CustomVariables.ContainsKey("DDT") ? r.CustomVariables["DDT"] : string.Empty,
				DepartureFlightNumber = r.CustomVariables.ContainsKey("DFN") ? r.CustomVariables["DFN"] : string.Empty,
				DisCountDen = r.CustomVariables.ContainsKey("DisCountDen") ? r.CustomVariables["DisCountDen"] : string.Empty,
				EmailOpenDateTime = r.CustomVariables.ContainsKey("EODT") ? r.CustomVariables["EODT"] : string.Empty,
				LoyaltyProg = r.CustomVariables.ContainsKey("LTY") ? r.CustomVariables["LTY"] : string.Empty,
				NumPassOnPNR = r.CustomVariables.ContainsKey("NPNR") ? r.CustomVariables["NPNR"] : string.Empty,
				OrigAirport = r.CustomVariables.ContainsKey("ORIG") ? r.CustomVariables["ORIG"] : string.Empty,
				OtherID = a.OtherId,
				PNR = r.CustomVariables.ContainsKey("PNR") ? r.CustomVariables["PNR"] : string.Empty,
				PageId = String.Empty,
				QuestionID = q.Id,
				ResponseID = r.Id,
				RowID = a.RowId,		
				SurveyId = r.SurveyId,
				SurveySentDateTime =  r.CustomVariables.ContainsKey("SSDT") ? r.CustomVariables["SSDT"] : string.Empty,
				Zip = r.CustomVariables.ContainsKey("Zip") ? r.CustomVariables["Zip"] : string.Empty,
			}
		)))
		;
	
	api.ApiRequestsMade.Dump();

	using (var writer = new CsvWriter(new StreamWriter("C:\\Temp\\167111222.07172019.07222019.csv")))
	{
		try
		{
			writer.WriteRecords(g);
		}
		catch (Exception ex)
		{
			ex.Dump();			
		}
	}
	g.Select(r => r.ResponseID).Distinct().Count().Dump();
	g.Count().Dump();

}