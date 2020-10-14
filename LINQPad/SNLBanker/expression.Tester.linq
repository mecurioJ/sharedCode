<Query Kind="Program" />

void Main()
{
//COALESCE
	var expressionTester = Expressions[90];
	
	var WhiteSpacePhrase = @"\S*";
	var ParensPhrase = @"\("+WhiteSpacePhrase+@"\)";

//Aggregate Rule

	expressionTester.Dump();
	String AggregateRule = @"COUNT"+
			WhiteSpacePhrase+
			@"(\(\*\))\s*\w*";
	//@"(COUNT|MIN|MAX) \((\w*|\d*)\)";
	String ArithmeticRule = @"((\w*|\d*)\s*(\+|-|\*|/|%)\s*(\w*|\d*))";
	String ComparisonRule = @"((\w*|\d*)\s*(\=<|>|*|<>)\s*(\w*|\d*))";
	String CaseRule = @"((CASE\s)|(WHEN\s*)|\w*|(THEN\s*)|(ELSE\s*)|(END\s*))";
	String CoalesceRule = @"(COALESCE)\(";
	
	
	var t = 
	System.Text.RegularExpressions.Regex.Matches(expressionTester,
		//AggregatePattern + "|" + CasePattern,
		//GreaterThanLessThan,
		CaseRule,
		RegexOptions.IgnoreCase
			)
			.Cast<Match>().Where(m => !String.IsNullOrEmpty(m.Value)).Select(v => v.Value)
			;
			
	t.Dump();	
}

// Define other methods and classes here


