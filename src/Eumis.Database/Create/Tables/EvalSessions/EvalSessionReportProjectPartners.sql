PRINT 'EvalSessionReportProjectPartners'
GO

CREATE TABLE [dbo].[EvalSessionReportProjectPartners] (
    [EvalSessionReportProjectPartnerId]    INT                NOT NULL IDENTITY,
    [EvalSessionId]                        INT                NOT NULL,
    [EvalSessionReportId]                  INT                NOT NULL,
    [ProjectId]                            INT                NOT NULL,

    [PartnerUin]                           NVARCHAR(200)      NOT NULL,
    [PartnerName]                          NVARCHAR(200)      NOT NULL,
    [PartnerLegalTypeId]                   INT                NOT NULL,
    [PartnerAddress]                       NVARCHAR(MAX)      NOT NULL,
    [PartnerRepresentative]                NVARCHAR(MAX)      NOT NULL,
    [PartnerFinancialContribution]         MONEY              NULL,

    CONSTRAINT [PK_EvalSessionReportProjectPartners]                             PRIMARY KEY ([EvalSessionReportProjectPartnerId]),
    CONSTRAINT [FK_EvalSessionReportProjectPartners_EvalSessions]                FOREIGN KEY ([EvalSessionId])                                       REFERENCES [dbo].[EvalSessions]              ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionReportProjectPartners_EvalSessionReports]          FOREIGN KEY ([EvalSessionReportId])                                 REFERENCES [dbo].[EvalSessionReports]        ([EvalSessionReportId]),
    CONSTRAINT [FK_EvalSessionReportProjectPartners_Projects]                    FOREIGN KEY ([ProjectId])                                           REFERENCES [dbo].[Projects]                  ([ProjectId]),
    CONSTRAINT [FK_EvalSessionReportProjectPartners_EvalSessionReportProjects]   FOREIGN KEY ([EvalSessionId], [EvalSessionReportId], [ProjectId])   REFERENCES [dbo].[EvalSessionReportProjects] ([EvalSessionId], [EvalSessionReportId], [ProjectId]),
    CONSTRAINT [FK_EvalSessionReportProjectPartners_CompanyLegalTypes]           FOREIGN KEY ([PartnerLegalTypeId])                                  REFERENCES [dbo].[CompanyLegalTypes]         ([CompanyLegalTypeId])
);
GO

exec spDescTable  N'EvalSessionReportProjectPartners', N'Партньори към проектни предложения към документ.'
exec spDescColumn N'EvalSessionReportProjectPartners', N'EvalSessionReportProjectPartnerId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionReportProjectPartners', N'EvalSessionId'                    , N'Идентификатор на оценителна сесия'
exec spDescColumn N'EvalSessionReportProjectPartners', N'EvalSessionReportId'              , N'Идентификатор на документ към оценителна сесия'
exec spDescColumn N'EvalSessionReportProjectPartners', N'ProjectId'                        , N'Идентификатор на проектно предложение'

exec spDescColumn N'EvalSessionReportProjectPartners', N'PartnerUin'                       , N'ЕИК/Булстат.'
exec spDescColumn N'EvalSessionReportProjectPartners', N'PartnerName'                      , N'Пълно наименование'
exec spDescColumn N'EvalSessionReportProjectPartners', N'PartnerLegalTypeId'               , N'Вид на организацията.'
exec spDescColumn N'EvalSessionReportProjectPartners', N'PartnerAddress'                   , N'Aдрес.'
exec spDescColumn N'EvalSessionReportProjectPartners', N'PartnerRepresentative'            , N'Имена на лицето, представляващо организацията.'
exec spDescColumn N'EvalSessionReportProjectPartners', N'PartnerFinancialContribution'     , N'Финансово участие.'
GO
