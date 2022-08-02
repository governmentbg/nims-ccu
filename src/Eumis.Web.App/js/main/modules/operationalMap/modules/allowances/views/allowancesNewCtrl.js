function AllowancesNewCtrl($scope, $state, Allowance, newAllowance) {
  $scope.newAllowance = newAllowance;

  $scope.save = function() {
    return $scope.newAllowanceForm.$validate().then(function() {
      if ($scope.newAllowanceForm.$valid) {
        return Allowance.save($scope.newAllowance).$promise.then(function() {
          return $state.go('root.map.allowances.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.allowances.search');
  };
}

AllowancesNewCtrl.$inject = ['$scope', '$state', 'Allowance', 'newAllowance'];

AllowancesNewCtrl.$resolve = {
  newAllowance: [
    'Allowance',
    function(Allowance) {
      return Allowance.newAllowance().$promise;
    }
  ]
};

export { AllowancesNewCtrl };
