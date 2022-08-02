export const ContractCommunicationFactory = [
  '$resource',
  function($resource) {
    return $resource('api/contracts/:id/contractCommunications/:ind');
  }
];
