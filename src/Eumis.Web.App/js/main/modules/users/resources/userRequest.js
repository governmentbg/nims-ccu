export const UserRequestFactory = [
  '$resource',
  function($resource) {
    return $resource('api/users/:id/requests');
  }
];
