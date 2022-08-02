export const IndicatorTypeFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/indicatorItemTypes/:id',
      {},
      {
        newIndicator: {
          method: 'GET',
          url: 'api/indicatorItemTypes/new'
        },
        canDelete: {
          method: 'POST',
          url: 'api/indicatorItemTypes/:id/canDelete'
        }
      }
    );
  }
];
