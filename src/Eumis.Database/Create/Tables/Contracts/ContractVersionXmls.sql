PRINT 'ContractVersionXmls'
GO

CREATE TABLE [dbo].[ContractVersionXmls] (
    [ContractVersionXmlId]            INT                 NOT NULL IDENTITY,
    [Gid]                             UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ContractId]                      INT                 NOT NULL,
    [Xml]                             XML                 NOT NULL,
    [Hash]                            NVARCHAR(10)        NOT NULL UNIQUE,
    [OrderNum]                        INT                 NOT NULL,

    [VersionType]                     INT                 NOT NULL,
    [VersionNum]                      INT                 NOT NULL,
    [VersionSubNum]                   INT                 NOT NULL,

    [Name]                            NVARCHAR(MAX)       NULL,
    [RegNumber]                       NVARCHAR(200)       NOT NULL,
    [ExecutionStatus]                 INT                 NULL,
    [ContractDate]                    DATETIME2           NULL,
    [StartDate]                       DATETIME2           NULL,
    [StartConditions]                 NVARCHAR(MAX)       NULL,
    [CompletionDate]                  DATETIME2           NULL,
    [TerminationDate]                 DATETIME2           NULL,
    [TerminationReason]               NVARCHAR(MAX)       NULL,

    [TotalEuAmount]                   MONEY               NULL,
    [TotalBgAmount]                   MONEY               NULL,
    [TotalBfpAmount]                  MONEY               NULL,
    [TotalSelfAmount]                 MONEY               NULL,
    [TotalAmount]                     MONEY               NULL,

    [Status]                          INT                 NOT NULL,
    [CreatedByUserId]                 INT                 NOT NULL,
    [CreateNote]                      NVARCHAR(MAX)       NOT NULL,
    [CreateDate]                      DATETIME2           NOT NULL,
    [ModifyDate]                      DATETIME2           NOT NULL,
    [Version]                         ROWVERSION          NOT NULL

    CONSTRAINT [PK_ContractVersionXmls]                 PRIMARY KEY ([ContractVersionXmlId]),
    CONSTRAINT [FK_ContractVersionXmls_Contracts]       FOREIGN KEY ([ContractId])                   REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractVersionXmls_Users]           FOREIGN KEY ([CreatedByUserId])              REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractXmls_VersionType]           CHECK ([VersionType] IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_ContractXmls_Status]                CHECK ([Status]      IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'ContractVersionXmls', N'Xml за договор за БФП.'
exec spDescColumn N'ContractVersionXmls', N'ContractVersionXmlId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractVersionXmls', N'Gid'                       , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractVersionXmls', N'ContractId'                , N'Идентификатор на договор.'
exec spDescColumn N'ContractVersionXmls', N'Xml'                       , N'Xml съдържание.'
exec spDescColumn N'ContractVersionXmls', N'Hash'                      , N'Уникален идентификатор на съдържанието на Xml-а.'
exec spDescColumn N'ContractVersionXmls', N'OrderNum'                  , N'Пореден номер.'

exec spDescColumn N'ContractVersionXmls', N'VersionType'               , N'Тип: 1 - Нов договор, 2 - Изменение, 3 - Промяна, 4 - Частично изменение, 5 - Частична промяна.'
exec spDescColumn N'ContractVersionXmls', N'VersionNum'                , N'Пореден номер на версия.'
exec spDescColumn N'ContractVersionXmls', N'VersionSubNum'             , N'Пореден номер на под-версия.'
exec spDescColumn N'ContractVersionXmls', N'RegNumber'                 , N'Системно генериран регистрационен номер.'

exec spDescColumn N'ContractVersionXmls', N'ExecutionStatus'           , N'Статус на изпълнение на договора: 1 - В изпълнение (от дата на стартиране); 2 - В изпълнение (временно спрян); 3 - В изпълнение (под наблюдение); 4 - Прекратен (към дата на прекратяване); 5 - Приключен (към датата на приключване).'
exec spDescColumn N'ContractVersionXmls', N'ContractDate'              , N'Дата на сключване на договор.'
exec spDescColumn N'ContractVersionXmls', N'StartDate'                 , N'Дата на стартиране.'
exec spDescColumn N'ContractVersionXmls', N'StartConditions'           , N'Условие за стартиране - задължително трябва да е попълнена или дата на стартиране или условие за стартиране.'
exec spDescColumn N'ContractVersionXmls', N'CompletionDate'            , N'Дата на завършване на договора.'
exec spDescColumn N'ContractVersionXmls', N'TerminationDate'           , N'Дата на прекратяване.'
exec spDescColumn N'ContractVersionXmls', N'TerminationReason'         , N'Основание за прекратяване.'

exec spDescColumn N'ContractVersionXmls', N'TotalEuAmount'             , N'Общо Финансиране от ЕС за договора.'
exec spDescColumn N'ContractVersionXmls', N'TotalBgAmount'             , N'Общо Финансиране от НФ за договора.'
exec spDescColumn N'ContractVersionXmls', N'TotalBfpAmount'            , N'Общо БФП за договора.'
exec spDescColumn N'ContractVersionXmls', N'TotalSelfAmount'           , N'Общо собствено финансиране за договора.'
exec spDescColumn N'ContractVersionXmls', N'TotalAmount'               , N'Обща стойност на договора.'

exec spDescColumn N'ContractVersionXmls', N'Status'                    , N'Статус: 1 - Чернова, 2 - Въведен, 3 - Актуален, 4 - Архивиран.'
exec spDescColumn N'ContractVersionXmls', N'CreatedByUserId'           , N'Създадено от.'
exec spDescColumn N'ContractVersionXmls', N'CreateNote'                , N'Бележка.'
exec spDescColumn N'ContractVersionXmls', N'CreateDate'                , N'Дата на създаване на записа.'
exec spDescColumn N'ContractVersionXmls', N'ModifyDate'                , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractVersionXmls', N'Version'                   , N'Версия.'
GO
