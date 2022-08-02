GO

update ActionLogs
set PostData=null
where action in (
'Users.ChangePassword',
'Registrations.Activate',
'Registrations.RecoverPassword',
'Registrations.ChangeCurrentUserPassword')

GO
