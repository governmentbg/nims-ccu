export const InterestRateFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/basicInterestRates/:id/interestRates/:ind',
      {},
      {
        newInterestRate: {
          method: 'GET',
          url: 'api/basicInterestRates/:id/interestRates/new'
        },
        isCorrectDate: {
          method: 'POST',
          url: 'api/basicInterestRates/:id/interestRates/isCorrectDate',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        }
      }
    );
  }
];
