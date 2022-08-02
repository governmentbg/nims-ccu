PRINT 'IrregularityFinancialCorrections'
GO

CREATE TABLE [dbo].[IrregularityFinancialCorrections] (
    [IrregularityFinancialCorrectionId]    INT   NOT NULL IDENTITY,
    [IrregularityId]                       INT   NOT NULL,
    [FinancialCorrectionId]                INT   NOT NULL,

    CONSTRAINT [PK_IrregularityFinancialCorrections]                      PRIMARY KEY ([IrregularityFinancialCorrectionId]),
    CONSTRAINT [FK_IrregularityFinancialCorrections_Irregularities]       FOREIGN KEY ([IrregularityId])        REFERENCES [dbo].[Irregularities] ([IrregularityId]),
    CONSTRAINT [FK_IrregularityFinancialCorrections_FinancialCorrections] FOREIGN KEY ([FinancialCorrectionId]) REFERENCES [dbo].[FinancialCorrections] ([FinancialCorrectionId])
);
GO

exec spDescTable  N'IrregularityFinancialCorrections', N'Финансови корекции към нередност.'
exec spDescColumn N'IrregularityFinancialCorrections', N'IrregularityFinancialCorrectionId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IrregularityFinancialCorrections', N'IrregularityId'                   , N'Идентификатор на нередност.'
exec spDescColumn N'IrregularityFinancialCorrections', N'FinancialCorrectionId'            , N'Идентификатор на финансова корекция.'
GO
