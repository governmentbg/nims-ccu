PRINT 'ContractReportFinancialCSDBudgetItems'
GO

CREATE TABLE [dbo].[ContractReportFinancialCSDBudgetItems] (
    [ContractReportFinancialCSDBudgetItemId]                INT               NOT NULL,
    [ContractReportFinancialCSDId]                          INT               NOT NULL,
    [ContractReportFinancialId]                             INT               NOT NULL,
    [ContractReportId]                                      INT               NOT NULL,
    [ContractId]                                            INT               NOT NULL,
    [Gid]                                                   UNIQUEIDENTIFIER  NOT NULL UNIQUE,

    [ContractBudgetLevel3AmountId]                          INT               NOT NULL,

    [BudgetDetailGid]                                       UNIQUEIDENTIFIER  NOT NULL,
    [BudgetDetailName]                                      NVARCHAR(MAX)     NOT NULL,
    [ContractActivityGid]                                   UNIQUEIDENTIFIER  NULL,
    [ContractActivityName]                                  NVARCHAR(MAX)     NULL,
    [EuAmount]                                              MONEY             NOT NULL,
    [BgAmount]                                              MONEY             NOT NULL,
    [BfpTotalAmount]                                        MONEY             NULL,
    [SelfAmount]                                            MONEY             NOT NULL,
    [CrossFinancing]                                        INT               NOT NULL,
    [IsVatAmount]                                           BIT               NOT NULL,
    [TotalAmount]                                           MONEY             NOT NULL,
    [UnitDefinition]                                        NVARCHAR(MAX)     NULL,
    [ProducedUnitsCount]                                    INT               NULL,
    [UnitCost]                                              MONEY             NULL,
    [InsideEU]                                              INT               NOT NULL,
    [OutsideEU]                                             INT               NOT NULL,
    [AdvancePayment]                                        INT               NOT NULL,
    [ContributionNature]                                    INT               NOT NULL,

    [CostSupportingDocumentApproved]                        BIT               NULL,
    [Status]                                                INT               NOT NULL,
    [Notes]                                                 NVARCHAR(MAX)     NULL,
    [CheckedByUserId]                                       INT               NULL,
    [CheckedDate]                                           DATETIME2         NULL,
    [TechCheckedByUserId]                                   INT               NULL,
    [TechCheckedDate]                                       DATETIME2         NULL,

    [UnapprovedEuAmount]                                    MONEY             NULL,
    [UnapprovedBgAmount]                                    MONEY             NULL,
    [UnapprovedBfpTotalAmount]                              MONEY             NULL,
    [UnapprovedSelfAmount]                                  MONEY             NULL,
    [UnapprovedTotalAmount]                                 MONEY             NULL,

    [UnapprovedByCorrectionEuAmount]                        MONEY             NULL,
    [UnapprovedByCorrectionBgAmount]                        MONEY             NULL,
    [UnapprovedByCorrectionBfpTotalAmount]                  MONEY             NULL,
    [UnapprovedByCorrectionSelfAmount]                      MONEY             NULL,
    [UnapprovedByCorrectionTotalAmount]                     MONEY             NULL,
    
    [CorrectionType]                                        INT               NULL,
    [FinancialCorrectionId]                                 INT               NULL,
    [IrregularityId]                                        INT               NULL,

    [ApprovedEuAmount]                                      MONEY             NULL,
    [ApprovedBgAmount]                                      MONEY             NULL,
    [ApprovedBfpTotalAmount]                                MONEY             NULL,
    [ApprovedSelfAmount]                                    MONEY             NULL,
    [ApprovedTotalAmount]                                   MONEY             NULL,

    [CertReportId]                                          INT               NULL,

    [CertStatus]                                            INT               NULL,
    [CertCheckedByUserId]                                   INT               NULL,
    [CertCheckedDate]                                       DATETIME2         NULL,
    [UncertifiedApprovedEuAmount]                           MONEY             NULL,
    [UncertifiedApprovedBgAmount]                           MONEY             NULL,
    [UncertifiedApprovedBfpTotalAmount]                     MONEY             NULL,
    [UncertifiedApprovedSelfAmount]                         MONEY             NULL,
    [UncertifiedApprovedTotalAmount]                        MONEY             NULL,

    [CertifiedApprovedEuAmount]                             MONEY             NULL,
    [CertifiedApprovedBgAmount]                             MONEY             NULL,
    [CertifiedApprovedBfpTotalAmount]                       MONEY             NULL,
    [CertifiedApprovedSelfAmount]                           MONEY             NULL,
    [CertifiedApprovedTotalAmount]                          MONEY             NULL,

    [CreateDate]                                            DATETIME2         NOT NULL,
    [ModifyDate]                                            DATETIME2         NOT NULL,
    [Version]                                               ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportFinancialCSDBudgetItems]                                   PRIMARY KEY ([ContractReportFinancialCSDBudgetItemId]),
    CONSTRAINT [FK_ContractReportFinancialCSDBudgetItems_ContractReportFinancialCSDs]       FOREIGN KEY ([ContractReportFinancialCSDId])      REFERENCES [dbo].[ContractReportFinancialCSDs] ([ContractReportFinancialCSDId]),
    CONSTRAINT [FK_ContractReportFinancialCSDBudgetItems_ContractReportFinancials]          FOREIGN KEY ([ContractReportFinancialId])         REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportFinancialCSDBudgetItems_ContractReports]                   FOREIGN KEY ([ContractReportId])                  REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportFinancialCSDBudgetItems_Contracts]                         FOREIGN KEY ([ContractId])                        REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportFinancialCSDBudgetItems_ContractBudgetLevel3Amounts]       FOREIGN KEY ([ContractBudgetLevel3AmountId])      REFERENCES [dbo].[ContractBudgetLevel3Amounts] ([ContractBudgetLevel3AmountId]),
    CONSTRAINT [FK_ContractReportFinancialCSDBudgetItems_FinancialCorrections]              FOREIGN KEY ([FinancialCorrectionId])     REFERENCES [dbo].[FinancialCorrections] ([FinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportFinancialCSDBudgetItems_Irregularities]                    FOREIGN KEY ([IrregularityId])            REFERENCES [dbo].[Irregularities] ([IrregularityId]),
    CONSTRAINT [FK_ContractReportFinancialCSDBudgetItems_CheckedByUser]                     FOREIGN KEY ([CheckedByUserId])           REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ContractReportFinancialCSDBudgetItems_TechCheckedByUser]                 FOREIGN KEY ([TechCheckedByUserId])       REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ContractReportFinancialCSDBudgetItems_CertCheckedByUser]                 FOREIGN KEY ([CertCheckedByUserId])       REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ContractReportFinancialCSDBudgetItems_CertReports]                       FOREIGN KEY ([CertReportId])              REFERENCES [dbo].[CertReports] ([CertReportId]),
    CONSTRAINT [CHK_ContractReportFinancialCSDBudgetItems_CorrectionType]                   CHECK       ([CorrectionType]      IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractReportFinancialCSDBudgetItems_Status]                           CHECK       ([Status]              IN (1, 2)),
    CONSTRAINT [CHK_ContractReportFinancialCSDBudgetItems_CertStatus]                       CHECK       ([CertStatus]          IN (1, 2)),
    CONSTRAINT [CHK_ContractReportFinancialCSDBudgetItems_CrossFinancing]                   CHECK       ([CrossFinancing]      IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractReportFinancialCSDBudgetItems_InsideEU]                         CHECK       ([InsideEU]            IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractReportFinancialCSDBudgetItems_OutsideEU]                        CHECK       ([OutsideEU]           IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractReportFinancialCSDBudgetItems_AdvancePayment]                   CHECK       ([AdvancePayment]      IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractReportFinancialCSDBudgetItems_ContributionNature]               CHECK       ([ContributionNature]  IN (1, 2, 3))
);
GO

