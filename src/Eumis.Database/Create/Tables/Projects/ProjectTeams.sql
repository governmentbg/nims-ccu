PRINT 'ProjectTeams'
GO

CREATE TABLE [dbo].[ProjectTeams] (
    [ProjectTeamId]         INT             NOT NULL IDENTITY,
    [ProjectId]             INT             NOT NULL,
    [Name]                  NVARCHAR(MAX)   NOT NULL,
    [Position]              NVARCHAR(50)    NOT NULL,
    [Responsibilities]      NVARCHAR(MAX)   NOT NULL,
    [Phone]                 NVARCHAR(50)    NOT NULL,
    [Fax]                   NVARCHAR(50)    NULL,
    [Email]                 NVARCHAR(50)    NOT NULL,

    CONSTRAINT [PK_ProjectTeams]                PRIMARY KEY ([ProjectTeamId]),
    CONSTRAINT [FK_ProjectTeams_Projects]       FOREIGN KEY ([ProjectId])       REFERENCES [dbo].[Projects] ([ProjectId])
);
GO

exec spDescTable  N'ProjectTeams', N'Екип на проектно предложение.'
exec spDescColumn N'ProjectTeams', N'ProjectTeamId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectTeams', N'ProjectId'             , N'Идентификатор на проектно предложние.'
exec spDescColumn N'ProjectTeams', N'Name'                  , N'Име.'
exec spDescColumn N'ProjectTeams', N'Position'              , N'Позиция по проекта.'
exec spDescColumn N'ProjectTeams', N'Responsibilities'      , N'Отговорности.'
exec spDescColumn N'ProjectTeams', N'Phone'                 , N'Телефон.'
exec spDescColumn N'ProjectTeams', N'Fax'                   , N'Факс.'
exec spDescColumn N'ProjectTeams', N'Email'                 , N'Ел. адрес.'

GO

