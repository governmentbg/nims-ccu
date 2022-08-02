export const FlatFinancialCorrectionFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/flatFinancialCorrections/:id/files/:fileKey');
  }
];
