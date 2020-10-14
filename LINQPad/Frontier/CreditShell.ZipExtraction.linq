<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.Compression.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.Compression.FileSystem.dll</Reference>
  <NuGetReference>CsvHelper</NuGetReference>
  <Namespace>CsvHelper</Namespace>
  <Namespace>CsvHelper.Configuration</Namespace>
  <Namespace>CsvHelper.Configuration.Attributes</Namespace>
  <Namespace>CsvHelper.Expressions</Namespace>
  <Namespace>CsvHelper.TypeConversion</Namespace>
  <Namespace>System.IO.Compression</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{

	foreach (FileInfo fi in new DirectoryInfo(StagingDirectory).GetFiles())
	{
		if (fi.Extension.Equals(".zip"))
		{
			var sourceZip = ZipFile.Open(fi.FullName, ZipArchiveMode.Read);
			sourceZip.ExtractToDirectory($@"{WorkingDirectory}");
		}
	}

	var CreditShellFiles = new CreditShell()
	{
		cs_CSHFX = File.ReadAllLines($@"{WorkingDirectory}\SkyLedgerReconciliationExtract_FrontierAirlines_Credit_CSHFX.csv").ExtractRows().Select(c => new CSHFX(c)).ToList(),
		cs_CSHUR_CF = File.ReadAllLines($@"{WorkingDirectory}\SkyLedgerReconciliationExtract_FrontierAirlines_Credit_CSHUR_CF.csv").ExtractRows().Select(c => new CSHUR_CF(c)).ToList(),
		cs_CSHUR_CS = File.ReadAllLines($@"{WorkingDirectory}\SkyLedgerReconciliationExtract_FrontierAirlines_Credit_CSHUR_CS.csv").ExtractRows().Select(c => new CSHUR_CS(c)).ToList(),
		cs_CSHXR = File.ReadAllLines($@"{WorkingDirectory}\SkyLedgerReconciliationExtract_FrontierAirlines_Credit_CSHXR.csv").ExtractRows().Select(c => new CSHXR(c)).ToList(),
		cs_PMTPM_CF = File.ReadAllLines($@"{WorkingDirectory}\SkyLedgerReconciliationExtract_FrontierAirlines_Credit_PMTPM_CF.csv").ExtractRows().Select(c => new PMTPM_CF(c)).ToList(),
		cs_PMTPM_CS = File.ReadAllLines($@"{WorkingDirectory}\SkyLedgerReconciliationExtract_FrontierAirlines_Credit_PMTPM_CS.csv").ExtractRows().Select(c => new PMTPM_CS(c)).ToList(),
		cs_ERExtract = File.ReadAllLines($@"{WorkingDirectory}\SkyLedgerReconciliationExtract_FrontierAirlines_ERExtract.csv").ExtractRows().Select(c => new ERExtract(c)).ToList(),
		cs_URExtract = File.ReadAllLines($@"{WorkingDirectory}\SkyLedgerReconciliationExtract_FrontierAirlines_URExtract.csv").ExtractRows().Select(c => new URExtract(c)).ToList(),
	};


	foreach (FileInfo csFile in new DirectoryInfo(WorkingDirectory).GetFiles())
	{
		csFile.Delete();
	}

	CreditShellFiles.Dump();

}

// Define other methods, classes and namespaces here
//the following fields will be passed down from the SSIS process
public String ArchiveDirectory = $@"G:\CreditShell\Archive";
public String SourceDirectory = $@"G:\CreditShell\Inbound";
public String StagingDirectory = $@"G:\CreditShell\Store\201909";

public string WorkingDirectory = $@"G:\CreditShell\Working";

public String FileName;
public String FileDate;

public class CreditShell
{
	public List<CSHFX> cs_CSHFX { get; set; }
	public List<CSHUR_CF> cs_CSHUR_CF { get; set; }
	public List<CSHUR_CS> cs_CSHUR_CS { get; set; }
	public List<CSHXR> cs_CSHXR { get; set; }
	public List<PMTPM_CF> cs_PMTPM_CF { get; set; }
	public List<PMTPM_CS> cs_PMTPM_CS { get; set; }
	public List<ERExtract> cs_ERExtract { get; set; }
	public List<URExtract> cs_URExtract { get; set; }
}


public class FileDetails
{	
	public String Name {get;set;}
	public DateTime CreationTime {get;set;}
	public DateTime LastAccessTime {get;set;}
	public DateTime LastWriteTime {get;set;}
	public IEnumerable<string[]> Contents {get;set;}
	public DataColumn[] Columns {get;set;}
}

