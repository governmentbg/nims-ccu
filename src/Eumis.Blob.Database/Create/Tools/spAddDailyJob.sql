IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spAddDailyJob')
  BEGIN
    DROP PROCEDURE spAddDailyJob
  END
GO

CREATE PROCEDURE spAddDailyJob
(
  @jobName NVARCHAR(200),
  @jobTSQL NVARCHAR(200),
  @startTime INT
)
AS

EXEC msdb.dbo.sp_add_job
    @job_name = @jobName;

EXEC msdb.dbo.sp_add_jobstep
    @job_name = @jobName,
    @step_name = N'Run job TSQL',
    @subsystem = N'TSQL',
    @command = @jobTSQL

-- Run daily at @startTime
EXEC msdb.dbo.sp_add_jobschedule
    @job_name = @jobName,
    @name = 'DailySchedule',
    @freq_type= 4,
    @freq_interval= 1,
    @active_start_time = @startTime

EXEC msdb.dbo.sp_add_jobserver
    @job_name =  @jobName;

GO
