export const ProcedureTechnicalReportDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/technicalReportDocs/:ind',
      {},
      {
        newTechnicalReportDocument: {
          method: 'GET',
          url: 'api/procedures/:id/technicalReportDocs/new'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/procedures/:id/technicalReportDocs/:ind/deactivate'
        },
        activate: {
          method: 'PUT',
          url: 'api/procedures/:id/technicalReportDocs/:ind/activate'
        }
      }
    );
  }
];
