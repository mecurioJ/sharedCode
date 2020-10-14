<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <NuGetReference>HtmlAgilityPack</NuGetReference>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	String content = String.Empty;
	List<HtmlNode> imageNodes = new List<HtmlNode>();
	System.IO.DirectoryInfo dir = new DirectoryInfo(@"D:\Books\to Compile\MDX 2016 Cookbook");

	

	foreach (var fi in dir.GetDirectories("Source").FirstOrDefault().GetFiles("*.html", SearchOption.AllDirectories))
	{
	HtmlDocument hDoc = new HtmlWeb().Load(fi.FullName);
		content = String.Format("{0} {1}"
			,content
			, hDoc.DocumentNode.Descendants().Where(d => d.Name.Contains("div")).Where(d => d.Id.Equals("sbo-rt-content")) .FirstOrDefault().OuterHtml);
			imageNodes.AddRange(hDoc.DocumentNode.Descendants().Where(d => d.Name.Contains("img")));
	}
	
	String.Format("<html><body>{0}</body></html>",content.Replace("Â",String.Empty)).Dump();
	File.WriteAllText(dir.FullName+@"\FinalParse.html",String.Format("<html><body>{0}</body></html>",content
		.Replace("Â",String.Empty)
		.Replace(@"/library/view/mdx-with-microsoft/9781786460998/graphics/",String.Empty)
		
		));
	

//	foreach (var element in imageNodes.Select(s => s.Attributes["src"].Value))
//	{
//		using (var client = new WebClient())
//		{
//		client.DownloadFile(String.Format(@"https://www.safaribooksonline.com/",element),String.Format(@"{0}\{1}",dir.FullName,element.Split('/').Last()));
//		}
//	}



	//HtmlDocument hDoc = new HtmlWeb().Load(@"D:\Books\to Compile\MDX 2016 Cookbook\ch01.html");
	//https://www.safaribooksonline.com/library/view/mdx-with-microsoft/9781786460998/graphics/image_01_001.jpg
	
}

// Define other methods and classes here