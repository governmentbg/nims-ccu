function ContractDebtsNewStep2Ctrl($scope, $state, scConfirm, ContractDebt, newContractDebt) {
  $scope.newContractDebt = newContractDebt;

  $scope.save = function() {
    return $scope.contractDebtsNewStep2Form.$validate().then(function() {
      if ($scope.contractDebtsNewStep2Form.$valid) {
        return ContractDebt.save({}, $scope.newContractDebt).$promise.then(function(result) {
          return $state.go('root.contractDebts.view.edit', {
            id: result.contractDebtId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractDebts.search');
  };
}

ContractDebtsNewStep2Ctrl.$inject = [
  '$scope',
  '$state',
  'scConfirm',
  'ContractDebt',
  'newContractDebt'
];

ContractDebtsNewStep2Ctrl.$resolve = {
  newContractDebt: [
    'ContractDebt',
    '$stateParams',
    function(ContractDebt, $stateParams) {
      return ContractDebt.newContractDebt({
        contractNum: $stateParams.cNum
      }).$promise;
    }
  ]
};

export { ContractDebtsNewStep2Ctrl };
