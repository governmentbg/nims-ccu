PRINT 'SpotCheckTargets'
GO

CREATE TABLE [dbo].[SpotCheckTargets] (
    [SpotCheckTargetId]     INT              NOT NULL IDENTITY,
    [SpotCheckId]           INT              NOT NULL,
    [Type]                  INT              NOT NULL,
    [Name]                  NVARCHAR(500)    NOT NULL,

    CONSTRAINT [PK_SpotCheckTargets]               PRIMARY KEY ([SpotCheckTargetId]),
    CONSTRAINT [FK_SpotCheckTargets_SpotChecks]    FOREIGN KEY ([SpotCheckId])          REFERENCES [dbo].[SpotChecks]       ([SpotCheckId]),
    CONSTRAINT [CHK_SpotCheckTargets_Type]         CHECK ([Type] IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'SpotCheckTargets', N'Обекти на проверка.'
exec spDescColumn N'SpotCheckTargets', N'SpotCheckTargetId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SpotCheckTargets', N'SpotCheckId'      , N'Идентификатор на проверка на място.'
exec spDescColumn N'SpotCheckTargets', N'Type'             , N'Обект на проверка: 1 – Бенефициент, 2 – Изпълнител, 3 – Краен получател, 4 - Партньор.'
exec spDescColumn N'SpotCheckTargets', N'Name'             , N'Наименование на проверявания.'
GO
