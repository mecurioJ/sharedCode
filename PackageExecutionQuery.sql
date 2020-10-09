--F9AZWCSQLBI03
--Passenger Job is FF3F080C-BE6E-4185-803A-F67A821DABE7

DECLARE @JobID UNIQUEIDENTIFIER = 'FF3F080C-BE6E-4185-803A-F67A821DABE7';


WITH cte_packageDirectory AS
(
SELECT 
fld.folder_id
, prj.project_id
, pkg.package_id
, fld.name AS Folder
, prj.name AS Project
, pkg.name AS PackageName
, CONCAT(fld.name,'\',prj.name,'\',pkg.name) AS path
FROM ssisdb.catalog.projects prj
JOIN SSISDB.catalog.folders fld ON fld.folder_id = prj.folder_id
JOIN SSISDB.catalog.packages pkg ON pkg.project_id = prj.project_id	
)

SELECT
sj.[name] AS JobName
, sjs.step_name
--this is really long so we can find the path
, CONCAT('Ingegration Services Catalogs\SSISDB\',execs.folder_name,'\Projects\', execs.project_name,'\Packages\', execs.package_name) AS SSISPackagePath
, opsMsg.message_time
, opsMsg.message
, CASE execs.status
	WHEN 1 THEN 'Created'
	WHEN 2 THEN 'Running'
	WHEN 3 THEN 'Cancelled'
	WHEN 4 THEN 'Failed'
	WHEN 5 THEN 'Pending'
	WHEN 6 THEN 'Unexpected End'
	WHEN 7 THEN 'Succeeded'
	WHEN 8 THEN 'Stopping'
	WHEN 9 THEN 'Completed'
END AS ExecStatus
, sjs.step_id
, sjs.on_success_step_id
FROM msdb.dbo.sysjobsteps sjs
JOIN msdb.dbo.sysjobs sj ON sj.job_id = sjs.job_id
JOIN cte_packageDirectory pkg ON pkg.path = REPLACE(SUBSTRING(sjs.command,PATINDEX('%SSISDB\%\%\_%',sjs.command),CHARINDEX('\""',sjs.command)-PATINDEX('%SSISDB\%',sjs.command)),'SSISDB\','')
JOIN ssisdb.catalog.executions execs ON pkg.PackageName = execs.package_name
JOIN ssisdb.catalog.operations ops ON ops.object_id = execs.object_id
	AND ops.process_id = execs.process_id
	AND ops.start_time = execs.start_time
	AND ops.end_time = execs.end_time
	AND ops.object_type = execs.object_type
	AND ops.operation_type = execs.operation_type
JOIN ssisdb.catalog.operation_messages opsMsg ON opsMsg.operation_id = ops.operation_id
WHERE sj.job_id = @JobID
AND execs.environment_name = 'Production'
AND execs.start_time >= DATEADD(DAY,-1,GETDATE())
ORDER BY opsMsg.operation_message_id desc
, sjs.step_id DESC
, opsMsg.message_time desc
