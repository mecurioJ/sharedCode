<Query Kind="Program" />

void Main()
{
	DMStoDecimal(104d,59d,32d,HemisphereOrMeridian.West).Dump();
}

// Define other methods and classes here

public double DMStoDecimal(double degrees, double minutes, double seconds, HemisphereOrMeridian hemisphereOrMeridian)
{
	double PosNeg = -1.0;
		if((hemisphereOrMeridian == HemisphereOrMeridian.North) || (hemisphereOrMeridian == HemisphereOrMeridian.East))
		{
			PosNeg = Math.Abs(PosNeg);
		}
	double LatLong = (Math.Floor(degrees) + (Math.Floor(minutes)/60.0) + (seconds/3600.0));
	
	return PosNeg * LatLong;
}

public enum HemisphereOrMeridian
{
	North,
	South,
	East,
	West
}