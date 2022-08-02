function ChooseFinancialCorrectionContractModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  FinancialCorrection,
  contracts
) {
  $scope.contracts = contracts;
  $scope.filters = {
    programmeId: scModalParams.programmeId,
    contractNumber: scModalParams.contractNumber
  };

  $scope.search = function() {
    return FinancialCorrection.getContracts($scope.filters).$promise.then(function(result) {
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

ChooseFinancialCorrectionContractModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'FinancialCorrection',
  'contracts'
];

ChooseFinancialCorrectionContractModalCtrl.$resolve = {
  contracts: [
    'FinancialCorrection',
    'scModalParams',
    function(FinancialCorrection, scModalParams) {
      return FinancialCorrection.getContracts(scModalParams).$promise;
    }
  ]
};

export { ChooseFinancialCorrectionContractModalCtrl };
