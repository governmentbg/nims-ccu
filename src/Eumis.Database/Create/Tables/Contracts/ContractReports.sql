PRINT 'ContractReports'
GO

CREATE TABLE [dbo].[ContractReports] (
    [ContractReportId]                      INT               NOT NULL IDENTITY,
    [ContractId]                            INT               NOT NULL,
    [Gid]                                   UNIQUEIDENTIFIER  NOT NULL UNIQUE,
    [ReportType]                            INT               NOT NULL,
    [OrderNum]                              INT               NOT NULL,

    [Status]                                INT               NOT NULL,
    [StatusNote]                            NVARCHAR(MAX)     NULL,
    [Source]                                INT               NOT NULL,

    [RegDate]                               DATETIME2         NULL,
    [OtherRegistration]                     NVARCHAR(200)     NULL,
    [StoragePlace]                          NVARCHAR(MAX)     NULL,
    [SubmitDate]                            DATETIME2         NULL,
    [SubmitDeadline]                        DATETIME2         NULL,
    [DateFrom]                              DATETIME2         NULL,
    [DateTo]                                DATETIME2         NULL,
    [CheckedDate]                           DATETIME2         NULL,

    [CreateDate]                            DATETIME2         NOT NULL,
    [ModifyDate]                            DATETIME2         NOT NULL,
    [Version]                               ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReports]                    PRIMARY KEY ([ContractReportId]),
    CONSTRAINT [FK_ContractReports_Contracts]          FOREIGN KEY ([ContractId])   REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [CHK_ContractReports_ReportType]        CHECK ([ReportType]     IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_ContractReports_Status]            CHECK ([Status]         IN (1, 2, 3, 4, 5, 6))
);
GO

exec spDescTable  N'ContractReports', N'Пакет отчетни документи.'
exec spDescColumn N'ContractReports', N'ContractReportId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReports', N'ContractId'               , N'Идентификатор на договор'
exec spDescColumn N'ContractReports', N'Gid'                      , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractReports', N'ReportType'               , N'Тип на пакета: 1 – Авансово искане за плащане; 2 – Технически отчет; 3 – Искане за плащане, технически отчет, финансов отчет, 4 - Искане за плащане и финансов отчет'
exec spDescColumn N'ContractReports', N'OrderNum'                 , N'Пореден номер.'

exec spDescColumn N'ContractReports', N'Status'                   , N'Статус: 1- Чернова; 2 - Въведен; 3 - Изпратен; 4 - В проверка, 5 - Приет; 6 - Отхвърлен.'
exec spDescColumn N'ContractReports', N'StatusNote'               , N'Бележка.'
exec spDescColumn N'ContractReports', N'Source'                   , N'Въведен от: 1 - Бенефициент, 2 – УО.'

exec spDescColumn N'ContractReports', N'RegDate'                  , N'Дата на регистрация.'
exec spDescColumn N'ContractReports', N'OtherRegistration'        , N'Друга регистрация.'
exec spDescColumn N'ContractReports', N'StoragePlace'             , N'Място на съхранение.'
exec spDescColumn N'ContractReports', N'SubmitDate'               , N'Дата на представяне.'
exec spDescColumn N'ContractReports', N'SubmitDeadline'           , N'Срок за представяне.'
exec spDescColumn N'ContractReports', N'DateFrom'                 , N'Начална дата.'
exec spDescColumn N'ContractReports', N'DateTo'                   , N'Крайна дата.'

exec spDescColumn N'ContractReports', N'CreateDate'               , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReports', N'ModifyDate'               , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReports', N'Version'                  , N'Версия.'
GO
