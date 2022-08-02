GO

EXEC sp_RENAME 'Registrations.FullName' , 'FirstName', 'COLUMN'

ALTER TABLE Registrations
ALTER COLUMN
    [FirstName] NVARCHAR (100)  NOT NULL

ALTER TABLE Registrations
ADD [LastName]  NVARCHAR (100)  NOT NULL DEFAULT(N''),
    [Phone]     NVARCHAR (50)   NULL
