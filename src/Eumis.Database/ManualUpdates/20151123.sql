--executed on 2015/11/23 ~ 17:10
UPDATE [Eumis].[dbo].[Users]
SET [UserOrganizationId] = 
	(SELECT [UserOrganizationId]
	 FROM [Eumis].[dbo].[UserOrganizations]
	 WHERE [Name] = N'ОПХ')
WHERE [Username] = 'e.dimitrova' AND [Gid] = 'BF3A19BC-2F77-4573-903D-EE806B0333F6' AND [Uin] = '7909302250'

UPDATE [Eumis].[dbo].[Users]
SET [UserOrganizationId] = 
	(SELECT [UserOrganizationId]
	 FROM [Eumis].[dbo].[UserOrganizations]
	 WHERE [Name] = N'ОПХ')
WHERE [Username] = 'b.karakasheva' AND [Gid] = '11A8A2EB-DE22-4E9C-84CA-EF0B7420D5C4' AND [Uin] = '8710227096'



UPDATE [Eumis].[dbo].[Users]
SET [UserTypeId] = 
	(SELECT [UserTypeId]
	 FROM [Eumis].[dbo].[UserTypes]
	 WHERE [Name] = N'Потребител от ОПХ' AND [UserOrganizationId] = 
		(SELECT [UserOrganizationId]
		 FROM [Eumis].[dbo].[UserOrganizations]
		 WHERE [Name] = N'ОПХ'))
WHERE [Username] = 'e.dimitrova' AND [Gid] = 'BF3A19BC-2F77-4573-903D-EE806B0333F6' AND [Uin] = '7909302250'

UPDATE [Eumis].[dbo].[Users]
SET [UserTypeId] = 
	(SELECT [UserTypeId]
	 FROM [Eumis].[dbo].[UserTypes]
	 WHERE [Name] = N'Администратор от ОПХ' AND [UserOrganizationId] = 
		(SELECT [UserOrganizationId]
		 FROM [Eumis].[dbo].[UserOrganizations]
		 WHERE [Name] = N'ОПХ'))
WHERE [Username] = 'b.karakasheva' AND [Gid] = '11A8A2EB-DE22-4E9C-84CA-EF0B7420D5C4' AND [Uin] = '8710227096'
