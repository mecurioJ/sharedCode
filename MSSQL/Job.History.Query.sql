DECLARE @job_name sysname = N'EDW - Navitaire - Passenger';

WITH cte_JobHistory AS
(
SELECT
    ROW_NUMBER() OVER (PARTITION BY
                           sjh.run_date
                       ORDER BY
                           sjh.run_date
                         , sjh.instance_id
                         , sjh.run_time
                      ) seqStep
  , sjh.instance_id
  , sj.job_id
  , sjh.step_id
  , sj.name AS job_name
  , sjh.step_name
  , sjh.message
  , CASE sjh.run_status
        WHEN 0 THEN
            'Failed'
        WHEN 1 THEN
            'Succeeded'
        WHEN 2 THEN
            'Retry'
        WHEN 3 THEN
            'Canceled'
        WHEN 4 THEN
            'In Progress'
        ELSE
            NULL
    END                 AS StatusLiteral
  , sjh.run_status
  , sjh.run_date
  , CAST(CASE
        WHEN sjh.step_id = 0 THEN
            '000000'
        ELSE
			CASE LEN(sjh.run_time)
				WHEN 5 THEN CONCAT('0', CONVERT(VARCHAR (5), sjh.run_time))
				WHEN 4 THEN CONCAT('00', CONVERT(VARCHAR (4), sjh.run_time))
				WHEN 3 THEN CONCAT('000', CONVERT(VARCHAR (3), sjh.run_time))
				WHEN 2 THEN CONCAT('0000', CONVERT(VARCHAR (2), sjh.run_time))
				WHEN 1 THEN CONCAT('00000', CONVERT(VARCHAR (1), sjh.run_time))
			ELSE
				CONVERT(VARCHAR (6), sjh.run_time)
			END
    END AS VARCHAR(6))                 AS run_time
  , CASE LEN(CAST(sjh.run_duration AS VARCHAR(6)))
		WHEN 5 THEN CONCAT('0',CAST(sjh.run_duration AS VARCHAR(6)))
		WHEN 4 THEN CONCAT('00',CAST(sjh.run_duration AS VARCHAR(6)))
		WHEN 3 THEN CONCAT('000',CAST(sjh.run_duration AS VARCHAR(6)))
		WHEN 2 THEN CONCAT('0000',CAST(sjh.run_duration AS VARCHAR(6)))
		WHEN 1 THEN CONCAT('00000',CAST(sjh.run_duration AS VARCHAR(6)))
		WHEN 0 THEN CONCAT('000000',CAST(sjh.run_duration AS VARCHAR(6)))
	ELSE
	CAST(sjh.run_duration AS VARCHAR(6))
	END AS run_duration
  , sjh.retries_attempted
  , sjh.server
FROM msdb.dbo.sysjobhistory               sjh
    INNER JOIN msdb.dbo.sysjobs_view      sj
        ON sj.job_id = sjh.job_id
    LEFT OUTER JOIN msdb.dbo.sysoperators so1
        ON (sjh.operator_id_emailed = so1.id)
    LEFT OUTER JOIN msdb.dbo.sysoperators so2
        ON (sjh.operator_id_netsent = so2.id)
    LEFT OUTER JOIN msdb.dbo.sysoperators so3
        ON (sjh.operator_id_paged = so3.id)
WHERE sj.name = @job_name

  )
, cte_DateConvert AS
(
SELECT
 cte_JobHistory.instance_id
, cte_JobHistory.step_id
, cte_JobHistory.job_name
, cte_JobHistory.step_name
, cte_JobHistory.message
, cte_JobHistory.StatusLiteral
, cte_JobHistory.run_status
, TRY_CONVERT(
DATETIME,
	CONCAT(
	SUBSTRING(CAST(cte_JobHistory.run_date AS varchar(8)),1,4), '-'
	, SUBSTRING(CAST(cte_JobHistory.run_date AS varchar(8)),5,2), '-'
	, SUBSTRING(CAST(cte_JobHistory.run_date AS varchar(8)),7,2), ' '
	, SUBSTRING(cte_JobHistory.run_time,1,2) , ':'
	, SUBSTRING(cte_JobHistory.run_time,3,2)  , ':'
	, SUBSTRING(cte_JobHistory.run_time,5,2)
	) 
	) AS RunDateTime
, CONCAT(
	SUBSTRING(cte_JobHistory.run_duration,1,2), ':'
	, SUBSTRING(cte_JobHistory.run_duration,3,2), ':'
	, SUBSTRING(cte_JobHistory.run_duration,5,2)
	) RunDurationHHMMSS
, cte_JobHistory.retries_attempted
FROM cte_JobHistory
)
SELECT
* 
FROM cte_DateConvert t1
WHERE t1.RunDateTime BETWEEN
	CAST('2020-08-20 21:50:00' AS datetime) AND GETDATE()
	ORDER BY t1.RunDateTime desc


