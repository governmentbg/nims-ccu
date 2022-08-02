function BfpCalculatorModalCtrl($scope, $uibModalInstance, scModalParams, BfpCalculator) {
  $scope.model = {};

  $scope.ok = function() {
    return $scope.bfpCalculatorForm.$validate().then(function() {
      if ($scope.bfpCalculatorForm.$valid) {
        return BfpCalculator.calculate(scModalParams, {
          bfpTotalAmount: $scope.model.bfpTotalAmount
        }).$promise.then(function(result) {
          return $uibModalInstance.close(result);
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

BfpCalculatorModalCtrl.$inject = ['$scope', '$uibModalInstance', 'scModalParams', 'BfpCalculator'];

export { BfpCalculatorModalCtrl };
