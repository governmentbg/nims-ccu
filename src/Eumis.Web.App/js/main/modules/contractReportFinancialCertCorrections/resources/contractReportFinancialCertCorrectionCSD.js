export const ContractReportFinancialCertCorrectionCSDFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportFinancialCertCorrections/:id/financialCertCorrectionCSDs/:ind',
      {},
      {
        changeStatusToEnded: {
          method: 'POST',
          url:
            'api/contractReportFinancialCertCorrections/:id/financialCertCorrectionCSDs' +
            '/:ind/changeStatusToEnded'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url:
            'api/contractReportFinancialCertCorrections/:id/financialCertCorrectionCSDs' +
            '/:ind/canChangeStatusToEnded'
        },
        changeStatusToDraft: {
          method: 'POST',
          url:
            'api/contractReportFinancialCertCorrections/:id/financialCertCorrectionCSDs' +
            '/:ind/changeStatusToDraft'
        },
        getCertReportFinancialCertCorrectionsAttachedFinancialCertCorrectionCSDs: {
          method: 'GET',
          url:
            'api/certReports/:id/financialCertCorrections/:ind/' +
            'attachedFinancialCertCorrectionCSDs',
          isArray: true
        },
        getCertReportFinancialCertCorrectionsUnattachedFinancialCertCorrectionCSDs: {
          method: 'GET',
          url:
            'api/certReports/:id/financialCertCorrections/:ind/' +
            'unattachedFinancialCertCorrectionCSDs',
          isArray: true
        },
        getCertReportFinancialCertCorrectionsFinancialCertCorrectionCSD: {
          method: 'GET',
          url:
            'api/certReports/:id/financialCertCorrections/:ind/financialCertCorrectionCSDs/' +
            ':index'
        }
      }
    );
  }
];
