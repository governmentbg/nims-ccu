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


WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10044' as t,
    N'http://ereg.egov.bg/segment/R-10057' as tm,
    N'http://ereg.egov.bg/segment/R-10000' as ut,
    N'http://ereg.egov.bg/segment/R-09991' as ct
),
TechnicalReportMembers as
(
SELECT
  crt.[ContractReportTechnicalId],
  crt.[ContractReportId],
  crt.[ContractId],
  Name = a.b.value('(tm:Name/text())[1]', 'NVARCHAR(MAX)'),
  Position = a.b.value('(tm:Position/text())[1]', 'NVARCHAR(MAX)'),
  Uin = a.b.value('(tm:Uin/text())[1]', 'NVARCHAR(MAX)'),
  UinType = CASE a.b.value('(tm:UinType/ut:Id/text())[1]', 'NVARCHAR(MAX)')
                WHEN 'egn' THEN 1
                WHEN 'EGN' THEN 1
                WHEN 'foreignperson' THEN 2
                WHEN 'foreignPerson' THEN 2
                ELSE 1
            END,
  CommitmentType = CASE a.b.value('(tm:CommitmentType/ct:Value/text())[1]', 'NVARCHAR(MAX)')
                WHEN 'civilContract' THEN 1
                WHEN 'civilcontract' THEN 1
                WHEN 'employmentContract' THEN 2
                WHEN 'employmentcontract' THEN 2
                WHEN 'order' THEN 3
                WHEN 'other' THEN 4
                ELSE 1
            END,
  Date = a.b.value('(tm:Date/text())[1]', 'DATETIME2'),
  Hours = a.b.value('(tm:Hours/text())[1]', 'DECIMAL(15,3)'),
  Activity = a.b.value('(tm:Activity/text())[1]', 'NVARCHAR(MAX)'),
  Result = a.b.value('(tm:Result/text())[1]', 'NVARCHAR(MAX)')
FROM [dbo].[ContractReportTechnicals] crt
CROSS APPLY crt.[Xml].nodes('(//TechnicalReport/t:Team/t:TeamMember)') AS a(b)
WHERE crt.[Status] = 3
)
INSERT INTO dbo.[ContractReportTechnicalMembers]
SELECT * FROM TechnicalReportMembers;
GO