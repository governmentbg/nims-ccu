export const ContractContractRegistrationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contracts/:id/registrations/:ind',
      {},
      {
        newRegistration: {
          method: 'GET',
          url: 'api/contracts/:id/registrations/new'
        },
        createNewRegistration: {
          method: 'POST',
          url: 'api/contracts/:id/registrations/create'
        },
        activate: {
          method: 'PUT',
          url: 'api/contracts/:id/registrations/:ind/activate'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/contracts/:id/registrations/:ind/deactivate'
        }
      }
    );
  }
];
