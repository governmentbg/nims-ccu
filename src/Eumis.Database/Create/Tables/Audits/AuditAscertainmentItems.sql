PRINT 'AuditAscertainmentItems'
GO

CREATE TABLE [dbo].[AuditAscertainmentItems] (
    [AuditAscertainmentItemId] INT   NOT NULL IDENTITY,
    [AuditAscertainmentId]     INT   NOT NULL,
    [AuditLevelItemId]         INT   NOT NULL,

    CONSTRAINT [PK_AuditAscertainmentItems]                     PRIMARY KEY ([AuditAscertainmentItemId]),
    CONSTRAINT [FK_AuditAscertainmentItems_AuditAscertainments] FOREIGN KEY ([AuditAscertainmentId]) REFERENCES [dbo].[AuditAscertainments]  ([AuditAscertainmentId]),
    CONSTRAINT [FK_AuditAscertainmentItems_AuditLevelItems]     FOREIGN KEY ([AuditLevelItemId])     REFERENCES [dbo].[AuditLevelItems]      ([AuditLevelItemId])
);
GO

exec spDescTable  N'AuditAscertainmentItems', N'Елементи от обхвата към констатация.'
exec spDescColumn N'AuditAscertainmentItems', N'AuditAscertainmentItemId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'AuditAscertainmentItems', N'AuditAscertainmentId'    , N'Идентификатор на констатация.'
exec spDescColumn N'AuditAscertainmentItems', N'AuditLevelItemId'        , N'Идентификатор на елемент от обхвата.'
GO
