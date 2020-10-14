<Query Kind="Program">
  <Reference>&lt;ProgramFilesX86&gt;\Microsoft.NET\ADOMD.NET\110\Microsoft.AnalysisServices.AdomdClient.dll</Reference>
  <Reference>&lt;ProgramFilesX86&gt;\Microsoft SQL Server\110\SDK\Assemblies\Microsoft.AnalysisServices.DLL</Reference>
  <Namespace>Microsoft.AnalysisServices</Namespace>
  <Namespace>Microsoft.AnalysisServices.AdomdClient</Namespace>
</Query>

void Main()
{
	var chartMdx = @"WITH MEMBER [Chart of Accounts].[Chart of Accounts].&[59020].[NET INCOME (59020)] AS '[Chart of Accounts].[Chart of Accounts].&[43019]+[Chart of Accounts].[Chart of Accounts].&[59019]', SOLVE_ORDER=100, FORMAT_STRING='Currency'
 SELECT HIERARCHIZE({{[Date].[Calendar Period].[All Calendar Periods]}}) ON COLUMNS FROM [GL Cube]";
 
 var strMdx = @"WITH MEMBER [Date].[Calendar Period].[a94bcbaba1884bec89f7f5459deee757] AS 'AGGREGATE({{[Date].[Calendar Period].[All Calendar Periods]}})'
MEMBER [Chart of Accounts].[Report Type].[195a37c0a83e4ca89731f1d0028aae27] AS 'AGGREGATE({{[Chart of Accounts].[Report Type].&[I]}})'
SET c0 AS 'HIERARCHIZE({[Measures].[Current Balance], [Measures].[Net Change by Period], [Measures].[YTD Balance], [Measures].[MTD Balance], [Measures].[QTD Balance], [Measures].[MTD Avg Balance], [Measures].[YTD Avg Balance]})'
SET r0 AS 'HIERARCHIZE({[Chart of Accounts].[Chart of Accounts].&[43019]})'
 SELECT  NON EMPTY {[c0]}
 ON COLUMNS,  NON EMPTY {[r0]}
 ON ROWS FROM [GL Cube] WHERE ([Date].[Calendar Period].[a94bcbaba1884bec89f7f5459deee757], [Chart of Accounts].[Report Type].[195a37c0a83e4ca89731f1d0028aae27])";
 
 
	AdomdConnection MetaDataConnection = new AdomdConnection(Utils.ConnectionString(ServerName, CatalogName));
	if (MetaDataConnection.State != ConnectionState.Open)
	{
		MetaDataConnection.Open();
	}
	
	
	AdomdCommand cmd = MetaDataConnection.CreateCommand();
	cmd.CommandType = CommandType.Text;
	cmd.CommandText = strMdx;
	
	var cellset = cmd.ExecuteCellSet();
	
	var Columns = cellset.Axes.Cast<Axis>().Where(ax => ax.Name == "Axis0").Select(col => new{
		cube = col.Set.Hierarchies.Cast<Microsoft.AnalysisServices.AdomdClient.Hierarchy>().FirstOrDefault().ParentDimension.ParentCube.Name,
		Hierarchies = col.Set.Hierarchies.Cast<Microsoft.AnalysisServices.AdomdClient.Hierarchy>().FirstOrDefault().UniqueName,
		Coordinates = col.Set.Tuples.Cast<Microsoft.AnalysisServices.AdomdClient.Tuple>().Select(tp => new{
				tp.TupleOrdinal,
				Name = tp.Members.Cast<Member>().Select(tm => tm.Name).FirstOrDefault()
		}).ToDictionary(tc => tc.TupleOrdinal, tc => tc.Name)
	});
	var Groups = cellset.Axes.Cast<Axis>().Where(ax => ax.Name == "Axis1").Select(col => new{
		cube = col.Set.Hierarchies.Cast<Microsoft.AnalysisServices.AdomdClient.Hierarchy>().FirstOrDefault().ParentDimension.ParentCube.Name,
		Hierarchies = col.Set.Hierarchies.Cast<Microsoft.AnalysisServices.AdomdClient.Hierarchy>().FirstOrDefault().UniqueName,
		Coordinates = col.Set.Tuples.Cast<Microsoft.AnalysisServices.AdomdClient.Tuple>().Select(tp => new{
				tp.TupleOrdinal,
				Name = tp.Members.Cast<Member>().Select(tm => tm.Name).FirstOrDefault()
		}).ToDictionary(tc => tc.TupleOrdinal, tc => tc.Name)
	});
	var Filters = cellset.FilterAxis;
	
	var colCube = MetaDataConnection.Cubes.Cast<CubeDef>().Where(cb => cb.Name.Contains(Columns.Select(c => c.cube).FirstOrDefault())).FirstOrDefault();
	
	
	Columns.Dump();
	Groups.Dump();
	Filters.Dump();
	if (MetaDataConnection.State != ConnectionState.Closed)
	{
		MetaDataConnection.Close();
	}
 
}

protected static String ServerName = "LT-JFILICHA";
protected static String CatalogName = "BankingCustomerIntelligence_JHLive";
// Define other methods and classes here
public class Utils
{

        public static String ConnectionString(String ServerName)
        {
            return ConnectionString(ServerName, null);
        }

        public static String ConnectionString(String ServerName, String InitialCatalog)
        {
            return !String.IsNullOrEmpty(InitialCatalog)
                       ? string.Format("Data Source={0};Application Name=CubeParser v{1}; Initial Catalog={2}",
                                       ServerName,
                                       System.Reflection.Assembly.GetExecutingAssembly().GetName().Version, InitialCatalog)
                       : string.Format("Data Source={0};Application Name=CubeParser v{1};",
                                       ServerName,
                                       System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
        }
}