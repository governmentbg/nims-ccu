function NotificationSettingsViewCtrl($scope, $interpolate, l10n, notificationSettingInfo) {
  $scope.notificationSettingInfo = notificationSettingInfo;

  $scope.infoText = $interpolate(l10n.get('notificationSetting_notificationSettingView_info'))({
    status: notificationSettingInfo.statusDescr
  });

  $scope.tabList = {
    notificationSetting_tabs_edit: 'root.notificationSettings.view.edit'
  };

  if ($scope.notificationSettingInfo.scope === 'contract') {
    $scope.tabList['notificationSetting_tabs_contract'] =
      'root.notificationSettings.view.attachedContracts';
  }

  if ($scope.notificationSettingInfo.scope === 'procedure') {
    $scope.tabList['notificationSetting_tabs_procedure'] =
      'root.notificationSettings.view.attachedProcedures';
  }

  if ($scope.notificationSettingInfo.scope === 'programmePriority') {
    $scope.tabList['notificationSetting_tabs_programmePriorities'] =
      'root.notificationSettings.view.attachedProgrammePriorities';
  }

  if ($scope.notificationSettingInfo.scope === 'programme') {
    $scope.tabList['notificationSetting_tabs_programmes'] =
      'root.notificationSettings.view.attachedProgrammes';
  }
}

NotificationSettingsViewCtrl.$inject = [
  '$scope',
  '$interpolate',
  'l10n',
  'notificationSettingInfo'
];

NotificationSettingsViewCtrl.$resolve = {
  notificationSettingInfo: [
    'NotificationSetting',
    '$stateParams',
    function(NotificationSetting, $stateParams) {
      return NotificationSetting.getInfo({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { NotificationSettingsViewCtrl };
