function OperationalMapBudgetCtrl($scope, moneyOperation) {
  $scope.$watch(
    '[model.euAmount, model.bgAmount]',
    function() {
      $scope.total = moneyOperation.addAmounts($scope.model.euAmount, $scope.model.bgAmount);
    },
    true
  );

  $scope.$watch(
    function() {
      if (
        $scope.model.euAmount === null ||
        $scope.model.euAmount === undefined ||
        $scope.model.bgAmount === null ||
        $scope.model.bgAmount === undefined
      ) {
        return undefined;
      }

      var p = ($scope.model.bgAmount * 100) / ($scope.model.euAmount + $scope.model.bgAmount);
      return Math.round((p + 0.00001) * 100) / 100;
    },
    function(percent) {
      $scope.bgPercent = percent;
    }
  );

  $scope.$watch(
    function() {
      if (
        $scope.model.euAmount === null ||
        $scope.model.euAmount === undefined ||
        $scope.model.bgAmount === null ||
        $scope.model.bgAmount === undefined
      ) {
        return undefined;
      }

      var p = ($scope.model.euAmount * 100) / ($scope.model.bgAmount + $scope.model.euAmount);
      return Math.round((p + 0.00001) * 100) / 100;
    },
    function(percent) {
      $scope.euPercent = percent;
    }
  );
}

OperationalMapBudgetCtrl.$inject = ['$scope', 'moneyOperation'];

export { OperationalMapBudgetCtrl };
