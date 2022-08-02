function AuditAuthorityCommunicationsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  contractCommunications
) {
  $scope.contractCommunications = contractCommunications;
  $scope.contractId = $stateParams.id;

  $scope.newContractCommunication = function() {
    return scConfirm({
      resource: 'AuditAuthorityCommunication',
      action: 'save',
      params: {
        id: $stateParams.id
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.auditAuthorityCommunications.view.communication.edit', {
          id: $stateParams.id,
          ind: result.result.communicationId
        });
      }
    });
  };
}

AuditAuthorityCommunicationsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractCommunications'
];

AuditAuthorityCommunicationsSearchCtrl.$resolve = {
  contractCommunications: [
    '$stateParams',
    'AuditAuthorityCommunication',
    function($stateParams, AuditAuthorityCommunication) {
      return AuditAuthorityCommunication.query({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { AuditAuthorityCommunicationsSearchCtrl };
