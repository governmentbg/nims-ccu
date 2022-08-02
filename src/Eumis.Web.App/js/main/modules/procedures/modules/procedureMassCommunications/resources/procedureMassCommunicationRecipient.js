export const ProcedureMassCommunicationRecipientFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedureMassCommunications/:id/recipients/:ind',
      {},
      {
        getUnattachedRecipients: {
          method: 'GET',
          url: 'api/procedureMassCommunications/:id/recipients/unattachedContracts',
          isArray: true
        }
      }
    );
  }
];
