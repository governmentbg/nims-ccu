PRINT 'SpotCheckRecommendationAscertainments'
GO

CREATE TABLE [dbo].[SpotCheckRecommendationAscertainments] (
    [SpotCheckRecommendationAscertainmentId] INT               NOT NULL IDENTITY,
    [SpotCheckAscertainmentId]               INT               NOT NULL,
    [SpotCheckRecommendationId]              INT               NOT NULL,

    CONSTRAINT [PK_SpotCheckRecommendationAscertainments]                          PRIMARY KEY ([SpotCheckRecommendationAscertainmentId]),
    CONSTRAINT [FK_SpotCheckRecommendationAscertainments_SpotCheckAscertainments]  FOREIGN KEY ([SpotCheckAscertainmentId])      REFERENCES [dbo].[SpotCheckAscertainments]  ([SpotCheckAscertainmentId]),
    CONSTRAINT [FK_SpotCheckRecommendationAscertainments_SpotCheckRecommendations] FOREIGN KEY ([SpotCheckRecommendationId])     REFERENCES [dbo].[SpotCheckRecommendations] ([SpotCheckRecommendationId])
);
GO

exec spDescTable  N'SpotCheckRecommendationAscertainments', N'Констатации към препоръка.'
exec spDescColumn N'SpotCheckRecommendationAscertainments', N'SpotCheckRecommendationAscertainmentId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SpotCheckRecommendationAscertainments', N'SpotCheckAscertainmentId'              , N'Идентификатор на констатация.'
exec spDescColumn N'SpotCheckRecommendationAscertainments', N'SpotCheckRecommendationId'             , N'Идентификатор на препоръка.'
GO
