PRINT 'TeamPositions'
GO

CREATE TABLE [dbo].[TeamPositions] (
    [TeamPositionId]     INT             NOT NULL IDENTITY,
    [Name]              NVARCHAR(MAX)   NOT NULL,

    CONSTRAINT [PK_TeamPositions] PRIMARY KEY ([TeamPositionId])
);
GO

exec spDescTable  N'TeamPositions', N'Позиция на член на екип.'
exec spDescColumn N'TeamPositions', N'TeamPositionId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'TeamPositions', N'Name'              , N'Наименование.'
/* Name exams
Ръководител на проекта
Счетоводител на проекта
Технически експерт
Финансов експерт
Координатор на проекта
Юрист
Друга роля
Технически експерт
Финансов експерт
Юрист
Друга роля
*/
GO
