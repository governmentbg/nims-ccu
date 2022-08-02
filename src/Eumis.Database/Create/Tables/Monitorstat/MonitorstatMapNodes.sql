PRINT 'MonitorstatMapNodes'
GO

CREATE TABLE [dbo].[MonitorstatMapNodes] (
    [MonitorstatMapNodeId]                               INT                 NOT NULL IDENTITY,
    [MapNodeId]                                          INT                 NOT NULL,
    [Type]                                               INT                 NOT NULL,
    [MonitorstatGid]                                     UNIQUEIDENTIFIER    NOT NULL,
    
    [CreateDate]                                         DATETIME2           NOT NULL,
    [ModifyDate]                                         DATETIME2           NOT NULL,
    [Version]                                            ROWVERSION          NOT NULL,
    
    CONSTRAINT [PK_MonitorstatMapNodes]                  PRIMARY KEY ([MonitorstatMapNodeId]),
    CONSTRAINT [CHK_MonitorstatMapNodes_Type]            CHECK       ([Type]   IN (1, 2, 3)),
);
GO

exec spDescTable  N'MonitorstatMapNodes', N'Съотевтстие на оперативна карта ИСУН с тази на Мониторстат.'
exec spDescColumn N'MonitorstatMapNodes', N'MonitorstatMapNodeId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'MonitorstatMapNodes', N'MapNodeId'              , N'Идентификатор на елемент от оперативната карта в ИСУН.'
exec spDescColumn N'MonitorstatMapNodes', N'Type'                   , N'Вид: 1 - Оперативна програма, 2 - Приоритетна ос, 3 - Процедура.'
exec spDescColumn N'MonitorstatMapNodes', N'MonitorstatGid'         , N'Идентификатор на елемент от оперативната карта в Мониторстат.'
exec spDescColumn N'MonitorstatMapNodes', N'CreateDate'             , N'Дата на създаване на записа.'
exec spDescColumn N'MonitorstatMapNodes', N'ModifyDate'             , N'Дата на последно редактиране на записа.'
exec spDescColumn N'MonitorstatMapNodes', N'Version'                , N'Версия.'
GO
