export const ProcedureFinancialReportDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/financialReportDocs/:ind',
      {},
      {
        newFinancialReportDocument: {
          method: 'GET',
          url: 'api/procedures/:id/financialReportDocs/new'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/procedures/:id/financialReportDocs/:ind/deactivate'
        },
        activate: {
          method: 'PUT',
          url: 'api/procedures/:id/financialReportDocs/:ind/activate'
        }
      }
    );
  }
];
