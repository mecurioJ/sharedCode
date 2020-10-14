<Query Kind="Program">
  <Connection>
    <ID>0799912f-9f83-4973-96a1-f8730f5f7db7</ID>
    <Persist>true</Persist>
    <Server>sqlsass</Server>
    <Database>ECSDMTools</Database>
  </Connection>
</Query>

void Main()
{


	var DataTables = TableFilter;
	
							

	var OdbcSourceConnection = "QSDN_172.16.8.219.Brightview";
	var OdbcDestinationConnection="SQLSASS.mjfilichia";

	//Get the Tables from the table
	//and load them into an XML Object set.
	var dataflows = 	
	BIMLData.Where(bi => DataTables.Contains(bi.SourceTable))
	//.Where(bi => bi.Table_text.Contains("Student"))
	.Select(
		bi => new{
			table = bi.SourceTable
			, bi.SourceName
			, bi.DestinationName
			//Build the various attributes into a set of objects
			//for the BIML Data Flows
			, DataflowName = new XElement("Dataflow", 
								new XAttribute("Name", String.Format("AS400 to SQLSASS {0}",bi.SourceTable)), 
								new XElement("Transformations",
								new XElement("OdbcSource",
									new XAttribute("Name",bi.SourceName),
									new XAttribute("Connection",OdbcSourceConnection),
									new XElement("ExternalTableInput",
										new XAttribute("Table",String.Format("\"CPDATA\".\"{0}\"",bi.SourceTable)))),
								new XElement("OdbcDestination",
								new XAttribute("Name",bi.DestinationName),
								new XAttribute("Connection",OdbcDestinationConnection),
								new XElement("InputPath",
								new XAttribute("SsisName","OLE DB Destination Input"),
								new XAttribute("OutputPathName",String.Format("{0}.Output",bi.SourceName))),
								new XElement("ExternalTableOutput",
								new XAttribute("Table",String.Format("[CPDATA].[{0}]",bi.SourceTable))))
								)
								)
			, ExternalTableOutput = String.Format("[CPDATA].[{0}]",bi.SourceTable)
			, OutputPathName = String.Format("{0}.Output",bi.SourceTable)
			, SortOrder = SortOrder(bi.SourceTable)
		}
	)
	.Select(bml => new OutputBiml(){SortOrder = bml.SortOrder, DataflowName = bml.DataflowName}).ToList()
	.Select(bml => bml).OrderBy(ss => ss.SortOrder).Select(bm => bm.DataflowName);
	
	var Tasks = new XElement("Tasks");
	
	foreach(var x in dataflows)
	{
		Tasks.Add(x);
	}
	
	Tasks.Dump();
	
	/*
	XElement dataFlowTasks = new XElement("Tasks",dataflows.Skip(2600).Take(200));
	
	String PackageName;
	
	
	XElement bimlRoot = new XElement("Biml",
		xConn,
		Packages(new XElement("Tasks",dataflows.Take(200)), out PackageName)
	);
	
	
	bimlRoot.Dump();
	*/
	//dataflows.Count().Dump(); //2672
	
	//dataflow1.Save(@"B:\Projects\BIML\Dataflow1To200.biml");
//	var dataflow2 = new XElement("Tasks",dataflows.Skip(200).Take(200));
//	var dataflow3 = new XElement("Tasks",dataflows.Skip(400).Take(200));
//	var dataflow4 = new XElement("Tasks",dataflows.Skip(600).Take(200));
//	var dataflow5 = new XElement("Tasks",dataflows.Skip(800).Take(200));
//	var dataflow6 = new XElement("Tasks",dataflows.Skip(1000).Take(200));
//	var dataflow7 = new XElement("Tasks",dataflows.Skip(1200).Take(200));
//	var dataflow8 = new XElement("Tasks",dataflows.Skip(1400).Take(200));
//	var dataflow9 = new XElement("Tasks",dataflows.Skip(1600).Take(200));
//	var dataflow10 = new XElement("Tasks",dataflows.Skip(1800).Take(200));
//	var dataflow11 = new XElement("Tasks",dataflows.Skip(2000).Take(200));
//	var dataflow12 = new XElement("Tasks",dataflows.Skip(2200).Take(200));
//	var dataflow13 = new XElement("Tasks",dataflows.Skip(2400).Take(272));
	//.Save(String.Format(@"\\etlsvr\e$\\Districts\Middletown\Data Analysis\AS400.To.Staging\AS400.To.Staging\{0}.biml",PackageName));
	
//	
//	
//	XElement bimlGen = new XElement("Tasks",dataflows);
	
	
	//bimlGen.Save(@"\\etlsvr\e$\\Districts\Middletown\Data Analysis\AS400.To.Staging\AS400.To.Staging\Dataflows.biml");
}

