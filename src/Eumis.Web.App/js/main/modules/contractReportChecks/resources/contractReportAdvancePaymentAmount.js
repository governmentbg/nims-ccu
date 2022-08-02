export const ContractReportAdvancePaymentAmountFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReports/:id/advancePaymentAmounts/:ind',
      {},
      {
        changeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReports/:id/advancePaymentAmounts/:ind/changeStatusToEnded'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReports/:id/advancePaymentAmounts/:ind/changeStatusToDraft'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReports/:id/advancePaymentAmounts/:ind/canChangeStatusToEnded'
        },
        canChangeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReports/:id/advancePaymentAmounts/:ind/canChangeStatusToDraft'
        },
        getCertReportContractReportAttachedAdvancePaymentAmounts: {
          method: 'GET',
          url: 'api/certReports/:id/advancedPayments/:ind/attachedAmounts',
          isArray: true
        },
        getCertReportContractReportUnattachedAdvancePaymentAmounts: {
          method: 'GET',
          url: 'api/certReports/:id/advancedPayments/:ind/unattachedamounts',
          isArray: true
        },
        getCertReportContractReportAdvancePaymentAmount: {
          method: 'GET',
          url: 'api/certReports/:id/advancedPayments/:ind/amounts/:index'
        },
        certUpdate: {
          method: 'PUT',
          url: 'api/certReports/:id/advancedPayments/:ind/amounts/:index/certUpdate'
        },
        changeCertStatusToEnded: {
          method: 'POST',
          url: 'api/certReports/:id/advancedPayments/:ind/amounts/:index/changeCertStatusToEnded'
        },
        changeCertStatusToDraft: {
          method: 'POST',
          url: 'api/certReports/:id/advancedPayments/:ind/amounts/:index/changeCertStatusToDraft'
        },
        canChangeCertStatusToEnded: {
          method: 'POST',
          url: 'api/certReports/:id/advancedPayments/:ind/amounts/:index/canChangeCertStatusToEnded'
        }
      }
    );
  }
];
