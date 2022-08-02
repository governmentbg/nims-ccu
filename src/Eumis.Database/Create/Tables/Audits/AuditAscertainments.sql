PRINT 'AuditAscertainments'
GO

CREATE TABLE [dbo].[AuditAscertainments] (
    [AuditAscertainmentId]      INT               NOT NULL IDENTITY,
    [AuditId]                   INT               NOT NULL,
    [Ascertainment]             NVARCHAR(MAX)     NOT NULL,
    [Recommendation]            NVARCHAR(MAX)     NULL,
    [RecommendationsFulfilled]  BIT               NULL,

    [IsFinancial]               BIT               NOT NULL,
    [FinancialSum]              MONEY             NULL,

    [OrderNum]                  INT               NOT NULL,

    CONSTRAINT [PK_AuditAscertainments]         PRIMARY KEY ([AuditAscertainmentId]),
    CONSTRAINT [FK_AuditAscertainments_Audits]  FOREIGN KEY ([AuditId])   REFERENCES [dbo].[Audits] ([AuditId])
);
GO

exec spDescTable  N'AuditAscertainments', N'Констатации към одити.'
exec spDescColumn N'AuditAscertainments', N'AuditAscertainmentId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'AuditAscertainments', N'AuditId'                 , N'Идентификатор на одит.'
exec spDescColumn N'AuditAscertainments', N'Ascertainment'           , N'Констатация.'
exec spDescColumn N'AuditAscertainments', N'Recommendation'          , N'Препоръка.'
exec spDescColumn N'AuditAscertainments', N'RecommendationsFulfilled', N'Изпълнени ли са препоръките.'
exec spDescColumn N'AuditAscertainments', N'IsFinancial'             , N'Финансово изражение.'
exec spDescColumn N'AuditAscertainments', N'FinancialSum'            , N'Сума.'
exec spDescColumn N'AuditAscertainments', N'OrderNum'                , N'Пореден номер.'
GO