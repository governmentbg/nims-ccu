PRINT 'AnnualAccountReportAppendices'
GO

CREATE TABLE [dbo].[AnnualAccountReportAppendices] (
    [AnnualAccountReportAppendixId]                     INT                 NOT NULL IDENTITY,
    [AnnualAccountReportId]                             INT                 NOT NULL,
    [ProgrammePriorityId]                               INT                 NOT NULL,
    [Type]                                              INT                 NOT NULL,
    [Comment]                                           NVARCHAR (MAX)      NULL,

    CONSTRAINT [PK_AnnualAccountReportAppendices]                       PRIMARY KEY ([AnnualAccountReportAppendixId]),
    CONSTRAINT [FK_AnnualAccountReportAppendices_AnnualAccountReports]  FOREIGN KEY ([AnnualAccountReportId])   REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportAppendices_MapNodes]              FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [CHK_AnnualAccountReportAppendices_Type]                 CHECK       ([Type] IN (1, 2)),
);
GO

exec spDescTable  N'AnnualAccountReportAppendices', N'Допълнения към годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportAppendices', N'AnnualAccountReportAppendixId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'AnnualAccountReportAppendices', N'AnnualAccountReportId'            , N'Идентификатор на годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportAppendices', N'ProgrammePriorityId'              , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'AnnualAccountReportAppendices', N'Type'                             , N'Тип на допълнение 1 - Допълнение 5, 2 - Допълнение 8.'
exec spDescColumn N'AnnualAccountReportAppendices', N'Comment'                          , N'Коментар.'

GO
