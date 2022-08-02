export const ContractReportCorrectionFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/contractReportCorrection/:id/files/:fileKey');
  }
];
