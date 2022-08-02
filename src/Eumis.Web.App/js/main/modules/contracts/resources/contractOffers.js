export const ContractOffersFactory = [
  '$resource',
  function($resource) {
    return $resource('api/contracts/:id/offers/:oid', {}, {});
  }
];
