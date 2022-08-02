GO

--ProcedureEvalTableXmlFiles
CREATE TABLE [dbo].[ProcedureEvalTableXmlFiles] (
    [ProcedureEvalTableXmlFileId]       INT                 NOT NULL IDENTITY,
    [ProcedureEvalTableXmlId]           INT                 NOT NULL,
    [BlobKey]                           UNIQUEIDENTIFIER    NOT NULL,
    [Name]                              NVARCHAR(200)       NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ProcedureEvalTableXmlFiles]                          PRIMARY KEY ([ProcedureEvalTableXmlFileId]),
    CONSTRAINT [FK_ProcedureEvalTableXmlFiles_ProcedureEvalTableXmls]   FOREIGN KEY ([ProcedureEvalTableXmlId])    REFERENCES [dbo].[ProcedureEvalTableXmls] ([ProcedureEvalTableXmlId])
);
GO

INSERT INTO [dbo].[ProcedureEvalTableXmlFiles]
([ProcedureEvalTableXmlId], [BlobKey], [Name], [Description])
SELECT * FROM
    (SELECT [ProcedureEvalTableXmlId],
            pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace key="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/key:BlobContentId/text())[1]', 'uniqueidentifier') as gid,
            pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace name="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/name:FileName/text())[1]', 'nvarchar(200)') as name,
            pref.value(
                'declare namespace descr="http://ereg.egov.bg/segment/R-10018";
                 (descr:Description/text())[1]', 'nvarchar(max)') as descr
    FROM [dbo].[ProcedureEvalTableXmls] CROSS APPLY
          [Xml].nodes('
           declare namespace doc="http://ereg.egov.bg/segment/R-10023";
          //EvalTable/doc:AttachedDocument') AS Docs(pref)) s
WHERE gid IS NOT NULL
GO

--EvalSessionSheetXmlFiles
CREATE TABLE [dbo].[EvalSessionSheetXmlFiles] (
    [EvalSessionSheetXmlFileId]         INT                 NOT NULL IDENTITY,
    [EvalSessionSheetXmlId]             INT                 NOT NULL,
    [BlobKey]                           UNIQUEIDENTIFIER    NOT NULL,
    [Name]                              NVARCHAR(200)       NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_EvalSessionSheetXmlFiles]                          PRIMARY KEY ([EvalSessionSheetXmlFileId]),
    CONSTRAINT [FK_EvalSessionSheetXmlFiles_EvalSessionSheetXmls]     FOREIGN KEY ([EvalSessionSheetXmlId])    REFERENCES [dbo].[EvalSessionSheetXmls] ([EvalSessionSheetXmlId])
);
GO

INSERT INTO [dbo].[EvalSessionSheetXmlFiles]
([EvalSessionSheetXmlId], [BlobKey], [Name], [Description])
SELECT * FROM
    (SELECT [EvalSessionSheetXmlId],
            pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace key="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/key:BlobContentId/text())[1]', 'uniqueidentifier') as gid,
            pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace name="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/name:FileName/text())[1]', 'nvarchar(200)') as name,
            pref.value(
                'declare namespace descr="http://ereg.egov.bg/segment/R-10018";
                 (descr:Description/text())[1]', 'nvarchar(max)') as descr
    FROM [dbo].[EvalSessionSheetXmls] CROSS APPLY
          [Xml].nodes(
              'declare namespace doc="http://ereg.egov.bg/segment/R-10026";
              //EvalSheet/doc:AttachedDocument') AS Docs(pref)) s
WHERE gid IS NOT NULL
GO

--EvalSessionStandpointXmlFiles
CREATE TABLE [dbo].[EvalSessionStandpointXmlFiles] (
    [EvalSessionStandpointXmlFileId]    INT                 NOT NULL IDENTITY,
    [EvalSessionStandpointXmlId]        INT                 NOT NULL,
    [BlobKey]                           UNIQUEIDENTIFIER    NOT NULL,
    [Name]                              NVARCHAR(200)       NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_EvalSessionStandpointXmlFiles]                            PRIMARY KEY ([EvalSessionStandpointXmlFileId]),
    CONSTRAINT [FK_EvalSessionStandpointXmlFiles_EvalSessionStandpointXmls]  FOREIGN KEY ([EvalSessionStandpointXmlId])    REFERENCES [dbo].[EvalSessionStandpointXmls] ([EvalSessionStandpointXmlId])
);
GO

INSERT INTO [dbo].[EvalSessionStandpointXmlFiles]
    ([EvalSessionStandpointXmlId], [BlobKey], [Name], [Description])
SELECT * FROM
    (SELECT [EvalSessionStandpointXmlId],
            pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace key="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/key:BlobContentId/text())[1]', 'uniqueidentifier') as gid,
            pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace name="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/name:FileName/text())[1]', 'nvarchar(200)') as name,
            pref.value(
                'declare namespace descr="http://ereg.egov.bg/segment/R-10018";
                 (descr:Description/text())[1]', 'nvarchar(max)') as descr
    FROM [dbo].[EvalSessionStandpointXmls] CROSS APPLY
          [Xml].nodes(
              'declare namespace doc="http://ereg.egov.bg/segment/R-10027";
              //Standpoint/doc:AttachedDocument') AS Docs(pref)) s
WHERE gid IS NOT NULL
GO

--ProjectCommunicationMessageFiles
CREATE TABLE [dbo].[ProjectCommunicationMessageFiles] (
    [ProjectCommunicationMessageFileId]  INT                 NOT NULL IDENTITY,
    [ProjectCommunicationId]             INT                 NOT NULL,
    [MessageType]                        INT                 NOT NULL,
    [BlobKey]                            UNIQUEIDENTIFIER    NOT NULL,
    [Name]                               NVARCHAR(200)       NOT NULL,
    [Description]                        NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ProjectCommunicationMessageFiles]                         PRIMARY KEY ([ProjectCommunicationMessageFileId]),
    CONSTRAINT [FK_ProjectCommunicationMessageFiles_ProjectCommunications]   FOREIGN KEY ([ProjectCommunicationId])    REFERENCES [dbo].[ProjectCommunications] ([ProjectCommunicationId]),
    CONSTRAINT [CHK_ProjectCommunicationMessageFiles_MessageType]            CHECK ([MessageType] IN (1, 2))
);
GO

INSERT INTO [dbo].[ProjectCommunicationMessageFiles]
    ([ProjectCommunicationId],[MessageType], [BlobKey], [Name], [Description])
SELECT * FROM
    (SELECT [ProjectCommunicationId],
           1 as MessageType,
           pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace key="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/key:BlobContentId/text())[1]', 'uniqueidentifier') as gid,
           pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace name="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/name:FileName/text())[1]', 'nvarchar(200)') as name,
           pref.value(
                'declare namespace descr="http://ereg.egov.bg/segment/R-10018";
                 (descr:Description/text())[1]', 'nvarchar(max)') as descr
    FROM [dbo].[ProjectCommunications] CROSS APPLY
          [QuestionXml].nodes(
              'declare namespace doc="http://ereg.egov.bg/segment/R-10020";
              //Message/doc:ContentAttachedDocument') AS Docs(pref)) s
WHERE gid IS NOT NULL
GO

INSERT INTO [dbo].[ProjectCommunicationMessageFiles]
    ([ProjectCommunicationId],[MessageType], [BlobKey], [Name], [Description])
SELECT * FROM
    (SELECT [ProjectCommunicationId],
           2 as MessageType,
           pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace key="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/key:BlobContentId/text())[1]', 'uniqueidentifier') as gid,
           pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace name="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/name:FileName/text())[1]', 'nvarchar(200)') as name,
           pref.value(
                'declare namespace descr="http://ereg.egov.bg/segment/R-10018";
                 (descr:Description/text())[1]', 'nvarchar(max)') as descr
    FROM [dbo].[ProjectCommunications] CROSS APPLY
          [AnswerXml].nodes(
              'declare namespace doc="http://ereg.egov.bg/segment/R-10020";
              //Message/doc:ReplyAttachedDocument') AS Docs(pref)) s
WHERE gid IS NOT NULL
GO

--ProjectVersionXmlFiles
CREATE TABLE [dbo].[ProjectVersionXmlFiles] (
    [ProjectVersionXmlFileId]  INT                 NOT NULL IDENTITY,
    [ProjectVersionXmlId]      INT                 NOT NULL,
    [BlobKey]                  UNIQUEIDENTIFIER    NOT NULL,
    [Name]                     NVARCHAR(200)       NOT NULL,
    [Description]              NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ProjectVersionXmlFiles]                      PRIMARY KEY ([ProjectVersionXmlFileId]),
    CONSTRAINT [FK_ProjectVersionXmlFiles_ProjectVersionXmls]   FOREIGN KEY ([ProjectVersionXmlId])    REFERENCES [dbo].[ProjectVersionXmls] ([ProjectVersionXmlId])
);
GO

INSERT INTO [dbo].[ProjectVersionXmlFiles]
    ([ProjectVersionXmlId], [BlobKey], [Name], [Description])
SELECT * FROM
    (SELECT [ProjectVersionXmlId],
            pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace key="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/key:BlobContentId/text())[1]', 'uniqueidentifier') as gid,
            pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace name="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/name:FileName/text())[1]', 'nvarchar(200)') as name,
            pref.value(
                'declare namespace descr="http://ereg.egov.bg/segment/R-10018";
                 (descr:Description/text())[1]', 'nvarchar(max)') as descr
    FROM [dbo].[ProjectVersionXmls] CROSS APPLY
          [Xml].nodes('
           declare namespace docs="http://ereg.egov.bg/segment/R-10019";
          //Project/docs:AttachedDocuments/docs:AttachedDocument') AS Docs(pref)) s
WHERE gid IS NOT NULL
GO

--RegProjectXmlFiles
CREATE TABLE [dbo].[RegProjectXmlFiles] (
    [RegProjectXmlFileId]  INT                 NOT NULL IDENTITY,
    [RegProjectXmlId]      INT                 NOT NULL,
    [BlobKey]              UNIQUEIDENTIFIER    NOT NULL,
    [Name]                 NVARCHAR(200)       NOT NULL,
    [Description]          NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_RegProjectXmlFiles]                  PRIMARY KEY ([RegProjectXmlFileId]),
    CONSTRAINT [FK_RegProjectXmlFiles_RegProjectXmls]   FOREIGN KEY ([RegProjectXmlId])    REFERENCES [dbo].[RegProjectXmls] ([RegProjectXmlId])
);
GO

INSERT INTO [dbo].[RegProjectXmlFiles]
    ([RegProjectXmlId], [BlobKey], [Name], [Description])
SELECT * FROM
    (SELECT [RegProjectXmlId],
            pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace key="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/key:BlobContentId/text())[1]', 'uniqueidentifier') as gid,
            pref.value(
                'declare namespace content="http://ereg.egov.bg/segment/R-10018";
                 declare namespace name="http://ereg.egov.bg/segment/R-09992";
                 (content:AttachedDocumentContent/name:FileName/text())[1]', 'nvarchar(200)') as name,
            pref.value(
                'declare namespace descr="http://ereg.egov.bg/segment/R-10018";
                 (descr:Description/text())[1]', 'nvarchar(max)') as descr
    FROM [dbo].[RegProjectXmls] CROSS APPLY
          [Xml].nodes('
           declare namespace docs="http://ereg.egov.bg/segment/R-10019";
          //Project/docs:AttachedDocuments/docs:AttachedDocument') AS Docs(pref)) s
WHERE gid IS NOT NULL
GO
