function UserDeclarationsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  UserDeclaration,
  userDeclarations
) {
  $scope.userDeclarations = userDeclarations;
  $scope.gdprUrl = 'gdprdeclarationView';
}

UserDeclarationsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'UserDeclaration',
  'userDeclarations'
];

UserDeclarationsSearchCtrl.$resolve = {
  userDeclarations: [
    '$stateParams',
    'UserDeclaration',
    function($stateParams, UserDeclaration) {
      return UserDeclaration.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { UserDeclarationsSearchCtrl };
