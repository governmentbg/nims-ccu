PRINT 'Allowances'
GO

CREATE TABLE [dbo].[Allowances] (
    [AllowanceId]          INT                 NOT NULL IDENTITY,
    [Gid]                  UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Name]                 NVARCHAR(100)       NOT NULL,
    [CreateDate]           DATETIME2           NOT NULL,
    [ModifyDate]           DATETIME2           NOT NULL,
    [Version]              ROWVERSION          NOT NULL,

    CONSTRAINT [PK_Allowances] PRIMARY KEY ([AllowanceId]),
);
GO

exec spDescTable  N'Allowances', N'Надбавки.'
exec spDescColumn N'Allowances', N'AllowanceId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Allowances', N'Gid'             , N'Глобален уникален идентификатор.'
exec spDescColumn N'Allowances', N'Name'            , N'Наименование.'
exec spDescColumn N'Allowances', N'CreateDate'      , N'Дата на създаване на записа.'
exec spDescColumn N'Allowances', N'ModifyDate'      , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Allowances', N'Version'         , N'Версия.'
GO
