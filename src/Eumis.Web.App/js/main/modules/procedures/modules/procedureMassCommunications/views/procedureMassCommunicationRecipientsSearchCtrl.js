import angular from 'angular';

function ProcedureMassCommunicationRecipientsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  scConfirm,
  recipients
) {
  $scope.communicationId = $stateParams.id;
  $scope.recipients = recipients;
  $scope.massCommunicationVersion = $scope.procedureMassCommunicationInfo.version;
  $scope.status = $scope.procedureMassCommunicationInfo.status;

  $scope.chooseItems = function() {
    var modalInstance = scModal.open('chooseRecipientsModal', {
      communicationId: $scope.communicationId,
      version: $scope.massCommunicationVersion
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.delItem = function(contractId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureMassCommunicationRecipient',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: contractId,
        version: $scope.massCommunicationVersion
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ProcedureMassCommunicationRecipientsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'scConfirm',
  'recipients'
];

ProcedureMassCommunicationRecipientsSearchCtrl.$resolve = {
  recipients: [
    '$stateParams',
    'ProcedureMassCommunicationRecipient',
    function($stateParams, ProcedureMassCommunicationRecipient) {
      return ProcedureMassCommunicationRecipient.query($stateParams).$promise;
    }
  ]
};

export { ProcedureMassCommunicationRecipientsSearchCtrl };