public class CSHFX : Credit_CS {
	public CSHFX()
	{
		
	}

	public CSHFX(String[] c)
	{
		AccountPeriod = c[0].ToString();
		ProperAccountPeriod = c[1].ToString();
		CreditAccountNumber = c[2].ToString();
		CreditCenterNumber = c[3].ToString();
		DebitAccountNumber = c[4].ToString();
		DebitCenterNumber = c[5].ToString();
		AccountEvent = c[6].ToString();
		AccountType = c[7].ToString();
		AccountMappingID = c[8].ToString();
		JournalEntry = c[9].ToString();
		HostAmount = c[10].ToString();
		LocalAmount = c[11].ToString();
		HostCurrency = c[12].ToString();
		LocalCurrency = c[13].ToString();
		HostCurrencyPrecision = c[14].ToString();
		LocalCurrencyPrecision = c[15].ToString();
		ReferenceDate = c[16].ToString();
		TransactionKey = c[17].ToString();
		LedgerVersion = c[18].ToString();
		LedgerType = c[19].ToString();
		LedgerID = c[20].ToString();
		IsRevenue = c[21].ToString();
		GLExtract = c[22].ToString();
		ReferenceCode = c[23].ToString();
		SuspenseTypeDescription = c[24].ToString();
		PostingDate = c[25].ToString();
		AcctNbr = c[26].ToString();
		RefRecLoc = c[27].ToString();
		CreditCode = c[28].ToString();
		AcctType = c[29].ToString();
		TransDateTime = c[30].ToString();
		PaymentNbr = c[31].ToString();
		TransactionType = c[32].ToString();
		BookingAgent = c[33].ToString();
		Department = c[34].ToString();
		ActiveDate = c[35].ToString();
		CurrencyBase = c[36].ToString();
		Amount = c[37].ToString();
		CurrencyCode = c[38].ToString();
		CurrencyAmount = c[39].ToString();
		CreditExpDate = c[40].ToString();

	}
};
public class CSHUR_CF : Credit_CS { 
	public CSHUR_CF()
	{

	}
	public CSHUR_CF(String[] c)
	{
		AccountPeriod = c[0].ToString();
		ProperAccountPeriod = c[1].ToString();
		CreditAccountNumber = c[2].ToString();
		CreditCenterNumber = c[3].ToString();
		DebitAccountNumber = c[4].ToString();
		DebitCenterNumber = c[5].ToString();
		AccountEvent = c[6].ToString();
		AccountType = c[7].ToString();
		AccountMappingID = c[8].ToString();
		JournalEntry = c[9].ToString();
		HostAmount = c[10].ToString();
		LocalAmount = c[11].ToString();
		HostCurrency = c[12].ToString();
		LocalCurrency = c[13].ToString();
		HostCurrencyPrecision = c[14].ToString();
		LocalCurrencyPrecision = c[15].ToString();
		ReferenceDate = c[16].ToString();
		TransactionKey = c[17].ToString();
		LedgerVersion = c[18].ToString();
		LedgerType = c[19].ToString();
		LedgerID = c[20].ToString();
		IsRevenue = c[21].ToString();
		GLExtract = c[22].ToString();
		ReferenceCode = c[23].ToString();
		SuspenseTypeDescription = c[24].ToString();
		PostingDate = c[25].ToString();
		AcctNbr = c[26].ToString();
		RefRecLoc = c[27].ToString();
		CreditCode = c[28].ToString();
		AcctType = c[29].ToString();
		TransDateTime = c[30].ToString();
		PaymentNbr = c[31].ToString();
		TransactionType = c[32].ToString();
		BookingAgent = c[33].ToString();
		Department = c[34].ToString();
		ActiveDate = c[35].ToString();
		CurrencyBase = c[36].ToString();
		Amount = c[37].ToString();
		CurrencyCode = c[38].ToString();
		CurrencyAmount = c[39].ToString();
		CreditExpDate = c[40].ToString();

	}
};
public class CSHUR_CS : Credit_CS { 
	public CSHUR_CS()
	{

	}
	public CSHUR_CS(String[] c)
	{
		AccountPeriod = c[0].ToString();
		ProperAccountPeriod = c[1].ToString();
		CreditAccountNumber = c[2].ToString();
		CreditCenterNumber = c[3].ToString();
		DebitAccountNumber = c[4].ToString();
		DebitCenterNumber = c[5].ToString();
		AccountEvent = c[6].ToString();
		AccountType = c[7].ToString();
		AccountMappingID = c[8].ToString();
		JournalEntry = c[9].ToString();
		HostAmount = c[10].ToString();
		LocalAmount = c[11].ToString();
		HostCurrency = c[12].ToString();
		LocalCurrency = c[13].ToString();
		HostCurrencyPrecision = c[14].ToString();
		LocalCurrencyPrecision = c[15].ToString();
		ReferenceDate = c[16].ToString();
		TransactionKey = c[17].ToString();
		LedgerVersion = c[18].ToString();
		LedgerType = c[19].ToString();
		LedgerID = c[20].ToString();
		IsRevenue = c[21].ToString();
		GLExtract = c[22].ToString();
		ReferenceCode = c[23].ToString();
		SuspenseTypeDescription = c[24].ToString();
		PostingDate = c[25].ToString();
		AcctNbr = c[26].ToString();
		RefRecLoc = c[27].ToString();
		CreditCode = c[28].ToString();
		AcctType = c[29].ToString();
		TransDateTime = c[30].ToString();
		PaymentNbr = c[31].ToString();
		TransactionType = c[32].ToString();
		BookingAgent = c[33].ToString();
		Department = c[34].ToString();
		ActiveDate = c[35].ToString();
		CurrencyBase = c[36].ToString();
		Amount = c[37].ToString();
		CurrencyCode = c[38].ToString();
		CurrencyAmount = c[39].ToString();
		CreditExpDate = c[40].ToString();

	}
};
public class CSHXR : Credit_CS { 
	public CSHXR()
	{
		
	}

