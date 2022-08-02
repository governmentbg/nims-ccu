export const ContractReportTechnicalFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReports/:id/technicals/:ind',
      {},
      {
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReports/:id/technicals/:ind/changeStatusToDraft'
        },
        changeStatusToReturned: {
          method: 'POST',
          url: 'api/contractReports/:id/technicals/:ind/changeStatusToReturned'
        },
        changeStatusToActual: {
          method: 'POST',
          url: 'api/contractReports/:id/technicals/:ind/changeStatusToActual'
        },
        canChangeStatusToReturned: {
          method: 'POST',
          url: 'api/contractReports/:id/technicals/:ind/canChangeStatusToReturned'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contractReports/:id/technicals/canCreate'
        }
      }
    );
  }
];
