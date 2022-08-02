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
