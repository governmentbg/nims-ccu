import angular from 'angular';

function NotificationSettingAttachedProgrammePrioritiesCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  scConfirm,
  NotificationSettingAttachedProgrammePriority,
  attachedProgrammePriorities
) {
  $scope.notificationSettingId = $stateParams.id;
  $scope.notificationSettingStatus = $scope.notificationSettingInfo.status;
  $scope.notificationSettingVersion = $scope.notificationSettingInfo.version;
  $scope.attachedProgrammePriorities = attachedProgrammePriorities;

  $scope.chooseItems = function() {
    var modalInstance = scModal.open('chooseNSPPrioritiesModal', {
      notificationSettingId: $scope.notificationSettingId,
      version: $scope.notificationSettingVersion
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.delItem = function(attachedProgrammePriorityId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'NotificationSettingAttachedProgrammePriority',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: attachedProgrammePriorityId,
        version: $scope.notificationSettingVersion
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

NotificationSettingAttachedProgrammePrioritiesCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'scConfirm',
  'NotificationSettingAttachedProgrammePriority',
  'attachedProgrammePriorities'
];

NotificationSettingAttachedProgrammePrioritiesCtrl.$resolve = {
  attachedProgrammePriorities: [
    '$stateParams',
    'NotificationSettingAttachedProgrammePriority',
    function($stateParams, NotificationSettingAttachedProgrammePriorities) {
      return NotificationSettingAttachedProgrammePriorities.query($stateParams).$promise;
    }
  ]
};

export { NotificationSettingAttachedProgrammePrioritiesCtrl };
