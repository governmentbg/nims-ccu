PRINT 'ContractReportMicrosMunicipalities'
GO 

CREATE TABLE [dbo].[ContractReportMicrosMunicipalities] (
    [ContractReportMicrosMunicipalityId] INT             NOT NULL IDENTITY,
    [ContractReportMicrosDistrictId]     INT             NOT NULL,
    [MunicipalityId]                     INT             NOT NULL,
    [Name]                               NVARCHAR(200)   NOT NULL,
    CONSTRAINT [PK_ContractReportMicrosMunicipalities]                               PRIMARY KEY ([ContractReportMicrosMunicipalityId]),
    CONSTRAINT [FK_ContractReportMicrosMunicipalities_ContractReportMicrosDistricts] FOREIGN KEY ([ContractReportMicrosDistrictId]) REFERENCES [dbo].[ContractReportMicrosDistricts] ([ContractReportMicrosDistrictId]),
    CONSTRAINT [FK_ContractReportMicrosMunicipalities_Municipalities]                FOREIGN KEY ([MunicipalityId])                 REFERENCES [dbo].[Municipalities]                ([MunicipalityId])
)
GO

exec spDescTable  N'ContractReportMicrosMunicipalities', N'Общини към микроданните.'
exec spDescColumn N'ContractReportMicrosMunicipalities', N'ContractReportMicrosMunicipalityId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportMicrosMunicipalities', N'ContractReportMicrosDistrictId'    , N'Идентификатор на област.'
exec spDescColumn N'ContractReportMicrosMunicipalities', N'MunicipalityId'                    , N'Идентификатор на община.'
exec spDescColumn N'ContractReportMicrosMunicipalities', N'Name'                              , N'Наименование.'
GO
