PRINT 'Municipalities'
GO

CREATE TABLE [dbo].[Municipalities] (
    [MunicipalityId]        INT             NOT NULL IDENTITY,
    [DistrictId]            INT             NOT NULL,
    [LauCode]               NVARCHAR(200)   NOT NULL,
    [Name]                  NVARCHAR(200)   NOT NULL,
    [DisplayName]           NVARCHAR(200)   NOT NULL,
	[FullPathName]          NVARCHAR(1000)  NOT NULL,
	[FullPath]              NVARCHAR(500)   NOT NULL,
    [NameAlt]               NVARCHAR(200)   NULL,
    [FullPathNameAlt]       NVARCHAR(1000)  NULL,
    CONSTRAINT [PK_Municipalities]           PRIMARY KEY ([MunicipalityId]),
    CONSTRAINT [FK_Municipalities_Districts] FOREIGN KEY ([DistrictId]) REFERENCES [dbo].[Districts] ([DistrictId]),
)
GO

exec spDescTable  N'Municipalities', N'Номенклатура общини LAU1 (Local Administrative Units) ниво 1.'
exec spDescColumn N'Municipalities', N'MunicipalityId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Municipalities', N'DistrictId'           , N'Област.'
exec spDescColumn N'Municipalities', N'LauCode'              , N'Код по LAU1.'
exec spDescColumn N'Municipalities', N'Name'                 , N'Наименование.'
exec spDescColumn N'Municipalities', N'DisplayName'          , N'Уникално наименование.'
exec spDescColumn N'Municipalities', N'FullPathName'         , N'Пълно наименование.'
exec spDescColumn N'Municipalities', N'FullPath'			 , N'Всички кодове.'
exec spDescColumn N'Municipalities', N'NameAlt'              , N'Наименование на английски.'
exec spDescColumn N'Municipalities', N'FullPathNameAlt'      , N'Пълно наименование на английски.'
GO
