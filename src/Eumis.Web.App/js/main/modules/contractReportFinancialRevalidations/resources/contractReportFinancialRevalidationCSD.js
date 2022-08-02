export const ContractReportFinancialRevalidationCSDFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportFinancialRevalidations/:id/financialRevalidationCSDs/:ind',
      {},
      {
        changeStatusToEnded: {
          method: 'POST',
          url:
            'api/contractReportFinancialRevalidations/:id/financialRevalidationCSDs' +
            '/:ind/changeStatusToEnded'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url:
            'api/contractReportFinancialRevalidations/:id/financialRevalidationCSDs' +
            '/:ind/canChangeStatusToEnded'
        },
        changeStatusToDraft: {
          method: 'POST',
          url:
            'api/contractReportFinancialRevalidations/:id/financialRevalidationCSDs' +
            '/:ind/changeStatusToDraft'
        },
        getCertReportFinancialRevalidationsAttachedFinancialRevalidationCSDs: {
          method: 'GET',
          url:
            'api/certReports/:id/financialRevalidations/:ind/' +
            'attachedFinancialRevalidationCSDs',
          isArray: true
        },
        getCertReportFinancialRevalidationsUnattachedFinancialRevalidationCSDs: {
          method: 'GET',
          url:
            'api/certReports/:id/financialRevalidations/:ind/' +
            'unattachedFinancialRevalidationCSDs',
          isArray: true
        },
        getCertReportFinancialRevalidationsFinancialRevalidationCSD: {
          method: 'GET',
          url: 'api/certReports/:id/financialRevalidations/:ind/financialRevalidationCSDs/:index'
        },
        certUpdate: {
          method: 'PUT',
          url:
            'api/certReports/:id/financialRevalidations/:ind/financialRevalidationCSDs/' +
            ':index/certUpdate'
        },
        changeCertStatusToEnded: {
          method: 'POST',
          url:
            'api/certReports/:id/financialRevalidations/:ind/financialRevalidationCSDs/' +
            ':index/changeCertStatusToEnded'
        },
        changeCertStatusToDraft: {
          method: 'POST',
          url:
            'api/certReports/:id/financialRevalidations/:ind/financialRevalidationCSDs/' +
            ':index/changeCertStatusToDraft'
        },
        canChangeCertStatusToEnded: {
          method: 'POST',
          url:
            'api/certReports/:id/financialRevalidations/:ind/financialRevalidationCSDs/' +
            ':index/canChangeCertStatusToEnded'
        }
      }
    );
  }
];
