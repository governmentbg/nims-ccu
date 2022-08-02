function UserOrganizationsSearchCtrl(
  $scope,
  $state,
  scMessage,
  UserOrganization,
  userOrganizations
) {
  $scope.userOrganizations = userOrganizations;
}

UserOrganizationsSearchCtrl.$inject = [
  '$scope',
  '$state',
  'scMessage',
  'UserOrganization',
  'userOrganizations'
];

UserOrganizationsSearchCtrl.$resolve = {
  userOrganizations: [
    'UserOrganization',
    function(UserOrganization) {
      return UserOrganization.query().$promise;
    }
  ]
};

export { UserOrganizationsSearchCtrl };
