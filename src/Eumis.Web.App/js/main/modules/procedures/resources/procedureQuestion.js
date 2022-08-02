export const ProcedureQuestionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/questions/:ind',
      {},
      {
        newQuestion: {
          method: 'GET',
          url: 'api/procedures/:id/questions/new'
        }
      }
    );
  }
];
