PRINT 'CompanyLegalTypes'
GO

CREATE TABLE [dbo].[CompanyLegalTypes] (
    [CompanyLegalTypeId]                        INT                 NOT NULL IDENTITY,
    [Gid]                                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [CompanyTypeId]                             INT                 NOT NULL,
    [Name]                                      NVARCHAR(MAX)       NOT NULL,
    [NameAlt]                                   NVARCHAR(MAX)       NULL,
    [Order]                                     DECIMAL(15,3)       NOT NULL,
    [Alias]                                     NVARCHAR(200)       NULL,
    [CodeCommercialRegister]                    NVARCHAR(20)        NULL,
    [CodeBulstatRegister]                       NVARCHAR(20)        NULL,
    CONSTRAINT [PK_CompanyLegalTypes]                   PRIMARY KEY ([CompanyLegalTypeId]),
    CONSTRAINT [FK_CompanyLegalTypes_CompanyTypes]      FOREIGN KEY ([CompanyTypeId])       REFERENCES [dbo].[CompanyTypes] ([CompanyTypeId]),
);
GO

exec spDescTable  N'CompanyLegalTypes', N'Типове правен статут на организация.'
exec spDescColumn N'CompanyLegalTypes', N'CompanyLegalTypeId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CompanyLegalTypes', N'Gid'                      , N'Глобален уникален идентификатор.'
exec spDescColumn N'CompanyLegalTypes', N'CompanyTypeId'            , N'Идентификатор на тип органицазия.'
exec spDescColumn N'CompanyLegalTypes', N'Name'                     , N'Наименование.'
exec spDescColumn N'CompanyLegalTypes', N'NameAlt'                  , N'Наименование на английски.'
exec spDescColumn N'CompanyLegalTypes', N'Order'                    , N'Пореден номер.'
exec spDescColumn N'CompanyLegalTypes', N'Alias'                    , N'Псевдоним'
exec spDescColumn N'CompanyLegalTypes', N'CodeCommercialRegister'   , N'Код в Тъговски регистър.'
exec spDescColumn N'CompanyLegalTypes', N'CodeBulstatRegister'      , N'Код в регистър Булстат.'

/* Name exams
Държавни институции / Община
Държавни институции / Областна администрация
Държавни институции / Министерство
Държавни институции / Агенция
Компании / Дружество с ограничена отговорност ООД
Компании / Еднолично дружество с ограничена отговорност ЕООД
Компании / Акционерно дружество АД
Компании / Едноличен Търговец ЕТ
Компании / Събирателно дружество СД
Компании / Командитно дружество КД
Компании / Командитно дружество с акции КДА
*/
GO
