import angular from 'angular';

function ContractDebtVersionCtrl($scope, scFormParams, scModal, moneyOperation) {
  $scope.contractId = scFormParams.contractId;

  $scope.calculate = function(type) {
    var modalInstance = scModal.open('bfpCalculatorModal', {
      contractId: scFormParams.contractId,
      programmePriorityId: scFormParams.programmePriorityId
    });

    modalInstance.result.then(function(result) {
      if (type === 'approved') {
        $scope.model.euAmount = result.euAmount;
        $scope.model.bgAmount = result.bgAmount;
      } else if (type === 'cert') {
        $scope.model.certEuAmount = result.euAmount;
        $scope.model.certBgAmount = result.bgAmount;
      }
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.$watch(
    '[model.euAmount, model.bgAmount]',
    function() {
      $scope.model.totalAmount = moneyOperation.addAmounts(
        $scope.model.euAmount,
        $scope.model.bgAmount
      );
    },
    true
  );

  $scope.$watch(
    '[model.certEuAmount, model.certBgAmount]',
    function() {
      $scope.model.certTotalAmount = moneyOperation.addAmounts(
        $scope.model.certEuAmount,
        $scope.model.certBgAmount
      );
    },
    true
  );
}

ContractDebtVersionCtrl.$inject = ['$scope', 'scFormParams', 'scModal', 'moneyOperation'];

export { ContractDebtVersionCtrl };
