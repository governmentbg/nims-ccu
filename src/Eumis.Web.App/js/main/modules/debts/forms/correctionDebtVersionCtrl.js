function CorrectionDebtVersionCtrl($scope, moneyOperation) {
  $scope.$watch(
    '[model.debtEuAmount, model.debtBgAmount]',
    function() {
      $scope.model.debtBfpAmount = moneyOperation.addAmounts(
        $scope.model.debtEuAmount,
        $scope.model.debtBgAmount
      );
    },
    true
  );

  $scope.$watch(
    '[model.certEuAmount, model.certBgAmount]',
    function() {
      $scope.model.certBfpAmount = moneyOperation.addAmounts(
        $scope.model.certEuAmount,
        $scope.model.certBgAmount
      );
    },
    true
  );

  $scope.$watch(
    '[model.reimbursedEuAmount, model.reimbursedBgAmount]',
    function() {
      $scope.model.reimbursedBfpAmount = moneyOperation.addAmounts(
        $scope.model.reimbursedEuAmount,
        $scope.model.reimbursedBgAmount
      );
    },
    true
  );
}

CorrectionDebtVersionCtrl.$inject = ['$scope', 'moneyOperation'];

export { CorrectionDebtVersionCtrl };
