export const NotificationSettingAttachedProgrammePriorityFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/notificationSettings/:id/attachedProgrammePriorities/:ind',
      {},
      {
        getPPriorities: {
          method: 'GET',
          url: 'api/notificationSettings/:id/attachedProgrammePriorities/pPriorities',
          isArray: true
        }
      }
    );
  }
];
