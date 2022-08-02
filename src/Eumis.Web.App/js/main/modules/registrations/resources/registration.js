export const RegistrationFactory = [
  '$resource',
  function($resource) {
    return $resource('api/registrations/:id');
  }
];
