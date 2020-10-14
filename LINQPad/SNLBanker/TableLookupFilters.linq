<Query Kind="Program" />

void Main()
{
var memberTables = new[]{"dim_Products",
"dim_Customer",
"dim_Branch",
"dim_Accounts",
"dim_RiskRating",
"dim_Loan_Participation_Status",
"FACT_Loans",};

	var measureJoins = new []{new { LeftTable = "dbo_dim_Products", LeftColumn = "ProductID", RightTable = "dbo_FACT_Loans", RightColumn = "ProductID" },
new { LeftTable = "dbo_dim_Branch", LeftColumn = "BranchID", RightTable = "dbo_FACT_Loans", RightColumn = "BranchID" },
new { LeftTable = "dbo_dim_Accounts", LeftColumn = "dimAccountID", RightTable = "dbo_FACT_Loans", RightColumn = "dimAccountID" },
new { LeftTable = "dbo_dim_RiskRating", LeftColumn = "Risk_Rate_ID", RightTable = "dbo_FACT_Loans", RightColumn = "Risk_Rate_ID" },
new { LeftTable = "dbo_dim_Part_Placed_Code", LeftColumn = "Orig_Direct_Indirect_ID", RightTable = "dbo_FACT_Loans", RightColumn = "Orig_Direct_Indirect_ID" },
new { LeftTable = "dbo_dim_AccountType", LeftColumn = "AccountTypeID", RightTable = "dbo_FACT_Loans", RightColumn = "AccountTypeID" },
new { LeftTable = "dbo_dim_Portfolio", LeftColumn = "PortfolioID", RightTable = "dbo_FACT_Loans", RightColumn = "PortfolioID" },
new { LeftTable = "dbo_dim_ClassCode", LeftColumn = "ClassCodeID", RightTable = "dbo_FACT_Loans", RightColumn = "ClassCodeID" },
new { LeftTable = "dbo_dim_RespCode", LeftColumn = "RespCodeID", RightTable = "dbo_FACT_Loans", RightColumn = "RespCodeID" },
new { LeftTable = "dbo_dim_Account_Status", LeftColumn = "AccountStatusID", RightTable = "dbo_FACT_Loans", RightColumn = "AccountStatusID" },
new { LeftTable = "dbo_dim_PRPCode", LeftColumn = "PurposeID", RightTable = "dbo_FACT_Loans", RightColumn = "PurposeID" },
new { LeftTable = "dbo_dim_CollateralCodes", LeftColumn = "CollateralID", RightTable = "dbo_FACT_Loans", RightColumn = "CollateralID" },
new { LeftTable = "dbo_dim_Source_Placed", LeftColumn = "Source_Placed_ID", RightTable = "dbo_FACT_Loans", RightColumn = "Source_Placed_ID" },
new { LeftTable = "dbo_dim_Loan_Rate", LeftColumn = "Loan_Rate_ID", RightTable = "dbo_FACT_Loans", RightColumn = "Loan_Rate_ID" },
new { LeftTable = "dbo_dim_CallReportCode", LeftColumn = "dimCallReportCodeID", RightTable = "dbo_FACT_Loans", RightColumn = "dimCallReportCodeID" },
new { LeftTable = "dbo_dim_DepartmentCode", LeftColumn = "dimDepartmentCodeID", RightTable = "dbo_FACT_Loans", RightColumn = "dimDepartmentCodeID" },
new { LeftTable = "dbo_dim_CreditLineCode", LeftColumn = "dimCreditLineCodeID", RightTable = "dbo_FACT_Loans", RightColumn = "dimCreditLineCodeID" },
new { LeftTable = "dbo_dim_Occupancy", LeftColumn = "dimOccupancyID", RightTable = "dbo_FACT_Loans", RightColumn = "dimOccupancyID" },
new { LeftTable = "dbo_dim_Date", LeftColumn = "DateKey", RightTable = "dbo_FACT_Loans", RightColumn = "dimDateLastUpdatedID" },
new { LeftTable = "dbo_dim_MaturityDateGroup", LeftColumn = "MaturityDateID", RightTable = "dbo_FACT_Loans", RightColumn = "MaturityDate_Group" },
new { LeftTable = "dbo_dim_WatchListCode", LeftColumn = "dimWatchListID", RightTable = "dbo_FACT_Loans", RightColumn = "dimWatchListID" },
new { LeftTable = "dbo_dim_Usage", LeftColumn = "dimUsageID", RightTable = "dbo_FACT_Loans", RightColumn = "dimUsageID" },
new { LeftTable = "dbo_dim_ChargeOffDate", LeftColumn = "dimChargeOffDateID", RightTable = "dbo_FACT_Loans", RightColumn = "dimChargeOffDateID" },
new { LeftTable = "dbo_dim_Account_Status_DPD", LeftColumn = "dimDPDStatusID", RightTable = "dbo_FACT_Loans", RightColumn = "dimDPDStatusID" },
new { LeftTable = "dbo_dim_DateOpened", LeftColumn = "dimDateOpenID", RightTable = "dbo_FACT_Loans", RightColumn = "dimDateOpenID" },
new { LeftTable = "dbo_dim_DateClosed", LeftColumn = "dimDateClosedID", RightTable = "dbo_FACT_Loans", RightColumn = "dimDateClosedID" },};

measureJoins.Where(mj => memberTables.Contains(mj.LeftTable.Replace("dbo_",String.Empty))).Dump();
}

// Define other methods and classes here
