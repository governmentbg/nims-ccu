PRINT 'ProcedureContractReportDocuments'
GO

CREATE TABLE [dbo].[ProcedureContractReportDocuments] (
    [ProcedureContractReportDocumentId]         INT                 NOT NULL IDENTITY,
    [Gid]                                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ProcedureId]                               INT                 NOT NULL,
    [Name]                                      NVARCHAR(200)       NOT NULL,
    [Type]                                      INT                 NOT NULL,
    [Extension]                                 NVARCHAR(50)        NULL,
    [IsRequired]                                BIT                 NOT NULL,
    [IsActivated]                               BIT                 NOT NULL,
    [IsActive]                                  BIT                 NOT NULL,

    CONSTRAINT [PK_ProcedureContractReportDocuments]                            PRIMARY KEY ([ProcedureContractReportDocumentId]),
    CONSTRAINT [FK_ProcedureContractReportDocuments_Procedures]                 FOREIGN KEY ([ProcedureId])                         REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_ProcedureContractReportDocuments_Type]                      CHECK       ([Type] IN (1, 2, 3, 4, 5, 6))
);
GO

exec spDescTable  N'ProcedureContractReportDocuments', N'Документи, които се подават при отчитане по договор.'
exec spDescColumn N'ProcedureContractReportDocuments', N'ProcedureContractReportDocumentId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureContractReportDocuments', N'Gid'                                   , N'Публичен системно генериран идентификатор.'
exec spDescColumn N'ProcedureContractReportDocuments', N'ProcedureId'                           , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureContractReportDocuments', N'Name'                                  , N'Наименование на документа.'
exec spDescColumn N'ProcedureContractReportDocuments', N'Type'                                  , N'Тип на документа:  1 - Документ към Технически отчет, 2 - Документ към Финансов отчет, 3 - Документ към Авансово искане за плащане, 4 - Документ към Междинно искане за плащане, 5 - Документ към Окончателно искане за плащане, 6 - Документ към Процедури за избор на изпълнител и сключени договори.'
exec spDescColumn N'ProcedureContractReportDocuments', N'Extension'                             , N'Разширение на документа(файла).'
exec spDescColumn N'ProcedureContractReportDocuments', N'IsRequired'                            , N'Маркер, дали е задължителен.'
exec spDescColumn N'ProcedureContractReportDocuments', N'IsActivated'                           , N'Маркер, дали документът е активиран.'
exec spDescColumn N'ProcedureContractReportDocuments', N'IsActive'                              , N'Маркер, дали документът е активен.'
GO
