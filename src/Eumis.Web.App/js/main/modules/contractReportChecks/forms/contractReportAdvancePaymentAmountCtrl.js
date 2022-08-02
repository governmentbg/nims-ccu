import angular from 'angular';

function ContractReportAdvancePaymentAmountCtrl($scope, scFormParams, scModal, moneyOperation) {
  $scope.calculate = function(type) {
    var modalInstance = scModal.open('bfpCalculatorModal', {
      contractId: $scope.model.contractId,
      programmePriorityId: $scope.model.programmePriorityId
    });

    modalInstance.result.then(function(result) {
      if (type === 'approved') {
        $scope.model.approvedEuAmount = result.euAmount;
        $scope.model.approvedBgAmount = result.bgAmount;
      } else if (type === 'cert') {
        $scope.model.uncertifiedApprovedEuAmount = result.euAmount;
        $scope.model.uncertifiedApprovedBgAmount = result.bgAmount;
      }
    }, angular.noop);

    return modalInstance.opened;
  };
  $scope.approvedEdit = scFormParams.approvedEdit;
  $scope.certEdit = scFormParams.certEdit;

  if ($scope.approvedEdit) {
    $scope.$watch(
      '[model.approvedEuAmount, model.approvedBgAmount]',
      function() {
        $scope.model.approvedBfpTotalAmount = moneyOperation.addAmounts(
          $scope.model.approvedEuAmount,
          $scope.model.approvedBgAmount
        );
      },
      true
    );
  }

  if ($scope.certEdit) {
    $scope.$watch(
      '[model.uncertifiedApprovedEuAmount, model.uncertifiedApprovedBgAmount]',
      function() {
        $scope.model.uncertifiedApprovedBfpTotalAmount = moneyOperation.addAmounts(
          $scope.model.uncertifiedApprovedEuAmount,
          $scope.model.uncertifiedApprovedBgAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.approvedEuAmount, model.uncertifiedApprovedEuAmount]',
      function() {
        $scope.model.certifiedApprovedEuAmount = moneyOperation.subtractAmounts(
          $scope.model.approvedEuAmount,
          $scope.model.uncertifiedApprovedEuAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.approvedBgAmount, model.uncertifiedApprovedBgAmount]',
      function() {
        $scope.model.certifiedApprovedBgAmount = moneyOperation.subtractAmounts(
          $scope.model.approvedBgAmount,
          $scope.model.uncertifiedApprovedBgAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.approvedBfpTotalAmount, model.uncertifiedApprovedBfpTotalAmount]',
      function() {
        $scope.model.certifiedApprovedBfpTotalAmount = moneyOperation.subtractAmounts(
          $scope.model.approvedBfpTotalAmount,
          $scope.model.uncertifiedApprovedBfpTotalAmount
        );
      },
      true
    );
  }
}

ContractReportAdvancePaymentAmountCtrl.$inject = [
  '$scope',
  'scFormParams',
  'scModal',
  'moneyOperation'
];

export { ContractReportAdvancePaymentAmountCtrl };
