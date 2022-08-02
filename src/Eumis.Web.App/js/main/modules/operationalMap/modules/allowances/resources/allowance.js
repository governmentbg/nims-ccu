export const AllowanceFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/allowances/:id',
      {},
      {
        newAllowance: {
          method: 'GET',
          url: 'api/allowances/new'
        },
        canDelete: {
          method: 'POST',
          url: 'api/allowances/:id/canDelete'
        }
      }
    );
  }
];
