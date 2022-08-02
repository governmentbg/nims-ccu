USE [$(dbName)]
GO

---------------------------------------------------------------
--Tools
---------------------------------------------------------------

:r $(rootPath)\"Tools\Tool_ScriptDiagram2008.sql"
:r $(rootPath)\"Tools\spDesc.sql"
:r $(rootPath)\"Tools\sp_generate_inserts.sql"
:r $(rootPath)\"Tools\spAddDailyJob.sql"

---------------------------------------------------------------
--Custom types
---------------------------------------------------------------

:r $(rootPath)\"CustomTypes\IntTableType.sql"
:r $(rootPath)\"CustomTypes\StringTableType.sql"

---------------------------------------------------------------
--Tables
---------------------------------------------------------------
-- System
:r $(rootPath)"\Tables\System\GParams.sql"
:r $(rootPath)"\Tables\System\BlobContentLocations.sql"
:r $(rootPath)"\Tables\System\Blobs.sql"
:r $(rootPath)"\Tables\System\Counters.sql"
:r $(rootPath)"\Tables\System\UpdateScripts.sql"

-- ActionLogs
:r $(rootPath)"\Tables\ActionLogs\ActionLogs.sql"

--Nuts
:r $(rootPath)"\Tables\Geography\Countries.sql"
:r $(rootPath)"\Tables\Geography\ProtectedZones.sql"
:r $(rootPath)"\Tables\Geography\Nuts1s.sql"
:r $(rootPath)"\Tables\Geography\Nuts2s.sql"
:r $(rootPath)"\Tables\Geography\Districts.sql"
:r $(rootPath)"\Tables\Geography\Municipalities.sql"
:r $(rootPath)"\Tables\Geography\Settlements.sql"

-- Users
:r $(rootPath)"\Tables\Users\PermissionTemplates.sql"
:r $(rootPath)"\Tables\Users\UserOrganizations.sql"
:r $(rootPath)"\Tables\Users\UserTypes.sql"
:r $(rootPath)"\Tables\Users\Users.sql"
:r $(rootPath)"\Tables\Users\RequestPackages.sql"
:r $(rootPath)"\Tables\Users\RequestPackageUsers.sql"
:r $(rootPath)"\Tables\Users\PermissionRequests.sql"
:r $(rootPath)"\Tables\Users\RegDataRequests.sql"
:r $(rootPath)"\Tables\Declarations\Declarations.sql"
:r $(rootPath)"\Tables\Declarations\DeclarationFiles.sql"
:r $(rootPath)"\Tables\Users\UserDeclarations.sql"

--Remuneration
:r $(rootPath)"\Tables\Remuneration\RemunerationAllowances.sql"
:r $(rootPath)"\Tables\Remuneration\RemunerationAllowanceValues.sql"
:r $(rootPath)"\Tables\Remuneration\RemunerationBasicRates.sql"
:r $(rootPath)"\Tables\Remuneration\RemunerationBasicRateValues.sql"
:r $(rootPath)"\Tables\Remuneration\RemunerationSchemes.sql"

--Institutions
:r $(rootPath)"\Tables\Institutions\InstitutionTypes.sql"
:r $(rootPath)"\Tables\Institutions\Institutions.sql"
:r $(rootPath)"\Tables\Institutions\InstitutionPersons.sql"

