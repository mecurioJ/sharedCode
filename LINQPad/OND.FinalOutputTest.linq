<Query Kind="Program">
  <NuGetReference>Microsoft.EntityFrameworkCore.SqlServer</NuGetReference>
  <Namespace>Microsoft.Data</Namespace>
  <Namespace>Microsoft.Data.Sql</Namespace>
  <Namespace>Microsoft.Data.SqlClient</Namespace>
  <Namespace>Microsoft.Data.SqlTypes</Namespace>
</Query>

void Main()
{
	String connectionString = "Server=tcp:f9azwcazuresqlbi01.database.windows.net,1433;Database=Enki;User ID=F9BIAZUREDMSSIS@f9azwcazuresqlbi01;Password=Z&uq847=5r#zXFE;Trusted_Connection=False;Encrypt=True;";

	//log.LogInformation("C# HTTP trigger function processed a request.");

	DataSet ds = new DataSet("OND");

	List<SegmentItem> SegmentItems;
	SqlDataAdapter sda;
	DataTable PNROutput = new DataTable();


	List<Market> Markets = new List<Market>();
	List<ondBooking> bookings = new List<ondBooking>();
	using (SqlConnection conn = new SqlConnection(connectionString))
	{
		
		SqlDataReader mktRdr;

		SqlCommand mktCmd = conn.CreateCommand();
		mktCmd.CommandText = @"SELECT ONDMKT, DepartureStation, ArrivalStation FROM stg.OND_PNRTargetPaxCount";
		mktCmd.CommandType = CommandType.Text;
		mktCmd.CommandTimeout = 60;

		conn.Open();
		mktRdr = mktCmd.ExecuteReader();
		while (mktRdr.Read())
		{
			Markets.Add(new Market(mktRdr));
		}
		conn.Close();


		SqlDataReader ondbkRdr;
		SqlCommand ondBkCmd = conn.CreateCommand();
		ondBkCmd.CommandText = @"SELECT bk.RecordLocator, bk.cp1_d, bk.cp1_a, bk.cp2_d, bk.cp2_a, bk.cp3_d, bk.cp3_a, bk.cp4_d, bk.cp4_a, bk.cp5_d, bk.cp5_a, bk.cp6_d, bk.cp6_a, bk.cp7_d, bk.cp7_a, bk.cp8_d, bk.cp8_a, bk.cp9_d, bk.cp9_a, bk.cp10_d, bk.cp10_a
FROM stg.OND_Bookings bk
WHERE EXISTS (SELECT 1 FROM OND.PNRSamples ops WHERE ops.RecordLocator=bk.RecordLocator)";
		ondBkCmd.CommandType = CommandType.Text;
		ondBkCmd.CommandTimeout = 6000;
		conn.Open();

		ondbkRdr = ondBkCmd.ExecuteReader();
		while (ondbkRdr.Read())
		{
			bookings.Add(new ondBooking(ondbkRdr));
		}
		conn.Close();
	}

//bookings.Where(r => r.RecordLocator.Equals("E5YKNV")).Dump();

var ArrayBuilder = bookings//.Where(r => r.RecordLocator.Equals("E5YKNV"))
	.Select( cp => new PNRStations {
		RecordLocator = cp.RecordLocator,
		Stations = String.Format(
		@"{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
		cp.cp1_d,
		CompareArrivalDeparture(cp.cp1_a, cp.cp2_d).Aggregate((p, n) => $"{p},{n}"),
		CompareArrivalDeparture(cp.cp2_a, cp.cp3_d).Aggregate((p, n) => $"{p},{n}"),
		CompareArrivalDeparture(cp.cp3_a, cp.cp4_d).Aggregate((p, n) => $"{p},{n}"),
		CompareArrivalDeparture(cp.cp4_a, cp.cp5_d).Aggregate((p, n) => $"{p},{n}"),
		CompareArrivalDeparture(cp.cp5_a, cp.cp6_d).Aggregate((p, n) => $"{p},{n}"),
		CompareArrivalDeparture(cp.cp6_a, cp.cp7_d).Aggregate((p, n) => $"{p},{n}"),
		CompareArrivalDeparture(cp.cp7_a, cp.cp8_d).Aggregate((p, n) => $"{p},{n}"),
		CompareArrivalDeparture(cp.cp8_a, cp.cp9_d).Aggregate((p, n) => $"{p},{n}"),
		CompareArrivalDeparture(cp.cp9_a, cp.cp10_d).Aggregate((p, n) => $"{p},{n}")
		).Replace(",,",string.Empty).TrimEnd(',')
		//.Split(',').Select((c, i) => new { City = $"City{i+1}", Station = c}).Where(s => !String.IsNullOrEmpty(s.Station))
	}).GroupBy(
			g => g.RecordLocator, 
			g => g.Stations.Split(',')
				.Select((c, i) => new { City = $"City{i+1}", Station = c}).Where(s => !String.IsNullOrEmpty(s.Station))
					.ToDictionary(d => d.City, d => d.Station)
		);
	
	SegmentItems = ArrayBuilder
	.Select(ab => new SegmentItem{
		RecordLocator = ab.Key,
		City1 = ab.Select(c => c.GetValueOrDefault("City1")).FirstOrDefault(),
		Fare1 = ParseSurfaceSegment(
			Markets,
			ab.Select(c => c.GetValueOrDefault("City1")).FirstOrDefault(),
			ab.Select(c => c.GetValueOrDefault("City2")).FirstOrDefault()
			),
		City2 = ab.Select(c => c.GetValueOrDefault("City2")).FirstOrDefault(),
		Fare2 = ParseSurfaceSegment(
			Markets,
			ab.Select(c => c.GetValueOrDefault("City2")).FirstOrDefault(),
			ab.Select(c => c.GetValueOrDefault("City3")).FirstOrDefault()
			),
		City3 = ab.Select(c => c.GetValueOrDefault("City3")).FirstOrDefault(),
		Fare3 = ParseSurfaceSegment(
			Markets,
			ab.Select(c => c.GetValueOrDefault("City3")).FirstOrDefault(),
			ab.Select(c => c.GetValueOrDefault("City4")).FirstOrDefault()
			),
		City4 = ab.Select(c => c.GetValueOrDefault("City4")).FirstOrDefault(),
		Fare4 = ParseSurfaceSegment(
			Markets,
			ab.Select(c => c.GetValueOrDefault("City4")).FirstOrDefault(),
			ab.Select(c => c.GetValueOrDefault("City5")).FirstOrDefault()
			),
		City5 = ab.Select(c => c.GetValueOrDefault("City5")).FirstOrDefault(),
		Fare5 = ParseSurfaceSegment(
			Markets,
			ab.Select(c => c.GetValueOrDefault("City5")).FirstOrDefault(),
			ab.Select(c => c.GetValueOrDefault("City6")).FirstOrDefault()
			),
		City6 = ab.Select(c => c.GetValueOrDefault("City6")).FirstOrDefault(),
		Fare6 = ParseSurfaceSegment(
			Markets,
			ab.Select(c => c.GetValueOrDefault("City6")).FirstOrDefault(),
			ab.Select(c => c.GetValueOrDefault("City7")).FirstOrDefault()
			),
		City7 = ab.Select(c => c.GetValueOrDefault("City7")).FirstOrDefault(),
		Fare7 = ParseSurfaceSegment(
			Markets,
			ab.Select(c => c.GetValueOrDefault("City7")).FirstOrDefault(),
			ab.Select(c => c.GetValueOrDefault("City8")).FirstOrDefault()
			),
		City8 = ab.Select(c => c.GetValueOrDefault("City8")).FirstOrDefault(),
		Fare8 = ParseSurfaceSegment(
			Markets,
			ab.Select(c => c.GetValueOrDefault("City8")).FirstOrDefault(),
			ab.Select(c => c.GetValueOrDefault("City9")).FirstOrDefault()
			),
		City9 = ab.Select(c => c.GetValueOrDefault("City9")).FirstOrDefault(),
		Fare9 = ParseSurfaceSegment(
			Markets,
			ab.Select(c => c.GetValueOrDefault("City9")).FirstOrDefault(),
			ab.Select(c => c.GetValueOrDefault("City10")).FirstOrDefault()
			),
		City10 = ab.Select(c => c.GetValueOrDefault("City10")).FirstOrDefault()
	}).ToList();
	
	SegmentItems.Where(si => !String.IsNullOrEmpty(si.Fare3)).Dump();
	
	//Markets.Dump();
	//Bookings.Dump();
}

