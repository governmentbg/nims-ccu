PRINT 'ProjectTypes'
GO

CREATE TABLE [dbo].[ProjectTypes] (
    [ProjectTypeId]     INT             NOT NULL IDENTITY,
    [Name]              NVARCHAR(MAX)   NOT NULL,
    [NameAlt]           NVARCHAR(MAX)   NOT NULL,
    [Alias]             NVARCHAR(50)    NULL,

    CONSTRAINT [PK_ProjectTypes] PRIMARY KEY ([ProjectTypeId])
);
GO

exec spDescTable  N'ProjectTypes', N'Типове проектно предложение.'
exec spDescColumn N'ProjectTypes', N'ProjectTypeId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectTypes', N'Name'              , N'Наименование.'
exec spDescColumn N'ProjectTypes', N'NameAlt'           , N'Наименование на друг език.'
exec spDescColumn N'ProjectTypes', N'Alias'             , N'Псевдоним.'
/* Name exams
Проектен фиш
Предложение за предварителен подбор
Проектно предложение
*/
GO
