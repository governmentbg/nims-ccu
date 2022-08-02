PRINT 'MapNodeFinanceSources'
GO

CREATE TABLE [dbo].[MapNodeFinanceSources](
    [MapNodeId]        INT             NOT NULL,
    [FinanceSource]    INT             NOT NULL,

    CONSTRAINT [PK_MapNodeFinanceSources]                PRIMARY KEY     ([MapNodeId], [FinanceSource]),
    CONSTRAINT [FK_MapNodeFinanceSources_MapNodes]       FOREIGN KEY     ([MapNodeId])       REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [CHK_MapNodeFinanceSources_FinanceSource] CHECK           ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

exec spDescTable  N'MapNodeFinanceSources', N'Фондове към елемент на оперативна карта.'
exec spDescColumn N'MapNodeFinanceSources', N'MapNodeId'                 , N'Идентификатор на елемент на оперативна карта.'
exec spDescColumn N'MapNodeFinanceSources', N'FinanceSource'             , N'Източник на финансиране.'
GO
