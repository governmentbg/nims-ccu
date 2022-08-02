export const MeasureFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/measures/:id',
      {},
      {
        newMeasure: {
          method: 'GET',
          url: 'api/measures/new'
        },
        canDelete: {
          method: 'POST',
          url: 'api/measures/:id/canDelete'
        }
      }
    );
  }
];
