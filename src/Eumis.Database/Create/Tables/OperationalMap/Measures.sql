PRINT 'Measures'
GO

CREATE TABLE [dbo].[Measures] (
    [MeasureId]     INT             NOT NULL IDENTITY,
    [ShortName]     NVARCHAR(100)   NOT NULL,
    [Name]          NVARCHAR(100)   NOT NULL,
    [NameAlt]       NVARCHAR(100)   NULL,
    [CreateDate]    DATETIME2       NOT NULL,
    [ModifyDate]    DATETIME2       NOT NULL,
    [Version]       ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Measures] PRIMARY KEY ([MeasureId])
);
GO

exec spDescTable  N'Measures', N'Мерни единици.'
exec spDescColumn N'Measures', N'MeasureId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Measures', N'ShortName'     , N'Кратко име.'
exec spDescColumn N'Measures', N'Name'          , N'Име на български.'
exec spDescColumn N'Measures', N'NameAlt'       , N'Име на английски.'
exec spDescColumn N'Measures', N'CreateDate'    , N'Дата на създаване на записа.'
exec spDescColumn N'Measures', N'ModifyDate'    , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Measures', N'Version'       , N'Версия.'
