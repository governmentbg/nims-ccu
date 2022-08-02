export const ContractContractRegistrationFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/contracts/:id/registrationFiles/:fileKey');
  }
];
