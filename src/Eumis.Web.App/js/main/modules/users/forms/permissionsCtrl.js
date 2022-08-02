import _ from 'lodash';

function PermissionsCtrl($scope) {
  $scope.programmesAllChecks = {};
  _.forEach(_.keys($scope.model.programmePermissions.programmes), function(programmeKey) {
    $scope.programmesAllChecks[programmeKey] = false;
  });

  $scope.permissionTypesAllChecks = {};

  _.forEach($scope.model.programmePermissions.permissionTypes, function(pType) {
    var pTypeName = pType.permissionType;
    $scope.permissionTypesAllChecks[pTypeName] = {};
    _.forEach(pType.permissions, function(perm) {
      if (
        _.some($scope.model.programmePermissions.permissions, function(permission) {
          if (
            permission[pTypeName] !== null &&
            permission[pTypeName] !== undefined &&
            permission[pTypeName][perm.permission] !== null &&
            permission[pTypeName][perm.permission] !== undefined
          ) {
            return true;
          }
        })
      ) {
        $scope.permissionTypesAllChecks[pTypeName][perm.permission] = false;
      }
    });
  });

  $scope.showPermissionTypesAllChecks =
    _.keys($scope.model.programmePermissions.programmes).length > 1;

  $scope.showProgrammePermissionTable = !_.isEmpty($scope.model.programmePermissions.programmes);
  $scope.showCommonPermissionTable = !_.isEmpty($scope.model.commonPermissions.permissionTypes);

  $scope.getProgrammePermissionName = function(permissionType, permission) {
    var typeItem = _.find($scope.model.programmePermissions.permissionTypes, {
      permissionType: permissionType
    });
    return _.find(typeItem.permissions, { permission: permission }).displayName;
  };

  $scope.getCommonPermissionName = function(permissionType, permission) {
    var typeItem = _.find($scope.model.commonPermissions.permissionTypes, {
      permissionType: permissionType
    });
    return _.find(typeItem.permissions, { permission: permission }).displayName;
  };

  function syncProgrammesAllChecks() {
    _.forEach(_.keys($scope.programmesAllChecks), function(programmeKey) {
      $scope.programmesAllChecks[programmeKey] = _.every(
        $scope.model.programmePermissions.permissions[programmeKey],
        function(permissionType) {
          return _.every(permissionType, function(permission) {
            return permission === true;
          });
        }
      );
    });
  }

  function syncPermissionTypesAllChecks() {
    _.forOwn($scope.permissionTypesAllChecks, function(permissionTypeValue, permissionTypeKey) {
      _.forEach(_.keys(permissionTypeValue), function(permissionKey) {
        $scope.permissionTypesAllChecks[permissionTypeKey][permissionKey] = _.every(
          $scope.model.programmePermissions.permissions,
          function(programmePermission) {
            if (
              programmePermission[permissionTypeKey] === undefined ||
              programmePermission[permissionTypeKey] === null ||
              programmePermission[permissionTypeKey][permissionKey] === undefined ||
              programmePermission[permissionTypeKey][permissionKey] === null
            ) {
              return true;
            } else {
              return programmePermission[permissionTypeKey][permissionKey] === true;
            }
          }
        );
      });
    });
  }

  $scope.programmesAllCheckChange = function(programmeId) {
    _.forEach($scope.model.programmePermissions.permissions[programmeId], function(permissionType) {
      _.forEach(_.keys(permissionType), function(key) {
        permissionType[key] = $scope.programmesAllChecks[programmeId];
      });
    });

    if ($scope.showPermissionTypesAllChecks) {
      syncPermissionTypesAllChecks();
    }
  };

  $scope.permissionTypesAllCheckChange = function(permissionType, permission) {
    _.forEach($scope.model.programmePermissions.permissions, function(programmePermissions) {
      if (
        programmePermissions[permissionType] !== undefined &&
        programmePermissions[permissionType] !== null &&
        programmePermissions[permissionType][permission] !== undefined &&
        programmePermissions[permissionType][permission] !== null
      ) {
        programmePermissions[permissionType][permission] =
          $scope.permissionTypesAllChecks[permissionType][permission];
      }
    });

    syncProgrammesAllChecks();
  };

  $scope.programmeCheckChange = function() {
    syncProgrammesAllChecks();
    if ($scope.showPermissionTypesAllChecks) {
      syncPermissionTypesAllChecks();
    }
  };

  syncProgrammesAllChecks();
  if ($scope.showPermissionTypesAllChecks) {
    syncPermissionTypesAllChecks();
  }
}

PermissionsCtrl.$inject = ['$scope'];

export { PermissionsCtrl };
