export const FlatFinancialCorrectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/flatFinancialCorrections/:id',
      {},
      {
        newFlatFinancialCorrection: {
          method: 'GET',
          url: 'api/flatFinancialCorrections/new'
        },
        canDelete: {
          method: 'POST',
          url: 'api/flatFinancialCorrections/:id/canDelete'
        },
        changeStatusToActual: {
          method: 'POST',
          url: 'api/flatFinancialCorrections/:id/changeStatusToActual'
        },
        canChangeStatusToActual: {
          method: 'POST',
          url: 'api/flatFinancialCorrections/:id/canChangeStatusToActual'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/flatFinancialCorrections/:id/changeStatusToDraft'
        },
        canChangeStatusToDraft: {
          method: 'POST',
          url: 'api/flatFinancialCorrections/:id/canChangeStatusToDraft'
        },
        getInfo: {
          method: 'GET',
          url: 'api/flatFinancialCorrections/:id/info'
        },
        getCorrectionDebtInfo: {
          method: 'GET',
          url: 'api/flatFinancialCorrections/:id/correctionDebtInfo'
        }
      }
    );
  }
];
