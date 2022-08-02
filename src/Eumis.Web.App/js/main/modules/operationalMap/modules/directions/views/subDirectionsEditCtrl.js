function SubDirectionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  SubDirection,
  subDirection
) {
  $scope.editMode = null;
  $scope.subDirection = subDirection;
  $scope.status = $scope.info.status;

  $scope.save = function() {
    return $scope.editSubDirectionForm.$validate().then(function() {
      if ($scope.editSubDirectionForm.$valid) {
        return SubDirection.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.subDirection
        ).$promise.then(function() {
          $state.partialReload();
        });
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
      resource: 'SubDirection',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.subDirection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.directions.view.subDirections.search');
      }
    });
  };
}

SubDirectionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'SubDirection',
  'subDirection'
];

SubDirectionsEditCtrl.$resolve = {
  subDirection: [
    'SubDirection',
    '$stateParams',
    function(SubDirection, $stateParams) {
      return SubDirection.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { SubDirectionsEditCtrl };
