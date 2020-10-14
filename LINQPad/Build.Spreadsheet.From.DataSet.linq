<Query Kind="Program">
  <Connection>
    <ID>c96d1b6f-b3ce-4545-8b74-e025ccf80296</ID>
    <Persist>true</Persist>
    <Server>JoeySurfacePro</Server>
    <Database>BVAData</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference Relative="Libs\spire.xls_7.10.79\NET4.0\Spire.Common.dll">C:\Users\joey\Dropbox\LinqPad\Libs\spire.xls_7.10.79\NET4.0\Spire.Common.dll</Reference>
  <Reference Relative="Libs\spire.xls_7.10.79\NET4.0\Spire.License.dll">C:\Users\joey\Dropbox\LinqPad\Libs\spire.xls_7.10.79\NET4.0\Spire.License.dll</Reference>
  <Reference Relative="Libs\spire.xls_7.10.79\NET4.0\Spire.Pdf.dll">C:\Users\joey\Dropbox\LinqPad\Libs\spire.xls_7.10.79\NET4.0\Spire.Pdf.dll</Reference>
  <Reference Relative="Libs\spire.xls_7.10.79\NET4.0\Spire.XLS.dll">C:\Users\joey\Dropbox\LinqPad\Libs\spire.xls_7.10.79\NET4.0\Spire.XLS.dll</Reference>
  <Namespace>Spire.CompoundFile.XLS</Namespace>
  <Namespace>Spire.CompoundFile.XLS.Net</Namespace>
  <Namespace>Spire.Xls</Namespace>
  <Namespace>Spire.Xls.Calculation</Namespace>
  <Namespace>Spire.Xls.Charts</Namespace>
  <Namespace>Spire.Xls.Collections</Namespace>
  <Namespace>Spire.Xls.Converter</Namespace>
  <Namespace>Spire.Xls.Core</Namespace>
  <Namespace>Spire.Xls.Core.Converter.Exporting.EMF</Namespace>
  <Namespace>Spire.Xls.Core.Converter.Spreadsheet.ExcelStyle</Namespace>
  <Namespace>Spire.Xls.Core.Converter.Spreadsheet.PivotTable</Namespace>
  <Namespace>Spire.Xls.Core.Interface</Namespace>
  <Namespace>Spire.Xls.Core.Interfaces</Namespace>
  <Namespace>Spire.Xls.Core.Parser.Biff_Records</Namespace>
  <Namespace>Spire.Xls.Core.Parser.Biff_Records.Charts</Namespace>
  <Namespace>Spire.Xls.Core.Parser.Biff_Records.Formula</Namespace>
  <Namespace>Spire.Xls.Core.Parser.Biff_Records.MsoDrawing</Namespace>
  <Namespace>Spire.Xls.Core.Parser.Biff_Records.ObjRecords</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Charts</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Collections</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Parser.Biff_Records.Charts</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.PivotTables</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Security</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Shapes</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Sorting</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.Tables</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.TemplateMarkers</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.XmlReaders</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.XmlSerialization</Namespace>
  <Namespace>Spire.Xls.Core.Spreadsheet.XmlSerialization.Charts</Namespace>
  <Namespace>Spire.Xls.License</Namespace>
</Query>

