export const ContractContractAccessCodeFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contracts/:id/contractAccessCodes/:ind',
      {},
      {
        activate: {
          method: 'POST',
          url: 'api/contracts/:id/contractAccessCodes/:ind/activate'
        },
        deactivate: {
          method: 'POST',
          url: 'api/contracts/:id/contractAccessCodes/:ind/deactivate'
        },
        getInfo: {
          method: 'GET',
          url: 'api/contracts/:id/contractAccessCodes/info'
        }
      }
    );
  }
];
