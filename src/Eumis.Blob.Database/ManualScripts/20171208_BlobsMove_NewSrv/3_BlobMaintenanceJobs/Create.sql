SET QUOTED_IDENTIFIER ON
GO

PRINT '------ Creating production blob maintеnance jobs'
:r ".\CreateLinkedServer.sql"
:r ".\jobDeleteBlobContents.sql"
:r ".\jobMarkDeletedBlobContents.sql"