// Define other methods, classes and namespaces here
public string ParseSurfaceSegment(List<Market> markets, string city_a, string city_b)
{
	return markets.Where(
					mkt => mkt.DepartureStation.Equals(city_a)
					&& mkt.ArrivalStation.Equals(city_b)
				).Any()
				? "X"
				: string.IsNullOrEmpty(city_b)
					? null
					: "--";
}


class PNRStations
{
	public string RecordLocator { get; set; }
	public string Stations {get;set;}
}

private static BookingPrep[] BookingsPrep(IEnumerable<ondBooking> bookings)
{
	BookingPrep[] bookingsPrep = bookings.Select(cp =>
	{
		BookingPrep prep = new BookingPrep
		{
			RecordLocator = cp.RecordLocator,
			city1 = cp.cp1_d,
			city2_compare = CompareArrivalDeparture(cp.cp1_a, cp.cp2_d),
			city3_compare = CompareArrivalDeparture(cp.cp2_a, cp.cp3_d),
			city4_compare = CompareArrivalDeparture(cp.cp3_a, cp.cp4_d),
			city5_compare = CompareArrivalDeparture(cp.cp4_a, cp.cp5_d),
			city6_compare = CompareArrivalDeparture(cp.cp5_a, cp.cp6_d),
			city7_compare = CompareArrivalDeparture(cp.cp6_a, cp.cp7_d),
			city8_compare = CompareArrivalDeparture(cp.cp7_a, cp.cp8_d),
			city9_compare = CompareArrivalDeparture(cp.cp8_a, cp.cp9_d),
			city10_compare = CompareArrivalDeparture(cp.cp9_a, cp.cp10_d)
		};
		return prep;
	}).ToArray();
	return bookingsPrep;
}

