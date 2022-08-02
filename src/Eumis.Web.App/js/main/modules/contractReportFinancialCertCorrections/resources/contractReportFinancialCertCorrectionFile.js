export const ContractReportFinancialCertCorrectionFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/contractReportFinancialCertCorrections/:id/files/:fileKey');
  }
];
