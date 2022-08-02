PRINT 'IndicativeAnnualWorkingProgrammeTableTimeLimits'
GO

CREATE TABLE [dbo].[IndicativeAnnualWorkingProgrammeTableTimeLimits] (
    [IndicativeAnnualWorkingProgrammeTableTimeLimitId]  INT         NOT NULL IDENTITY,
    [IndicativeAnnualWorkingProgrammeTableId]           INT         NOT NULL,
    [EndDate]                                           DATETIME2   NOT NULL,
    CONSTRAINT [PK_IndicativeAnnualWorkingProgrammeTableTimeLimits] PRIMARY KEY ([IndicativeAnnualWorkingProgrammeTableTimeLimitId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTableTimeLimits_IndicativeAnnualWorkingProgrammeTables]  FOREIGN KEY ([IndicativeAnnualWorkingProgrammeTableId])  REFERENCES [dbo].[IndicativeAnnualWorkingProgrammeTables] ([IndicativeAnnualWorkingProgrammeTableId]),
);
GO

exec spDescTable  N'IndicativeAnnualWorkingProgrammeTableTimeLimits', N'Срокове за подаване на предложения по процедура към таблица за ИГРП.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTableTimeLimits', N'IndicativeAnnualWorkingProgrammeTableTimeLimitId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTableTimeLimits', N'IndicativeAnnualWorkingProgrammeTableId'            , N'Идентификатор на таблица за ИГРП.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTableTimeLimits', N'EndDate'                                            , N'Дата на изтичане на срока.'
GO
