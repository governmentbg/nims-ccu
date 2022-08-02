import angular from 'angular';

function ActuallyPaidAmountDataCtrl($scope, scModal) {
  $scope.calculate = function() {
    var modalInstance = scModal.open('bfpCalculatorModal', {
      contractId: $scope.model.contractId,
      programmePriorityId: $scope.model.programmePriorityId
    });

    modalInstance.result.then(function(result) {
      $scope.model.paidBfpEuAmount = result.euAmount;
      $scope.model.paidBfpBgAmount = result.bgAmount;
    }, angular.noop);

    return modalInstance.opened;
  };
}

ActuallyPaidAmountDataCtrl.$inject = ['$scope', 'scModal'];

export { ActuallyPaidAmountDataCtrl };
