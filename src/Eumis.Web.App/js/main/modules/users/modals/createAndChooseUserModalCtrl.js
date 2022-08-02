function CreateAndChooseUserModalCtrl($scope, $uibModalInstance, User, newUser) {
  $scope.newUser = newUser;

  $scope.createAndChoose = function() {
    return $scope.createAndChooseUserForm.$validate().then(function() {
      if ($scope.createAndChooseUserForm.$valid) {
        return User.save($scope.newUser).$promise.then(function(user) {
          return $uibModalInstance.close(user);
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

CreateAndChooseUserModalCtrl.$inject = ['$scope', '$uibModalInstance', 'User', 'newUser'];

CreateAndChooseUserModalCtrl.$resolve = {
  newUser: [
    'User',
    'scModalParams',
    function(User, scModalParams) {
      return User.newUser({
        userOrganizationId: scModalParams.userOrganizationId
      }).$promise;
    }
  ]
};

export { CreateAndChooseUserModalCtrl };
