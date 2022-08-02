export const UserFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/users/:id',
      {},
      {
        isUniqueUin: {
          method: 'GET',
          url: 'api/users/isUniqueUin'
        },
        isUniqueUsername: {
          method: 'GET',
          url: 'api/users/isUniqueUsername'
        },
        newUser: {
          method: 'GET',
          url: 'api/users/new'
        },
        getUserInfo: {
          method: 'GET',
          url: 'api/users/userInfo'
        },
        changePassword: {
          method: 'PUT',
          url: 'api/users/changePassword'
        },
        isCorrectPassword: {
          method: 'POST',
          url: 'api/users/isCorrectPassword',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        },
        isSuperUser: {
          method: 'GET',
          url: 'api/users/isSuperUser',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        },
        deleteUser: {
          method: 'PUT',
          url: 'api/users/:id/deleteUser'
        },
        recover: {
          method: 'PUT',
          url: 'api/users/:id/recover'
        },
        canRecover: {
          method: 'POST',
          url: 'api/users/:id/canRecover'
        },
        lock: {
          method: 'PUT',
          url: 'api/users/:id/lock'
        },
        unlock: {
          method: 'PUT',
          url: 'api/users/:id/unlock'
        }
      }
    );
  }
];
