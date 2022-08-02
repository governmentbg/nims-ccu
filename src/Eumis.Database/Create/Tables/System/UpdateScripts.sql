PRINT 'UpdateScripts'
GO

CREATE TABLE [dbo].[UpdateScripts] (
    [Id] INT IDENTITY(1,1) NOT NULL,
	[ScriptName] NVARCHAR(255) NOT NULL,
	[Applied] DATETIME NOT NULL,
    CONSTRAINT [PK_UpdateScripts] PRIMARY KEY ([Id])
);
GO

exec spDescTable  N'UpdateScripts', N'Системна таблица за скриптове за обновление.'
exec spDescColumn N'UpdateScripts', N'Id'           , N'Идентификатор.'
exec spDescColumn N'UpdateScripts', N'ScriptName'   , N'Приложен скрипт за обновление.'
exec spDescColumn N'UpdateScripts', N'Applied'      , N'Дата на приложение.'
GO
