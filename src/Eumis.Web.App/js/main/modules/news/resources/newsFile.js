export const NewsFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/news/:id/files/:fileKey');
  }
];
