export const ProcurementFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/procurements/:id/files/:fileKey');
  }
];
