PRINT 'MapNodes'
GO

CREATE TABLE [dbo].[MapNodes](
    --Common
    [MapNodeId]             INT                NOT NULL IDENTITY,
    [Gid]                   UNIQUEIDENTIFIER   NOT NULL UNIQUE,
    [Status]                INT                NOT NULL,
    [Type]                  NVARCHAR(50)       NOT NULL,
    [Code]                  NVARCHAR(200)      NULL,
    [ShortName]             NVARCHAR(50)       NULL,
    [Name]                  NVARCHAR(MAX)      NOT NULL,
    [NameAlt]               NVARCHAR(MAX)      NULL,
    [CreateDate]            DATETIME2(7)       NOT NULL,
    [ModifyDate]            DATETIME2(7)       NOT NULL,
    --Programmes
    [PortalName]            NVARCHAR(MAX)      NULL,
    [PortalNameAlt]         NVARCHAR(MAX)      NULL,
    [PortalShortNameAlt]    NVARCHAR(MAX)      NULL,
    [PortalOrderNum]        INT                NULL,
    [IrregularityCode]      NVARCHAR(200)      NULL,
    --Programmes, ProgrammePriorities
    [Description]           NVARCHAR(MAX)      NULL,
    [DescriptionAlt]        NVARCHAR(MAX)      NULL,
    --Programmes
    [RegulationNumber]      NVARCHAR(200)      NULL,
    [RegulationDate]        DATETIME2(7)       NULL,
    [CompanyId]             INT                NULL,
    [ProgrammeGroupId]      INT                NULL,
    --InvestmentPriorities
    [InvestmentPriorityId]  INT                NULL,
    [IsHidden]              BIT                NULL,
    --Common
    [Version]               TIMESTAMP          NOT NULL,

    CONSTRAINT [PK_MapNodes]                      PRIMARY KEY ([MapNodeId]),
    CONSTRAINT [FK_MapNodes_InvestmentPriorities] FOREIGN KEY ([InvestmentPriorityId])  REFERENCES [dbo].[InvestmentPriorities] ([InvestmentPriorityId]),
    CONSTRAINT [FK_MapNodes_ProgrammeGroups]      FOREIGN KEY ([ProgrammeGroupId])      REFERENCES [dbo].[ProgrammeGroups] ([ProgrammeGroupId]),
    CONSTRAINT [CHK_MapNodes_Type]                CHECK       ([Type]   IN (N'Programme', N'ProgrammePriority', N'InvestmentPriority', N'SpecificTarget')),
    CONSTRAINT [CHK_MapNodes_Status]              CHECK       ([Status] IN (1, 2, 3))
);

GO

exec spDescTable  N'MapNodes', N'Оперативна карта.'
exec spDescColumn N'MapNodes', N'MapNodeId'           , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'MapNodes', N'Type'                , N'Тип на елемента.'
exec spDescColumn N'MapNodes', N'Status'              , N'Статус: 1 - Чернова; 2 - Въведен; 3 - Анулиран.'
exec spDescColumn N'MapNodes', N'Code'                , N'Код.'
exec spDescColumn N'MapNodes', N'ShortName'           , N'Кратко наименование.'
exec spDescColumn N'MapNodes', N'Name'                , N'Наименование.'
exec spDescColumn N'MapNodes', N'NameAlt'             , N'Наименование на друг език.'
exec spDescColumn N'MapNodes', N'CreateDate'          , N'Дата на създаване на записа.'
exec spDescColumn N'MapNodes', N'ModifyDate'          , N'Дата на последно редактиране на записа.'
exec spDescColumn N'MapNodes', N'Description'         , N'Описание.'
exec spDescColumn N'MapNodes', N'DescriptionAlt'      , N'Описание на друг език.'
exec spDescColumn N'MapNodes', N'CompanyId'           , N'Организация(Управляващ орган).'
exec spDescColumn N'MapNodes', N'ProgrammeGroupId'    , N'Номенклатура за междинна сума.'
exec spDescColumn N'MapNodes', N'RegulationNumber'    , N'Номер на решение.'
exec spDescColumn N'MapNodes', N'RegulationDate'      , N'Дата на решение.'
exec spDescColumn N'MapNodes', N'InvestmentPriorityId', N'Идентификатор на инвестиционен приоритет.'
exec spDescColumn N'MapNodes', N'IsHidden'            , N'Поле отбелязващто системните(скрити) ИП при липса на такива и наличие на специфични цели.'
exec spDescColumn N'MapNodes', N'Version'             , N'Версия.'

GO
