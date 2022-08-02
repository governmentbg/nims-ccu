export const NotificationSettingAttachedProgrammeFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/notificationSettings/:id/attachedProgrammes/:ind',
      {},
      {
        getProgrammes: {
          method: 'GET',
          url: 'api/notificationSettings/:id/attachedProgrammes/programmes',
          isArray: true
        }
      }
    );
  }
];
