-- Create staging tables matching the production tables
CREATE TABLE [dbo].[ContractProcurementXmlFiles_Staging] (
    [ContractProcurementXmlFileId]  INT                 NOT NULL IDENTITY,
    [ContractProcurementXmlId]      INT                 NOT NULL,
    [Type]                          INT                 NOT NULL,
    [BlobKey]                       UNIQUEIDENTIFIER    NOT NULL,
    [Name]                          NVARCHAR(200)       NOT NULL,
    [Description]                   NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractProcurementXmlFiles_Staging]                           PRIMARY KEY ([ContractProcurementXmlFileId]),
    CONSTRAINT [FK_ContractProcurementXmlFiles_ContractProcurementXmls_Staging]   FOREIGN KEY ([ContractProcurementXmlId])    REFERENCES [dbo].[ContractProcurementXmls] ([ContractProcurementXmlId]),
    CONSTRAINT [FK_ContractProcurementXmlFiles_Blobs_Staging]                     FOREIGN KEY ([BlobKey])                     REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ContractProcurementXmlFiles_Type_Staging]                     CHECK ([Type] IN (1, 2, 3))
);
GO

CREATE TABLE [dbo].[EvalSessionSheetXmlFiles_Staging] (
    [EvalSessionSheetXmlFileId]         INT                 NOT NULL IDENTITY,
    [EvalSessionSheetXmlId]             INT                 NOT NULL,
    [BlobKey]                           UNIQUEIDENTIFIER    NOT NULL,
    [Name]                              NVARCHAR(200)       NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,
    [Type]                              INT                 NOT NULL,

    CONSTRAINT [PK_EvalSessionSheetXmlFiles_Staging]                          PRIMARY KEY ([EvalSessionSheetXmlFileId]),
    CONSTRAINT [FK_EvalSessionSheetXmlFiles_EvalSessionSheetXmls_Staging]     FOREIGN KEY ([EvalSessionSheetXmlId])    REFERENCES [dbo].[EvalSessionSheetXmls] ([EvalSessionSheetXmlId]),
    CONSTRAINT [FK_EvalSessionSheetXmlFiles_Blobs_Staging]                    FOREIGN KEY ([BlobKey])                  REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_EvalSessionSheetXmlFiles_Type_Staging]                    CHECK ([Type] IN (1, 2))
);
GO

CREATE TABLE [dbo].[ContractReportTechnicalXmlFiles_Staging] (
    [ContractReportTechnicalXmlFileId]  INT                 NOT NULL IDENTITY,
    [ContractReportTechnicalId]         INT                 NOT NULL,
    [BlobKey]                           UNIQUEIDENTIFIER    NOT NULL,
    [Name]                              NVARCHAR(200)       NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,
    [Type]                              INT                 NOT NULL,

    CONSTRAINT [PK_ContractReportTechnicalXmlFiles_Staging]                            PRIMARY KEY ([ContractReportTechnicalXmlFileId]),
    CONSTRAINT [FK_ContractReportTechnicalXmlFiles_ContractReportTechnicals_Staging]   FOREIGN KEY ([ContractReportTechnicalId])    REFERENCES [dbo].[ContractReportTechnicals] ([ContractReportTechnicalId]),
    CONSTRAINT [FK_ContractReportTechnicalXmlFiles_Blobs_Staging]                      FOREIGN KEY ([BlobKey])                      REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ContractReportTechnicalXmlFiles_Type_Staging]                      CHECK ([Type] IN (1, 2, 3))
);
GO

CREATE TABLE [dbo].[ProjectCommunicationMessageFiles_Staging] (
    [ProjectCommunicationMessageFileId]  INT                 NOT NULL IDENTITY,
    [ProjectCommunicationId]             INT                 NOT NULL,
    [MessageType]                        INT                 NOT NULL,
    [BlobKey]                            UNIQUEIDENTIFIER    NOT NULL,
    [Name]                               NVARCHAR(200)       NOT NULL,
    [Description]                        NVARCHAR(MAX)       NULL,
    [Type]                               INT                 NOT NULL,

    CONSTRAINT [PK_ProjectCommunicationMessageFiles_Staging]                         PRIMARY KEY ([ProjectCommunicationMessageFileId]),
    CONSTRAINT [FK_ProjectCommunicationMessageFiles_ProjectCommunications_Staging]   FOREIGN KEY ([ProjectCommunicationId])    REFERENCES [dbo].[ProjectCommunications] ([ProjectCommunicationId]),
    CONSTRAINT [FK_ProjectCommunicationMessageFiles_Blobs_Staging]                   FOREIGN KEY ([BlobKey])                   REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ProjectCommunicationMessageFiles_MessageType_Staging]            CHECK ([MessageType] IN (1, 2)),
    CONSTRAINT [CHK_ProjectCommunicationMessageFiles_Type_Staging]                   CHECK ([Type] IN (1, 2, 3))
);
GO

