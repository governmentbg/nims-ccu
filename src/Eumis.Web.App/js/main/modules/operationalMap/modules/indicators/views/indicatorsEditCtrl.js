function IndicatorsEditCtrl($scope, $state, $stateParams, scConfirm, Indicator, indicator) {
  $scope.editMode = null;
  $scope.indicator = indicator;

  $scope.save = function() {
    return $scope.editIndicatorForm.$validate().then(function() {
      if ($scope.editIndicatorForm.$valid) {
        return Indicator.update({ id: $stateParams.id }, $scope.indicator).$promise.then(
          function() {
            $state.partialReload();
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'Indicator',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.indicator.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.indicators.search');
      }
    });
  };
}

IndicatorsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'Indicator',
  'indicator'
];

IndicatorsEditCtrl.$resolve = {
  indicator: [
    'Indicator',
    '$stateParams',
    function(Indicator, $stateParams) {
      return Indicator.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { IndicatorsEditCtrl };
