<Query Kind="Program">
  <NuGetReference>Isg.Extensions</NuGetReference>
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>Isg.Collections</Namespace>
  <Namespace>Isg.Extensions</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
//	var filter = new[]{"5 Why RCA.xlsx",
//	"SMART Goals Template.xlsx",
//	"CE_Matrix.xls",
//	"Project Gantt Chart.xlsx",
//	"FFU Template.xlsx",
//	"SOP Template.docx"};

	//filter.Dump();
	String StartingPoint = @"C:\Projects\intranet\temp\Perf Review Forms";
	String DocDirectory = @"\documents\hr";
	
	System.IO.DirectoryInfo dir = new DirectoryInfo(StartingPoint);
	
	dir.GetFiles()
	//.Where(fnm => filter.Contains(fnm.Name))
	.Select(fi => 
			//String.Format("Response.Write(\"<li><a href='{1}' class='regular'>{0} (Updated 07/2014)</a></li>\")",
			String.Format("<li><a href=\"{1}\" class=\"regular\">{0}</a></li>",
			fi.Name.Replace(fi.Extension,String.Empty),fi.FullName
				.Replace(StartingPoint,DocDirectory)
				.Replace(@"\",@"/")))
				
				.Aggregate((p,n) => String.Format("{0}{1}",p,n)).Dump();
	
	dir.GetDirectories().Select(di => new{
		directoryName = di.Name,
		Files = di.GetFiles().Select(fi => 
			String.Format("<li><a href=\"{1}\" class=\"regular\">{0} (Updated 07/2014)</a></li>",
			fi.Name.Replace(fi.Extension,String.Empty),fi.FullName
				.Replace(@"G:\hr\Employment Eligibility",String.Empty)
				.Replace(@"\",@"/"))).Aggregate((p,n) => String.Format("{0}{1}",p,n))
		}).ToLookup(n => n.directoryName)
		.Select(k => string.Format("<h3>{0}</h3><div style=\"text-align: left\"><ul>{1}</ul></div>",k.Key,k.Select(f => f.Files).FirstOrDefault()))
		.Dump();
	
	//dir.GetFiles().Select(fi => fi.Name).Skip(1000).Dump();
	
	dir.GetFiles().Select(fi => new{
		lineVal = String.Format("<li><a href='/documents/hr/{0}'>{1}<em>(Updated 06/19/2014)</em></a></li>"
			, fi.Name
			, fi.Name.Replace(fi.Extension,String.Empty)
		)
		});
//	
//	dir.GetDirectories("*").Select(di => 
//	di.GetFiles()
////		new{
////			//Directory = String.Format("<h4>{0}</h4>",di.Name),
////	 		files = new DirectoryInfo(di.FullName).GetFiles()
////				.Select(fi => String.Format("{1}.{0}.{2}",
////				fi.Name,
////				di.Name,
////				Regex.Replace(
////				fi.Name.Replace(fi.Extension,String.Empty),@"\([0-9]\)*",String.Empty)
////					
////				)).ToList()
////		}
//	//String.Format("<li><h4>{0}</h4></li>",di.Name)
//	).Dump();
	
	//FileInfo[] fileList = dir.GetFiles("*.Schema", SearchOption.AllDirectories);
	
	//fileList.Select(fi => fi.Name).Dump();
	
//	var FileSetDetails = 
//	fileList.Where(fl => fl.Extension.Equals(".csv"))
//	.Select(fi => new{
//		fi.DirectoryName,
//		Name = fi.Name.Split('.').First(),
//		fi.Extension,
//		fi.FullName,
//		fi.Length,
//		Season = fi.DirectoryName.Replace(@"B:\Projects\cfbStats\cfbstats.com-",string.Empty).Split('-').FirstOrDefault()
//	})
//	.GroupBy(g => g.DirectoryName, g => new{
//		ConnectionName = String.Format("{0}-{1}",g.Season,g.Name),
//		g.Season,
//		g.Name,
//		g.FullName,
//		g.Length
//	});
//	
////	FileSetDetails.SelectMany(fsd => fsd.Select(s => s.Season).Distinct()).Dump();
//	
////	FileSetDetails.Select(k => new{
////		VariableName = String.Format("_dir_{0}",
////		k.Key.Split('\\').Last().Substring(0,17).Replace("-",String.Empty).Replace(".",String.Empty)
////		),
////		k.Key
////	}).Dump();
//	
//	FileSetDetails.SelectMany(k => k.Select(v => new{
//		FileName = v.FullName,
//		ShortName = v.Name
//		})).GroupBy(sn => sn.ShortName).Select(t => 
//		new{
//			connMgrName = t.Key.ToTitleCase().Replace("-",String.Empty),
//			Files = t.Select(sm => sm.FileName).Aggregate((prev,next) => String.Format("{0}|{1}",prev,next))
//			})
//			;
//			
//	FileSetDetails.SelectMany(k => k.Select(v => new{
//		v.Season,
//		v.FullName,
//		Table = "["+v.Name.ToTitleCase().Replace("-",String.Empty)+"]",
//	})).Dump();
//	
//	FileSetDetails.SelectMany(k => k
//	//.Where(w => w.Season.Equals("2005"))
//	.Select(v => new{
//		FileName = v.FullName,
//		ConnMgrName = v.Name.ToTitleCase().Replace("-",String.Empty),
//		Server = "JOEYMOBILE",
//		Database = "cfbstats",
//		Table = "[Season"+v.Season+"]."+"["+v.Name.ToTitleCase().Replace("-",String.Empty)+"]",
//		Package = "Season"+v.Season+"."+v.Name.ToTitleCase().Replace("-",String.Empty)+".dtsx",
//		v.Length
//		})).OrderByDescending(d => d.Length).Select(t => t.ConnMgrName).Distinct().Dump();
}

// Define other methods and classes here