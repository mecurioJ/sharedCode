<Query Kind="Program" />

void Main()
{
	//Get First Day of Last Month	
	DateTime.Today.AddDays(((DateTime.Today.Day-1)*-1)).AddMonths(-1).Dump();
	//Get First Day of This Month
	DateTime.Today.AddDays(((DateTime.Today.Day-1)*-1)).Dump();
	
	
    var credSet = new NetworkCredential() { Domain = "emllc", UserName = "eswsqlagent", Password = "txkjlxa12"};
}

// Define other methods and classes here
