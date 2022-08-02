export const IndicatorFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/indicators/:id',
      {},
      {
        newIndicator: {
          method: 'GET',
          url: 'api/indicators/new'
        },
        canDelete: {
          method: 'POST',
          url: 'api/indicators/:id/canDelete'
        },
        isUnique: {
          method: 'GET',
          url: 'api/indicators/isUnique',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        }
      }
    );
  }
];
