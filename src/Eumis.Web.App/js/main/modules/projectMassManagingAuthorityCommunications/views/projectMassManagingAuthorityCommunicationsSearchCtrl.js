function ProjectMassManagingAuthorityCommunicationsSearchCtrl($scope, communications) {
  $scope.communications = communications;
}

ProjectMassManagingAuthorityCommunicationsSearchCtrl.$inject = ['$scope', 'communications'];

ProjectMassManagingAuthorityCommunicationsSearchCtrl.$resolve = {
  communications: [
    'ProjectMassManagingAuthorityCommunication',
    ProjectMassManagingAuthorityCommunication =>
      ProjectMassManagingAuthorityCommunication.query().$promise
  ]
};

export { ProjectMassManagingAuthorityCommunicationsSearchCtrl };
