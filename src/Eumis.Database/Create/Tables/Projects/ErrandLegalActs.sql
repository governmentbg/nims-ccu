PRINT 'ErrandLegalActs'
GO

CREATE TABLE [dbo].[ErrandLegalActs] (
    [ErrandLegalActId]  INT              NOT NULL IDENTITY,
    [Gid]               UNIQUEIDENTIFIER NOT NULL UNIQUE,
    [Name]              NVARCHAR(MAX)    NOT NULL,

    CONSTRAINT [PK_ErrandLegalActs] PRIMARY KEY ([ErrandLegalActId])
);
GO

exec spDescTable  N'ErrandLegalActs', N'Номенклатура на приложим нормативен акт на процедура за външно възлагане.'
exec spDescColumn N'ErrandLegalActs', N'ErrandLegalActId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ErrandLegalActs', N'Gid'                , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ErrandLegalActs', N'Name'               , N'Наименование.'
GO
