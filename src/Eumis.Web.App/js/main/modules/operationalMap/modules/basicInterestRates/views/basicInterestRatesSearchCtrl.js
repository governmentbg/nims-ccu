function BasicInterestRatesSearchCtrl($scope, basicInterestRates) {
  $scope.basicInterestRates = basicInterestRates;
}

BasicInterestRatesSearchCtrl.$inject = ['$scope', 'basicInterestRates'];

BasicInterestRatesSearchCtrl.$resolve = {
  basicInterestRates: [
    'BasicInterestRate',
    function(BasicInterestRate) {
      return BasicInterestRate.query().$promise;
    }
  ]
};

export { BasicInterestRatesSearchCtrl };
