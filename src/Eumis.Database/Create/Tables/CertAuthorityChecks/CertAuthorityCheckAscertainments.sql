PRINT 'CertAuthorityCheckAscertainments'
GO

CREATE TABLE [dbo].[CertAuthorityCheckAscertainments] (
    [CertAuthorityCheckAscertainmentId] INT               NOT NULL IDENTITY,
    [CertAuthorityCheckId]              INT               NOT NULL,
    [Ascertainment]                     NVARCHAR(MAX)     NOT NULL,
    [OrderNum]                          INT               NOT NULL,
    [Status]                            INT               NOT NULL,
    [Type]                              INT               NOT NULL,

    [Recommendation]                    NVARCHAR(MAX)     NULL,
    [RecommendationDeadline]            DATETIME2         NULL,
    [RecommendationExecutionStatus]     INT               NULL,

    [CertAuthorityComment]              NVARCHAR(MAX)     NULL,
    [ManagingAuthorityComment]          NVARCHAR(MAX)     NULL,

    CONSTRAINT [PK_CertAuthorityCheckAscertainments]               PRIMARY KEY ([CertAuthorityCheckAscertainmentId]),
    CONSTRAINT [FK_CertAuthorityCheckAscertainments_CertAuthorityChecks]    FOREIGN KEY ([CertAuthorityCheckId])      REFERENCES [dbo].[CertAuthorityChecks] ([CertAuthorityCheckId]),
    CONSTRAINT [CHK_CertAuthorityCheckAscertainments_Type]         CHECK ([Type]  IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_CertAuthorityCheckAscertainments_Status]       CHECK ([Status] IN (1, 2, 3)),
    CONSTRAINT [CHK_CertAuthorityCheckAscertainments_РStatus]      CHECK ([RecommendationExecutionStatus] IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'CertAuthorityCheckAscertainments', N'Констатации към проверки на СО.'
exec spDescColumn N'CertAuthorityCheckAscertainments', N'CertAuthorityCheckAscertainmentId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CertAuthorityCheckAscertainments', N'CertAuthorityCheckId'             , N'Идентификатор на проверка на СО.'
exec spDescColumn N'CertAuthorityCheckAscertainments', N'Ascertainment'                    , N'Констатация.'
exec spDescColumn N'CertAuthorityCheckAscertainments', N'OrderNum'                         , N'Пореден номер.'
exec spDescColumn N'CertAuthorityCheckAscertainments', N'Status'                           , N'Статус на констатацията: 1 – Отворена, 2 - Затворена, 3 - За информация.'
exec spDescColumn N'CertAuthorityCheckAscertainments', N'Type'                             , N'Вид на констатацията: 1 - Съществена, 2 – Второстепенна, 3 – Несъществена, 4 - Информативна.'

exec spDescColumn N'CertAuthorityCheckAscertainments', N'Recommendation'                   , N'Препоръка.'
exec spDescColumn N'CertAuthorityCheckAscertainments', N'RecommendationDeadline'           , N'Срок за изпълнение на препоръката.'
exec spDescColumn N'CertAuthorityCheckAscertainments', N'RecommendationExecutionStatus'    , N'Статус на изпълнение на препоръката: 1 – изпълнена, 2 – в процес на изпълнение, 3 – неизпълнена, 4 - оттеглена.'

exec spDescColumn N'CertAuthorityCheckAscertainments', N'CertAuthorityComment'             , N'Коментар/становище на СО.'
exec spDescColumn N'CertAuthorityCheckAscertainments', N'ManagingAuthorityComment'         , N'Коментар/становище на УО.'
GO
