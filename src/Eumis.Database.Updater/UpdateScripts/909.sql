CREATE TABLE [dbo].[ProcedureApplicationSectionAdditionalSettings](
    [ProcedureId]           INT             NOT NULL,
    [FillMainData]          BIT             NOT NULL
    CONSTRAINT [PK_ProcedureApplicationSectionAdditionalSettings]               PRIMARY KEY     ([ProcedureId]),
    CONSTRAINT [FK_ProcedureApplicationSectionAdditionalSettings_Procedures]    FOREIGN KEY     ([ProcedureId])     REFERENCES [dbo].[Procedures] ([ProcedureId])
);
GO

exec spDescTable  N'ProcedureApplicationSectionAdditionalSettings', N'Типове процедура.'
exec spDescColumn N'ProcedureApplicationSectionAdditionalSettings', N'ProcedureId'      , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureApplicationSectionAdditionalSettings', N'FillMainData'     , N'Настройка, която определя дали данните от процедурата да се попълват автоматично в секция Основни данни на формуляра за кандидатстване.'
GO
