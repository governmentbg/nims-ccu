export const EvalSessionStandpointFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/standpoints/:ind',
      {},
      {
        newStandpoint: {
          method: 'GET',
          url: 'api/evalSessions/:id/standpoints/new'
        },
        getForStandpointProject: {
          method: 'GET',
          url: 'api/evalSessionStandpoints/:id/projectStandpoints',
          isArray: true
        },
        getForSheet: {
          method: 'GET',
          url: 'api/evalSessionSheets/:id/standpoints',
          isArray: true
        },
        cancelStandpoint: {
          method: 'POST',
          url: 'api/evalSessions/:id/standpoints/:ind/cancelStandpoint'
        },
        canCreate: {
          method: 'POST',
          url: 'api/evalSessions/:id/standpoints/canCreate',
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
