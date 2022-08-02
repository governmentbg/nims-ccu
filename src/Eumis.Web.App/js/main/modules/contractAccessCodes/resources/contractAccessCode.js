export const ContractAccessCodeFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractAccessCodes/:id',
      {},
      {
        getInfo: {
          method: 'GET',
          url: 'api/contractAccessCodes/info'
        }
      }
    );
  }
];
