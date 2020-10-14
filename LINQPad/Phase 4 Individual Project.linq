<Query Kind="Program" />

void Main()
{
	//http://rapidtables.com/math/symbols/Set_Symbols.htm
	//Simulation
	/*
	
		http://www.codeproject.com/Articles/5436/Genetic-and-Ant-Colony-Optimization-Algorithms
	*/

	//Red Numbers
	var A = new []{1,36,3,34,5,32,7,30,9,14,23,16,21,18,19,12,25,27};
	//Black Numbers
	var B = new []{13,24,15,22,17,20,11,30,26,28,2,35,4,33,6,31,8,29,10};
	//Green Numbers
	var C = new []{0};
	//Set of All Numbers
	var unionSet = A.Union(B.Union(C)).OrderBy(t =>t );
	//Even Numbers
	var D = unionSet.Where(GetOdd).ToList();
	
	//Odd Numbers
	var E = unionSet.Where(GetEven).ToList();
	
	
	//part 1
	var F = new []{1,2,3,4,5,6,7,8,9,10,11,12};
	//A∪B
	A.Union(B);
    //A∩D
	A.Intersect(D);
    //B∩C
	B.Intersect(C);
    //C∪E
	C.Union(E);
    //B∩F
	B.Intersect(D);
    //E∩F 
	E.Intersect(F);
	
	
	//Part 2
	//Create an object to hold the objects generated via our
	List<SimulationRun> Simulation = new List<SimulationRun>();
		Random r = new Random(0);
	
	//iterate through a loop for x amount of times
	for(int i = 0; i <= 10; i++)
	{
		var simSpin = r.Next(36);
		//Add object to the Array
		Simulation.Add(
		new SimulationRun(){
			//Set Primary Key value to iteration loop
			Key = i,
			//Save value of "Spin"
			Number = simSpin,
			//identify if the object is black or red
			Color = (A.Where(a => a.Equals(simSpin)).Any())
						? "Red"
						: B.Where(b => b.Equals(simSpin)).Any()
						? "Black"
						: "Green",
			//identify if object is even or odd. 
			IsOdd = GetOdd(simSpin)
		});
		
	}
	Simulation.Dump();
	
}

public class SimulationRun
{
	public int Key {get;set;}
	public int Number {get;set;}
	public String Color{get;set;}
	public bool IsOdd {get;set;}
}
// Define other methods and classes here
static bool GetEven(int number)
{
	return (number % 2 == 0);
}

// Define other methods and classes here
static bool GetOdd(int number)
{
	return (number % 2 != 0);
}