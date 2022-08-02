export const UserProfileFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/userProfile',
      {},
      {
        getDeclarations: {
          method: 'GET',
          url: 'api/userProfile/declarations',
          isArray: true
        },
        getPermissions: {
          method: 'GET',
          url: 'api/userProfile/permissions'
        },
        getRequests: {
          method: 'GET',
          url: 'api/userProfile/requests'
        },
        getUserInfo: {
          method: 'GET',
          url: 'api/userProfile/userInfo'
        }
      }
    );
  }
];
