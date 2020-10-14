<Query Kind="Program">
  <Connection>
    <ID>3225e59a-9211-49f8-86c5-232105e4bb98</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>f9azwcazuresqlbi01.database.windows.net</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <SqlSecurity>true</SqlSecurity>
    <UserName>F9BIAZURESQLSA</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAJlEpdUAn4EGHFz3/0wH3fgAAAAACAAAAAAAQZgAAAAEAACAAAAD81OgK/mXEyM7x8m262C3krg5cXd0UpSKdFTWLFGHrPAAAAAAOgAAAAAIAACAAAAANiDZFlP+PLJhmLXBKfH734YqVjrcy++ImgZggYt/H+hAAAACfoNXHf8Rf4m92WdLbEGuQQAAAAHwiP8hdzQqOrqkRA1VGVJACtlzbUpMc4aHk0xuDKdjoJeUec3JtMaHWBC6u+aJwb03ZYIHZSMxr6PQaPd04X+c=</Password>
    <DbVersion>Azure</DbVersion>
    <Database>Enki</Database>
  </Connection>
</Query>

//Everything but SVC messages will contain /AN
void Main()
{
	RawFileData
	//.Where(f => f.SMI.Equals("M10"))
	//.Where(f => f.Payload.Contains("/AL"))
	//.Take(1000)
	.ToArray()
	//.Where(t1 => Regex.Split(t1.Payload, @"\n").ToArray()[2].Contains("/SL"))
	.Select(t1 => new
	{
		t1.Priority,
		t1.Receivers,
		t1.Sender,
		t1.DateTimeGroup,
		t1.SMI,
		AN = Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[1], "/AN N[0-9]{0,3}FR").Value.Replace("/AN", string.Empty).Trim(),
		DA = Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[1], "/DA [A-Z]{4}").Value.Replace("/DA", string.Empty).Trim(),
		AD = Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[1], "/AD [A-Z]{4}").Value.Replace("/AD", string.Empty).Trim(),
		DS = Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[1], "/DS [A-Z]{4}").Value.Replace("/DS", string.Empty).Trim(),
		IN = Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[1], "/IN [0-9]{0,6}").Value.Replace("/IN", string.Empty).Trim(),
		OF = Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[1], "/OF [0-9]{0,6}").Value.Replace("/OF", string.Empty).Trim(),
		FB = Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[1], "/FB {1,4}[0-9]{0,6}").Value.Replace("/FB", string.Empty).Trim(),
		BF = Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[1], "/BF {1,4}[0-9]{0,6}").Value.Replace("/BF", string.Empty).Trim(),
		LA = Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[1], "/LA {0,2}[0-9]{0,1}").Value.Replace("/LA", string.Empty).Trim(),
		LR = Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[1], "/LR {0,2}[A-Z0-9]{0,1}").Value.Replace("/LR", string.Empty).Trim(),
		//FlightIdentifier = Regex.Split(t1.Payload, @"\n").ToArray()[1].Split("/AN")[0].Split(" ")[1].Trim(),
		//Departure = (new[] {"ARR","DEP"}).Contains(t1.SMI)
		//			? Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[1],"/DA [A-Z]{4}").Value
		//			: String.Empty,
		//RegistrationNumber = Regex.Split(t1.Payload, @"\n").ToArray()[1].Split("/AN")[1].Trim().Split("/")[0],
		GroundStation = Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[2], "DT DDL [A-Z]{3,4}").Value.Replace("DT DDL", String.Empty).Trim(),
		MessageSequenceNumber = Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[2], "[A-Z][0-9]{2}[A-Z]").Value,
		MessageDateTime = new DateTime(
			Int32.Parse($"20{t1.FileName.Substring(0, 6).Substring(0, 2)}"),
			Int32.Parse(t1.FileName.Substring(0, 6).Substring(2, 2)),
			Int32.Parse(t1.FileName.Substring(0, 6).Substring(4, 2)),
			Int32.Parse(t1.DateTimeGroup.Substring(2, 2)),
			Int32.Parse(t1.DateTimeGroup.Substring(4, 2)),
			0
			),
		MessageReceptionTimeStamp = Regex.Match(Regex.Split(t1.Payload, @"\n").ToArray()[2], "[0-9]{6}").Value,
		//MessageSequenceNumber = Regex.Split(t1.Payload, @"\n").ToArray()[2].Substring(17, 6).Trim(),
		TextElements = Regex.Split(t1.Payload, @"\n").ToArray()[1],
		CommicationService = Regex.Split(t1.Payload, @"\n").ToArray()[2],
		FreeText = Regex.Split(t1.Payload, @"\n").ToArray()[3],
		t1.FileName,
		t1.FileDate
	})
	.Where(fr => fr.AN.Equals("N216FR"))
	.Where(fd => fd.MessageDateTime.Date.Equals(DateTime.Parse("6/22/2020").Date))
	.OrderBy(fd => fd.MessageDateTime)
	.ThenBy(fd => fd.DateTimeGroup)
	.Dump();
}

// Define other methods, classes and namespaces here
