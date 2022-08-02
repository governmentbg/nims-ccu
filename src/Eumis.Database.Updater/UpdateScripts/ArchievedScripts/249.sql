GO
ALTER TABLE [dbo].[News] ADD
[Author]        NVARCHAR(200)   NULL,
[AuthorAlt]     NVARCHAR(200)   NULL
GO

UPDATE [dbo].[News] SET 
    [Author] = '',
    [AuthorAlt] = ''
WHERE [Type] = 2
GO
