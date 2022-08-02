export const ContractReportCheckFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/contractReportChecks/:id/files/:fileKey');
  }
];