-- OperationalMap
:r $(rootPath)"\Tables\OperationalMap\Measures.sql"
:r $(rootPath)"\Tables\OperationalMap\ProgrammeGroups.sql"
:r $(rootPath)"\Tables\OperationalMap\BudgetPeriods.sql"
:r $(rootPath)"\Tables\OperationalMap\InterventionCategories.sql"
:r $(rootPath)"\Tables\OperationalMap\InvestmentPriorities.sql"
:r $(rootPath)"\Tables\OperationalMap\MapNodes.sql"
:r $(rootPath)"\Tables\OperationalMap\IndicatorItemTypes.sql"
:r $(rootPath)"\Tables\OperationalMap\Indicators.sql"
:r $(rootPath)"\Tables\OperationalMap\MapNodeRelations.sql"
:r $(rootPath)"\Tables\OperationalMap\MapNodeInterventionCategories.sql"
:r $(rootPath)"\Tables\OperationalMap\MapNodeInstitutions.sql"
:r $(rootPath)"\Tables\OperationalMap\MapNodeIndicators.sql"
:r $(rootPath)"\Tables\OperationalMap\MapNodeIndicatorReportedAmounts.sql"
:r $(rootPath)"\Tables\OperationalMap\MapNodeFinanceSources.sql"
:r $(rootPath)"\Tables\OperationalMap\MapNodeBudgets.sql"
:r $(rootPath)"\Tables\OperationalMap\MapNodeDocuments.sql"
:r $(rootPath)"\Tables\OperationalMap\ProgrammeProcedureManuals.sql"
:r $(rootPath)"\Tables\OperationalMap\ProgrammeCheckLists.sql"
:r $(rootPath)"\Tables\OperationalMap\ProgrammeCheckListVersionXmls.sql"
:r $(rootPath)"\Tables\OperationalMap\ProgrammeCheckListVersionXmlFiles.sql"
:r $(rootPath)"\Tables\OperationalMap\ProgrammeApplicationDocuments.sql"
:r $(rootPath)"\Tables\OperationalMap\Directions.sql"
:r $(rootPath)"\Tables\OperationalMap\SubDirections.sql"
:r $(rootPath)"\Tables\OperationalMap\ProgrammeDirections.sql"
:r $(rootPath)"\Tables\OperationalMap\ProgrammeDeclarations.sql"
:r $(rootPath)"\Tables\OperationalMap\ProgrammeDeclarationItems.sql"

--Companies
:r $(rootPath)"\Tables\Companies\KidCodes.sql"
:r $(rootPath)"\Tables\Companies\CompanySizeTypes.sql"
:r $(rootPath)"\Tables\Companies\CompanyTypes.sql"
:r $(rootPath)"\Tables\Companies\CompanyLegalTypes.sql"
:r $(rootPath)"\Tables\Companies\Companies.sql"
:r $(rootPath)"\Tables\Companies\CompanyPersons.sql"
:r $(rootPath)"\Tables\Companies\LocalActionGroupMunicipalities.sql"

--Monitorstat
:r $(rootPath)"\Tables\Monitorstat\MonitorstatSurveys.sql"
:r $(rootPath)"\Tables\Monitorstat\MonitorstatReports.sql"
:r $(rootPath)"\Tables\Monitorstat\MonitorstatMapNodes.sql"

-- Procedures
:r $(rootPath)"\Tables\Procedures\ExpenseTypes.sql"
:r $(rootPath)"\Tables\Procedures\ExpenseSubTypes.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureTypes.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureReportingDocTypes.sql"

:r $(rootPath)"\Tables\Procedures\Procedures.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureTimeLimits.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureShares.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureSpecificTargets.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureInvestmentPriorities.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureLocations.sql"

:r $(rootPath)"\Tables\Procedures\ProcedureProgrammes.sql"

:r $(rootPath)"\Tables\Procedures\ProcedureBudgetLevel1.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureBudgetLevel2.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureBudgetLevel3.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureBudgetValidationRules.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureContractReportDocuments.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureNumbers.sql"

:r $(rootPath)"\Tables\Procedures\ProcedureIndicators.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureInterventionCategories.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureSpecFields.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureSpecificBeneficiaries.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureApplicationGuidelines.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureApplicationDocs.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureReportingDocs.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureDocuments.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureEvalTables.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureEvalTableXmls.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureEvalTableXmlFiles.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureQuestions.sql"

:r $(rootPath)"\Tables\Procedures\ProcedureVersions.sql"

:r $(rootPath)"\Tables\Procedures\ProcedureIndicativeAnnualWorkingProgrammes.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureIndicativeAnnualWorkingProgrammeCompanies.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureIndicativeAnnualWorkingProgrammeCandidates.sql"

:r $(rootPath)"\Tables\Procedures\ProcedureMonitorstatDocuments.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureMonitorstatRequests.sql"

:r $(rootPath)"\Tables\Procedures\ProcedureBudgetComponents.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureBudgetKidCodes.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureBudgetSizeTypes.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureDeclarations.sql"

