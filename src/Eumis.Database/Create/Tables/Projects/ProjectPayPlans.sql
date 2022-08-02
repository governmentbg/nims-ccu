PRINT 'ProjectPayPlans'
GO

CREATE TABLE [dbo].[ProjectPayPlans] (
    [ProjectPayPlanId]  INT             NOT NULL IDENTITY,
    [ProjectId]         INT             NOT NULL,
    [Name]              NVARCHAR(MAX)   NOT NULL,
    [StartDate]         DATETIME2       NOT NULL,
    [EndDate]           DATETIME2       NOT NULL,
    [Amount]            MONEY           NOT NULL,

    CONSTRAINT [PK_ProjectPayPlans]               PRIMARY KEY ([ProjectPayPlanId]),
    CONSTRAINT [FK_ProjectPayPlans_Projects]      FOREIGN KEY ([ProjectId])       REFERENCES [dbo].[Projects] ([ProjectId])
);
GO

exec spDescTable  N'ProjectPayPlans', N'План за плащане на проектно предложение.'
exec spDescColumn N'ProjectPayPlans', N'ProjectPayPlanId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectPayPlans', N'ProjectId'             , N'Идентификатор на проектно предложние'
exec spDescColumn N'ProjectPayPlans', N'Name'                  , N'Описание'
exec spDescColumn N'ProjectPayPlans', N'StartDate'             , N'Начална дата '
exec spDescColumn N'ProjectPayPlans', N'EndDate'               , N'Крайна дата '
exec spDescColumn N'ProjectPayPlans', N'Amount'                , N'Стойност.'

GO

