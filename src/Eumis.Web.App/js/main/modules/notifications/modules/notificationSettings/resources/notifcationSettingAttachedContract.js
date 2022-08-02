export const NotificationSettingAttachedContractFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/notificationSettings/:id/attachedContracts/:ind',
      {},
      {
        getContracts: {
          method: 'GET',
          url: 'api/notificationSettings/:id/attachedContracts/contracts',
          isArray: true
        }
      }
    );
  }
];
