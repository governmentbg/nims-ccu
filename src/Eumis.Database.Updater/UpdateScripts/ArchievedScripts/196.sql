GO

THROW 50000,'The script cannot be run by the dbupdater. Run the rest of the script manually.',1
GO

-- Mark script as applied
IF NOT EXISTS (SELECT * FROM [dbo].[UpdateScripts] WHERE [ScriptName] = '196')
  BEGIN
    INSERT INTO [dbo].[UpdateScripts] ([ScriptName], [Applied]) VALUES ('196', GETDATE())
  END
GO

-- ActionLogs
ALTER DATABASE [Eumis] ADD FILEGROUP [FG_ActionLogData]
GO

DECLARE @SQL NVARCHAR(MAX) = ''

SELECT @SQL = @SQL + '
  ALTER DATABASE [Eumis] ADD FILE
  (
      NAME = [Eumis_ActionLogData],
      FILENAME = ''' + CAST(SERVERPROPERTY('INSTANCEDEFAULTDATAPATH') AS NVARCHAR(MAX)) + 'Eumis_ActionLogData.mdf''
  )
  TO FILEGROUP [FG_ActionLogData]';

EXEC sp_executesql @SQL
GO

-- Partitions by quarter starting from Q1 2015
CREATE PARTITION FUNCTION pfActionLogs (datetime2)
AS RANGE RIGHT FOR VALUES (
  '20150101', '20150401', '20150701', '20151001',
  '20160101', '20160401', '20160701', '20161001',
  '20170101', '20170401', '20170701', '20171001',
  '20180101', '20180401', '20180701', '20181001',
  '20190101', '20190401', '20190701', '20191001',
  '20200101', '20200401', '20200701', '20201001',
  '20210101', '20210401', '20210701', '20211001',
  '20220101', '20220401', '20220701', '20221001'
);
GO

CREATE PARTITION SCHEME psActionLogs
AS PARTITION pfActionLogs ALL TO ([FG_ActionLogData]);
GO

CREATE TABLE [dbo].[ActionLogs](
    [LogDate]                   DATETIME2          NOT NULL,
    [ActionLogId]               INT                NOT NULL IDENTITY,
    [ActionLogType]             INT                NOT NULL,
    [Action]                    NVARCHAR (100)     NOT NULL,
    [AggregateRootId]           INT                NULL,
    [ChildRootId]               INT                NULL,
    [Username]                  NVARCHAR (200)     NULL,
    [RegistrationEmail]         NVARCHAR (200)     NULL,
    [ContractRegistrationEmail] NVARCHAR (200)     NULL,
    [ContractAccessCodeEmail]   NVARCHAR (200)     NULL,
    [PostData]                  NVARCHAR (MAX)     NULL,
    [ResponseData]              NVARCHAR (MAX)     NULL,
    [RawUrl]                    NVARCHAR (MAX)     NOT NULL,
    [RequestId]                 UNIQUEIDENTIFIER   NULL,
    [RemoteIpAddress]           NVARCHAR (50)      NOT NULL,
    CONSTRAINT [PK_ActionLogs]                  PRIMARY KEY ([LogDate] ASC, [ActionLogId] ASC),
    CONSTRAINT [CHK_ActionLogs_ActionLogType]   CHECK       ([ActionLogType] IN (1, 2, 3)),
)
ON psActionLogs([LogDate]);
GO

-- Blobs
ALTER TABLE [dbo].[BlobContentLocations] DROP CONSTRAINT [UQ_BlobContentLocations_Hash_Size]
GO

ALTER TABLE [dbo].[BlobContentLocations] ADD
    [PartitionId]   INT         NOT NULL CONSTRAINT [DEFAULT_PartitionId] DEFAULT 0,
    [IsDeleted]     BIT         NOT NULL CONSTRAINT [DEFAULT_IsDeleted]   DEFAULT 0,
    [CreateDate]    DATETIME2   NOT NULL CONSTRAINT [DEFAULT_CreateDate]  DEFAULT GETDATE(),
    [DeleteDate]    DATETIME2   NULL
GO

ALTER TABLE [dbo].[BlobContentLocations] DROP CONSTRAINT [DEFAULT_PartitionId]
GO

ALTER TABLE [dbo].[BlobContentLocations] DROP CONSTRAINT [DEFAULT_IsDeleted]
GO

ALTER TABLE [dbo].[BlobContentLocations] DROP CONSTRAINT [DEFAULT_CreateDate]
GO

ALTER TABLE [dbo].[Blobs] ADD
    [IsDeleted]     BIT         NOT NULL CONSTRAINT [DEFAULT_IsDeleted]   DEFAULT 0,
    [CreateDate]    DATETIME2   NOT NULL CONSTRAINT [DEFAULT_CreateDate]  DEFAULT GETDATE(),
    [DeleteDate]    DATETIME2   NULL
GO

ALTER TABLE [dbo].[Blobs] DROP CONSTRAINT [DEFAULT_IsDeleted]
GO

ALTER TABLE [dbo].[Blobs] DROP CONSTRAINT [DEFAULT_CreateDate]
GO

UPDATE [dbo].[BlobContentLocations]
SET [PartitionId] = 100 + [BlobContentId] % 16
WHERE [PartitionId] = 0
GO

CREATE UNIQUE INDEX [UQ_BlobContentLocations_Hash_Size]
    ON [dbo].[BlobContentLocations]([Hash], [Size]) WHERE [IsDeleted] = 0
GO

ALTER SEQUENCE [dbo].[BlobContentSequence] RESTART WITH 2000000
GO

---------------------------------------------------------------
--spDeleteBlobs
---------------------------------------------------------------

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteBlobs')
  BEGIN
    DROP PROCEDURE spDeleteBlobs
  END
GO

CREATE PROCEDURE spDeleteBlobs
AS

SET NOCOUNT ON;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

BEGIN TRANSACTION;

BEGIN TRY

    -- Delete marked blobs older than 40 days
    DELETE b
    FROM Blobs b
    WHERE
      b.IsDeleted = 1 AND b.DeleteDate < DATEADD(day, -30, GETDATE())

    PRINT 'Deleted ' + CAST(@@ROWCOUNT AS NVARCHAR(20)) + ' Blobs'

    -- Delete marked blob content locations older than 40 days
    DELETE bcl
    FROM BlobContentLocations bcl
    WHERE
      bcl.IsDeleted = 1 AND bcl.DeleteDate < DATEADD(day, -30, GETDATE())

    PRINT 'Deleted ' + CAST(@@ROWCOUNT AS NVARCHAR(20)) + ' BlobContentLocations'
  
    COMMIT;
END TRY
BEGIN CATCH

    DECLARE @error int,
            @message varchar(4000);

    SELECT
        @error = ERROR_NUMBER(),
        @message = ERROR_MESSAGE();

    ROLLBACK;

    RAISERROR ('An error ocurred in spDeleteBlobs: %d: %s', 16, 1, @error, @message);
END CATCH;

GO

---------------------------------------------------------------
--spMarkDeletedBlobs
---------------------------------------------------------------

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spMarkDeletedBlobs')
  BEGIN
    DROP PROCEDURE spMarkDeletedBlobs
  END
GO

CREATE PROCEDURE spMarkDeletedBlobs
AS

SET NOCOUNT ON;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

BEGIN TRANSACTION;

BEGIN TRY

    CREATE TABLE #UsedBlobs ([Key] UNIQUEIDENTIFIER, [SourceTable] NVARCHAR(200))
    DECLARE @SQL NVARCHAR(MAX) = ''

    --get used blobs
    SELECT @SQL = @SQL +
      'INSERT INTO #UsedBlobs ([Key], [SourceTable]) SELECT ' +
      COL_NAME(col.parent_object_id, col.parent_column_id) + ' AS [Key], ' +
      '''' + OBJECT_NAME(fk.parent_object_id) + ''' AS [SourceTable] ' +
      'FROM ' + OBJECT_NAME(fk.parent_object_id) + ' ' +
      'WHERE ' + COL_NAME(col.parent_object_id, col.parent_column_id) + ' IS NOT NULL;'
    FROM sys.foreign_keys AS fk
    INNER JOIN sys.foreign_key_columns AS col
        ON fk.object_id = col.constraint_object_id
    WHERE 
        OBJECT_NAME(fk.referenced_object_id) = 'Blobs'

    EXEC sp_ExecuteSQL @SQL

    -- Mark deleted blobs
    UPDATE b SET
      b.IsDeleted = 1,
      b.DeleteDate = GETDATE()
    FROM Blobs b
    WHERE
      b.IsDeleted = 0 AND
      NOT EXISTS (SELECT NULL FROM #UsedBlobs ub WHERE ub.[Key] = b.[Key]) AND
      b.CreateDate < DATEADD(day, -2, GETDATE())

    PRINT 'Marked ' + CAST(@@ROWCOUNT AS NVARCHAR(20)) + ' Blobs as deleted'

    -- Mark deleted blob content locations
    UPDATE bcl SET
      bcl.IsDeleted = 1,
      bcl.DeleteDate = GETDATE()
    FROM BlobContentLocations bcl
    WHERE
      bcl.IsDeleted = 0 AND
      NOT EXISTS (SELECT NULL FROM Blobs b WHERE b.IsDeleted = 0 AND bcl.BlobContentLocationId = b.BlobContentLocationId)

    PRINT 'Marked ' + CAST(@@ROWCOUNT AS NVARCHAR(20)) + ' BlobContentLocations as deleted'

    DROP TABLE #UsedBlobs;

    COMMIT;
END TRY
BEGIN CATCH

    DECLARE @error int,
            @message varchar(4000);

    SELECT
        @error = ERROR_NUMBER(),
        @message = ERROR_MESSAGE();

    ROLLBACK;

    RAISERROR ('An error ocurred in spMarkDeletedBlobs: %d: %s', 16, 1, @error, @message);
END CATCH;

GO

---------------------------------------------------------------
--spAddDailyJob
---------------------------------------------------------------
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

---------------------------------------------------------------
--jobDeleteBlobs
---------------------------------------------------------------
EXEC spAddDailyJob
    @jobName = N'jobDeleteBlobs',
    @jobTSQL = N'EXECUTE Eumis.dbo.spDeleteBlobs',
    @startTime = 20000 -- daily at 02:00 AM

GO

---------------------------------------------------------------
--jobMarkDeletedBlobs
---------------------------------------------------------------
EXEC spAddDailyJob
    @jobName = N'jobMarkDeletedBlobs',
    @jobTSQL = N'EXECUTE Eumis.dbo.spMarkDeletedBlobs',
    @startTime = 10000 -- daily at 01:00 AM

GO
