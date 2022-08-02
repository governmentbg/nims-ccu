export const ProgrammePriorityFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/programmePriorities/:id/files/:fileKey');
  }
];
