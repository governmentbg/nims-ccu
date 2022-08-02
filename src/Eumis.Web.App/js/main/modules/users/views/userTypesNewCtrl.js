function UserTypesNewCtrl($scope, $state, UserType, newUserType) {
  $scope.newUserType = newUserType;

  $scope.save = function() {
    return $scope.newUserTypeForm.$validate().then(function() {
      if ($scope.newUserTypeForm.$valid) {
        return UserType.save($scope.newUserType).$promise.then(function() {
          return $state.go('root.userTypes.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.userTypes.search');
  };
}

UserTypesNewCtrl.$inject = ['$scope', '$state', 'UserType', 'newUserType'];

UserTypesNewCtrl.$resolve = {
  newUserType: [
    'UserType',
    function(UserType) {
      return UserType.newUserType().$promise;
    }
  ]
};

export { UserTypesNewCtrl };
