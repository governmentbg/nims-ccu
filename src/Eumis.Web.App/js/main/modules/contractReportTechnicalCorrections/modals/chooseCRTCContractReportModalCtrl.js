function ChooseCRTCContractReportModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ContractReportTechnicalCorrection,
  contractReports
) {
  $scope.contractReports = contractReports;
  $scope.filters = {
    procedureId: scModalParams.procedureId,
    contractRegNum: scModalParams.contractRegNum,
    contractReportNum: scModalParams.contractReportNum
  };

  $scope.search = function() {
    return ContractReportTechnicalCorrection.getContractReports($scope.filters).$promise.then(
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

ChooseCRTCContractReportModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ContractReportTechnicalCorrection',
  'contractReports'
];

ChooseCRTCContractReportModalCtrl.$resolve = {
  contractReports: [
    'ContractReportTechnicalCorrection',
    'scModalParams',
    function(ContractReportTechnicalCorrection, scModalParams) {
      return ContractReportTechnicalCorrection.getContractReports(scModalParams).$promise;
    }
  ]
};

export { ChooseCRTCContractReportModalCtrl };
