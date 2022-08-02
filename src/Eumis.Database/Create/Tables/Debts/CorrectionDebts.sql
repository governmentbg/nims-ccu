PRINT 'CorrectionDebts'
GO

CREATE TABLE [dbo].[CorrectionDebts] (
    [CorrectionDebtId]          INT             NOT NULL IDENTITY,
    [FlatFinancialCorrectionId] INT             NOT NULL,
    [RegNumber]                 NVARCHAR(200)   NULL,
    [RegDate]                   DATETIME2       NOT NULL,
    [Status]                    INT             NOT NULL,

    [Comment]                   NVARCHAR(MAX)   NULL,
    [DeleteNote]                NVARCHAR(MAX)   NULL,

    [CreateDate]                DATETIME2       NOT NULL,
    [ModifyDate]                DATETIME2       NOT NULL,
    [Version]                   ROWVERSION      NOT NULL,

    CONSTRAINT [PK_CorrectionDebts]                             PRIMARY KEY ([CorrectionDebtId]),
    CONSTRAINT [FK_CorrectionDebts_FlatFinancialCorrections]    FOREIGN KEY ([FlatFinancialCorrectionId])      REFERENCES [dbo].[FlatFinancialCorrections] ([FlatFinancialCorrectionId]),
    CONSTRAINT [CHK_CorrectionDebts_Status]                     CHECK ([Status]              IN (1, 2, 3))
);
GO

CREATE UNIQUE INDEX [UQ_CorrectionDebts_RegNumber]
ON [CorrectionDebts]([RegNumber])
WHERE [Status] <> 1;
GO

exec spDescTable  N'CorrectionDebts', N'Дълг към договор за БФП.'
exec spDescColumn N'CorrectionDebts', N'CorrectionDebtId'           , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CorrectionDebts', N'FlatFinancialCorrectionId'  , N'Идентификатор на ФКСП.'
exec spDescColumn N'CorrectionDebts', N'RegNumber'                  , N'Номер на дълга.'
exec spDescColumn N'CorrectionDebts', N'RegDate'                    , N'Дата на регистрация.'
exec spDescColumn N'CorrectionDebts', N'Status'                     , N'Статус: 1 - Нова; 2 - Въведена; 3 - Анулирана.'

exec spDescColumn N'CorrectionDebts', N'Comment'                    , N'Коментар.'
exec spDescColumn N'CorrectionDebts', N'DeleteNote'                 , N'Причина за изтриване.'

exec spDescColumn N'CorrectionDebts', N'CreateDate'                 , N'Дата на създаване на записа.'
exec spDescColumn N'CorrectionDebts', N'ModifyDate'                 , N'Дата на последно редактиране на записа.'
exec spDescColumn N'CorrectionDebts', N'Version'                    , N'Версия.'
GO
