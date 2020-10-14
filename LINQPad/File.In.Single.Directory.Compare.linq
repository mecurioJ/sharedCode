<Query Kind="Program" />

void Main()
{
	var DBNames = new[]{"master","tempdb","model","msdb","ReportServer","ReportServerTempDB","EMAZ","EMCA","EMCSE",
						"EMML","EMUT","EMNV","EMFL","EMNC","EMGA","EMCO","EMPA","EMMN","VFIN","EMFI","LSA","VMFG",
						"ESWData","ESW_Intranet","ESW_RPT","distribution","ESWManufacturing","ESW_CapEx","SSRS",
						"MFG_Test","JIM","Geography","ESWTicketSplits","EstoneConnect"};
						
	new DirectoryInfo(@"\\redcloud\sql$\backup")
	.GetFiles("*.bak", SearchOption.AllDirectories)
	.ToList().Select(fi => new{
		ShortName = System.Text.RegularExpressions.Regex.Split(fi.Name,"_backup")[0],
		fi.Name,
		fi.FullName,
		Date = fi.CreationTime.Date
	})
	.Where(fi => (new String[]
		{
			DateTime.Today.ToShortDateString(),
			DateTime.Today.AddDays(-1).ToShortDateString(),
			DateTime.Today.AddDays(-2).ToShortDateString()
		}
		).Contains(fi.Date.ToShortDateString()))
	.GroupBy(fi => fi.ShortName)
	.Select(k => new{
		k.Key,
		Dates = k.Select(d => d.Date.ToShortDateString()).Aggregate((p,n) => String.Format("{0};{1}",p,n)),
		BackupCount = k.Count()
		})
	.OrderByDescending(o => o.BackupCount)
	.Dump();
	
}

// Define other methods and classes here