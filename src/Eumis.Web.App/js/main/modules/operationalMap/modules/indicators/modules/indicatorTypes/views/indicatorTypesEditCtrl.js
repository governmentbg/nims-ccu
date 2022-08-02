function IndicatorTypesEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  IndicatorType,
  indicatorType
) {
  $scope.editMode = null;
  $scope.indicatorType = indicatorType;

  $scope.save = function() {
    return $scope.editIndicatorTypeForm.$validate().then(function() {
      if ($scope.editIndicatorTypeForm.$valid) {
        return IndicatorType.update({ id: $stateParams.id }, $scope.indicatorType).$promise.then(
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
      resource: 'IndicatorType',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.indicatorType.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.indicatorTypes.search');
      }
    });
  };
}

IndicatorTypesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'IndicatorType',
  'indicatorType'
];

IndicatorTypesEditCtrl.$resolve = {
  indicatorType: [
    'IndicatorType',
    '$stateParams',
    function(IndicatorType, $stateParams) {
      return IndicatorType.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { IndicatorTypesEditCtrl };
