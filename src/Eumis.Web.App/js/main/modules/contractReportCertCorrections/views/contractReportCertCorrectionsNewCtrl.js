function ContractReportCertCorrectionsNewCtrl(
  $scope,
  $state,
  ContractReportCertCorrection,
  newContractReportCertCorrection
) {
  $scope.newContractReportCertCorrection = newContractReportCertCorrection;

  $scope.save = function() {
    return $scope.contractReportCertCorrectionsNewForm.$validate().then(function() {
      if ($scope.contractReportCertCorrectionsNewForm.$valid) {
        return ContractReportCertCorrection.save(
          $scope.newContractReportCertCorrection
        ).$promise.then(function(result) {
          $state.go('root.contractReportCertCorrections.view.basicData', {
            id: result.contractReportCertCorrectionId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractReportCertCorrections.search');
  };

  $scope.typeChanged = function() {
    $scope.newContractReportCertCorrection.programmeId = null;
    $scope.newContractReportCertCorrection.programmePriorityId = null;
    $scope.newContractReportCertCorrection.procedureId = null;
    $scope.newContractReportCertCorrection.contractId = null;
    $scope.newContractReportCertCorrection.contractReportPaymentId = null;
  };
}

ContractReportCertCorrectionsNewCtrl.$inject = [
  '$scope',
  '$state',
  'ContractReportCertCorrection',
  'newContractReportCertCorrection'
];

ContractReportCertCorrectionsNewCtrl.$resolve = {
  newContractReportCertCorrection: [
    'ContractReportCertCorrection',
    function(ContractReportCertCorrection) {
      return ContractReportCertCorrection.newContractReportCertCorrection().$promise;
    }
  ]
};

export { ContractReportCertCorrectionsNewCtrl };
