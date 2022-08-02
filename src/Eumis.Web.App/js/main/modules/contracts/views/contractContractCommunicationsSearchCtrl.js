function ContractContractCommunicationsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  scConfirm,
  contractCommunications
) {
  $scope.contractCommunications = contractCommunications;
  $scope.contractId = $stateParams.id;

  $scope.contractCommunicationsExportUrl = $interpolate(
    'api/contracts/{{contractId}}/contractCommunications/excelExport?'
  )({
    contractId: $scope.contractId
  });

  $scope.newContractCommunication = function() {
    return scConfirm({
      resource: 'ContractCommunication',
      action: 'save',
      params: {
        id: $stateParams.id
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contracts.view.communications.edit', {
          id: $stateParams.id,
          ind: result.result.communicationId
        });
      }
    });
  };
}

ContractContractCommunicationsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'scConfirm',
  'contractCommunications'
];

ContractContractCommunicationsSearchCtrl.$resolve = {
  contractCommunications: [
    '$stateParams',
    'ContractCommunication',
    function($stateParams, ContractCommunication) {
      return ContractCommunication.query({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractContractCommunicationsSearchCtrl };
