function ChooseCRFCCContractReportModalCtrl(
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

ChooseCRFCCContractReportModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ContractReportFinancialCorrection',
  'contractReports'
];

ChooseCRFCCContractReportModalCtrl.$resolve = {
  contractReports: [
    'ContractReportFinancialCorrection',
    'scModalParams',
    function(ContractReportFinancialCorrection, scModalParams) {
      return ContractReportFinancialCorrection.getContractReports(scModalParams).$promise;
    }
  ]
};

export { ChooseCRFCCContractReportModalCtrl };
