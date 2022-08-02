PRINT 'Insert Users'
GO

DECLARE @suTemplate NVARCHAR(MAX);
SELECT @suTemplate = PermissionsString FROM PermissionTemplates WHERE Name = N'Всички права';

SET IDENTITY_INSERT [Users] ON

INSERT INTO [Users]
    ([UserId], [Gid]                                 , [UserTypeId], [UserOrganizationId], [IsSystem], [Username], [PasswordHash]                                                         , [PasswordSalt]             , [Uin]        , [Fullname]             , [Email]            , [IsActive], [IsDeleted], [IsLocked], [CreateDate], [ModifyDate], [PermissionTemplate], [FailedAttempts])
VALUES
    (1       , '3A459B75-AC1C-40EB-A50E-7A5993850906', 1           , 1                   , 1         , N'system' , NULL                                                                   , NULL                       , N'1212927890', N'Системен потребител' , N''                , 1         , 0          , 0         , GETDATE()    ,GETDATE()   , N''                 , 0               ),
    (2       , '72D84827-31C0-47FA-B7A0-10D3F4C348E8', 1           , 1                   , 0         , N'admin'  , N'AMWr4Peeajc9Q0bsdV7mWBwK6fLJN/Cr/ksp2jV4M50RQ587JQHptHzA7HLDOLSHGg==', N'88SHIJlJUThYeUFDUQKJoQ==', N'1502784562', N'Администратор'       , N'admin@isun.com'  , 1         , 0          , 0         , GETDATE()    ,GETDATE()   , @suTemplate         , 0               ),
    (3       , 'D148653E-BB25-49A9-9072-4F0271316BB5', 1           , 1                   , 0         , N'uis'    , N'ABIyNHO6L7Kz25WG+DqSMK3b1S0vdyfk4Jg8rVaDNIecOcw9b9v11w2jI2tasvfpPQ==', N'rYQCvEmLmQBla59wepaPGA==', N'0612887525', N'Администратор ЦКЗ'   , N'uis@isun.com'    , 1         , 0          , 0         , GETDATE()    ,GETDATE()   , @suTemplate         , 0               )

SET IDENTITY_INSERT [Users] OFF
GO
