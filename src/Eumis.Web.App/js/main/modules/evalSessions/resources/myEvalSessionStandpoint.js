export const MyEvalSessionStandpointFactory = [
  '$resource',
  function($resource) {
    return $resource('api/myEvalSessions/:id/standpoints/:ind');
  }
];