-- Fill staging tables
WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-09992' as bc,
    N'http://ereg.egov.bg/segment/R-10018' as a,
    N'http://ereg.egov.bg/segment/R-10047' as cc,
    N'http://ereg.egov.bg/segment/R-10048' as pp,
    N'http://ereg.egov.bg/segment/R-10041' as p
),
ContractProcurementContractorFiles as
(
    SELECT
        [ContractProcurementXmlId],
        1 AS [Type], -- ContractorDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') as [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') as [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') as [Description]
    FROM [ContractProcurementXmls] p
        OUTER APPLY p.Xml.nodes('(/Procurements/p:ContractContractors/p:ContractContractor/cc:AttachedDocument)') a(s)
),
ContractProcurementAttachedFiles as
(
    SELECT
        [ContractProcurementXmlId],
        2 AS [Type], -- ProcurementDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') as [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') as [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') as [Description]
    FROM [ContractProcurementXmls] p
        OUTER APPLY p.Xml.nodes('(/Procurements/p:ProcurementPlans/p:ProcurementPlan/pp:AttachedDocument)') a(s)
),
ContractProcurementPublicAttachedFiles as
(
    SELECT
        [ContractProcurementXmlId],
        3 AS [Type], -- ProcurementPublicDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') as [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') as [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') as [Description]
    FROM [ContractProcurementXmls] p
        OUTER APPLY p.Xml.nodes('(/Procurements/p:ProcurementPlans/p:ProcurementPlan/pp:PublicAttachedDocument)') a(s)
)
INSERT INTO [dbo].[ContractProcurementXmlFiles_Staging]
    ([ContractProcurementXmlId], [Type], [BlobKey], [Name], [Description])
SELECT
    [ContractProcurementXmlId], [Type], [BlobKey], [Name], [Description]
from (
    SELECT * FROM ContractProcurementContractorFiles where BlobKey is not null
    UNION ALL
    SELECT * FROM ContractProcurementAttachedFiles where BlobKey is not null
    UNION ALL
    SELECT * FROM ContractProcurementPublicAttachedFiles where BlobKey is not null
) s
GO


WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-09992' as bc,
    N'http://ereg.egov.bg/segment/R-10018' as a,
    N'http://ereg.egov.bg/segment/R-10048' as pp,
    N'http://ereg.egov.bg/segment/R-10044' as tr
),
ContractReportTechnicalAttachedFiles as
(
    SELECT
        [ContractReportTechnicalId],
        1 AS [Type], -- AttachedDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') as [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') as [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') as [Description]
    FROM [ContractReportTechnicals] p
        OUTER APPLY p.Xml.nodes('(/TechnicalReport/tr:AttachedDocuments/tr:AttachedDocument)') a(s)
),
ContractReportTechnicalProcurementAttachedFiles as
(
    SELECT
        [ContractReportTechnicalId],
        2 AS [Type], -- ProcurementDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') as [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') as [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') as [Description]
    FROM [ContractReportTechnicals] p
        OUTER APPLY p.Xml.nodes('(/TechnicalReport/tr:ProcurementPlan/pp:AttachedDocument)') a(s)
),
ContractReportTechnicalProcurementPublicAttachedFiles as
(
    SELECT
        [ContractReportTechnicalId],
        3 AS [Type], -- ProcurementPublicDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') as [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') as [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') as [Description]
    FROM [ContractReportTechnicals] p
        OUTER APPLY p.Xml.nodes('(/TechnicalReport/tr:ProcurementPlan/pp:PublicAttachedDocument)') a(s)
)
INSERT INTO [dbo].[ContractReportTechnicalXmlFiles_Staging]
    ([ContractReportTechnicalId], [Type], [BlobKey], [Name], [Description])
SELECT
    [ContractReportTechnicalId], [Type], [BlobKey], [Name], [Description]
