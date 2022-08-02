export const ProgrammePriorityPrognosisFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/programmePriorityPrognoses/:id',
      {},
      {
        newProgrammePriorityPrognosis: {
          url: 'api/programmePriorityPrognoses/new',
          method: 'GET'
        },
        canCreate: {
          url: 'api/programmePriorityPrognoses/canCreate',
          method: 'POST'
        },
        enter: {
          method: 'POST',
          url: 'api/programmePriorityPrognoses/:id/enter'
        },
        setToDraft: {
          method: 'POST',
          url: 'api/programmePriorityPrognoses/:id/setToDraft'
        },
        setToRemoved: {
          method: 'POST',
          url: 'api/programmePriorityPrognoses/:id/setToRemoved'
        }
      }
    );
  }
];
