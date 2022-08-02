export const ContractReportRevalidationFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/contractReportRevalidations/:id/files/:fileKey');
  }
];
