PRINT 'EvalSessionTypes'
GO

CREATE TABLE [dbo].[EvalSessionTypes] (
    [EvalSessionTypeId]     INT             NOT NULL IDENTITY,
    [Name]                  NVARCHAR(MAX)   NOT NULL,

    CONSTRAINT [PK_EvalSessionTypes] PRIMARY KEY ([EvalSessionTypeId])
);
GO

exec spDescTable  N'EvalSessionTypes', N'Тип оценителна сесия.'
exec spDescColumn N'EvalSessionTypes', N'EvalSessionTypeId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionTypes', N'Name'                  , N'Наименование.'
/* Name exams
За предварителен подбор
За оценка на проектни предложения
За оценка на проектни фишове
*/
GO
