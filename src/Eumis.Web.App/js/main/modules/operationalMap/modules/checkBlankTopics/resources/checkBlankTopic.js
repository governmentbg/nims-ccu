export const CheckBlankTopicFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/checkBlankTopics/:id',
      {},
      {
        newCheckBlankTopic: {
          method: 'GET',
          url: 'api/checkBlankTopics/new'
        }
      }
    );
  }
];
