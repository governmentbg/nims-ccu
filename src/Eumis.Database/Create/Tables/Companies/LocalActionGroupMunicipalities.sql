PRINT 'LocalActionGroupMunicipalities'
GO

CREATE TABLE [dbo].[LocalActionGroupMunicipalities] (
    [LocalActionGroupMunicipalityId]     INT                 NOT NULL IDENTITY,
    [CompanyId]                          INT                 NOT NULL,
    [MunicipalityId]                     INT                 NOT NULL,

    CONSTRAINT [PK_LocalActionGroupMunicipalities]                 PRIMARY KEY ([LocalActionGroupMunicipalityId]),
    CONSTRAINT [FK_LocalActionGroupMunicipalities_Companies]       FOREIGN KEY ([CompanyId])      REFERENCES [dbo].[Companies] ([CompanyId]),
    CONSTRAINT [FK_LocalActionGroupMunicipalities_Municipalities]  FOREIGN KEY ([MunicipalityId]) REFERENCES [dbo].[Municipalities] ([MunicipalityId]),
);
GO

exec spDescTable  N'LocalActionGroupMunicipalities', N'Местоположение на МИГ/МИРГ'
exec spDescColumn N'LocalActionGroupMunicipalities', N'LocalActionGroupMunicipalityId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LocalActionGroupMunicipalities', N'CompanyId'                       , N'Идентификатор на организация.'
exec spDescColumn N'LocalActionGroupMunicipalities', N'MunicipalityId'                  , N'Идентификатор на община.'
GO
