GO
ALTER TABLE [dbo].[CheckSheets] 
ADD [LastModifiedByUserId] INT NULL,
CONSTRAINT [FK_CheckSheets_LastModifiedByUser]          FOREIGN KEY ([LastModifiedByUserId])          REFERENCES [dbo].[Users] ([UserId]);

GO

PRINT 'Insert Notification events'
GO

SET IDENTITY_INSERT [dbo].[NotificationEvents] ON
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (28, N'Връщане на контролен лист', N'On check sheet return', 1, GETDATE())
GO
SET IDENTITY_INSERT [dbo].[NotificationEvents] OFF
GO
