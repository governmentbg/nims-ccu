export const UserNotificationSettingFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/users/:id/notificationSettings/:notificationSettingId',
      {},
      {
        getProgrammes: {
          method: 'GET',
          url: 'api/users/:id/notificationSettings/:notificationSettingId/attachedProgrammes',
          isArray: true
        },
        getPPriorities: {
          method: 'GET',
          url:
            'api/users/:id/notificationSettings/:notificationSettingId/attachedProgrammePriorities',
          isArray: true
        },
        getProcedures: {
          method: 'GET',
          url: 'api/users/:id/notificationSettings/:notificationSettingId/attachedProcedures',
          isArray: true
        },
        getContracts: {
          method: 'GET',
          url: 'api/users/:id/notificationSettings/:notificationSettingId/attachedContracts',
          isArray: true
        }
      }
    );
  }
];