public static string[] CompareArrivalDeparture(String arrival, String departure)
{
	return Convert.ToBoolean(String.Compare(arrival, departure))
		? !String.IsNullOrEmpty(departure) ? new[] { arrival, departure } : new[] { arrival }
		: new[] { arrival };
}
/*
private static List<SegmentItem> BuildSegment(DataSet ds)
{
	IEnumerable<ondBooking> bookings = ds.Tables["table"].Rows.Cast<DataRow>().Select(dr => new ondBooking(dr));
	IEnumerable<Market> Markets =
		ds.Tables["table1"].Rows.Cast<DataRow>().Select(dr => new Market(dr)).ToArray();

	var bookingsPrep = BookingsPrep(bookings);

	//if there are two items in the compare array, then return the item from the previous attribute,
	//else return the current attribute
	var Bookings = Enumerable(bookingsPrep);

	List<SegmentItem> SegmentItems =
		Bookings.Select(
			bk => new SegmentItem
			{
				RecordLocator = bk.RecordLocator,
				pax = 0,
				City1 = bk.city1,
				Fare1 =
					Markets.Any(mkt => mkt.DepartureStation.Equals(bk.city1) && mkt.ArrivalStation.Equals(bk.city2)),
				City2 = bk.city2,
				Fare2 = Markets.Any(mkt => mkt.DepartureStation.Equals(bk.city2) && mkt.ArrivalStation.Equals(bk.city3))
					? (bool?)true
					: String.IsNullOrEmpty(bk.city2) || String.IsNullOrEmpty(bk.city3)
						? null
						: (bool?)false,
				City3 = bk.city3,
				Fare3 = Markets.Any(mkt => mkt.DepartureStation.Equals(bk.city3) && mkt.ArrivalStation.Equals(bk.city4))
					? (bool?)true
					: String.IsNullOrEmpty(bk.city3) || String.IsNullOrEmpty(bk.city4)
						? null
						: (bool?)false,
						//OP3 = RetrieveSurfaceSegment(bk.city3, bk.city4),
						City4 = bk.city4,
				Fare4 = Markets.Any(mkt => mkt.DepartureStation.Equals(bk.city4) && mkt.ArrivalStation.Equals(bk.city5))
					? (bool?)true
					: String.IsNullOrEmpty(bk.city4) || String.IsNullOrEmpty(bk.city5)
						? null
						: (bool?)false,
						//OP4 = RetrieveSurfaceSegment(bk.city4, bk.city5),
						City5 = bk.city5,
				Fare5 = Markets.Any(mkt => mkt.DepartureStation.Equals(bk.city5) && mkt.ArrivalStation.Equals(bk.city6))
					? (bool?)true
					: String.IsNullOrEmpty(bk.city5) || String.IsNullOrEmpty(bk.city6)
						? null
						: (bool?)false,
						//OP5 = RetrieveSurfaceSegment(bk.city5, bk.city6),
						City6 = bk.city6,
				Fare6 = Markets.Any(mkt => mkt.DepartureStation.Equals(bk.city6) && mkt.ArrivalStation.Equals(bk.city7))
					? (bool?)true
					: String.IsNullOrEmpty(bk.city6) || String.IsNullOrEmpty(bk.city7)
						? null
						: (bool?)false,
						//OP6 = RetrieveSurfaceSegment(bk.city6, bk.city7),
						City7 = bk.city7,
				Fare7 = Markets.Any(mkt => mkt.DepartureStation.Equals(bk.city7) && mkt.ArrivalStation.Equals(bk.city8))
					? (bool?)true
					: String.IsNullOrEmpty(bk.city7) || String.IsNullOrEmpty(bk.city8)
						? null
						: (bool?)false,
						//OP7 = RetrieveSurfaceSegment(bk.city7, bk.city8),
						City8 = bk.city8,
				Fare8 = Markets.Any(mkt => mkt.DepartureStation.Equals(bk.city8) && mkt.ArrivalStation.Equals(bk.city9))
					? (bool?)true
					: String.IsNullOrEmpty(bk.city8) || String.IsNullOrEmpty(bk.city9)
						? null
						: (bool?)false,
						//OP8 = RetrieveSurfaceSegment(bk.city8, bk.city9),
						City9 = bk.city9,
				Fare9 = Markets.Any(
					mkt => mkt.DepartureStation.Equals(bk.city9) && mkt.ArrivalStation.Equals(bk.city10))
					? (bool?)true
					: String.IsNullOrEmpty(bk.city9) || String.IsNullOrEmpty(bk.city10)
						? null
						: (bool?)false,
						//OP9 = RetrieveSurfaceSegment(bk.city9, bk.city10),
						City10 = bk.city10
			}).ToList();
	return SegmentItems;
}

private static IEnumerable<Booking> Enumerable(BookingPrep[] bookingsPrep)
{
	IEnumerable<Booking> Bookings = bookingsPrep
		.Select(bk => new Booking
		{
			RecordLocator = bk.RecordLocator,
			city1 = bk.city1,
			city2 = bk.city2_compare[0],
			city3 = bk.city2_compare.Length > 1 ? bk.city2_compare[1] : bk.city3_compare[0],
			city4 = bk.city3_compare.Length > 1 ? bk.city3_compare[1] : bk.city4_compare[0],
			city5 = bk.city4_compare.Length > 1 ? bk.city4_compare[1] : bk.city5_compare[0],
			city6 = bk.city5_compare.Length > 1 ? bk.city5_compare[1] : bk.city6_compare[0],
			city7 = bk.city6_compare.Length > 1 ? bk.city6_compare[1] : bk.city7_compare[0],
			city8 = bk.city7_compare.Length > 1 ? bk.city7_compare[1] : bk.city8_compare[0],
			city9 = bk.city8_compare.Length > 1 ? bk.city8_compare[1] : bk.city9_compare[0],
			city10 = bk.city9_compare.Length > 1 ? bk.city9_compare[1] : bk.city10_compare[0],
		}).ToArray();
	return Bookings;
}



        //public string RetrieveRecordLocator(Int64? bookingID)
        //{
        //    return OND_RecordLocators.Where(rl => rl.BookingID.Equals(bookingID)).FirstOrDefault().RecordLocator ?? string.Empty;
        //}

        //public string RetrieveSurfaceSegment(String city1, String city2)
        //{
        //    return !String.IsNullOrEmpty(city1) && !String.IsNullOrEmpty(city2)
        //        ? OND_PNRTargetPaxCounts.ToArray().Where(mkt =>
        //            Convert.ToBoolean(String.CompareOrdinal(mkt.DepartureStation, city1)) && Convert.ToBoolean(String.CompareOrdinal(mkt.ArrivalStation, city2))
        //        ).Any()
        //            ? "X"
        //            : "--"
        //        : String.Empty;
        //}


//    }
*/
    public class ondBooking
{
	public ondBooking() { }
	public ondBooking(DataRow dr)
	{
		RecordLocator = dr["RecordLocator"].ToString();
		cp1_d = dr["cp1_d"].ToString();
		cp1_a = dr["cp1_a"].ToString();
		cp2_d = dr["cp2_d"].ToString();
		cp2_a = dr["cp2_a"].ToString();
		cp3_d = dr["cp3_d"].ToString();
		cp3_a = dr["cp3_a"].ToString();
		cp4_d = dr["cp4_d"].ToString();
		cp4_a = dr["cp4_a"].ToString();
		cp5_d = dr["cp5_d"].ToString();
		cp5_a = dr["cp5_a"].ToString();
		cp6_d = dr["cp6_d"].ToString();
		cp6_a = dr["cp6_a"].ToString();
		cp7_d = dr["cp7_d"].ToString();
		cp7_a = dr["cp7_a"].ToString();
		cp8_d = dr["cp8_d"].ToString();
		cp8_a = dr["cp8_a"].ToString();
		cp9_d = dr["cp9_d"].ToString();
		cp9_a = dr["cp9_a"].ToString();
		cp10_d = dr["cp10_d"].ToString();
		cp10_a = dr["cp10_a"].ToString();
	}

