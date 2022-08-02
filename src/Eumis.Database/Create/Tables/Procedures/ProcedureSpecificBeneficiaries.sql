PRINT 'ProcedureSpecificBeneficiaries'
GO

CREATE TABLE [dbo].[ProcedureSpecificBeneficiaries] (
    [ProcedureId]           INT NOT NULL,
    [CompanyId]             INT             NOT NULL ,
    CONSTRAINT [PK_ProcedureSpecificBeneficiaries]              PRIMARY KEY ([ProcedureId], [CompanyId]),
    CONSTRAINT [FK_ProcedureSpecificBeneficiaries_Procedures]   FOREIGN KEY ([ProcedureId]) REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureSpecificBeneficiaries_Companies]    FOREIGN KEY ([CompanyId])   REFERENCES [dbo].[Companies] ([CompanyId])
);
GO

exec spDescTable  N'ProcedureSpecificBeneficiaries', N'Специфични бенефициенти за процедура.'
exec spDescColumn N'ProcedureSpecificBeneficiaries', N'ProcedureId'            , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureSpecificBeneficiaries', N'CompanyId'                             , N'Идентификатор на компанията специфичен бенефициент.'

GO

