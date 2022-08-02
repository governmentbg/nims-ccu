PRINT 'CompanyPersons'
GO

CREATE TABLE [dbo].[CompanyPersons] (
    [CompanyPersonId]         INT               NOT NULL IDENTITY,
    [CompanyId]               INT               NOT NULL,
    [Name]                    NVARCHAR(200)     NOT NULL,
    [Position]                NVARCHAR(50)      NOT NULL,
    [Phone]                   NVARCHAR(50)      NOT NULL,
    [Fax]                     NVARCHAR(50)      NOT NULL,
    [Email]                   NVARCHAR(50)      NOT NULL,
    [IsContact]               BIT               NOT NULL,
    
    CONSTRAINT [PK_CompanyPersons]           PRIMARY KEY ([CompanyPersonId]),
    CONSTRAINT [FK_CompanyPersons_Companies] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies] ([CompanyId])
);
GO

exec spDescTable  N'CompanyPersons', N'Лица към Организация.'
exec spDescColumn N'CompanyPersons', N'CompanyPersonId',        N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CompanyPersons', N'CompanyId',              N'Идентификатор на Организация.'
exec spDescColumn N'CompanyPersons', N'Name',                   N'Име.'
exec spDescColumn N'CompanyPersons', N'Position',               N'Длъжност.'
exec spDescColumn N'CompanyPersons', N'Phone',                  N'Телефон.'
exec spDescColumn N'CompanyPersons', N'Fax',                    N'Факс.'
exec spDescColumn N'CompanyPersons', N'Email',                  N'Ел. адрес.'
exec spDescColumn N'CompanyPersons', N'IsContact',              N'Маркер, дали лицето е за контакт за организацията.'