	public ondBooking(SqlDataReader dr)
	{
		RecordLocator = dr["RecordLocator"].ToString();
		cp1_d = dr["cp1_d"].ToString();
		cp1_a = dr["cp1_a"].ToString();
		cp2_d = dr["cp2_d"].ToString();
		cp2_a = dr["cp2_a"].ToString();
		cp3_d = dr["cp3_d"].ToString();
		cp3_a = dr["cp3_a"].ToString();
		cp4_d = dr["cp4_d"].ToString();
		cp4_a = dr["cp4_a"].ToString();
		cp5_d = dr["cp5_d"].ToString();
		cp5_a = dr["cp5_a"].ToString();
		cp6_d = dr["cp6_d"].ToString();
		cp6_a = dr["cp6_a"].ToString();
		cp7_d = dr["cp7_d"].ToString();
		cp7_a = dr["cp7_a"].ToString();
		cp8_d = dr["cp8_d"].ToString();
		cp8_a = dr["cp8_a"].ToString();
		cp9_d = dr["cp9_d"].ToString();
		cp9_a = dr["cp9_a"].ToString();
		cp10_d = dr["cp10_d"].ToString();
		cp10_a = dr["cp10_a"].ToString();
	}

	public string RecordLocator { get; set; }
	public string cp1_d { get; set; }
	public string cp1_a { get; set; }
	public string cp2_d { get; set; }
	public string cp2_a { get; set; }
	public string cp3_d { get; set; }
	public string cp3_a { get; set; }
	public string cp4_d { get; set; }
	public string cp4_a { get; set; }
	public string cp5_d { get; set; }
	public string cp5_a { get; set; }
	public string cp6_d { get; set; }
	public string cp6_a { get; set; }
	public string cp7_d { get; set; }
	public string cp7_a { get; set; }
	public string cp8_d { get; set; }
	public string cp8_a { get; set; }
	public string cp9_d { get; set; }
	public string cp9_a { get; set; }
	public string cp10_d { get; set; }
	public string cp10_a { get; set; }

}

