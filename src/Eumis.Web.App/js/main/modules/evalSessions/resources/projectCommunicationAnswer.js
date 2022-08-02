export const ProjectCommunicationAnswerFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/projects/:ind/communications/:mid/answers/:aid',
      {},
      {
        getCommunicationAnswersForSheet: {
          method: 'GET',
          url: 'api/evalSessionSheets/:id/communications/:mid/answers',
          isArray: true
        },
        getCommunicationAnswersForStandpoint: {
          method: 'GET',
          url: 'api/evalSessionStandpoints/:id/communications/:mid/answers',
          isArray: true
        },
        canRegisterAnswer: {
          method: 'POST',
          url: 'api/projects/:ind/communications/:mid/answers/:aid/canRegister'
        },
        registerAnswer: {
          method: 'POST',
          url: 'api/projects/:ind/communications/:mid/answers/:aid/register'
        }
      }
    );
  }
];
