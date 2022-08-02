export const ContractReportTechnicalCorrectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportTechnicalCorrections/:id',
      {},
      {
        changeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReportTechnicalCorrections/:id/changeStatusToEnded'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReportTechnicalCorrections/:id/canChangeStatusToEnded'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReportTechnicalCorrections/:id/changeStatusToDraft'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contractReportTechnicalCorrections/canCreate'
        },
        existsEnded: {
          method: 'POST',
          url: 'api/contractReportTechnicalCorrections/existsEnded',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        },
        getInfo: {
          method: 'GET',
          url: 'api/contractReportTechnicalCorrections/:id/info'
        },
        canDelete: {
          method: 'POST',
          url: 'api/contractReportTechnicalCorrections/:id/canDelete'
        },
        getContractReportIndicators: {
          method: 'GET',
          url: 'api/contractReportTechnicalCorrections/:id/contractReportIndicators',
          isArray: true
        },
        getContractReportIndicator: {
          method: 'GET',
          url: 'api/contractReportTechnicalCorrections/:id/contractReportIndicators/:ind'
        },
        getContractReports: {
          method: 'GET',
          url: 'api/contractReportTechnicalCorrections/contractReports',
          isArray: true
        }
      }
    );
  }
];
