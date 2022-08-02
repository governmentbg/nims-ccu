function ChooseCRFRContractReportModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ContractReportFinancialRevalidation,
  contractReports
) {
  $scope.contractReports = contractReports;
  $scope.filters = {
    procedureId: scModalParams.procedureId,
    contractRegNum: scModalParams.contractRegNum,
    contractReportNum: scModalParams.contractReportNum
  };

  $scope.search = function() {
    return ContractReportFinancialRevalidation.getContractReports($scope.filters).$promise.then(
      function(result) {
        $scope.contractReports = result;
      }
    );
  };

  $scope.choose = function(contractReport) {
    return $uibModalInstance.close(contractReport);
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

ChooseCRFRContractReportModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ContractReportFinancialRevalidation',
  'contractReports'
];

ChooseCRFRContractReportModalCtrl.$resolve = {
  contractReports: [
    'ContractReportFinancialRevalidation',
    'scModalParams',
    function(ContractReportFinancialRevalidation, scModalParams) {
      return ContractReportFinancialRevalidation.getContractReports(scModalParams).$promise;
    }
  ]
};

export { ChooseCRFRContractReportModalCtrl };
