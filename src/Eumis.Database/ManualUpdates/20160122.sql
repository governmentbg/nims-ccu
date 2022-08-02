--executed on 2016/01/22 ~ 09:00
UPDATE [Eumis].[dbo].[Users]
SET [UserOrganizationId] = 
	(SELECT [UserOrganizationId]
	 FROM [Eumis].[dbo].[UserOrganizations]
	 WHERE [Name] = N'ОПНОИР')
WHERE [Username] = 'kpenevska' AND [Gid] = '63FDD7AE-0891-43C0-9FBC-3EAC26FAA2A0' AND [Uin] = '8508133050'

UPDATE [Eumis].[dbo].[Users]
SET [UserOrganizationId] = 
	(SELECT [UserOrganizationId]
	 FROM [Eumis].[dbo].[UserOrganizations]
	 WHERE [Name] = N'ОПНОИР')
WHERE [Username] = 'v.stanchev' AND [Gid] = '8940336C-8E17-4506-9A9D-7F4C1B56FB57' AND [Uin] = '8501052866'



UPDATE [Eumis].[dbo].[Users]
SET [UserTypeId] = 
	(SELECT [UserTypeId]
	 FROM [Eumis].[dbo].[UserTypes]
	 WHERE [Name] = N'Потребител от ОПНОИР' AND [UserOrganizationId] = 
		(SELECT [UserOrganizationId]
		 FROM [Eumis].[dbo].[UserOrganizations]
		 WHERE [Name] = N'ОПНОИР'))
WHERE [Username] = 'kpenevska' AND [Gid] = '63FDD7AE-0891-43C0-9FBC-3EAC26FAA2A0' AND [Uin] = '8508133050'

UPDATE [Eumis].[dbo].[Users]
SET [UserTypeId] = 
	(SELECT [UserTypeId]
	 FROM [Eumis].[dbo].[UserTypes]
	 WHERE [Name] = N'Потребител от ОПНОИР' AND [UserOrganizationId] = 
		(SELECT [UserOrganizationId]
		 FROM [Eumis].[dbo].[UserOrganizations]
		 WHERE [Name] = N'ОПНОИР'))
WHERE [Username] = 'v.stanchev' AND [Gid] = '8940336C-8E17-4506-9A9D-7F4C1B56FB57' AND [Uin] = '8501052866'
