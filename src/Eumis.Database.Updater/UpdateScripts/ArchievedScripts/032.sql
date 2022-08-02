GO

-- Allowances
CREATE TABLE [dbo].[Allowances] (
    [AllowanceId]          INT                 NOT NULL IDENTITY,
    [Gid]                  UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Name]                 NVARCHAR(100)       NOT NULL,
    [CreateDate]           DATETIME2           NOT NULL,
    [ModifyDate]           DATETIME2           NOT NULL,
    [Version]              ROWVERSION          NOT NULL,

    CONSTRAINT [PK_Allowances] PRIMARY KEY ([AllowanceId]),
);
GO

exec spDescTable  N'Allowances', N'Надбавки.'
exec spDescColumn N'Allowances', N'AllowanceId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Allowances', N'Gid'             , N'Глобален уникален идентификатор.'
exec spDescColumn N'Allowances', N'Name'            , N'Наименование.'
exec spDescColumn N'Allowances', N'CreateDate'      , N'Дата на създаване на записа.'
exec spDescColumn N'Allowances', N'ModifyDate'      , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Allowances', N'Version'         , N'Версия.'
GO

CREATE TABLE [dbo].[AllowanceRates] (
    [AllowanceRateId]      INT                 NOT NULL IDENTITY,
    [AllowanceId]          INT                 NOT NULL,
    [Date]                 DATETIME2           NOT NULL,
    [Rate]                 DECIMAL(15,3)       NOT NULL,

    CONSTRAINT [PK_AllowanceRates]               PRIMARY KEY ([AllowanceRateId]),
    CONSTRAINT [FK_AllowanceRates_Allowances]    FOREIGN KEY ([AllowanceId])      REFERENCES [dbo].[Allowances] ([AllowanceId])
);
GO

exec spDescTable  N'AllowanceRates', N'Проценти към надбавка.'
exec spDescColumn N'AllowanceRates', N'AllowanceRateId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'AllowanceRates', N'AllowanceId'        , N'Идентификатор на надбавка.'
exec spDescColumn N'AllowanceRates', N'Date'               , N'Дата.'
exec spDescColumn N'AllowanceRates', N'Rate'               , N'Процент.'
GO


-- BasicInterestRates
CREATE TABLE [dbo].[BasicInterestRates] (
    [BasicInterestRateId]  INT                 NOT NULL IDENTITY,
    [Gid]                  UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Name]                 NVARCHAR(100)       NOT NULL,
    [CreateDate]           DATETIME2           NOT NULL,
    [ModifyDate]           DATETIME2           NOT NULL,
    [Version]              ROWVERSION          NOT NULL,

    CONSTRAINT [PK_BasicInterestRates] PRIMARY KEY ([BasicInterestRateId]),
);
GO

exec spDescTable  N'BasicInterestRates', N'Основен лихвен процент.'
exec spDescColumn N'BasicInterestRates', N'BasicInterestRateId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'BasicInterestRates', N'Gid'                     , N'Глобален уникален идентификатор.'
exec spDescColumn N'BasicInterestRates', N'Name'                    , N'Наименование.'
exec spDescColumn N'BasicInterestRates', N'CreateDate'              , N'Дата на създаване на записа.'
exec spDescColumn N'BasicInterestRates', N'ModifyDate'              , N'Дата на последно редактиране на записа.'
exec spDescColumn N'BasicInterestRates', N'Version'                 , N'Версия.'
GO

CREATE TABLE [dbo].[InterestRates] (
    [InterestRateId]       INT                 NOT NULL IDENTITY,
    [BasicInterestRateId]  INT                 NOT NULL,
    [Date]                 DATETIME2           NOT NULL,
    [Rate]                 DECIMAL(15,3)       NOT NULL,

    CONSTRAINT [PK_InterestRates]                       PRIMARY KEY ([InterestRateId]),
    CONSTRAINT [FK_InterestRates_BasicInterestRates]    FOREIGN KEY ([BasicInterestRateId])   REFERENCES [dbo].[BasicInterestRates] ([BasicInterestRateId])
);
GO

exec spDescTable  N'InterestRates', N'Проценти към основен лихвен процент.'
exec spDescColumn N'InterestRates', N'InterestRateId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'InterestRates', N'BasicInterestRateId', N'Идентификатор на основен лихвен процент.'
exec spDescColumn N'InterestRates', N'Date'               , N'Дата.'
exec spDescColumn N'InterestRates', N'Rate'               , N'Процент.'
GO


-- InterestSchemes
CREATE TABLE [dbo].[InterestSchemes] (
    [InterestSchemeId]    INT                 NOT NULL IDENTITY,
    [Gid]                  UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Name]                 NVARCHAR(100)       NOT NULL,
    [BasicInterestRateId]  INT                 NOT NULL,
    [AllowanceId]          INT                 NOT NULL,
    [AnnualBasis]          INT                 NOT NULL,
    [IsActive]             BIT                 NOT NULL,
    [CreateDate]           DATETIME2           NOT NULL,
    [ModifyDate]           DATETIME2           NOT NULL,
    [Version]              ROWVERSION          NOT NULL,

    CONSTRAINT [PK_InterestSchemes]                      PRIMARY KEY ([InterestSchemeId]),
    CONSTRAINT [FK_InterestSchemes_BasicInterestRates]   FOREIGN KEY ([BasicInterestRateId])  REFERENCES [dbo].[BasicInterestRates] ([BasicInterestRateId]),
    CONSTRAINT [FK_InterestSchemes_Allowances]           FOREIGN KEY ([AllowanceId])          REFERENCES [dbo].[Allowances]         ([AllowanceId])
);
GO

exec spDescTable  N'InterestSchemes', N'Схеми за олихвяване.'
exec spDescColumn N'InterestSchemes', N'InterestSchemeId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'InterestSchemes', N'Gid'                    , N'Глобален уникален идентификатор.'
exec spDescColumn N'InterestSchemes', N'Name'                   , N'Наименование.'
exec spDescColumn N'InterestSchemes', N'BasicInterestRateId'    , N'Идентификатор на основен лихвен процент.'
exec spDescColumn N'InterestSchemes', N'AllowanceId'            , N'Идентификатор на надбавка.'
exec spDescColumn N'InterestSchemes', N'AnnualBasis'            , N'Годишна база.'
exec spDescColumn N'InterestSchemes', N'IsActive'               , N'Маркер за активност.'
exec spDescColumn N'InterestSchemes', N'CreateDate'             , N'Дата на създаване на записа.'
exec spDescColumn N'InterestSchemes', N'ModifyDate'             , N'Дата на последно редактиране на записа.'
exec spDescColumn N'InterestSchemes', N'Version'                , N'Версия.'
GO