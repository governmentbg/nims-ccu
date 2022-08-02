PRINT 'CheckSheets'
GO

CREATE TABLE [dbo].[CheckSheets] (
    [CheckSheetId]                              INT                 NOT NULL IDENTITY,
    [MapNodeId]                                 INT                 NOT NULL,
    [ProgrammeCheckListId]                      INT                 NOT NULL,

    [ProcedureId]                               INT                 NULL,
    [ContractId]                                INT                 NULL,
    [CompanyId]                                 INT                 NULL,
    [ContractProcurementXmlId]                  INT                 NULL,
    [ContractProcurementPlanGid]                UNIQUEIDENTIFIER    NULL,
    [ContractReportId]                          INT                 NULL,
    [CertReportId]                              INT                 NULL,
    [SpotCheckId]                               INT                 NULL,
    [ProjectId]                                 INT                 NULL,
    [IrregularitySignalId]                      INT                 NULL,
    [ContractReportFinancialCorrectionId]       INT                 NULL,
    [ContractReportCorrectionId]                INT                 NULL,
    [ContractReportFinancialRevalidationId]     INT                 NULL,
    [ContractReportRevalidationId]              INT                 NULL,

    [CheckListVersionNum]                       INT                 NOT NULL,
    [CheckListName]                             NVARCHAR(MAX)       NOT NULL,
    [Notes]                                     NVARCHAR(MAX)       NULL,
    [Type]                                      INT                 NOT NULL,
    [Status]                                    INT                 NOT NULL,
    [CreatedByUserId]                           INT                 NOT NULL,
    [LastModifiedByUserId]                      INT                 NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL,
    [Version]                                   ROWVERSION          NOT NULL,

    CONSTRAINT [PK_CheckSheets]                                       PRIMARY KEY ([CheckSheetId]),
    CONSTRAINT [FK_CheckSheets_MapNodes]                              FOREIGN KEY ([MapNodeId])                              REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_CheckSheets_ProgrammeCheckLists]                   FOREIGN KEY ([ProgrammeCheckListId])                   REFERENCES [dbo].[ProgrammeCheckLists] ([ProgrammeCheckListId]),
    CONSTRAINT [FK_CheckSheets_Users]                                 FOREIGN KEY ([CreatedByUserId])                        REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_CheckSheets_LastModifiedByUser]                    FOREIGN KEY ([LastModifiedByUserId])                   REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_CheckSheets_Procedures]                            FOREIGN KEY ([ProcedureId])                            REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_CheckSheets_Contracts]                             FOREIGN KEY ([ContractId])                             REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_CheckSheets_Companies]                             FOREIGN KEY ([CompanyId])                              REFERENCES [dbo].[Companies] ([CompanyId]),
    CONSTRAINT [FK_CheckSheets_ContractProcurementXmls]               FOREIGN KEY ([ContractProcurementXmlId])               REFERENCES [dbo].[ContractProcurementXmls] ([ContractProcurementXmlId]),
    CONSTRAINT [FK_CheckSheets_ContractReports]                       FOREIGN KEY ([ContractReportId])                       REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_CheckSheets_CertReports]                           FOREIGN KEY ([CertReportId])                           REFERENCES [dbo].[CertReports] ([CertReportId]),
    CONSTRAINT [FK_CheckSheets_SpotChecks]                            FOREIGN KEY ([SpotCheckId])                            REFERENCES [dbo].[SpotChecks] ([SpotCheckId]),
    CONSTRAINT [FK_CheckSheets_Projects]                              FOREIGN KEY ([ProjectId])                              REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_CheckSheets_IrregularitySignals]                   FOREIGN KEY ([IrregularitySignalId])                   REFERENCES [dbo].[IrregularitySignals] ([IrregularitySignalId]),
    CONSTRAINT [FK_CheckSheets_ContractReportFinancialRevalidations]  FOREIGN KEY ([ContractReportFinancialRevalidationId])  REFERENCES [dbo].[ContractReportFinancialRevalidations] ([ContractReportFinancialRevalidationId]),
    CONSTRAINT [FK_CheckSheets_ContractReportFinancialCorrections]    FOREIGN KEY ([ContractReportFinancialCorrectionId])    REFERENCES [dbo].[ContractReportFinancialCorrections] ([ContractReportFinancialCorrectionId]),
    CONSTRAINT [FK_CheckSheets_ContractReportCorrections]             FOREIGN KEY ([ContractReportCorrectionId])             REFERENCES [dbo].[ContractReportCorrections] ([ContractReportCorrectionId]),
    CONSTRAINT [FK_CheckSheets_ContractReportRevalidations]           FOREIGN KEY ([ContractReportRevalidationId])           REFERENCES [dbo].[ContractReportRevalidations] ([ContractReportRevalidationId]),
    CONSTRAINT [CHK_CheckSheets_Type]                                 CHECK       ([Type] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)),
    CONSTRAINT [CHK_CheckSheets_Status]                               CHECK       ([Status] IN (1, 2, 3, 4, 5))
);
GO

