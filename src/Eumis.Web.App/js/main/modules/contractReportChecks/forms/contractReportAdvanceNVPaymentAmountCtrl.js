import angular from 'angular';

function ContractReportAdvanceNVPaymentAmountCtrl($scope, scFormParams, scModal, moneyOperation) {
  $scope.calculate = function() {
    var modalInstance = scModal.open('bfpCalculatorModal', {
      contractId: $scope.model.contractId,
      programmePriorityId: $scope.model.programmePriorityId
    });

    modalInstance.result.then(function(result) {
      $scope.model.euAmount = result.euAmount;
      $scope.model.bgAmount = result.bgAmount;
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.$watch(
    '[model.euAmount, model.bgAmount]',
    function() {
      $scope.model.bfpTotalAmount = moneyOperation.addAmounts(
        $scope.model.euAmount,
        $scope.model.bgAmount
      );
    },
    true
  );
}

ContractReportAdvanceNVPaymentAmountCtrl.$inject = [
  '$scope',
  'scFormParams',
  'scModal',
  'moneyOperation'
];

export { ContractReportAdvanceNVPaymentAmountCtrl };
