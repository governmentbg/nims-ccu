function ContractReportCorrectionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ContractReportCorrection,
  contractReportCorrection
) {
  $scope.editMode = null;
  $scope.contractReportCorrection = contractReportCorrection;
  $scope.status = $scope.contractReportCorrectionInfo.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editContractReportCorrectionData.$validate().then(function() {
      if ($scope.editContractReportCorrectionData.$valid) {
        return ContractReportCorrection.update(
          {
            id: $stateParams.id
          },
          $scope.contractReportCorrection
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

ContractReportCorrectionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractReportCorrection',
  'contractReportCorrection'
];

ContractReportCorrectionsEditCtrl.$resolve = {
  contractReportCorrection: [
    'ContractReportCorrection',
    '$stateParams',
    function(ContractReportCorrection, $stateParams) {
      return ContractReportCorrection.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportCorrectionsEditCtrl };
