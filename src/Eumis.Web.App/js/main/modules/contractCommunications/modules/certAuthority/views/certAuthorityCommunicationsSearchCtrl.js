function CertAuthorityCommunicationsSearchCtrl(
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
      resource: 'CertAuthorityCommunication',
      action: 'save',
      params: {
        id: $stateParams.id
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.certAuthorityCommunications.view.communication.edit', {
          id: $stateParams.id,
          ind: result.result.communicationId
        });
      }
    });
  };
}

CertAuthorityCommunicationsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractCommunications'
];

CertAuthorityCommunicationsSearchCtrl.$resolve = {
  contractCommunications: [
    '$stateParams',
    'CertAuthorityCommunication',
    function($stateParams, CertAuthorityCommunication) {
      return CertAuthorityCommunication.query({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { CertAuthorityCommunicationsSearchCtrl };
