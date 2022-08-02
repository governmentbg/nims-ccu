export const GuidanceNavFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/navGuidances/:id/files/:fileKey');
  }
];
