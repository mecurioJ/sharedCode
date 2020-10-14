<Query Kind="SQL">
  <Connection>
    <ID>f268594a-d7ea-477c-8f43-f7e6c4adfcdc</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>Consulting</Database>
  </Connection>
</Query>

--TFS 332 filtering Bug.
--TFS 616 Pack List



EXEC dbo.GetHours 'Latitude40', '11/03/2014 12:00:00 AM' 
EXEC dbo.WeekSchedule 'Latitude40', '11/03/2014 12:00:00 AM' 

/*
-- 06:00:00 17:00:00 Latitude40  Ten Day Excel Model & Populate 464 1201 

DECLARE 
	@EntryDate datetime, @StartTime time(7), @EndTime time(7), @ClientName varchar(500), @TaskItem varchar(500), @TaskDetails varchar(MAX), @TFSId int, @OnTimeId int

SELECT
	@EntryDate = getdate()
	, @ClientName = 'Latitude40'
--	, @starttime = '17:00'
--	, @endtime = '18:30'
--	, @taskitem = 'Show Cuttings Column'
--	, @taskdetails = 'Pick List'
--	, @tfsid = 800
--	, @ontimeid = 1304

	, @starttime = '08:30'	
	, @endtime = '08:45'
	, @taskitem =	'morning stand up'
	, @taskdetails = 'what did you do, what are you doing'
	, @tfsid = null
	, @ontimeid = 1208

EXECUTE [dbo].[AddTimeEntry]  @EntryDate  ,@StartTime  ,@EndTime  ,@ClientName  ,@TaskItem  ,@TaskDetails  ,@TFSId  ,@OnTimeId
GO




*/

--select * from dbo.Timesheet where TaskItem like '%Ten Day%'
--
--SELECT CONVERT(VARCHAR(20),CAST(EntryDate as DAte),101),	SUM(DATEDIFF(mi,StartTime,EndTime))/60.0 Hours, OnTimeId
--from [dbo].[Timesheet] Where OnTimeId = 1208
--AND EntryDate > '09/21/2014' AND EntryDate <= getdate()
--group by CONVERT(VARCHAR(20),CAST(EntryDate as DAte),101), OnTimeId
--
--SELECT 
--CONVERT(VARCHAR(20),CAST(EntryDate as DAte),101),	SUM(DATEDIFF(mi,StartTime,EndTime))/60.0 Hours, OnTimeId
--,TaskItem,TaskDetails
--from [dbo].[Timesheet] where EntryDate > '09/21/2014' AND EntryDate <= getdate()
--AND OnTimeId NOT IN (840,852,853,1114,1201)
--group by OnTimeId, CONVERT(VARCHAR(20),CAST(EntryDate as DAte),101),TaskItem,TaskDetails
--order by  CONVERT(VARCHAR(20),CAST(EntryDate as DAte),101) desc


/*
Excel = 1114
Backend = 1201

*/

--Update Timesheet SET StartTime = '08:30' WHERE EntryIndex = 4170
--Update Timesheet SET EndTime = '19:30' WHERE EntryIndex = 5218
--Update Timesheet SET TFSId = '833',OnTimeId = NULL WHERE EntryIndex IN (5218)
--Update Timesheet SET OnTimeId = '853' WHERE EntryIndex IN (2106,3120)
--Update Timesheet Set EntryDate =getdate() where EntryIndex IN (5217)


--Update Timesheet Set TaskItem = 'Code Cleanup' where EntryIndex = 5217


--UPDATE Timesheet  
--SET 
----StartTime = '06:00:00'
----	, EndTime = ''
--	 TaskItem ='Stick Screen'
--	, TaskDetails = 'UI Bugs'
----	, TFSId = 473
----	, OnTimeId = 853
--WHERE EntryIndex = 2105

--SET 
--TFSId = 331
--OnTimeId = 1143
--, TaskItem = 'Spike Research Reporting Tools'
--, TaskDetails = 'Build Query'
--WHERE EntryIndex = 1071
--WHERE TaskItem = 'RC Availability Page to Excel'


--DELETE FROM Timesheet  WHERE EntryIndex = 3157