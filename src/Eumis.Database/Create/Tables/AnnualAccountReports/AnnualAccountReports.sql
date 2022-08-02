PRINT 'AnnualAccountReports'
GO

CREATE TABLE [dbo].[AnnualAccountReports] (
    [AnnualAccountReportId]                 INT                     NOT NULL IDENTITY,
    [ProgrammeId]                           INT                     NOT NULL,
    [OrderNum]                              INT                     NOT NULL,
    [OrderVersionNum]                       INT                     NOT NULL,
    [RegDate]                               DATETIME2               NOT NULL,
    [Status]                                INT                     NOT NULL,
    [StatusNote]                            NVARCHAR(MAX)           NULL,
    [ApprovalDate]                          DATETIME2               NULL,
    [AccountPeriod]                         INT                     NOT NULL,
    [CreateDate]                            DATETIME2               NOT NULL,
    [ModifyDate]                            DATETIME2               NOT NULL,
    [Version]                               ROWVERSION              NOT NULL,

    CONSTRAINT [PK_AnnualAccountReports]                PRIMARY KEY ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReports_Programmes]     FOREIGN KEY ([ProgrammeId])					REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [CHK_AnnualAccountReports_Status]        CHECK       ([Status]                       IN (1, 2, 3)),
    CONSTRAINT [CHK_AnnualAccountReports_AccountPeriod] CHECK       ([AccountPeriod]                IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
);
GO

exec spDescTable  N'AnnualAccountReports', N'Годишни счетоводни отчети.'
exec spDescColumn N'AnnualAccountReports', N'AnnualAccountReportId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'AnnualAccountReports', N'ProgrammeId'           , N'Идентификатор на оперативна програма.'
exec spDescColumn N'AnnualAccountReports', N'OrderNum'              , N'Пореден номер.'
exec spDescColumn N'AnnualAccountReports', N'RegDate'               , N'Дата на регистрация.'
exec spDescColumn N'AnnualAccountReports', N'AccountPeriod'         , N'Счетоводен период: 1 - 2014-2015, 2 - 2015-2016, 3 - 2016-2017, 4 - 2017-2018, 5 - 2018-2019, 6 - 2019-2020, 7 - 2020-2021, 8 - 2021-2022, 9 - 2022-2023, 10 - 2023-2024.'
exec spDescColumn N'AnnualAccountReports', N'Status'                , N'Статус: 1 - Чернова, 2 - Приключен, 3 - Върнат.'
exec spDescColumn N'AnnualAccountReports', N'ApprovalDate'          , N'Дата на одобрение.'
exec spDescColumn N'AnnualAccountReports', N'CreateDate'            , N'Дата на създаване на записа.'
exec spDescColumn N'AnnualAccountReports', N'ModifyDate'            , N'Дата на последно редактиране на записа.'
exec spDescColumn N'AnnualAccountReports', N'Version'               , N'Версия.'

GO
