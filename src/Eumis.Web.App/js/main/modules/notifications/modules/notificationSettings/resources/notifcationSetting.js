export const NotificationSettingFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/notificationSettings/:id',
      {},
      {
        newNotificationSetting: {
          method: 'GET',
          url: 'api/notificationSettings/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/notificationSettings/:id/info'
        },
        canChangeStatusToActual: {
          method: 'GET',
          url: 'api/notificationSettings/:id/canChangeStatusToActual'
        },
        canChangeStatusToDraft: {
          method: 'GET',
          url: 'api/notificationSettings/:id/canChangeStatusToDraft'
        },
        changeStatusToDraft: {
          method: 'GET',
          url: 'api/notificationSettings/:id/changeStatusToDraft'
        },
        changeStatusToActual: {
          method: 'GET',
          url: 'api/notificationSettings/:id/changeStatusToActual'
        },
        copyUserSettings: {
          method: 'GET',
          url: 'api/notificationSettings/copyUserSettings/:uid'
        }
      }
    );
  }
];
