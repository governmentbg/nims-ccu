import * as angular from 'angular';

function ProjectMassManagingAuthorityCommunicationRecipientsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  scConfirm,
  recipients
) {
  $scope.communicationId = $stateParams.id;
  $scope.recipients = recipients;
  $scope.massCommunicationVersion = $scope.massCommunicationInfo.version;
  $scope.status = $scope.massCommunicationInfo.status;

  $scope.chooseItems = function() {
    const modalInstance = scModal.open('chooseProjectMassMACommunicationRecipientsModal', {
      communicationId: $scope.communicationId,
      version: $scope.massCommunicationVersion
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.delItem = function(projectId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProjectMassManagingAuthorityCommunicationRecipient',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: projectId,
        version: $scope.massCommunicationVersion
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ProjectMassManagingAuthorityCommunicationRecipientsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'scConfirm',
  'recipients'
];

ProjectMassManagingAuthorityCommunicationRecipientsSearchCtrl.$resolve = {
  recipients: [
    '$stateParams',
    'ProjectMassManagingAuthorityCommunicationRecipient',
    function($stateParams, ProjectMassManagingAuthorityCommunicationRecipient) {
      return ProjectMassManagingAuthorityCommunicationRecipient.query($stateParams).$promise;
    }
  ]
};

export { ProjectMassManagingAuthorityCommunicationRecipientsSearchCtrl };
