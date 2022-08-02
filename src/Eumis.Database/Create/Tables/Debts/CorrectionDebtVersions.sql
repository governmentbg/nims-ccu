PRINT 'CorrectionDebtVersions'
GO

CREATE TABLE [dbo].[CorrectionDebtVersions] (
    [CorrectionDebtVersionId]   INT             NOT NULL IDENTITY,
    [CorrectionDebtId]          INT             NOT NULL,
    [OrderNum]                  INT             NOT NULL,
    [Status]                    INT             NOT NULL,

    [DebtEuAmount]              MONEY           NULL,
    [DebtBgAmount]              MONEY           NULL,
    [DebtBfpAmount]             MONEY           NULL,

    [CertEuAmount]              MONEY           NULL,
    [CertBgAmount]              MONEY           NULL,
    [CertBfpAmount]             MONEY           NULL,

    [ReimbursedEuAmount]        MONEY           NULL,
    [ReimbursedBgAmount]        MONEY           NULL,
    [ReimbursedBfpAmount]       MONEY           NULL,

    [CreateNotes]               NVARCHAR(MAX)   NULL,
    [CreatedByUserId]           INT             NOT NULL,
    [CreateDate]                DATETIME2       NOT NULL,
    [ModifyDate]                DATETIME2       NOT NULL,
    [Version]                   ROWVERSION      NOT NULL,

    CONSTRAINT [PK_CorrectionDebtVersions]                          PRIMARY KEY ([CorrectionDebtVersionId]),
    CONSTRAINT [FK_CorrectionDebtVersions_CorrectionDebts]          FOREIGN KEY ([CorrectionDebtId])          REFERENCES [dbo].[CorrectionDebts] ([CorrectionDebtId]),
    CONSTRAINT [FK_CorrectionDebtVersions_Users]                    FOREIGN KEY ([CreatedByUserId])         REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_CorrectionDebtVersions_Status]                  CHECK ([Status] IN (1,2,3))
);
GO

exec spDescTable  N'CorrectionDebtVersions', N'Версия на дълг по ФКСП.'
exec spDescColumn N'CorrectionDebtVersions', N'CorrectionDebtVersionId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CorrectionDebtVersions', N'CorrectionDebtId'            , N'Идентификатор на дълг по ФКСП.'
exec spDescColumn N'CorrectionDebtVersions', N'OrderNum'                    , N'Пореден номер.'
exec spDescColumn N'CorrectionDebtVersions', N'Status'                      , N'Статус: 1 - Чернова, 3 - Актуален, 4 - Архивиран.'

exec spDescColumn N'CorrectionDebtVersions', N'DebtEuAmount'                , N'Дължима сума ЕС.'
exec spDescColumn N'CorrectionDebtVersions', N'DebtBgAmount'                , N'Дължима сума НФ.'
exec spDescColumn N'CorrectionDebtVersions', N'DebtBfpAmount'               , N'Дължима сума общо БФП.'

exec spDescColumn N'CorrectionDebtVersions', N'CertEuAmount'                , N'Сертифицирана сума ЕС.'
exec spDescColumn N'CorrectionDebtVersions', N'CertBgAmount'                , N'Сертифицирана сума НФ.'
exec spDescColumn N'CorrectionDebtVersions', N'CertBfpAmount'                , N'Сертифицирана сума общо БФП.'

exec spDescColumn N'CorrectionDebtVersions', N'ReimbursedEuAmount'          , N'Възстановена сума ЕС.'
exec spDescColumn N'CorrectionDebtVersions', N'ReimbursedBgAmount'          , N'Възстановена сума НФ.'
exec spDescColumn N'CorrectionDebtVersions', N'ReimbursedBfpAmount'          , N'Възстановена сума общо БФП.'

exec spDescColumn N'CorrectionDebtVersions', N'CreateNotes'                 , N'Основание за промяната.'
exec spDescColumn N'CorrectionDebtVersions', N'CreatedByUserId'             , N'Идентификатор на потребител създал записа.'

exec spDescColumn N'CorrectionDebtVersions', N'CreateDate'                  , N'Дата на създаване на записа.'
exec spDescColumn N'CorrectionDebtVersions', N'ModifyDate'                  , N'Дата на последно редактиране на записа.'
exec spDescColumn N'CorrectionDebtVersions', N'Version'                     , N'Версия.'

GO