// Define other methods and classes here


public static List<String> TableFilter
{
	get{
		return new List<String>(new[]{"ADEFPORG","ADEFP","ADEFP_S15","AHLPP","AHEPP","AWHLPP"
								,"HXJTLP","HXDEDP","AUSAP","AWBPP","AXBSDP","AUSPP"
								,"AXMSDP","AWBAP","AWBAPBK","AUSRP","AUSBP","ADEFPTMP"
								,"APATP","HXPTPP","HXPTPP_102","AEMFP","APGDP"});
	}
}
public class OutputBiml
{
	public int SortOrder {get;set;}
	public XElement DataflowName {get;set;}
}

public static int SortOrder(String sourceTable)
{
	var retval = int.MinValue;
	 switch(sourceTable)
	{
			case "SRAVP": retval = 1; break;
	case "SGCHP": retval = 2; break;
	case "SSCXP": retval = 3; break;
	case "STAVP": retval = 4; break;
	case "OLDCIMS": retval = 5; break;
	case "XNYTCHNP": retval = 6; break;
	case "SPDSPTMP": retval = 7; break;
	case "SCRLP0615": retval = 8; break;
	case "HJTLCSV": retval = 9; break;
	case "ACSAP": retval = 10; break;
	case "HGN2PBK": retval = 11; break;
	case "SSSBP": retval = 12; break;
	case "STPFP": retval = 13; break;
	case "SBUSP": retval = 14; break;
	case "SBRSP": retval = 15; break;
	case "HGN2P": retval = 16; break;
	case "HLUSP": retval = 17; break;
	case "XNYTCHP": retval = 18; break;
	case "HCERPV1": retval = 19; break;
	case "HCERPV2": retval = 20; break;
	case "SCAVP": retval = 21; break;
	case "SCRLP0615B": retval = 22; break;
	case "XXNY02P": retval = 23; break;
	case "XXNY01P": retval = 24; break;
	case "SCRLP": retval = 25; break;
	case "HCERP": retval = 26; break;
	case "SCAFP_BKP": retval = 27; break;
	case "SIEPP": retval = 28; break;
	case "HCNTP": retval = 29; break;
	case "SCLKP": retval = 30; break;
	case "SCRTP": retval = 31; break;
	case "HEIDP": retval = 32; break;
	case "HPRAP": retval = 33; break;
	case "SHMTP": retval = 34; break;
	case "HGNIP": retval = 35; break;
	case "STCHPBK": retval = 36; break;
	case "SPKGP": retval = 37; break;
	case "SGPAP_BKP": retval = 38; break;
	case "STCHP": retval = 39; break;
	case "SGRDP": retval = 40; break;
	case "HPC1P": retval = 41; break;
	case "SADMP": retval = 42; break;
	case "ADEFCAL": retval = 43; break;
	case "SGPAP": retval = 44; break;
	case "SBELP": retval = 45; break;
	case "HSPSP": retval = 46; break;
	case "SSACP": retval = 47; break;
	case "HQTWP_B01": retval = 48; break;
	case "ABRRP": retval = 49; break;
	case "HEEVP": retval = 50; break;
	case "SCAFP": retval = 51; break;
	case "SSTPP": retval = 52; break;
	case "SCLTP": retval = 53; break;
	case "SPCNP": retval = 54; break;
	case "STUFAM": retval = 55; break;
	case "SATHP": retval = 56; break;
	case "SLKRP": retval = 57; break;
	case "SHRHP": retval = 58; break;
	case "HJB1P": retval = 59; break;
	case "HJB2P": retval = 60; break;
	case "SPCRPTMP": retval = 61; break;
	case "BEDSSBSRP": retval = 62; break;
	case "SSSTP": retval = 63; break;
	case "SBS2P": retval = 64; break;
	case "SPCRP": retval = 65; break;
	case "SBSRP": retval = 66; break;
	case "SFSNP": retval = 67; break;
	case "BEHAVE": retval = 68; break;
	case "SCRQP0617": retval = 69; break;
	case "SSPCP": retval = 70; break;
	case "SPDMP0501": retval = 71; break;
	case "SMODP": retval = 72; break;
	case "SCMSP": retval = 73; break;
	case "SDEVP": retval = 74; break;
	case "SSGPP": retval = 75; break;
	case "SCLRP": retval = 76; break;
	case "SNTEP": retval = 77; break;
	case "SCOMP": retval = 78; break;
	case "SCRRP": retval = 79; break;
	case "SSCRP": retval = 80; break;
	case "SCRQP": retval = 81; break;
	case "SPDMP": retval = 82; break;
	case "SSYRP": retval = 83; break;
	case "SPCSP": retval = 84; break;
	case "SSFRP": retval = 85; break;
	case "ADEFPCP": retval = 86; break;
	case "LCAFP_BKP2": retval = 87; break;
	case "LCAFP_BKP": retval = 88; break;
	case "LCAFP": retval = 89; break;
	case "SSDSP": retval = 90; break;
	case "SCONP": retval = 91; break;
	case "BEDSSENRP": retval = 92; break;
	case "SENRP": retval = 93; break;
	case "SADRP": retval = 94; break;
	case "SACHP14JUN": retval = 95; break;
	case "SACHP15FEB": retval = 96; break;
	case "SACHP": retval = 97; break;
	case "SSHRP": retval = 98; break;
	case "SSTGP": retval = 99; break;
	case "SSBTP": retval = 100; break;
	case "SCROP": retval = 101; break;
	case "STRBP": retval = 102; break;
	case "HTHSP": retval = 103; break;
	case "SSCSP": retval = 104; break;
	case "SPDSP": retval = 105; break;
	case "SPCMP": retval = 106; break;
	case "SCTMP": retval = 107; break;
	case "SABSP": retval = 108; break;
	case "STAIP": retval = 109; break;
	case "SSTMP": retval = 110; break;
	}
	return retval;
}

