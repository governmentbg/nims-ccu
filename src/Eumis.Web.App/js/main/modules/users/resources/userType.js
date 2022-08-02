export const UserTypeFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/userTypes/:id',
      {},
      {
        newUserType: {
          method: 'GET',
          url: 'api/userTypes/new'
        },
        canDelete: {
          method: 'POST',
          url: 'api/userTypes/:id/canDelete'
        }
      }
    );
  }
];
