export const ProgrammePriorityFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/programmePriorities/:id',
      {},
      {
        newProgrammePriority: {
          method: 'GET',
          url: 'api/programmePriorities/new'
        },
        canCreate: {
          method: 'POST',
          url: 'api/programmePriorities/canCreate'
        },
        canUpdate: {
          method: 'POST',
          url: 'api/programmePriorities/:id/canUpdate'
        },
        canDelete: {
          method: 'POST',
          url: 'api/programmePriorities/:id/canDelete'
        },
        canEnter: {
          method: 'POST',
          url: 'api/programmePriorities/:id/canEnter'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/programmePriorities/:id/changeStatusToDraft'
        },
        changeStatusToEntered: {
          method: 'POST',
          url: 'api/programmePriorities/:id/changeStatusToEntered'
        },
        getInfo: {
          method: 'GET',
          url: 'api/programmePriorities/:id/info'
        },
        getForProgramme: {
          method: 'GET',
          url: 'api/programmes/:id/programmePriorities',
          isArray: true
        }
      }
    );
  }
];
