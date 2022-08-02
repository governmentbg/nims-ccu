PRINT 'FinancialCorrectionVersions'
GO

CREATE TABLE [dbo].[FinancialCorrectionVersions] (
    [FinancialCorrectionVersionId]          INT                    NOT NULL IDENTITY,
    [FinancialCorrectionId]                 INT                    NOT NULL,
    [OrderNum]                              INT                    NOT NULL,
    [Status]                                INT                    NOT NULL,

    [Percent]                               MONEY                  NULL,
    [EuAmount]                              MONEY                  NULL,
    [BgAmount]                              MONEY                  NULL,
    [BfpAmount]                             MONEY                  NULL,
    [SelfAmount]                            MONEY                  NULL,
    [TotalAmount]                           MONEY                  NULL,

    [FinancialCorrectionImposingReasonId]   INT                    NULL,
    [Description]                           NVARCHAR(MAX)          NULL,
    [ViolationFoundBy]                      INT                    NULL,
    [AmendmentReason]                       INT                    NULL,
    [Irregularity]                          NVARCHAR(MAX)          NULL,
    [CorrectionBearer]                      INT                    NULL,
    [BlobKey]                               UNIQUEIDENTIFIER       NULL,

    [IsFirstVersion]                        BIT                    NOT NULL,
    [CreateDate]                            DATETIME2              NOT NULL,
    [ModifyDate]                            DATETIME2              NOT NULL,
    [Version]                               ROWVERSION             NOT NULL,

    CONSTRAINT [PK_FinancialCorrectionVersions]                                    PRIMARY KEY ([FinancialCorrectionVersionId]),
    CONSTRAINT [FK_FinancialCorrectionVersions_FinancialCorrections]               FOREIGN KEY ([FinancialCorrectionId])                     REFERENCES [dbo].[FinancialCorrections] ([FinancialCorrectionId]),
    CONSTRAINT [FK_FinancialCorrectionVersions_FinancialCorrectionImposingReasons] FOREIGN KEY ([FinancialCorrectionImposingReasonId])       REFERENCES [dbo].[FinancialCorrectionImposingReasons] ([FinancialCorrectionImposingReasonId]),
    CONSTRAINT [FK_FinancialCorrectionVersions_Blobs]                              FOREIGN KEY ([BlobKey])                                   REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_FinancialCorrectionVersions_Status]                            CHECK       ([Status]                              IN (1, 2, 3)),
    CONSTRAINT [CHK_FinancialCorrectionVersions_ViolationFoundBy]                  CHECK       ([ViolationFoundBy]                    IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_FinancialCorrectionVersions_AmendmentReason]                   CHECK       ([AmendmentReason]                     IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_FinancialCorrectionVersions_CorrectionBearer]                  CHECK       ([CorrectionBearer]                    IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'FinancialCorrectionVersions', N'Версии на финансови корекции.'
exec spDescColumn N'FinancialCorrectionVersions', N'FinancialCorrectionVersionId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'FinancialCorrectionVersions', N'FinancialCorrectionId'               , N'Идентификатор на финансова корекция.'
exec spDescColumn N'FinancialCorrectionVersions', N'OrderNum'                            , N'Пореден номер.'
exec spDescColumn N'FinancialCorrectionVersions', N'Status'                              , N'Статус: 1 - Чернова, 2 - Актуален, 3 - Архивиран.'

exec spDescColumn N'FinancialCorrectionVersions', N'Percent'                             , N'Процент на наложената финансова корекция.'
exec spDescColumn N'FinancialCorrectionVersions', N'EuAmount'                            , N'Стойност финансиране ЕС.'
exec spDescColumn N'FinancialCorrectionVersions', N'BgAmount'                            , N'Стойност национално финансиране.'
exec spDescColumn N'FinancialCorrectionVersions', N'BfpAmount'                           , N'Стойност общо БФП.'
exec spDescColumn N'FinancialCorrectionVersions', N'SelfAmount'                          , N'Стойност собствено съфинансиране.'
exec spDescColumn N'FinancialCorrectionVersions', N'TotalAmount'                         , N'Обща сума.'

exec spDescColumn N'FinancialCorrectionVersions', N'FinancialCorrectionImposingReasonId' , N'Идентификатор на основание за налагане на финансовата корекция.'
exec spDescColumn N'FinancialCorrectionVersions', N'Description'                         , N'Описание на фактическата обстановка на посоченото нарушение.'
exec spDescColumn N'FinancialCorrectionVersions', N'ViolationFoundBy'                    , N'Орган/институция, установила нарушението за финансовата корекция: 1 - УО, 2 - СО, 3 - ОО, 4 - ЕК, 5 - ЕСП.'
exec spDescColumn N'FinancialCorrectionVersions', N'AmendmentReason'                     , N'Причина за изменението: 1 - Съдебно решение, 2 - Предложение на СО, 3 - Предложение на ОО, 4 - Предложение ЕК, 5 - Предложение ЕСП, 6 - Решение на УО).'
exec spDescColumn N'FinancialCorrectionVersions', N'Irregularity'                        , N'Нередност.'
exec spDescColumn N'FinancialCorrectionVersions', N'CorrectionBearer'                    , N'Следва да се понесе от: 1 - Бенефициент, 2 - УО, 3 - УО и Бенефициент, 4 - Национален бюджет.'
exec spDescColumn N'FinancialCorrectionVersions', N'BlobKey'                             , N'Идентификатор на файл.'

exec spDescColumn N'FinancialCorrectionVersions', N'IsFirstVersion'                      , N'Маркер дали версията е първа.'
exec spDescColumn N'FinancialCorrectionVersions', N'CreateDate'                          , N'Дата на създаване на записа.'
exec spDescColumn N'FinancialCorrectionVersions', N'ModifyDate'                          , N'Дата на последно редактиране на записа.'
exec spDescColumn N'FinancialCorrectionVersions', N'Version'                             , N'Версия.'
GO