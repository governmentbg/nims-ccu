import angular from 'angular';

function FIReimbursedAmountDataCtrl($scope, scModal, moneyOperation) {
  $scope.calculate = function(type) {
    var modalInstance = scModal.open('bfpCalculatorModal', {
      contractId: $scope.model.contractId,
      programmePriorityId: $scope.model.programmePriorityId
    });

    modalInstance.result.then(function(result) {
      if (type === 'principal') {
        $scope.model.principalBfpEuAmount = result.euAmount;
        $scope.model.principalBfpBgAmount = result.bgAmount;
      } else if (type === 'interest') {
        $scope.model.interestBfpEuAmount = result.euAmount;
        $scope.model.interestBfpBgAmount = result.bgAmount;
      }
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.$watch(
    '[model.principalBfpEuAmount, model.principalBfpBgAmount]',
    function() {
      $scope.model.principalBfpTotalAmount = moneyOperation.addAmounts(
        $scope.model.principalBfpEuAmount,
        $scope.model.principalBfpBgAmount
      );
    },
    true
  );
  $scope.$watch(
    '[model.interestBfpEuAmount, model.interestBfpBgAmount]',
    function() {
      $scope.model.interestBfpTotalAmount = moneyOperation.addAmounts(
        $scope.model.interestBfpEuAmount,
        $scope.model.interestBfpBgAmount
      );
    },
    true
  );
}

FIReimbursedAmountDataCtrl.$inject = ['$scope', 'scModal', 'moneyOperation'];

export { FIReimbursedAmountDataCtrl };
