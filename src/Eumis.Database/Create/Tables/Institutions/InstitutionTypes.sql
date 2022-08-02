PRINT 'InstitutionTypes'
GO

CREATE TABLE [dbo].[InstitutionTypes] (
    [InstitutionTypeId] INT             NOT NULL IDENTITY,
    [Code]              NVARCHAR(200)   NOT NULL,
    [Name]              NVARCHAR(200)   NOT NULL,
    [Alias]             NVARCHAR(200)   NULL,
    CONSTRAINT [PK_InstitutionTypes] PRIMARY KEY ([InstitutionTypeId])
);
GO

exec spDescTable  N'InstitutionTypes', N'Тип на институцията.'
exec spDescColumn N'InstitutionTypes', N'InstitutionTypeId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'InstitutionTypes', N'Code'              , N'Код'
exec spDescColumn N'InstitutionTypes', N'Name'              , N'Наименование - Управляващ орган, Междинно звено, Одитиращ орган, Сертифициращ орган, Централно координационно звено, Министър, Контрол, Външен оценител.'
exec spDescColumn N'InstitutionTypes', N'Alias'             , N'Псевдоним'
