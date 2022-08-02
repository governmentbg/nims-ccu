PRINT 'Create linked server'
GO

EXEC sp_addlinkedserver 
    @server = N'EumisSrv',
    @srvproduct = N'',
    @provider = N'SQLNCLI', 
    @datasrc = N'$(eumisServerIp)',
    @catalog = N'Eumis'

EXEC sp_addlinkedsrvlogin 
    @rmtsrvname = N'EumisSrv',
    @useself = N'False',
    @rmtuser = N'$(eumisServerUser)',
    @rmtpassword = N'$(eumisServerPassword)'
