PRINT 'NotificationEntries'
CREATE TABLE [dbo].[NotificationEntries] (
    [NotificationEntryId]                       INT                 NOT NULL IDENTITY,
    [NotificationEventId]                       INT                 NOT NULL,
    [Status]                                    INT                 NOT NULL,
    [DispatcherId]                              INT                 NOT NULL,
    [ProgrammeId]                               INT                 NULL,
    [ProgrammePriorityId]                       INT                 NULL,
    [ProcedureId]                               INT                 NULL,
    [ContractId]                                INT                 NULL,
    [DispatcherPath]                            NVARCHAR(100)       NULL,
    [FailedAttempts]                            INT                 NOT NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL,
    [Version]                                   ROWVERSION          NOT NULL

    CONSTRAINT [PK_NotificationEntries]                             PRIMARY KEY ([NotificationEntryId]),
    CONSTRAINT [FK_NotificationEntries_NotificationEvents]          FOREIGN KEY ([NotificationEventId])     REFERENCES [dbo].[NotificationEvents]    ([NotificationEventId]),
    CONSTRAINT [CHK_NotificationEntries_Status]                     CHECK       ([Status]                   IN (1, 2, 3)),
);
GO

exec spDescTable  N'NotificationEntries', N'Нотификации.'
exec spDescColumn N'NotificationEntries', N'NotificationEntryId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'NotificationEntries', N'NotificationEventId'        , N'Идентификатор на събитие за нотификация.'
exec spDescColumn N'NotificationEntries', N'Status'                     , N'Статус: 1 - За обработка, 2 - Грешка при обработка, 3 - Обработено.'
exec spDescColumn N'NotificationEntries', N'DispatcherId'               , N'Идентификатор на обекта генериращ нотификацията.'
exec spDescColumn N'NotificationEntries', N'ProgrammeId'                , N'Идентификатор на оперативна програма.'
exec spDescColumn N'NotificationEntries', N'ProgrammePriorityId'        , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'NotificationEntries', N'ProcedureId'                , N'Идентификатор на процедура.'
exec spDescColumn N'NotificationEntries', N'ContractId'                 , N'Идентификатор на договор.'
exec spDescColumn N'NotificationEntries', N'DispatcherPath'             , N'Адрес на който е достъпен ресурса.'
exec spDescColumn N'NotificationEntries', N'FailedAttempts'             , N'Брой неуспешни опити за обработка.'
exec spDescColumn N'NotificationEntries', N'CreateDate'                 , N'Дата на създаване.'
exec spDescColumn N'NotificationEntries', N'ModifyDate'                 , N'Дата на модифициране.'
exec spDescColumn N'NotificationEntries', N'Version'                    , N'Версия.'
