<Query Kind="Program" />

void Main()
{
	var sourcePath = @"G:\Data\FLNERExtract\";
	var sourceDirectory = @"Frontier_FLNER_Extract_201802";

	var left = new DirectoryInfo(@$"{sourcePath}{sourceDirectory}").GetFiles();
	var right = new DirectoryInfo(@$"{sourcePath}{sourceDirectory} (2)").GetFiles();


	var isIdentical = left.SequenceEqual(right, new FileCompare());
	var CommonFiles = left.Intersect(right, new FileCompare());

	if (isIdentical)
	{
		isIdentical.Dump();
		left.FirstOrDefault().DirectoryName.Dump();
		foreach (var fi in right)
		{
			fi.Delete();
		}
		
		new DirectoryInfo(right.FirstOrDefault().DirectoryName).Delete();
	}
	else
	{
		CommonFiles.Dump();
	}

	//new { 
	//	isIdentical,
	//	CommonFiles
	//}.Dump();
	
	

}

// Define other methods, classes and namespaces here
class FileCompare : System.Collections.Generic.IEqualityComparer<System.IO.FileInfo>
{
	public FileCompare() { }

	public bool Equals(System.IO.FileInfo f1, System.IO.FileInfo f2)
	{
		return (f1.Name == f2.Name &&
				f1.Length == f2.Length);
	}

	// Return a hash that reflects the comparison criteria. According to the
	// rules for IEqualityComparer<T>, if Equals is true, then the hash codes must  
	// also be equal. Because equality as defined here is a simple value equality, not  
	// reference identity, it is possible that two or more objects will produce the same  
	// hash code.  
	public int GetHashCode(System.IO.FileInfo fi)
	{
		string s = $"{fi.Name}{fi.Length}";
		return s.GetHashCode();
	}
}