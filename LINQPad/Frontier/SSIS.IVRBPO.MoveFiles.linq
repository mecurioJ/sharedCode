<Query Kind="Program" />

void Main()
{

	/*
		String sourcePath = Dts.Variables["sourcePath"].Value.ToString();
		String targetPath = Dts.Variables["targetPath"].Value.ToString();
		String regExMatch = Dts.Variables["regex"].Value.ToString();
	*/
	var sourcePath = @"\\goaspen\Department_Shares\Business Intelligence\Customer Care - CR.RES\";
	var targetPath = @"Archive";	
	var regExMatch = @"[0-9]{1,8}(WRAP|IVR|CSAT)_(mc|col|cp|cd)\.txt\-[0-9]{1,9}";


	var destination = Path.Combine(sourcePath, targetPath);
	
	var files = Directory.GetFiles(sourcePath);


	foreach (string fi in Directory.GetFiles(sourcePath))
	{

		string fileName = Path.GetFileName(fi);
		if (Regex.Match(fi, regExMatch).Success)
		{
			string sourceFileName = Path.Combine(sourcePath, fileName);
			string destinationFileName = Path.Combine(destination, fileName);

			File.Move(sourceFileName, destinationFileName);
		}

	}

}

// Define other methods and classes here
