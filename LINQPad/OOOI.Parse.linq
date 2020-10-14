<Query Kind="Program" />

void Main()
{
	var dir = new DirectoryInfo(@"\\goaspen\Department_Shares\Business Intelligence\Integrator\ARR_ON\Archive").GetFiles();

	//	foreach (var di in dir)
	//	{

	var di = dir.FirstOrDefault();
	var FullPath = di.FullName;
	var filetype = di.Extension.Remove(0,1);
	var fileName = di.Name.ToString();
	var content = File.ReadLines(FullPath).Skip(4).Take(3).Aggregate((p, n) => string.Format("{1} {0}",p,n));
	
	content.Dump();
	//var t1 = content.IndexOf("F9");
	//var FlightID = content.Substring(3,9).Dump();
	
	
//	
//	var dateString = di.Name.Substring(2, 6);
//
//	//string flightdate = "";
////
////	string filedate = $"20{dateString.Substring(0,2)}-{dateString.Substring(2,2)}-{dateString.Substring(4,2)}";
////
////	filedate.Dump();
//
//	RSOutput rs = new RSOutput(File.ReadAllLines(FullPath)
//	.Where(li => !String.IsNullOrWhiteSpace(li))
//	.Where(li => !li.Contains((char)1))
//	.Where(li => !li.Contains((char)2))
//	.Where(li => !li.Contains((char)3))
//	.Skip(1)
//	.ToArray(), di.Name.ToString());
//
//	rs.filedate.Dump();
//	var day = rs.filedate.Substring(4, 2);
//	var month = rs.filedate.Substring(2, 2);
//	var dayflightint = Convert.ToInt32(rs.dayflight);
//	var year = DateTime.Now.Year.ToString();
//
//	var dayint = Convert.ToInt32(day);
//	int yearint = Convert.ToInt32(year);
//	int monthint = Convert.ToInt32(month);
//
//
//	if (month == "12" && day == "31" && rs.dayflight == "01")
//	{
//		yearint = Convert.ToInt32(year) + 1;
//		year = yearint.ToString();
//		rs.flightdate = year + "01" + rs.dayflight;
//	}
//	if (dayflightint >= dayint)
//	{
//		rs.flightdate = year + month + rs.dayflight;
//	}
//	if (month != "12" && day != "31" && dayflightint < dayint)
//	{
//		monthint = Convert.ToInt32(month) + 1;
//		month = monthint.ToString();
//		if (month.Length == 1)
//		{
//			month = "0" + month;
//		}
//		rs.flightdate = year + month + rs.dayflight;
//	}
//
////F9,2430,N336FR,KSRQ,KCLE,on,1643,CLE, 52,20201926
//rs.WriteLine.Dump();
	//$"{rs.zero},{rs.one},{rs.two},{rs.three},{rs.four},{rs.filetype},{rs.five},{rs.six},{rs.seven},{rs.flightdate}".Dump();	

	// Define other methods and classes here
}


class RSOutput
{
	
	public string zero { get; set; }
	public string one { get; set; }
	public string two { get; set; }
	public string three { get; set; }
	public string four { get; set; }
	public string filetype { get; set; }
	public string five { get; set; }
	public string six { get; set; }
	public string seven { get; set; }
	public string dayflight { get; set; }
	public string flightdate { get; set; }
	public string filedate { get; set; }

	public RSOutput() {}

	public RSOutput(string[] resultlines, string fileName)
	{
		filedate = $"20{fileName.Substring(2, 6).Substring(0, 2)}-{fileName.Substring(2, 6).Substring(2, 2)}-{fileName.Substring(2, 6).Substring(4, 2)}";
		zero = resultlines[0].Substring(3, 2);
		one = resultlines[0].Substring(0, resultlines[0].IndexOf("/")).Remove(0, 5).PadLeft(4, ' ');
		two = resultlines[0].Substring(resultlines[0].LastIndexOf("AN ") + 3, resultlines[0].IndexOf("/DA") - (resultlines[0].IndexOf("/AN ") + 4)).PadLeft(5, ' ');
		three = resultlines[0].Substring(resultlines[0].LastIndexOf("DA ") + 3, 4).PadLeft(4, ' ');
		four = resultlines[0].Substring(resultlines[0].LastIndexOf("AD ") + 3, 4).PadLeft(4, ' ');
		five = resultlines[0].Substring(resultlines[0].LastIndexOf("ON ") + 3, 4).PadLeft(4, ' ');
		six = resultlines[1].Substring(7, 3);
		dayflight = resultlines[1].Substring(11, 2);
		seven = resultlines[2].Substring(4, resultlines[2].IndexOf(",") - 4);
	}

	public string WriteLine
	{
		get
		{
			return String.Format(
				"{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}"
				, zero
				, one
				, two
				, three
				, four
				, filetype
				, five
				, six
				, seven
				, flightdate
			);
		}
		private set {}
	}
}