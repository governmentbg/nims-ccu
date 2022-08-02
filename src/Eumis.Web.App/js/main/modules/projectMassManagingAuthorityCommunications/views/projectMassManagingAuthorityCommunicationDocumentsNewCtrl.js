function ProjectMassManagingAuthorityCommunicationDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProjectMassManagingAuthorityCommunicationDocument,
  projectMassManagingAuthorityCommunicationDocument
) {
  $scope.communicationId = $stateParams.id;
  $scope.document = projectMassManagingAuthorityCommunicationDocument;

  $scope.save = function() {
    return $scope.newProjectMassManagingAuthorityCommunicationDocumentForm
      .$validate()
      .then(function() {
        if ($scope.newProjectMassManagingAuthorityCommunicationDocumentForm.$valid) {
          return ProjectMassManagingAuthorityCommunicationDocument.save(
            { id: $stateParams.id },
            $scope.document
          ).$promise.then(function() {
            return $state.go(
              'root.projectMassManagingAuthorityCommunications.view.documents.search'
            );
          });
        }
      });
  };

  $scope.cancel = function() {
    return $state.go('root.projectMassManagingAuthorityCommunications.view.documents.search');
  };
}

ProjectMassManagingAuthorityCommunicationDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProjectMassManagingAuthorityCommunicationDocument',
  'projectMassManagingAuthorityCommunicationDocument'
];

ProjectMassManagingAuthorityCommunicationDocumentsNewCtrl.$resolve = {
  projectMassManagingAuthorityCommunicationDocument: [
    'ProjectMassManagingAuthorityCommunicationDocument',
    '$stateParams',
    function(ProjectMassManagingAuthorityCommunicationDocument, $stateParams) {
      return ProjectMassManagingAuthorityCommunicationDocument.newDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProjectMassManagingAuthorityCommunicationDocumentsNewCtrl };
