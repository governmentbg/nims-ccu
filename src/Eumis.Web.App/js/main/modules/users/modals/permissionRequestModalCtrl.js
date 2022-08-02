function PermissionRequestModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  PermissionRequest,
  permissionRequest,
  userInfo
) {
  $scope.user = scModalParams.user;
  $scope.version = scModalParams.version;
  $scope.isReadonly = scModalParams.isReadonly;
  $scope.permissionRequest = permissionRequest;
  $scope.userInfo = userInfo;

  $scope.save = function() {
    return $scope.permissionRequestModalForm.$validate().then(function() {
      if ($scope.permissionRequestModalForm.$valid) {
        if ($scope.user.hasPermissionRequest) {
          return PermissionRequest.update(
            {
              id: $scope.user.requestPackageId,
              ind: $scope.user.userId,
              version: $scope.version
            },
            $scope.permissionRequest
          ).$promise.then(function() {
            return $uibModalInstance.close();
          });
        } else {
          return PermissionRequest.save(
            {
              id: $scope.user.requestPackageId,
              ind: $scope.user.userId,
              version: $scope.version
            },
            $scope.permissionRequest
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
}

PermissionRequestModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'PermissionRequest',
  'permissionRequest',
  'userInfo'
];

PermissionRequestModalCtrl.$resolve = {
  permissionRequest: [
    'scModalParams',
    'PermissionRequest',
    function(scModalParams, PermissionRequest) {
      if (scModalParams.user.hasPermissionRequest) {
        return PermissionRequest.get({
          id: scModalParams.user.requestPackageId,
          ind: scModalParams.user.userId
        }).$promise;
      } else {
        return PermissionRequest.newPermissionRequest({
          id: scModalParams.user.requestPackageId,
          ind: scModalParams.user.userId
        }).$promise;
      }
    }
  ],
  userInfo: [
    'scModalParams',
    'PermissionRequest',
    function(scModalParams, PermissionRequest) {
      return PermissionRequest.userInfo({
        id: scModalParams.user.requestPackageId,
        ind: scModalParams.user.userId
      }).$promise;
    }
  ]
};

export { PermissionRequestModalCtrl };
