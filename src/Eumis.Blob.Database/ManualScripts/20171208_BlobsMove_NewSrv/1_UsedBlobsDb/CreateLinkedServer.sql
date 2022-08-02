PRINT 'Create linked server'
GO

EXEC sp_addlinkedserver 
    @server = N'UsedBlobsSrv',
    @srvproduct = N'',
    @provider = N'SQLNCLI', 
    @datasrc = N'$(usedBlobsServerIp)',
    @catalog = N'Eumis'

EXEC sp_addlinkedsrvlogin 
    @rmtsrvname = N'UsedBlobsSrv',
    @useself = N'False',
    @rmtuser = N'$(usedBlobsServerUser)',
    @rmtpassword = N'$(usedBlobsServerPassword)'
