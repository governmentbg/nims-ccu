PRINT 'ContractReportFinancialCSDs'
GO

CREATE TABLE [dbo].[ContractReportFinancialCSDs] (
    [ContractReportFinancialCSDId]          INT               NOT NULL,
    [ContractReportFinancialId]             INT               NOT NULL,
    [ContractReportId]                      INT               NOT NULL,
    [ContractId]                            INT               NOT NULL,
    [Gid]                                   UNIQUEIDENTIFIER  NOT NULL UNIQUE,

    [Type]                                  INT               NOT NULL,
    [Description]                           NVARCHAR(MAX)     NULL,
    [Number]                                NVARCHAR(MAX)     NOT NULL,
    [Date]                                  DATETIME2         NOT NULL,
    [PaymentDate]                           DATETIME2         NOT NULL,

    [CompanyType]                           INT               NOT NULL,
    [CompanyGid]                            UNIQUEIDENTIFIER  NOT NULL,
    [CompanyUin]                            NVARCHAR(200)     NOT NULL,
    [CompanyUinType]                        INT               NOT NULL,
    [CompanyName]                           NVARCHAR(MAX)     NOT NULL,

    [ContractContractorGid]                 UNIQUEIDENTIFIER  NULL,
    [ContractContractorName]                NVARCHAR(MAX)     NULL,

    [CreateDate]                            DATETIME2         NOT NULL,
    [ModifyDate]                            DATETIME2         NOT NULL,
    [Version]                               ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportFinancialCSDs]                             PRIMARY KEY ([ContractReportFinancialCSDId]),
    CONSTRAINT [FK_ContractReportFinancialCSDs_ContractReportFinancials]    FOREIGN KEY ([ContractReportFinancialId])         REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportFinancialCSDs_ContractReports]             FOREIGN KEY ([ContractReportId])                  REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportFinancialCSDs_Contracts]                   FOREIGN KEY ([ContractId])                        REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [CHK_ContractReportFinancialCSDs_Type]                       CHECK       ([Type]         IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11)),
    CONSTRAINT [CHK_ContractReportFinancialCSDs_CompanyType]                CHECK       ([CompanyType]  IN (1, 2, 3))
);
GO

exec spDescTable  N'ContractReportFinancialCSDs', N'Разходооправдателни документи.'
exec spDescColumn N'ContractReportFinancialCSDs', N'ContractReportFinancialCSDId'     , N'Уникален системно генериран идентификатор'
exec spDescColumn N'ContractReportFinancialCSDs', N'ContractReportFinancialId'        , N'Идентификатор на финансов отчет'
exec spDescColumn N'ContractReportFinancialCSDs', N'ContractReportId'                 , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialCSDs', N'ContractId'                       , N'Идентификатор на договор'
exec spDescColumn N'ContractReportFinancialCSDs', N'Gid'                              , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportFinancialCSDs', N'Type'                             , N'Вид: 1 - Авансов отчет, 2 - Касова бележка (фискален бон), 3 - Разходен касов ордер, 4 - Заповед за командировка в страната, 5 - Кредитно известие, 6 - Дебитно известие, 7 - Фактура, 8 - Друг, 9 - Сметка за изплатени суми, 10 - Ведомост, 11 - Протокол по ЗДДС.'
exec spDescColumn N'ContractReportFinancialCSDs', N'Description'                      , N'Описание.'
exec spDescColumn N'ContractReportFinancialCSDs', N'Number'                           , N'Уникален номер.'
exec spDescColumn N'ContractReportFinancialCSDs', N'Date'                             , N'Дата.'
exec spDescColumn N'ContractReportFinancialCSDs', N'PaymentDate'                      , N'Дата на извършване на плащането.'

exec spDescColumn N'ContractReportFinancialCSDs', N'CompanyType'                      , N'Тип компания: 1 - Бенефициент, 2 - Партньор, 3 - Изпълнител.'
exec spDescColumn N'ContractReportFinancialCSDs', N'CompanyGid'                       , N'Публичен идентификатор на компания.'
exec spDescColumn N'ContractReportFinancialCSDs', N'CompanyUin'                       , N'Уникален идентификационен номер на компания.'
exec spDescColumn N'ContractReportFinancialCSDs', N'CompanyUinType'                   , N'0-ЕИК, 1-булстат, 2 - булстат за свободни професии (ЕГН), 3 - Чуждестранни фирми.'
exec spDescColumn N'ContractReportFinancialCSDs', N'CompanyName'                      , N'Наименование на компания.'
exec spDescColumn N'ContractReportFinancialCSDs', N'ContractContractorGid'            , N'Публичен идентификатор на договор за изпълнение/доставчик.'
exec spDescColumn N'ContractReportFinancialCSDs', N'ContractContractorName'           , N'Наименование на договор за изпълнение/доставчик.'

exec spDescColumn N'ContractReportFinancialCSDs', N'CreateDate'                       , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportFinancialCSDs', N'ModifyDate'                       , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportFinancialCSDs', N'Version'                          , N'Версия.'
GO