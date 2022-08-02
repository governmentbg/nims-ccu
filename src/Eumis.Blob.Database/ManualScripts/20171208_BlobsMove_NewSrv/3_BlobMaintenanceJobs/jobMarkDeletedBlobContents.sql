PRINT 'Create jobMarkDeletedBlobContents'

IF EXISTS (SELECT * FROM msdb.dbo.sysjobs WHERE name = 'jobMarkDeletedBlobContents')
  BEGIN
    EXEC msdb.dbo.sp_delete_job @job_name = 'jobMarkDeletedBlobContents';
  END
GO

EXEC spAddDailyJob
    @jobName = N'jobMarkDeletedBlobContents',
    @jobTSQL = N'EXECUTE [$(dbName)].dbo.spMarkDeletedBlobContents N''EumisSrv.Eumis''',
    @startTime = 13000 -- daily at 01:30 AM

GO
