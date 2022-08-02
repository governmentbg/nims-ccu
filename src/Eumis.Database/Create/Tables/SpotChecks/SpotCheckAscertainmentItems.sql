PRINT 'SpotCheckAscertainmentItems'
GO

CREATE TABLE [dbo].[SpotCheckAscertainmentItems] (
    [SpotCheckAscertainmentItemId] INT   NOT NULL IDENTITY,
    [SpotCheckAscertainmentId]     INT   NOT NULL,
    [SpotCheckItemId]              INT   NOT NULL,

    CONSTRAINT [PK_SpotCheckAscertainmentItems]                         PRIMARY KEY ([SpotCheckAscertainmentItemId]),
    CONSTRAINT [FK_SpotCheckAscertainmentItems_SpotCheckAscertainments] FOREIGN KEY ([SpotCheckAscertainmentId]) REFERENCES [dbo].[SpotCheckAscertainments]  ([SpotCheckAscertainmentId]),
    CONSTRAINT [FK_SpotCheckAscertainmentItems_SpotCheckItems]          FOREIGN KEY ([SpotCheckItemId])          REFERENCES [dbo].[SpotCheckItems]           ([SpotCheckItemId])
);
GO

exec spDescTable  N'SpotCheckAscertainmentItems', N'Елементи от обхвата към констатация.'
exec spDescColumn N'SpotCheckAscertainmentItems', N'SpotCheckAscertainmentItemId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SpotCheckAscertainmentItems', N'SpotCheckAscertainmentId'    , N'Идентификатор на констатация.'
exec spDescColumn N'SpotCheckAscertainmentItems', N'SpotCheckItemId'             , N'Идентификатор на елемент от обхвата.'
GO
