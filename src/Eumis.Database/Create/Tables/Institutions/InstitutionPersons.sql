PRINT 'InstitutionPersons'
GO

CREATE TABLE [dbo].[InstitutionPersons] (
    [InstitutionPersonId]         INT               NOT NULL IDENTITY,
    [InstitutionId]               INT               NOT NULL, 
    [ParentInstitutionPersonId]   INT               NULL, 
    [Name]                        NVARCHAR(200)     NOT NULL,
    [Position]                    NVARCHAR(50)      NOT NULL,
    [Phone]                       NVARCHAR(50)      NOT NULL,
    [Fax]                         NVARCHAR(50)      NOT NULL,
    [Email]                       NVARCHAR(50)      NOT NULL,
    [IsContact]                   BIT               NOT NULL, 
    
    CONSTRAINT [PK_InstitutionPersons]              PRIMARY KEY ([InstitutionPersonId]),
    CONSTRAINT [FK_InstitutionPersons_Institutions] FOREIGN KEY ([InstitutionId]) REFERENCES [dbo].[Institutions] ([InstitutionId])
);
GO

exec spDescTable  N'InstitutionPersons', N'Лица към Институция.'
exec spDescColumn N'InstitutionPersons', N'InstitutionPersonId',        N'Уникален системно генериран идентификатор.'
exec spDescColumn N'InstitutionPersons', N'InstitutionId',              N'Идентификатор на Институция.'
exec spDescColumn N'InstitutionPersons', N'Name',                       N'Име.'
exec spDescColumn N'InstitutionPersons', N'Position',                   N'Длъжност.'
exec spDescColumn N'InstitutionPersons', N'Phone',                      N'Телефон.'
exec spDescColumn N'InstitutionPersons', N'Fax',                        N'Факс.'
exec spDescColumn N'InstitutionPersons', N'Email',                      N'Ел. адрес.'
exec spDescColumn N'InstitutionPersons', N'IsContact',                  N'Маркер, дали лицето е за контакт за институцията.'
