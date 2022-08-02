PRINT 'ContractLocations'
GO

CREATE TABLE [dbo].[ContractLocations] (
    [ContractLocationId]    INT             NOT NULL IDENTITY,
    [ContractId]            INT             NOT NULL,

    [NutsCode]              NVARCHAR(MAX)   NOT NULL,
    [Name]                  NVARCHAR(MAX)   NOT NULL,
    [FullPath]              NVARCHAR(MAX)   NOT NULL,
    [FullPathName]          NVARCHAR(MAX)   NOT NULL,
    [NameAlt]               NVARCHAR(200)   NULL,
    [FullPathNameAlt]       NVARCHAR(1000)  NULL,

    CONSTRAINT [PK_ContractLocations]               PRIMARY KEY ([ContractLocationId]),
    CONSTRAINT [FK_ContractLocations_Contracts]     FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId])
);
GO

exec spDescTable  N'ContractLocations', N'Местонахождения към договор.'
exec spDescColumn N'ContractLocations', N'ContractLocationId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractLocations', N'ContractId'               , N'Идентификатор на договор.'

exec spDescColumn N'ContractLocations', N'NutsCode'                 , N'Код на местонахождение.'
exec spDescColumn N'ContractLocations', N'Name'                     , N'Наименование.'
exec spDescColumn N'ContractLocations', N'FullPath'                 , N'Пълен път на местонахождение.'
exec spDescColumn N'ContractLocations', N'FullPathName'             , N'Пълно наименование на местонахождение.'
exec spDescColumn N'ContractLocations', N'NameAlt'                  , N'Наименование.'
exec spDescColumn N'ContractLocations', N'FullPathNameAlt'          , N'Пълно наименование на местонахождение.'
GO
