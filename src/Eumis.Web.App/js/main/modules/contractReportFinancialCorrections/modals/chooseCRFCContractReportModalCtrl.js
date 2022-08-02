function ChooseCRFCContractReportModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ContractReportFinancialCorrection,
  contractReports
) {
  $scope.contractReports = contractReports;
  $scope.filters = {
    procedureId: scModalParams.procedureId,
    contractRegNum: scModalParams.contractRegNum,
    contractReportNum: scModalParams.contractReportNum
  };

  $scope.search = function() {
    return ContractReportFinancialCorrection.getContractReports($scope.filters).$promise.then(
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

ChooseCRFCContractReportModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ContractReportFinancialCorrection',
  'contractReports'
];

ChooseCRFCContractReportModalCtrl.$resolve = {
  contractReports: [
    'ContractReportFinancialCorrection',
    'scModalParams',
    function(ContractReportFinancialCorrection, scModalParams) {
      return ContractReportFinancialCorrection.getContractReports(scModalParams).$promise;
    }
  ]
};

export { ChooseCRFCContractReportModalCtrl };
