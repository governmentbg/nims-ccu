export const EvalSessionFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/evalSessions/:id/files/:fileKey');
  }
];
