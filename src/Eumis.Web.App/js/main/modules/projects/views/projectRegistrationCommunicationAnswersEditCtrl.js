function ProjectRegistrationCommunicationAnswersEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  projectRegistrationCommunicationAnswer
) {
  $scope.answer = projectRegistrationCommunicationAnswer;

  $scope.answerUpdated = function() {
    return $state.partialReload();
  };

  $scope.back = function() {
    return $state.go('root.projects.view.communications.edit');
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProjectRegistrationCommunicationAnswer',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        aid: $stateParams.aid,
        version: $scope.answer.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.projects.view.communications.edit', {
          id: $stateParams.id,
          ind: $stateParams.ind
        });
      }
    });
  };
}

ProjectRegistrationCommunicationAnswersEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'projectRegistrationCommunicationAnswer'
];

ProjectRegistrationCommunicationAnswersEditCtrl.$resolve = {
  projectRegistrationCommunicationAnswer: [
    'ProjectRegistrationCommunicationAnswer',
    '$stateParams',
    function(ProjectRegistrationCommunicationAnswer, $stateParams) {
      return ProjectRegistrationCommunicationAnswer.get({
        id: $stateParams.id,
        ind: $stateParams.ind,
        aid: $stateParams.aid
      }).$promise;
    }
  ]
};

export { ProjectRegistrationCommunicationAnswersEditCtrl };
