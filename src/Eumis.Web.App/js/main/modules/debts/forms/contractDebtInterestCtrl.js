function ContractDebtInterestCtrl($scope, $timeout, moneyOperation, ContractDebtInterest) {
  var timeout;
  $scope.hasError = false;

  $scope.$watch(
    '[model.euInterestAmount, model.bgInterestAmount]',
    function() {
      $scope.model.totalInterestAmount = moneyOperation.addAmounts(
        $scope.model.euInterestAmount,
        $scope.model.bgInterestAmount
      );
    },
    true
  );

  $scope.schemeOrDateToChanged = function() {
    $scope.model.euAmount = null;
    $scope.model.bgAmount = null;
    $scope.model.totalAmount = null;

    $scope.model.euInterestAmount = null;
    $scope.model.bgInterestAmount = null;
    $scope.model.totalInterestAmount = null;
  };

  $scope.calculate = function() {
    return ContractDebtInterest.calculate(
      {
        id: $scope.model.contractDebtId
      },
      $scope.model
    ).$promise.then(function(result) {
      if (!result.error) {
        $scope.model.euAmount = result.euAmount;
        $scope.model.bgAmount = result.bgAmount;
        $scope.model.totalAmount = result.totalAmount;
        $scope.model.euInterestAmount = result.euInterestAmount;
        $scope.model.bgInterestAmount = result.bgInterestAmount;
        $scope.model.totalInterestAmount = result.totalInterestAmount;
        $scope.model.error = null;
      } else {
        if (timeout) {
          $timeout.cancel(timeout);
        }
        timeout = $timeout(function() {
          $scope.model.error = null;
        }, 5000);
        $scope.model.error = result.error;
      }
    });
  };
}

ContractDebtInterestCtrl.$inject = ['$scope', '$timeout', 'moneyOperation', 'ContractDebtInterest'];

export { ContractDebtInterestCtrl };
