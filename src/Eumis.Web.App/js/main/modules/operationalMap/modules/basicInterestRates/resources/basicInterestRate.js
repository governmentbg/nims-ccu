export const BasicInterestRateFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/basicInterestRates/:id',
      {},
      {
        newBasicInterestRate: {
          method: 'GET',
          url: 'api/basicInterestRates/new'
        },
        canDelete: {
          method: 'POST',
          url: 'api/basicInterestRates/:id/canDelete'
        }
      }
    );
  }
];
