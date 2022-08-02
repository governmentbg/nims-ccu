export const FinancialCorrectionVersionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/financialCorrections/:id/versions/:ind',
      {},
      {
        canCreate: {
          method: 'POST',
          url: 'api/financialCorrections/:id/versions/canCreate'
        },
        changeStatusToActual: {
          method: 'POST',
          url: 'api/financialCorrections/:id/versions/:ind/changeStatusToActual'
        },
        canChangeStatusToActual: {
          method: 'POST',
          url: 'api/financialCorrections/:id/versions/:ind/canChangeStatusToActual'
        },
        calculate: {
          method: 'POST',
          url: 'api/financialCorrections/:id/versions/:ind/calculate'
        }
      }
    );
  }
];