-- IndicativeAnnualWorkingProgrammes
:r $(rootPath)"\Tables\IndicativeAnnualWorkingProgrammes\IndicativeAnnualWorkingProgrammes.sql"
:r $(rootPath)"\Tables\IndicativeAnnualWorkingProgrammes\IndicativeAnnualWorkingProgrammeTables.sql"
:r $(rootPath)"\Tables\IndicativeAnnualWorkingProgrammes\IndicativeAnnualWorkingProgrammeTableCandidates.sql"
:r $(rootPath)"\Tables\IndicativeAnnualWorkingProgrammes\IndicativeAnnualWorkingProgrammeTableTimeLimits.sql"
:r $(rootPath)"\Tables\IndicativeAnnualWorkingProgrammes\IndicativeAnnualWorkingProgrammeTableProgrammes.sql"

--Projects
:r $(rootPath)"\Tables\Projects\TeamPositions.sql"

:r $(rootPath)"\Tables\Projects\ErrandLegalActs.sql"
:r $(rootPath)"\Tables\Projects\ErrandTypes.sql"
:r $(rootPath)"\Tables\Projects\ErrandAreas.sql"
:r $(rootPath)"\Tables\Projects\ProjectTypes.sql"

:r $(rootPath)"\Tables\Projects\Projects.sql"
:r $(rootPath)"\Tables\Projects\ProjectPartners.sql"
:r $(rootPath)"\Tables\Projects\ProjectInterventionCategories.sql"
:r $(rootPath)"\Tables\Projects\ProjectShareDetailExpenseBudgets.sql"
:r $(rootPath)"\Tables\Projects\ProjectActivities.sql"
:r $(rootPath)"\Tables\Projects\ProjectPayPlans.sql"
:r $(rootPath)"\Tables\Projects\ProjectIndicators.sql"
:r $(rootPath)"\Tables\Projects\ProjectIndividualIndicators.sql"
:r $(rootPath)"\Tables\Projects\ProjectTeams.sql"
:r $(rootPath)"\Tables\Projects\ProjectErrands.sql"
:r $(rootPath)"\Tables\Projects\ProjectSpecFields.sql"
:r $(rootPath)"\Tables\Projects\ProjectVersionXmls.sql"
:r $(rootPath)"\Tables\Projects\ProjectVersionXmlFiles.sql"
:r $(rootPath)"\Tables\Projects\ProjectFiles.sql"
:r $(rootPath)"\Tables\Projects\ProjectFileSignatures.sql"
:r $(rootPath)"\Tables\Projects\ProjectMonitorstatRequests.sql"
:r $(rootPath)"\Tables\Projects\ProjectMonitorstatResponses.sql"

--ProjectMassManagingAuthorityCommunication
:r $(rootPath)"\Tables\Projects\ProjectMassManagingAuthorityCommunications.sql"
:r $(rootPath)"\Tables\Projects\ProjectMassManagingAuthorityCommunicationDocuments.sql"
:r $(rootPath)"\Tables\Projects\ProjectMassManagingAuthorityCommunicationRecipients.sql"

--Registrations
:r $(rootPath)"\Tables\Registrations\Registrations.sql"
:r $(rootPath)"\Tables\Registrations\RegProjectXmls.sql"
:r $(rootPath)"\Tables\Registrations\RegProjectXmlFiles.sql"

:r $(rootPath)"\Tables\Procedures\ProcedureDiscussions.sql"

-- PublicDiscussions
:r $(rootPath)"\Tables\PublicDiscussions\PublicDiscussions.sql"
:r $(rootPath)"\Tables\PublicDiscussions\PublicDiscussionGuidelines.sql"
:r $(rootPath)"\Tables\PublicDiscussions\PublicDiscussionComments.sql"

--EvalSession
:r $(rootPath)"\Tables\EvalSessions\EvalSessions.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionProjects.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionUsers.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionDecisions.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionCorrespondence.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionReports.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionReportProjects.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionReportProjectPartners.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionDistributions.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionDistributionProjects.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionDistributionUsers.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionEvaluations.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionDocuments.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionResults.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionResultProjects.sql"

