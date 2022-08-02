function NotificationSettingsEditCtrl(
  $scope,
  $state,
  $stateParams,
  NotificationSetting,
  notificationSetting,
  $interpolate,
  l10n,
  scConfirm
) {
  $scope.editMode = null;
  $scope.notificationSetting = notificationSetting;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editNotificationSetting.$validate().then(function() {
      if ($scope.editNotificationSetting.$valid) {
        return NotificationSetting.update(
          {
            id: $stateParams.id
          },
          $scope.notificationSetting
        ).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.changeStatus = function(status) {
    var validationAction = 'canChangeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      confirmMsg = $interpolate(
        l10n.get('notificationSetting_notificationSettingsEdit_confirmChangeStatus')
      )({
        status: l10n.get('notificationSetting_notificationSettingsEdit_' + status)
      });

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'NotificationSetting',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        version: notificationSetting.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'NotificationSetting',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.notificationSetting.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.notificationSettings.search');
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };
}

NotificationSettingsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'NotificationSetting',
  'notificationSetting',
  '$interpolate',
  'l10n',
  'scConfirm'
];

NotificationSettingsEditCtrl.$resolve = {
  notificationSetting: [
    'NotificationSetting',
    '$stateParams',
    function(NotificationSetting, $stateParams) {
      return NotificationSetting.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { NotificationSettingsEditCtrl };
