PRINT 'ProgrammeCheckLists'
GO

CREATE TABLE [dbo].[ProgrammeCheckLists] (
    [ProgrammeCheckListId]                      INT                 NOT NULL IDENTITY,
    [MapNodeId]                                 INT                 NOT NULL,
    [Name]                                      NVARCHAR(MAX)       NOT NULL,
    [ProgrammeName]                             NVARCHAR(MAX)       NOT NULL,
    [Description]                               NVARCHAR(MAX)       NOT NULL,
    [Type]                                      INT                 NOT NULL,
    [Status]                                    INT                 NOT NULL,
    [ActivationDate]                            DATETIME2           NULL,
    [ActivatedByUserId]                         INT                 NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL,
    [Version]                                   ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ProgrammeCheckLists]                          PRIMARY KEY ([ProgrammeCheckListId]),
    CONSTRAINT [FK_ProgrammeCheckLists_MapNodes]                 FOREIGN KEY ([MapNodeId])          REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ProgrammeCheckLists_Users]                    FOREIGN KEY ([ActivatedByUserId])  REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ProgrammeCheckLists_Type]                    CHECK       ([Type] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)),
    CONSTRAINT [CHK_ProgrammeCheckLists_Status]                  CHECK       ([Status] IN (1, 2, 3))
);
GO

exec spDescTable  N'ProgrammeCheckLists', N'Контролен лист.'
exec spDescColumn N'ProgrammeCheckLists', N'ProgrammeCheckListId'              , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeCheckLists', N'MapNodeId'                         , N'Идентификатор на оперативна програма.'
exec spDescColumn N'ProgrammeCheckLists', N'Name'                              , N'Наименование.'
exec spDescColumn N'ProgrammeCheckLists', N'ProgrammeName'                     , N'Наименование на оперативна програма.'
exec spDescColumn N'ProgrammeCheckLists', N'Description'                       , N'Описание.'
exec spDescColumn N'ProgrammeCheckLists', N'Type'                              , N'Тип на контролния лист: 1 - Процедура, 2 - Договор, 3 - Пакет отчетни документи, 4 - Процедура за избор на изпълнител, 5 - Доклад по сертификация, 6 - Проверка на място, 7 - Оперативна програма, 8 - Сигнал за нередност,  9 - Корекция на верифицирани суми на ниво РОД, 10 - Препотвърждаване на верифицирани суми на ниво РОД,  11 - Корекция на верифицирани суми на други нива, 12 - Препотвърждаване на верифицирани суми на други нива.'
exec spDescColumn N'ProgrammeCheckLists', N'Status'                            , N'Статус на контролния лист: 1 - Неактивиран, 2 - Активен, 3 - Неактивен.'
exec spDescColumn N'ProgrammeCheckLists', N'ActivationDate'                    , N'Дата на активиране.'
exec spDescColumn N'ProgrammeCheckLists', N'ActivatedByUserId'                 , N'Идентификатор на потребител активирал контролния лист.'
exec spDescColumn N'ProgrammeCheckLists', N'CreateDate'                        , N'Дата на създаване на записа.'
exec spDescColumn N'ProgrammeCheckLists', N'ModifyDate'                        , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProgrammeCheckLists', N'Version'                           , N'Версия.'
GO