:r $(rootPath)"\Tables\EvalSessions\EvalSessionSheets.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionSheetXmls.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionSheetXmlFiles.sql"

:r $(rootPath)"\Tables\EvalSessions\EvalSessionEvaluationSheets.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionStandings.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionStandingProjects.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionProjectStandingRejectionReasons.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionProjectStandings.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionProjectStandingEvaluations.sql"

:r $(rootPath)"\Tables\EvalSessions\EvalSessionStandpoints.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionStandpointXmls.sql"
:r $(rootPath)"\Tables\EvalSessions\EvalSessionStandpointXmlFiles.sql"

--ProjectCommunications
:r $(rootPath)"\Tables\Projects\ProjectCommunications.sql"
:r $(rootPath)"\Tables\Projects\ProjectCommunicationAnswers.sql"
:r $(rootPath)"\Tables\Projects\ProjectCommunicationMessageFiles.sql"
:r $(rootPath)"\Tables\Projects\ProjectCommunicationFiles.sql"
:r $(rootPath)"\Tables\Projects\ProjectCommunicationFileSignatures.sql"


--Contract
:r $(rootPath)"\Tables\Contracts\ContractRegistrations.sql"

:r $(rootPath)"\Tables\Contracts\Contracts.sql"
:r $(rootPath)"\Tables\Contracts\ContractLocations.sql"
:r $(rootPath)"\Tables\Contracts\ContractVersionXmls.sql"
:r $(rootPath)"\Tables\Contracts\ContractBudgetLevel3Amounts.sql"
:r $(rootPath)"\Tables\Contracts\ContractActivities.sql"
:r $(rootPath)"\Tables\Contracts\ContractActivityCompanies.sql"
:r $(rootPath)"\Tables\Contracts\ContractPartners.sql"
:r $(rootPath)"\Tables\Contracts\ContractProcurementDocuments.sql"
:r $(rootPath)"\Tables\Contracts\ContractContractors.sql"
:r $(rootPath)"\Tables\Contracts\ContractContracts.sql"
:r $(rootPath)"\Tables\Contracts\ContractSubcontracts.sql"
:r $(rootPath)"\Tables\Contracts\ContractContractActivities.sql"
:r $(rootPath)"\Tables\Contracts\ContractProcurementPlans.sql"
:r $(rootPath)"\Tables\Contracts\ContractProcurementPlanAdditionalDocuments.sql"
:r $(rootPath)"\Tables\Contracts\ContractProcurementPlanPublicDocuments.sql"
:r $(rootPath)"\Tables\Contracts\ContractDifferentiatedPositions.sql"
:r $(rootPath)"\Tables\Contracts\ContractGrantDocuments.sql"
:r $(rootPath)"\Tables\Contracts\ContractIndicators.sql"
:r $(rootPath)"\Tables\Contracts\ContractVersionXmlFiles.sql"
:r $(rootPath)"\Tables\Contracts\ContractProcurementXmls.sql"
:r $(rootPath)"\Tables\Contracts\ContractProcurementXmlFiles.sql"
:r $(rootPath)"\Tables\Contracts\ContractsContractRegistrations.sql"
:r $(rootPath)"\Tables\Contracts\ContractCommunicationXmls.sql"
:r $(rootPath)"\Tables\Contracts\ContractCommunicationXmlFiles.sql"
:r $(rootPath)"\Tables\Contracts\ContractAccessCodes.sql"
:r $(rootPath)"\Tables\Contracts\ContractSpendingPlanXmls.sql"
:r $(rootPath)"\Tables\Contracts\ContractVersionXmlAmounts.sql"
:r $(rootPath)"\Tables\Contracts\ContractUsers.sql"
:r $(rootPath)"\Tables\Contracts\ContractExtensions.sql"

--ProcedureMassCommunication
:r $(rootPath)"\Tables\Procedures\ProcedureMassCommunications.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureMassCommunicationDocuments.sql"
:r $(rootPath)"\Tables\Procedures\ProcedureMassCommunicationRecipients.sql"

