function BasicInterestRatesNewCtrl($scope, $state, BasicInterestRate, newBasicInterestRate) {
  $scope.newBasicInterestRate = newBasicInterestRate;

  $scope.save = function() {
    return $scope.newBasicInterestRateForm.$validate().then(function() {
      if ($scope.newBasicInterestRateForm.$valid) {
        return BasicInterestRate.save($scope.newBasicInterestRate).$promise.then(function() {
          return $state.go('root.map.basicInterestRates.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.basicInterestRates.search');
  };
}

BasicInterestRatesNewCtrl.$inject = [
  '$scope',
  '$state',
  'BasicInterestRate',
  'newBasicInterestRate'
];

BasicInterestRatesNewCtrl.$resolve = {
  newBasicInterestRate: [
    'BasicInterestRate',
    function(BasicInterestRate) {
      return BasicInterestRate.newBasicInterestRate().$promise;
    }
  ]
};

export { BasicInterestRatesNewCtrl };
