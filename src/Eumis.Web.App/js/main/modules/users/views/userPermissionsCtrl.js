function UserPermissionsCtrl($scope, $state, $stateParams, UserPermission, permissions) {
  $scope.permissions = permissions;
}

UserPermissionsCtrl.$inject = ['$scope', '$state', '$stateParams', 'UserPermission', 'permissions'];

UserPermissionsCtrl.$resolve = {
  permissions: [
    '$stateParams',
    'UserPermission',
    function($stateParams, UserPermission) {
      return UserPermission.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { UserPermissionsCtrl };
