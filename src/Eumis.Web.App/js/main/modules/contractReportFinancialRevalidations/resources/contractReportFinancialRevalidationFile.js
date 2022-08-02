export const ContractReportFinancialRevalidationFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/contractReportFinancialRevalidations/:id/files/:fileKey');
  }
];