	public CSHXR(String[] c)
	{
		AccountPeriod = c[0].ToString();
		ProperAccountPeriod = c[1].ToString();
		CreditAccountNumber = c[2].ToString();
		CreditCenterNumber = c[3].ToString();
		DebitAccountNumber = c[4].ToString();
		DebitCenterNumber = c[5].ToString();
		AccountEvent = c[6].ToString();
		AccountType = c[7].ToString();
		AccountMappingID = c[8].ToString();
		JournalEntry = c[9].ToString();
		HostAmount = c[10].ToString();
		LocalAmount = c[11].ToString();
		HostCurrency = c[12].ToString();
		LocalCurrency = c[13].ToString();
		HostCurrencyPrecision = c[14].ToString();
		LocalCurrencyPrecision = c[15].ToString();
		ReferenceDate = c[16].ToString();
		TransactionKey = c[17].ToString();
		LedgerVersion = c[18].ToString();
		LedgerType = c[19].ToString();
		LedgerID = c[20].ToString();
		IsRevenue = c[21].ToString();
		GLExtract = c[22].ToString();
		ReferenceCode = c[23].ToString();
		SuspenseTypeDescription = c[24].ToString();
		PostingDate = c[25].ToString();
		AcctNbr = c[26].ToString();
		RefRecLoc = c[27].ToString();
		CreditCode = c[28].ToString();
		AcctType = c[29].ToString();
		TransDateTime = c[30].ToString();
		PaymentNbr = c[31].ToString();
		TransactionType = c[32].ToString();
		BookingAgent = c[33].ToString();
		Department = c[34].ToString();
		ActiveDate = c[35].ToString();
		CurrencyBase = c[36].ToString();
		Amount = c[37].ToString();
		CurrencyCode = c[38].ToString();
		CurrencyAmount = c[39].ToString();
		CreditExpDate = c[40].ToString();

	}
};
public class PMTPM_CF : Credit_PMTPM { 
public PMTPM_CF()
{
	
}
public PMTPM_CF(String[] c)
{
		AccountPeriod = c[0].ToString();
		ProperAccountPeriod = c[1].ToString();
		CreditAccountNumber = c[2].ToString();
		CreditCenterNumber = c[3].ToString();
		DebitAccountNumber = c[4].ToString();
		DebitCenterNumber = c[5].ToString();
		AccountEvent = c[6].ToString();
		AccountType = c[7].ToString();
		AccountMappingID = c[8].ToString();
		JournalEntry = c[9].ToString();
		HostAmount = c[10].ToString();
		LocalAmount = c[11].ToString();
		HostCurrency = c[12].ToString();
		LocalCurrency = c[13].ToString();
		HostCurrencyPrecision = c[14].ToString();
		LocalCurrencyPrecision = c[15].ToString();
		ReferenceDate = c[16].ToString();
		TransactionKey = c[17].ToString();
		LedgerVersion = c[18].ToString();
		LedgerType = c[19].ToString();
		LedgerID = c[20].ToString();
		IsRevenue = c[21].ToString();
		GLExtract = c[22].ToString();
		ReferenceCode = c[23].ToString();
		SuspenseTypeDescription = c[24].ToString();
		PostingDate = c[25].ToString();
		PaymentDate = c[26].ToString();
		BookingAgent = c[27].ToString();
		PaymentGroup = c[28].ToString();
		TypeCode = c[29].ToString();
		PaymentAmount = c[30].ToString();
		PaymentLocalAmount = c[31].ToString();
		LocalCurrency_1 = c[32].ToString();
		BatchGroup = c[33].ToString();
		ModifiedDate = c[34].ToString();
	}
};
public class PMTPM_CS : Credit_PMTPM { 
public PMTPM_CS()
{
	
}

