PRINT 'ProtectedZones'
GO

CREATE TABLE [dbo].[ProtectedZones] (
    [ProtectedZoneId]   INT             NOT NULL IDENTITY,
    [CountryId]         INT             NOT NULL,
    [NutsCode]          NVARCHAR(200)   NOT NULL,
    [Name]              NVARCHAR(MAX)   NOT NULL,
    [FullPathName]      NVARCHAR(1000)  NOT NULL,
    [NameAlt]           NVARCHAR(200)   NULL,
    [FullPathNameAlt]   NVARCHAR(1000)  NULL,
	[FullPath]          NVARCHAR(500)   NOT NULL,
    CONSTRAINT [PK_ProtectedZones]           PRIMARY KEY ([ProtectedZoneId]),
    CONSTRAINT [FK_ProtectedZones_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([CountryId]),
);
GO

exec spDescTable  N'ProtectedZones', N'Защитени зони'
exec spDescColumn N'ProtectedZones', N'ProtectedZoneId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProtectedZones', N'CountryId'       , N'Идентификатор на държава.'
exec spDescColumn N'ProtectedZones', N'NutsCode'        , N'Код по ZZ'
exec spDescColumn N'ProtectedZones', N'Name'            , N'Наименование.'
exec spDescColumn N'ProtectedZones', N'FullPathName'    , N'Пълно наименование.'
exec spDescColumn N'ProtectedZones', N'NameAlt'         , N'Наименование на друг език.'
exec spDescColumn N'ProtectedZones', N'FullPathNameAlt' , N'Пълно наименование на друг език.'
exec spDescColumn N'ProtectedZones', N'FullPath'        , N'Всички кодове.'
GO
