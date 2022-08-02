function ChooseActuallyPaidAmountContractModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ActuallyPaidAmount,
  contracts
) {
  $scope.contracts = contracts;
  $scope.filters = {
    programmeId: scModalParams.programmeId,
    contractNumber: scModalParams.contractNumber
  };

  $scope.search = function() {
    return ActuallyPaidAmount.getContracts($scope.filters).$promise.then(function(result) {
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

ChooseActuallyPaidAmountContractModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ActuallyPaidAmount',
  'contracts'
];

ChooseActuallyPaidAmountContractModalCtrl.$resolve = {
  contracts: [
    'ActuallyPaidAmount',
    'scModalParams',
    function(ActuallyPaidAmount, scModalParams) {
      return ActuallyPaidAmount.getContracts(scModalParams).$promise;
    }
  ]
};

export { ChooseActuallyPaidAmountContractModalCtrl };