	public PMTPM_CS(String[] c)
	{
		AccountPeriod = c[0].ToString();
		ProperAccountPeriod = c[1].ToString();
		CreditAccountNumber = c[2].ToString();
		CreditCenterNumber = c[3].ToString();
		DebitAccountNumber = c[4].ToString();
		DebitCenterNumber = c[5].ToString();
		AccountEvent = c[6].ToString();
		AccountType = c[7].ToString();
		AccountMappingID = c[8].ToString();
		JournalEntry = c[9].ToString();
		HostAmount = c[10].ToString();
		LocalAmount = c[11].ToString();
		HostCurrency = c[12].ToString();
		LocalCurrency = c[13].ToString();
		HostCurrencyPrecision = c[14].ToString();
		LocalCurrencyPrecision = c[15].ToString();
		ReferenceDate = c[16].ToString();
		TransactionKey = c[17].ToString();
		LedgerVersion = c[18].ToString();
		LedgerType = c[19].ToString();
		LedgerID = c[20].ToString();
		IsRevenue = c[21].ToString();
		GLExtract = c[22].ToString();
		ReferenceCode = c[23].ToString();
		SuspenseTypeDescription = c[24].ToString();
		PostingDate = c[25].ToString();
		PaymentDate = c[26].ToString();
		BookingAgent = c[27].ToString();
		PaymentGroup = c[28].ToString();
		TypeCode = c[29].ToString();
		PaymentAmount = c[30].ToString();
		PaymentLocalAmount = c[31].ToString();
		LocalCurrency_1 = c[32].ToString();
		BatchGroup = c[33].ToString();
		ModifiedDate = c[34].ToString();
	}
};
public class ERExtract : R_Extract {
	public ERExtract()
	{
		
	}
	
	public ERExtract(String[] c)
	{
		LedgerKey = c[0].ToString();
		FlightDate = c[1].ToString();
		GDSRecLoc = c[2].ToString();
		BookingDate = c[3].ToString();
		LegOrigin = c[4].ToString();
		LegDest = c[5].ToString();
		SegmentOrigin = c[6].ToString();
		SegmentDest = c[7].ToString();
		FareClass = c[8].ToString();
		ModifiedAgent = c[9].ToString();
		BookingAgent = c[10].ToString();
		AirlineCode = c[11].ToString();
		ScheduledDeparture = c[12].ToString();
		FlightNumber = c[13].ToString();
		HostAmount = c[14].ToString();
	}
};
public class URExtract : R_Extract {
public URExtract()
{
	
}

	public URExtract(String[] c)
	{
		LedgerKey = c[0].ToString();
		FlightDate = c[1].ToString();
		GDSRecLoc = c[2].ToString();
		BookingDate = c[3].ToString();
		LegOrigin = c[4].ToString();
		LegDest = c[5].ToString();
		SegmentOrigin = c[6].ToString();
		SegmentDest = c[7].ToString();
		FareClass = c[8].ToString();
		ModifiedAgent = c[9].ToString();
		BookingAgent = c[10].ToString();
		AirlineCode = c[11].ToString();
		ScheduledDeparture = c[12].ToString();
		FlightNumber = c[13].ToString();
		HostAmount = c[14].ToString();
	}
};



