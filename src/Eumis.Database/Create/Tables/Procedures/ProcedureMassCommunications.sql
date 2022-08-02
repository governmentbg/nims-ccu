PRINT 'ProcedureMassCommunications'
GO

CREATE TABLE [dbo].[ProcedureMassCommunications] (
    [ProcedureMassCommunicationId]                  INT             NOT NULL IDENTITY,
    [ProgrammeId]                                   INT             NOT NULL,
    [ProcedureId]                                   INT             NOT NULL,
    [Status]                                        INT             NOT NULL,
    [Subject]                                       NVARCHAR(MAX)   NULL,
    [Body]                                          NVARCHAR(MAX)   NULL,

    [CreateDate]                                    DATETIME2       NOT NULL,
    [ModifyDate]                                    DATETIME2       NOT NULL,
    [Version]                                       ROWVERSION      NOT NULL,

    CONSTRAINT [PK_ProcedureMassCommunications]                     PRIMARY KEY ([ProcedureMassCommunicationId]),
    CONSTRAINT [FK_ProcedureMassCommunications_Procedures]          FOREIGN KEY ([ProcedureId])           REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureMassCommunications_MapNodes]            FOREIGN KEY ([ProgrammeId])           REFERENCES [dbo].[MapNodes]    ([MapNodeId]),
    CONSTRAINT [CHK_ProcedureMassCommunications_Status]             CHECK       ([Status] IN (1, 2)),
);
GO

exec spDescTable  N'ProcedureMassCommunications', N'Обща кореспонденция.'
exec spDescColumn N'ProcedureMassCommunications', N'ProcedureMassCommunicationId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureMassCommunications', N'ProgrammeId'                  , N'Идентификатор на оперативна програма.'
exec spDescColumn N'ProcedureMassCommunications', N'ProcedureId'                  , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureMassCommunications', N'Status'                       , N'Статус: 1 - Чернова, 2 - Изпратена.'
exec spDescColumn N'ProcedureMassCommunications', N'Subject'                      , N'Тема на кореспонденцията.'
exec spDescColumn N'ProcedureMassCommunications', N'Body'                         , N'Съдържание.'
exec spDescColumn N'ProcedureMassCommunications', N'CreateDate'                   , N'Дата на създаване на записа.'
exec spDescColumn N'ProcedureMassCommunications', N'ModifyDate'                   , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProcedureMassCommunications', N'Version'                      , N'Версия.'
GO
