PRINT 'Procedures'
GO

CREATE TABLE [dbo].[Procedures] (
    [ProcedureId]                                           INT                 NOT NULL IDENTITY,
    [Gid]                                                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ProcedureTypeId]                                       INT                 NOT NULL,
    [ProcedureStatus]                                       INT                 NOT NULL,
    [ProcedureKind]                                         INT                 NOT NULL,
    [ProcedureContractReportDocumentsSectionStatus]         INT                 NOT NULL,
    [ApplicationFormType]                                   INT                 NOT NULL,
    [AllowConcurrancyContractReports]                       BIT                 NOT NULL,
    [ListingDate]                                           DATETIME2           NULL,
    [Code]                                                  NVARCHAR(200)       NOT NULL UNIQUE,
    [Name]                                                  NVARCHAR(MAX)       NOT NULL,
    [NameAlt]                                               NVARCHAR(MAX)       NULL,
    [Description]                                           NVARCHAR(MAX)       NULL,
    [DescriptionAlt]                                        NVARCHAR(MAX)       NULL,

    [AttachedProcedureId]                                   INT                 NULL,

    [AllowedRegistrationType]                               INT                 NOT NULL,
    [ProjectMinAmount]                                      MONEY               NOT NULL,
    [ProjectMinAmountInfo]                                  NVARCHAR(500)       NULL,
    [ProjectMinAmountInfoAlt]                               NVARCHAR(500)       NULL,
    [ProjectMaxAmount]                                      MONEY               NOT NULL,
    [ProjectMaxAmountInfo]                                  NVARCHAR(500)       NULL,
    [ProjectMaxAmountInfoAlt]                               NVARCHAR(500)       NULL,
    [ProjectDuration]                                       INT                 NOT NULL,

    [InternetAddress]                                       NVARCHAR(MAX)       NULL,
    
    [ActivationDate]                                        DATETIME2           NULL,
    [IsIntegrated]                                          BIT                 NOT NULL,
    [IsIntroducedByLAG]                                     BIT                 NOT NULL DEFAULT(0),
    [LocalActionGroupId]                                    INT                 NULL,
    [HasBudgetComponents]                                   BIT                 NOT NULL DEFAULT(0),
    [CreateDate]                                            DATETIME2           NOT NULL,
    [ModifyDate]                                            DATETIME2           NOT NULL,
    [Version]                                               ROWVERSION          NOT NULL,

    CONSTRAINT [PK_Procedures]                                                PRIMARY KEY ([ProcedureId]),
    CONSTRAINT [FK_Procedures_ProcedureTypes]                                 FOREIGN KEY ([ProcedureTypeId])     REFERENCES [dbo].[ProcedureTypes] ([ProcedureTypeId]),
    CONSTRAINT [FK_Procedures_AttachedProcedures]                             FOREIGN KEY ([AttachedProcedureId]) REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_Procedures_LocalActionGroups]                              FOREIGN KEY ([LocalActionGroupId])  REFERENCES [dbo].[Companies] ([CompanyId]),
    CONSTRAINT [CHK_Procedures_AllowedRegistrationType]                       CHECK       ([AllowedRegistrationType] IN (1, 2, 3)),
    CONSTRAINT [CHK_Procedures_ProcedureStatuses]                             CHECK       ([ProcedureStatus] IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_Procedures_ProcedureKind]                                 CHECK       ([ProcedureKind] IN (1, 2)),
    CONSTRAINT [CHK_Procedures_ProcedureContractReportDocumentsSectionStatus] CHECK       ([ProcedureContractReportDocumentsSectionStatus] IN (1, 2)),
    CONSTRAINT [CHK_Procedures_ApplicationFormType]                           CHECK       ([ApplicationFormType] IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_Procedures_ProjectDuration]                               CHECK       ([ProjectDuration] > 0)
);
GO

