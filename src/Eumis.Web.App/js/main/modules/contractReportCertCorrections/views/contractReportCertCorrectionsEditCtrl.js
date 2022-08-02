function ContractReportCertCorrectionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ContractReportCertCorrection,
  contractReportCertCorrection
) {
  $scope.editMode = null;
  $scope.contractReportCertCorrection = contractReportCertCorrection;
  $scope.status = $scope.contractReportCertCorrectionInfo.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editContractReportCertCorrectionData.$validate().then(function() {
      if ($scope.editContractReportCertCorrectionData.$valid) {
        return ContractReportCertCorrection.update(
          {
            id: $stateParams.id
          },
          $scope.contractReportCertCorrection
        ).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };
}

ContractReportCertCorrectionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractReportCertCorrection',
  'contractReportCertCorrection'
];

ContractReportCertCorrectionsEditCtrl.$resolve = {
  contractReportCertCorrection: [
    'ContractReportCertCorrection',
    '$stateParams',
    function(ContractReportCertCorrection, $stateParams) {
      return ContractReportCertCorrection.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportCertCorrectionsEditCtrl };
