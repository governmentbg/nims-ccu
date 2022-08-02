export const NewsFeedFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/newsFeed/:id/files/:fileKey');
  }
];
