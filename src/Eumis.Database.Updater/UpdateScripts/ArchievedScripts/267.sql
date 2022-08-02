PRINT 'ProgrammeGroups'
GO

CREATE TABLE [dbo].[ProgrammeGroups] (
    [ProgrammeGroupId]     INT             NOT NULL IDENTITY,
    [Name]                 NVARCHAR(100)   NOT NULL,
    [NameAlt]              NVARCHAR(100)   NOT NULL,
    [ShortName]            NVARCHAR(10)    NOT NULL,
    [ShortNameAlt]         NVARCHAR(10)    NOT NULL,
    [PortalOrderNum]       INT             NOT NULL,

    [CreateDate]           DATETIME2       NOT NULL,
    [ModifyDate]           DATETIME2       NOT NULL,
    [Version]              ROWVERSION      NOT NULL,
    CONSTRAINT [PK_ProgrammeGroups]          PRIMARY KEY     ([ProgrammeGroupId])
);
GO

exec spDescTable  N'ProgrammeGroups', N'Междинни суми.'
exec spDescColumn N'ProgrammeGroups', N'ProgrammeGroupId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeGroups', N'Name'                 , N'Име на български.'
exec spDescColumn N'ProgrammeGroups', N'NameAlt'              , N'Име на английски.'
exec spDescColumn N'ProgrammeGroups', N'ShortName'            , N'Кратко име на български.'
exec spDescColumn N'ProgrammeGroups', N'ShortNameAlt'         , N'Кратко име на английски.'
exec spDescColumn N'ProgrammeGroups', N'PortalOrderNum'       , N'Пореден номер в публичния портал.'
exec spDescColumn N'ProgrammeGroups', N'CreateDate'           , N'Дата на създаване на записа.'
exec spDescColumn N'ProgrammeGroups', N'ModifyDate'           , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProgrammeGroups', N'Version'              , N'Версия.'

GO

ALTER TABLE [dbo].[MapNodes] ADD [ProgrammeGroupId] INT NULL
GO

ALTER TABLE [dbo].[MapNodes] ADD CONSTRAINT [FK_MapNodes_ProgrammeGroup]
FOREIGN KEY ([ProgrammeGroupId]) REFERENCES [dbo].[ProgrammeGroups] ([ProgrammeGroupId]);

GO
