export const TestFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/test/:testparam/files/:fileKey');
  }
];
