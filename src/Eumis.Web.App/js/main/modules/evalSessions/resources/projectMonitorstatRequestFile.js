export const ProjectMonitorstatRequestFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/projectMonitorstatRequest/:id/files/:fileKey');
  }
];
