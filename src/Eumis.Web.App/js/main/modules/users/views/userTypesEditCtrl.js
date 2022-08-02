function UserTypesEditCtrl($scope, $state, $stateParams, scMessage, scConfirm, UserType, userType) {
  $scope.editMode = null;
  $scope.userType = userType;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editUserTypeForm.$validate().then(function() {
      if ($scope.editUserTypeForm.$valid) {
        return UserType.update({ id: $stateParams.id }, $scope.userType).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.deleteUserType = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'UserType',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.userType.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.userTypes.search');
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };
}

UserTypesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scMessage',
  'scConfirm',
  'UserType',
  'userType'
];

UserTypesEditCtrl.$resolve = {
  userType: [
    '$stateParams',
    'UserType',
    function($stateParams, UserType) {
      return UserType.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { UserTypesEditCtrl };
