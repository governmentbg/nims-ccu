import angular from 'angular';

function ContractReportFinancialCorrectionCSDCtrl($scope, scFormParams, scModal, moneyOperation) {
  $scope.correctionTypeChanged = function() {
    $scope.model.irregularityId = null;
    $scope.model.financialCorrectionId = null;
  };

  $scope.approvedEdit = scFormParams.approvedEdit;
  $scope.certEdit = scFormParams.certEdit;

  $scope.calculate = function(type) {
    var modalInstance = scModal.open('bfpCalculatorModal', {
      contractId: $scope.model.contractReportFinancialCSDBudgetItem.contractId,
      contractBudgetLevel3AmountId:
        $scope.model.contractReportFinancialCSDBudgetItem.contractBudgetLevel3AmountId
    });

    modalInstance.result.then(function(result) {
      if (type === 'correctedUnapproved') {
        $scope.model.correctedUnapprovedEuAmount = result.euAmount;
        $scope.model.correctedUnapprovedBgAmount = result.bgAmount;
      } else if (type === 'correctedUnapprovedByCorrection') {
        $scope.model.correctedUnapprovedByCorrectionEuAmount = result.euAmount;
        $scope.model.correctedUnapprovedByCorrectionBgAmount = result.bgAmount;
      } else if (type === 'uncertifiedCorrectedApproved') {
        $scope.model.uncertifiedCorrectedApprovedEuAmount = result.euAmount;
        $scope.model.uncertifiedCorrectedApprovedBgAmount = result.bgAmount;
      }
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.changeSignNote = function(sign) {
    if (sign === 'positive') {
      $scope.noteLabel =
        'contractReportFinancialCorrections_correctionCSDBudgetItemForm_signPlusNote';
    } else if (sign === 'negative') {
      $scope.noteLabel =
        'contractReportFinancialCorrections_correctionCSDBudgetItemForm_signMinusNote';
    } else {
      $scope.noteLabel = null;
    }
  };

  $scope.changeSignNote($scope.model.sign);

  if ($scope.approvedEdit) {
    $scope.$watch(
      '[model.correctedUnapprovedEuAmount, model.correctedUnapprovedBgAmount]',
      function() {
        $scope.model.correctedUnapprovedBfpTotalAmount = moneyOperation.addAmounts(
          $scope.model.correctedUnapprovedEuAmount,
          $scope.model.correctedUnapprovedBgAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.correctedUnapprovedBfpTotalAmount, model.correctedUnapprovedSelfAmount]',
      function() {
        $scope.model.correctedUnapprovedTotalAmount = moneyOperation.addAmounts(
          $scope.model.correctedUnapprovedBfpTotalAmount,
          $scope.model.correctedUnapprovedSelfAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.correctedUnapprovedByCorrectionEuAmount, ' +
        'model.correctedUnapprovedByCorrectionBgAmount]',
      function() {
        $scope.model.correctedUnapprovedByCorrectionBfpTotalAmount = moneyOperation.addAmounts(
          $scope.model.correctedUnapprovedByCorrectionEuAmount,
          $scope.model.correctedUnapprovedByCorrectionBgAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.correctedUnapprovedByCorrectionBfpTotalAmount, ' +
        'model.correctedUnapprovedByCorrectionSelfAmount]',
      function() {
        $scope.model.correctedUnapprovedByCorrectionTotalAmount = moneyOperation.addAmounts(
          $scope.model.correctedUnapprovedByCorrectionBfpTotalAmount,
          $scope.model.correctedUnapprovedByCorrectionSelfAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.correctedUnapprovedEuAmount, model.correctedUnapprovedByCorrectionEuAmount]',
      function() {
        $scope.model.correctedApprovedEuAmount = moneyOperation.addAmounts(
          $scope.model.correctedUnapprovedEuAmount,
          $scope.model.correctedUnapprovedByCorrectionEuAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.correctedUnapprovedBgAmount, model.correctedUnapprovedByCorrectionBgAmount]',
      function() {
        $scope.model.correctedApprovedBgAmount = moneyOperation.addAmounts(
          $scope.model.correctedUnapprovedBgAmount,
          $scope.model.correctedUnapprovedByCorrectionBgAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.correctedUnapprovedBfpTotalAmount, ' +
        'model.correctedUnapprovedByCorrectionBfpTotalAmount]',
      function() {
        $scope.model.correctedApprovedBfpTotalAmount = moneyOperation.addAmounts(
          $scope.model.correctedUnapprovedBfpTotalAmount,
          $scope.model.correctedUnapprovedByCorrectionBfpTotalAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.correctedUnapprovedSelfAmount, ' + 'model.correctedUnapprovedByCorrectionSelfAmount]',
      function() {
        $scope.model.correctedApprovedSelfAmount = moneyOperation.addAmounts(
          $scope.model.correctedUnapprovedSelfAmount,
          $scope.model.correctedUnapprovedByCorrectionSelfAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.correctedUnapprovedTotalAmount, ' +
        'model.correctedUnapprovedByCorrectionTotalAmount]',
      function() {
        $scope.model.correctedApprovedTotalAmount = moneyOperation.addAmounts(
          $scope.model.correctedUnapprovedTotalAmount,
          $scope.model.correctedUnapprovedByCorrectionTotalAmount
        );
      },
      true
    );
  }
}

ContractReportFinancialCorrectionCSDCtrl.$inject = [
  '$scope',
  'scFormParams',
  'scModal',
  'moneyOperation'
];

export { ContractReportFinancialCorrectionCSDCtrl };
