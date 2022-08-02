function ProfileViewCtrl($scope, userInfo, l10n, $interpolate) {
  $scope.userInfo = userInfo;
  $scope.text = $interpolate(l10n.get('users_userInfo_text'))({
    username: userInfo.username,
    fullname: userInfo.fullname
  });
}

ProfileViewCtrl.$inject = ['$scope', 'userInfo', 'l10n', '$interpolate'];

ProfileViewCtrl.$resolve = {
  userInfo: [
    'UserProfile',
    function(UserProfile) {
      return UserProfile.getUserInfo().$promise;
    }
  ]
};

export { ProfileViewCtrl };
