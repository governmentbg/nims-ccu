export const ContractReportFinancialCorrectionFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/contractReportFinancialCorrections/:id/files/:fileKey');
  }
];
