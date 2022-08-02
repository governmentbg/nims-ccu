PRINT 'FinancialCorrectionImposingReasons'
GO

CREATE TABLE [dbo].[FinancialCorrectionImposingReasons] (
    [FinancialCorrectionImposingReasonId]    INT                NOT NULL IDENTITY,
    [Gid]                                    UNIQUEIDENTIFIER   NOT NULL,
    [Name]                                   NVARCHAR(MAX)      NOT NULL,
    [Code]                                   NVARCHAR(100)      NULL,
    [IsActive]                               BIT                NOT NULL,
    [CreateDate]                             DATETIME2          NOT NULL,
    [ModifyDate]                             DATETIME2          NOT NULL,
    [Version]                                ROWVERSION         NOT NULL,

    CONSTRAINT [PK_FinancialCorrectionImposingReasons] PRIMARY KEY ([FinancialCorrectionImposingReasonId])
);
GO

exec spDescTable  N'FinancialCorrectionImposingReasons', N'Основания за налагане на финансова корекция.'
exec spDescColumn N'FinancialCorrectionImposingReasons', N'FinancialCorrectionImposingReasonId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'FinancialCorrectionImposingReasons', N'Gid'                                   , N'Уникален публичен системно генериран идентификатор.'
exec spDescColumn N'FinancialCorrectionImposingReasons', N'Name'                                  , N'Наименование.'
exec spDescColumn N'FinancialCorrectionImposingReasons', N'Code'                                  , N'Код.'
exec spDescColumn N'FinancialCorrectionImposingReasons', N'IsActive'                              , N'Маркер за активност.'
exec spDescColumn N'FinancialCorrectionImposingReasons', N'CreateDate'                            , N'Дата на създаване на записа.'
exec spDescColumn N'FinancialCorrectionImposingReasons', N'ModifyDate'                            , N'Дата на последно редактиране на записа.'
exec spDescColumn N'FinancialCorrectionImposingReasons', N'Version'                               , N'Версия.'
