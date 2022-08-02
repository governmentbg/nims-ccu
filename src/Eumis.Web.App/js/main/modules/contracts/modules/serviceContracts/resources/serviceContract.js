export const ServiceContractFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contracts/service',
      {},
      {
        newRegistration: {
          method: 'GET',
          url: 'api/contracts/service/new'
        }
      }
    );
  }
];
