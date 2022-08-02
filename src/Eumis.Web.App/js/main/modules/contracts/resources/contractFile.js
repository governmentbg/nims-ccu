export const ContractFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/contracts/:id/files/:fileKey');
  }
];
