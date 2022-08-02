export const UserPermissionFactory = [
  '$resource',
  function($resource) {
    return $resource('api/users/:id/permissions');
  }
];
