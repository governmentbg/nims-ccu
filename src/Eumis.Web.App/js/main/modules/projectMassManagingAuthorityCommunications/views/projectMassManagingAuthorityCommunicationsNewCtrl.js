function ProjectMassManagingAuthorityCommunicationsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProjectMassManagingAuthorityCommunication,
  projectMassManagingAuthorityCommunication
) {
  $scope.communicationId = $stateParams.id;
  $scope.projectMassManagingAuthorityCommunication = projectMassManagingAuthorityCommunication;

  $scope.save = function() {
    return $scope.newProjectMassMACommunicationForm.$validate().then(function() {
      if ($scope.newProjectMassMACommunicationForm.$valid) {
        return ProjectMassManagingAuthorityCommunication.save(
          { id: $stateParams.id },
          $scope.projectMassManagingAuthorityCommunication
        ).$promise.then(function(data) {
          return $state.go('root.projectMassManagingAuthorityCommunications.view.edit', {
            id: data.projectMassManagingAuthorityCommunicationId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.projectMassManagingAuthorityCommunications.search');
  };
}

ProjectMassManagingAuthorityCommunicationsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProjectMassManagingAuthorityCommunication',
  'projectMassManagingAuthorityCommunication'
];

ProjectMassManagingAuthorityCommunicationsNewCtrl.$resolve = {
  projectMassManagingAuthorityCommunication: [
    'ProjectMassManagingAuthorityCommunication',
    function(ProjectMassManagingAuthorityCommunication) {
      return ProjectMassManagingAuthorityCommunication.newProjectMassManagingAuthorityCommunication()
        .$promise;
    }
  ]
};

export { ProjectMassManagingAuthorityCommunicationsNewCtrl };
