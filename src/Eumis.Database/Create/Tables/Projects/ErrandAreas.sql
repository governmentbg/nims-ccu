PRINT 'ErrandAreas'
GO

CREATE TABLE [dbo].[ErrandAreas] (
    [ErrandAreaId]     INT              NOT NULL IDENTITY,
    [Code]             NVARCHAR(50)     NOT NULL UNIQUE,
    [Name]             NVARCHAR(MAX)    NOT NULL,

    CONSTRAINT [PK_ErrandAreas] PRIMARY KEY ([ErrandAreaId])
);
GO

exec spDescTable  N'ErrandAreas', N'Приложна област на процедура за външно  възлагане.'
exec spDescColumn N'ErrandAreas', N'ErrandAreaId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ErrandAreas', N'Code'           , N'Код.'
exec spDescColumn N'ErrandAreas', N'Name'           , N'Наименование.'
/* Name exams
0 - Доставка
1 - Услуга
2 - Строителство
*/
GO
