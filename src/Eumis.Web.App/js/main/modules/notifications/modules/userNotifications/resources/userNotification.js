export const UserNotificationFactory = [
  '$resource',
  function($resource) {
    return $resource('api/notifications/:id', {}, {});
  }
];
