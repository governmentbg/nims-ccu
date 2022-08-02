PRINT 'Insert Notification events'
GO
SET IDENTITY_INSERT [dbo].[NotificationEvents] ON
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (1, N'Корекция на индикатори', N'On indicator correction', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (2, N'Корекция на данни от ОП', N'On data correction for OP', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (3, N'Промяна статуса на процедура от Активна в Чернова', N'On procedure status change from Active to Draft', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (4, N'Подадено проектно предложение', N'On project submission', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (5, N'Нова версия на процедура за избора на изпълнител', N'On new procurement activation', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (6, N'Нова версия на план за разходване на средства', N'On new spending plan activation', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (7, N'Получаване на нова кореспонденция', N'On new communication received', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (8, N'Получаване подаване на ПОД', N'On contract report change status to sent unchecked', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (9, N'Повторно изпращане на искане за плащане', N'On contract report payment re-sent', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (10, N'Връщане на искане за плащане към ПОД', N'On contract report payment status to returned', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (11, N'Връщане на финансов отчет към ПОД', N'On contract report financial status to returned', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (12, N'Повторно изпращане на финансов отчет към ПОД', N'On contract report financial re-sent', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (13, N'Връщане на технически отчет към ПОД', N'On contract report technical status to returned', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (14, N'Повторно изпращане на технически отчет към ПОД', N'On contract report technical re-sent', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (15, N'Връщане на микроданни към ПОД', N'On contract report microdata status to returned', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (16, N'Повторно изпращане на микроданни към ПОД', N'On contract report microdata re-sent', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (17, N'Подаден доклад по сертификaция', N'On submit cert report', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (18, N'Връщане на доклад по сертификация', N'On return cert report ', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (19, N'Получена комуникация със сертифициращ орган', N'On cert authority communication received', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (20, N'Промяна на статуса на пакет заявки от чернова на въведен', N'On request package status to entered', 0, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (21, N'Промяна на статуса на пакет заявки от въведен на проверен', N'On request package status to checked', 0, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (22, N'Промяна на статуса на пакет заявки от проверен на чернова', N'On request package status to draft', 0, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (23, N'Върнат отговор по проектно предложение от кандидат', N'On project candidate submit answer', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (24, N'Промяна на статуса на оценителен лист от Прекъснат на Продължена оценка', N'On eval sheet status change', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (25, N'Получена комуникация с одитиращ орган', N'On audit authority communication received', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (26, N'Насочване на контролен лист', N'On check sheet redirect', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (27,  N'Промяна статуса на процедура от Проверена в Активна', N'On procedure status change from Checked to Activated', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (28, N'Връщане на контролен лист', N'On check sheet return', 1, GETDATE())
GO

SET IDENTITY_INSERT [dbo].[NotificationEvents] OFF
GO