from (
    SELECT * FROM ContractReportTechnicalAttachedFiles where BlobKey is not null
    UNION ALL
    SELECT * FROM ContractReportTechnicalProcurementAttachedFiles where BlobKey is not null
    UNION ALL
    SELECT * FROM ContractReportTechnicalProcurementPublicAttachedFiles where BlobKey is not null
) s
GO


WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-09992' as bc,
    N'http://ereg.egov.bg/segment/R-10018' as a,
    N'http://ereg.egov.bg/segment/R-10026' as ess
),
EvalSessionSheetAttachedFiles as
(
    SELECT
        [EvalSessionSheetXmlId],
        1 AS [Type], -- AttachedDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') as [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') as [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') as [Description]
    FROM [EvalSessionSheetXmls] x
        OUTER APPLY x.Xml.nodes('(/EvalSheet/ess:AttachedDocument)') a(s)
),
EvalSessionSheetEvalTableAttachedFiles as
(
    SELECT
        [EvalSessionSheetXmlId],
        2 AS [Type], -- EvalTableAttachedDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') as [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') as [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') as [Description]
    FROM [EvalSessionSheetXmls] x
        OUTER APPLY x.Xml.nodes('(/EvalSheet/ess:EvalTableAttachedDocument)') a(s)
)
INSERT INTO [dbo].[EvalSessionSheetXmlFiles_Staging]
    ([EvalSessionSheetXmlId], [Type], [BlobKey], [Name], [Description])
SELECT
    [EvalSessionSheetXmlId], [Type], [BlobKey], [Name], [Description]
from (
    SELECT * FROM EvalSessionSheetAttachedFiles where BlobKey is not null
    UNION ALL
    SELECT * FROM EvalSessionSheetEvalTableAttachedFiles where BlobKey is not null
) s
GO


WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-09992' AS bc,
    N'http://ereg.egov.bg/segment/R-10018' AS a,
    N'http://ereg.egov.bg/segment/R-10019' AS p,
    N'http://ereg.egov.bg/segment/R-10020' AS m
),
ProjectCommunicationQuestionContentAttachedFiles AS
(
    SELECT
        pc.ProjectCommunicationId,
        1 AS [MessageType], -- Question
        1 AS [Type], -- ContentAttachedDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') AS [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') AS [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') AS [Description]
    FROM ProjectCommunications pc
        OUTER APPLY pc.QuestionXml.nodes('(/Message/m:ContentAttachedDocument)') a(s)
),
ProjectCommunicationQuestionReplyAttachedFiles AS
(
    SELECT
        pc.ProjectCommunicationId,
        1 AS [MessageType], -- Question
        2 AS [Type], -- ReplyAttachedDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') AS [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') AS [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') AS [Description]
    FROM ProjectCommunications pc
        OUTER APPLY pc.QuestionXml.nodes('(/Message/m:ReplyAttachedDocument)') a(s)
),
ProjectCommunicationQuestionProjectFiles AS
(
    SELECT
        pc.ProjectCommunicationId,
        1 AS [MessageType], -- Question
        3 AS [Type], -- ProjectDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') AS [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') AS [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') AS [Description]
    FROM ProjectCommunications pc
        OUTER APPLY pc.QuestionXml.nodes('(/Message/m:Project/p:AttachedDocuments/p:AttachedDocument)') a(s)
),
ProjectCommunicationAnswerContentAttachedFiles AS
(
    SELECT
        pc.ProjectCommunicationId,
        2 AS [MessageType], -- Answer
        1 AS [Type], -- ContentAttachedDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') AS [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') AS [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') AS [Description]
    FROM ProjectCommunications pc
        OUTER APPLY pc.AnswerXml.nodes('(/Message/m:ContentAttachedDocument)') a(s)
),
ProjectCommunicationAnswerReplyAttachedFiles AS
(
    SELECT
        pc.ProjectCommunicationId,
        2 AS [MessageType], -- Answer
        2 AS [Type], -- ReplyAttachedDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') AS [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') AS [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') AS [Description]
    FROM ProjectCommunications pc
        OUTER APPLY pc.AnswerXml.nodes('(/Message/m:ReplyAttachedDocument)') a(s)
),
ProjectCommunicationAnswerProjectFiles AS
(
    SELECT
        pc.ProjectCommunicationId,
        2 AS [MessageType], -- Answer
        3 AS [Type], -- ProjectDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') AS [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') AS [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') AS [Description]
    FROM ProjectCommunications pc
        OUTER APPLY pc.AnswerXml.nodes('(/Message/m:Project/p:AttachedDocuments/p:AttachedDocument)') a(s)
)
INSERT INTO [dbo].[ProjectCommunicationMessageFiles_Staging]
    ([ProjectCommunicationId], [MessageType], [Type], [BlobKey], [Name], [Description])
