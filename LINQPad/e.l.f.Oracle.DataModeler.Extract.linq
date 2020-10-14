<Query Kind="Program" />

void Main()
{
	var fileDirectory = @"D:\Documents\BrainSpire\OneDrive\Brainspire Solutions\Clients & Projects - e.l.f (Eyes Lips Face)\elf_sfmc\logical\entity\seg_0";
	var EmailMaster = "BFDAF078-8EC0-8CB1-53F4-10C0E75F3134";
	var Email_Sailthru_Orphaned = "BA8FA000-96A9-8E79-DE74-5384574C783B";
	var Email_OrdersDerived_SailThru = "ADB92693-3099-3740-F179-02027378122E";
	var Email_Engagement = "588A4A3E-DAF7-E4E3-48B6-C0581270CF58";
	var Email_LastEvents = "3F60850A-10E9-7AFB-1351-7FF295AA832D";
	var Orders_Payments = "C367AA05-1D05-A61A-D521-1AEF755F84E2";
	var Orders_Summary = "46796499-2F98-344F-042A-A88489685593";
	var Orders_LineItems = "87F3AEA3-1547-A58C-4782-C71351B582D4";
	
	var fileName = EmailMaster;
	
	var xE = XElement.Parse(File.ReadAllText(String.Format(@"{0}\{1}.xml",fileDirectory,fileName)));
	var TableName = xE.Attribute("name").Value;
	TableName.Dump();
	
	var columns = xE.Element("attributes").Elements("Attribute").Select(a => new{
		ColumnName = a.Attribute("name").Value,
		DataType = a.Elements("logicalDatatype").Any()
					? a.Elements("logicalDatatype").FirstOrDefault().Value
						.Replace("LOGDT024","VARCHAR")
						.Replace("LOGDT008","DATETIME")
						.Replace("LOGDT007","DATETIME")
						.Replace("LOGDT006","BIT")
						.Replace("LOGDT011","INT")
						.Replace("LOGDT026","DECIMAL")
					: "Unknown",
		Parameters = a.Element("ownDataTypeParameters").Value
	})
//	.Select(cc => String.Format("{0} {1}({2})",	
//		cc.ColumnName,
//		cc.DataType, 
//		cc.Parameters.Split(',')[0]).Replace("()",String.Empty)
//		)
	.Select(cc => new{
		cc.ColumnName,
		cc.DataType, 
		Length = cc.Parameters.Split(',')[0],
		SFMCType = cc.DataType.Contains("VARCHAR") ? "Text" :
						cc.DataType.Contains("INT") ? "Number" :
						cc.DataType.Contains("DATETIME") ? "Date" :
						cc.DataType.Contains("BIT") ? "Boolean" :
						cc.DataType.Contains("DECIMAL") ? "Decimal" :
						"Source Type Not Specified"
	})
	.Dump();
}

// Define other methods and classes here