function UserTypesSearchCtrl($scope, $state, userTypes) {
  $scope.userTypes = userTypes;
}

UserTypesSearchCtrl.$inject = ['$scope', '$state', 'userTypes'];

UserTypesSearchCtrl.$resolve = {
  userTypes: [
    'UserType',
    function(UserType) {
      return UserType.query().$promise;
    }
  ]
};

export { UserTypesSearchCtrl };
