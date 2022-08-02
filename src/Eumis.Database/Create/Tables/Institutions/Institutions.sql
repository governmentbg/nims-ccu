PRINT 'Institutions'
GO

CREATE TABLE [dbo].[Institutions] (
    [InstitutionId]         INT             NOT NULL IDENTITY,
    [Name]                  NVARCHAR(200)   NOT NULL,
    [Description]           NVARCHAR(50)    NOT NULL,
    
    CONSTRAINT [PK_Institutions]                  PRIMARY KEY ([InstitutionId])
);
GO

exec spDescTable  N'Institutions', N'Институции.'
exec spDescColumn N'Institutions', N'Name', N'Кратко име.'
exec spDescColumn N'Institutions', N'Description', N'Пълно име.'

GO