public class Credit_CS
{
	public String AccountPeriod { get; set; }
	public String ProperAccountPeriod { get; set; }
	public String CreditAccountNumber { get; set; }
	public String CreditCenterNumber { get; set; }
	public String DebitAccountNumber { get; set; }
	public String DebitCenterNumber { get; set; }
	public String AccountEvent { get; set; }
	public String AccountType { get; set; }
	public String AccountMappingID { get; set; }
	public String JournalEntry { get; set; }
	public String HostAmount { get; set; }
	public String LocalAmount { get; set; }
	public String HostCurrency { get; set; }
	public String LocalCurrency { get; set; }
	public String HostCurrencyPrecision { get; set; }
	public String LocalCurrencyPrecision { get; set; }
	public String ReferenceDate { get; set; }
	public String TransactionKey { get; set; }
	public String LedgerVersion { get; set; }
	public String LedgerType { get; set; }
	public String LedgerID { get; set; }
	public String IsRevenue { get; set; }
	public String GLExtract { get; set; }
	public String ReferenceCode { get; set; }
	public String SuspenseTypeDescription { get; set; }
	public String PostingDate { get; set; }
	public String AcctNbr { get; set; }
	public String RefRecLoc { get; set; }
	public String CreditCode { get; set; }
	public String AcctType { get; set; }
	public String TransDateTime { get; set; }
	public String PaymentNbr { get; set; }
	public String TransactionType { get; set; }
	public String BookingAgent { get; set; }
	public String Department { get; set; }
	public String ActiveDate { get; set; }
	public String CurrencyBase { get; set; }
	public String Amount { get; set; }
	public String CurrencyCode { get; set; }
	public String CurrencyAmount { get; set; }
	public String CreditExpDate { get; set; }
}
public class Credit_PMTPM
{
	public String AccountPeriod { get; set; }
	public String ProperAccountPeriod { get; set; }
	public String CreditAccountNumber { get; set; }
	public String CreditCenterNumber { get; set; }
	public String DebitAccountNumber { get; set; }
	public String DebitCenterNumber { get; set; }
	public String AccountEvent { get; set; }
	public String AccountType { get; set; }
	public String AccountMappingID { get; set; }
	public String JournalEntry { get; set; }
	public String HostAmount { get; set; }
	public String LocalAmount { get; set; }
	public String HostCurrency { get; set; }
	public String LocalCurrency { get; set; }
	public String HostCurrencyPrecision { get; set; }
	public String LocalCurrencyPrecision { get; set; }
	public String ReferenceDate { get; set; }
	public String TransactionKey { get; set; }
	public String LedgerVersion { get; set; }
	public String LedgerType { get; set; }
	public String LedgerID { get; set; }
	public String IsRevenue { get; set; }
	public String GLExtract { get; set; }
	public String ReferenceCode { get; set; }
	public String SuspenseTypeDescription { get; set; }
	public String PostingDate { get; set; }
	public String PaymentDate { get; set; }
	public String BookingAgent { get; set; }
	public String PaymentGroup { get; set; }
	public String TypeCode { get; set; }
	public String PaymentAmount { get; set; }
	public String PaymentLocalAmount { get; set; }
	public String LocalCurrency_1 { get; set; }
	public String BatchGroup { get; set; }
	public String ModifiedDate { get; set; }
}

public class R_Extract
{
	public String LedgerKey { get; set; }
	public String FlightDate { get; set; }
	public String GDSRecLoc { get; set; }
	public String BookingDate { get; set; }
	public String LegOrigin { get; set; }
	public String LegDest { get; set; }
	public String SegmentOrigin { get; set; }
	public String SegmentDest { get; set; }
	public String FareClass { get; set; }
	public String ModifiedAgent { get; set; }
	public String BookingAgent { get; set; }
	public String AirlineCode { get; set; }
	public String ScheduledDeparture { get; set; }
	public String FlightNumber { get; set; }
	public String HostAmount { get; set; }
}

public static class Extension
{
	public static DataTable ToDataTable<T>(this IEnumerable<T> varlist)
	{
		DataTable dtReturn = new DataTable();

		// column names 
		PropertyInfo[] oProps = null;

		if (varlist == null) return dtReturn;

		foreach (T rec in varlist)
		{
			// Use reflection to get property names, to create table, Only first time, others will follow 
			if (oProps == null)
			{
				oProps = ((Type)rec.GetType()).GetProperties();
				foreach (PropertyInfo pi in oProps)
				{
					Type colType = pi.PropertyType;

					if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
					{
						colType = colType.GetGenericArguments()[0];
					}

					dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
				}
			}

			DataRow dr = dtReturn.NewRow();

			foreach (PropertyInfo pi in oProps)
			{
				dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
				(rec, null);
			}

			dtReturn.Rows.Add(dr);
		}
		return dtReturn;
	}

	public static List<String[]> ExtractRows<T>(this IEnumerable<T> varlist)
	{
		List<String[]> contents = new List<String[]>();
		contents.AddRange(varlist.Where(li => !String.IsNullOrEmpty(li.ToString()))
			.Except(
				varlist.Where(li => Regex.IsMatch(li.ToString(), @"\([0-9]{1,10}\srows affected\)"))
				)
			.Where(li => Regex.Matches(li.ToString(), @"\w,").Cast<Match>().Any()).Select(li => li.ToString().Split(',')).Skip(1));


		return contents;
		
	}

}