function InterestSchemesEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  InterestScheme,
  interestScheme
) {
  $scope.editMode = null;
  $scope.interestScheme = interestScheme;

  $scope.save = function() {
    return $scope.editInterestSchemeForm.$validate().then(function() {
      if ($scope.editInterestSchemeForm.$valid) {
        return InterestScheme.update(
          {
            id: $stateParams.id
          },
          $scope.interestScheme
        ).$promise.then(function() {
          return $state.partialReload();
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
      validationAction: 'canDelete',
      resource: 'InterestScheme',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.interestScheme.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.interestSchemes.search');
      }
    });
  };
}

InterestSchemesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'InterestScheme',
  'interestScheme'
];

InterestSchemesEditCtrl.$resolve = {
  interestScheme: [
    'InterestScheme',
    '$stateParams',
    function(InterestScheme, $stateParams) {
      return InterestScheme.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { InterestSchemesEditCtrl };
