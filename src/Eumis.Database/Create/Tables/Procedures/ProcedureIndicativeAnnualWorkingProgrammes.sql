PRINT 'ProcedureIndicativeAnnualWorkingProgrammes'
GO

CREATE TABLE [dbo].[ProcedureIndicativeAnnualWorkingProgrammes] (
    [ProcedureIndicativeAnnualWorkingProgrammeId]   INT             NOT NULL IDENTITY,
    [ProcedureId]                                   INT             NOT NULL,
    [Year]                                          INT             NOT NULL,
    [EligibleActivities]                            NVARCHAR(MAX)   NOT NULL,
    [EligibleActivitiesAlt]                         NVARCHAR(MAX)   NOT NULL,
    [EligibleCosts]                                 NVARCHAR(MAX)   NOT NULL,
    [EligibleCostsAlt]                              NVARCHAR(MAX)   NOT NULL,
    [MaxPercentCoFinancing]                         MONEY           NOT NULL,
    [MaxPercentCoFinancingInfo]                     NVARCHAR(MAX)   NOT NULL,
    [MaxPercentCoFinancingInfoAlt]                  NVARCHAR(MAX)   NOT NULL,
    [IsStateAssistance]                             INT             NOT NULL,
    [IsMinimalAssistance]                           INT             NOT NULL,
    [CreateDate]                                    DATETIME2       NOT NULL,
    [ModifyDate]                                    DATETIME2       NOT NULL,
    [Version]                                       ROWVERSION      NOT NULL,

    CONSTRAINT [PK_ProcedureIndicativeAnnualWorkingProgrammes]                          PRIMARY KEY ([ProcedureIndicativeAnnualWorkingProgrammeId]),
    CONSTRAINT [FK_ProcedureIndicativeAnnualWorkingProgrammes_Procedures]               FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_ProcedureIndicativeAnnualWorkingProgrammes_Year]                    CHECK ([Year] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)),
    CONSTRAINT [CHK_ProcedureIndicativeAnnualWorkingProgrammes_IsStateAssistance]       CHECK ([IsStateAssistance] IN (1, 2, 3)),
    CONSTRAINT [CHK_ProcedureIndicativeAnnualWorkingProgrammes_IsMinimalAssistance]     CHECK ([IsMinimalAssistance] IN (1, 2, 3)),
);
GO

exec spDescTable  N'ProcedureIndicativeAnnualWorkingProgrammes', N'Данни на процедура за индикативна годишна работна програма.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'ProcedureIndicativeAnnualWorkingProgrammeId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'ProcedureId'                                 , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'Year'                                        , N'Година.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'EligibleActivities'                          , N'Примерни допустими дейности.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'EligibleActivitiesAlt'                       , N'Примерни допустими дейности на английски език.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'EligibleCosts'                               , N'Категории допустими разходи.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'EligibleCostsAlt'                            , N'Категории допустими разходи на английски език.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'MaxPercentCoFinancing'                       , N'Максимален % на съфинансиране.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'MaxPercentCoFinancingInfo'                   , N'Допълнителни уточнения към максимален % на съфинансиране.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'MaxPercentCoFinancingInfoAlt'                , N'Допълнителни уточнения на английски език към максимален % на съфинансиране.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'IsStateAssistance'                           , N'Представлява ли процедурата/част от нея държавна помощ: 1- Да, 2 - Не, 3 - Предстои да бъде уточнено.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'IsMinimalAssistance'                         , N'Представлява ли процедурата/част от нея минимална помощ: 1- Да, 2 - Не, 3 - Предстои да бъде уточнено.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'CreateDate'                                  , N'Дата на създаване на записа.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'ModifyDate'                                  , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProcedureIndicativeAnnualWorkingProgrammes', N'Version'                                     , N'Версия.'
GO
