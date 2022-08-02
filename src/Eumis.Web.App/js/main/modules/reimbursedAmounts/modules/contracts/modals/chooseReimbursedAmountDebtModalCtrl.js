function ChooseReimbursedAmountDebtModalCtrl($scope, $uibModalInstance, scModalParams) {
  $scope.model = {};
  $scope.scModalParams = scModalParams;

  $scope.ok = function() {
    return $scope.chooseDebtForm.$validate().then(function() {
      if ($scope.chooseDebtForm.$valid) {
        return $uibModalInstance.close({
          contractDebtId: $scope.model.contractDebtId
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.close({
      confirm: false
    });
  };
}

ChooseReimbursedAmountDebtModalCtrl.$inject = ['$scope', '$uibModalInstance', 'scModalParams'];

export { ChooseReimbursedAmountDebtModalCtrl };
