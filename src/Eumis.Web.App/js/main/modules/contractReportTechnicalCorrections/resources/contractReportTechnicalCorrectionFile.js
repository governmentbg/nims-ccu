export const ContractReportTechnicalCorrectionFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/contractReportTechnicalCorrections/:id/files/:fileKey');
  }
];
