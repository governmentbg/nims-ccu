function ProjectMassManagingAuthorityCommunicationDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProjectMassManagingAuthorityCommunicationDocument,
  projectMassManagingAuthorityCommunicationDocument
) {
  $scope.communicationId = $stateParams.id;
  $scope.document = projectMassManagingAuthorityCommunicationDocument;
  $scope.editMode = undefined;
  $scope.status = $scope.massCommunicationInfo.status;

  $scope.edit = function() {
    $scope.editMode = !$scope.editMode;
  };

  $scope.save = function() {
    return $scope.editProjectMassManagingAuthorityCommunicationDocumentForm
      .$validate()
      .then(function() {
        if ($scope.editProjectMassManagingAuthorityCommunicationDocumentForm.$valid) {
          return ProjectMassManagingAuthorityCommunicationDocument.update(
            { id: $stateParams.id, ind: $stateParams.ind },
            $scope.document
          ).$promise.then(function() {
            return $state.partialReload();
          });
        }
      });
  };

  $scope.deleteDocument = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProjectMassManagingAuthorityCommunicationDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.document.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.projectMassManagingAuthorityCommunications.view.documents.search');
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.sendRequest = function() {
    return scConfirm({
      confirmMessage:
        'projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsEdit_sendMessage',
      resource: 'ProjectMassManagingAuthorityCommunicationDocument',
      validationAction: 'canSendMonitorstatRequest',
      action: 'sendMonitorstatRequest',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.request.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ProjectMassManagingAuthorityCommunicationDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProjectMassManagingAuthorityCommunicationDocument',
  'projectMassManagingAuthorityCommunicationDocument'
];

ProjectMassManagingAuthorityCommunicationDocumentsEditCtrl.$resolve = {
  projectMassManagingAuthorityCommunicationDocument: [
    'ProjectMassManagingAuthorityCommunicationDocument',
    '$stateParams',
    function(ProjectMassManagingAuthorityCommunicationDocument, $stateParams) {
      return ProjectMassManagingAuthorityCommunicationDocument.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProjectMassManagingAuthorityCommunicationDocumentsEditCtrl };
