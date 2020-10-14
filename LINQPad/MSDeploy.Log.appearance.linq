<Query Kind="Program" />

void Main()
{

	
	XElement bi01 = XElement.Load(@"C:\Users\mjfil\OneDrive\VA Projects\IIS Logs\vhacdwdwhbi06.xml");
	
	bi01
	.Dump();
	
	DataSet ds = new DataSet();
	ds.ReadXmlSchema(@"C:\Users\mjfil\OneDrive\VA Projects\IIS Logs\vhacdwdwhbi06.xsd");
	
	ds.Dump();
	
	

/*
	var DirPath = @"C:\Users\mjfil\OneDrive\VA Projects\IIS Logs";
	
	var FileSets = new DirectoryInfo(DirPath).GetFiles("*.log").Select(fi => new{
		Server = fi.Name.Replace(".log",String.Empty),
		Contents = File.ReadAllLines(fi.FullName)
	});
	
	var vhacdwdwhbi01 = FileSets.Where(fi => fi.Server.Equals("vhacdwdwhbi01")).First();
	var vhacdwdwhbi02 = FileSets.Where(fi => fi.Server.Equals("vhacdwdwhbi02")).First();
	var vhacdwdwhbi03 = FileSets.Where(fi => fi.Server.Equals("vhacdwdwhbi03")).First();
	var vhacdwdwhbi04 = FileSets.Where(fi => fi.Server.Equals("vhacdwdwhbi04")).First();
	var vhacdwdwhbi05 = FileSets.Where(fi => fi.Server.Equals("vhacdwdwhbi05b")).First();
	var vhacdwdwhbi06 = FileSets.Where(fi => fi.Server.Equals("vhacdwdwhbi06b")).First();
*/	
	
//	//Regex.Matches(tt,"/").Count == 11
//	vhacdwdwhbi02.Contents
//	.Select((x,i) => new {Id = i, LineItem = x})
//	.Dump();
	
	
//	("vhacdwdwhbi01 compared to all").Dump();
//	vhacdwdwhbi01.Contents.Except(vhacdwdwhbi02.Contents).Dump();
//	vhacdwdwhbi01.Contents.Except(vhacdwdwhbi03.Contents).Dump();
//	vhacdwdwhbi01.Contents.Except(vhacdwdwhbi04.Contents).Dump();
//	vhacdwdwhbi01.Contents.Except(vhacdwdwhbi05.Contents).Dump();
//	vhacdwdwhbi01.Contents.Except(vhacdwdwhbi06.Contents).Dump();
//	
//	("vhacdwdwhbi02 compared to all, excluding vhacdwdwhbi01").Dump();
//	vhacdwdwhbi02.Contents.Except(vhacdwdwhbi03.Contents).Dump();
//	vhacdwdwhbi02.Contents.Except(vhacdwdwhbi04.Contents).Dump();
//	vhacdwdwhbi02.Contents.Except(vhacdwdwhbi05.Contents).Dump();
//	vhacdwdwhbi02.Contents.Except(vhacdwdwhbi06.Contents).Dump();
//	
//	("vhacdwdwhbi03 compared to all, excluding vhacdwdwhbi01 and vhacdwdwhbi02").Dump();
//	vhacdwdwhbi03.Contents.Except(vhacdwdwhbi04.Contents).Dump();
//	vhacdwdwhbi03.Contents.Except(vhacdwdwhbi05.Contents).Dump();
//	vhacdwdwhbi03.Contents.Except(vhacdwdwhbi06.Contents).Dump();
//	
//	("vhacdwdwhbi04 compared to all, excluding vhacdwdwhbi01,vhacdwdwhbi02 and vhacdwdwhbi03").Dump();
//	vhacdwdwhbi04.Contents.Except(vhacdwdwhbi05.Contents).Dump();
//	vhacdwdwhbi04.Contents.Except(vhacdwdwhbi06.Contents).Dump();
//	
//	("vhacdwdwhbi05 compared to vhacdwdwhbi06 only").Dump();
//	vhacdwdwhbi05.Contents.Except(vhacdwdwhbi06.Contents).Dump();
	
	
	
	
}

// Define other methods and classes here
public class MSDeploy
{
	public struct CertStoreSettings {
		public string path;
	}
}
