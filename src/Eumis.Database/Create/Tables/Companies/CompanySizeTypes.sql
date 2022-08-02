PRINT 'CompanySizeTypes'
GO

CREATE TABLE [dbo].[CompanySizeTypes] (
    [CompanySizeTypeId] INT                 NOT NULL IDENTITY,
    [Gid]               UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Name]              NVARCHAR(MAX)       NOT NULL,
    [NameAlt]           NVARCHAR(200)       NULL,
    [Order]             DECIMAL(15,3)       NOT NULL,
    [Alias]             NVARCHAR(200)       NULL,
    CONSTRAINT [PK_CompanySizeTypes] PRIMARY KEY ([CompanySizeTypeId])
);
GO

exec spDescTable  N'CompanySizeTypes', N'Класификация на предприятията спрямо „Закона на малки и средни предприятия“.'
exec spDescColumn N'CompanySizeTypes', N'CompanySizeTypeId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CompanySizeTypes', N'Gid'                , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'CompanySizeTypes', N'Name'               , N'Наименование.'
exec spDescColumn N'CompanySizeTypes', N'NameAlt'            , N'Наименование на друг език.'
exec spDescColumn N'CompanySizeTypes', N'Order'              , N'Пореден номер.'
exec spDescColumn N'CompanySizeTypes', N'Alias'              , N'Псевдоним'
GO
