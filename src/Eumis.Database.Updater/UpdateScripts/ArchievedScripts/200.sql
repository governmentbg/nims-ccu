-- Create staging tables matching the production tables
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
    CONSTRAINT [CHK_ProjectCommunicationMessageFiles_Type_Staging]                   CHECK ([Type] IN (1, 2, 3, 4))
);
GO

CREATE TABLE [dbo].[ProjectVersionXmlFiles_Staging] (
    [ProjectVersionXmlFileId]  INT                 NOT NULL IDENTITY,
    [ProjectVersionXmlId]      INT                 NOT NULL,
    [BlobKey]                  UNIQUEIDENTIFIER    NOT NULL,
    [Name]                     NVARCHAR(200)       NOT NULL,
    [Description]              NVARCHAR(MAX)       NULL,
    [Type]                     INT                 NOT NULL,

    CONSTRAINT [PK_ProjectVersionXmlFiles_Staging]                      PRIMARY KEY ([ProjectVersionXmlFileId]),
    CONSTRAINT [FK_ProjectVersionXmlFiles_ProjectVersionXmls_Staging]   FOREIGN KEY ([ProjectVersionXmlId])    REFERENCES [dbo].[ProjectVersionXmls] ([ProjectVersionXmlId]),
    CONSTRAINT [FK_ProjectVersionXmlFiles_Blobs_Staging]                FOREIGN KEY ([BlobKey])                REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ProjectVersionXmlFiles_Type_Staging]                CHECK ([Type] IN (1, 2))
);
GO

CREATE TABLE [dbo].[RegOfferXmlFiles_Staging] (
    [RegOfferXmlFileId]    INT                 NOT NULL IDENTITY,
    [RegOfferXmlId]        INT                 NOT NULL,
    [BlobKey]              UNIQUEIDENTIFIER    NOT NULL,
    [Name]                 NVARCHAR(200)       NOT NULL,
    [Description]          NVARCHAR(MAX)       NULL,
    [Type]                 INT                 NOT NULL,

    CONSTRAINT [PK_RegOfferXmlFiles_Staging]                  PRIMARY KEY ([RegOfferXmlFileId]),
    CONSTRAINT [FK_RegOfferXmlFiles_RegOfferXmls_Staging]     FOREIGN KEY ([RegOfferXmlId])      REFERENCES [dbo].[RegOfferXmls] ([RegOfferXmlId]),
    CONSTRAINT [FK_RegOfferXmlFiles_Blobs_Staging]            FOREIGN KEY ([BlobKey])            REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_RegOfferXmlFiles_Type_Staging]            CHECK ([Type] IN (1, 2))
);
GO

CREATE TABLE [dbo].[RegProjectXmlFiles_Staging] (
    [RegProjectXmlFileId]  INT                 NOT NULL IDENTITY,
    [RegProjectXmlId]      INT                 NOT NULL,
    [BlobKey]              UNIQUEIDENTIFIER    NOT NULL,
    [Name]                 NVARCHAR(200)       NOT NULL,
    [Description]          NVARCHAR(MAX)       NULL,
    [Type]                 INT                 NOT NULL,

    CONSTRAINT [PK_RegProjectXmlFiles_Staging]                  PRIMARY KEY ([RegProjectXmlFileId]),
    CONSTRAINT [FK_RegProjectXmlFiles_RegProjectXmls_Staging]   FOREIGN KEY ([RegProjectXmlId])    REFERENCES [dbo].[RegProjectXmls] ([RegProjectXmlId]),
    CONSTRAINT [FK_RegProjectXmlFiles_Blobs_Staging]            FOREIGN KEY ([BlobKey])            REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_RegProjectXmlFiles_Type_Staging]            CHECK ([Type] IN (1, 2))
);
GO


-- Fill staging tables
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
ProjectCommunicationQuestionProjectSignatureFiles AS
(
    SELECT
        pc.ProjectCommunicationId,
        1 AS [MessageType], -- Question
        4 AS [Type], -- ProjectSignature
        s.value('(bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') AS [BlobKey],
        s.value('(bc:FileName/text())[1]', 'NVARCHAR(MAX)') AS [Name],
        NULL AS [Description]
    FROM ProjectCommunications pc
        OUTER APPLY pc.QuestionXml.nodes('(/Message/m:Project/p:AttachedDocuments/p:AttachedDocument/a:SignatureContent)') a(s)
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
),
ProjectCommunicationAnswerProjectSignatureFiles AS
(
    SELECT
        pc.ProjectCommunicationId,
        2 AS [MessageType], -- Answer
        4 AS [Type], -- ProjectSignature
        s.value('(bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') AS [BlobKey],
        s.value('(bc:FileName/text())[1]', 'NVARCHAR(MAX)') AS [Name],
        NULL AS [Description]
    FROM ProjectCommunications pc
        OUTER APPLY pc.AnswerXml.nodes('(/Message/m:Project/p:AttachedDocuments/p:AttachedDocument/a:SignatureContent)') a(s)
)
INSERT INTO [dbo].[ProjectCommunicationMessageFiles_Staging]
    ([ProjectCommunicationId], [MessageType], [Type], [BlobKey], [Name], [Description])
