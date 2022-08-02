export const ProcedureMassCommunicationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedureMassCommunications/:id',
      {},
      {
        newProcedureMassCommunications: {
          method: 'GET',
          url: 'api/procedureMassCommunications/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/procedureMassCommunications/:id/info'
        },
        canSend: {
          method: 'POST',
          url: 'api/procedureMassCommunications/:id/canSend'
        },
        send: {
          method: 'POST',
          url: 'api/procedureMassCommunications/:id/send'
        }
      }
    );
  }
];
