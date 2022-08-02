export const ProcedureIndicatorFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/indicators/:ind',
      {},
      {
        newAttachedIndicator: {
          method: 'GET',
          url: 'api/procedures/:id/indicators/newAttached'
        },
        newIndicator: {
          method: 'GET',
          url: 'api/procedures/:id/indicators/new'
        },
        createNewIndicator: {
          method: 'POST',
          url: 'api/procedures/:id/indicators/create'
        },
        hasIndicatorsForAttach: {
          method: 'GET',
          url: 'api/procedures/:id/indicators/hasIndicatorsForAttach',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        },
        deactivate: {
          method: 'PUT',
          url: 'api/procedures/:id/indicators/:ind/deactivate'
        },
        activate: {
          method: 'PUT',
          url: 'api/procedures/:id/indicators/:ind/activate'
        }
      }
    );
  }
];