void Main()
{

	var SheetDefinition = UploadFileDefinitions
		.Select(fd => new{ fd.AggregateResource, fd.FieldName, fd.Comment})
		.GroupBy(g => g.AggregateResource, g => new{g.FieldName, g.Comment})
		.Where(g => !g.Key.Contains("descriptor"))
		;
		
		
		
	
#region CellList

	var cellList = new []{
	new KeyValuePair<int,String>(0,"A"),
	new KeyValuePair<int,String>(1,"B"),
	new KeyValuePair<int,String>(2,"C"),
	new KeyValuePair<int,String>(3,"D"),
	new KeyValuePair<int,String>(4,"E"),
	new KeyValuePair<int,String>(5,"F"),
	new KeyValuePair<int,String>(6,"G"),
	new KeyValuePair<int,String>(7,"H"),
	new KeyValuePair<int,String>(8,"I"),
	new KeyValuePair<int,String>(9,"J"),
	new KeyValuePair<int,String>(10,"K"),
	new KeyValuePair<int,String>(11,"L"),
	new KeyValuePair<int,String>(12,"M"),
	new KeyValuePair<int,String>(13,"N"),
	new KeyValuePair<int,String>(14,"O"),
	new KeyValuePair<int,String>(15,"P"),
	new KeyValuePair<int,String>(16,"Q"),
	new KeyValuePair<int,String>(17,"R"),
	new KeyValuePair<int,String>(18,"S"),
	new KeyValuePair<int,String>(19,"T"),
	new KeyValuePair<int,String>(20,"U"),
	new KeyValuePair<int,String>(21,"V"),
	new KeyValuePair<int,String>(22,"W"),
	new KeyValuePair<int,String>(23,"X"),
	new KeyValuePair<int,String>(24,"Y"),
	new KeyValuePair<int,String>(25,"Z"),
	new KeyValuePair<int,String>(26,"AA"),
	new KeyValuePair<int,String>(27,"AB"),
	new KeyValuePair<int,String>(28,"AC"),
	new KeyValuePair<int,String>(29,"AD"),
	new KeyValuePair<int,String>(30,"AE"),
	new KeyValuePair<int,String>(31,"AF"),
	new KeyValuePair<int,String>(32,"AG"),
	new KeyValuePair<int,String>(33,"AH"),
	new KeyValuePair<int,String>(34,"AI"),
	new KeyValuePair<int,String>(35,"AJ"),
	new KeyValuePair<int,String>(36,"AK"),
	new KeyValuePair<int,String>(37,"AL"),
	new KeyValuePair<int,String>(38,"AM"),
	new KeyValuePair<int,String>(39,"AN"),
	new KeyValuePair<int,String>(40,"AO"),
	new KeyValuePair<int,String>(41,"AP"),
	new KeyValuePair<int,String>(42,"AQ"),
	new KeyValuePair<int,String>(43,"AR"),
	new KeyValuePair<int,String>(44,"AS"),
	new KeyValuePair<int,String>(45,"AT"),
	new KeyValuePair<int,String>(46,"AU"),
	new KeyValuePair<int,String>(47,"AV"),
	new KeyValuePair<int,String>(48,"AW"),
	new KeyValuePair<int,String>(49,"AX"),
	new KeyValuePair<int,String>(50,"AY"),
	new KeyValuePair<int,String>(51,"AZ"),
	new KeyValuePair<int,String>(52,"BA"),
	new KeyValuePair<int,String>(53,"BB"),
	new KeyValuePair<int,String>(54,"BC"),
	new KeyValuePair<int,String>(55,"BD"),
	new KeyValuePair<int,String>(56,"BE"),
	new KeyValuePair<int,String>(57,"BF"),
	new KeyValuePair<int,String>(58,"BG"),
	new KeyValuePair<int,String>(59,"BH"),
	new KeyValuePair<int,String>(60,"BI"),
	new KeyValuePair<int,String>(61,"BJ"),
	new KeyValuePair<int,String>(62,"BK"),
	new KeyValuePair<int,String>(63,"BL"),
	new KeyValuePair<int,String>(64,"BM"),
	new KeyValuePair<int,String>(65,"BN"),
	new KeyValuePair<int,String>(66,"BO"),
	new KeyValuePair<int,String>(67,"BP"),
	new KeyValuePair<int,String>(68,"BQ"),
	new KeyValuePair<int,String>(69,"BR"),
	new KeyValuePair<int,String>(60,"BS"),
	new KeyValuePair<int,String>(71,"BT"),
	new KeyValuePair<int,String>(72,"BU"),
	new KeyValuePair<int,String>(73,"BV"),
	new KeyValuePair<int,String>(74,"BW"),
	new KeyValuePair<int,String>(75,"BX"),
	new KeyValuePair<int,String>(76,"BY"),
	new KeyValuePair<int,String>(77,"BZ")
	};
#endregion
	
	Workbook wb = new Workbook();
	wb.LoadFromFile(@"D:\BrightView Analytics\Data Import Templates\TestRun.xlsx");
	
	
	
	foreach (var st in SheetDefinition.Where(k => new[]{
									"CalendarDate",
								"ClassPeriod",
								"CourseOffering",
								"CourseTranscript",
								"Course",
								"DisciplineAction",
								"DisciplineIncident",
								"Grade",
								"GradingPeriod",
								"LocalEducationAgency",
								"Location",
								"Parent",
								"School",
								"Section",
								"Session",
								"StaffEducationOrganizationAssignmentAssociation",
								"StaffSchoolAssociation",
								"StaffSectionAssociation",
								"Student",
								"StudentAcademicRecord",
								"StudentProgramAssociation",
								"StudentDisciplineIncidentAssociation",
								//"StudentEarlyLearningProgramAssociation",
								"StudentParentAssociation",
								"StudentSchoolAssociation",
								"StudentSchoolAttendanceEvent",
								"StudentSectionAssociation",
								"StudentSectionAttendanceEvent",
	}.Contains(k.Key)).Select(k => k.Key))
	{
		//add the sheet
		var sht = wb.Worksheets.Add(st);
		var data = SheetDefinition.Where(k => k.Key.Equals(st)).SelectMany(dd => dd.Select(f => new{f.FieldName, f.Comment})).ToArray();
		
		for (int i = 0; i < data.Count(); i++)
		{
			var cellName = cellList[i].Value;
			
			try
			{
				var cell = sht.Range[String.Format("{0}1",cellName)];
				cell.Text = data[i].FieldName;
				cell.Comment.Text = data[i].Comment;	        
				cell.Comment.AutoSize = true;
			}
			catch (Exception ex)
			{	
				st.Dump();
				data[i].Dump();
				cellName.Dump();
				
				throw;
			}
		}
		
		
	}
	
	wb.SaveToFile(@"D:\BrightView Analytics\Data Import Templates\ICSheets.xlsx");
	
}

// Define other methods and classes here