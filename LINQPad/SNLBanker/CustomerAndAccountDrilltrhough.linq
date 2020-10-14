<Query Kind="SQL">
  <Connection>
    <ID>1e5485fa-aac2-4b53-8481-ce2675973975</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>SNLBanker_SampleDW</Database>
  </Connection>
</Query>

select 
	t1.CustomerID,
	t2.dimAccountID,
	t1.CustomerNumber,
	t1.CustomerName,
	t3.AccountNumber,
	t3.AccountType,
	t3.Bank_Name,
	t5.Current_Balance,
	t6.*
	from dim_Customer t1
	Join dim_AccountCustomers t2 on t1.CustomerID = t2.dimCustomerID
	Join dim_Accounts t3 on t2.dimAccountID = t3.dimAccountID
	join Fact_Global t5 on t3.dimAccountID = t5.dimAccountID
	join dim_products t6 on t5.ProductID = t6.productID
	join dim_RespCode t4 on t1.Officercode = t4.resp_Code