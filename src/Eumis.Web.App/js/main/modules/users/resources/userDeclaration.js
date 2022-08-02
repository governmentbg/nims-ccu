export const UserDeclarationFactory = [
  '$resource',
  function($resource) {
    return $resource('api/users/:id/declarations', {});
  }
];
