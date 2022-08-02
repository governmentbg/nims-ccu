import angular from 'angular';

function NotificationSettingAttachedProceduresCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  scConfirm,
  NotificationSettingAttachedProcedure,
  attachedProcedures
) {
  $scope.notificationSettingId = $stateParams.id;
  $scope.notificationSettingStatus = $scope.notificationSettingInfo.status;
  $scope.notificationSettingVersion = $scope.notificationSettingInfo.version;
  $scope.attachedProcedures = attachedProcedures;

  $scope.chooseItems = function() {
    var modalInstance = scModal.open('chooseNSProceduresModal', {
      notificationSettingId: $scope.notificationSettingId,
      version: $scope.notificationSettingVersion
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.delItem = function(attachedProcedureId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'NotificationSettingAttachedProcedure',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: attachedProcedureId,
        version: $scope.notificationSettingVersion
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

NotificationSettingAttachedProceduresCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'scConfirm',
  'NotificationSettingAttachedProcedure',
  'attachedProcedures'
];

NotificationSettingAttachedProceduresCtrl.$resolve = {
  attachedProcedures: [
    '$stateParams',
    'NotificationSettingAttachedProcedure',
    function($stateParams, NotificationSettingAttachedProcedures) {
      return NotificationSettingAttachedProcedures.query($stateParams).$promise;
    }
  ]
};

export { NotificationSettingAttachedProceduresCtrl };
