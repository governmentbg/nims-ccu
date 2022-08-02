function UserOrganizationsNewCtrl($scope, $state, UserOrganization, newUserOrganization) {
  $scope.newUserOrganization = newUserOrganization;

  $scope.save = function() {
    return $scope.newUserOrganizationForm.$validate().then(function() {
      if ($scope.newUserOrganizationForm.$valid) {
        return UserOrganization.save($scope.newUserOrganization).$promise.then(function() {
          return $state.go('root.userOrganizations.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.userOrganizations.search');
  };
}

UserOrganizationsNewCtrl.$inject = ['$scope', '$state', 'UserOrganization', 'newUserOrganization'];

UserOrganizationsNewCtrl.$resolve = {
  newUserOrganization: [
    'UserOrganization',
    function(UserOrganization) {
      return UserOrganization.newUserOrganization().$promise;
    }
  ]
};

export { UserOrganizationsNewCtrl };
