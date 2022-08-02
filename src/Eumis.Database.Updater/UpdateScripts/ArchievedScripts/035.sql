GO

ALTER TABLE [dbo].[RegProjectXmls] DROP
    COLUMN [ProcedureCode],
    COLUMN [Name],
    COLUMN [IsDraft];

ALTER TABLE [dbo].[RegProjectXmls] ADD
    [ProcedureId]       INT             NOT NULL,
    [Status]            INT             NOT NULL,
    [ProjectName]       NVARCHAR(200)   NULL,
    [RegistrationType]  INT             NULL,
    [ProjectId]         INT             NULL,
    CONSTRAINT [FK_RegProjectXmls_Procedures]           FOREIGN KEY ([ProcedureId])         REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_RegProjectXmls_Projects]             FOREIGN KEY ([ProjectId])           REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [CHK_RegProjectXmls_Status]              CHECK ([Status] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_RegProjectXmls_RegistrationType]    CHECK ([RegistrationType] IN (NULL, 1, 2));
