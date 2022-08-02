--executed on 2016/04/19 ~ 15:30

UPDATE [Eumis].[dbo].[Users]
SET [UserTypeId] = 
	(SELECT [UserTypeId]
	 FROM [Eumis].[dbo].[UserTypes]
	 WHERE [Name] = N'Администратор ЦКЗ' AND [UserOrganizationId] = 
		(SELECT [UserOrganizationId]
		 FROM [Eumis].[dbo].[UserOrganizations]
		 WHERE [Name] = N'ЦКЗ'))
WHERE [Username] = 's.dimitrova' AND [Gid] = 'DF068016-81CE-427F-909F-0AF195BBBA9B' AND [Uin] = '8602176818'


