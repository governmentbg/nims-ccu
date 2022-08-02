export const ContractGrantDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contracts/:id/grantDocuments/:ind',
      {},
      {
        newContractGrantDocument: {
          method: 'GET',
          url: 'api/contracts/:id/grantDocuments/new'
        }
      }
    );
  }
];