--SpotChecks
:r $(rootPath)"\Tables\SpotChecks\SpotCheckPlans.sql"
:r $(rootPath)"\Tables\SpotChecks\SpotCheckPlanItems.sql"
:r $(rootPath)"\Tables\SpotChecks\SpotCheckPlanTargets.sql"
:r $(rootPath)"\Tables\SpotChecks\SpotCheckPlanDocs.sql"
:r $(rootPath)"\Tables\SpotChecks\SpotChecks.sql"
:r $(rootPath)"\Tables\SpotChecks\SpotCheckItems.sql"
:r $(rootPath)"\Tables\SpotChecks\SpotCheckTargets.sql"
:r $(rootPath)"\Tables\SpotChecks\SpotCheckDocs.sql"
:r $(rootPath)"\Tables\SpotChecks\SpotCheckAscertainments.sql"
:r $(rootPath)"\Tables\SpotChecks\SpotCheckAscertainmentItems.sql"
:r $(rootPath)"\Tables\SpotChecks\SpotCheckRecommendations.sql"
:r $(rootPath)"\Tables\SpotChecks\SpotCheckRecommendationAscertainments.sql"

--CertAuthorityChecks
:r $(rootPath)"\Tables\CertAuthorityChecks\CertAuthorityChecks.sql"
:r $(rootPath)"\Tables\CertAuthorityChecks\CertAuthorityCheckAscertainments.sql"
:r $(rootPath)"\Tables\CertAuthorityChecks\CertAuthorityCheckLevelItems.sql"
:r $(rootPath)"\Tables\CertAuthorityChecks\CertAuthorityCheckDocuments.sql"
:r $(rootPath)"\Tables\CertAuthorityChecks\CertAuthorityCheckProjects.sql"

--Audits
:r $(rootPath)"\Tables\Audits\Audits.sql"
:r $(rootPath)"\Tables\Audits\AuditLevelItems.sql"
:r $(rootPath)"\Tables\Audits\AuditAscertainments.sql"
:r $(rootPath)"\Tables\Audits\AuditAscertainmentItems.sql"
:r $(rootPath)"\Tables\Audits\AuditDocs.sql"
:r $(rootPath)"\Tables\Audits\AuditProjects.sql"

--Registrations
:r $(rootPath)"\Tables\Registrations\RegOfferXmls.sql"
:r $(rootPath)"\Tables\Registrations\RegOfferXmlFiles.sql"

--News
:r $(rootPath)"\Tables\News\News.sql"
:r $(rootPath)"\Tables\News\NewsFiles.sql"

--Messages
:r $(rootPath)"\Tables\Messages\Messages.sql"
:r $(rootPath)"\Tables\Messages\MessageRecipients.sql"
:r $(rootPath)"\Tables\Messages\MessageFiles.sql"

--Notifications
:r $(rootPath)"\Tables\Notifications\NotificationEvents.sql"
:r $(rootPath)"\Tables\Notifications\NotificationEventPermissions.sql"
:r $(rootPath)"\Tables\Notifications\NotificationSettings.sql"
:r $(rootPath)"\Tables\Notifications\NotificationSettingSets.sql"
:r $(rootPath)"\Tables\Notifications\NotificationEntries.sql"
:r $(rootPath)"\Tables\Notifications\UserNotifications.sql"

--Guidances
:r $(rootPath)"\Tables\Guidances\Guidances.sql"

--Nomenclatures
:r $(rootPath)"\Tables\Nomenclatures\CheckBlankTopics.sql"
:r $(rootPath)"\Tables\Nomenclatures\BasicInterestRates.sql"
:r $(rootPath)"\Tables\Nomenclatures\InterestRates.sql"
:r $(rootPath)"\Tables\Nomenclatures\Allowances.sql"
:r $(rootPath)"\Tables\Nomenclatures\AllowanceRates.sql"
:r $(rootPath)"\Tables\Nomenclatures\InterestSchemes.sql"
:r $(rootPath)"\Tables\Nomenclatures\BulstatNomenclatures.sql"

--User permissions
:r $(rootPath)"\Tables\Users\UserPermissions.sql"

--Email
:r $(rootPath)"\Tables\Emails\Emails.sql"

