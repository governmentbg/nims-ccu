export const ProcedureAppGuidelineFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/appGuidelines/:ind',
      {},
      {
        newAppGuideline: {
          method: 'GET',
          url: 'api/procedures/:id/appGuidelines/new'
        }
      }
    );
  }
];
