function ContractReportCorrectionsNewCtrl(
  $scope,
  $state,
  ContractReportCorrection,
  newContractReportCorrection
) {
  $scope.newContractReportCorrection = newContractReportCorrection;

  $scope.save = function() {
    return $scope.contractReportCorrectionsNewForm.$validate().then(function() {
      if ($scope.contractReportCorrectionsNewForm.$valid) {
        return ContractReportCorrection.save($scope.newContractReportCorrection).$promise.then(
          function(result) {
            $state.go('root.contractReportCorrections.view.basicData', {
              id: result.contractReportCorrectionId
            });
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractReportCorrections.search');
  };

  $scope.typeChanged = function() {
    $scope.newContractReportCorrection.programmeId = null;
    $scope.newContractReportCorrection.programmePriorityId = null;
    $scope.newContractReportCorrection.procedureId = null;
    $scope.newContractReportCorrection.contractId = null;
    $scope.newContractReportCorrection.contractReportPaymentId = null;
  };

  $scope.changeSignNote = function(sign) {
    if (sign === 'positive') {
      $scope.noteLabel = 'contractReportCorrections_new_signPlusNote';
    } else if (sign === 'negative') {
      $scope.noteLabel = 'contractReportCorrections_new_signMinusNote';
    } else {
      $scope.noteLabel = null;
    }
  };
}

ContractReportCorrectionsNewCtrl.$inject = [
  '$scope',
  '$state',
  'ContractReportCorrection',
  'newContractReportCorrection'
];

ContractReportCorrectionsNewCtrl.$resolve = {
  newContractReportCorrection: [
    'ContractReportCorrection',
    function(ContractReportCorrection) {
      return ContractReportCorrection.newContractReportCorrection().$promise;
    }
  ]
};

export { ContractReportCorrectionsNewCtrl };
