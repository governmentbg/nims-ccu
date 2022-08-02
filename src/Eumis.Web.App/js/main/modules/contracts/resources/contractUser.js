export const ContractUserFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contracts/:id/users/:ind',
      {},
      {
        newRegistration: {
          method: 'GET',
          url: 'api/contracts/:id/users/new'
        }
      }
    );
  }
];
