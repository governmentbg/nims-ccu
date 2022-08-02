PRINT 'FinancialCorrectionVersionViolations'
GO

CREATE TABLE [dbo].[FinancialCorrectionVersionViolations] (
    [FinancialCorrectionVersionId]          INT                    NOT NULL,
    [OtherViolationId]                      INT                    NOT NULL,
    
    CONSTRAINT [PK_FinancialCorrectionVersionViolations]                             PRIMARY KEY ([FinancialCorrectionVersionId], [OtherViolationId]),
    CONSTRAINT [FK_FinancialCorrectionVersionViolations_FinancialCorrectionVersions] FOREIGN KEY ([FinancialCorrectionVersionId]) REFERENCES [dbo].[FinancialCorrectionVersions] ([FinancialCorrectionVersionId]),
    CONSTRAINT [FK_FinancialCorrectionVersionViolations_OtherViolations]             FOREIGN KEY ([OtherViolationId]) REFERENCES [dbo].[OtherViolations] ([OtherViolationId]),
);
GO

exec spDescTable  N'FinancialCorrectionVersionViolations', N'Други констатирани нарушения към версия на финансови корекции.'
exec spDescColumn N'FinancialCorrectionVersionViolations', N'FinancialCorrectionVersionId'        , N'Идентификатор на версия на финансова корекция.'
exec spDescColumn N'FinancialCorrectionVersionViolations', N'OtherViolationId'                    , N'Идентификатор на друго констатирано нарушение.'

GO