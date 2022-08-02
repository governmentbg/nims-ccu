GO
SET IDENTITY_INSERT [dbo].[NotificationEvents] ON
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (27,  N'Промяна статуса на процедура от Проверена в Активна', N'On procedure status change from Checked to Activated', 1, GETDATE())
GO
SET IDENTITY_INSERT [dbo].[NotificationEvents] OFF
GO


GO
SET IDENTITY_INSERT [dbo].[NotificationEventPermissions] ON
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (56, 27, N'CanRead', N'Procedure', GETDATE())
GO
SET IDENTITY_INSERT [dbo].[NotificationEventPermissions] OFF
GO