public static XElement xConn
{
get{
	return new XElement("Connections",
		new XElement("Connection",
			new XAttribute("Name",@"SQLSASS.ECSDM_Staging"),
			new XAttribute("ConnectionString",@"Data Source=SQLSASS;Initial Catalog=ECSDM_Staging;Provider=SQLNCLI11.1;Integrated Security=SSPI;Auto Translate=False;"),
			new XElement("Annotations",
				new XElement("Annotation","OLEDB For SQLSASS Connection")
			)
		),
		new XElement("OdbcConnection",
			new XAttribute("Name",@"QSDN_172.16.8.219.Brightview"),
			new XAttribute("ConnectionString",@"uid=Brightview;Dsn=QSDN_172.16.8.219;"),
			new XElement("Annotations",
				new XElement("Annotation","ODBC For IBM AS 400 Connection")
			)
		)
	);
}
}

public XElement Packages(XElement dataFlowTasks, out string packageName)
{
	
	packageName = String.Format("{0}to{1}",
		dataFlowTasks.Elements().Descendants("OleDbSource").First().Attribute("Name").Value,
		dataFlowTasks.Elements().Descendants("OleDbSource").Last().Attribute("Name").Value
	);
	
	XElement Packages = new XElement("Packages",
		new XElement("Package", 
			new XAttribute("Name",packageName),
			new XElement("Connections",
				new XElement("Connection",
					new XAttribute("ConnectionName",@"172.16.8.219.S102187C.brightview")),
				new XElement("Connection",
					new XAttribute("ConnectionName",@"sqlsass.ECSDM_Staging1"))
					),
			dataFlowTasks
		)
	);
	
	return Packages;
}