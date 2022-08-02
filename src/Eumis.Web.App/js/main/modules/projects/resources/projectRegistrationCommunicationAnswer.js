export const ProjectRegistrationCommunicationAnswerFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/projects/:id/managingAuthorityCommunications/:ind/answers/:aid',
      {},
      {
        canCreate: {
          method: 'POST',
          url: 'api/projects/:id/managingAuthorityCommunications/:ind/answers/canCreate'
        }
      }
    );
  }
];
