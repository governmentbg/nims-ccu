PRINT 'ContractReportTechnicalMembers'
GO

CREATE TABLE [dbo].[ContractReportTechnicalMembers] (
    [ContractReportTechnicalMemberId]       INT                   NOT NULL IDENTITY,
    [ContractReportTechnicalId]             INT                   NOT NULL,
    [ContractReportId]                      INT                   NOT NULL,
    [ContractId]                            INT                   NOT NULL,
    [Name]                                  NVARCHAR(MAX)         NULL,
    [Position]                              NVARCHAR(MAX)         NULL,
    [Uin]                                   NVARCHAR(MAX)         NULL,
    [UinType]                               INT                   NULL,
    [CommitmentType]                        INT                   NULL,
    [Date]                                  DATETIME2             NULL,
    [Hours]                                 DECIMAL(15,3)         NULL,
    [Activity]                              NVARCHAR(MAX)         NULL,
    [Result]                                NVARCHAR(MAX)         NULL,

    CONSTRAINT [PK_ContractReportTechnicalMembers]                                  PRIMARY KEY ([ContractReportTechnicalMemberId]),
    CONSTRAINT [FK_ContractReportTechnicalMembers_Contracts]                        FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportTechnicalMembers_ContractReports]                  FOREIGN KEY ([ContractReportId])            REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportTechnicalMembers_ContractReportTechnicals]         FOREIGN KEY ([ContractReportTechnicalId])   REFERENCES [dbo].[ContractReportTechnicals] ([ContractReportTechnicalId]),
    CONSTRAINT [CHK_ContractReportTechnicalMembers_UinType]                         CHECK ([UinType]          IN (1, 2)),
    CONSTRAINT [CHK_ContractReportTechnicalMembers_CommitmentType]                  CHECK ([CommitmentType]   IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'ContractReportTechnicalMembers', N'Екип към технически отчет.'
exec spDescColumn N'ContractReportTechnicalMembers', N'ContractReportTechnicalMemberId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportTechnicalMembers', N'ContractReportTechnicalId'        , N'Идентификатор на технически отчет.'
exec spDescColumn N'ContractReportTechnicalMembers', N'ContractReportId'                 , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportTechnicalMembers', N'ContractId'                       , N'Идентификатор на договор'

exec spDescColumn N'ContractReportTechnicalMembers', N'Name'                             , N'Име по документ за самоличност.'
exec spDescColumn N'ContractReportTechnicalMembers', N'Position'                         , N'Позиция по проекта/споразумението.'
exec spDescColumn N'ContractReportTechnicalMembers', N'Uin'                              , N'Тип идентификатор: 1 - ЕГН, 2 - ЛНЧ.'
exec spDescColumn N'ContractReportTechnicalMembers', N'UinType'                          , N'Идентификатор'
exec spDescColumn N'ContractReportTechnicalMembers', N'CommitmentType'                   , N'Тип на ангажимента: 1 - Граждански договор, 2 - Трудов договор, 3 - Заповед, 4 - Друго.'
exec spDescColumn N'ContractReportTechnicalMembers', N'Date'                             , N'Дата.'
exec spDescColumn N'ContractReportTechnicalMembers', N'Hours'                            , N'Отработени часове.'
exec spDescColumn N'ContractReportTechnicalMembers', N'Activity'                         , N'Извършена дейност.'
exec spDescColumn N'ContractReportTechnicalMembers', N'Result'                           , N'Конкретни резултати.'


