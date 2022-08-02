function ChooseContractDebtContractModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ContractDebt,
  contracts
) {
  $scope.contracts = contracts;
  $scope.filters = {
    programmeId: scModalParams.programmeId,
    contractNumber: scModalParams.contractNumber
  };

  $scope.search = function() {
    return ContractDebt.getContracts($scope.filters).$promise.then(function(result) {
      $scope.contracts = result;
    });
  };

  $scope.choose = function(contract) {
    return $uibModalInstance.close(contract);
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

ChooseContractDebtContractModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ContractDebt',
  'contracts'
];

ChooseContractDebtContractModalCtrl.$resolve = {
  contracts: [
    'ContractDebt',
    'scModalParams',
    function(ContractDebt, scModalParams) {
      return ContractDebt.getContracts(scModalParams).$promise;
    }
  ]
};

export { ChooseContractDebtContractModalCtrl };
