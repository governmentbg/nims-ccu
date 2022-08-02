PRINT 'SpotCheckRecommendations'
GO

CREATE TABLE [dbo].[SpotCheckRecommendations] (
    [SpotCheckRecommendationId]  INT               NOT NULL IDENTITY,
    [SpotCheckId]                INT               NOT NULL,
    [OrderNumber]                INT               NOT NULL,

    [Recommendation]             NVARCHAR(MAX)     NOT NULL,
    [Deadline]                   DATETIME2         NULL,
    [ExecutionStatus]            INT               NULL,
    [StatusDate]                 DATETIME2         NULL,
    [ExecutionDate]              DATETIME2         NULL,
    [ExecutionProofDate]         DATETIME2         NULL,

    CONSTRAINT [PK_SpotCheckRecommendations]               PRIMARY KEY ([SpotCheckRecommendationId]),
    CONSTRAINT [FK_SpotCheckRecommendations_SpotChecks]    FOREIGN KEY ([SpotCheckId])      REFERENCES [dbo].[SpotChecks] ([SpotCheckId]),
    CONSTRAINT [CHK_SpotCheckRecommendations_РStatus]      CHECK ([ExecutionStatus] IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'SpotCheckRecommendations', N'Препоръки към проверка на място.'
exec spDescColumn N'SpotCheckRecommendations', N'SpotCheckRecommendationId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SpotCheckRecommendations', N'SpotCheckId'              , N'Идентификатор на проверка на място.'
exec spDescColumn N'SpotCheckRecommendations', N'OrderNumber'              , N'Пореден номер.'

exec spDescColumn N'SpotCheckRecommendations', N'Recommendation'           , N'Препоръка.'
exec spDescColumn N'SpotCheckRecommendations', N'Deadline'                 , N'Срок за изпълнение на препоръката.'
exec spDescColumn N'SpotCheckRecommendations', N'ExecutionStatus'          , N'Статус на изпълнение на препоръката: 1 – изпълнена, 2 – в процес на изпълнение, 3 – неизпълнена; 4 - отпаднала.'
exec spDescColumn N'SpotCheckRecommendations', N'StatusDate'               , N'Дата на въвеждане на статуса на изпълнение на препоръката.'
exec spDescColumn N'SpotCheckRecommendations', N'ExecutionDate'            , N'Дата на изпълнение на препоръката.'
exec spDescColumn N'SpotCheckRecommendations', N'ExecutionProofDate'       , N'Дата на получаване на доказателствата за изпълнение на препоръката'
GO
