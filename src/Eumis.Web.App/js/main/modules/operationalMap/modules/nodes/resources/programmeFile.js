export const ProgrammeFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/programmes/:id/files/:fileKey');
  }
];
