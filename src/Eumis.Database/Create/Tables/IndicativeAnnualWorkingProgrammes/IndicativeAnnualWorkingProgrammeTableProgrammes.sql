PRINT 'IndicativeAnnualWorkingProgrammeTableProgrammes'
GO

CREATE TABLE [dbo].[IndicativeAnnualWorkingProgrammeTableProgrammes] (
    [IndicativeAnnualWorkingProgrammeTableProgrammeId]  INT             NOT NULL IDENTITY,
    [IndicativeAnnualWorkingProgrammeTableId]           INT             NOT NULL,
    [ProgrammeId]                                       INT             NOT NULL,

    CONSTRAINT [PK_IndicativeAnnualWorkingProgrammeTableProgrammes]                                         PRIMARY KEY ([IndicativeAnnualWorkingProgrammeTableProgrammeId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTableProgrammes_IndicativeAnnualWorkingProgrammeTables]  FOREIGN KEY ([IndicativeAnnualWorkingProgrammeTableId]) REFERENCES [dbo].[IndicativeAnnualWorkingProgrammeTables] ([IndicativeAnnualWorkingProgrammeTableId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTableProgrammes_Programmes]                              FOREIGN KEY ([ProgrammeId])                             REFERENCES [dbo].[MapNodes] ([MapNodeId]),
);
GO

exec spDescTable  N'IndicativeAnnualWorkingProgrammeTableProgrammes', N'Програми, по които се предоставя БФП по процедура към таблица за ИГРП.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTableProgrammes', N'IndicativeAnnualWorkingProgrammeTableProgrammeId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTableProgrammes', N'IndicativeAnnualWorkingProgrammeTableId'            , N'Идентификатор на таблица за ИГРП'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTableProgrammes', N'ProgrammeId'                                        , N'Идентификатор на оперативна програма'
GO
