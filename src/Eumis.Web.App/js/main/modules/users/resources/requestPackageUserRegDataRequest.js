export const RegDataRequestFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/requestPackages/:id/users/:ind/regDataRequests/:index',
      {},
      {
        newRegDataRequest: {
          method: 'GET',
          url: 'api/requestPackages/:id/users/:ind/regDataRequests/new'
        },
        update: {
          method: 'PUT',
          url: 'api/requestPackages/:id/users/:ind/regDataRequests/update'
        }
      }
    );
  }
];
