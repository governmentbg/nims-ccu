PRINT 'ProjectIndicators'
GO

CREATE TABLE [dbo].[ProjectIndicators] (
    [ProjectId]           INT NOT NULL,
    [IndicatorId]           INT NOT NULL,
    [Base]                  DECIMAL(15,3)   NOT NULL,
    [BaseDate]              DATETIME2       NOT NULL,
    [Medial]                DECIMAL(15,3)       NULL,
    [MedialDate]            DATETIME2           NULL,
    [Target]                DECIMAL(15,3)   NOT NULL,
    [TargetDate]            DATETIME2       NOT NULL,
    [Description]           NVARCHAR(MAX)       NULL,
    CONSTRAINT [PK_ProjectIndicators]                     PRIMARY KEY ([ProjectId], [IndicatorId]),
    CONSTRAINT [FK_ProjectIndicators_Projects]          FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_ProjectIndicators_Indicators]          FOREIGN KEY ([IndicatorId]) REFERENCES [dbo].[Indicators] ([IndicatorId])
);
GO

exec spDescTable  N'ProjectIndicators', N'Индикатори за проектно предложение.'
exec spDescColumn N'ProjectIndicators', N'ProjectId'            , N'Идентификатор на проектно предложение.'
exec spDescColumn N'ProjectIndicators', N'IndicatorId'            , N'Идентификатор на индикатор.'
exec spDescColumn N'ProjectIndicators', N'Base'                   , N'Базова стйност.'
exec spDescColumn N'ProjectIndicators', N'BaseDate'               , N'Дата на Базова стйност.'
exec spDescColumn N'ProjectIndicators', N'Medial'                 , N'Междинна стойност.'
exec spDescColumn N'ProjectIndicators', N'MedialDate'             , N'Дата на междинна стойност.'
exec spDescColumn N'ProjectIndicators', N'Target'                 , N'Целева стойност.'
exec spDescColumn N'ProjectIndicators', N'TargetDate'             , N'Дата на целева стойност.'
exec spDescColumn N'ProjectIndicators', N'Description'            , N'Описание.'

GO

