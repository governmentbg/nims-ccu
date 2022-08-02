export const NewsFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/news/:id',
      {},
      {
        getNewsFeeds: {
          method: 'GET',
          url: 'api/newsFeed',
          isArray: true
        },
        getAllNews: {
          method: 'GET',
          url: 'api/allNews'
        },
        getNewsFeed: {
          method: 'GET',
          url: 'api/newsFeed/:id'
        },
        newNews: {
          method: 'GET',
          url: 'api/news/new'
        },
        newFile: {
          method: 'GET',
          url: 'api/news/newFile'
        },
        newPublication: {
          method: 'GET',
          url: 'api/news/:id/newPublication'
        },
        publish: {
          method: 'POST',
          url: 'api/news/:id/publish'
        },
        archive: {
          method: 'POST',
          url: 'api/news/:id/archive'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/news/:id/changeStatusToDraft'
        }
      }
    );
  }
];
