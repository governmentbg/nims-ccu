NIMS
=====

#### To run the project

1. Prerequisites
 - VisualStudio 2019 v16.2+
 - .net 3.5
 - .net 4.7.2+
 - NodeJS 10.16+
 - Locally installed default SQLServer instance
2. Run `npm install` in `.\src\Eumis.Web.App` to install the required nodejs packages
3. Open and build the solution in `.\src`  
4. Build the angularjs app by running `npm run build` in `.\src\Eumis.Web.App`
  ** To continuously build the app run `npm run watch`
5. Create the required databases by running the CreateAll.tt templates for each database project (right click on the file and then `Run Custom Tool`)
 - Nims.Database
 - Nims.Blob.Database
 - Nims.Log.Database
6. Start the following projects
 - Nims.Web.Host
 - Nims.PortalIntegration.Host
 - Nims.Blob.Host

#### Prepare a new server for installation

1. Add WebServer Role with the following checked:
  - Common HTTP Features
    * HTTP Redirection
  - Performance
    * Dynamic Content Compression
  - Application
    * Application Initialization
    * ASP.NET 4.6
  - Security
    * IP and Domain Restrictions
  - Management tools
    * Management Service
  
**Important!!** If you decide to install All features for the Web Role make sure you remove the `Common Http Features > WebDAV Publishing` feature or it will break the application by returning `405 Method Not Allowed` on PUT requests. If the feature was already installed remove it and **restart** the server.
    
2. Install Microsoft Web Platform Installer and install the following addons
 - URL Rewrite 2.0
 - Web Deploy 3.6 without bundled SQL support (latest)
3. Add web sites:
 - Nims
 - NimsBlob
 - NimsJob
 - NimsPortalIntegration
4. Add full control permissions on C:\Windows\TEMP for 'IIS APPPOOL\Nims' and 'IIS APPPOOL\NimsJob' (Needed for razor view compilation)
5. Add full control permissions on C:\Logs\ for 'IIS APPPOOL\Nims', 'IIS APPPOOL\NimsPortalIntegration', 'IIS APPPOOL\NimsBlob' and 'IIS APPPOOL\NimsJob'
6. For each apppool set
 - Start Mode to AlwaysRunning
 - Idle Time-out (minutes) to 0
 - Regular Time Interval (minutes) to 0
 - Specific Times to 00:00:00
7. In the server ConfigurationEditor:
 - In system.applicationHost/sites  
  For each site set application/preloadEnabled="true".  
	`<application preloadEnabled="true">`  
 - In system.webServer/httpCompression/dynamicTypes add the following above the `*/*` rule  
  `<add mimeType="application/json" enabled="true" />`  
  `<add mimeType="application/json; charset=utf-8" enabled="true" />`
8. In the NimsPortalIntegration site > IP Address and Domain Restriction
 - "Add Allow Entry" for the ip addresses of all portal installation
 - In "Edit Feature Settings" set "Access for unspecified clients" to "Deny"
9. Change password for WDeployAdmin with current environment default
10. Set WDeployAdmin in site/IIS manager permission as "Allow user" entry
11. Add regix-EGOV-ROOT-CA (`Eumis\resources\regix-EGOV-ROOT-CA.cer`) to the Trusted Root Certification Authorities - Local Computer (Needed by Eumis.PortalIntegration.Host to connect to SimevServer)
12. Access to RegiX 
 - Create certificate signing request according to http://regixaisweb.egov.bg/RegiXInfo/RegiXGuides/#!Documents/sslwindowscertificatestore.htm
 - Install received certificate chain from DAEU http://regixaisweb.egov.bg/RegiXInfo/RegiXGuides/#!Documents/sslregixwindowscertificatestore.htm
 - Change attribute 'system.serviceModel/behaviors/endpointBehaviors/behavior/clientCredentials/clientCertificate - findValue' in 'integrations_src/Eumis.IntegrationRegiX.Host/Web.config' with the thumbprint of received transport certificate.
 - If needed, change the endpoint address of RegiX service in 'integrations_src/Eumis.IntegrationRegiX.Host/Web.config'
