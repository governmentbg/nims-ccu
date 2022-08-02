PRINT 'EvalSessionStatuses'
GO

CREATE TABLE [dbo].[EvalSessionStatuses] (
    [EvalSessionStatusId]       INT             NOT NULL IDENTITY,
    [Name]                  NVARCHAR(MAX)   NOT NULL,

    CONSTRAINT [PK_EvalSessionStatuses] PRIMARY KEY ([EvalSessionStatusId])
);
GO

exec spDescTable  N'EvalSessionStatuses', N'Статуси на оценителна сесия.'
exec spDescColumn N'EvalSessionStatuses', N'EvalSessionStatusId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionStatuses', N'Name'                , N'Наименование.'
/* Name exams
Оценка
Доклад
Приоритизиращ доклад
Решение
Приключена
Промяна/ново решение
*/
GO
