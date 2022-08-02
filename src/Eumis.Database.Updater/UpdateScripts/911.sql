GO
delete from NotificationEventPermissions where NotificationEventId in (15, 16, 17, 18, 19, 25)
GO
delete from NotificationEvents where NotificationEventId in (15, 16, 17, 18, 19, 25, 26, 28)
GO
Update NotificationEvents Set Name = 'Корекция на данни от OO' where NotificationEventId = 2
GO
Update NotificationEvents Set Name = 'Промяна статуса на бюджет от Активен в Чернова' where NotificationEventId = 3
GO
Update NotificationEvents Set Name = 'Промяна статуса на Бюджет от Проверен в Активен' where NotificationEventId = 27
GO


GO

ALTER TABLE [dbo].[ProjectCommunications] DROP CONSTRAINT [CHK_ProjectCommunications_Subject]
GO
ALTER TABLE [dbo].[ProjectCommunications] WITH CHECK ADD CONSTRAINT [CHK_ProjectCommunications_Subject] CHECK ([Subject]   IN (1, 2, 3, 4))
GO