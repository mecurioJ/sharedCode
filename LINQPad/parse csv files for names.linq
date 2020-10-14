<Query Kind="Program">
  <NuGetReference>Soltys.ChangeCase</NuGetReference>
  <Namespace>Soltys.ChangeCase</Namespace>
</Query>

void Main()
{
	new DirectoryInfo(@"\\bvaserver\nas\etl_PowerSchoolStage").GetFiles("*.csv")
		//The following is still being accessed by the export process
		//.Where(fi => !fi.FullName.Equals(@"\\bvaserver\nas\etl_PowerSchoolStage\PS_CLASS_MEETING2015-09-15.csv"))
		
		.OrderByDescending(fi => fi.Length)
		.Select(fi => new{
		fi.FullName,
		Server = "JOEYWIN10",
		DB = "LCPS_PowerSchool",
			Name = "psinbound."+fi.Name.Replace(".csv","").Replace("2015-09-15",""),
			//"E:\etl_PowerSchoolStage\Schemas\"
			SourceName = "ps."+fi.Name.Replace(".csv","").Replace("2015-09-15",""),
			SSISSource = "ps_"+fi.Name.Replace(".csv","").Replace("2015-09-15",""),
			SSISDestination = "psInbound_"+fi.Name.Replace(".csv","").Replace("2015-09-15","").TitleCase().Replace(" ",""),
			TableName = "psInbound."+fi.Name.Replace(".csv","").Replace("2015-09-15","").TitleCase().Replace(" ",""),
			//"psInbound_"
			//FileName = @"C:\Workspaces\LCPS\etl_PowerSchoolStage"+fi.Name.Replace(".csv","").Replace("2015-09-15","")+".sql",
			FileName = @"C:\Workspaces\LCPS\etl_PowerSchoolStage\"+fi.Name,
			fi.Length,
			ImportName = fi.Name.Replace(".csv","").Replace("2015-09-15","")
		})
		.OrderByDescending(ord => ord.Length)
		.Select(st =>new{
			st.ImportName,
			st.SourceName,
			st.SSISDestination,
			st.TableName
			})
		.Dump();
}

// Define other methods and classes here
/*
Views
PS_CLASS_MEETING
PS_ENROLLMENT_PROG
PS_MEMBERSHIP_PROG
PS_SCHOOLENROLLMENT
*/