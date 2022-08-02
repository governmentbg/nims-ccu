PRINT 'CheckSheetVersionXmls'
GO

CREATE TABLE [dbo].[CheckSheetVersionXmls] (
    [CheckSheetVersionXmlId]                    INT                 NOT NULL IDENTITY,
    [Gid]                                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [CheckSheetId]                              INT                 NOT NULL,
    [VersionNum]                                INT                 NOT NULL,
    [Xml]                                       XML                 NOT NULL,
    [Hash]                                      NVARCHAR(10)        NOT NULL UNIQUE,
    [UserId]                                    INT                 NOT NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL,
    [Version]                                   ROWVERSION          NOT NULL,

    CONSTRAINT [PK_CheckSheetVersionXmls]                          PRIMARY KEY ([CheckSheetVersionXmlId]),
    CONSTRAINT [FK_CheckSheetVersionXmls_CheckSheets]              FOREIGN KEY ([CheckSheetId])            REFERENCES [dbo].[CheckSheets] ([CheckSheetId]),
    CONSTRAINT [FK_CheckSheetVersionXmls_Users]                    FOREIGN KEY ([UserId])                  REFERENCES [dbo].[Users] ([UserId])
);
GO

exec spDescTable  N'CheckSheetVersionXmls', N'Xml за контролен лист.'
exec spDescColumn N'CheckSheetVersionXmls', N'CheckSheetVersionXmlId'            , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CheckSheetVersionXmls', N'Gid'                               , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'CheckSheetVersionXmls', N'CheckSheetId'                      , N'Идентификатор на контролен лист.'
exec spDescColumn N'CheckSheetVersionXmls', N'VersionNum'                        , N'Пореден номер на версия.'
exec spDescColumn N'CheckSheetVersionXmls', N'Xml'                               , N'Xml съдържание.'
exec spDescColumn N'CheckSheetVersionXmls', N'Hash'                              , N'Уникален идентификатор на съдържанието на Xml-а.'
exec spDescColumn N'CheckSheetVersionXmls', N'UserId'                            , N'Идентификатор на потребител създал версията.'
exec spDescColumn N'CheckSheetVersionXmls', N'CreateDate'                        , N'Дата на създаване на записа.'
exec spDescColumn N'CheckSheetVersionXmls', N'ModifyDate'                        , N'Дата на последно редактиране на записа.'
exec spDescColumn N'CheckSheetVersionXmls', N'Version'                           , N'Версия.'

GO
