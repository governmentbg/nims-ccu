export const FinancialCorrectionVersionFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/financialCorrectionVersions/:id/files/:fileKey');
  }
];
