function RegDataRequestModalCtrl(
  $q,
  $scope,
  $uibModalInstance,
  scModalParams,
  eumisConstants,
  uinValidation,
  RegDataRequest,
  User,
  regDataRequest,
  isSuperUser
) {
  $scope.user = scModalParams.user;
  $scope.version = scModalParams.version;
  $scope.isReadonly = scModalParams.isReadonly;
  $scope.regDataRequest = regDataRequest;
  $scope.isSuperUser = isSuperUser;
  $scope.emailRegex = eumisConstants.emailRegex;

  $scope.save = function() {
    return $scope.regDataRequestModalForm.$validate().then(function() {
      if ($scope.regDataRequestModalForm.$valid) {
        if ($scope.user.hasRegDataRequest) {
          return RegDataRequest.update(
            {
              id: $scope.user.requestPackageId,
              ind: $scope.user.userId,
              version: $scope.version
            },
            $scope.regDataRequest
          ).$promise.then(function() {
            return $uibModalInstance.close();
          });
        } else {
          return RegDataRequest.save(
            {
              id: $scope.user.requestPackageId,
              ind: $scope.user.userId,
              version: $scope.version
            },
            $scope.regDataRequest
          ).$promise.then(function() {
            return $uibModalInstance.close();
          });
        }
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };

  $scope.uinValid = function(uin) {
    return uinValidation.uinValid(uin, 'personalBulstat');
  };

  $scope.isUniqueUin = function(uin) {
    if (!uin || $scope.isReadonly) {
      return $q.resolve();
    }
    return User.isUniqueUin({
      uin: uin,
      userId: $scope.regDataRequest.userId
    }).$promise.then(function(result) {
      return result.isUnique ? $q.resolve() : $q.reject();
    });
  };
}

RegDataRequestModalCtrl.$inject = [
  '$q',
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'eumisConstants',
  'uinValidation',
  'RegDataRequest',
  'User',
  'regDataRequest',
  'isSuperUser'
];

RegDataRequestModalCtrl.$resolve = {
  regDataRequest: [
    'scModalParams',
    'RegDataRequest',
    function(scModalParams, RegDataRequest) {
      if (scModalParams.user.hasRegDataRequest) {
        return RegDataRequest.get({
          id: scModalParams.user.requestPackageId,
          ind: scModalParams.user.userId
        }).$promise;
      } else {
        return RegDataRequest.newRegDataRequest({
          id: scModalParams.user.requestPackageId,
          ind: scModalParams.user.userId
        }).$promise;
      }
    }
  ],
  isSuperUser: [
    'scModalParams',
    'User',
    function(scModalParams, User) {
      return User.isSuperUser().$promise;
    }
  ]
};

export { RegDataRequestModalCtrl };
