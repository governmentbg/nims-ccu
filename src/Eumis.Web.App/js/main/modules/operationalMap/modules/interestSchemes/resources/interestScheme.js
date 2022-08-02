export const InterestSchemeFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/interestSchemes/:id',
      {},
      {
        newInterestScheme: {
          method: 'GET',
          url: 'api/interestSchemes/new'
        },
        canDelete: {
          method: 'POST',
          url: 'api/interestSchemes/:id/canDelete'
        }
      }
    );
  }
];
