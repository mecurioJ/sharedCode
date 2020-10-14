<Query Kind="Program">
  <Connection>
    <ID>1e5485fa-aac2-4b53-8481-ce2675973975</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>SNLBanker_SampleDW</Database>
  </Connection>
</Query>

void Main()
{
	
}

// Define other methods and classes here
public class Expense
{
	public String PersonName {get;set;}
	public String PersonID {get;set;}
	public DateTime ExpenseDate {get;set;}
	public ExpenseType SpendType {get;set;}
	public MealType FoodType {get;set;}
	public String Origin {get;set;}
	public String Destination {get;set;}
	public String VendorName {get;set;}
	public String ReceiptScanPath {get;set;}
	public String Notes {get;set;}
}

public enum ExpenseType
{
	Transportation,
	Lodging,
	Meals
}

public enum MealType
{
	Breakfast,
	Lunch,
	Dinner,
	Snacks
}