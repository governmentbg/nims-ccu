function ProjectMassManagingAuthorityCommunicationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProjectMassManagingAuthorityCommunication,
  projectMassManagingAuthorityCommunication
) {
  $scope.communicationId = $stateParams.id;
  $scope.projectMassManagingAuthorityCommunication = projectMassManagingAuthorityCommunication;
  $scope.isDraftCommunication = projectMassManagingAuthorityCommunication.status === 'draft';
  $scope.editMode = undefined;

  $scope.edit = function() {
    $scope.editMode = !$scope.editMode;
  };

  $scope.save = function() {
    return $scope.editProjectMassMACommunicationForm.$validate().then(function() {
      if ($scope.editProjectMassMACommunicationForm.$valid) {
        return ProjectMassManagingAuthorityCommunication.update(
          { id: $stateParams.id },
          $scope.projectMassManagingAuthorityCommunication
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.send = function() {
    return scConfirm({
      confirmMessage:
        'projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsEdit_sendMessage',
      resource: 'ProjectMassManagingAuthorityCommunication',
      validationAction: 'canSend',
      action: 'send',
      params: {
        id: $stateParams.id,
        version: $scope.projectMassManagingAuthorityCommunication.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.projectMassManagingAuthorityCommunications.search');
      }
    });
  };

  $scope.delete = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProjectMassManagingAuthorityCommunication',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.projectMassManagingAuthorityCommunication.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.projectMassManagingAuthorityCommunications.search');
      }
    });
  };
}

ProjectMassManagingAuthorityCommunicationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProjectMassManagingAuthorityCommunication',
  'projectMassManagingAuthorityCommunication'
];

ProjectMassManagingAuthorityCommunicationsEditCtrl.$resolve = {
  projectMassManagingAuthorityCommunication: [
    'ProjectMassManagingAuthorityCommunication',
    '$stateParams',
    function(ProjectMassManagingAuthorityCommunication, $stateParams) {
      return ProjectMassManagingAuthorityCommunication.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProjectMassManagingAuthorityCommunicationsEditCtrl };
