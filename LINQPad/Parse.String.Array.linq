<Query Kind="Program" />

void Main()
{
	var emailList = new []{new[]{"Joelle Aeh","joelle@cbrbrokerage.com"},
new[]{"Michael Anderson","mike2002a2002@yahoo.com"},
new[]{"Jim Anderson","jimiwanderson@yahoo.com"},
new[]{"Lisa Bader-Davidson","gabriel11817@yahoo.com"},
new[]{"Julie Baker-Caplinger","julie615@columbus.rr.com"},
new[]{"Cameron Balaban","balabanc@ddg88.navy.mil"},
new[]{"Dwayne Battle","battledwayne28@yahoo.com"},
new[]{"Lisa Benton","basketnurse@sbcglobal.net"},
new[]{"Jeff Bishop","jbishop216@yahoo.com"},
new[]{"Kim Bocook","breezyberi@aol.com"},
new[]{"Mike Brewer","mrbrewer@ameritech.net"},
new[]{"Jodi Brown-Ware","jandsware@msn.com"},
new[]{"Deedra Bryant","dbserieux@hotmail.com"},
new[]{"Chanda Bucci","chandabucci@netzero.net"},
new[]{"Kent Chatterji","kent.chatterji@gmail.com"},
new[]{"Amber Christensen-Stults","stults.amber@gmail.com"},
new[]{"Kelly Culshaw-Schneider","kellyandtroy@earthlink.net"},
new[]{"Michelle Dacons","mdacons@gmail.com"},
new[]{"Debora Dardinger","debora.c.dardinger@us.pwc.com"},
new[]{"Andrea DeCarlo-Miesse","anurse891@aol.com"},
new[]{"Chad Dinan","chaddinan@comcast.net"},
new[]{"Chris Donnell","leveragefx@gmail.com"},
new[]{"Jody Douridas-Sprague","jmsprague@insight.rr.com"},
new[]{"Michelle Duda","michelle@musemedia.info"},
new[]{"Erin Evec","eevec@rothconstruction.com"},
new[]{"Joey Filichia","mjfilichia@gmail.com"},
new[]{"Mike Fitzer","writerproducermike@yahoo.com"},
new[]{"Hope Foster","hcfoster@franklincountyohio.gov"},
new[]{"Jim Fowler","yfowler@columbus.rr.com"},
new[]{"David Gilg","dgilg@columbus.rr.com"},
new[]{"Jerri Graham","jerricgraham@yahoo.com"},
new[]{"Kyle Grate","kdgrate@worthingtonindustries.com"},
new[]{"Jason Gunn","racinjasin69@aol.com"},
new[]{"Jennifer Heise-Teal","tealmjni@mac.com"},
new[]{"Devon Immelt","devon-i@hotmail.com"},
new[]{"Heather Jackson","hsjbo@sbcglobal.net"},
new[]{"Maria Jacobs","mariaj1@bellsouth.net"},
new[]{"Michelle Karshner-Cruz","mcruz@columbus.rr.com"},
new[]{"Andy Kelly","akelly1294@wowway.com"},
new[]{"Jennifer Kish","jsk4299@yahoo.com"},
new[]{"Doug Lindgren","mumbai_doug@yahoo.com"},
new[]{"Chad Lindsey","chadkelindsey@yahoo.com"},
new[]{"Debbie Logozzo","deblogozzo@yahoo.com"},
new[]{"JR Lynch","wilecoyote.john@gmail.com"},
new[]{"Michael Mahon","dixonbanjo@wideopenwest.com"},
new[]{"Heather Marshall-Gergen","hgergen@gmail.com"},
new[]{"Michelle Middleton","shelly@middletonauctions.com"},
new[]{"Heather Moon","alliyvette@yahoo.com"},
new[]{"Cherrise Mooso-Ream","reamfam@cox.net"},
new[]{"Brian Murray","bmurray02@earthlink.net"},
new[]{"Leigh Nock-Price","price_leigh@yahoo.com"},
new[]{"Chipo Nyambuya","chnyambu@gmail.com"},
new[]{"Lance Orr","lmlorr@yahoo.com"},
new[]{"Denise Parrish","itsmeneicie@yahoo.com"},
new[]{"Jennifer Payne-Schiavone","jenschiavone@yahoo.com"},
new[]{"David Pence","resq16@insight.rr.com"},
new[]{"Kelly Petty","ckburleson@att.net"},
new[]{"George Place","gplace13@yahoo.com"},
new[]{"Brian Poole","bpoole20@yahoo.com"},
new[]{"Christine Sally","schrisisthe1@yahoo.com"},
new[]{"Eric Schreiber","eric.schreiber@gmail.com"},
new[]{"Michelle Shevlin","shellss33@cs.com"},
new[]{"Greg Shook","gmjjshook@brightchoice.net"},
new[]{"James Simpson","jamesanyc@yahoo.com"},
new[]{"Claudette Singleton-Sims","peaches770@hotmail.com"},
new[]{"Daryl Skarloken","dskarloken@versitec.com"},
new[]{"Eric Stinson","e2_stinson@yahoo.com"},
new[]{"Kenny Stout","kenny.stout@hotmail.com"},
new[]{"Chris Strickland","chris.strickland@jfs.ohio.gov"},
new[]{"Chris Stults","cstults@gmail.com"},
new[]{"Amy Sturm","missamy19@gmail.com"},
new[]{"Mark Swisshelm","m_swisshelm@yahoo.com"},
new[]{"Kim Vandemark","kim_vandemark@hotmail.com"},
new[]{"Melissa Werner-Christian","melron424@yahoo.com"},
new[]{"Dorothy White-Williams","daoww@hotmail.com"},
new[]{"Shawn Lanning","shawn@watconeng.com"},
new[]{"Dorothy White-Williams","daoww@hotmail.com"},
new[]{"Lisa Benton","northlandclassof89@sbcglobal.net"}
	};
	
var FacebookList = new List<String>(){"Patrick Foster",
"J.r. Clark",
"Stacy Krutsch-Iacobucci",
"Jim Brew Anderson",
"Teresa Firth",
"Woody Matt Woodruff",
"Robert Palmer",
"Chris Anthony",
"Mike Brewer",
"Robert Carter III",
"Rena Vance",
"Victoria Keller",
"Lisa Davidson",
"Tom Grabo",
"Deedra Bryant",
"Daryl Finley",
"Michael Anderson",
"Mike Mahan",
"Danielle Grate",
"Lyle Giacomelli",
"Jr Lynch",
"Scott Hilleary",
"Jason Crowder",
"Michelle Shevlin",
"Shelley GravesBarrow",
"Maria Jacobs",
"Ron Stackpole",
"Cody Codeman Lawrence",
"Mike Worthington",
"Leticia Jenkins",
"Tara Herrick-Farabee",
"Stacie Walker",
"Leondra Rogers-Drakeford",
"Jodi Blackburn Elswick",
"Kent Chatterji",
"Cheryl Enke-Richardson",
"Tammi Crum-Henry",
"Jeff Bishop",
"Dwayne Battle",
"Bill Wilkerson",
"Terrence Wadley",
"Tammy Walker",
"Mark A. Swisshelm",
"Amy Sturm",
"Kenny Stout",
"Eric Stinson",
"Jody Douridas Sprague",
"Daryl Skarloken",
"Wendy Markin Sipes",
"Greg Shook",
"Amber Stults",
"Eric Schreiber",
"Brian Poole",
"Davon Osborne",
"Lance Orr",
"Andrea Nichols",
"Brian Murray",
"Susie Moore-Queen",
"Michelle Middleton",
"Denise Parrish McLellan",
"Debora Dardinger McGraw",
"Kim Bocook Mayhan",
"Doug Lindgren",
"Jeannine Kraatz",
"Yuji Kotaka",
"Connie Konieczny",
"Trent Kaufman",
"Chanda Kibler",
"Heather Gergen",
"Heather Jackson",
"Heather Moon",
"Stacy Hayes-Jeter",
"Ron Isbell",
"Devon Immelt",
"Paul Heiserman",
"Debbie Harlow Logozzo",
"Richard Green",
"Rick Naumoff",
"Nicole Tyler Griffin",
"Jerri Graham",
"Jim Fowler",
"Joey Filichia",
"Chris Donnell",
"Jeff Walker Crone",
"Tim Christian",
"Kelly Burleson",
"Phetmany Brower",
"Virginia Clagg",
"Vicki Benson Truck",
"Tina SpiritWind Basora",
"Cameron M. Balaban",
"Julie Baker-Caplinger",
"David Dennis",
"David Pence",
"Scott Pritchard",
"Richard Bitonte",
"Ronnika Sunshine Wilkey",
"Leigh Nock Price",
"Michelle Dacons",
"Michelle Duda",
"Cherrise Mooso Ream",
"Jodi Brown-Ware",
"Erin Evec-Lemity",
"Donna Suren",
"Andrea Miesse DeCarlo",
"Melissa Christian",
"Jennifer Heise Teal",
"Jennifer Sue",
"Vernon Bell",
"James A Simpson",
"Kim Ebelberger Vandemark",
"Shawn Lanning",
"Lisa Nash-Benton"};

var reunioncom = new[]{
new[]{"mike2002a2002","Michael Anderson"},
new[]{"cinati1966","Kim Barnes"},
new[]{"sgbarrow","Shelley Barrow"},
new[]{"jennycochenour","Jennifer Beck"},
new[]{"tnjhappyhouse","Jenny Beck"},
new[]{"lisanash","Lisa Benton"},
new[]{"jbishop216","Jeff Bishop"},
new[]{"klburnette3","Kelly Burnette"},
new[]{"cabungcc","Christi Cabungcal"},
new[]{"julie6151971","Julie Caplinger"},
new[]{"cronej2","Jeff Crone"},
new[]{"mrd89","Michelle Dacons"},
new[]{"directjason","Jason Davis"},
new[]{"coachd32","David Dennis Sr."},
new[]{"jodyd2","Jody Douridas"},
new[]{"hate13","Kimberly Doyle"},
new[]{"ldrakeford","Leondra Drakeford"},
new[]{"butterflies555","Tara Farabee"},
new[]{"sfeldpusch","Shawn Feldpusch"},
new[]{"mjfilichia5362w","Joey Filichia"},
new[]{"mjfilichia","Mario Filichia"},
new[]{"classclown1989","Daryl Finley"},
new[]{"tinamarie5","Tina Geygan"},
new[]{"catsmama","Jerri Graham"},
new[]{"racinjasin69","Jason Gunn"},
new[]{"blaquehawk5171","Patrice Hawkins"},
new[]{"www720","Stacy Hayes"},
new[]{"jdhenders","Jeff Henderson"},
new[]{"hendersonjd","Jeffrey Henderson"},
new[]{"1571","Tamra Henry"},
new[]{"scotthilleary","Scott Hilleary"},
new[]{"stacymhayes","Stacy Jeter"},
new[]{"tylerjennifer","Jennifer Kish"},
new[]{"ykoyaka","Yuji Kotaka"},
new[]{"spoiledlilb1tch","Melissa Kreider"},
new[]{"doug5371","Douglas Lindgren"},
new[]{"logozzod","Debbie Logozzo"},
new[]{"dixonbanjo","Mike Mahan"},
new[]{"mikemarshall","Mike Marshall"},
new[]{"tinar7","Tina Mattox"},
new[]{"breezyberi","Kimberly Mayhan"},
new[]{"breezyberi1","Kimberly Mayhan"},
new[]{"wwwkevonmangc","Kevin McKitrick"},
new[]{"neicieintx","Denise McLellan"},
new[]{"mlm514","Michelle Middleton"},
new[]{"andee1580","Andrea Miesse"},
new[]{"miamouse1","Alissa Moore"},
new[]{"bmurray1989","Brian Murray"},
new[]{"neicie226","Denise Parrish-McLellan"},
new[]{"dpence1","David Pence"},
new[]{"resq161","David Pence"},
new[]{"herc16","Tyrone Preston"},
new[]{"leighp70","Leigh Price"},
new[]{"bud83","Antoinette Pyett"},
new[]{"susie510","Susan Queen"},
new[]{"cherriseream1989","Cherrise Ream"},
new[]{"noyesha","Nicole Renee"},
new[]{"w_shelton812","Weslynne Russell"},
new[]{"weslynne11","Weslynne Russell"},
new[]{"jamesa89","Jim Simpson"},
new[]{"wlsipes","Wendy Sipes"},
new[]{"wsipes","Wendy Sipes"},
new[]{"4lisa","Lisa Smith"},
new[]{"slade_smith","Slade Smith"},
new[]{"cstrickly","Chris Strickland"},
new[]{"akaauntie","Amy Sturm"},
new[]{"surend","Donna Suren"},
new[]{"marjont1","Marty Tucker"},
new[]{"vandemark14","Kim Vandemark"},
new[]{"swalk9","Stacie Walker"},
new[]{"tfwnorthland","Tamela (Tammy) Walker"},
new[]{"tzwalker","Tamela Walker"},
new[]{"94suzanne","Jodi Ware"},
new[]{"bwilkserson","Bill Wilkerson"},
new[]{"sprout027","Kelly Wise"},
};

var reunion = 
reunioncom.Select(rc => new Person{
	Name = rc[1],
	NameSplit = rc[1].Split(' '),
	FirstName = rc[1].Split(' ')[0] ?? String.Empty,
	LastName = rc[1].Split(' ')[1] ?? String.Empty,
	}).Distinct().ToList();

var email = emailList.Select(t => 
	new Person { 
		Name = t[0], 
		Email = t[1], 
		NameSplit = t[0].Split(' '),
	FirstName = t[0].Split(' ')[0] ?? String.Empty,
	LastName = t[0].Split(' ')[1] ?? String.Empty
	}
	).ToList();
	
var facebook = FacebookList.Select(li => new Person{
	Name = li,
	NameSplit = li.Split(' '),
	FirstName = li.Split(' ')[0] ?? String.Empty,
	LastName = li.Split(' ')[1] ?? String.Empty
}).ToList();

//e2.Dump();
facebook.Select(fb => new{
	fbName = fb.Name,
	emName = email.Where(em => (em.Name.CompareTo(fb.Name) == 0)).Any()
});

email.Select(em => new{
	emName = em.Name,
	HasMatch = facebook.Where(fb => (fb.Name.CompareTo(em.Name) == 0)).Distinct(),
	em.Email
}).Where(t1 => !t1.HasMatch.Any()).Select(t2 => new{t2.emName,t2.Email}).Distinct();



reunion
.Select(rc => new{
	rc.Name,
	intersect = 
	(facebook.Select(fb => fb.LastName.Contains(rc.LastName) && fb.FirstName.Contains(rc.FirstName)).Distinct().Count() == 1)
	&&
	(email.Select(fb => fb.LastName.Contains(rc.LastName) && fb.FirstName.Contains(rc.FirstName)).Distinct().Count() == 1)
	
}).Distinct().Where(tx => tx.intersect)
.Select(nm => nm.Name);

facebook.Where(nm => nm.Name.Contains("Tucker")).Dump();
//e1.Where(li => !e2.Contains(li.name.ToString())).Dump();
}

// Define other methods and classes here

public class Person
{
	public String Name {get;set;}
	public String[] NameSplit {get;set;}
	public String FirstName {get;set;}
	public String LastName {get;set;}
	public String Email {get;set;}
}
