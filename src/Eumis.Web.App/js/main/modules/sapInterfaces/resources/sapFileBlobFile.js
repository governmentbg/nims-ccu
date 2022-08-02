export const SapFileBlobFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/sapFiles/:id/blobFiles/:fileKey');
  }
];
