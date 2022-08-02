PRINT 'CertReports'
GO

CREATE TABLE [dbo].[CertReports] (
    [CertReportId]                          INT                    NOT NULL IDENTITY,
    [ProgrammeId]                           INT                    NOT NULL,
    [OrderNum]                              INT                    NOT NULL,
    [OrderVersionNum]                       INT                    NOT NULL,
    [CertReportNumber]                      NVARCHAR(30)           NOT NULL,
    [RegDate]                               DATETIME2              NOT NULL,
    [DateFrom]                              DATETIME2              NOT NULL,
    [DateTo]                                DATETIME2              NOT NULL,
    [Status]                                INT                    NOT NULL,
    [StatusNote]                            NVARCHAR(MAX)          NULL,
    [Type]                                  INT                    NOT NULL,
    [ApprovalDate]                          DATETIME2              NULL,

    [CertReportOriginId]                    INT                    NULL,

    [CreateDate]                            DATETIME2              NOT NULL,
    [ModifyDate]                            DATETIME2              NOT NULL,
    [Version]                               ROWVERSION             NOT NULL,

    CONSTRAINT [PK_CertReports]                             PRIMARY KEY ([CertReportId]),
    CONSTRAINT [FK_CertReports_Programmes]                  FOREIGN KEY ([ProgrammeId])                  REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_CertReports_CertReportOrigins]           FOREIGN KEY ([CertReportOriginId])           REFERENCES [dbo].[CertReports] ([CertReportId]),
    CONSTRAINT [CHK_CertReports_Status]                     CHECK       ([Status]                        IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_CertReports_Type]                       CHECK       ([Type]                          IN (1, 2, 3))
);
GO

exec spDescTable  N'CertReports', N'Доклади по сертификация.'
exec spDescColumn N'CertReports', N'CertReportId'                        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CertReports', N'ProgrammeId'                         , N'Идентификатор на оперативна програма.'
exec spDescColumn N'CertReports', N'OrderNum'                            , N'Пореден номер.'
exec spDescColumn N'CertReports', N'CertReportNumber'                    , N'Номер на Доклад по сертификация.'
exec spDescColumn N'CertReports', N'RegDate'                             , N'Дата на регистрация.'
exec spDescColumn N'CertReports', N'DateFrom'                            , N'Период от.'
exec spDescColumn N'CertReports', N'DateTo'                              , N'Период до.'
exec spDescColumn N'CertReports', N'Status'                              , N'Статус: 1 - Чернова, 2 - Приключен, 3 - В проверка, 4 - Одобрен, 5 - Частично одобрен, 6 - Неодобрен, 7 - Върнат.'
exec spDescColumn N'CertReports', N'Type'                                , N'Тип: 1 – Междинен, 2 – Финален, 3 – Годишен.'
exec spDescColumn N'CertReports', N'ApprovalDate'                        , N'Дата на одобрение.'

exec spDescColumn N'CertReports', N'CreateDate'                          , N'Дата на създаване на записа.'
exec spDescColumn N'CertReports', N'ModifyDate'                          , N'Дата на последно редактиране на записа.'
exec spDescColumn N'CertReports', N'Version'                             , N'Версия.'

GO