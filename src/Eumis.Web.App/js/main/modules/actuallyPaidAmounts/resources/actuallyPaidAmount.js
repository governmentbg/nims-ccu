export const ActuallyPaidAmountFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/actuallyPaidAmounts/:id',
      {},
      {
        canCreate: {
          method: 'POST',
          url: 'api/actuallyPaidAmounts/canCreate'
        },
        getInfo: {
          method: 'GET',
          url: 'api/actuallyPaidAmounts/:id/info'
        },
        getBasicData: {
          method: 'GET',
          url: 'api/actuallyPaidAmounts/:id/data'
        },
        canChangeStatusToEntered: {
          method: 'POST',
          url: 'api/actuallyPaidAmounts/:id/canChangeStatusToEntered'
        },
        changeStatusToEntered: {
          method: 'POST',
          url: 'api/actuallyPaidAmounts/:id/changeStatusToEntered'
        },
        setToDraft: {
          method: 'POST',
          url: 'api/actuallyPaidAmounts/:id/setToDraft'
        },
        setToRemoved: {
          method: 'POST',
          url: 'api/actuallyPaidAmounts/:id/setToRemoved'
        },
        getContracts: {
          method: 'GET',
          url: 'api/actuallyPaidAmountContracts',
          isArray: true
        },
        getContractReportPayments: {
          method: 'GET',
          url: 'api/actuallyPaidAmounts/:id/contractReportPayments',
          isArray: true
        },
        assignContractReportPayment: {
          method: 'POST',
          url: 'api/actuallyPaidAmounts/:id/assignContractReportPayment'
        },
        changeContractReportPayment: {
          method: 'POST',
          url: 'api/actuallyPaidAmounts/:id/changeContractReportPayment'
        },
        dissociateContractReportPayment: {
          method: 'POST',
          url: 'api/actuallyPaidAmounts/:id/dissociateContractReportPayment'
        }
      }
    );
  }
];