SELECT
    [ProjectCommunicationId], [MessageType], [Type], [BlobKey], [Name], [Description]
FROM (
    SELECT * FROM ProjectCommunicationQuestionContentAttachedFiles WHERE BlobKey is not null
    UNION ALL
    SELECT * FROM ProjectCommunicationQuestionReplyAttachedFiles WHERE BlobKey is not null
    UNION ALL
    SELECT * FROM ProjectCommunicationQuestionProjectFiles WHERE BlobKey is not null
    UNION ALL
    SELECT * FROM ProjectCommunicationQuestionProjectSignatureFiles WHERE BlobKey is not null
    UNION ALL
    SELECT * FROM ProjectCommunicationAnswerContentAttachedFiles WHERE BlobKey is not null
    UNION ALL
    SELECT * FROM ProjectCommunicationAnswerReplyAttachedFiles WHERE BlobKey is not null
    UNION ALL
    SELECT * FROM ProjectCommunicationAnswerProjectFiles WHERE BlobKey is not null
    UNION ALL
    SELECT * FROM ProjectCommunicationAnswerProjectSignatureFiles WHERE BlobKey is not null
) s
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-09992' as bc,
    N'http://ereg.egov.bg/segment/R-10018' as a,
    N'http://ereg.egov.bg/segment/R-10019' as p
),
ProjectVersionXmlAttachedFiles as
(
    SELECT
        [ProjectVersionXmlId],
        1 AS [Type], -- AttachedDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') AS [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') AS [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') AS [Description]
    FROM [ProjectVersionXmls] p
        OUTER APPLY p.Xml.nodes('(/Project/p:AttachedDocuments/p:AttachedDocument)') a(s)
),
ProjectVersionXmlAttachedSignatureFiles as
(
    SELECT
        [ProjectVersionXmlId],
        2 AS [Type], -- Signature
        s.value('(bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') as [BlobKey],
        s.value('(bc:FileName/text())[1]', 'NVARCHAR(MAX)') as [Name],
        NULL AS [Description]
    FROM [ProjectVersionXmls] p
        OUTER APPLY p.Xml.nodes('(/Project/p:AttachedDocuments/p:AttachedDocument/a:SignatureContent)') a(s)
)
INSERT INTO [dbo].[ProjectVersionXmlFiles_Staging]
    ([ProjectVersionXmlId], [Type], [BlobKey], [Name], [Description])
SELECT
    [ProjectVersionXmlId], [Type], [BlobKey], [Name], [Description]
FROM (
    SELECT * FROM ProjectVersionXmlAttachedFiles WHERE BlobKey is not null
    UNION ALL
    SELECT * FROM ProjectVersionXmlAttachedSignatureFiles WHERE BlobKey is not null
) s
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-09992' as bc,
    N'http://ereg.egov.bg/segment/R-10018' as a,
    N'http://ereg.egov.bg/segment/R-10080' as o
),
RegOfferXmlAttachedFiles as
(
    SELECT
        [RegOfferXmlId],
        1 AS [Type], -- AttachedDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') AS [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') AS [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') AS [Description]
    FROM [RegOfferXmls] p
        OUTER APPLY p.Xml.nodes('(/Offer/o:AttachedDocuments/o:AttachedDocument)') a(s)
),
RegOfferXmlAttachedSignatureFiles as
(
    SELECT
        [RegOfferXmlId],
        2 AS [Type], -- Signature
        s.value('(bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') as [BlobKey],
        s.value('(bc:FileName/text())[1]', 'NVARCHAR(MAX)') as [Name],
        NULL AS [Description]
    FROM [RegOfferXmls] p
        OUTER APPLY p.Xml.nodes('(/Offer/o:AttachedDocuments/o:AttachedDocument/a:SignatureContent)') a(s)
)
INSERT INTO [dbo].[RegOfferXmlFiles_Staging]
    ([RegOfferXmlId], [Type], [BlobKey], [Name], [Description])
SELECT
    [RegOfferXmlId], [Type], [BlobKey], [Name], [Description]