exec spDescTable  N'CheckSheets', N'Контролен лист.'
exec spDescColumn N'CheckSheets', N'CheckSheetId'                            , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CheckSheets', N'MapNodeId'                               , N'Идентификатор на оперативна програма.'
exec spDescColumn N'CheckSheets', N'ProgrammeCheckListId'                    , N'Идентификатор на шаблон за контролен лист.'
exec spDescColumn N'CheckSheets', N'ProcedureId'                             , N'Идентификатор на процедура.'
exec spDescColumn N'CheckSheets', N'ContractId'                              , N'Идентификатор на договор.'
exec spDescColumn N'CheckSheets', N'CompanyId'                               , N'Идентификатор на бенефициент.'
exec spDescColumn N'CheckSheets', N'ContractProcurementXmlId'                , N'Идентификатор на процедура за избор на изпълнител.'
exec spDescColumn N'CheckSheets', N'ContractProcurementPlanGid'              , N'Публичен идентификатор на процедура за избор на изпълнител.'
exec spDescColumn N'CheckSheets', N'ContractReportId'                        , N'Идентификатор на пакет отчетни документи.'
exec spDescColumn N'CheckSheets', N'CertReportId'                            , N'Идентификатор на доклад по сертификация.'
exec spDescColumn N'CheckSheets', N'SpotCheckId'                             , N'Идентификатор на проверка на място.'
exec spDescColumn N'CheckSheets', N'ProjectId'                               , N'Идентификатор на проект.'
exec spDescColumn N'CheckSheets', N'IrregularitySignalId'                    , N'Идентификатор на сигнал за нередност.'
exec spDescColumn N'CheckSheets', N'ContractReportFinancialRevalidationId'   , N'Идентификатор на препотвърждаване на верифицирани суми на ниво РОД.'
exec spDescColumn N'CheckSheets', N'ContractReportRevalidationId'            , N'Идентификатор на препотвърждаване на верифицирани суми на други нива.'
exec spDescColumn N'CheckSheets', N'ContractReportFinancialCorrectionId'     , N'Идентификатор на корекция на верифицирани суми на ниво РОД.'
exec spDescColumn N'CheckSheets', N'ContractReportCorrectionId'              , N'Идентификатор на корекция на верифицирани суми на други нива.'
exec spDescColumn N'CheckSheets', N'CheckListVersionNum'                     , N'Номер на текущата версия.'
exec spDescColumn N'CheckSheets', N'CheckListName'                           , N'Наименование.'
exec spDescColumn N'CheckSheets', N'Notes'                                   , N'Бележки.'
exec spDescColumn N'CheckSheets', N'Type'                                    , N'Тип на контролен лист: 1 - Процедура, 2 - Договор, 3 - Пакет отчетни документи, 4 - Процедура за избор на изпълнител, 5 - Доклад по сертификация, 6 - Проверка на място, 7 - Оперативна програма, 8 - Сигнал за нередност, 9 - Корекция на верифицирани суми на ниво РОД, 10 - Препотвърждаване на верифицирани суми на ниво РОД, 11 - Корекция на верифицирани суми на други нива, 12 - Препотвърждаване на верифицирани суми на други нива.'
exec spDescColumn N'CheckSheets', N'Status'                                  , N'Статус на контролен лист: 1 - Чернова, 2 - В изпълнение, 3 - Приключен, 4 - Анулиран, 5 - Прекъснат'
exec spDescColumn N'CheckSheets', N'CreatedByUserId'                         , N'Идентификатор на потребител създал контролен лист.'
exec spDescColumn N'CheckSheets', N'LastModifiedByUserId'                    , N'Идентификатор на потребител редактирал последно записа.'
exec spDescColumn N'CheckSheets', N'CreateDate'                              , N'Дата на създаване на записа.'
exec spDescColumn N'CheckSheets', N'ModifyDate'                              , N'Дата на последно редактиране на записа.'
exec spDescColumn N'CheckSheets', N'Version'                                 , N'Версия.'

GO
