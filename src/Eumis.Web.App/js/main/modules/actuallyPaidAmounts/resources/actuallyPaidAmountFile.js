export const PaidAmountFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/actuallyPaidAmounts/:id/files/:fileKey');
  }
];
