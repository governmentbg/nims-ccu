export const ContractRegistrationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractRegistrations/:id',
      {},
      {
        isUnique: {
          method: 'GET',
          url: 'api/contractRegistrations/isUnique',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        }
      }
    );
  }
];
