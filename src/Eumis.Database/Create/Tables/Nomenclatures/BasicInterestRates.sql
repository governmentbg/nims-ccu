PRINT 'BasicInterestRates'
GO

CREATE TABLE [dbo].[BasicInterestRates] (
    [BasicInterestRateId]  INT                 NOT NULL IDENTITY,
    [Gid]                  UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Name]                 NVARCHAR(100)       NOT NULL,
    [CreateDate]           DATETIME2           NOT NULL,
    [ModifyDate]           DATETIME2           NOT NULL,
    [Version]              ROWVERSION          NOT NULL,

    CONSTRAINT [PK_BasicInterestRates] PRIMARY KEY ([BasicInterestRateId]),
);
GO

exec spDescTable  N'BasicInterestRates', N'Основен лихвен процент.'
exec spDescColumn N'BasicInterestRates', N'BasicInterestRateId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'BasicInterestRates', N'Gid'                     , N'Глобален уникален идентификатор.'
exec spDescColumn N'BasicInterestRates', N'Name'                    , N'Наименование.'
exec spDescColumn N'BasicInterestRates', N'CreateDate'              , N'Дата на създаване на записа.'
exec spDescColumn N'BasicInterestRates', N'ModifyDate'              , N'Дата на последно редактиране на записа.'
exec spDescColumn N'BasicInterestRates', N'Version'                 , N'Версия.'
GO
