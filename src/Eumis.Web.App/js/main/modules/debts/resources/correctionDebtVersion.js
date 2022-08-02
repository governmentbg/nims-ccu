export const CorrectionDebtVersionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/correctionDebts/:id/versions/:ind',
      {},
      {
        canCreate: {
          method: 'POST',
          url: 'api/correctionDebts/:id/versions/canCreate'
        },
        changeStatusToActual: {
          method: 'POST',
          url: 'api/correctionDebts/:id/versions/:ind/changeStatusToActual'
        },
        canChangeStatusToActual: {
          method: 'POST',
          url: 'api/correctionDebts/:id/versions/:ind/canChangeStatusToActual'
        }
      }
    );
  }
];
