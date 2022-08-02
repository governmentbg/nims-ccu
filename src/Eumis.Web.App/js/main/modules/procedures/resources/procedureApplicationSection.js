export const ProcedureApplicationSectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/applicationSections',
      {},
      {
        getSections: {
          method: 'GET',
          url: 'api/procedures/:id/applicationSections',
          isArray: false
        }
      }
    );
  }
];
