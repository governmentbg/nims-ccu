import angular from 'angular';

function NotificationSettingAttachedProgrammesCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  scConfirm,
  NotificationSettingAttachedProgramme,
  attachedProgrammes
) {
  $scope.notificationSettingId = $stateParams.id;
  $scope.notificationSettingStatus = $scope.notificationSettingInfo.status;
  $scope.notificationSettingVersion = $scope.notificationSettingInfo.version;
  $scope.attachedProgrammes = attachedProgrammes;

  $scope.chooseItems = function() {
    var modalInstance = scModal.open('chooseNSProgrammesModal', {
      notificationSettingId: $scope.notificationSettingId,
      version: $scope.notificationSettingVersion
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.delItem = function(nomValueId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'NotificationSettingAttachedProgramme',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: nomValueId,
        version: $scope.notificationSettingVersion
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

NotificationSettingAttachedProgrammesCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'scConfirm',
  'NotificationSettingAttachedProgramme',
  'attachedProgrammes'
];

NotificationSettingAttachedProgrammesCtrl.$resolve = {
  attachedProgrammes: [
    '$stateParams',
    'NotificationSettingAttachedProgramme',
    function($stateParams, NotificationSettingAttachedProgrammes) {
      return NotificationSettingAttachedProgrammes.query($stateParams).$promise;
    }
  ]
};

export { NotificationSettingAttachedProgrammesCtrl };