exec spDescTable  N'Procedures', N'Процедура по инвестиционен приоритет.'
exec spDescColumn N'Procedures', N'ProcedureId'                                    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Procedures', N'Gid'                                            , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'Procedures', N'ProcedureTypeId'                                , N'Идентификатор на тип на процедура.'
exec spDescColumn N'Procedures', N'ProcedureStatus'                                , N'Статус на процедура.'
exec spDescColumn N'Procedures', N'ProcedureStatus'                                , N'Вид на процедура: 1 - Бюджет, 2 - Схема за кандидатсване.'
exec spDescColumn N'Procedures', N'ProcedureContractReportDocumentsSectionStatus'  , N'Статус на секция "Отчетни документи": 1 - Чернова, 2 - Активна.'
exec spDescColumn N'Procedures', N'ApplicationFormType'                            , N'Тип на формуляра. 1 - Стандартен, 2 - За предварителен подбор, 3 - Стандартен (разширен по концепция), 4 - Стандартен (бюджетна линия).'
exec spDescColumn N'Procedures', N'AllowConcurrancyContractReports'                , N'Разреши подаването на повече от 1 ПОД, преди да е одобрен или отхвърлен от УО.'

exec spDescColumn N'Procedures', N'ListingDate'                                    , N'Дата на обявяване.'
exec spDescColumn N'Procedures', N'Code'                                           , N'Системно генериран код/номер на процедурата .'
exec spDescColumn N'Procedures', N'Name'                                           , N'Наименование.'
exec spDescColumn N'Procedures', N'NameAlt'                                        , N'Наименование на друг език.'
exec spDescColumn N'Procedures', N'Description'                                    , N'Описание/Цели на предоставяната БФП.'
exec spDescColumn N'Procedures', N'DescriptionAlt'                                 , N'Описание/Цели на предоставяната БФП на друг език.'

exec spDescColumn N'Procedures', N'AllowedRegistrationType'                        , N'Вид на подаването.'
exec spDescColumn N'Procedures', N'ProjectMinAmount'                               , N'Изисквания за проектно предложение - Минимална стойност .'
exec spDescColumn N'Procedures', N'ProjectMinAmountInfo'                           , N'Изисквания за проектно предложение - Допълнителна информация към минимална стойност .'
exec spDescColumn N'Procedures', N'ProjectMinAmountInfoAlt'                        , N'Изисквания за проектно предложение - Допълнителна информация на английски език към минимална стойност .'
exec spDescColumn N'Procedures', N'ProjectMaxAmount'                               , N'Изисквания за проектно предложение - Максимална стойност .'
exec spDescColumn N'Procedures', N'ProjectMaxAmountInfo'                           , N'Изисквания за проектно предложение - Допълнителна информация към максимална стойност .'
exec spDescColumn N'Procedures', N'ProjectMaxAmountInfoAlt'                        , N'Изисквания за проектно предложение - Допълнителна информация на английски език към максимална стойност .'
exec spDescColumn N'Procedures', N'ProjectDuration'                                , N'Изисквания за проектно предложение - Продължителност на изпълнение.'

exec spDescColumn N'Procedures', N'ActivationDate'                                 , N'Дата на активиране на процедура.'
exec spDescColumn N'Procedures', N'IsIntegrated'                                   , N'Флаг, че процедурата е интегрирана.'
exec spDescColumn N'Procedures', N'IsIntroducedByLAG'                              , N'Флаг, че процедурата е въведена от МИГ/МИРГ.'
exec spDescColumn N'Procedures', N'LocalActionGroupId'                             , N'Идентификатор на МИГ/МИРГ.'
exec spDescColumn N'Procedures', N'HasBudgetComponents'                            , N'Флаг за детайлизация на бюджет по компоненти.'
exec spDescColumn N'Procedures', N'CreateDate'                                     , N'Дата на създаване на записа.'
exec spDescColumn N'Procedures', N'ModifyDate'                                     , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Procedures', N'Version'                                        , N'Версия.'
GO
