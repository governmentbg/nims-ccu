export const ProcedureTimeLimitFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/timeLimits/:ind',
      {},
      {
        newProcedureTimeLimit: {
          method: 'GET',
          url: 'api/procedures/:id/timeLimits/new'
        },
        isValidEndTime: {
          method: 'GET',
          url: 'api/procedures/:id/timeLimits/isValidEndTime',
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
