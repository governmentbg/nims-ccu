PRINT 'MapNodeIndicatorReportedAmounts'
GO

CREATE TABLE [dbo].[MapNodeIndicatorReportedAmounts] (
    [MapNodeId]         INT NOT NULL,
    [IndicatorId]       INT NOT NULL,
    [Men2014]           INT NOT NULL    DEFAULT 0,
    [Men2015]           INT NOT NULL    DEFAULT 0,
    [Men2016]           INT NOT NULL    DEFAULT 0,
    [Men2017]           INT NOT NULL    DEFAULT 0,
    [Men2018]           INT NOT NULL    DEFAULT 0,
    [Men2019]           INT NOT NULL    DEFAULT 0,
    [Men2020]           INT NOT NULL    DEFAULT 0,
    [Men2021]           INT NOT NULL    DEFAULT 0,
    [Men2022]           INT NOT NULL    DEFAULT 0,
    [Men2023]           INT NOT NULL    DEFAULT 0,
    [Women2014]         INT NOT NULL    DEFAULT 0,
    [Women2015]         INT NOT NULL    DEFAULT 0,
    [Women2016]         INT NOT NULL    DEFAULT 0,
    [Women2017]         INT NOT NULL    DEFAULT 0,
    [Women2018]         INT NOT NULL    DEFAULT 0,
    [Women2019]         INT NOT NULL    DEFAULT 0,
    [Women2020]         INT NOT NULL    DEFAULT 0,
    [Women2021]         INT NOT NULL    DEFAULT 0,
    [Women2022]         INT NOT NULL    DEFAULT 0,
    [Women2023]         INT NOT NULL    DEFAULT 0,
    [Total2014]         INT NOT NULL    DEFAULT 0,
    [Total2015]         INT NOT NULL    DEFAULT 0,      
    [Total2016]         INT NOT NULL    DEFAULT 0,
    [Total2017]         INT NOT NULL    DEFAULT 0,
    [Total2018]         INT NOT NULL    DEFAULT 0,
    [Total2019]         INT NOT NULL    DEFAULT 0,
    [Total2020]         INT NOT NULL    DEFAULT 0,
    [Total2021]         INT NOT NULL    DEFAULT 0,
    [Total2022]         INT NOT NULL    DEFAULT 0,
    [Total2023]         INT NOT NULL    DEFAULT 0,
    CONSTRAINT [PK_MapNodeIndicatorReportedAmounts]                 PRIMARY KEY ([MapNodeId], [IndicatorId]),
    CONSTRAINT [FK_MapNodeIndicatorReportedAmounts_MapNodes]        FOREIGN KEY ([MapNodeId])       REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_MapNodeIndicatorReportedAmounts_Indicators]      FOREIGN KEY ([IndicatorId])     REFERENCES [dbo].[Indicators] ([IndicatorId])
);

GO

exec spDescTable  N'MapNodeIndicatorReportedAmounts', N'Отчетни стойности за индикатор.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'MapNodeId'     , N'Идентификатор на елемент на оперативна карта.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'IndicatorId'   , N'Идентификатор на индикатор.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Men2014'       , N'Отчетена стойност мъже 2014.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Men2015'       , N'Отчетена стойност мъже 2015.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Men2016'       , N'Отчетена стойност мъже 2016.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Men2017'       , N'Отчетена стойност мъже 2017.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Men2018'       , N'Отчетена стойност мъже 2018.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Men2019'       , N'Отчетена стойност мъже 2019.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Men2020'       , N'Отчетена стойност мъже 2020.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Men2021'       , N'Отчетена стойност мъже 2021.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Men2022'       , N'Отчетена стойност мъже 2022.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Men2023'       , N'Отчетена стойност мъже 2023.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Women2014'     , N'Отчетена стойност жени 2014.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Women2015'     , N'Отчетена стойност жени 2015.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Women2016'     , N'Отчетена стойност жени 2016.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Women2017'     , N'Отчетена стойност жени 2017.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Women2018'     , N'Отчетена стойност жени 2018.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Women2019'     , N'Отчетена стойност жени 2019.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Women2020'     , N'Отчетена стойност жени 2020.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Women2021'     , N'Отчетена стойност жени 2021.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Women2022'     , N'Отчетена стойност жени 2022.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Women2023'     , N'Отчетена стойност жени 2023.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Total2014'     , N'Отчетена стойност общо 2014.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Total2015'     , N'Отчетена стойност общо 2015.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Total2016'     , N'Отчетена стойност общо 2016.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Total2017'     , N'Отчетена стойност общо 2017.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Total2018'     , N'Отчетена стойност общо 2018.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Total2019'     , N'Отчетена стойност общо 2019.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Total2020'     , N'Отчетена стойност общо 2020.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Total2021'     , N'Отчетена стойност общо 2021.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Total2022'     , N'Отчетена стойност общо 2022.'
exec spDescColumn N'MapNodeIndicatorReportedAmounts', N'Total2023'     , N'Отчетена стойност общо 2023.'
GO