FROM (
    SELECT * FROM RegOfferXmlAttachedFiles WHERE BlobKey is not null
    UNION ALL
    SELECT * FROM RegOfferXmlAttachedSignatureFiles WHERE BlobKey is not null
) s
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-09992' as bc,
    N'http://ereg.egov.bg/segment/R-10018' as a,
    N'http://ereg.egov.bg/segment/R-10019' as p
),
RegProjectXmlAttachedFiles as
(
    SELECT
        [RegProjectXmlId],
        1 AS [Type], -- AttachedDoc
        s.value('(a:AttachedDocumentContent/bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') AS [BlobKey],
        s.value('(a:AttachedDocumentContent/bc:FileName/text())[1]', 'NVARCHAR(MAX)') AS [Name],
        s.value('(a:Description/text())[1]', 'NVARCHAR(MAX)') AS [Description]
    FROM [RegProjectXmls] p
        OUTER APPLY p.Xml.nodes('(/Project/p:AttachedDocuments/p:AttachedDocument)') a(s)
),
RegProjectXmlAttachedSignatureFiles as
(
    SELECT
        [RegProjectXmlId],
        2 AS [Type], -- Signature
        s.value('(bc:BlobContentId/text())[1]', 'UNIQUEIDENTIFIER') as [BlobKey],
        s.value('(bc:FileName/text())[1]', 'NVARCHAR(MAX)') as [Name],
        NULL AS [Description]
    FROM [RegProjectXmls] p
        OUTER APPLY p.Xml.nodes('(/Project/p:AttachedDocuments/p:AttachedDocument/a:SignatureContent)') a(s)
)
INSERT INTO [dbo].[RegProjectXmlFiles_Staging]
    ([RegProjectXmlId], [Type], [BlobKey], [Name], [Description])
SELECT
    [RegProjectXmlId], [Type], [BlobKey], [Name], [Description]
FROM (
    SELECT * FROM RegProjectXmlAttachedFiles WHERE BlobKey is not null
    UNION ALL
    SELECT * FROM RegProjectXmlAttachedSignatureFiles WHERE BlobKey is not null
) s
GO

-- TRUNCATE and ALTER production tables
-- SWITCH staging and production
-- DROP staging
TRUNCATE TABLE [dbo].[ProjectCommunicationMessageFiles]
GO
ALTER TABLE [dbo].[ProjectCommunicationMessageFiles] DROP CONSTRAINT [CHK_ProjectCommunicationMessageFiles_Type]
GO
ALTER TABLE [dbo].[ProjectCommunicationMessageFiles] ADD CONSTRAINT [CHK_ProjectCommunicationMessageFiles_Type] CHECK ([Type] IN (1, 2, 3, 4))
GO
ALTER TABLE [dbo].[ProjectCommunicationMessageFiles_Staging] SWITCH TO [dbo].[ProjectCommunicationMessageFiles]
GO
DROP TABLE [dbo].[ProjectCommunicationMessageFiles_Staging]
GO
DBCC CHECKIDENT ('[dbo].[ProjectCommunicationMessageFiles]', RESEED)
GO

TRUNCATE TABLE [dbo].[ProjectVersionXmlFiles]
GO
ALTER TABLE [dbo].[ProjectVersionXmlFiles] ADD [Type] INT NOT NULL
GO
ALTER TABLE [dbo].[ProjectVersionXmlFiles] ADD CONSTRAINT [CHK_ProjectVersionXmlFiles_Type] CHECK ([Type] IN (1, 2))
GO
ALTER TABLE [dbo].[ProjectVersionXmlFiles_Staging] SWITCH TO [dbo].[ProjectVersionXmlFiles]
GO
DROP TABLE [dbo].[ProjectVersionXmlFiles_Staging]
GO
DBCC CHECKIDENT ('[dbo].[ProjectVersionXmlFiles]', RESEED)
GO

TRUNCATE TABLE [dbo].[RegOfferXmlFiles]
GO
ALTER TABLE [dbo].[RegOfferXmlFiles] ADD [Type] INT NOT NULL
GO
ALTER TABLE [dbo].[RegOfferXmlFiles] ADD CONSTRAINT [CHK_RegOfferXmlFiles_Type] CHECK ([Type] IN (1, 2))
GO
ALTER TABLE [dbo].[RegOfferXmlFiles_Staging] SWITCH TO [dbo].[RegOfferXmlFiles]
GO
DROP TABLE [dbo].[RegOfferXmlFiles_Staging]
GO
DBCC CHECKIDENT ('[dbo].[RegOfferXmlFiles]', RESEED)
GO

TRUNCATE TABLE [dbo].[RegProjectXmlFiles]
GO
ALTER TABLE [dbo].[RegProjectXmlFiles] ADD [Type] INT NOT NULL
GO
ALTER TABLE [dbo].[RegProjectXmlFiles] ADD CONSTRAINT [CHK_RegProjectXmlFiles_Type] CHECK ([Type] IN (1, 2))
GO
ALTER TABLE [dbo].[RegProjectXmlFiles_Staging] SWITCH TO [dbo].[RegProjectXmlFiles]
GO
DROP TABLE [dbo].[RegProjectXmlFiles_Staging]
GO
DBCC CHECKIDENT ('[dbo].[RegProjectXmlFiles]', RESEED)
GO
