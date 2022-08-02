function DirectionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  Direction,
  direction,
  $interpolate,
  l10n
) {
  $scope.editMode = null;
  $scope.direction = direction;
  $scope.status = $scope.info.status;

  $scope.save = function() {
    return $scope.editDirectionForm.$validate().then(function() {
      if ($scope.editDirectionForm.$valid) {
        return Direction.update({ id: $stateParams.id }, $scope.direction).$promise.then(
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
      resource: 'Direction',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.direction.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.directions.search');
      }
    });
  };

  $scope.changeStatus = function(status) {
    let question = $interpolate(l10n.get('directions_editDirectionForm_changeStatusQuestion'))({
      status: $scope.info.statusDescr
    });
    var action = 'changeStatusTo' + status;
    return scConfirm({
      confirmMessage: question,
      resource: 'Direction',
      action: action,
      params: {
        id: $stateParams.id,
        version: $scope.direction.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

DirectionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'Direction',
  'direction',
  '$interpolate',
  'l10n'
];

DirectionsEditCtrl.$resolve = {
  direction: [
    'Direction',
    '$stateParams',
    function(Direction, $stateParams) {
      return Direction.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { DirectionsEditCtrl };
