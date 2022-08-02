export const ContractReportPaymentRequestFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReports/:id/paymentRequests/:ind',
      {},
      {
        getContractBudgetTree: {
          method: 'GET',
          url: 'api/contractReports/:id/paymentRequests/tree'
        }
      }
    );
  }
];
