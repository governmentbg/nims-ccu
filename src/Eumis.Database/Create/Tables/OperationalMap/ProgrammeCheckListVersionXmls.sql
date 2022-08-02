PRINT 'ProgrammeCheckListVersionXmls'
GO

CREATE TABLE [dbo].[ProgrammeCheckListVersionXmls] (
    [ProgrammeCheckListVersionXmlId]            INT                 NOT NULL IDENTITY,
    [Gid]                                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ProgrammeCheckListId]                      INT                 NOT NULL,
    [Xml]                                       XML                 NOT NULL,
    [Hash]                                      NVARCHAR(10)        NOT NULL UNIQUE,
    [VersionNum]                                INT                 NOT NULL,
    [Status]                                    INT                 NOT NULL,
    [ActivationDate]                            DATETIME2           NULL,
    [ActivatedByUserId]                         INT                 NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL,
    [Version]                                   ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ProgrammeCheckListVersionXmls]                          PRIMARY KEY ([ProgrammeCheckListVersionXmlId]),
    CONSTRAINT [FK_ProgrammeCheckListVersionXmls_ProgrammeCheckLists]      FOREIGN KEY ([ProgrammeCheckListId])       REFERENCES [dbo].[ProgrammeCheckLists] ([ProgrammeCheckListId]),
    CONSTRAINT [FK_ProgrammeCheckListVersionXmls_Users]                    FOREIGN KEY ([ActivatedByUserId])          REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ProgrammeCheckListVersionXmls_Status]                  CHECK       ([Status] IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'ProgrammeCheckListVersionXmls', N'Xml за контролен лист.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'ProgrammeCheckListVersionXmlId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'Gid'                               , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'ProgrammeCheckListId'              , N'Идентификатор на контролен лист.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'Xml'                               , N'Xml съдържание.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'Hash'                              , N'Уникален идентификатор на съдържанието на Xml-а.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'VersionNum'                        , N'Пореден номер на версия.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'Status'                            , N'Статус: 1 - Чернова, 2 - Въведен, 3 - Актуален, 4 - Архивиран.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'ActivationDate'                    , N'Дата на активиране.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'ActivatedByUserId'                 , N'Идентификатор на потребител активирал версията.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'CreateDate'                        , N'Дата на създаване на записа.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'ModifyDate'                        , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProgrammeCheckListVersionXmls', N'Version'                           , N'Версия.'

GO
