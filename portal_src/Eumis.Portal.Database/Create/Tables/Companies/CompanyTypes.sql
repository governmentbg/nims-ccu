PRINT 'CompanyTypes'
GO

CREATE TABLE [dbo].[CompanyTypes] (
    [CompanyTypeId]     INT                 NOT NULL IDENTITY,
    [Gid]               UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Name]              NVARCHAR(MAX)       NOT NULL,
    [NameAlt]           NVARCHAR(200)       NULL,
    [Order]             DECIMAL(15,3)       NOT NULL,
    [Alias]             NVARCHAR(200)       NULL,
    CONSTRAINT [PK_CompanyTypes] PRIMARY KEY ([CompanyTypeId])
);
GO

exec spDescTable  N'CompanyTypes', N'Типове организация.'
exec spDescColumn N'CompanyTypes', N'CompanyTypeId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CompanyTypes', N'Gid'               , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'CompanyTypes', N'Name'              , N'Наименование.'
exec spDescColumn N'CompanyTypes', N'NameAlt'           , N'Наименование на английски.'
exec spDescColumn N'CompanyTypes', N'Order'             , N'Пореден номер.'
exec spDescColumn N'CompanyTypes', N'Alias'             , N'Псевдоним'
/* Name exams
Нестопански организации
Учебни и медицински заведения
Държавни институции
Компании
Чуждестранен
Други
Физически лица, регистрирани по Булстат
*/
GO
