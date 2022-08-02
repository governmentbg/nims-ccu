-- fda35358ad54fa84629ad08f70d3ebc3e4a18d86
GO

CREATE TABLE [dbo].[ProcedureVersions] (
    [ProcedureId]               INT                 NOT NULL,
    [ProcedureVersionId]        INT                 NOT NULL,
    [ProcedureGid]              UNIQUEIDENTIFIER    NOT NULL,
    [ProcedureText]             NVARCHAR(MAX)       NOT NULL,
    [IsActive]                  BIT                 NOT NULL,
    [CreateDate]                DATETIME2           NOT NULL,
    [ModifyDate]                DATETIME2           NOT NULL,
    [Version]                   ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ProcedureVersions]                            PRIMARY KEY ([ProcedureId], [ProcedureVersionId]),
    CONSTRAINT [FK_ProcedureVersions_Procedures]                 FOREIGN KEY ([ProcedureId])    REFERENCES [dbo].[Procedures] ([ProcedureId])
);
GO
