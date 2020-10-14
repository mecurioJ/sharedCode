<Query Kind="Program">
  <NuGetReference>LinqToExcel</NuGetReference>
  <Namespace>LinqToExcel</Namespace>
  <Namespace>LinqToExcel.Attributes</Namespace>
  <Namespace>LinqToExcel.Domain</Namespace>
  <Namespace>LinqToExcel.Extensions</Namespace>
  <Namespace>LinqToExcel.Query</Namespace>
</Query>

void Main()
{
	 String SpreadsheetLocation = @"D:\Downloads\Source to Target Mapping.xlsx";

            //initialize LinqToExcel Factory

            ExcelQueryFactory excel = new ExcelQueryFactory(SpreadsheetLocation);

            //Get worksheets
            IEnumerable<string> worksheetStruct = excel.GetWorksheetNames();

            //get the array of worksheets

            List<TableDetail> Tables = new List<TableDetail>();
            

            for (int sheet = 0; sheet < worksheetStruct.Count()-1; sheet++)
            {

                ExcelQueryable<RowNoHeader> tableSheet = excel.WorksheetNoHeader(sheet);

                TableDetail td = new TableDetail
                {
                    TableName = tableSheet.FirstOrDefault()[1].ToString(),
                    TableSchema = "elf_etl",
                    Columns = tableSheet.Select(rw => new ColumnDetail(rw)).Skip(7)
                };

                Tables.Add(td);
            }
	
	
	Tables.Select(tt => 
	new{
	tt.TableName,
	tt.TableSchema,
	Columns = tt.Columns
	.Where(cc => !String.IsNullOrEmpty(cc.TargetColumnName))
	.Select(cc => 
	String.Format("{0} {1} {2}",
		cc.TargetColumnName,
		cc.DataType, 
		cc.Size.IsNumber()
			? cc.Precision.IsNumber()
				? String.Format("({0},{1})",cc.Size,cc.Precision)
				: String.Format("({0})",cc.Size)
			: String.Empty)
	).Aggregate((p,n) => String.Format("{0}, {1}",p,n)
	)
	}
	).Select(ft => 
		String.Format( 
		"CREATE TABLE {1}.{0} ({2})",
		ft.TableName,
		ft.TableSchema,
		ft.Columns
		)
	
	)
	
	
	.Dump();
	
}

// Define other methods and classes here
//will need to create an expression object here, to catch the format and dump it out.

class TableDetail
    {
        public TableDetail()
        {
            
        }

        public String TableName { get; set; }
        public String TableSchema { get; set; }
        public IEnumerable<ColumnDetail> Columns { get; set; }
    }

    class ColumnDetail
    {

        public ColumnDetail()
        {
            
        }

        public ColumnDetail(RowNoHeader rw)
        {
            TargetColumnName = rw[0].ToString();
            Description = rw[1].ToString();
            NaturalKey = rw[2].ToString();
            DataType = rw[3].ToString();
            Size = rw[4].ToString();
            Precision = rw[5].ToString();
            PrimaryKey = rw[6].ToString();
            Nullable = rw[7].ToString();
            DefaultValue = rw[8].ToString();
        }
        
        
        public String TargetColumnName { get; set; }
        public String Description { get; set; }
        public String NaturalKey { get; set; }
        public String DataType { get; set; }
        public String Size { get; set; }
        public String Precision { get; set; }
        public String PrimaryKey { get; set; }
        public String Nullable { get; set; }
        public String DefaultValue { get; set; }
    }