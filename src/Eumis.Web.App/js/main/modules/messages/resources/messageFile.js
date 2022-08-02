export const MessageFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/messages/:id/files/:fileKey');
  }
];
