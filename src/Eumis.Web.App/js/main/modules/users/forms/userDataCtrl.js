function UserDataCtrl(
  $q,
  $scope,
  $state,
  $stateParams,
  scFormParams,
  eumisConstants,
  uinValidation,
  User
) {
  $scope.emailRegex = eumisConstants.emailRegex;
  $scope.isNew = scFormParams.isNew;
  $scope.hasPredefinedOrganization = scFormParams.hasPredefinedOrganization;

  $scope.isUniqueUsername = function(username) {
    if (!username || !$scope.isNew) {
      return $q.resolve();
    }
    return User.isUniqueUsername({
      username: username,
      userId: $stateParams.userId
    }).$promise.then(function(result) {
      return result.isUnique ? $q.resolve() : $q.reject();
    });
  };

  $scope.uinValid = function(uin) {
    return uinValidation.uinValid(uin, 'personalBulstat');
  };

  $scope.isUniqueUin = function(uin) {
    if (!uin || !$scope.isNew) {
      return $q.resolve();
    }
    return User.isUniqueUin({
      uin: uin
    }).$promise.then(function(result) {
      return result.isUnique ? $q.resolve() : $q.reject();
    });
  };
}

UserDataCtrl.$inject = [
  '$q',
  '$scope',
  '$state',
  '$stateParams',
  'scFormParams',
  'eumisConstants',
  'uinValidation',
  'User'
];

export { UserDataCtrl };
