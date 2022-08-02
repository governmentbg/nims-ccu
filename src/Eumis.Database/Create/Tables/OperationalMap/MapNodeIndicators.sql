PRINT 'MapNodeIndicators'
GO


CREATE TABLE [dbo].[MapNodeIndicators](
    [MapNodeId]                      INT                NOT NULL,--table3, table4, table4a, table5, table6, table12, table13
    [IndicatorId]                    INT                NOT NULL,--table3, table4, table4a, table5, table6, table12, table13
    [Type]                           NVARCHAR(10)       NOT NULL,--table3, table4, table4a, table5, table6, table12, table13
    [RegionCategory]                 INT                NULL,    --table3, table4,          table5, table6                  
    [FinanceSource]                  INT                NULL,    --                         table5, table6                  
    [BaseTotalValue]                 DECIMAL(15,3)      NULL,    --table3, table4, table4a,                 table12.........
    [BaseMenValue]                   DECIMAL(15,3)      NULL,    --        table4, table4a,                 table12.........
    [BaseWomenValue]                 DECIMAL(15,3)      NULL,    --        table4, table4a,                 table12.........
    [BaseQualitativeValue]           NVARCHAR(100)      NULL,    --        table4                                           
    [BaseYear]                       INT                NULL,    --table3, table4, table4a,                 table12
    [TargetTotalValue]               DECIMAL(15,3)      NULL,    --table3, table4, table4a, table5,         table12, table13
    [TargetMenValue]                 DECIMAL(15,3)      NULL,    --        table4, table4a, table5,         table12, table13
    [TargetWomenValue]               DECIMAL(15,3)      NULL,    --        table4, table4a, table5,         table12, table13
    [TargetQualitativeValue]         NVARCHAR(100)      NULL,    --        table4                                           
    [MilestoneTargetTotalValue]      DECIMAL(15,3)      NULL,    --                                 table6                  
    [MilestoneTargetMenValue]        DECIMAL(15,3)      NULL,    --                                 table6                  
    [MilestoneTargetWomenValue]      DECIMAL(15,3)      NULL,    --                                 table6                  
    [FinalTargetTotalValue]          DECIMAL(15,3)      NULL,    --                                 table6                  
    [FinalTargetMenValue]            DECIMAL(15,3)      NULL,    --                                 table6                  
    [FinalTargetWomenValue]          DECIMAL(15,3)      NULL,    --                                 table6                  
    [MeasureId]                      INT                NULL,    --        table4, table4a                                  
    [DataSource]                     NVARCHAR(100)      NULL,    --table3, table4, table4a, table5, table6, table12, table13
    [ReportingFrequancy]             NVARCHAR(100)      NULL,    --table3, table4, table4a, table5,         table12.........
    [CommonBaseIndicator]            NVARCHAR(200)      NULL,    --        table4, table4a                                  
    [Description]                    NVARCHAR(MAX)      NULL,    --                                 table6                  

    CONSTRAINT [PK_MapNodeIndicators]                   PRIMARY KEY   ([MapNodeId], [IndicatorId]),
    CONSTRAINT [FK_MapNodeIndicators_MapNodes]          FOREIGN KEY   ([MapNodeId])     REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_MapNodeIndicators_Indicators]        FOREIGN KEY   ([IndicatorId])   REFERENCES [dbo].[Indicators] ([IndicatorId]),
    CONSTRAINT [FK_MapNodeIndicators_Measures]          FOREIGN KEY   ([MeasureId])     REFERENCES [dbo].[Measures] ([MeasureId]),
    CONSTRAINT [CHK_MapNodeIndicators_Type]             CHECK         ([Type] IN (N'Table3', N'Table4', N'Table4a', N'Table5', N'Table6', N'Table12', N'Table13')),
    CONSTRAINT [CHK_MapNodeIndicators_RegionCategory]   CHECK         ([RegionCategory] IN (1)),
    CONSTRAINT [CHK_MapNodeIndicators_FinanceSource]    CHECK         ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)),
);

GO

exec spDescTable  N'MapNodeIndicators', N'Индикатори на елемент на оперативна карта.'
exec spDescColumn N'MapNodeIndicators', N'MapNodeId'                 , N'Идентификатор на елемент на оперативна карта.'
exec spDescColumn N'MapNodeIndicators', N'IndicatorId'               , N'Идентификатор на индикатор.'
exec spDescColumn N'MapNodeIndicators', N'Type'                      , N'Тип на идикатора за присъединяване'
exec spDescColumn N'MapNodeIndicators', N'RegionCategory'            , N'Категория регион: 1 - По-слабо развити региони'
exec spDescColumn N'MapNodeIndicators', N'FinanceSource'             , N'Източник на финансиране'
exec spDescColumn N'MapNodeIndicators', N'BaseTotalValue'            , N'Базова стойност.'
exec spDescColumn N'MapNodeIndicators', N'BaseMenValue'              , N'Базова стойност - Мъже.'
exec spDescColumn N'MapNodeIndicators', N'BaseWomenValue'            , N'Базова стойност - Жени.'
exec spDescColumn N'MapNodeIndicators', N'BaseQualitativeValue'      , N'Качествена базова стойност.'
exec spDescColumn N'MapNodeIndicators', N'BaseYear'                  , N'Базова година.'
exec spDescColumn N'MapNodeIndicators', N'TargetTotalValue'          , N'Целева стойност. (2023г.)'
exec spDescColumn N'MapNodeIndicators', N'TargetMenValue'            , N'Целева стойност (2023г.) - Мъже'
exec spDescColumn N'MapNodeIndicators', N'TargetWomenValue'          , N'Целева стойност (2023г.) - Жени'
exec spDescColumn N'MapNodeIndicators', N'TargetQualitativeValue'    , N'Качествена целева стойност (2023 г.)'
exec spDescColumn N'MapNodeIndicators', N'MilestoneTargetTotalValue' , N'Етапна цел за 2018 - Общо (Дробно число)'
exec spDescColumn N'MapNodeIndicators', N'MilestoneTargetMenValue'   , N'Етапна цел за 2018 - Мъже (Дробно число)'
exec spDescColumn N'MapNodeIndicators', N'MilestoneTargetWomenValue' , N'Етапна цел за 2018 - Жени (Дробно число)'
exec spDescColumn N'MapNodeIndicators', N'FinalTargetTotalValue'     , N'Крайна цел (2023)  Мъже	(Дробно число)'
exec spDescColumn N'MapNodeIndicators', N'FinalTargetMenValue'       , N'Крайна цел (2023)  Жени	(Дробно число)'
exec spDescColumn N'MapNodeIndicators', N'FinalTargetWomenValue'     , N'Крайна цел (2023) (Дробно число)'
exec spDescColumn N'MapNodeIndicators', N'MeasureId'                 , N'Мерна единица за базовата и целевата стойност'
exec spDescColumn N'MapNodeIndicators', N'DataSource'                , N'Източник на данните.'
exec spDescColumn N'MapNodeIndicators', N'ReportingFrequancy'        , N'Честота на отчитане.'
exec spDescColumn N'MapNodeIndicators', N'CommonBaseIndicator'       , N'Общ индикатор за продукт използван като основа за определянето на целите'
exec spDescColumn N'MapNodeIndicators', N'Description'               , N'Обяснение за значението на индикатора.'

GO