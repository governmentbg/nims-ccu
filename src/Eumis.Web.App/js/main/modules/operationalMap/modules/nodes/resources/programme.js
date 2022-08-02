export const ProgrammeFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/programmes/:id',
      {},
      {
        newProgamme: {
          method: 'GET',
          url: 'api/programmes/new'
        },
        canCreate: {
          method: 'POST',
          url: 'api/programmes/canCreate'
        },
        canUpdate: {
          method: 'POST',
          url: 'api/programmes/:id/canUpdate'
        },
        canDelete: {
          method: 'POST',
          url: 'api/programmes/:id/canDelete'
        },
        canEnter: {
          method: 'POST',
          url: 'api/programmes/:id/canEnter'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/programmes/:id/changeStatusToDraft'
        },
        changeStatusToEntered: {
          method: 'POST',
          url: 'api/programmes/:id/changeStatusToEntered'
        },
        getInfo: {
          method: 'GET',
          url: 'api/programmes/:id/info'
        }
      }
    );
  }
];
