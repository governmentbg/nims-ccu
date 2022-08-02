PRINT 'Create jobDeleteBlobs'

IF EXISTS (SELECT * FROM msdb.dbo.sysjobs WHERE name = 'jobDeleteBlobs')
  BEGIN
    EXEC msdb.dbo.sp_delete_job @job_name = 'jobDeleteBlobs';
  END
GO

EXEC spAddDailyJob
    @jobName = N'jobDeleteBlobs',
    @jobTSQL = N'EXECUTE Eumis.dbo.spDeleteBlobs',
    @startTime = 20000 -- daily at 02:00 AM

GO
