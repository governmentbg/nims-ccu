function NotificationSettingsNewCtrl($scope, $state, NotificationSetting, newNotificationSetting) {
  $scope.newNotificationSetting = newNotificationSetting;

  $scope.save = function() {
    return $scope.newNotificationSettingForm.$validate().then(function() {
      if ($scope.newNotificationSettingForm.$valid) {
        return NotificationSetting.save($scope.newNotificationSetting).$promise.then(function(
          result
        ) {
          return $state.go('root.notificationSettings.view.edit', {
            id: result.notificationSettingId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.notificationSettings.search');
  };
}

NotificationSettingsNewCtrl.$inject = [
  '$scope',
  '$state',
  'NotificationSetting',
  'newNotificationSetting'
];

NotificationSettingsNewCtrl.$resolve = {
  newNotificationSetting: [
    'NotificationSetting',
    function(NotificationSetting) {
      return NotificationSetting.newNotificationSetting().$promise;
    }
  ]
};

export { NotificationSettingsNewCtrl };
