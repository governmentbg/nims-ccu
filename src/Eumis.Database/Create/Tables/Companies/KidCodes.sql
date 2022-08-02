PRINT 'KidCodes'
GO

CREATE TABLE [dbo].[KidCodes] (
    [KidCodeId] INT             NOT NULL IDENTITY,
    [Code]      NVARCHAR(50)    NOT NULL UNIQUE,
    [Name]      NVARCHAR(MAX)   NOT NULL,
    [NameAlt]   NVARCHAR(200)   NULL,

    CONSTRAINT [PK_KidCodes]   PRIMARY KEY ([KidCodeId])
);
GO

exec spDescTable  N'KidCodes', N'Класификация на икономическите дейности (КИД 2008)'
exec spDescColumn N'KidCodes', N'KidCodeId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'KidCodes', N'Code'         , N'Код.'
exec spDescColumn N'KidCodes', N'Name'         , N'Наименование.'
exec spDescColumn N'KidCodes', N'NameAlt'      , N'Наименование на английски.'
GO
