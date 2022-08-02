function IndicatorsSearchCtrl($scope, $stateParams, indicators) {
  $scope.indicators = indicators;
}

IndicatorsSearchCtrl.$inject = ['$scope', '$stateParams', 'indicators'];

IndicatorsSearchCtrl.$resolve = {
  indicators: [
    'Indicator',
    function(Indicator) {
      return Indicator.query().$promise;
    }
  ]
};

export { IndicatorsSearchCtrl };
