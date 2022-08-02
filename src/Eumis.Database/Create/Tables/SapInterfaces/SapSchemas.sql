PRINT 'SapSchemas'
GO

CREATE TABLE [dbo].[SapSchemas] (
    [SapSchemaId]      INT               NOT NULL IDENTITY,
    [Type]             INT               NOT NULL,
    [Content]          XML               NOT NULL,
    [IsActive]         BIT               NOT NULL,
    [CreateDate]       DATETIME2         NOT NULL,
    [ModifyDate]       DATETIME2         NOT NULL,
    [Version]          ROWVERSION        NOT NULL,

    [IsActiveCheck] AS
        CASE [IsActive]
            WHEN 1 THEN -1
            WHEN 0 THEN [SapSchemaId]
        END,

    CONSTRAINT [PK_SapSchemas]                      PRIMARY KEY ([SapSchemaId]),
    CONSTRAINT [UQ_SapSchemas_SingleTypeIsActive]   UNIQUE ([IsActiveCheck], [Type]),
    CONSTRAINT [CHK_SapSchemas_Type]                CHECK  ([Type] IN (1, 2))
);
GO

exec spDescTable  N'SapSchemas', N'Схеми на файловете от SAP.'
exec spDescColumn N'SapSchemas', N'SapSchemaId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SapSchemas', N'Type'           , N'Вид SAP файл, 1 - Изплатени суми, 2 - възстановени лимити.'
exec spDescColumn N'SapSchemas', N'Content'        , N'Схема.'
exec spDescColumn N'SapSchemas', N'IsActive'       , N'Маркер за активност.'
exec spDescColumn N'SapSchemas', N'CreateDate'     , N'Дата на създаване на записа.'
exec spDescColumn N'SapSchemas', N'ModifyDate'     , N'Дата на последно редактиране на записа.'
exec spDescColumn N'SapSchemas', N'Version'        , N'Версия.'
GO
