export const RequestPackageFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/requestPackages/:id',
      {},
      {
        newRequestPackage: {
          method: 'GET',
          url: 'api/requestPackages/new'
        },
        changeStatusToDraft: {
          method: 'PUT',
          url: 'api/requestPackages/:id/changeStatusToDraft'
        },
        changeStatusToEntered: {
          method: 'PUT',
          url: 'api/requestPackages/:id/changeStatusToEntered'
        },
        changeStatusToChecked: {
          method: 'PUT',
          url: 'api/requestPackages/:id/changeStatusToChecked'
        },
        changeStatusToEnded: {
          method: 'PUT',
          url: 'api/requestPackages/:id/changeStatusToEnded'
        },
        changeStatusToCanceled: {
          method: 'PUT',
          url: 'api/requestPackages/:id/changeStatusToCanceled'
        },
        canChangeStatusToEntered: {
          method: 'POST',
          url: 'api/requestPackages/:id/canChangeStatusToEntered'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url: 'api/requestPackages/:id/canChangeStatusToEnded'
        },
        getInfo: {
          method: 'GET',
          url: 'api/requestPackages/:id/info'
        }
      }
    );
  }
];
