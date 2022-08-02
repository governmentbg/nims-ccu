PRINT 'ProcedureIndicators'
GO

CREATE TABLE [dbo].[ProcedureIndicators] (

    [ProcedureId]                    INT                NOT NULL,
    [IndicatorId]                    INT                NOT NULL,
    [BaseTotalValue]                 DECIMAL(15,3)      NULL,
    [BaseMenValue]                   DECIMAL(15,3)      NULL,
    [BaseWomenValue]                 DECIMAL(15,3)      NULL,
    [BaseYear]                       INT                NULL,
    [TargetTotalValue]               DECIMAL(15,3)      NULL,
    [TargetMenValue]                 DECIMAL(15,3)      NULL,
    [TargetWomenValue]               DECIMAL(15,3)      NULL,
    [MilestoneTargetTotalValue]      DECIMAL(15,3)      NULL,
    [MilestoneTargetMenValue]        DECIMAL(15,3)      NULL,
    [MilestoneTargetWomenValue]      DECIMAL(15,3)      NULL,
    [DataSource]                     NVARCHAR(100)      NULL,
    [Description]                    NVARCHAR(MAX)      NULL,
    [IsActivated]                    BIT                NOT NULL,
    [IsActive]                       BIT                NOT NULL,
    [SourceMapNodeId]                INT                NOT NULL,

    CONSTRAINT [PK_ProcedureIndicators]                     PRIMARY KEY ([ProcedureId], [IndicatorId]),
    CONSTRAINT [FK_ProcedureIndicators_Procedures]          FOREIGN KEY ([ProcedureId]) REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureIndicators_Indicators]          FOREIGN KEY ([IndicatorId]) REFERENCES [dbo].[Indicators] ([IndicatorId]),
    CONSTRAINT [FK_ProcedureIndicators_MapNodes]            FOREIGN KEY ([SourceMapNodeId])   REFERENCES [dbo].[MapNodes] ([MapNodeId])
);
GO

exec spDescTable  N'ProcedureIndicators', N'Индикатори за процедура.'
exec spDescColumn N'ProcedureIndicators', N'ProcedureId'               , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureIndicators', N'IndicatorId'               , N'Идентификатор на индикатор.'
exec spDescColumn N'ProcedureIndicators', N'BaseTotalValue'            , N'Базова стойност.'
exec spDescColumn N'ProcedureIndicators', N'BaseMenValue'              , N'Базова стойност - Мъже.'
exec spDescColumn N'ProcedureIndicators', N'BaseWomenValue'            , N'Базова стойност - Жени.'
exec spDescColumn N'ProcedureIndicators', N'BaseYear'                  , N'Базова година.'
exec spDescColumn N'ProcedureIndicators', N'TargetTotalValue'          , N'Целева стойност. (2023г.)'
exec spDescColumn N'ProcedureIndicators', N'TargetMenValue'            , N'Целева стойност (2023г.) - Мъже'
exec spDescColumn N'ProcedureIndicators', N'TargetWomenValue'          , N'Целева стойност (2023г.) - Жени'
exec spDescColumn N'ProcedureIndicators', N'MilestoneTargetTotalValue' , N'Етапна цел за 2018 - Общо (Дробно число)'
exec spDescColumn N'ProcedureIndicators', N'MilestoneTargetMenValue'   , N'Етапна цел за 2018 - Мъже (Дробно число)'
exec spDescColumn N'ProcedureIndicators', N'MilestoneTargetWomenValue' , N'Етапна цел за 2018 - Жени (Дробно число)'
exec spDescColumn N'ProcedureIndicators', N'DataSource'                , N'Източник на данните.'
exec spDescColumn N'ProcedureIndicators', N'Description'               , N'Обяснение за значението на индикатора.'
exec spDescColumn N'ProcedureIndicators', N'SourceMapNodeId'           , N'Източник на прикачване на индикатора.'

GO

