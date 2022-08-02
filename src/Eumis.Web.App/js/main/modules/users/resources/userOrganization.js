export const UserOrganizationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/userOrganizations/:id',
      {},
      {
        newUserOrganization: {
          method: 'GET',
          url: 'api/userOrganizations/new'
        },
        canDelete: {
          method: 'POST',
          url: 'api/userOrganizations/:id/canDelete'
        }
      }
    );
  }
];
