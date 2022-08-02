PRINT 'ProcedureIndicativeAnnualWorkingProgrammeCandidates'
GO

CREATE TABLE [dbo].[ProcedureIndicativeAnnualWorkingProgrammeCandidates] (
    [ProcedureIndicativeAnnualWorkingProgrammeCandidateId]  INT             NOT NULL IDENTITY,
    [ProcedureIndicativeAnnualWorkingProgrammeId]           INT             NOT NULL,
    [CompanyTypeId]                                         INT             NULL,
    [CompanyLegalTypeId]                                    INT             NULL,
    [Info]                                                  NVARCHAR(MAX)   NULL,
    [InfoAlt]                                               NVARCHAR(MAX)   NULL,

    CONSTRAINT [PK_ProcedureIndicativeAnnualWorkingProgrammeCandidates]                                              PRIMARY KEY ([ProcedureIndicativeAnnualWorkingProgrammeCandidateId]),
    CONSTRAINT [FK_ProcedureIndicativeAnnualWorkingProgrammeCandidates_ProcedureIndicativeAnnualWorkingProgrammes]   FOREIGN KEY ([ProcedureIndicativeAnnualWorkingProgrammeId]) REFERENCES [dbo].[ProcedureIndicativeAnnualWorkingProgrammes] ([ProcedureIndicativeAnnualWorkingProgrammeId]),
    CONSTRAINT [FK_ProcedureIndicativeAnnualWorkingProgrammeCandidates_CompanyTypes]                                 FOREIGN KEY ([CompanyTypeId])                               REFERENCES [dbo].[CompanyTypes] ([CompanyTypeId]),
    CONSTRAINT [FK_ProcedureIndicativeAnnualWorkingProgrammeCandidates_CompanyLegalTypes]                            FOREIGN KEY ([CompanyLegalTypeId])                          REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
);
GO

exec spDescTable  N'ProcedureIndicativeAnnualWorkingProgrammeCandidates', N'Допустими кандидати на процедура за индикативна годишна работна програма.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammeCandidates', N'ProcedureIndicativeAnnualWorkingProgrammeCandidateId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammeCandidates', N'ProcedureIndicativeAnnualWorkingProgrammeId'             , N'Идентификатор на данни за индикативна годишна работна програма..'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammeCandidates', N'CompanyTypeId'                                           , N'Идентификатор на тип органицазия.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammeCandidates', N'CompanyLegalTypeId'                                      , N'Идентификатор на вид органицазия.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammeCandidates', N'Info'                                                    , N'Допълнителни уточнения.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammeCandidates', N'InfoAlt'                                                 , N'Допълнителни уточнения на английски език.'
GO
