function NotificationSettingsModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  userNotificationSetting,
  attachedProgrammes,
  attachedProgrammePriorities,
  attachedProcedures,
  attachedContracts
) {
  $scope.isReadonly = scModalParams.isReadonly;
  $scope.userNotificationSetting = userNotificationSetting;
  $scope.attachedProgrammes = attachedProgrammes;
  $scope.attachedProgrammePriorities = attachedProgrammePriorities;
  $scope.attachedProcedures = attachedProcedures;
  $scope.attachedContracts = attachedContracts;

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

NotificationSettingsModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'userNotificationSetting',
  'attachedProgrammes',
  'attachedProgrammePriorities',
  'attachedProcedures',
  'attachedContracts'
];

NotificationSettingsModalCtrl.$resolve = {
  userNotificationSetting: [
    'scModalParams',
    'UserNotificationSetting',
    function(scModalParams, UserNotificationSetting) {
      return UserNotificationSetting.get({
        id: scModalParams.userId,
        notificationSettingId: scModalParams.notificationSetting.notificationSettingId
      }).$promise;
    }
  ],
  attachedProgrammes: [
    'scModalParams',
    'UserNotificationSetting',
    function(scModalParams, UserNotificationSetting) {
      return UserNotificationSetting.getProgrammes({
        id: scModalParams.userId,
        notificationSettingId: scModalParams.notificationSetting.notificationSettingId
      }).$promise;
    }
  ],
  attachedProgrammePriorities: [
    'scModalParams',
    'UserNotificationSetting',
    function(scModalParams, UserNotificationSetting) {
      return UserNotificationSetting.getPPriorities({
        id: scModalParams.userId,
        notificationSettingId: scModalParams.notificationSetting.notificationSettingId
      }).$promise;
    }
  ],
  attachedProcedures: [
    'scModalParams',
    'UserNotificationSetting',
    function(scModalParams, UserNotificationSetting) {
      return UserNotificationSetting.getProcedures({
        id: scModalParams.userId,
        notificationSettingId: scModalParams.notificationSetting.notificationSettingId
      }).$promise;
    }
  ],
  attachedContracts: [
    'scModalParams',
    'UserNotificationSetting',
    function(scModalParams, UserNotificationSetting) {
      return UserNotificationSetting.getContracts({
        id: scModalParams.userId,
        notificationSettingId: scModalParams.notificationSetting.notificationSettingId
      }).$promise;
    }
  ]
};

export { NotificationSettingsModalCtrl };
