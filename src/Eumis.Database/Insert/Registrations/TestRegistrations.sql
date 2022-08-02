PRINT 'Insert Registrations'
GO

SET IDENTITY_INSERT [dbo].[Registrations] ON
GO

INSERT INTO [Registrations]
    ([RegistrationId], [Email]                , [FirstName], [LastName], [Phone]      , [PasswordHash]                                                         , [PasswordSalt]             , [ActivationCode], [CreateDate], [ModifyDate])
VALUES
    (1               , N'isun@isun.com'       , N'ИСУН'    , N'Тест'   , N'0888888888', N'ABIyNHO6L7Kz25WG+DqSMK3b1S0vdyfk4Jg8rVaDNIecOcw9b9v11w2jI2tasvfpPQ==', N'rYQCvEmLmQBla59wepaPGA==', NULL            , GETDATE()   , GETDATE()   )

SET IDENTITY_INSERT [dbo].[Registrations] OFF
GO
