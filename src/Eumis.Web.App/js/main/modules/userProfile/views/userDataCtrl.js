function UserDataCtrl($scope, user) {
  $scope.user = user;
}

UserDataCtrl.$inject = ['$scope', 'user'];

UserDataCtrl.$resolve = {
  user: [
    'UserProfile',
    function(UserProfile) {
      return UserProfile.get().$promise;
    }
  ]
};

export { UserDataCtrl };
