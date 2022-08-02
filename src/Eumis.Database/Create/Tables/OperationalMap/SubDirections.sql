PRINT 'SubDirections'
GO

CREATE TABLE [dbo].[SubDirections] (
    [SubDirectionId]                INT                NOT NULL    IDENTITY,
    [DirectionId]                   INT                NOT NULL,
    [Name]                          NVARCHAR(1000)     NOT NULL,
    [NameAlt]                       NVARCHAR(1000)     NOT NULL,

    CONSTRAINT [PK_SubDirections]                      PRIMARY KEY ([SubDirectionId]),
    CONSTRAINT [FK_SubDirections_Directions]           FOREIGN KEY ([DirectionId])          REFERENCES [dbo].[Directions] ([DirectionId])
)
GO

exec spDescTable  N'SubDirections', N'Област на направление.'
exec spDescColumn N'SubDirections', N'SubDirectionId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SubDirections', N'DirectionId'         , N'Идентификатор на направление.'
exec spDescColumn N'SubDirections', N'Name'                , N'Наименование.'
exec spDescColumn N'SubDirections', N'NameAlt'             , N'Наименование на друг едзик.'
