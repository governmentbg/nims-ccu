GO

-- Extend ProcedureTypes
ALTER TABLE [dbo].[ProcedureTypes]
ADD
    [Alias]       NVARCHAR(50)                 NULL
GO

SET IDENTITY_INSERT [dbo].[ProcedureTypes] ON

INSERT INTO [ProcedureTypes]
    ([ProcedureTypeId], [Name], [Alias])
VALUES
    (9           , N'Процедура за фонд на фондовете и ЕИФ' , N'fof'),
    (10          , N'Процедура за финансови посредници'    , N'agents'),
    (11          , N'Процедура за крайни получатели'       , N'endusers')

SET IDENTITY_INSERT [dbo].[ProcedureTypes] OFF
GO

-- Extend procedures ApplicationFormType
ALTER TABLE [dbo].[Procedures]
DROP
    CONSTRAINT [CHK_Procedures_ApplicationFormType]
GO

ALTER TABLE [dbo].[Procedures]
ADD
    CONSTRAINT [CHK_Procedures_ApplicationFormType]       CHECK       ([ApplicationFormType] IN (1, 2, 3, 4, 5, 6))
GO

-- Add link between procedures
ALTER TABLE [dbo].[Procedures]
ADD
    [AttachedProcedureId]       INT                 NULL,
    CONSTRAINT [FK_Procedures_AttachedProcedures]         FOREIGN KEY ([AttachedProcedureId]) REFERENCES [dbo].[Procedures] ([ProcedureId])
GO

-- Extend ContractType
ALTER TABLE [dbo].[Contracts]
DROP
    CONSTRAINT [CHK_Contracts_ContractType]
GO

ALTER TABLE [dbo].[Contracts]
ADD
    CONSTRAINT [CHK_Contracts_ContractType]       CHECK       ([ContractType] IN (1, 2, 3, 4, 5, 6))
GO

-- Add link between contracts
ALTER TABLE [dbo].[Contracts]
ADD
    [AttachedContractId]       INT                 NULL,
    CONSTRAINT [FK_Contracts_AttachedContracts]     FOREIGN KEY ([AttachedContractId])          REFERENCES [dbo].[Contracts] ([ContractId])

GO

-- Add eval session project standing rejection reasons

CREATE TABLE [dbo].[EvalSessionProjectStandingRejectionReasons] (
    [EvalSessionProjectStandingRejectionReasonId]     INT             NOT NULL IDENTITY,
    [Name]                                            NVARCHAR(MAX)   NOT NULL,

    CONSTRAINT [PK_EvalSessionProjectStandingRejectionReasons] PRIMARY KEY ([EvalSessionProjectStandingRejectionReasonId])
);
GO

SET IDENTITY_INSERT [EvalSessionProjectStandingRejectionReasons] ON

INSERT INTO [EvalSessionProjectStandingRejectionReasons]
    ([EvalSessionProjectStandingRejectionReasonId], [Name])
