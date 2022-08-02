export const ProcedureShareFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/shares/:ind',
      {},
      {
        newProcedureShare: {
          method: 'GET',
          url: 'api/procedures/:id/shares/new'
        }
      }
    );
  }
];
