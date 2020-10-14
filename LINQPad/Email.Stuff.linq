<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
  <NuGetReference>Pop3</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>Pop3</Namespace>
</Query>

void Main()
{
	var hostName = @"filichia.com";
	var useSSL = false;
	var userName = "joey@filichia.com";
	var passWord = "XWJj589a8yjVfxpnOlxuVHJaL";
	
	using(var pop3Client = new Pop3Client())
	{
		pop3Client.Connect(hostName,userName,passWord,useSSL);
		
		//var messages = pop3Client.List();
		
		foreach(var message in pop3Client.List())
		{
			pop3Client.Retrieve(message);
			message.Dump();
		}
		
		pop3Client.Disconnect();
	}
}

// Define other methods and classes here