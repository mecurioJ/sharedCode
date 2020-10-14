<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>RestSharp</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>RestSharp</Namespace>
  <Namespace>RestSharp.Authenticators</Namespace>
</Query>

void Main()
{	

	//Add your Id and Secret
	var ClientId ="f7b5a3b4-4c35-49ff-b088-8a49afece531";
	var ClientSecret="37711685-a18f-4330-8016-3a8ba2405394";
	var restUri = "https://partner12.powerschool.com";
	
	//Create a client helper object
	RestClient client = new RestClient(restUri);
	
	//Create a request object using a post method
	RestRequest request = new RestRequest(
		String.Format("/oauth/access_token?client_id={0}&client_secret={1}"
			,ClientId
			,ClientSecret)
		, Method.POST);
	
	//Add Grant type Object
	request.AddParameter("grant_type", "client_credentials");

	//populate a struct to store the token
	AccessToken token = JsonConvert.DeserializeObject<AccessToken>(client.Execute(request).Content);
	
	token.Dump();
	
	//Make a request to get the functional areas
	///ws/schema/query/com.pearson.core.attendance.attendance_code_by_school_date
	RestRequest areaRequest = new RestRequest("/ws/v1/school");
	
	//Add token for authentication
	areaRequest.AddParameter("access_token", "e1095ba5-9a52-4dfe-afd5-9f0146947f21");
	
	//Get the data!
	var area = client.Execute(areaRequest).Content;
	
	area.Dump();
	
	
}

// Define other methods and classes here
public struct AccessToken
{
    public long expires_in {get; set;}
    public string token_type { get; set; }
	public string access_token  { get; set; }
}