function UserOrganizationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  UserOrganization,
  userOrganization
) {
  $scope.editMode = null;
  $scope.userOrganization = userOrganization;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editUserOrganizationForm.$validate().then(function() {
      if ($scope.editUserOrganizationForm.$valid) {
        return UserOrganization.update(
          { id: $stateParams.id },
          $scope.userOrganization
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.deleteUserOrganization = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'UserOrganization',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: userOrganization.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.userOrganizations.search');
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };
}

UserOrganizationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'UserOrganization',
  'userOrganization'
];

UserOrganizationsEditCtrl.$resolve = {
  userOrganization: [
    '$stateParams',
    'UserOrganization',
    function($stateParams, UserOrganization) {
      return UserOrganization.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { UserOrganizationsEditCtrl };
