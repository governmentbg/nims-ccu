export const AllowanceRateFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/allowances/:id/allowanceRates/:ind',
      {},
      {
        newAllowanceRate: {
          method: 'GET',
          url: 'api/allowances/:id/allowanceRates/new'
        },
        isCorrectDate: {
          method: 'POST',
          url: 'api/allowances/:id/allowanceRates/isCorrectDate',
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