--Monitoring and Financial Control
:r $(rootPath)"\Tables\MonitoringFinancialControl\FlatFinancialCorrections.sql"
:r $(rootPath)"\Tables\MonitoringFinancialControl\FlatFinancialCorrectionLevelItems.sql"
:r $(rootPath)"\Tables\MonitoringFinancialControl\OtherViolations.sql"
:r $(rootPath)"\Tables\MonitoringFinancialControl\FinancialCorrectionImposingReasons.sql"
:r $(rootPath)"\Tables\MonitoringFinancialControl\FinancialCorrections.sql"
:r $(rootPath)"\Tables\MonitoringFinancialControl\FinancialCorrectionVersions.sql"
:r $(rootPath)"\Tables\MonitoringFinancialControl\FinancialCorrectionVersionViolations.sql"

--Irregularities
:r $(rootPath)"\Tables\Irregularities\IrregularityCategories.sql"
:r $(rootPath)"\Tables\Irregularities\IrregularityTypes.sql"
:r $(rootPath)"\Tables\Irregularities\IrregularitySanctionCategories.sql"
:r $(rootPath)"\Tables\Irregularities\IrregularitySanctionTypes.sql"
:r $(rootPath)"\Tables\Irregularities\IrregularityFinancialStatuses.sql"

:r $(rootPath)"\Tables\Irregularities\IrregularitySignals.sql"
:r $(rootPath)"\Tables\Irregularities\IrregularitySignalInvolvedPersons.sql"
:r $(rootPath)"\Tables\Irregularities\IrregularitySignalDocs.sql"

:r $(rootPath)"\Tables\Irregularities\Irregularities.sql"
:r $(rootPath)"\Tables\Irregularities\IrregularityFinancialCorrections.sql"
:r $(rootPath)"\Tables\Irregularities\IrregularityDocs.sql"
:r $(rootPath)"\Tables\Irregularities\IrregularityVersions.sql"
:r $(rootPath)"\Tables\Irregularities\IrregularityVersionInvolvedPersons.sql"
:r $(rootPath)"\Tables\Irregularities\IrregularityVersionDocs.sql"

--CertReports
:r $(rootPath)"\Tables\CertReports\CertReports.sql"
:r $(rootPath)"\Tables\CertReports\CertReportDocuments.sql"
:r $(rootPath)"\Tables\CertReports\CertReportCertificationDocuments.sql"
:r $(rootPath)"\Tables\CertReports\CertReportAttachedCertReports.sql"
:r $(rootPath)"\Tables\CertReports\CertReportSnapshots.sql"
:r $(rootPath)"\Tables\CertReports\CertReportSnapshotFiles.sql"

--ContractReports
:r $(rootPath)"\Tables\Contracts\ContractReports.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportPayments.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportPaymentXmlFiles.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportTechnicals.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportTechnicalXmlFiles.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportFinancials.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportFinancialXmlFiles.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportFinancialChecks.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportPaymentChecks.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportTechnicalChecks.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportTechnicalMembers.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportFinancialCSDs.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportFinancialCSDBudgetItems.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportFinancialCSDFiles.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportFinancialCorrections.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportFinancialCorrectionCSDs.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportAttachedFinancialCorrections.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportIndicators.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportAdvancePaymentAmounts.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportCorrections.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportCorrectionDocuments.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportFinancialRevalidations.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportFinancialRevalidationCSDs.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportRevalidations.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportRevalidationDocuments.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportFinancialCertCorrections.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportFinancialCertCorrectionCSDs.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportCertAuthorityFinancialCorrections.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportCertAuthorityFinancialCorrectionCSDs.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportCertCorrections.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportCertCorrectionDocuments.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportCertAuthorityCorrections.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportCertAuthorityCorrectionDocuments.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportMicros.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportMicrosDistricts.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportMicrosMunicipalities.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportMicrosSettlements.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportMicrosType1Items.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportMicrosType2Items.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportMicrosType3Items.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportMicrosType4Items.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportMicroChecks.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportAdvanceNVPaymentAmounts.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportPaymentCheckAmounts.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportTechnicalCorrections.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportTechnicalCorrectionIndicators.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportRevalidationCertAuthorityCorrections.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportRevalidationCertAuthorityCorrectionDocuments.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportRevalidationCertAuthorityFinancialCorrections.sql"
:r $(rootPath)"\Tables\Contracts\ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs.sql"

