import _ from 'lodash';
import angular from 'angular';

function EvalSessionProjectCommunicationAnswerEditCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  projectCommunicationAnswer
) {
  $scope.answer = projectCommunicationAnswer;
  $scope.projectId = $stateParams.ind;
  $scope.communicationId = $stateParams.mid;
  $scope.answerId = $stateParams.aid;
  $scope.currentSessionId = parseInt($stateParams.id, 10);
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';

  $scope.canRegisterAnswer =
    projectCommunicationAnswer.status === 'paperAnswer' &&
    $scope.isSessionActive &&
    $scope.currentSessionId === $scope.evalSessionInfo.evalSessionId;

  $scope.canPrintRegistration =
    $scope.isSessionActive &&
    _.includes(['answer', 'applied', 'rejected'], projectCommunicationAnswer.status);

  $scope.register = function() {
    var modalInstance = scModal.open('registerAnswerModal', {
      projectId: $stateParams.ind,
      communicationId: $stateParams.mid,
      answerId: $stateParams.aid,
      regNumber: $scope.answer.communicationRegNumber,
      version: $scope.answer.version
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.back = function() {
    return $state.go('root.evalSessions.view.projects.view.communications.edit');
  };
}

EvalSessionProjectCommunicationAnswerEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'projectCommunicationAnswer'
];

EvalSessionProjectCommunicationAnswerEditCtrl.$resolve = {
  projectCommunicationAnswer: [
    'ProjectCommunicationAnswer',
    'ProjectCommunicationFile',
    '$stateParams',
    function(ProjectCommunicationAnswer, ProjectCommunicationFile, $stateParams) {
      return ProjectCommunicationAnswer.get({
        ind: $stateParams.ind,
        mid: $stateParams.mid,
        aid: $stateParams.aid
      }).$promise.then(function(answer) {
        if (answer.projectCommunicationFile) {
          answer.projectCommunicationFile.url = ProjectCommunicationFile.getUrl({
            id: $stateParams.ind,
            ind: $stateParams.mid,
            projectCommunicationFileId: answer.projectCommunicationFile.id
          });

          _(answer.projectCommunicationFileSignatures).forEach(function(pcfs) {
            pcfs.url = ProjectCommunicationFile.getUrl({
              id: $stateParams.ind,
              ind: $stateParams.mid,
              projectCommunicationFileId: answer.projectCommunicationFile.id,
              projectCommunicationFileSignatureId: pcfs.id
            });
          });
        }

        return answer;
      });
    }
  ]
};

export { EvalSessionProjectCommunicationAnswerEditCtrl };
