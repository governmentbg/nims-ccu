PRINT 'Directions'
GO

CREATE TABLE [dbo].[Directions] (
    [DirectionId]                   INT                NOT NULL    IDENTITY,
    [Name]                          NVARCHAR(1000)     NOT NULL,
    [NameAlt]                       NVARCHAR(1000)     NOT NULL,
    [Status]                        INT                NOT NULL,

    [CreateDate]                    DATETIME2          NOT NULL,
    [ModifyDate]                    DATETIME2          NOT NULL,
    [Version]                       ROWVERSION         NOT NULL,

    CONSTRAINT [PK_Directions]                         PRIMARY KEY ([DirectionId]),
    CONSTRAINT [CHK_Directions_Status]                 CHECK       ([Status] IN (1, 2))
)
GO

exec spDescTable  N'Directions', N'Направления.'
exec spDescColumn N'Directions', N'DirectionId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Directions', N'Name'                , N'Наименование.'
exec spDescColumn N'Directions', N'NameAlt'             , N'Наименование на друг едзик.'
exec spDescColumn N'Directions', N'Status'              , N'Статус: 1 - Чернова, 2 - Въведен.'
exec spDescColumn N'Directions', N'CreateDate'          , N'Дата на създаване.'
exec spDescColumn N'Directions', N'ModifyDate'          , N'Дата на последно модифициране.'
exec spDescColumn N'Directions', N'Version'             , N'Версия.'
