function UsersViewCtrl($scope, userInfo, l10n, $interpolate) {
  $scope.userInfo = userInfo;
  $scope.text = $interpolate(l10n.get('users_userInfo_text'))({
    username: userInfo.username,
    fullname: userInfo.fullname
  });
}

UsersViewCtrl.$inject = ['$scope', 'userInfo', 'l10n', '$interpolate'];

UsersViewCtrl.$resolve = {
  userInfo: [
    '$stateParams',
    'User',
    function($stateParams, User) {
      return User.getUserInfo({ userId: $stateParams.id }).$promise;
    }
  ]
};

export { UsersViewCtrl };
