PRINT 'Settlements'
GO

CREATE TABLE [dbo].[Settlements] (
    [SettlementId]          INT            NOT NULL IDENTITY,
    [MunicipalityId]        INT            NOT NULL,
    [LauCode]               NVARCHAR (10)  NOT NULL,
    [Name]                  NVARCHAR (200) NOT NULL,
    [DisplayName]           NVARCHAR (200) NOT NULL,
    [FullPathName]          NVARCHAR(1000) NOT NULL,
	[NameAlt]               NVARCHAR (200) NULL,
    [FullPathNameAlt]       NVARCHAR(1000) NULL,
    [FullPath]              NVARCHAR(500)  NOT NULL,
    [Order]                 DECIMAL(18,0)  NOT NULL,
    CONSTRAINT [PK_Settlements]                PRIMARY KEY ([SettlementId]),
    CONSTRAINT [FK_Settlements_Municipalities] FOREIGN KEY ([MunicipalityId]) REFERENCES [dbo].Municipalities (MunicipalityId)
)
GO

exec spDescTable  N'Settlements', N'Териториални единици LAU2 (Local Administrative Units) ниво 2.'
exec spDescColumn N'Settlements', N'SettlementId'           , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Settlements', N'MunicipalityId'         , N'Община.'
exec spDescColumn N'Settlements', N'LauCode'                , N'Код по LAU2.'
exec spDescColumn N'Settlements', N'Name'                   , N'Наименование.'
exec spDescColumn N'Settlements', N'DisplayName'            , N'Уникално наименование.'
exec spDescColumn N'Settlements', N'FullPathName'           , N'Пълно наименование.'
exec spDescColumn N'Settlements', N'NameAlt'                , N'Наименование на друг език.'
exec spDescColumn N'Settlements', N'FullPathNameAlt'        , N'Пълно наименование на друг език.'
exec spDescColumn N'Settlements', N'FullPath'               , N'Всички кодове.'
GO
