export const BfpCalculatorFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/bfpCalculator',
      {},
      {
        calculate: {
          method: 'POST',
          url: 'api/bfpCalculator/calculate'
        }
      }
    );
  }
];
