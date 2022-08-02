function ChooseContractReportContractModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ContractReport,
  contracts
) {
  $scope.contracts = contracts;
  $scope.filters = {
    procedureId: scModalParams.procedureId,
    contractNumber: scModalParams.contractNumber
  };

  $scope.search = function() {
    return ContractReport.getContracts($scope.filters).$promise.then(function(result) {
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

ChooseContractReportContractModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ContractReport',
  'contracts'
];

ChooseContractReportContractModalCtrl.$resolve = {
  contracts: [
    'ContractReport',
    'scModalParams',
    function(ContractReport, scModalParams) {
      return ContractReport.getContracts(scModalParams).$promise;
    }
  ]
};

export { ChooseContractReportContractModalCtrl };
