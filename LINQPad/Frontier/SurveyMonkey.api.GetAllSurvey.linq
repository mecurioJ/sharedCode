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

var token = "BnD3DkfZSZDQfqb4xeoLqUkNw2-YVvZ8-gGxrZknFVhBbV-tWsLK3zZZoaoGFLG1cU48z8Nn7SCnaP6SpYI5X5RmR1cTY-QGWUmstdXrN8aEXNzMakSZWvAk2AtTUo6d";

//var token = "2ivTqZYSLeqZEm10ay-LK3uFaKKuzqkTm1lO7D5Zc8yEUXARRGjoiJE5LTFNeb.JTAScSIwaWeP7r2vHIL8xCu5pMAt47MWAOUmJjBjPX3VJQ7nvH7BzYUC080vVXflD";


using(var api = new SurveyMonkeyApi(token))
{
	
	
	api.ApiRequestsMade.Dump();

	var g = api.GetSurveyList();
	//.Where(tt => tt.Title.Equals("Post Flight Survey: AUG2019-DEC2019"));
		
	api.ApiRequestsMade.Dump();

	//File.WriteAllText("C:\\Temp\\allSurveys.json",	JsonConvert.SerializeObject(g));
	
	g.Dump();

//	using (var writer = new CsvWriter(new StreamWriter("C:\\Temp\\167111222.07172019.07222019.csv")))
//	{
//		try
//		{
//			writer.WriteRecords(g);
//		}
//		catch (Exception ex)
//		{
//			ex.Dump();			
//		}
//	}
//	g.Select(r => r.ResponseID).Distinct().Count().Dump();
//	g.Count().Dump();

}