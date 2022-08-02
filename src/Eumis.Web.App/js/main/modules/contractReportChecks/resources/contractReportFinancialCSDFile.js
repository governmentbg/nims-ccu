export const ContractReportFinancialCSDFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/contractReportFinancialCSDFiles/:id/files/:fileKey');
  }
];
