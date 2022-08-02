PRINT 'Counters'
GO

CREATE TABLE [dbo].[Counters] (
    [Name]                        NVARCHAR(200)        NOT NULL UNIQUE,
    [CurrentNumber]               INT                  NOT NULL
    CONSTRAINT [PK_Counters]                           PRIMARY KEY CLUSTERED ([Name] ASC)
);
GO

exec spDescTable  N'Counters', N'Броячи.'
exec spDescColumn N'Counters', N'Name'          , N'Уникално име на брояча.'
exec spDescColumn N'Counters', N'CurrentNumber' , N'Текущ пореден номер.'
GO
