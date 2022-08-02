import angular from 'angular';

function UserNotificationSettingsSearchCtrl(
  $scope,
  $stateParams,
  scModal,
  userNotificationSettings
) {
  $scope.userNotificationSettings = userNotificationSettings;

  $scope.viewRegDataRequest = function(userNotificationSetting) {
    var modalInstance = scModal.open('notificationSettingsModal', {
      notificationSetting: userNotificationSetting,
      userId: $stateParams.id,
      isReadonly: true
    });
    modalInstance.result.catch(angular.noop);
    return modalInstance.opened;
  };
}

UserNotificationSettingsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  'scModal',
  'userNotificationSettings'
];

UserNotificationSettingsSearchCtrl.$resolve = {
  userNotificationSettings: [
    '$stateParams',
    'UserNotificationSetting',
    function($stateParams, UserNotificationSetting) {
      return UserNotificationSetting.query($stateParams).$promise;
    }
  ]
};

export { UserNotificationSettingsSearchCtrl };
