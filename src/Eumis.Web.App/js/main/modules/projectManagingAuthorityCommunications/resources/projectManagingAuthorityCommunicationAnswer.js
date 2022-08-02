export const ProjectManagingAuthorityCommunicationAnswerFactory = [
  '$resource',
  function($resource) {
    return $resource('api/projectManagingAuthorityCommunications/:id/answers/:ind');
  }
];
