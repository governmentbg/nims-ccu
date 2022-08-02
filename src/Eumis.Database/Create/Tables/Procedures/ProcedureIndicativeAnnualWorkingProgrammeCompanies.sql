PRINT 'ProcedureIndicativeAnnualWorkingProgrammeCompanies'
GO

CREATE TABLE [dbo].[ProcedureIndicativeAnnualWorkingProgrammeCompanies] (
    [ProcedureIndicativeAnnualWorkingProgrammeCompanyId]    INT             NOT NULL IDENTITY,
    [ProcedureIndicativeAnnualWorkingProgrammeId]           INT             NOT NULL,
    [CompanyId]                                             INT             NOT NULL,

    CONSTRAINT [PK_ProcedureIndicativeAnnualWorkingProgrammeCompanies]                                              PRIMARY KEY ([ProcedureIndicativeAnnualWorkingProgrammeCompanyId]),
    CONSTRAINT [FK_ProcedureIndicativeAnnualWorkingProgrammeCompanies_ProcedureIndicativeAnnualWorkingProgrammes]   FOREIGN KEY ([ProcedureIndicativeAnnualWorkingProgrammeId]) REFERENCES [dbo].[ProcedureIndicativeAnnualWorkingProgrammes] ([ProcedureIndicativeAnnualWorkingProgrammeId]),
    CONSTRAINT [FK_ProcedureIndicativeAnnualWorkingProgrammeCompanies_Companies]                                    FOREIGN KEY ([CompanyId])                                   REFERENCES [dbo].[Companies] ([CompanyId])
);
GO

exec spDescTable  N'ProcedureIndicativeAnnualWorkingProgrammeCompanies', N'Директни бенефициенти на процедура за индикативна годишна работна програма.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammeCompanies', N'ProcedureIndicativeAnnualWorkingProgrammeCompanyId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammeCompanies', N'ProcedureIndicativeAnnualWorkingProgrammeId'             , N'Идентификатор на данни за индикативна годишна работна програма..'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammeCompanies', N'CompanyId'                                               , N'Идентификатор на организация.'
GO
