export const ProjectCommunicationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/projects/:id/communications/:mid',
      {},
      {
        getCommunicationsForSession: {
          method: 'GET',
          url: 'api/evalSessions/:id/communications',
          isArray: true
        },
        getCommunicationsForSheet: {
          method: 'GET',
          url: 'api/evalSessionSheets/:id/communications',
          isArray: true
        },
        getCommunicationForSheet: {
          method: 'GET',
          url: 'api/evalSessionSheets/:id/communications/:mid'
        },
        getCommunicationsForStandpoint: {
          method: 'GET',
          url: 'api/evalSessionStandpoints/:id/communications',
          isArray: true
        },
        getCommunicationForStandpoint: {
          method: 'GET',
          url: 'api/evalSessionStandpoints/:id/communications/:mid'
        },
        cancelCommunication: {
          method: 'POST',
          url: 'api/projects/:id/communications/:mid/cancel'
        },
        canCreate: {
          method: 'POST',
          url: 'api/projects/:id/communications/canCreate'
        }
      }
    );
  }
];
