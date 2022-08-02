export const ContractReportAdvanceNVPaymentAmountFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReports/:id/advanceNVPaymentAmounts/:ind',
      {},
      {
        changeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReports/:id/advanceNVPaymentAmounts/:ind/changeStatusToEnded'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReports/:id/advanceNVPaymentAmounts/:ind/changeStatusToDraft'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReports/:id/advanceNVPaymentAmounts/:ind/canChangeStatusToEnded'
        },
        canChangeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReports/:id/advanceNVPaymentAmounts/:ind/canChangeStatusToDraft'
        }
      }
    );
  }
];
