PRINT 'Insert Notification events'
GO

SET IDENTITY_INSERT [dbo].[NotificationEvents] ON
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (26, N'Насочване на контролен лист', N'On check sheet redirect', 1, GETDATE())
GO
SET IDENTITY_INSERT [dbo].[NotificationEvents] OFF
GO
