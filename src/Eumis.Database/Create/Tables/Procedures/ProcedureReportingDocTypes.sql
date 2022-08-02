PRINT 'ProcedureReportingDocTypes'
GO

CREATE TABLE [dbo].[ProcedureReportingDocTypes] (
    [ProcedureReportingDocTypeId]               INT           NOT NULL IDENTITY,
    [Name]                                      NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [PK_ProcedureReportingDocTypes]  PRIMARY KEY ([ProcedureReportingDocTypeId])
);
GO

exec spDescTable  N'ProcedureReportingDocTypes', N'Тип документ при електронно отчитане.'
exec spDescColumn N'ProcedureReportingDocTypes', N'ProcedureReportingDocTypeId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureReportingDocTypes', N'Name'                               , N'Наименование'
GO
