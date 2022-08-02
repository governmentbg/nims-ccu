export const ContractReportCertCorrectionFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/contractReportCertCorrection/:id/files/:fileKey');
  }
];
