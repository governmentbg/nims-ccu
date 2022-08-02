PRINT 'ProjectIndividualIndicators'
GO

CREATE TABLE [dbo].[ProjectIndividualIndicators] (
    [ProjectIndividualIndicatorId]      INT                 NOT NULL IDENTITY,
    [ProjectId]                         INT                 NOT NULL,
    [Gid]                               UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [MeasureId]                         INT                 NOT NULL,
    [Name]                              NVARCHAR(MAX)       NOT NULL,
    [Type]                              INT                 NOT NULL,
    [Trend]                             INT                 NOT NULL,
    [Mode]                              INT                 NOT NULL,
    [MaleFemale]                        INT                 NOT NULL,
    [Base]                              DECIMAL(15,3)       NOT NULL,
    [BaseDate]                          DATETIME2           NOT NULL,
    [Medial]                            DECIMAL(15,3)       NULL,
    [MedialDate]                        DATETIME2           NULL,
    [Target]                            DECIMAL(15,3)       NOT NULL,
    [TargetDate]                        DATETIME2           NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,
    CONSTRAINT [PK_ProjectIndividualIndicators]                     PRIMARY KEY ([ProjectIndividualIndicatorId]),
    CONSTRAINT [FK_ProjectIndividualIndicators_Projects]            FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([ProjectId])
);
GO

exec spDescTable  N'ProjectIndividualIndicators', N'Индикатори за проектно предложение.'
exec spDescColumn N'ProjectIndividualIndicators', N'ProjectIndividualIndicatorId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectIndividualIndicators', N'ProjectId'                      , N'Идентификатор на проектно предложение.'
exec spDescColumn N'ProjectIndividualIndicators', N'Gid'                            , N'Глобален уникален идентификатор.'
exec spDescColumn N'ProjectIndividualIndicators', N'MeasureId'                      , N'Идентификатор на мерна единица.'
exec spDescColumn N'ProjectIndividualIndicators', N'Name'                           , N'Наименование.'
exec spDescColumn N'ProjectIndividualIndicators', N'Type'                           , N'Тип : 1 - резултат, 2 - изпълнение, 3 - въздействие.'
exec spDescColumn N'ProjectIndividualIndicators', N'Trend'                          , N'Тенденция : 0 - намаление, 1 - увеличение.'
exec spDescColumn N'ProjectIndividualIndicators', N'Mode'                           , N'Вид : 1 - Основен/Общ индикатор, 2 - Индивидуален индикатор.'
exec spDescColumn N'ProjectIndividualIndicators', N'MaleFemale'                     , N'Мъже/Жени : 0 - Неприложимо, 1 - Мъже, 2 - Жени.'
exec spDescColumn N'ProjectIndividualIndicators', N'Base'                           , N'Базова стйност.'
exec spDescColumn N'ProjectIndividualIndicators', N'BaseDate'                       , N'Дата на Базова стйност.'
exec spDescColumn N'ProjectIndividualIndicators', N'Medial'                         , N'Междинна стойност.'
exec spDescColumn N'ProjectIndividualIndicators', N'MedialDate'                     , N'Дата на междинна стойност.'
exec spDescColumn N'ProjectIndividualIndicators', N'Target'                         , N'Целева стойност.'
exec spDescColumn N'ProjectIndividualIndicators', N'TargetDate'                     , N'Дата на целева стойност.'
exec spDescColumn N'ProjectIndividualIndicators', N'Description'                    , N'Описание.'

GO

