<Query Kind="Program" />

void Main()
{

	var Iterations = 5;
	var randomSeed = 2;
	var precision = 4;
	var multiplier = 100000d;
	var random = new Random(randomSeed);
	for(int i = 1; i <= Iterations; i++)
	{
		Decimal.Round((decimal)(random.NextDouble() * multiplier),precision).Dump();
	}
}

// Define other methods and classes here
