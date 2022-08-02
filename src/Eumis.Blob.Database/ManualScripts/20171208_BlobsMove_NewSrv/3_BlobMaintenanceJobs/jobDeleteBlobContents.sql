PRINT 'Create jobDeleteBlobContents'

IF EXISTS (SELECT * FROM msdb.dbo.sysjobs WHERE name = 'jobDeleteBlobContents')
  BEGIN
    EXEC msdb.dbo.sp_delete_job @job_name = 'jobDeleteBlobContents';
  END
GO

EXEC spAddDailyJob
    @jobName = N'jobDeleteBlobContents',
    @jobTSQL = N'EXECUTE [$(dbName)].dbo.spDeleteBlobContents',
    @startTime = 23000 -- daily at 02:30 AM

GO
