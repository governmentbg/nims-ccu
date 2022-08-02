function ContractDebtVersionsCtrl($scope, $state, $stateParams, scConfirm, contractDebtVersions) {
  $scope.contractDebtVersions = contractDebtVersions;
  $scope.contractDebtIsDeleted = $scope.contractDebtInfo.status === 'removed';
  $scope.contractDebtId = $stateParams.id;

  $scope.newAmendment = function() {
    return scConfirm({
      validationAction: 'canCreate',
      resource: 'ContractDebtVersion',
      action: 'save',
      params: {
        id: $stateParams.id
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractDebts.view.versions.edit', {
          ind: result.result.contractDebtVersionId
        });
      }
    });
  };
}

ContractDebtVersionsCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractDebtVersions'
];

ContractDebtVersionsCtrl.$resolve = {
  contractDebtVersions: [
    '$stateParams',
    'ContractDebtVersion',
    function($stateParams, ContractDebtVersion) {
      return ContractDebtVersion.query($stateParams).$promise;
    }
  ]
};

export { ContractDebtVersionsCtrl };
