function EuReimbursedAmountDataCtrl($scope, moneyConversion, moneyOperation) {
  $scope.$watch(
    '[model.certExpensesBfpEuAmountLv, model.certExpensesBfpBgAmountLv]',
    function() {
      $scope.model.certExpensesBfpTotalAmountLv = moneyOperation.addAmounts(
        $scope.model.certExpensesBfpEuAmountLv,
        $scope.model.certExpensesBfpBgAmountLv
      );
    },
    true
  );

  $scope.$watch(
    '[model.certExpensesBfpTotalAmountLv, model.certExpensesSelfAmountLv]',
    function() {
      $scope.model.certExpensesTotalAmountLv = moneyOperation.addAmounts(
        $scope.model.certExpensesBfpTotalAmountLv,
        $scope.model.certExpensesSelfAmountLv
      );
    },
    true
  );

  $scope.$watch(
    '[model.certExpensesBfpEuAmountEuro, model.certExpensesBfpBgAmountEuro]',
    function() {
      $scope.model.certExpensesBfpTotalAmountEuro = moneyOperation.addAmounts(
        $scope.model.certExpensesBfpEuAmountEuro,
        $scope.model.certExpensesBfpBgAmountEuro
      );
    },
    true
  );
  $scope.$watch(
    '[model.certExpensesBfpTotalAmountEuro, model.certExpensesSelfAmountEuro]',
    function() {
      $scope.model.certExpensesTotalAmountEuro = moneyOperation.addAmounts(
        $scope.model.certExpensesBfpTotalAmountEuro,
        $scope.model.certExpensesSelfAmountEuro
      );
    },
    true
  );

  $scope.calcExpenses = function() {
    $scope.model.certExpensesBfpEuAmountLv = moneyConversion.convertToLv(
      $scope.model.certExpensesBfpEuAmountEuro,
      'euroEC'
    );
    $scope.model.certExpensesBfpBgAmountLv = moneyConversion.convertToLv(
      $scope.model.certExpensesBfpBgAmountEuro,
      'euroEC'
    );
    $scope.model.certExpensesSelfAmountLv = moneyConversion.convertToLv(
      $scope.model.certExpensesSelfAmountEuro,
      'euroEC'
    );
  };
}

EuReimbursedAmountDataCtrl.$inject = ['$scope', 'moneyConversion', 'moneyOperation'];

export { EuReimbursedAmountDataCtrl };
