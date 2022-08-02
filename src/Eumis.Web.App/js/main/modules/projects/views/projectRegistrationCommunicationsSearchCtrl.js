function ProjectRegistrationCommunicationsSearchCtrl(
  $scope,
  $stateParams,
  $state,
  scConfirm,
  projectRegistrationCommunications
) {
  $scope.projectRegistrationCommunications = projectRegistrationCommunications;

  $scope.newCommunication = function() {
    return scConfirm({
      resource: 'ProjectRegistrationCommunication',
      validationAction: 'canCreate',
      action: 'save',
      params: {
        id: $stateParams.id
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.projects.view.communications.edit', {
          ind: result.result.projectCommunicationId
        });
      }
    });
  };
}

ProjectRegistrationCommunicationsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$state',
  'scConfirm',
  'projectRegistrationCommunications'
];

ProjectRegistrationCommunicationsSearchCtrl.$resolve = {
  projectRegistrationCommunications: [
    '$stateParams',
    'ProjectRegistrationCommunication',
    function($stateParams, ProjectRegistrationCommunication) {
      return ProjectRegistrationCommunication.query($stateParams).$promise;
    }
  ]
};

export { ProjectRegistrationCommunicationsSearchCtrl };
