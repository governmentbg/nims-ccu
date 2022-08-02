export const ContractReportFinancialCorrectionCSDFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportFinancialCorrections/:id/financialCorrectionCSDs/:ind',
      {},
      {
        changeStatusToEnded: {
          method: 'POST',
          url:
            'api/contractReportFinancialCorrections/:id/financialCorrectionCSDs' +
            '/:ind/changeStatusToEnded'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url:
            'api/contractReportFinancialCorrections/:id/financialCorrectionCSDs' +
            '/:ind/canChangeStatusToEnded'
        },
        changeStatusToDraft: {
          method: 'POST',
          url:
            'api/contractReportFinancialCorrections/:id/financialCorrectionCSDs' +
            '/:ind/changeStatusToDraft'
        },
        getCertReportFinancialCorrectionsAttachedFinancialCorrectionCSDs: {
          method: 'GET',
          url: 'api/certReports/:id/financialCorrections/:ind/' + 'attachedFinancialCorrectionCSDs',
          isArray: true
        },
        getCertReportFinancialCorrectionsUnattachedFinancialCorrectionCSDs: {
          method: 'GET',
          url:
            'api/certReports/:id/financialCorrections/:ind/' + 'unattachedFinancialCorrectionCSDs',
          isArray: true
        },
        getAnnualAccountReportFinancialCorrectionsUnattachedFinancialCorrectionCSDs: {
          method: 'GET',
          url:
            'api/annualAccountReports/:id/financialCorrections/:ind/' +
            'unattachedFinancialCorrectionCSDs',
          isArray: true
        },
        getCertReportFinancialCorrectionsFinancialCorrectionCSD: {
          method: 'GET',
          url: 'api/certReports/:id/financialCorrections/:ind/financialCorrectionCSDs/:index'
        },
        getAnnualAccountReportFinancialCorrectionsFinancialCorrectionCSD: {
          method: 'GET',
          url:
            'api/annualAccountReports/:id/financialCorrections/:ind/financialCorrectionCSDs/:index'
        },
        getAnnualAccountReportFinancialCorrectionsAttachedFinancialCorrectionCSDs: {
          method: 'GET',
          url:
            'api/annualAccountReports/:id/financialCorrections/:ind/' +
            'attachedFinancialCorrectionCSDs',
          isArray: true
        },
        certUpdate: {
          method: 'PUT',
          url:
            'api/certReports/:id/financialCorrections/:ind/financialCorrectionCSDs/:index/' +
            'certUpdate'
        },
        changeCertStatusToEnded: {
          method: 'POST',
          url:
            'api/certReports/:id/financialCorrections/:ind/financialCorrectionCSDs/:index/' +
            'changeCertStatusToEnded'
        },
        changeCertStatusToDraft: {
          method: 'POST',
          url:
            'api/certReports/:id/financialCorrections/:ind/financialCorrectionCSDs/:index/' +
            'changeCertStatusToDraft'
        },
        canChangeCertStatusToEnded: {
          method: 'POST',
          url:
            'api/certReports/:id/financialCorrections/:ind/financialCorrectionCSDs/:index/' +
            'canChangeCertStatusToEnded'
        }
      }
    );
  }
];
