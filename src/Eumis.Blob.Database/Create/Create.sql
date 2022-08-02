USE [$(dbName)]
GO

---------------------------------------------------------------
--Tools
---------------------------------------------------------------

:r $(rootPath)\"Tools\Tool_ScriptDiagram2008.sql"
:r $(rootPath)\"Tools\spDesc.sql"
:r $(rootPath)\"Tools\sp_generate_inserts.sql"
:r $(rootPath)\"Tools\spAddDailyJob.sql"

---------------------------------------------------------------
--Tables
---------------------------------------------------------------
:r $(rootPath)"\Tables\BlobContents.sql"

---------------------------------------------------------------
--Views
---------------------------------------------------------------
:r $(rootPath)"\Views\vwBlobContentPartitions.sql"

---------------------------------------------------------------
--Procedures
---------------------------------------------------------------

:r $(rootPath)"\Procedures\spDeleteBlobContents.sql"
:r $(rootPath)"\Procedures\spMarkDeletedBlobContents.sql"
