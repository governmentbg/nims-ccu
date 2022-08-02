import angular from 'angular';

function NotificationSettingAttachedContractsCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  scConfirm,
  NotificationSettingAttachedContract,
  attachedContracts
) {
  $scope.notificationSettingId = $stateParams.id;
  $scope.notificationSettingStatus = $scope.notificationSettingInfo.status;
  $scope.notificationSettingVersion = $scope.notificationSettingInfo.version;
  $scope.attachedContracts = attachedContracts;

  $scope.chooseItems = function() {
    var modalInstance = scModal.open('chooseNSContractsModal', {
      notificationSettingId: $scope.notificationSettingId,
      version: $scope.notificationSettingVersion
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.delItem = function(attachedContractId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'NotificationSettingAttachedContract',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: attachedContractId,
        version: $scope.notificationSettingVersion
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

NotificationSettingAttachedContractsCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'scConfirm',
  'NotificationSettingAttachedContract',
  'attachedContracts'
];

NotificationSettingAttachedContractsCtrl.$resolve = {
  attachedContracts: [
    '$stateParams',
    'NotificationSettingAttachedContract',
    function($stateParams, NotificationSettingAttachedContracts) {
      return NotificationSettingAttachedContracts.query($stateParams).$promise;
    }
  ]
};

export { NotificationSettingAttachedContractsCtrl };