SELECT
    [ProjectCommunicationId], [MessageType], [Type], [BlobKey], [Name], [Description]
from (
    SELECT * FROM ProjectCommunicationQuestionContentAttachedFiles where BlobKey is not null
    UNION ALL
    SELECT * FROM ProjectCommunicationQuestionReplyAttachedFiles where BlobKey is not null
    UNION ALL
    SELECT * FROM ProjectCommunicationQuestionProjectFiles where BlobKey is not null
    UNION ALL
    SELECT * FROM ProjectCommunicationAnswerContentAttachedFiles where BlobKey is not null
    UNION ALL
    SELECT * FROM ProjectCommunicationAnswerReplyAttachedFiles where BlobKey is not null
    UNION ALL
    SELECT * FROM ProjectCommunicationAnswerProjectFiles where BlobKey is not null
) s
GO

-- TRUNCATE and ALTER production tables
-- SWITCH staging and production
-- DROP staging
TRUNCATE TABLE [dbo].[ContractProcurementXmlFiles]
GO
ALTER TABLE [dbo].[ContractProcurementXmlFiles] DROP CONSTRAINT [CHK_ContractProcurementXmlFiles_Type]
GO
ALTER TABLE [dbo].[ContractProcurementXmlFiles] ADD CONSTRAINT [CHK_ContractProcurementXmlFiles_Type] CHECK ([Type] IN (1, 2, 3))
GO
ALTER TABLE [dbo].[ContractProcurementXmlFiles_Staging] SWITCH TO [dbo].[ContractProcurementXmlFiles]
GO
DROP TABLE [dbo].[ContractProcurementXmlFiles_Staging]
GO
DBCC CHECKIDENT ('[dbo].[ContractProcurementXmlFiles]', RESEED)
GO

TRUNCATE TABLE [dbo].[ContractReportTechnicalXmlFiles]
GO
ALTER TABLE [dbo].[ContractReportTechnicalXmlFiles] ADD [Type] INT NOT NULL
GO
ALTER TABLE [dbo].[ContractReportTechnicalXmlFiles] ADD CONSTRAINT [CHK_ContractReportTechnicalXmlFiles_Type] CHECK ([Type] IN (1, 2, 3))
GO
ALTER TABLE [dbo].[ContractReportTechnicalXmlFiles_Staging] SWITCH TO [dbo].[ContractReportTechnicalXmlFiles]
GO
DROP TABLE [dbo].[ContractReportTechnicalXmlFiles_Staging]
GO
DBCC CHECKIDENT ('[dbo].[ContractReportTechnicalXmlFiles]', RESEED)
GO

TRUNCATE TABLE [dbo].[EvalSessionSheetXmlFiles]
GO
ALTER TABLE [dbo].[EvalSessionSheetXmlFiles] ADD [Type] INT NOT NULL
GO
ALTER TABLE [dbo].[EvalSessionSheetXmlFiles] ADD CONSTRAINT [CHK_EvalSessionSheetXmlFiles_Type] CHECK ([Type] IN (1, 2))
GO
ALTER TABLE [dbo].[EvalSessionSheetXmlFiles_Staging] SWITCH TO [dbo].[EvalSessionSheetXmlFiles]
GO
DROP TABLE [dbo].[EvalSessionSheetXmlFiles_Staging]
GO
DBCC CHECKIDENT ('[dbo].[EvalSessionSheetXmlFiles]', RESEED)
GO

TRUNCATE TABLE [dbo].[ProjectCommunicationMessageFiles]
GO
ALTER TABLE [dbo].[ProjectCommunicationMessageFiles] ADD [Type] INT NOT NULL
GO
ALTER TABLE [dbo].[ProjectCommunicationMessageFiles] ADD CONSTRAINT [CHK_ProjectCommunicationMessageFiles_Type] CHECK ([Type] IN (1, 2, 3))
GO
ALTER TABLE [dbo].[ProjectCommunicationMessageFiles_Staging] SWITCH TO [dbo].[ProjectCommunicationMessageFiles]
GO
DROP TABLE [dbo].[ProjectCommunicationMessageFiles_Staging]
GO
DBCC CHECKIDENT ('[dbo].[ProjectCommunicationMessageFiles]', RESEED)
GO
