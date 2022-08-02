function UserDeclarationsSearchCtrl($scope, userDeclarations) {
  $scope.userDeclarations = userDeclarations;
  $scope.gdprUrl = 'gdprdeclarationView';
}

UserDeclarationsSearchCtrl.$inject = ['$scope', 'userDeclarations'];

UserDeclarationsSearchCtrl.$resolve = {
  userDeclarations: [
    'UserProfile',
    function(UserProfile) {
      return UserProfile.getDeclarations().$promise;
    }
  ]
};

export { UserDeclarationsSearchCtrl };