public class Market
{
	public Market() { }
	public Market(DataRow dr)
	{
		ONDMKT = dr["ONDMKT"].ToString();
		DepartureStation = dr["DepartureStation"].ToString();
		ArrivalStation = dr["ArrivalStation"].ToString();
	}
	
	public Market(SqlDataReader mktRdr)
	{
		ONDMKT = mktRdr["ONDMkt"].ToString();
		DepartureStation = mktRdr["DepartureStation"].ToString();
		ArrivalStation = mktRdr["ArrivalStation"].ToString();
	}


	public string ONDMKT { get; set; }
	public string DepartureStation { get; set; }
	public string ArrivalStation { get; set; }
}


public class BookingPrep
{
	public string city1 { get; set; }
	public string[] city2_compare { get; set; }
	public string[] city3_compare { get; set; }
	public string[] city4_compare { get; set; }
	public string[] city5_compare { get; set; }
	public string[] city6_compare { get; set; }
	public string[] city7_compare { get; set; }
	public string[] city8_compare { get; set; }
	public string[] city9_compare { get; set; }
	public string[] city10_compare { get; set; }
	public string RecordLocator { get; set; }
}

public class Booking
{
	public string RecordLocator { get; set; }
	public string city1 { get; set; }
	public string city2 { get; set; }
	public string city3 { get; set; }
	public string city4 { get; set; }
	public string city5 { get; set; }
	public string city6 { get; set; }
	public string city7 { get; set; }
	public string city8 { get; set; }
	public string city9 { get; set; }
	public string city10 { get; set; }
}

public class SegmentItem
{
	public string RecordLocator { get; set; }
	public int pax { get; set; }
	public string City1 { get; set; }
	public string Fare1 { get; set; }
	public string City2 { get; set; }
	public string Fare2 { get; set; }
	public string City3 { get; set; }
	public string Fare3 { get; set; }
	public string City4 { get; set; }
	public string Fare4 { get; set; }
	public string City5 { get; set; }
	public string Fare5 { get; set; }
	public string City6 { get; set; }
	public string Fare6 { get; set; }
	public string City7 { get; set; }
	public string Fare7 { get; set; }
	public string City8 { get; set; }
	public string Fare8 { get; set; }
	public string City9 { get; set; }
	public string Fare9 { get; set; }
	public string City10 { get; set; }
}