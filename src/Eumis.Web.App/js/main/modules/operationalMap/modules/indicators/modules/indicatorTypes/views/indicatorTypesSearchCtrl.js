function IndicatorTypesSearchCtrl($scope, $stateParams, indicatorTypes) {
  $scope.indicatorTypes = indicatorTypes;
}

IndicatorTypesSearchCtrl.$inject = ['$scope', '$stateParams', 'indicatorTypes'];

IndicatorTypesSearchCtrl.$resolve = {
  indicatorTypes: [
    'IndicatorType',
    function(IndicatorType) {
      return IndicatorType.query().$promise;
    }
  ]
};

export { IndicatorTypesSearchCtrl };
