export const ContractReportTechnicalCorrectionIndicatorFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportTechnicalCorrections/:id/technicalCorrectionIndicators/:ind',
      {},
      {
        changeStatusToEnded: {
          method: 'POST',
          url:
            'api/contractReportTechnicalCorrections/:id/technicalCorrectionIndicators' +
            '/:ind/changeStatusToEnded'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url:
            'api/contractReportTechnicalCorrections/:id/technicalCorrectionIndicators' +
            '/:ind/canChangeStatusToEnded'
        },
        changeStatusToDraft: {
          method: 'POST',
          url:
            'api/contractReportTechnicalCorrections/:id/technicalCorrectionIndicators' +
            '/:ind/changeStatusToDraft'
        }
      }
    );
  }
];
