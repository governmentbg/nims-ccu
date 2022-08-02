function ContractContractCommunicationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractCommunication,
  contractCommunication
) {
  $scope.contractCommunication = contractCommunication;
  $scope.communicationId = $stateParams.ind;

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractCommunication',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractCommunication.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contracts.view.communications.search');
      }
    });
  };

  $scope.communicationUpdated = function() {
    return $state.partialReload();
  };

  $scope.back = function() {
    return $state.go('root.contracts.view.communications.search');
  };
}

ContractContractCommunicationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractCommunication',
  'contractCommunication'
];

ContractContractCommunicationsEditCtrl.$resolve = {
  contractCommunication: [
    'ContractCommunication',
    '$stateParams',
    function(ContractCommunication, $stateParams) {
      return ContractCommunication.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractContractCommunicationsEditCtrl };
