export const PermissionRequestFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/requestPackages/:id/users/:ind/permissionRequests/:index',
      {},
      {
        newPermissionRequest: {
          method: 'GET',
          url: 'api/requestPackages/:id/users/:ind/permissionRequests/new'
        },
        userInfo: {
          method: 'GET',
          url: 'api/requestPackages/:id/users/:ind/permissionRequests/userInfo'
        },
        update: {
          method: 'PUT',
          url: 'api/requestPackages/:id/users/:ind/permissionRequests/update'
        }
      }
    );
  }
];
