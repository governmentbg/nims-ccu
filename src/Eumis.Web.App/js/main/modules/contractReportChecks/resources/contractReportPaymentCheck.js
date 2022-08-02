export const ContractReportPaymentCheckFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReports/:id/paymentChecks/:ind',
      {},
      {
        changeStatusToActive: {
          method: 'POST',
          url: 'api/contractReports/:id/paymentChecks/:ind/changeStatusToActive'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contractReports/:id/paymentChecks/canCreate'
        },
        canChangeStatusToActive: {
          method: 'POST',
          url: 'api/contractReports/:id/paymentChecks/:ind/canChangeStatusToActive'
        },
        changeStatusToArchived: {
          method: 'POST',
          url: 'api/contractReports/:id/paymentChecks/:ind/changeStatusToArchived'
        },
        canChangeStatusToArchived: {
          method: 'POST',
          url: 'api/contractReports/:id/paymentChecks/:ind/canChangeStatusToArchived'
        },
        getCertReportPaymentContractReportPaymentCheck: {
          method: 'GET',
          url: 'api/certReports/:id/payments/:ind/check'
        },
        getCertReportAdvancePaymenyContractReportPaymentCheck: {
          method: 'GET',
          url: 'api/certReports/:id/advancePayments/:ind/check'
        }
      }
    );
  }
];
