function UsersDatatableCtrl($scope) {
  $scope.activeText = 'active';
  $scope.inactiveText = 'inactive';
  $scope.lockedText = 'locked';
  $scope.deletedText = 'deleted';
  $scope.users = [
    {
      username: 'tester1',
      userOrganization: 'test1 org',
      userType: 'test1 type',
      fullname: 'tester1 name',
      email: 'test1@email.com',
      isActive: true,
      isLocked: true,
      isDeleted: true
    },
    {
      username: 'tester2',
      userOrganization: 'test2 org',
      userType: 'test2 type',
      fullname: 'tester2 name',
      email: 'test2@email.com',
      isActive: false,
      isLocked: true,
      isDeleted: true
    }
  ];
}

UsersDatatableCtrl.$inject = ['$scope'];

export { UsersDatatableCtrl };
