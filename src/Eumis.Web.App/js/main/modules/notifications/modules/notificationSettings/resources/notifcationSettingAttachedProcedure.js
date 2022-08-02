export const NotificationSettingAttachedProcedureFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/notificationSettings/:id/attachedProcedures/:ind',
      {},
      {
        getProcedures: {
          method: 'GET',
          url: 'api/notificationSettings/:id/attachedProcedures/procedures',
          isArray: true
        }
      }
    );
  }
];
