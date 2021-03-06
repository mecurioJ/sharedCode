<Query Kind="Program" />

void Main()
{

}

// Define other methods and classes here

	public static string[] SqlReservedKeywords
	        {
	            get
	            {
	
	                return new []{
	                    "ADD","EXTERNAL","PROCEDUREALL","FETCH","PUBLICALTER","FILE","RAISERRORAND",
	                    "FILLFACTOR","READANY","FOR","READTEXTAS","FOREIGN","RECONFIGUREASC","FREETEXT",
	                    "REFERENCESAUTHORIZATION","FREETEXTTABLE","REPLICATIONBACKUP","FROM","RESTOREBEGIN",
	                    "FULL","RESTRICTBETWEEN","FUNCTION","RETURNBREAK","GOTO","REVERTBROWSE","GRANT",
	                    "REVOKEBULK","GROUP","RIGHTBY","HAVING","ROLLBACKCASCADE","HOLDLOCK","ROWCOUNTCASE",
	                    "IDENTITY","ROWGUIDCOLCHECK","IDENTITY_INSERT","RULECHECKPOINT","IDENTITYCOL",
	                    "SAVECLOSE","IF","SCHEMACLUSTERED","IN","SECURITYAUDITCOALESCE","INDEX","SELECTCOLLATE",
	                    "INNER","SEMANTICKEYPHRASETABLECOLUMN","INSERT","SEMANTICSIMILARITYDETAILSTABLECOMMIT",
	                    "INTERSECT","SEMANTICSIMILARITYTABLECOMPUTE","INTO","SESSION_USERCONSTRAINT","IS",
	                    "SETCONTAINS","JOIN","SETUSERCONTAINSTABLE","KEY","SHUTDOWNCONTINUE","KILL","SOMECONVERT",
	                    "LEFT","STATISTICSCREATE","LIKE","SYSTEM_USERCROSS","LINENO","TABLECURRENT","LOAD",
	                    "TABLESAMPLECURRENT_DATE","MERGE","TEXTSIZECURRENT_TIME","NATIONAL","THENCURRENT_TIMESTAMP",
	                    "NOCHECK","TOCURRENT_USER","NONCLUSTERED","TOPCURSOR","NOT","TRANDATABASE","NULL",
	                    "TRANSACTIONDBCC","NULLIF","TRIGGERDEALLOCATE","OF","TRUNCATEDECLARE","OFF","TRY_CONVERTDEFAULT",
	                    "OFFSETS","TSEQUALDELETE","ON","UNIONDENY","OPEN","UNIQUEDESC","OPENDATASOURCE","UNPIVOTDISK",
	                    "OPENQUERY","UPDATEDISTINCT","OPENROWSET","UPDATETEXTDISTRIBUTED","OPENXML","USEDOUBLE",
	                    "OPTION","USERDROP","OR","VALUESDUMP","ORDER","VARYINGELSE","OUTER","VIEWEND","OVER",
	                    "WAITFORERRLVL","PERCENT","WHENESCAPE","PIVOT","WHEREEXCEPT","PLAN","WHILEEXEC","PRECISION",
	                    "WITHEXECUTE","PRIMARY","WITHIN GROUPEXISTS","PRINT","WRITETEXTEXIT","PROC"
	                    };
	            }
	        }
	        public static string[] MdxReservedKeywords
	        {
	            get
	            {
	
	                return new []{
	                    "ABSOLUTE","DESC","LEAVES","SELF_BEFORE_AFTERACTIONPARAMETERSET","DESCENDANTS","LEVEL",
	                    "SESSIONADDCALCULATEDMEMBERS","DESCRIPTION","LEVELS","SETAFTER","DIMENSION","LINKMEMBER",
	                    "SETTOARRAYAGGREGATE","DIMENSIONS","LINREGINTERCEPT","SETTOSTRALL","DISTINCT","LINREGPOINT",
	                    "SORTALLMEMBERS","DISTINCTCOUNT","LINREGR2","STDDEVANCESTOR","DRILLDOWNLEVEL","LINREGSLOPE",
	                    "STDDEVPANCESTORS","DRILLDOWNLEVELBOTTOM","LINREGVARIANCE","STDEVAND","DRILLDOWNLEVELTOP",
	                    "LOOKUPCUBE","STDEVPAS","DRILLDOWNMEMBER","MAX","STORAGEASC","DRILLDOWNMEMBERBOTTOM",
	                    "MEASURE","STRIPCALCULATEDMEMBERSASCENDANTS","DRILLDOWNMEMBERTOP","MEDIAN","STRTOMEMBERAVERAGE",
	                    "DRILLUPLEVEL","MEMBER","STRTOSETAXIS","DRILLUPMEMBER","MEMBERS","STRTOTUPLEBASC","DROP",
	                    "MEMBERTOSTR","STRTOVALBDESC","EMPTY","MIN","STRTOVALUEBEFORE","END","MTD","SUBSETBEFORE_AND_AFTER",
	                    "ERROR","NAME","SUMBOTTOMCOUNT","EXCEPT","NAMETOSET","TAILBOTTOMPERCENT","EXCLUDEEMPTY","NEST",
	                    "THISBOTTOMSUM","EXTRACT","NEXTMEMBER","TOGGLEDRILLSTATEBY","FALSE","NO_ALLOCATION","TOPCOUNTCACHE",
	                    "FILTER","NO_PROPERTIES","TOPPERCENTCALCULATE","FIRSTCHILD","NON","TOPSUMCALCULATION","FIRSTSIBLING",
	                    "NONEMPTYCROSSJOIN","TOTALSCALCULATIONCURRENTPASS","FOR","NOT_RELATED_TO_FACTS",
	                    "TREECALCULATIONPASSVALUE","FREEZE","NULL","TRUECALCULATIONS","FROM","ON","TUPLETOSTRCALL",
	                    "GENERATE","OPENINGPERIOD","TYPECELL","GLOBAL","OR","UNIONCELLFORMULASETLIST","GROUP","PAGES",
	                    "UNIQUECHAPTERS","GROUPING","PARALLELPERIOD","UNIQUENAMECHILDREN","HEAD","PARENT","UPDATECLEAR",
	                    "HIDDEN","PASS","USECLOSINGPERIOD","HIERARCHIZE","PERIODSTODATE","USE_EQUAL_ALLOCATIONCOALESCEEMPTY",
	                    "HIERARCHY","POST","USE_WEIGHTED_ALLOCATIONCOLUMN","IGNORE","PREDICT","USE_WEIGHTED_INCREMENTCOLUMNS",
	                    "IIF","PREVMEMBER","USERNAMECORRELATION","INCLUDEEMPTY","PROPERTIES","VALIDMEASURECOUNT","INDEX",
	                    "PROPERTY","VALUECOUSIN","INTERSECT","QTD","VARCOVARIANCE","IS","RANK","VARIANCECOVARIANCEN",
	                    "ISANCESTOR","RECURSIVE","VARIANCEPCREATE","ISEMPTY","RELATIVE","VARPCREATEPROPERTYSET","ISGENERATION",
	                    "ROLLUPCHILDREN","VISUALCREATEVIRTUALDIMENSION","ISLEAF","ROOT","VISUALTOTALSCROSSJOIN","ISSIBLING",
	                    "ROWS","WHERECUBE","ITEM","SCOPE","WITHCURRENT","LAG","SECTIONS","WTDCURRENTCUBE","LASTCHILD",
	                    "SELECT","XORCURRENTMEMBER","LASTPERIODS","SELF","YTDDEFAULT_MEMBER","LASTSIBLING","SELF_AND_AFTER",
	                    "DEFAULTMEMBER","LEAD","SELF_AND_BEFORE"
	                    };
	            }
	        }