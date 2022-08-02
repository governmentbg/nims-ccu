export const ProgrammeApplicationDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/programmes/:id/applicationDocuments/:ind',
      {},
      {
        newDocument: {
          method: 'GET',
          url: 'api/programmes/:id/applicationDocuments/new'
        },
        canAdd: {
          method: 'POST',
          url: 'api/programmes/:id/applicationDocuments/canAdd'
        },
        canLoadDocuments: {
          method: 'POST',
          url: 'api/programmes/:id/applicationDocuments/canLoad'
        },
        canDelete: {
          method: 'POST',
          url: 'api/programmes/:id/applicationDocuments/:ind/canDelete'
        },
        loadDocuments: {
          method: 'POST',
          url: 'api/programmes/:id/applicationDocuments/load'
        },
        activate: {
          method: 'PUT',
          url: 'api/programmes/:id/applicationDocuments/:ind/activate'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/programmes/:id/applicationDocuments/:ind/deactivate'
        },
        getRelatedProcedures: {
          method: 'GET',
          url: 'api/programmes/:id/applicationDocuments/:ind/relatedProcedures',
          isArray: true
        }
      }
    );
  }
];
