<Query Kind="Program">
  <NuGetReference>Isg.Extensions</NuGetReference>
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>Isg.Collections</Namespace>
  <Namespace>Isg.Extensions</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
	String StartingPoint = @"E:\Projects\CFB\FlatFileSources";
	
	System.IO.DirectoryInfo dir = new DirectoryInfo(StartingPoint);
	
	FileInfo[] fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);
	
	var FileSetDetails = 
	fileList.Where(fl => fl.Extension.Equals(".csv"))
	.Select(fi => new{
		fi.DirectoryName,
		Name = fi.Name.Split('.').First(),
		fi.Extension,
		fi.FullName,
		fi.Length,
		Season = fi.DirectoryName.Replace(@"E:\Projects\CFB\FlatFileSources\cfbstats.com-",string.Empty).Split('-').FirstOrDefault()
	})
	.GroupBy(g => g.DirectoryName, g => new{
		ConnectionName = String.Format("{0}-{1}",g.Season,g.Name),
		g.Season,
		g.Name,
		g.FullName,
		g.Length
	});
	
//	FileSetDetails.SelectMany(fsd => fsd.Select(s => s.Season).Distinct()).Dump();
	
//	FileSetDetails.Select(k => new{
//		VariableName = String.Format("_dir_{0}",
//		k.Key.Split('\\').Last().Substring(0,17).Replace("-",String.Empty).Replace(".",String.Empty)
//		),
//		k.Key
//	}).Dump();
	
//	FileSetDetails.SelectMany(k => k.Select(v => new{
//		FileName = v.FullName,
//		ShortName = v.Name
//		})).GroupBy(sn => sn.ShortName).Select(t => 
//		new{
//			connMgrName = t.Key.ToTitleCase().Replace("-",String.Empty),
//			Files = t.Select(sm => sm.FileName).Aggregate((prev,next) => String.Format("{0}|{1}",prev,next))
//			})
//			;
			
	FileSetDetails.SelectMany(k => k.Select(v => new{
		v.Season,
		v.FullName,
		Table = "["+v.Name.ToTitleCase().Replace("-",String.Empty)+"]",
	})).Select(t => String.Format("[Season{0}].{1}",t.Season,t.Table)).Dump();
	
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