ALTER TABLE [dbo].[ProjectMonitorstatRequests] ADD
   [CompanyUin]       NVARCHAR(200)  NULL,
   [CompanyUinType]   INT            NULL,
CONSTRAINT [CHK_ProjectMonitorstatRequests_CompanyUinType] CHECK ([CompanyUinType] IN (0, 1, 2, 3))
