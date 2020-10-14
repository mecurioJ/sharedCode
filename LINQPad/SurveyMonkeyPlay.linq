<Query Kind="Program">
  <NuGetReference>SurveyMonkeyApi</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>SurveyMonkey</Namespace>
  <Namespace>SurveyMonkey.Enums</Namespace>
</Query>

void Main()
{
	var token = "Tpt.p.YeYZ9Nk25cfnMBz7kYdiMtuvtXM4DyNRAhQYuxGCFlEg-VRZuk3gvIYjLBgjnsoNKb2pZr.b.qzFVIXHjshxHvW.piBcaUAGXdK3K1Uba8f.oKiIbehw1n1lQd";
	using (SurveyMonkeyApi api = new SurveyMonkeyApi(token))
	{
		//set the object to the actual details from Survey Monkey
		var survey = api.GetSurveyList();
		survey.Dump();
	}
}

// Define other methods, classes and namespaces here
