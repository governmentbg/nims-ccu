import angular from 'angular';

function MyEvalSessionStandpointsProjectCommunicationEditCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  projectCommunication,
  projectCommunicationAnswers
) {
  $scope.currentSessionId = parseInt($stateParams.id, 10);
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.projectCommunication = projectCommunication;
  $scope.communicationId = $stateParams.mid;

  $scope.projectCommunicationAnswers = projectCommunicationAnswers;

  $scope.viewProjectCommunicationAnswer = function(answer) {
    var modalInstance = scModal.open('portalIntegrationModal', {
      doc: 'projectCommunicationAnswer',
      action: 'view',
      parentGid: $scope.projectCommunication.xmlGid,
      childGid: answer.xmlGid
    });
    modalInstance.result.catch(angular.noop);
    return modalInstance.opened;
  };

  $scope.back = function() {
    return $state.go('root.evalSessions.my.view.standpoints.edit.project', {
      id: $stateParams.id,
      ind: $stateParams.ind
    });
  };
}

MyEvalSessionStandpointsProjectCommunicationEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'projectCommunication',
  'projectCommunicationAnswers'
];

MyEvalSessionStandpointsProjectCommunicationEditCtrl.$resolve = {
  projectCommunication: [
    'ProjectCommunication',
    '$stateParams',
    function(ProjectCommunication, $stateParams) {
      return ProjectCommunication.getCommunicationForStandpoint({
        id: $stateParams.ind,
        mid: $stateParams.mid
      }).$promise;
    }
  ],
  projectCommunicationAnswers: [
    'ProjectCommunicationAnswer',
    '$stateParams',
    function(ProjectCommunicationAnswer, $stateParams) {
      return ProjectCommunicationAnswer.getCommunicationAnswersForStandpoint({
        id: $stateParams.ind,
        mid: $stateParams.mid
      }).$promise;
    }
  ]
};

export { MyEvalSessionStandpointsProjectCommunicationEditCtrl };
