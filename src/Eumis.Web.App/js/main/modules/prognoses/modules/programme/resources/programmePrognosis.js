export const ProgrammePrognosisFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/programmePrognoses/:id',
      {},
      {
        newProgrammePrognosis: {
          url: 'api/programmePrognoses/new',
          method: 'GET'
        },
        canCreate: {
          url: 'api/programmePrognoses/canCreate',
          method: 'POST'
        },
        enter: {
          method: 'POST',
          url: 'api/programmePrognoses/:id/enter'
        },
        setToDraft: {
          method: 'POST',
          url: 'api/programmePrognoses/:id/setToDraft'
        },
        setToRemoved: {
          method: 'POST',
          url: 'api/programmePrognoses/:id/setToRemoved'
        }
      }
    );
  }
];
