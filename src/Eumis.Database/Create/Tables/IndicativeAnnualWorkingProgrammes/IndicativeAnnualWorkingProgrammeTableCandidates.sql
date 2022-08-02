PRINT 'IndicativeAnnualWorkingProgrammeTableCandidates'
GO

CREATE TABLE [dbo].[IndicativeAnnualWorkingProgrammeTableCandidates] (
    [IndicativeAnnualWorkingProgrammeTableCandidateId]      INT             NOT NULL IDENTITY,
    [IndicativeAnnualWorkingProgrammeTableId]               INT             NOT NULL,
    [CompanyTypeId]                                         INT             NULL,
    [CompanyLegalTypeId]                                    INT             NULL,
    [Info]                                                  NVARCHAR(MAX)   NULL,
    [InfoAlt]                                               NVARCHAR(MAX)   NULL,

    CONSTRAINT [PK_IndicativeAnnualWorkingProgrammeTableCandidates]                                             PRIMARY KEY ([IndicativeAnnualWorkingProgrammeTableCandidateId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTableCandidates_IndicativeAnnualWorkingProgrammeTables]      FOREIGN KEY ([IndicativeAnnualWorkingProgrammeTableId]) REFERENCES [dbo].[IndicativeAnnualWorkingProgrammeTables] ([IndicativeAnnualWorkingProgrammeTableId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTableCandidates_CompanyTypes]                                FOREIGN KEY ([CompanyTypeId])                           REFERENCES [dbo].[CompanyTypes] ([CompanyTypeId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTableCandidates_CompanyLegalTypes]                           FOREIGN KEY ([CompanyLegalTypeId])                      REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
);
GO

exec spDescTable  N'IndicativeAnnualWorkingProgrammeTableCandidates', N'Допустими кандидати на процедура за индикативна годишна работна програма.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTableCandidates', N'IndicativeAnnualWorkingProgrammeTableCandidateId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTableCandidates', N'IndicativeAnnualWorkingProgrammeTableId'            , N'Идентификатор на таблица за ИГРП.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTableCandidates', N'CompanyTypeId'                                      , N'Идентификатор на тип органицазия.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTableCandidates', N'CompanyLegalTypeId'                                 , N'Идентификатор на вид органицазия.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTableCandidates', N'Info'                                               , N'Допълнителни уточнения.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTableCandidates', N'InfoAlt'                                            , N'Допълнителни уточнения на английски език.'
GO
