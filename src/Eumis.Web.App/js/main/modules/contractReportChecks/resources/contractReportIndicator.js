export const ContractReportIndicatorFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReports/:id/indicators/:ind',
      {},
      {
        changeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReports/:id/indicators/:ind/changeStatusToEnded'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReports/:id/indicators/:ind/canChangeStatusToEnded'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReports/:id/indicators/:ind/changeStatusToDraft'
        }
      }
    );
  }
];
