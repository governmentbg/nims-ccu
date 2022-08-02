PRINT 'Create jobMarkDeletedBlobs'

IF EXISTS (SELECT * FROM msdb.dbo.sysjobs WHERE name = 'jobMarkDeletedBlobs')
  BEGIN
    EXEC msdb.dbo.sp_delete_job @job_name = 'jobMarkDeletedBlobs';
  END
GO

EXEC spAddDailyJob
    @jobName = N'jobMarkDeletedBlobs',
    @jobTSQL = N'EXECUTE Eumis.dbo.spMarkDeletedBlobs',
    @startTime = 10000 -- daily at 01:00 AM

GO
