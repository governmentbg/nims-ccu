function ProjectManagingAuthorityCommunicationAnswersEditCtrl(
  $scope,
  $state,
  projectRegistrationCommunicationAnswer
) {
  $scope.answer = projectRegistrationCommunicationAnswer;

  $scope.back = function() {
    return $state.go('root.projectCommunications.edit');
  };
}

ProjectManagingAuthorityCommunicationAnswersEditCtrl.$inject = [
  '$scope',
  '$state',
  'projectRegistrationCommunicationAnswer'
];

ProjectManagingAuthorityCommunicationAnswersEditCtrl.$resolve = {
  projectRegistrationCommunicationAnswer: [
    'ProjectManagingAuthorityCommunicationAnswer',
    '$stateParams',
    function(ProjectManagingAuthorityCommunicationAnswer, $stateParams) {
      return ProjectManagingAuthorityCommunicationAnswer.get({
        id: $stateParams.id,
        ind: $stateParams.ind,
        aid: $stateParams.aid
      }).$promise;
    }
  ]
};

export { ProjectManagingAuthorityCommunicationAnswersEditCtrl };
