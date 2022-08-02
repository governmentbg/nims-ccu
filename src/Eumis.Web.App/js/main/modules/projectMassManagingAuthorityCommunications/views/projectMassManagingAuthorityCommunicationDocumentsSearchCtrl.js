import * as _ from 'lodash';

function ProjectMassManagingAuthorityCommunicationDocumentsSearchCtrl(
  $scope,
  $stateParams,
  $state,
  scConfirm,
  projectMassManagingAuthorityCommunicationDocuments,
  ProjectMassManagingAuthorityCommunicationFile
) {
  $scope.communicationId = $stateParams.id;
  $scope.massCommunicationVersion = $scope.massCommunicationInfo.version;
  $scope.status = $scope.massCommunicationInfo.status;

  $scope.documents = _.map(projectMassManagingAuthorityCommunicationDocuments, function(item) {
    if (item.file) {
      item.file.url = ProjectMassManagingAuthorityCommunicationFile.getUrl({
        id: item.projectMassManagingAuthorityCommunicationId,
        fileKey: item.file.key
      });
    }
    return item;
  });

  $scope.delItem = function(documentId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProjectMassManagingAuthorityCommunicationDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: documentId,
        version: $scope.massCommunicationVersion
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ProjectMassManagingAuthorityCommunicationDocumentsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$state',
  'scConfirm',
  'projectMassManagingAuthorityCommunicationDocuments',
  'ProjectMassManagingAuthorityCommunicationFile'
];

ProjectMassManagingAuthorityCommunicationDocumentsSearchCtrl.$resolve = {
  projectMassManagingAuthorityCommunicationDocuments: [
    '$stateParams',
    'ProjectMassManagingAuthorityCommunicationDocument',
    function($stateParams, ProjectMassManagingAuthorityCommunicationDocument) {
      return ProjectMassManagingAuthorityCommunicationDocument.query({ id: $stateParams.id })
        .$promise;
    }
  ]
};

export { ProjectMassManagingAuthorityCommunicationDocumentsSearchCtrl };
