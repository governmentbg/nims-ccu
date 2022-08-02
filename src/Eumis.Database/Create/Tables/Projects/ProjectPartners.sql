PRINT 'ProjectPartners'
GO

CREATE TABLE [dbo].[ProjectPartners] (
    [ProjectId]                             INT             NOT NULL IDENTITY,
    [CompanyId]                             INT             NOT NULL ,
    CONSTRAINT [PK_ProjectPartners]             PRIMARY KEY ([ProjectId], [CompanyId]),
    CONSTRAINT [FK_ProjectPartners_Projects]   FOREIGN KEY ([ProjectId])           REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_ProjectPartners_Companies]   FOREIGN KEY ([CompanyId])           REFERENCES [dbo].[Companies] ([CompanyId])
);
GO

exec spDescTable  N'ProjectPartners', N'Партньори по Проектно предложение.'
exec spDescColumn N'ProjectPartners', N'ProjectId'                             , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectPartners', N'CompanyId'                             , N'Идентификатор на компанията партньор.'

GO


