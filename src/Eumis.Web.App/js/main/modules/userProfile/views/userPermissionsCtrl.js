function UserPermissionsCtrl($scope, permissions) {
  $scope.permissions = permissions;
}

UserPermissionsCtrl.$inject = ['$scope', 'permissions'];

UserPermissionsCtrl.$resolve = {
  permissions: [
    'UserProfile',
    function(UserProfile) {
      return UserProfile.getPermissions().$promise;
    }
  ]
};

export { UserPermissionsCtrl };
