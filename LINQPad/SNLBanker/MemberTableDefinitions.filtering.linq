<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
	var memberTableDefinitions = new[]{
		new { TableName = "dim_Usage", KeyColumn = "dimUsageID", Caption = "Usage", DataType = "Integer", DisplayColumn = "dimUsageID", Value = "Usage Less Than 75%", Expression = "" },
		new { TableName = "dim_Usage", KeyColumn = "UsageDesc", Caption = "Usage", DataType = "WChar", DisplayColumn = "UsageDesc", Value = "Usage Less Than 75%", Expression = "" },
		new { TableName = "dim_Products", KeyColumn = "ProductID", Caption = "Product Name", DataType = "Integer", DisplayColumn = "ProductID", Value = "", Expression = "" },
		new { TableName = "dim_Customer", KeyColumn = "CustomerID", Caption = "Customer Name", DataType = "Integer", DisplayColumn = "CustomerID", Value = "", Expression = "" },
		new { TableName = "dim_Branch", KeyColumn = "BranchID", Caption = "Branch", DataType = "Integer", DisplayColumn = "BranchID", Value = "", Expression = "" },
		new { TableName = "dim_Accounts", KeyColumn = "dimAccountID", Caption = "Accounts", DataType = "BigInt", DisplayColumn = "dimAccountID", Value = "", Expression = "" },
		new { TableName = "dim_RiskRating", KeyColumn = "Risk_Rate_ID", Caption = "All Risk Rating", DataType = "Integer", DisplayColumn = "Risk_Rate_ID", Value = "", Expression = "" },
		new { TableName = "dim_Customer", KeyColumn = "RespName_Display", Caption = "Officer Name", DataType = "WChar", DisplayColumn = "RespName_Display", Value = "", Expression = "OfficerCode + ': ' + ISNULL(Resp_Name, '')" },
		new { TableName = "dim_Products", KeyColumn = "ProductID", Caption = "Product Family", DataType = "WChar", DisplayColumn = "ProductClassCode", Value = "", Expression = "" },
		new { TableName = "dim_Products", KeyColumn = "ProductID", Caption = "Product Family", DataType = "WChar", DisplayColumn = "ProductCategory", Value = "", Expression = "" },
		new { TableName = "dim_Products", KeyColumn = "ProductName_Display", Caption = "Product Name", DataType = "WChar", DisplayColumn = "ProductName_Display", Value = "", Expression = "CASE \r\nWHEN Product_Number IS NULL THEN ProductName\r\nELSE\r\nCAST(Product_Number AS varchar(10))  + ': ' + isnull(ProductName, '')\r\nEND" },
		new { TableName = "dim_Customer", KeyColumn = "CustomerName_Display", Caption = "Customer Name", DataType = "WChar", DisplayColumn = "CustomerName_Display", Value = "", Expression = "CustomerNumber + ': ' + ISNULL(CustomerName, '')" },
		new { TableName = "dim_Branch", KeyColumn = "BranchName_Display", Caption = "Branch", DataType = "WChar", DisplayColumn = "BranchName_Display", Value = "", Expression = "CASE \r\nWHEN Branch_Number IS NULL THEN BranchName\r\nELSE\r\nCAST(Branch_Number AS varchar(10))  + ': ' + BranchName\r\nEND" },
		new { TableName = "dim_Accounts", KeyColumn = "dimAccountID", Caption = "Accounts", DataType = "WChar", DisplayColumn = "AccountNumber", Value = "", Expression = "" },
		new { TableName = "dim_RiskRating", KeyColumn = "Risk_Rate_Display", Caption = "All Risk Rating", DataType = "WChar", DisplayColumn = "Risk_Rate_Display", Value = "", Expression = "CASE \r\nWHEN Loan_Rating_Code_1 IS NULL THEN Risk\r\nWHEN Risk = '' THEN CAST(Loan_Rating_Code_1 AS varchar(10))\r\nELSE\r\nCAST(Loan_Rating_Code_1 AS varchar(10)) + ': ' + Risk\r\nEND" },
		new { TableName = "dim_Products", KeyColumn = "ProductFamily_Display", Caption = "Product Family", DataType = "WChar", DisplayColumn = "ProductFamily_Display", Value = "", Expression = "CASE \r\nWHEN ProductClassCode IS NULL THEN ProductClassDesc\r\nELSE\r\nProductClassCode + ': ' + isnull(ProductClassDesc, '')\r\nEND" },
		new { TableName = "FACT_Loans", KeyColumn = "Active_Principal_Balance", Caption = "Current Balance", DataType = "Double", DisplayColumn = "Active_Principal_Balance", Value = "", Expression = "" },
		new { TableName = "FACT_Loans", KeyColumn = "NoteRate_x_Balance", Caption = "Note Rate x Balance", DataType = "Double", DisplayColumn = "NoteRate_x_Balance", Value = "", Expression = "Rate_Over_Split * Active_Principal_Balance" },	
	};
	
	memberTableDefinitions.ToLookup(tbl => tbl.TableName).Dump();
}

// Define other methods and classes here
