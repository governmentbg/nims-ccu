export const RequestPackageFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/requestPackages/:id/files/:fileKey');
  }
];
