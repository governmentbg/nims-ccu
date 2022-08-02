PRINT 'Insert MapNodeInstitutions'
GO

INSERT INTO [MapNodeInstitutions]
    ([MapNodeId], [InstitutionId], [InstitutionTypeId] , [ContactName]       , [ContactPosition], [ContactPhone]       , [ContactFax], [ContactEmail])
VALUES
    (1            , 1              , 3                   , N'Име на контакт 1' , N'Длъжност 1'    , N'Телефонен номер 2' , N'Факс 2'   , N'Е-mail 1'   ),
    (1            , 2              , 2                   , N'Име на контакт 2' , N'Длъжност 2'    , N'Телефонен номер 3' , N'Факс 1'   , N'Е-mail 2'   ),
    (1            , 3              , 1                   , N'Име на контакт 3' , N'Длъжност 3'    , N'Телефонен номер 1' , N'Факс 3'   , N'Е-mail 3'   )
GO
