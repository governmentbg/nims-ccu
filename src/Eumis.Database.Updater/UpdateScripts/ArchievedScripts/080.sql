GO

ALTER TABLE [dbo].[RegProjectXmlFiles]
ADD CONSTRAINT [FK_RegProjectXmlFiles_Blobs]                FOREIGN KEY ([BlobKey])            REFERENCES [dbo].[Blobs] ([Key]);
GO

ALTER TABLE [dbo].[ProjectVersionXmlFiles]
ADD CONSTRAINT [FK_ProjectVersionXmlFiles_Blobs]            FOREIGN KEY ([BlobKey])            REFERENCES [dbo].[Blobs] ([Key]);
GO

ALTER TABLE [dbo].[ProjectCommunicationMessageFiles]
ADD CONSTRAINT [FK_ProjectCommunicationMessageFiles_Blobs]  FOREIGN KEY ([BlobKey])            REFERENCES [dbo].[Blobs] ([Key]);
GO

ALTER TABLE [dbo].[ProcedureEvalTableXmlFiles]
ADD CONSTRAINT [FK_ProcedureEvalTableXmlFiles_Blobs]        FOREIGN KEY ([BlobKey])            REFERENCES [dbo].[Blobs] ([Key]);
GO

ALTER TABLE [dbo].[EvalSessionStandpointXmlFiles]
ADD CONSTRAINT [FK_EvalSessionStandpointXmlFiles_Blobs]     FOREIGN KEY ([BlobKey])            REFERENCES [dbo].[Blobs] ([Key]);
GO

ALTER TABLE [dbo].[EvalSessionSheetXmlFiles]
ADD CONSTRAINT [FK_EvalSessionSheetXmlFiles_Blobs]          FOREIGN KEY ([BlobKey])            REFERENCES [dbo].[Blobs] ([Key]);
GO
