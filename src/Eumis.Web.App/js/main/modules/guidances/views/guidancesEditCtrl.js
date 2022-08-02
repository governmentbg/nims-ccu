function GuidancesEditCtrl($scope, $state, $stateParams, scConfirm, Guidance, guidance) {
  $scope.editMode = null;
  $scope.guidance = guidance;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editGuidanceForm.$validate().then(function() {
      if ($scope.editGuidanceForm.$valid) {
        return Guidance.update(
          {
            id: $stateParams.id
          },
          $scope.guidance
        ).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'Guidance',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.guidance.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.guidances.search', $stateParams, {
          reload: true
        });
      }
    });
  };
}

GuidancesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'Guidance',
  'guidance'
];

GuidancesEditCtrl.$resolve = {
  guidance: [
    'Guidance',
    '$stateParams',
    function(Guidance, $stateParams) {
      return Guidance.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { GuidancesEditCtrl };
