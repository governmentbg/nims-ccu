export const ProjectDossierContractFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/projectDossier/:id/contract/:ind',
      {},
      {
        getContractVersions: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/versions',
          isArray: true
        },
        getContractVersion: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/versions/:index'
        },
        getContractProcurements: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/procurements',
          isArray: true
        },
        getContractProcurement: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/procurements/:index'
        },
        getContractSpendingPlans: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/spendingPlans',
          isArray: true
        },
        getContractSpendingPlan: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/spendingPlans/:index'
        },
        getContractProcurementOffers: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/offers',
          isArray: true
        },
        getContractProcurementOffer: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/offers/:index'
        },
        getContractCommunications: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/communications',
          isArray: true
        },
        getContractCommunication: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/communications/:index'
        },
        getContractReportRequestedAmounts: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportRequestedAmounts',
          isArray: true
        },
        getContractReportApprovedAmounts: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportApprovedAmounts',
          isArray: true
        },
        getContractReportCertifiedAmounts: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportCertifiedAmounts',
          isArray: true
        },
        getContractActuallyPaidAmounts: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/actuallyPaidAmounts',
          isArray: true
        },
        getContractDebts: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/debts',
          isArray: true
        },
        getCorrectionDebts: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/correctionDebts',
          isArray: true
        },
        getDebtReimbursedAmounts: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/debtReimbursedAmounts',
          isArray: true
        },
        getContractReimbursedAmounts: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/contractReimbursedAmounts',
          isArray: true
        },
        getFiReimbursedAmounts: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/fiReimbursedAmounts',
          isArray: true
        },
        getContractFinancialCorrections: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/financialCorrections',
          isArray: true
        },
        getContractFlatFinancialCorrections: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/flatFinancialCorrections',
          isArray: true
        },
        getContractReportCorrections: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportCorrection',
          isArray: true
        },
        getContractReportFinancialCorrections: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportFinancialCorrection',
          isArray: true
        },
        getContractReportFinancialCSDs: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportFinancialCSDs',
          isArray: true
        },
        getContractReportCertCorrections: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportCertCorrection',
          isArray: true
        },
        getContractReportFinancialCertCorrections: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportFinancialCertCorrection',
          isArray: true
        },
        getContractReportCertifiedAmountFinancialCorrections: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportCertifiedAmountFinancialCorrections',
          isArray: true
        },
        getContractReportCertifiedAmountCorrections: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportCertifiedAmountCorrections',
          isArray: true
        },
        getContractReportCertAuthorityFinancialCorrections: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportCertAuthorityFinancialCorrections',
          isArray: true
        },
        getContractReportCertAuthorityCorrections: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportCertAuthorityCorrections',
          isArray: true
        },
        getContractReportRevalidations: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportRevalidation',
          isArray: true
        },
        getContractReportFinancialRevalidations: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportFinancialRevalidation',
          isArray: true
        },
        getContractReportsWithTechnical: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/reportsWithTechnical',
          isArray: true
        },
        getContractInternalEnvironmentSpotChecks: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/internalEnvironmentSpotChecks',
          isArray: true
        },
        getContractTechnicalReportSpotChecks: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/technicalReportSpotChecks',
          isArray: true
        },
        getContractIrregularitySignals: {
          method: 'GET',
          url: 'api/projectDossier/:id/irregularitySignals',
          isArray: true
        },
        getContractIrregularities: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/irregularities',
          isArray: true
        },
        getContractInternalEnvironmentAudits: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/internalEnvironmentAudits',
          isArray: true
        },
        getContractTechnicalReportAudits: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/technicalReportAudits',
          isArray: true
        },
        getContractPhysicalExecutionActivities: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/physicalExecutionActivities',
          isArray: true
        },
        getContractPhysicalExecutionIndicators: {
          method: 'GET',
          url: 'api/projectDossier/:id/contract/:ind/physicalExecutionIndicators',
          isArray: true
        }
      }
    );
  }
];
