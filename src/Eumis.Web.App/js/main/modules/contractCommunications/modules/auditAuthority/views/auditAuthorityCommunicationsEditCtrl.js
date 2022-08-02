function AuditAuthorityCommunicationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  contractCommunication
) {
  $scope.contractCommunication = contractCommunication;
  $scope.communicationId = $stateParams.ind;

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'AuditAuthorityCommunication',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractCommunication.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.auditAuthorityCommunications.view.communication.search');
      }
    });
  };

  $scope.communicationUpdated = function() {
    return $state.partialReload();
  };
}

AuditAuthorityCommunicationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractCommunication'
];

AuditAuthorityCommunicationsEditCtrl.$resolve = {
  contractCommunication: [
    'AuditAuthorityCommunication',
    '$stateParams',
    function(AuditAuthorityCommunication, $stateParams) {
      return AuditAuthorityCommunication.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { AuditAuthorityCommunicationsEditCtrl };
