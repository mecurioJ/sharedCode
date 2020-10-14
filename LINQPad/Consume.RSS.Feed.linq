<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.ServiceModel.Syndication</Namespace>
</Query>

void Main()
{
//Documents:  https://www.vapulse.net/community/feeds/documents?community=2052 
//Blog Posts:  https://www.vapulse.net/community/feeds/blogs?community=2052 
//Popular Discussions:  https://www.vapulse.net/community/feeds/popularthreads?community=2052 
//Discussions:  https://www.vapulse.net/community/feeds/threads?community=2052 

//Announcements:  https://www.vapulse.net/community/feeds/announcements?community=2052  


	//https://www.vapulse.net/activity?streamSource=custom&streamID=19766
//	var _vaPulse = "https://www.vapulse.net/community/feeds/allcontent?community=2052";
//	var client = new HttpClient();
//	client.DefaultRequestHeaders.Authorization = CreateHeader;a
//	String response = client.GetStringAsync(_vaPulse).Result;
//	var allContent = XDocument.Parse(response);
//	
	//allContent.Save(@"P:\PASinglePage\PASinglePage\content\allcontent.xml");
	
//	
//	GetFeedData("https://www.vapulse.net/community/feeds/allcontent?community=2052")
//		.Save(@"P:\PASinglePage\PASinglePage\content\allcontent-feed.xml");

	var feedItem = GetFeedData("https://www.vapulse.net/community/feeds/documents?community=2052");
	SyndicationFeed PAAnnounce = SyndicationFeed.Load(feedItem.CreateReader());
	PAAnnounce.Items.Dump();
	
	
//	GetFeedData("https://www.vapulse.net/community/feeds/blogs?community=2052")
//		.Save(@"P:\PASinglePage\PASinglePage\content\blogs-feed.xml");
//	
//	GetFeedData("https://www.vapulse.net/community/feeds/popularthreads?community=2052")
//		.Save(@"P:\PASinglePage\PASinglePage\content\popular-discussions-feed.xml");
//	
//	GetFeedData("https://www.vapulse.net/community/feeds/threads?community=2052")
//		.Save(@"P:\PASinglePage\PASinglePage\content\discussions-feed.xml");
//	
//	GetFeedData("https://www.vapulse.net/community/feeds/announcements?community=2052")
//		.Save(@"P:\PASinglePage\PASinglePage\content\announcements-feed.xml");
	
//	SyndicationFeed PAAnnounce = SyndicationFeed.Load(XElement.Parse(response).CreateReader());
//	
//	PAAnnounce.Dump();
	
	//JsonConvert.SerializeObject(XElement.Parse(response)).Dump();
	
	//response.Dump();
	
	//XDocument.Parse(response.ToString()).Dump();
	
}

public XDocument GetFeedData(string feedLink)
{
	HttpClient client = new HttpClient();
	client.DefaultRequestHeaders.Authorization = CreateHeader;
	String response = client.GetStringAsync(feedLink).Result;
	return XDocument.Parse(response);
}



public AuthenticationHeaderValue CreateHeader
{
get{
	return new AuthenticationHeaderValue(
		"Basic",
		Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}",
			"mario.filichia@va.gov",
			"Time4fun4u"
			)))
		);
}
}
// Define other methods and classes here