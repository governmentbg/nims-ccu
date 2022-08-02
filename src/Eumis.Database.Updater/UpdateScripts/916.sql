GO

ALTER TABLE [dbo].[ProgrammeDeclarations]
    ADD [IsConsentForNSIDataProviding] BIT NOT NULL CONSTRAINT [DF_ProgrammeDeclarations_IsConsentForNSIDataProviding] DEFAULT(0);
GO

ALTER TABLE [dbo].[ProgrammeDeclarations]
    DROP CONSTRAINT [DF_ProgrammeDeclarations_IsConsentForNSIDataProviding];
GO

ALTER TABLE
    [dbo].[ProjectMonitorstatRequests]
ADD
    [ProgrammeDeclarationId] INT NULL,
    CONSTRAINT [FK_ProjectMonitorstatRequests_ProgrammeDeclarations] FOREIGN KEY ([ProgrammeDeclarationId]) REFERENCES [dbo].[ProgrammeDeclarations] ([ProgrammeDeclarationId])

GO

ALTER TABLE
    [dbo].[ProjectMonitorstatRequests]
ADD
    [DeclarationBlobKey] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [FK_ProjectMonitorstatRequests_Blobs] FOREIGN KEY ([DeclarationBlobKey]) REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ProjectMonitorstatRequests_DeclarationFile] CHECK (
        [ProjectVersionXmlFileId] IS NOT NULL
        OR [DeclarationBlobKey] IS NOT NULL
    );
