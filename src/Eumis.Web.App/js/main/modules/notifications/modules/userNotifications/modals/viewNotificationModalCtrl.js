function ViewNotificationModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  UserNotification,
  notification,
  $interpolate,
  l10n,
  scConfirm
) {
  $scope.notification = notification;
  $scope.infoText = $interpolate(l10n.get('userNotifications_viewNotificationModal_subject'))({
    eventName: notification.eventName
  });

  $scope.deleteNotification = function(notificationId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'UserNotification',
      action: 'remove',
      params: {
        id: notificationId
      }
    }).then(function() {
      $uibModalInstance.close();
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.close();
  };
}

ViewNotificationModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'UserNotification',
  'notification',
  '$interpolate',
  'l10n',
  'scConfirm'
];

ViewNotificationModalCtrl.$resolve = {
  notification: [
    'UserNotification',
    'scModalParams',
    function(UserNotification, scModalParams) {
      return UserNotification.get({
        id: scModalParams.notificationSettingId
      }).$promise;
    }
  ]
};

export { ViewNotificationModalCtrl };
