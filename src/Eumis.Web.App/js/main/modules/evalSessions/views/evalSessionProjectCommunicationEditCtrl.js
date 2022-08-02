import _ from 'lodash';

function EvalSessionProjectCommunicationEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  scModal,
  structuredDocument,
  ProjectCommunication,
  projectCommunication,
  projectCommunicationAnswers
) {
  $scope.currentSessionId = parseInt($stateParams.id, 10);
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.projectCommunication = projectCommunication;
  $scope.communicationId = $stateParams.mid;
  $scope.projectId = $stateParams.ind;
  $scope.canDeleteCommunication =
    projectCommunication.status === 'draftQuestion' &&
    $scope.isSessionActive &&
    $scope.currentSessionId === projectCommunication.evalSessionId;
  $scope.canCancelCommunication =
    $scope.isSessionActive &&
    _.includes(
      ['question', 'draftAnswer', 'answerFinalized', 'answer', 'paperAnswer'],
      projectCommunication.status
    ) &&
    $scope.currentSessionId === projectCommunication.evalSessionId;

  $scope.projectCommunicationAnswers = _.map(projectCommunicationAnswers, function(item) {
    item.viewXmlUrl = structuredDocument.getParentChildUrl(
      'projectCommunicationAnswer',
      'view',
      $scope.projectCommunication.xmlGid,
      item.xmlGid
    );
    return item;
  });
  $scope.canRegisterAnswer =
    projectCommunication.status === 'paperAnswer' &&
    $scope.isSessionActive &&
    $scope.currentSessionId === projectCommunication.evalSessionId;
  $scope.canPrintRegistration =
    $scope.isSessionActive &&
    _.includes(['answer', 'applied', 'rejected'], projectCommunication.status);
  $scope.canViewAnswer = _.includes(['answer', 'applied', 'rejected'], projectCommunication.status);
  $scope.canApplyAnswer =
    projectCommunication.status === 'answer' &&
    $scope.isSessionActive &&
    $scope.currentSessionId === projectCommunication.evalSessionId;
  $scope.canRejectAnswer =
    projectCommunication.status === 'answer' &&
    $scope.isSessionActive &&
    $scope.currentSessionId === projectCommunication.evalSessionId;

  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProjectCommunicationForm.$validate().then(function() {
      if ($scope.editProjectCommunicationForm.$valid) {
        return ProjectCommunication.update(
          { id: $stateParams.ind, mid: $stateParams.mid },
          $scope.projectCommunication
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.back = function() {
    return $state.go('root.evalSessions.view.projects.view', {
      id: $stateParams.id,
      ind: $stateParams.ind
    });
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProjectCommunication',
      action: 'remove',
      params: {
        id: $stateParams.ind,
        mid: $stateParams.mid,
        version: $scope.projectCommunication.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.evalSessions.view.projects.view', $stateParams, {
          reload: true
        });
      }
    });
  };

  $scope.cancelCommunication = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_editEvalSessionProjectCommunication_cancelConfirm',
      noteLabel: 'evalSessions_editEvalSessionProjectCommunication_cancelNote',
      resource: 'ProjectCommunication',
      action: 'cancelCommunication',
      params: {
        id: $stateParams.ind,
        mid: $stateParams.mid,
        version: projectCommunication.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.questionUpdated = function() {
    return $state.partialReload();
  };
}

EvalSessionProjectCommunicationEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'scModal',
  'structuredDocument',
  'ProjectCommunication',
  'projectCommunication',
  'projectCommunicationAnswers'
];

EvalSessionProjectCommunicationEditCtrl.$resolve = {
  projectCommunication: [
    'ProjectCommunication',
    '$stateParams',
    function(ProjectCommunication, $stateParams) {
      return ProjectCommunication.get({
        id: $stateParams.ind,
        mid: $stateParams.mid
      }).$promise;
    }
  ],
  projectCommunicationAnswers: [
    'ProjectCommunicationAnswer',
    '$stateParams',
    function(ProjectCommunicationAnswer, $stateParams) {
      return ProjectCommunicationAnswer.query({ ind: $stateParams.ind, mid: $stateParams.mid })
        .$promise;
    }
  ]
};

export { EvalSessionProjectCommunicationEditCtrl };
