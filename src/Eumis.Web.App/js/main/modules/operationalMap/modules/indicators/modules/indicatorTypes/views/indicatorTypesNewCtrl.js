function IndicatorTypesNewCtrl($scope, $state, scConfirm, indicatorType) {
  $scope.indicatorType = indicatorType;

  $scope.save = function() {
    return $scope.newIndicatorTypeForm.$validate().then(function() {
      if ($scope.newIndicatorTypeForm.$valid) {
        return scConfirm({
          resource: 'IndicatorType',
          action: 'save',
          data: $scope.indicatorType
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.map.indicatorTypes.search');
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.indicatorTypes.search');
  };
}

IndicatorTypesNewCtrl.$inject = ['$scope', '$state', 'scConfirm', 'indicatorType'];

IndicatorTypesNewCtrl.$resolve = {
  indicatorType: [
    'IndicatorType',
    function(IndicatorType) {
      return IndicatorType.newIndicator().$promise;
    }
  ]
};

export { IndicatorTypesNewCtrl };
