function UsersNewCtrl($scope, $state, User, newUser) {
  $scope.newUser = newUser;

  $scope.save = function() {
    return $scope.newUserForm.$validate().then(function() {
      if ($scope.newUserForm.$valid) {
        return User.save($scope.newUser).$promise.then(function() {
          return $state.go('root.users.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.users.search');
  };
}

UsersNewCtrl.$inject = ['$scope', '$state', 'User', 'newUser'];

UsersNewCtrl.$resolve = {
  newUser: [
    'User',
    function(User) {
      return User.newUser().$promise;
    }
  ]
};

export { UsersNewCtrl };