exec spDescTable  N'ContractReportFinancialCSDBudgetItems', N'Верифицирани суми на разходооправдателни документи за конкретни бюджетен ред и дейност.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'ContractReportFinancialCSDBudgetItemId'    , N'Уникален системно генериран идентификатор'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'ContractReportFinancialId'                 , N'Идентификатор на финансов отчет'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'ContractReportId'                          , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'ContractId'                                , N'Идентификатор на договор'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'Gid'                                       , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'BudgetDetailGid'                           , N'Публичен идентификатор на ред от бюджет.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'BudgetDetailName'                          , N'Наименование на ред от бюджет.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'ContractActivityGid'                       , N'Публичен идентификатор на дейност.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'ContractActivityName'                      , N'Наименование на дейност.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'EuAmount'                                  , N'Стойност финансиране ЕС.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'BgAmount'                                  , N'Стойност национално финансиране.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'SelfAmount'                                , N'Собствено съфинансиране.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'CrossFinancing'                            , N'Сумата е кръстосано съфинансиране: 1 - Да, 2 - Не, 3 - Не е приложимо.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'IsVatAmount'                               , N'Сумата е ДДС.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'TotalAmount'                               , N'Обща сума.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UnitDefinition'                            , N'Определение за единица, съгласно стандартните таблици.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'ProducedUnitsCount'                        , N'Брой произведени единици.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UnitCost'                                  , N'Разход за единица.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'InsideEU'                                  , N'Дейности извън програмния район, но в границите на ЕС (само за ЕФРР): 1 - Да, 2 - Не, 3 - Не е приложимо.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'OutsideEU'                                 , N'Дейности извън границите на ЕС (само за ЕСФ): 1 - Да, 2 - Не, 3 - Не е приложимо.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'AdvancePayment'                            , N'Разходът покрива авансово плащане: 1 - Да, 2 - Не, 3 - Не е приложимо.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'ContributionNature'                        , N'Разходът представлява "принос в натура": 1 - Да, 2 - Не, 3 - Не е приложимо.'

exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'CostSupportingDocumentApproved'            , N'Съгласие по разходооправдателен документ: 1 - Да, 2 - Не.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'Status'                                    , N'Статус: 1 - Чернова, 2 - Въведен.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'Notes'                                     , N'Бележки.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'CheckedByUserId'                           , N'Проверено от.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'CheckedDate'                               , N'Дата на проверка.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'TechCheckedByUserId'                       , N'Технически роверено от.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'TechCheckedDate'                           , N'Дата на техническа проверка.'

exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UnapprovedEuAmount'                        , N'Неверифицирана стойност финансиране ЕС.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UnapprovedBgAmount'                        , N'Неверифицирана стойност национално финансиране.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UnapprovedSelfAmount'                      , N'Неверифицирано собствено съфинансиране.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UnapprovedTotalAmount'                     , N'Неверифицирана обща сума.'

exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UnapprovedByCorrectionEuAmount'            , N'Неверифицирана стойност финансиране ЕС по наложена финансова корекция.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UnapprovedByCorrectionBgAmount'            , N'Неверифицирана стойност национално финансиране по наложена финансова корекция.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UnapprovedByCorrectionSelfAmount'          , N'Неверифицирано собствено съфинансиране по наложена финансова корекция.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UnapprovedByCorrectionTotalAmount'         , N'Неверифицирана обща сума по наложена финансова корекция.'

exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'CorrectionType'                            , N'Тип на корекцията: 1 - Финансова корекция, 2 - Нередност, 3 - Финансова корекция и нередност.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'FinancialCorrectionId'                     , N'Идентификатор на финансова корекция.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'IrregularityId'                            , N'Идентификатор на нередност.'

exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'ApprovedEuAmount'                          , N'Одобрена стойност финансиране ЕС.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'ApprovedBgAmount'                          , N'Одобрена стойност национално финансиране.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'ApprovedSelfAmount'                        , N'Одобрено собствено съфинансиране.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'ApprovedTotalAmount'                       , N'Одобрена обща сума.'

exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'CertStatus'                                , N'Статус за сертификация: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UncertifiedApprovedEuAmount'               , N'Несертифицирана одобрена стойност финансиране ЕС.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UncertifiedApprovedBgAmount'               , N'Несертифицирана одобрена стойност национално финансиране.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UncertifiedApprovedSelfAmount'             , N'Несертифицирана одобрено собствено съфинансиране.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'UncertifiedApprovedTotalAmount'            , N'Несертифицирана одобрена обща сума.'

exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'CertifiedApprovedEuAmount'                 , N'Сертифицирана одобрена стойност финансиране ЕС.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'CertifiedApprovedBgAmount'                 , N'Сертифицирана одобрена стойност национално финансиране.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'CertifiedApprovedSelfAmount'               , N'Сертифицирана одобрено собствено съфинансиране.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'CertifiedApprovedTotalAmount'              , N'Сертифицирана одобрена обща сума.'

exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'CreateDate'                                , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'ModifyDate'                                , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportFinancialCSDBudgetItems', N'Version'                                   , N'Версия.'
GO
