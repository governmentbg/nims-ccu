export const RequestPackageUserFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/requestPackages/:id/users/:ind',
      {},
      {
        changeStatusToChecked: {
          method: 'PUT',
          url: 'api/requestPackages/:id/users/:ind/changeStatusToChecked'
        },
        changeStatusToActive: {
          method: 'PUT',
          url: 'api/requestPackages/:id/users/:ind/changeStatusToActive'
        },
        changeStatusToRejected: {
          method: 'PUT',
          url: 'api/requestPackages/:id/users/:ind/changeStatusToRejected'
        }
      }
    );
  }
];