VALUES
    --Липса на  административно съответствие (не е валидно за финансови инструменти). (Идентифицирано в резултат на извършена оценка на административното съответствие и допустимост (ОАСД))
    (1                                            , N'Липса на  административно съответствие - Документацията по проектното предложение не е получена в рамките на дефинирания в Насоки за кандидатстване срок'),
    (2                                            , N'Липса на  административно съответствие - Информацията не е предоставена според определените в Насоки за кандидатстване  шаблони или канали за комуникация'),
    (3                                            , N'Липса на  административно съответствие - Некоректно изготвена документация (лисващи или грешни справки, документи, атрибути)'),
    (4                                            , N'Липса на  административно съответствие - Липса на получен от бенефициента/получателя в срок отговор на поставени от комисията по оценка въпроси или допълнително изискани документи/разяснения'),
    --Недопустимост на проекта/кандидата (идентифицирано в резултат на извършена оценка на административното съответствие и допустимост (ОАСД))
    (5                                            , N'Недопустимост на проекта/кандидата - Правната форма или предметът на дейност/сектор на кандидата не отговарят на изискванията за допустимост или собствеността на дружеството не е ясна'),
    (6                                            , N'Недопустимост на проекта/кандидата - Партньорите по проекта  не отговарят на изискванията за допустимост'),
    (7                                            , N'Недопустимост на проекта/кандидата - Проектът не в съответствие с или не отговаря на приоритетите на съответната оперативна програма/приоритетна ос/инвестиционен приоритет'),
    (8                                            , N'Недопустимост на проекта/кандидата - Проектът е стартирал (или приключил) преди даването на одобрение (само когато това е в  противоречие със съответните зададени критерии)'),
    (9                                            , N'Недопустимост на проекта/кандидата - Установено е наличие на недопустимо дублиране с други проекти, финансирани по фондове от ЕС или с национални средства'),
    --Несъответствие на технически изисквания (идентифицирано в резултат на извършена Техническа и финансова оценка (ТФО)
    (10                                           , N'Несъответствие на технически изисквания - Проектът не доказва стратегическа необходимост и значителен ефект/ползи  - за самия кандидат, сектора или географския регион, в който ще се приложи проектът'),
    (11                                           , N'Несъответствие на технически изисквания - Не са формулирани ясни и постижими цели, съответстващи на изискванията на процедурата, заложени в поканата за изпращане на предложения'),
    (12                                           , N'Несъответствие на технически изисквания - Не са налице измерими и ясно формулирани крайни резултати от проекта')


SET IDENTITY_INSERT [EvalSessionProjectStandingRejectionReasons] OFF
GO

ALTER TABLE [dbo].[EvalSessionProjectStandings] ADD
    [RejectionReasonId]             INT                NULL,
    CONSTRAINT [FK_EvalSessionProjectStandings_RejectionReasons]      FOREIGN KEY ([RejectionReasonId])                           REFERENCES [dbo].[EvalSessionProjectStandingRejectionReasons] ([EvalSessionProjectStandingRejectionReasonId])
GO

CREATE TABLE [dbo].[FIReimbursedAmounts] (
    [FIReimbursedAmountId]        INT           NOT NULL IDENTITY,
    [ProgrammeId]                 INT           NOT NULL,
    [ProgrammePriorityId]         INT           NULL,
    [FinanceSource]               INT           NULL,
    [ContractId]                  INT           NOT NULL,
    [Status]                      INT           NOT NULL,
    [ReimbursementDate]           DATETIME2     NOT NULL,
    [Type]                        INT           NOT NULL,
    [Reimbursement]               INT           NOT NULL,
    [RegNumber]                   NVARCHAR(200) NULL,

    [PrincipalBfpEuAmount]        MONEY         NULL,
    [PrincipalBfpBgAmount]        MONEY         NULL,
    [PrincipalBfpTotalAmount]     MONEY         NULL,
    [InterestBfpEuAmount]         MONEY         NULL,
    [InterestBfpBgAmount]         MONEY         NULL,
    [InterestBfpTotalAmount]      MONEY         NULL,

    [Comment]                     NVARCHAR(MAX) NULL,
    [ShouldInfluencePaidAmounts]  BIT           NOT NULL,

    [CertReportId]                INT           NULL,

    [IsActivated]                 BIT           NOT NULL,
    [IsDeletedNote]               NVARCHAR(MAX) NULL,
    [CreateDate]                  DATETIME2     NOT NULL,
    [ModifyDate]                  DATETIME2     NOT NULL,
    [Version]                     ROWVERSION    NOT NULL,

    CONSTRAINT [PK_FIReimbursedAmounts]                        PRIMARY KEY ([FIReimbursedAmountId]),
    CONSTRAINT [FK_FIReimbursedAmounts_Programmes]             FOREIGN KEY ([ProgrammeId])    REFERENCES [dbo].[MapNodes]      ([MapNodeId]),
    CONSTRAINT [FK_FIReimbursedAmounts_Contracts]              FOREIGN KEY ([ContractId])     REFERENCES [dbo].[Contracts]     ([ContractId]),
    CONSTRAINT [FK_FIReimbursedAmounts_CertReports]            FOREIGN KEY ([CertReportId])   REFERENCES [dbo].[CertReports]   ([CertReportId]),
    CONSTRAINT [CHK_FIReimbursedAmounts_Status]                CHECK       ([Status]         IN (1, 2, 3)),
    CONSTRAINT [CHK_FIReimbursedAmounts_Type]                  CHECK       ([Type]           IN (1, 2, 3)),
    CONSTRAINT [CHK_FIReimbursedAmounts_Reimbursement]         CHECK       ([Reimbursement]  IN (1, 2, 3))
);
GO

CREATE UNIQUE INDEX [UQ_FIReimbursedAmounts_RegNumber]
ON [FIReimbursedAmounts]([RegNumber])
WHERE [RegNumber] IS NOT NULL;

GO