GO

CREATE TABLE [dbo].[ProjectSignatures] (
    [ProjectSignatureId]    INT             NOT NULL IDENTITY,
    [ProjectId]             INT             NOT NULL,
    [Signature]             VARBINARY(MAX)  NOT NULL,
    [CreateDate]            DATETIME2       NOT NULL,
    [ModifyDate]            DATETIME2       NOT NULL,
    [Version]               ROWVERSION      NOT NULL

    CONSTRAINT [PK_ProjectSignatures]           PRIMARY KEY ([ProjectSignatureId]),
    CONSTRAINT [FK_ProjectSignatures_Projects]  FOREIGN KEY ([ProjectId])   REFERENCES [dbo].[Projects] ([ProjectId])
);

GO
