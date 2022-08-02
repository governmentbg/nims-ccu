function ChooseCorrectionDebtContractModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  CorrectionDebt,
  contracts
) {
  $scope.contracts = contracts;
  $scope.filters = {
    programmeId: scModalParams.programmeId,
    contractNumber: scModalParams.contractNumber
  };

  $scope.search = function() {
    return CorrectionDebt.getContracts($scope.filters).$promise.then(function(result) {
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

ChooseCorrectionDebtContractModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'CorrectionDebt',
  'contracts'
];

ChooseCorrectionDebtContractModalCtrl.$resolve = {
  contracts: [
    'CorrectionDebt',
    'scModalParams',
    function(CorrectionDebt, scModalParams) {
      return CorrectionDebt.getContracts(scModalParams).$promise;
    }
  ]
};

export { ChooseCorrectionDebtContractModalCtrl };
