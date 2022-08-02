export const ContractReportTechnicalCheckFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReports/:id/technicalChecks/:ind',
      {},
      {
        changeStatusToActive: {
          method: 'POST',
          url: 'api/contractReports/:id/technicalChecks/:ind/changeStatusToActive'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contractReports/:id/technicalChecks/canCreate'
        },
        canChangeStatusToActive: {
          method: 'POST',
          url: 'api/contractReports/:id/technicalChecks/:ind/canChangeStatusToActive'
        }
      }
    );
  }
];
