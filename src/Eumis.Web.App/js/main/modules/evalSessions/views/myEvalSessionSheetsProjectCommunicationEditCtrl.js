import angular from 'angular';

function MyEvalSessionSheetsProjectCommunicationEditCtrl(
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
    return $state.go('root.evalSessions.my.view.sheets.edit.project', {
      id: $stateParams.id,
      ind: $stateParams.ind
    });
  };
}

MyEvalSessionSheetsProjectCommunicationEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'projectCommunication',
  'projectCommunicationAnswers'
];

MyEvalSessionSheetsProjectCommunicationEditCtrl.$resolve = {
  projectCommunication: [
    'ProjectCommunication',
    '$stateParams',
    function(ProjectCommunication, $stateParams) {
      return ProjectCommunication.getCommunicationForSheet({
        id: $stateParams.ind,
        mid: $stateParams.mid
      }).$promise;
    }
  ],
  projectCommunicationAnswers: [
    'ProjectCommunicationAnswer',
    '$stateParams',
    function(ProjectCommunicationAnswer, $stateParams) {
      return ProjectCommunicationAnswer.getCommunicationAnswersForSheet({
        id: $stateParams.ind,
        mid: $stateParams.mid
      }).$promise;
    }
  ]
};

export { MyEvalSessionSheetsProjectCommunicationEditCtrl };
