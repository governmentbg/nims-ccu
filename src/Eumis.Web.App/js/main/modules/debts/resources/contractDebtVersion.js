export const ContractDebtVersionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractDebts/:id/versions/:ind',
      {},
      {
        canCreate: {
          method: 'POST',
          url: 'api/contractDebts/:id/versions/canCreate'
        },
        changeStatusToActual: {
          method: 'POST',
          url: 'api/contractDebts/:id/versions/:ind/changeStatusToActual'
        },
        canChangeStatusToActual: {
          method: 'POST',
          url: 'api/contractDebts/:id/versions/:ind/canChangeStatusToActual'
        },
        canUpdatePartial: {
          method: 'POST',
          url: 'api/contractDebts/:id/versions/:ind/canUpdatePartial'
        },
        updatePartial: {
          method: 'PUT',
          url: 'api/contractDebts/:id/versions/:ind/updatePartial'
        }
      }
    );
  }
];
