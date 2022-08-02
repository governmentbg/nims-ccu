export const ProjectMonitorstatResponseFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/projectMonitorstatResponse/:id/files/:fileKey');
  }
];