--AnnualAccountReports
:r $(rootPath)"\Tables\AnnualAccountReports\AnnualAccountReports.sql"
:r $(rootPath)"\Tables\AnnualAccountReports\AnnualAccountReportCertificationDocuments.sql"
:r $(rootPath)"\Tables\AnnualAccountReports\AnnualAccountReportAuditDocuments.sql"
:r $(rootPath)"\Tables\AnnualAccountReports\AnnualAccountReportCertReports.sql"
:r $(rootPath)"\Tables\AnnualAccountReports\AnnualAccountReportContractReportCorrections.sql"
:r $(rootPath)"\Tables\AnnualAccountReports\AnnualAccountReportFinancialCorrectionCSDs.sql"
:r $(rootPath)"\Tables\AnnualAccountReports\AnnualAccountReportCertCorrections.sql"
:r $(rootPath)"\Tables\AnnualAccountReports\AnnualAccountReportCertFinancialCorrectionCSDs.sql"
:r $(rootPath)"\Tables\AnnualAccountReports\AnnualAccountReportAppendices.sql"
:r $(rootPath)"\Tables\AnnualAccountReports\AnnualAccountReportCertRevalidationCorrections.sql"
:r $(rootPath)"\Tables\AnnualAccountReports\AnnualAccountReportCertRevalidationFinancialCorrectionCSDs.sql"

--Debts
:r $(rootPath)"\Tables\Debts\ContractDebts.sql"
:r $(rootPath)"\Tables\Debts\ContractDebtVersions.sql"
:r $(rootPath)"\Tables\Debts\ContractDebtPayments.sql"
:r $(rootPath)"\Tables\Debts\ContractDebtInterests.sql"
:r $(rootPath)"\Tables\Debts\CorrectionDebts.sql"
:r $(rootPath)"\Tables\Debts\CorrectionDebtVersions.sql"

--SapImport
:r $(rootPath)"\Tables\SapInterfaces\SapSchemas.sql"
:r $(rootPath)"\Tables\SapInterfaces\SapFiles.sql"

--Monitoring and Financial Control
:r $(rootPath)"\Tables\MonitoringFinancialControl\ActuallyPaidAmounts.sql"
:r $(rootPath)"\Tables\MonitoringFinancialControl\ActuallyPaidAmountDocuments.sql"
:r $(rootPath)"\Tables\MonitoringFinancialControl\CompensationDocuments.sql"
:r $(rootPath)"\Tables\MonitoringFinancialControl\CompensationDocumentDocs.sql"

:r $(rootPath)"\Tables\MonitoringFinancialControl\ReimbursedAmounts.sql"
:r $(rootPath)"\Tables\MonitoringFinancialControl\ReimbursedAmountPayments.sql"
:r $(rootPath)"\Tables\MonitoringFinancialControl\Prognoses.sql"
:r $(rootPath)"\Tables\MonitoringFinancialControl\FIReimbursedAmounts.sql"

--EuReimbursedAmounts
:r $(rootPath)"\Tables\EuReimbursedAmounts\EuReimbursedAmounts.sql"
:r $(rootPath)"\Tables\EuReimbursedAmounts\EuReimbursedAmountCertReports.sql"

--SapImport/SapPaidAmounts
:r $(rootPath)"\Tables\SapInterfaces\SapPaidAmounts.sql"
:r $(rootPath)"\Tables\SapInterfaces\SapDistributedLimits.sql"

--CheckSheets
:r $(rootPath)"\Tables\CheckSheets\CheckSheets.sql"
:r $(rootPath)"\Tables\CheckSheets\CheckSheetVersionXmls.sql"
:r $(rootPath)"\Tables\CheckSheets\CheckSheetVersionXmlFiles.sql"
:r $(rootPath)"\Tables\CheckSheets\CheckSheetActionLogs.sql"

