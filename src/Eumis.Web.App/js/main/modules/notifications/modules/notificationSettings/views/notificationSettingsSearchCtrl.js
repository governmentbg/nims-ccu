import angular from 'angular';

function NotificationSettingsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  $interpolate,
  notificationSettings
) {
  $scope.notificationSettings = notificationSettings;
  $scope.copyItems = function() {
    var modalInstance = scModal.open('chooseUserModal', {});

    modalInstance.result.then(function() {
      return $state.reload();
    }, angular.noop);

    return modalInstance.opened;
  };
}

NotificationSettingsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  '$interpolate',
  'notificationSettings'
];

NotificationSettingsSearchCtrl.$resolve = {
  notificationSettings: [
    '$stateParams',
    'NotificationSetting',
    function($stateParams, NotificationSetting) {
      return NotificationSetting.query($stateParams).$promise;
    }
  ]
};

export { NotificationSettingsSearchCtrl };
