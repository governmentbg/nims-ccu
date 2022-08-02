PRINT 'ContractReportMicrosDistricts'
GO 

CREATE TABLE [dbo].[ContractReportMicrosDistricts] (
    [ContractReportMicrosDistrictId]  INT             NOT NULL IDENTITY,
    [DistrictId]                      INT             NOT NULL,
    [Name]                            NVARCHAR(200)   NOT NULL,
    CONSTRAINT [PK_ContractReportMicrosDistricts]           PRIMARY KEY ([ContractReportMicrosDistrictId]),
    CONSTRAINT [FK_ContractReportMicrosDistricts_Districts] FOREIGN KEY ([DistrictId]) REFERENCES [dbo].[Districts] ([DistrictId])
)
GO

exec spDescTable  N'ContractReportMicrosDistricts', N'Области към микроданните'
exec spDescColumn N'ContractReportMicrosDistricts', N'ContractReportMicrosDistrictId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportMicrosDistricts', N'DistrictId'                    , N'Идентификатор на област.'
exec spDescColumn N'ContractReportMicrosDistricts', N'Name'                          , N'Наименование.'
GO
