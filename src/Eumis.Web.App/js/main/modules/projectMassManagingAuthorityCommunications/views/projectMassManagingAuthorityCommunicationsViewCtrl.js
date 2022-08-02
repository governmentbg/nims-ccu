function ProjectMassManagingAuthorityCommunicationsViewCtrl($scope, info) {
  $scope.massCommunicationInfo = info;

  $scope.tabList = {
    projectMassManagingAuthorityCommunications_tabs_data:
      'root.projectMassManagingAuthorityCommunications.view.edit',
    projectMassManagingAuthorityCommunications_tabs_documents:
      'root.projectMassManagingAuthorityCommunications.view.documents',
    projectMassManagingAuthorityCommunications_tabs_recipients:
      'root.projectMassManagingAuthorityCommunications.view.recipients'
  };
}

ProjectMassManagingAuthorityCommunicationsViewCtrl.$inject = ['$scope', 'info'];

ProjectMassManagingAuthorityCommunicationsViewCtrl.$resolve = {
  info: [
    'ProjectMassManagingAuthorityCommunication',
    '$stateParams',
    function(ProjectMassManagingAuthorityCommunication, $stateParams) {
      return ProjectMassManagingAuthorityCommunication.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProjectMassManagingAuthorityCommunicationsViewCtrl };
