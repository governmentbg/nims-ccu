export const ContractVersionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contracts/:id/versions/:vid',
      {},
      {
        newContractVersion: {
          method: 'GET',
          url: 'api/contracts/:id/versions/new'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contracts/:id/versions/canCreate'
        },
        markAsChecked: {
          method: 'POST',
          url: 'api/contracts/:id/versions/:vid/markAsChecked'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contracts/:id/versions/:vid/changeStatusToDraft'
        },
        getSAPData: {
          method: 'GET',
          url: 'api/contracts/:id/versions/:vid/sapData'
        },
        technicalEdit: {
          method: 'PUT',
          url: 'api/contracts/:id/versions/:vid/technicalEdit'
        }
      }
    );
  }
];
