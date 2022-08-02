PRINT 'ProcedureReportingDocs'
GO

CREATE TABLE [dbo].[ProcedureReportingDocs] (
    [ProcedureReportingDocId]           INT             NOT NULL IDENTITY,
    [ProcedureId]                       INT             NOT NULL,
    [ProcedureReportingDocTypeId]       INT             NOT NULL,
    [IsRequired]                        BIT             NOT NULL,
    [IsOriginal]                        BIT             NOT NULL,
    [ReportType]                        INT             NOT NULL,
    CONSTRAINT [PK_ProcedureReportingDocs]                            PRIMARY KEY ([ProcedureReportingDocId]),
    CONSTRAINT [FK_ProcedureReportingDocs_Procedures]                 FOREIGN KEY ([ProcedureId])                   REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureReportingDocs_ProcedureReportingDocTypes] FOREIGN KEY ([ProcedureReportingDocTypeId])   REFERENCES [dbo].[ProcedureReportingDocTypes] ([ProcedureReportingDocTypeId]),
);
GO

exec spDescTable  N'ProcedureReportingDocs', N'Документи, който се подават при кандидатстване по процедура.'
exec spDescColumn N'ProcedureReportingDocs', N'ProcedureReportingDocId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureReportingDocs', N'ProcedureId'                     , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureReportingDocs', N'ProcedureReportingDocTypeId'     , N'Идентификатор на тип документ при електронно кандидастване.'
exec spDescColumn N'ProcedureReportingDocs', N'IsRequired'                      , N'Маркер, дали е задължителен.'
exec spDescColumn N'ProcedureReportingDocs', N'IsOriginal'                      , N'Маркер, дали подаването е електронно или се изисква оригинал.'
exec spDescColumn N'ProcedureReportingDocs', N'ReportType'                      , N'тип отчет, към който се подава : 1-Технически доклад, 2-Финансов отчет, 3-Искане за плащане.'

GO
