PRINT 'SpotCheckAscertainments'
GO

CREATE TABLE [dbo].[SpotCheckAscertainments] (
    [SpotCheckAscertainmentId]         INT               NOT NULL IDENTITY,
    [SpotCheckId]                      INT               NOT NULL,
    [OrderNumber]                      INT               NOT NULL,

    [Ascertainment]                    NVARCHAR(MAX)     NOT NULL,
    [Status]                           INT               NOT NULL,
    [Type]                             INT               NOT NULL,
    [CheckSubjectComment]              NVARCHAR(MAX)     NULL,
    [ManagingAuthorityComment]         NVARCHAR(MAX)     NULL,

    CONSTRAINT [PK_SpotCheckAscertainments]                PRIMARY KEY ([SpotCheckAscertainmentId]),
    CONSTRAINT [FK_SpotCheckAscertainments_SpotChecks]     FOREIGN KEY ([SpotCheckId])      REFERENCES [dbo].[SpotChecks] ([SpotCheckId]),
    CONSTRAINT [CHK_SpotCheckAscertainments_Type]          CHECK ([Type]  IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_SpotCheckAscertainments_Status]        CHECK ([Status] IN (1, 2, 3))
);
GO

exec spDescTable  N'SpotCheckAscertainments', N'Констатации към проверки на място.'
exec spDescColumn N'SpotCheckAscertainments', N'SpotCheckAscertainmentId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SpotCheckAscertainments', N'SpotCheckId'                     , N'Идентификатор на проверка на място.'
exec spDescColumn N'SpotCheckAscertainments', N'OrderNumber'                     , N'Пореден номер.'
 
exec spDescColumn N'SpotCheckAscertainments', N'Ascertainment'                   , N'Констатация.'
exec spDescColumn N'SpotCheckAscertainments', N'Status'                          , N'Статус на констатацията: 1 – отворена, 2 - затворена, 3- за информация.'
exec spDescColumn N'SpotCheckAscertainments', N'Type'                            , N'Вид на констатацията: 1 - съществена, 2 – второстепенна, 3 – несъществена, 4 - информативна.'
exec spDescColumn N'SpotCheckAscertainments', N'CheckSubjectComment'             , N'Коментар/становище на проверявания.'
exec spDescColumn N'SpotCheckAscertainments', N'ManagingAuthorityComment'        , N'Коментар/становище на УО.'
GO
