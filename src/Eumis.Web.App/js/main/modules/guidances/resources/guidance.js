export const GuidanceFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/guidances/:id',
      {},
      {
        newGuidance: {
          method: 'GET',
          url: 'api/guidances/new'
        },
        getNavGuidances: {
          method: 'GET',
          url: 'api/navGuidances',
          isArray: true
        }
      }
    );
  }
];
