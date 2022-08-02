export const ProcedurePrognosisFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedurePrognoses/:id',
      {},
      {
        newProcedurePrognosis: {
          url: 'api/procedurePrognoses/new',
          method: 'GET'
        },
        canCreate: {
          url: 'api/procedurePrognoses/canCreate',
          method: 'POST'
        },
        enter: {
          method: 'POST',
          url: 'api/procedurePrognoses/:id/enter'
        },
        setToDraft: {
          method: 'POST',
          url: 'api/procedurePrognoses/:id/setToDraft'
        },
        setToRemoved: {
          method: 'POST',
          url: 'api/procedurePrognoses/:id/setToRemoved'
        }
      }
    );
  }
];
