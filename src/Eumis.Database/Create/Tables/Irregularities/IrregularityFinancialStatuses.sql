PRINT 'IrregularityFinancialStatuses'
GO

CREATE TABLE [dbo].[IrregularityFinancialStatuses] (
    [IrregularityFinancialStatusId]  INT                 NOT NULL IDENTITY,
    [Name]                           NVARCHAR(MAX)       NOT NULL,
    [Code]                           NVARCHAR(200)       NULL,

    CONSTRAINT [PK_IrregularityFinancialStatuses]       PRIMARY KEY ([IrregularityFinancialStatusId])
);
GO

exec spDescTable  N'IrregularityFinancialStatuses', N'Финансов статус на нередност.'
exec spDescColumn N'IrregularityFinancialStatuses', N'IrregularityFinancialStatusId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IrregularityFinancialStatuses', N'Name'                         , N'Наименование.'
exec spDescColumn N'IrregularityFinancialStatuses', N'Code'                         , N'Код.'
GO
