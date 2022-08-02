PRINT 'InvestmentPriorities'
GO

CREATE TABLE [dbo].[InvestmentPriorities] (
    [InvestmentPriorityId]      INT                 NOT NULL IDENTITY,
    [InterventionCategoryId]    INT                 NOT NULL,
    [Code]                      NVARCHAR(200)       NOT NULL,
    [Name]                      NVARCHAR(MAX)       NOT NULL,
    [NameAlt]                   NVARCHAR(MAX)           NULL,

    CONSTRAINT [PK_InvestmentPriorities]                        PRIMARY KEY ([InvestmentPriorityId]),
    CONSTRAINT [FK_InvestmentPriorities_InterventionCategories] FOREIGN KEY ([InterventionCategoryId])     REFERENCES [dbo].InterventionCategories ([InterventionCategoryId]),
    CONSTRAINT [UQ_InvestmentPriorities]                        UNIQUE ([InterventionCategoryId], [Code])
);
GO

exec spDescTable  N'InvestmentPriorities', N'Инвестиционни приоритети.'
exec spDescColumn N'InvestmentPriorities', N'InvestmentPriorityId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'InvestmentPriorities', N'InterventionCategoryId'    , N'Идентификатор на тематична цел.'
exec spDescColumn N'InvestmentPriorities', N'Code'                      , N'Код.'
exec spDescColumn N'InvestmentPriorities', N'Name'                      , N'Наименование.'
exec spDescColumn N'InvestmentPriorities', N'NameAlt'                      , N'Наименование на английски.'
GO
