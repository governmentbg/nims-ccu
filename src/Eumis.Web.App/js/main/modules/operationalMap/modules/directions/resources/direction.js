export const DirectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/directions/:id',
      {},
      {
        newDirection: {
          method: 'GET',
          url: 'api/directions/new'
        },
        canDelete: {
          method: 'POST',
          url: 'api/directions/:id/canDelete',
          isArray: true
        },
        getInfo: {
          method: 'GET',
          url: 'api/directions/:id/info'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/directions/:id/changeStatusToDraft'
        },
        changeStatusToEntered: {
          method: 'POST',
          url: 'api/directions/:id/changeStatusToEntered'
        }
      }
    );
  }
];
