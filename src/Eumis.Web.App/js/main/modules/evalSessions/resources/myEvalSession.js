export const MyEvalSessionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/myEvalSessions/:id',
      {},
      {
        getInfo: {
          method: 'GET',
          url: 'api/myEvalSessions/:id/info'
        }
      }
    );
  }
];