public List<String> Expressions = new List<String>(){
"-(CONVERT(int, ActualDate, 112))", 
"-(Month)", 
"-(Quarter)", 
"-(Year)", 
"Active_Principal_Balance*Ceiling_Rate", 
"Active_Principal_Balance*Floor_Rate", 
"case  when  DistancetoBranch IS NULL then 'Unknown' when  DistancetoBranch  < 6 then '0-5'  when  DistancetoBranch  < 11 then '6-10'  when  DistancetoBranch  < 16 then '11-15'  when  DistancetoBranch  < 21 then '16-20'  when  DistancetoBranch  < 26 then '21-25'  else '25+' end", 
"case  when  DistancetoBranch IS NULL then 'Unknown' when  DistancetoBranch  < 6 then '0-5'  when  DistancetoBranch  < 11 then '6-10'  when  DistancetoBranch  < 16 then '11-15'  when  DistancetoBranch  < 21 then '16-20'  when  DistancetoBranch  < 26 then '21-25'  else '25+' end", 
"case  when  DistancetoBranch IS NULL then 'Unknown' when  DistancetoBranch  < 6 then '0-5'  when  DistancetoBranch  < 11 then '6-10'  when  DistancetoBranch  < 16 then '11-15'  when  DistancetoBranch  < 21 then '16-20'  when  DistancetoBranch  < 26 then '21-25'  else '25+' end", 
"case  when  DistancetoBranch IS NULL then 'Unknown' when  DistancetoBranch  < 6 then '0-5'  when  DistancetoBranch  < 11 then '6-10'  when  DistancetoBranch  < 16 then '11-15'  when  DistancetoBranch  < 21 then '16-20'  when  DistancetoBranch  < 26 then '21-25'  else '25+' end", 
"CASE  WHEN AcctStatusCode IS NULL THEN AcctStatusDesc ELSE CAST(AcctStatusCode AS varchar(10))  + ': ' + isnull(AcctStatusDesc, '') END", 
"CASE  WHEN Bank_Number IS NULL THEN Bank_Name ELSE CAST(Bank_Number AS varchar(10))  + ': ' + Bank_Name END", 
"CASE  WHEN Bank_Number IS NULL THEN Bank_Name ELSE CAST(Bank_Number AS varchar(10))  + ': ' + Bank_Name END", 
"CASE  WHEN Bank_Number IS NULL THEN Bank_Name ELSE CAST(Bank_Number AS varchar(10))  + ': ' + Bank_Name END", 
"CASE  WHEN Bank_Number IS NULL THEN Bank_Name ELSE CAST(Bank_Number AS varchar(10))  + ': ' + Bank_Name END", 
"CASE  WHEN Bank_Number IS NULL THEN Bank_Name ELSE CAST(Bank_Number AS varchar(10))  + ': ' + Bank_Name END", 
"CASE  WHEN Bank_Number IS NULL THEN Bank_Name ELSE CAST(Bank_Number AS varchar(10))  + ': ' + Bank_Name END", 
"CASE  WHEN Branch_Number IS NULL THEN BranchName ELSE CAST(Branch_Number AS varchar(10))  + ': ' + BranchName END", 
"CASE  WHEN Branch_Number IS NULL THEN BranchName ELSE CAST(Branch_Number AS varchar(10))  + ': ' + BranchName END", 
"CASE  WHEN Branch_Number IS NULL THEN BranchName ELSE CAST(Branch_Number AS varchar(10))  + ': ' + BranchName END", 
"CASE  WHEN Branch_Number IS NULL THEN BranchName ELSE CAST(Branch_Number AS varchar(10))  + ': ' + BranchName END", 
"CASE  WHEN Branch_Number IS NULL THEN BranchName ELSE CAST(Branch_Number AS varchar(10))  + ': ' + BranchName END", 
"CASE  WHEN Branch_Number IS NULL THEN BranchName ELSE CAST(Branch_Number AS varchar(10))  + ': ' + BranchName END", 
"CASE  WHEN CallReportCode IS NULL THEN CallReportCodeDesc WHEN CallReportCodeDesc = '' THEN CallReportCode  ELSE CallReportCode + ': ' + ISNULL(CallReportCodeDesc, '') END", 
"CASE  WHEN Class_Code IS NULL THEN Class_Name ELSE  Class_Code + ': ' + Class_Name END", 
"CASE  WHEN Closed_Acct_Flag IS NULL THEN AcctDescription ELSE CAST(Closed_Acct_Flag AS varchar(10))  + ': ' + AcctDescription END", 
"CASE  WHEN Closed_Acct_Flag IS NULL THEN AcctDescription ELSE CAST(Closed_Acct_Flag AS varchar(10))  + ': ' + AcctDescription END", 
"CASE  WHEN ClosedReason_Code IS NULL THEN ClosedReason_Desc WHEN ClosedReason_Code = '' THEN CAST(ClosedReason_Code AS varchar(10)) ELSE CAST(ClosedReason_Code AS varchar(10)) + ': ' + ClosedReason_Desc END", 
"CASE  WHEN ClosedReason_Code IS NULL THEN ClosedReason_Desc WHEN ClosedReason_Code = '' THEN CAST(ClosedReason_Code AS varchar(10)) ELSE CAST(ClosedReason_Code AS varchar(10)) + ': ' + ClosedReason_Desc END", 
"CASE  WHEN Collateral_Code IS NULL THEN Description WHEN Description = '' THEN CAST(Collateral_Code AS varchar(10)) ELSE CAST(Collateral_Code AS varchar(10)) + ': ' + Description END", 
"CASE  WHEN DateDiff(day, getDate(), MaturityDate) between 0 and 30 THEN 1   WHEN DateDiff(day, getDate(), MaturityDate) between 31 and 60 THEN 2   WHEN DateDiff(day, getDate(), MaturityDate) between 61 and 90 THEN 3   WHEN DateDiff(day, getDate(), MaturityDate) between 91 and 180 THEN 4   WHEN DateDiff(day, getDate(), MaturityDate) between 181 and 365 THEN 5 ELSE -1 END", 
"CASE  WHEN DateDiff(yy, Dateofbirth, getdate()) IS NULL THEN 0 WHEN DateDiff(yy, Dateofbirth, getdate()) between 0 and 20 THEN 1 WHEN DateDiff(yy, Dateofbirth, getdate()) between 21 and 40 THEN 2 WHEN DateDiff(yy, Dateofbirth, getdate()) between 41 and 60 THEN 3 WHEN DateDiff(yy, Dateofbirth, getdate()) between 61 and 80 THEN 4 ELSE 5 END", 
"CASE  WHEN DateDiff(yy, Dateofbirth, getdate()) IS NULL THEN 0 WHEN DateDiff(yy, Dateofbirth, getdate()) between 0 and 20 THEN 1 WHEN DateDiff(yy, Dateofbirth, getdate()) between 21 and 40 THEN 2 WHEN DateDiff(yy, Dateofbirth, getdate()) between 41 and 60 THEN 3 WHEN DateDiff(yy, Dateofbirth, getdate()) between 61 and 80 THEN 4 ELSE 5 END", 
"CASE  WHEN DateDiff(yy, Dateofbirth, getdate()) IS NULL THEN 0 WHEN DateDiff(yy, Dateofbirth, getdate()) between 1 and 20 THEN 1 WHEN DateDiff(yy, Dateofbirth, getdate()) between 21 and 40 THEN 2 WHEN DateDiff(yy, Dateofbirth, getdate()) between 41 and 60 THEN 3 WHEN DateDiff(yy, Dateofbirth, getdate()) between 61 and 80 THEN 4 ELSE 5 END", 
"CASE  WHEN DateDiff(yy, Dateofbirth, getdate()) IS NULL THEN 0 WHEN DateDiff(yy, Dateofbirth, getdate()) between 1 and 20 THEN 1 WHEN DateDiff(yy, Dateofbirth, getdate()) between 21 and 40 THEN 2 WHEN DateDiff(yy, Dateofbirth, getdate()) between 41 and 60 THEN 3 WHEN DateDiff(yy, Dateofbirth, getdate()) between 61 and 80 THEN 4 ELSE 5 END", 
"CASE  WHEN DateDiff(yy, Dateofbirth, getdate()) IS NULL THEN 'Unknown' WHEN DateDiff(yy, Dateofbirth, getdate()) between 0 and 20 THEN '0-20' WHEN DateDiff(yy, Dateofbirth, getdate()) between 21 and 40 THEN '21-40' WHEN DateDiff(yy, Dateofbirth, getdate()) between 41 and 60 THEN '41-60' WHEN DateDiff(yy, Dateofbirth, getdate()) between 61 and 80 THEN '61-80' ELSE '81+' END", 
"CASE  WHEN DateDiff(yy, Dateofbirth, getdate()) IS NULL THEN 'Unknown' WHEN DateDiff(yy, Dateofbirth, getdate()) between 0 and 20 THEN '0-20' WHEN DateDiff(yy, Dateofbirth, getdate()) between 21 and 40 THEN '21-40' WHEN DateDiff(yy, Dateofbirth, getdate()) between 41 and 60 THEN '41-60' WHEN DateDiff(yy, Dateofbirth, getdate()) between 61 and 80 THEN '61-80' ELSE '81+' END", 
"CASE  WHEN DateDiff(yy, Dateofbirth, getdate()) IS NULL THEN 'Unknown' WHEN DateDiff(yy, Dateofbirth, getdate()) between 1 and 20 THEN '1-20' WHEN DateDiff(yy, Dateofbirth, getdate()) between 21 and 40 THEN '21-40' WHEN DateDiff(yy, Dateofbirth, getdate()) between 41 and 60 THEN '41-60' WHEN DateDiff(yy, Dateofbirth, getdate()) between 61 and 80 THEN '61-80' ELSE '81+' END", 
"CASE  WHEN DateDiff(yy, Dateofbirth, getdate()) IS NULL THEN 'Unknown' WHEN DateDiff(yy, Dateofbirth, getdate()) between 1 and 20 THEN '1-20' WHEN DateDiff(yy, Dateofbirth, getdate()) between 21 and 40 THEN '21-40' WHEN DateDiff(yy, Dateofbirth, getdate()) between 41 and 60 THEN '41-60' WHEN DateDiff(yy, Dateofbirth, getdate()) between 61 and 80 THEN '61-80' ELSE '81+' END", 
"CASE  WHEN DepartmentCode IS NULL THEN DepartmentCodeDesc ELSE DepartmentCode + ': ' + DepartmentCodeDesc END", 
"case  when DistancetoBranch  IS NULL then 99 when  DistancetoBranch  < 6 then 0 when  DistancetoBranch  < 11 then 1 when  DistancetoBranch  < 16 then 2 when  DistancetoBranch  < 21 then 3 when  DistancetoBranch  < 26 then 4 else 5 end", 
"case  when DistancetoBranch  IS NULL then 99 when  DistancetoBranch  < 6 then 0 when  DistancetoBranch  < 11 then 1 when  DistancetoBranch  < 16 then 2 when  DistancetoBranch  < 21 then 3 when  DistancetoBranch  < 26 then 4 else 5 end", 
"case  when DistancetoBranch  IS NULL then 99 when  DistancetoBranch  < 6 then 0 when  DistancetoBranch  < 11 then 1 when  DistancetoBranch  < 16 then 2 when  DistancetoBranch  < 21 then 3 when  DistancetoBranch  < 26 then 4 else 5 end", 
"case  when DistancetoBranch  IS NULL then 99 when  DistancetoBranch  < 6 then 0 when  DistancetoBranch  < 11 then 1 when  DistancetoBranch  < 16 then 2 when  DistancetoBranch  < 21 then 3 when  DistancetoBranch  < 26 then 4 else 5 end", 
"CASE  WHEN FICOScore <= 640 THEN '0-640' WHEN FICOScore between 641 and 679 THEN '641-679' WHEN FICOScore >= 680 THEN  '680+'  ELSE 'Unknown' END", 
"CASE  WHEN FICOScore <= 640 THEN '0-640' WHEN FICOScore between 641 and 679 THEN '641-679' WHEN FICOScore >= 680 THEN  '680+'  ELSE 'Unknown' END", 
"CASE  WHEN FICOScore <= 640 THEN '0-640' WHEN FICOScore between 641 and 679 THEN '641-679' WHEN FICOScore >= 680 THEN  '680+'  ELSE 'Unknown' END", 
"CASE  WHEN FICOScore <= 640 THEN '0-640' WHEN FICOScore between 641 and 679 THEN '641-679' WHEN FICOScore >= 680 THEN  '680+'  ELSE 'Unknown' END", 
"CASE  WHEN GL_Category_Desc = 'I' THEN 'Income' WHEN GL_Category_Desc = 'A' THEN 'Assets' WHEN GL_Category_Desc = 'C' THEN 'Capital' WHEN GL_Category_Desc = 'L' THEN 'Liability' WHEN GL_Category_Desc = 'E' THEN 'Expense' ELSE 'N/A' END", 
"CASE  WHEN IRAPlan_Code IS NULL THEN IRAPlan_Desc WHEN IRAPlan_Code = '' THEN CAST(IRAPlan_Code AS varchar(10)) ELSE CAST(IRAPlan_Code AS varchar(10)) + ': ' + IRAPlan_Desc END", 
"CASE  WHEN Loan_Rate_Code IS NULL THEN Loan_Rate_Desc ELSE CAST(Loan_Rate_Code AS varchar(10))  + ': ' + Loan_Rate_Desc END", 
"CASE  WHEN Loan_Rate_Code IS NULL THEN Loan_Rate_Desc ELSE CAST(Loan_Rate_Code AS varchar(10))  + ': ' + Loan_Rate_Desc END", 
"CASE  WHEN Loan_Rate_Code IS NULL THEN Loan_Rate_Desc ELSE CAST(Loan_Rate_Code AS varchar(10))  + ': ' + Loan_Rate_Desc END", 
"CASE  WHEN Loan_Rating_Code_1 IS NULL THEN Risk WHEN Risk = '' THEN CAST(Loan_Rating_Code_1 AS varchar(10)) ELSE CAST(Loan_Rating_Code_1 AS varchar(10)) + ': ' + Risk END", 
"CASE  WHEN NAICSCode IS NULL THEN NAICSCodeDesc ELSE CAST(NAICSCode AS varchar(10))  + ': ' + NAICSCodeDesc END", 
"CASE  WHEN NAICSCode IS NULL THEN NAICSCodeDesc ELSE CAST(NAICSCode AS varchar(10))  + ': ' + NAICSCodeDesc END", 
"CASE  WHEN NAICSCode IS NULL THEN NAICSCodeDesc ELSE CAST(NAICSCode AS varchar(10))  + ': ' + NAICSCodeDesc END", 
"CASE  WHEN NAICSCode IS NULL THEN NAICSCodeDesc ELSE CAST(NAICSCode AS varchar(10))  + ': ' + NAICSCodeDesc END", 
"CASE  WHEN NAICSCode2Digit IS NULL THEN NAICSCodeDesc2Digit ELSE CAST(NAICSCode2Digit AS varchar(10))  + ': ' + NAICSCodeDesc2Digit END", 
"CASE  WHEN NAICSCode2Digit IS NULL THEN NAICSCodeDesc2Digit ELSE CAST(NAICSCode2Digit AS varchar(10))  + ': ' + NAICSCodeDesc2Digit END", 
"CASE  WHEN NAICSCode2Digit IS NULL THEN NAICSCodeDesc2Digit ELSE CAST(NAICSCode2Digit AS varchar(10))  + ': ' + NAICSCodeDesc2Digit END", 
"CASE  WHEN NAICSCode2Digit IS NULL THEN NAICSCodeDesc2Digit ELSE CAST(NAICSCode2Digit AS varchar(10))  + ': ' + NAICSCodeDesc2Digit END", 
"CASE  WHEN OccupancyCode IS NULL THEN OccupancyDesc ELSE OccupancyCode  + ': ' + OccupancyDesc END", 
"CASE  WHEN Orig_Direct_Indirect_Code  IS NULL THEN Orig_Direct_Indirect_Desc ELSE Orig_Direct_Indirect_Code + ': ' + Orig_Direct_Indirect_Desc END", 
"CASE  WHEN Orig_Note_Amount > 0 THEN Active_Principal_Balance/Orig_Note_Amount ELSE 0 END", 
"CASE  WHEN Product_Number IS NULL THEN ProductName ELSE CAST(Product_Number AS varchar(10))  + ': ' + isnull(ProductName, '') END", 
"CASE  WHEN Product_Number IS NULL THEN ProductName ELSE CAST(Product_Number AS varchar(10))  + ': ' + isnull(ProductName, '') END", 
"CASE  WHEN ProductClassCode IS NULL THEN ProductClassDesc ELSE ProductClassCode + ': ' + isnull(ProductClassDesc, '') END", 
"CASE  WHEN Purpose_Code IS NULL THEN Purpose WHEN Purpose = '' THEN CAST(Purpose_Code AS varchar(10)) ELSE CAST(Purpose_Code AS varchar(10)) + ': ' + Purpose END", 
"CASE  WHEN Source_Placed_Code IS NULL THEN Source_Placed_Desc WHEN Source_Placed_Desc = '' THEN CAST(Source_Placed_Code  AS varchar(10))  ELSE CAST(Source_Placed_Code  AS varchar(10)) + ': ' + Source_Placed_Desc END", 
"CASE  WHEN TranCode IS NULL THEN TranCodeDesc WHEN TranCodeDesc IS NULL THEN TranCode ELSE TranCode  + ': ' + TranCodeDesc END", 
"CASE  WHEN TranCode IS NULL THEN TranCodeDesc WHEN TranCodeDesc IS NULL THEN TranCode ELSE TranCode  + ': ' + TranCodeDesc END", 
"CASE  WHEN TranSource IS NULL THEN TranSourceDesc WHEN TranSourceDesc IS NULL THEN TranSource COLLATE DATABASE_DEFAULT ELSE TranSource COLLATE DATABASE_DEFAULT  + ': ' + TranSourceDesc END", 
"CASE  WHEN TranSource IS NULL THEN TranSourceDesc WHEN TranSourceDesc IS NULL THEN TranSource COLLATE DATABASE_DEFAULT ELSE TranSource COLLATE DATABASE_DEFAULT  + ': ' + TranSourceDesc END", 
"CASE  WHEN WaivedFeeCode IS NULL THEN WaivedFeeDesc WHEN WaivedFeeDesc IS NULL THEN WaivedFeeCode ELSE WaivedFeeCode  + ': ' + WaivedFeeDesc END", 
"CASE   WHEN ClassDesc LIKE '%*%'   THEN 'Personal'    WHEN ClassDesc NOT LIKE  '%*%'   THEN 'Non-Personal'    ELSE 'Unknown' END", 
"CASE   WHEN ClassDesc LIKE '%*%'   THEN 'Personal'    WHEN ClassDesc NOT LIKE  '%*%'   THEN 'Non-Personal'    ELSE 'Unknown' END", 
"CASE   WHEN ClassDesc LIKE '%*%'   THEN 'Personal'    WHEN ClassDesc NOT LIKE  '%*%'   THEN 'Non-Personal'    ELSE 'Unknown' END", 
"CASE   WHEN ClassDesc LIKE '%*%'   THEN 'Personal'    WHEN ClassDesc NOT LIKE  '%*%'   THEN 'Non-Personal'    ELSE 'Unknown' END", 
"CASE GL_Type  WHEN 'B' THEN 'Balance Sheet' WHEN 'I' THEN 'Income Statement' ELSE 'N/A' END", 
"CASE Product_Count WHEN 0 THEN 'Non-Primary Account Owners' WHEN 1 THEN '1 Product' WHEN 2 THEN '2 Products'  ELSE '3+ Products'  END", 
"CASE Product_Count WHEN 0 THEN 'Non-Primary Account Owners' WHEN 1 THEN '1 Product' WHEN 2 THEN '2 Products'  ELSE '3+ Products'  END", 
"CASE WHEN     CASE     WHEN Orig_Note_Amount > 0    THEN    Active_Principal_Balance/Orig_Note_Amount    ELSE 0    END > 0.75 THEN 1 ELSE 0 END", 
"CASE WHEN    DaysPastDue   between 0 and 29   and AccountStatusID <> 16    and Orig_Direct_Indirect_Code not in ('P', 'S')   THEN   Active_Principal_Balance END", 
"CASE WHEN    DaysPastDue BETWEEN 30 and 89   AND AccountStatusID <> 16   AND Orig_Direct_Indirect_Code not in ('P', 'S')   THEN   Active_Principal_Balance END", 
"CASE WHEN   AccountStatusID = 16 OR DaysPastDue > 89  THEN    isnull(Active_Principal_Balance, 0) END", 
"CASE WHEN   DaysPastDue > 89   and AccountStatusID <> 16   and Orig_Direct_Indirect_Code not in ('P', 'S')   THEN   Active_Principal_Balance END", 
"CASE WHEN    RiskRating >= 6 THEN    isnull(Active_Principal_Balance, 0)  ELSE   0 END", 
"CASE WHEN  AccountStatusID = 16  THEN   isnull(Active_Principal_Balance, 0) END", 
"CASE WHEN CostCenter > 0 THEN  cast(CostCenter as varchar(10)) + ': ' + CostCenterDesc ELSE CostCenterDesc END", 
"CASE WHEN GL_Number_Display IS NOT NULL THEN LTRIM(replace(GL_Number_Name, '*', '')) + ' (' + cast(GL_Number_Display AS varchar(10)) + ')'  ELSE  LTRIM(replace(GL_Number_Name, '*', ''))  END", 
"CASE WHEN TranAmount < 10000 THEN 1 WHEN TranAmount BETWEEN 10000 and 24999.99 THEN 2 WHEN TranAmount BETWEEN 25000 and 49999.99 THEN 3 ELSE 4 END", 
"CAST(  CASE  WHEN  AccountStatusID = 16  THEN 5  WHEN DaysPastDue BETWEEN 0 and 29   THEN  1  WHEN DaysPastDue BETWEEN 30 and 59    THEN 2  WHEN DaysPastDue BETWEEN 60 and 89   THEN  3  WHEN  DaysPastDue > 89   THEN 4  ELSE 6  END as smallint)", 
"cast(isnull(SegmentCode, '') as varchar(10)) + ': ' + isnull(SegmentName, '')", 
"CAST(rtrim(Resp_Code) as varchar(20)) + ': ' + Resp_Name", 
"CAST(Year AS char(4)) + ' - ' + MonthName", 
"CAST(Year AS char(4)) + ' - ' + MonthName", 
"CAST(Year AS char(4)) + ' - ' + MonthName", 
"CAST(Year AS char(4)) + ' - ' + 'Q' + CAST(Quarter AS varchar(10))", 
"CAST(Year AS char(4)) + ' - ' + 'Q' + CAST(Quarter AS varchar(10))", 
"CAST(Year AS char(4)) + ' - ' + 'Q' + CAST(Quarter AS varchar(10))", 
"COALESCE(Class + ': ' +ClassDesc, 'Unknown')", 
"COALESCE(Class + ': ' +ClassDesc, 'Unknown')", 
"COALESCE(Class + ': ' +ClassDesc, 'Unknown')", 
"COALESCE(Class + ': ' +ClassDesc, 'Unknown')", 
"COALESCE(InsiderCode + ': ' + InsiderCodeDesc, 'Unknown')", 
"COALESCE(InsiderCode + ': ' + InsiderCodeDesc, 'Unknown')", 
"COALESCE(InsiderCode + ': ' + InsiderCodeDesc, 'Unknown')", 
"COALESCE(InsiderCode + ': ' + InsiderCodeDesc, 'Unknown')", 
"convert(varchar(12), ActualDate, 110)", 
"convert(varchar(12), ActualDate, 110)", 
"convert(varchar(12), ActualDate, 110)", 
"CustomerNumber + ': ' + ISNULL(CustomerName, '')", 
"CustomerNumber + ': ' + ISNULL(CustomerName, '')", 
"DateDiff(yy, Dateofbirth, getdate())", 
"DateDiff(yy, Dateofbirth, getdate())", 
"DateDiff(yy, Dateofbirth, getdate())", 
"DateDiff(yy, Dateofbirth, getdate())", 
"DaysPastDue * Active_Principal_Balance", 
"DrawerNumber + ' - ' + DrawerDesc", 
"Interest_Rate*Current_Balance", 
"Interest_Rate*Current_Balance", 
"Interest_Rate1 * Current_Balance", 
"LEFT(CustomerName, 1)", 
"LEFT(CustomerName, 1)", 
"LEFT(Zipcode, 5)", 
"LEFT(Zipcode, 5)", 
"LEFT(Zipcode, 5)", 
"LEFT(Zipcode, 5)", 
"Loan_to_Value_Ratio * Active_Principal_Balance", 
"MTD_Days * MthAggBalance", 
"OfficerCode + ': ' + ISNULL(Resp_Name, '')", 
"OfficerCode + ': ' + ISNULL(Resp_Name, '')", 
"Portfolio + ' - ' + PortfolioName", 
"Rate_Over_Split * Active_Principal_Balance", 
"RateVariance * Active_Principal_Balance", 
"'Region ' + Region", 
"Region + ': ' + isnull(RegionDesc, '')", 
"Region + ': ' + isnull(RegionDesc, '')", 
"Region + ': ' + isnull(RegionDesc, '')", 
"RiskRating * Active_Principal_Balance", 
"ROA * Current_Balance", 
"ROE * Current_Balance", 
"rtrim(Resp_Code) + ': ' + isnull(Resp_Name, '')", 
"rtrim(Resp_Code) + ': ' + isnull(Resp_Name, '')", 
};