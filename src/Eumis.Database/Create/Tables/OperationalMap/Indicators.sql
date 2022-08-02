PRINT 'Indicators'
GO

CREATE TABLE [dbo].[Indicators] (
    [IndicatorId]           INT                 NOT NULL IDENTITY,
    [ProgrammeId]           INT                 NOT NULL,
    [MeasureId]             INT                 NOT NULL,
    [Gid]                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Code]                  NVARCHAR(20)        NOT NULL,
    [Name]                  NVARCHAR(MAX)       NOT NULL,
    [NameAlt]               NVARCHAR(MAX)       NULL,
    [Type]                  INT                 NOT NULL,
    [Kind]                  INT                 NOT NULL,
    [Trend]                 INT                 NOT NULL,
    [AggregatedReport]      INT                 NOT NULL,
    [AggregatedTarget]      INT                 NOT NULL,
    [HasGenderDivision]     BIT                 NOT NULL,
    [HasQualitativeTarget]  BIT                 NOT NULL,
    [HasSF]                 BIT                 NOT NULL DEFAULT 0,
    [ReportingType]         INT                 NOT NULL,
    [CreateDate]            DATETIME2           NOT NULL,
    [ModifyDate]            DATETIME2           NOT NULL,
    [Version]               ROWVERSION          NOT NULL,

    CONSTRAINT [PK_Indicators]                      PRIMARY KEY ([IndicatorId]),
    CONSTRAINT [FK_Indicators_MapNodes]             FOREIGN KEY ([ProgrammeId])   REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_Indicators_Measures]             FOREIGN KEY ([MeasureId])     REFERENCES [dbo].[Measures] ([MeasureId]),
    CONSTRAINT [CHK_Indicators_Type]                CHECK       ([Type] IN (1, 2, 3)),
    CONSTRAINT [CHK_Indicators_Kind]                CHECK       ([Kind] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_Indicators_Trend]               CHECK       ([Trend] IN (1, 2, 3)),
    CONSTRAINT [CHK_Indicators_AggregatedReport]    CHECK       ([AggregatedReport] IN (1, 2, 3)),
    CONSTRAINT [CHK_Indicators_AggregatedTarget]    CHECK       ([AggregatedTarget] IN (1, 2, 3)),
    CONSTRAINT [CHK_Indicators_ReportingType]       CHECK       ([ReportingType] IN (1, 2, 3)),
    CONSTRAINT [UQ_Indicators_Code_Type_Kind]       UNIQUE      ([Code], [Type], [Kind], [ProgrammeId])
);

GO


exec spDescTable  N'Indicators', N'Индикатори.'
exec spDescColumn N'Indicators', N'IndicatorId'          , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Indicators', N'ProgrammeId'          , N'Идентификатор на елемента на ниво оперативна програма.'
exec spDescColumn N'Indicators', N'MeasureId'            , N'Идентификатор на мерна единица.'
exec spDescColumn N'Indicators', N'Gid'                  , N'Глобален уникален идентификатор.'
exec spDescColumn N'Indicators', N'Code'                 , N'Код.'
exec spDescColumn N'Indicators', N'Name'                 , N'Наименование.'
exec spDescColumn N'Indicators', N'NameAlt'              , N'Наименование на английски.'
exec spDescColumn N'Indicators', N'Type'                 , N'Тип : 1 - Специфичен за програмата индикатор за резултат, 2 - Общ индикатор за резултат, 3 - Общ за програмата индикатор за изпълнението, 4 - Специфичен за програмата показател за изпълнението, 5 - Рамка за изпълнение, 6 - Индивидуален за процедура.'
exec spDescColumn N'Indicators', N'Kind'                 , N'Вид : 1 - Финансов, 2 - Изпълнение, 3 - Резултат, 4 - Етап на изпълнение.'
exec spDescColumn N'Indicators', N'Trend'                , N'Тенденция : 1 - Намаление, 2 - Увеличение, 3 - Неприложимо.'
exec spDescColumn N'Indicators', N'AggregatedReport'     , N'Маркер, дали индикатора има отчитане с натрупване при отчет: 1 - Не, 2 - Да, 3 - Неприложимо.'
exec spDescColumn N'Indicators', N'AggregatedTarget'     , N'Маркер, дали индикатора има отчитане с натрупване при целева стойност: 1 - Не, 2 - Да, 3 - Неприложимо.'
exec spDescColumn N'Indicators', N'HasGenderDivision'    , N'Маркер, дали индикатора има стойностно разделение по пол: 0 - Не, 1 - Да.'
exec spDescColumn N'Indicators', N'HasQualitativeTarget' , N'Маркер, дали индикатора има качествена стойност: 0 - Не, 1 - Да.'
exec spDescColumn N'Indicators', N'HasSF'                , N'Допълнителен аналитичен признак "S/F" за класифициране на индикаторите с цел генериране на коректен ГДИ в системата: 0 - Не, 1 - Да.'
exec spDescColumn N'Indicators', N'ReportingType'        , N'Начин на отчитане : 1 - Ръчно, 2 - Автоматично (сумиране), 3 - Данни от НСИ.'
exec spDescColumn N'Indicators', N'CreateDate'           , N'Дата на създаване на записа.'
exec spDescColumn N'Indicators', N'ModifyDate'           , N'Дата на последно редактиране на записа.'

GO
