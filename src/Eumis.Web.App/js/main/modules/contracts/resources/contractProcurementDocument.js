export const ContractProcurementDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contracts/:id/procurementDocuments/:ind',
      {},
      {
        newContractProcurementDocument: {
          method: 'GET',
          url: 'api/contracts/:id/procurementDocuments/new'
        }
      }
    );
  }
];
