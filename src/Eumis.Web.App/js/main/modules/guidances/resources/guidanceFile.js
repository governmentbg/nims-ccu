export const GuidanceFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/guidances/:id/files/:fileKey');
  }
];
