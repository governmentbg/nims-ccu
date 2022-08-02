function ChooseReimbursedAmountContractModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ContractReimbursedAmount,
  contracts
) {
  $scope.contracts = contracts;
  $scope.filters = {
    programmeId: scModalParams.programmeId,
    contractNumber: scModalParams.contractNumber
  };

  $scope.search = function() {
    return ContractReimbursedAmount.getContracts($scope.filters).$promise.then(function(result) {
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

ChooseReimbursedAmountContractModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ContractReimbursedAmount',
  'contracts'
];

ChooseReimbursedAmountContractModalCtrl.$resolve = {
  contracts: [
    'ContractReimbursedAmount',
    'scModalParams',
    function(ContractReimbursedAmount, scModalParams) {
      return ContractReimbursedAmount.getContracts(scModalParams).$promise;
    }
  ]
};

export { ChooseReimbursedAmountContractModalCtrl };