--HistoricContracts
:r $(rootPath)"\Tables\HistoricContracts\HistoricContracts.sql"
:r $(rootPath)"\Tables\HistoricContracts\HistoricContractActivities.sql"
:r $(rootPath)"\Tables\HistoricContracts\HistoricContractLocations.sql"
:r $(rootPath)"\Tables\HistoricContracts\HistoricContractPartners.sql"
:r $(rootPath)"\Tables\HistoricContracts\HistoricContractProcurementPlans.sql"
:r $(rootPath)"\Tables\HistoricContracts\HistoricContractProcurementPlanPositions.sql"
:r $(rootPath)"\Tables\HistoricContracts\HistoricContractContractedAmounts.sql"
:r $(rootPath)"\Tables\HistoricContracts\HistoricContractActuallyPaidAmounts.sql"
:r $(rootPath)"\Tables\HistoricContracts\HistoricContractReimbursedAmounts.sql"
:r $(rootPath)"\Tables\HistoricContracts\HistoricContractRequests.sql"


---------------------------------------------------------------
--Sequences
---------------------------------------------------------------

:r $(rootPath)"\Sequences\BlobContentSequence.sql"
:r $(rootPath)"\Sequences\CompanySequence.sql"
:r $(rootPath)"\Sequences\EvalSessionSheetSequence.sql"
:r $(rootPath)"\Sequences\ContractReportFinancialCSDSequence.sql"
:r $(rootPath)"\Sequences\ContractReportFinancialCSDBudgetItemSequence.sql"
:r $(rootPath)"\Sequences\ContractReportFinancialCSDFileSequence.sql"
:r $(rootPath)"\Sequences\ActuallyPaidAmountSequence.sql"
:r $(rootPath)"\Sequences\ReimbursedAmountSequence.sql"
:r $(rootPath)"\Sequences\HistoricContractActivitySequence.sql"
:r $(rootPath)"\Sequences\HistoricContractActuallyPaidAmountSequence.sql"
:r $(rootPath)"\Sequences\HistoricContractContractedAmountSequence.sql"
:r $(rootPath)"\Sequences\HistoricContractLocationSequence.sql"
:r $(rootPath)"\Sequences\HistoricContractPartnerSequence.sql"
:r $(rootPath)"\Sequences\HistoricContractProcurementPlanPositionSequence.sql"
:r $(rootPath)"\Sequences\HistoricContractProcurementPlanSequence.sql"
:r $(rootPath)"\Sequences\HistoricContractReimbursedAmountSequence.sql"
:r $(rootPath)"\Sequences\HistoricContractSequence.sql"
:r $(rootPath)"\Sequences\HistoricContractRequestSequence.sql"

---------------------------------------------------------------
--Diagrams
---------------------------------------------------------------
:r $(rootPath)"\Diagrams\Nuts.sql"
:r $(rootPath)"\Diagrams\Institutions.sql"
:r $(rootPath)"\Diagrams\Programmes.sql"
:r $(rootPath)"\Diagrams\Procedures.sql"
:r $(rootPath)"\Diagrams\Companies.sql"
:r $(rootPath)"\Diagrams\Projects.sql"
:r $(rootPath)"\Diagrams\Evals.sql"
:r $(rootPath)"\Diagrams\Contracts.sql"

---------------------------------------------------------------
--Views
---------------------------------------------------------------

:r $(rootPath)"\Views\UniqueEvalSessionProjectIndexed.sql"
:r $(rootPath)"\Views\UniqueContractEmailAccessCodeIndexed.sql"
:r $(rootPath)"\Views\MonitoringMapNodeIndicator.sql"
:r $(rootPath)"\Views\UniqueProcedureBudgetSizeTypeKidCodeIndexed.sql"
---------------------------------------------------------------
--Procedures
---------------------------------------------------------------

:r $(rootPath)"\Procedures\spDeleteBlobs.sql"
:r $(rootPath)"\Procedures\spMarkDeletedBlobs.sql"
:r $(rootPath)"\Procedures\spExistsBlobReference.sql"

---------------------------------------------------------------
--Jobs
---------------------------------------------------------------

-- :r $(rootPath)"\Jobs\jobDeleteBlobs.sql"
-- :r $(rootPath)"\Jobs\jobMarkDeletedBlobs.sql"
