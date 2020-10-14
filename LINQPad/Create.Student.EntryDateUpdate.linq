<Query Kind="Program" />

void Main()
{

	var updateList = new List<String>();
	
	
	foreach (var file in new DirectoryInfo(@"\\10.1.10.25\e$\Projects\Mappings\SourceXML\StudentGradeLevel").GetFiles("*.xml"))
	{
		
	var StudentData = XElement.Load(file.FullName);
	
	updateList.AddRange(
	StudentData
		.Descendants()
		.Where(xn => xn.Name.LocalName.Equals("student"))
		.Select(e => string.Format("UPDATE [Source].[StudentSchoolGradeLevel] SET [EntryDate] = '{0}', [ExitDate] = '{1}'"+
										" WHERE [IdentificationCode] = {2}"+
										" AND [schoolID] = {3}"+
										" AND [grade] = '{4}'"+
										" AND [stateGrade] = '{5}'"+
										" AND [BEDSCode] = '{6}'"+
										" AND [locationCode] = '{7}'",
			e.Attribute("activeEnrollment.startDate").Value,
			e.Attribute("activeEnrollment.endDate").Value,
			e.Attribute("student.studentNumber").Value,
			e.Attribute("sch.schoolID").Value,
			e.Attribute("student.grade").Value,
			e.Attribute("student.stateGrade").Value,
			e.Attribute("customSchool.BEDSCode").Value,
			e.Attribute("customSchool.locationCode").Value
		)));
	}
	
		
		//updateList.Count.Dump();
		
		File.WriteAllLines(@"\\10.1.10.25\e$\Projects\Mappings\StudentGradeLevelUpdates.sql",updateList.ToArray());
}

// Define other methods and classes here
