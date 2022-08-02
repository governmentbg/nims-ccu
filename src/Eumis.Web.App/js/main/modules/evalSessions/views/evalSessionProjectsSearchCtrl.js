import angular from 'angular';

function EvalSessionProjectsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scModal,
  scConfirm,
  EvalSessionProject,
  evalSessionProjects
) {
  $scope.evalSessionId = $scope.evalSessionInfo.evalSessionId;
  $scope.evalSessionVersion = $scope.evalSessionInfo.version;
  $scope.evalSessionProjects = evalSessionProjects;
  $scope.isSessionDraft = $scope.evalSessionInfo.evalSessionStatusName === 'draft';
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';

  $scope.yesText = l10n.get('common_texts_yes');
  $scope.noText = l10n.get('common_texts_no');

  $scope.chooseProjects = function() {
    var modalInstance = scModal.open('chooseProjectsModal', {
      evalSessionId: $scope.evalSessionId,
      version: $scope.evalSessionVersion
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.evaluateProject = function(evaluationType) {
    var modalInstance = scModal.open('chooseAndEvaluateProjectModal', {
      evalSessionId: $stateParams.id,
      evaluationType: evaluationType,
      version: $scope.evalSessionVersion
    });

    modalInstance.result.then(function(result) {
      if (result.evaluation) {
        return $state.go('root.evalSessions.view.projects.evaluations.new', {
          esId: result.evaluation.evalSessionId,
          pId: result.evaluation.projectId,
          t: result.evaluation.evaluationType
        });
      } else {
        $state.partialReload();
      }
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.deleteProject = function(project) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'EvalSessionProject',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: project.evalSessionId,
        ind: project.projectId,
        version: $scope.evalSessionVersion
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.openModal = function(modalName) {
    const modalInstance = scModal.open(modalName, {
      evalSessionId: $scope.evalSessionId,
      version: $scope.evalSessionVersion
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };
}

EvalSessionProjectsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scModal',
  'scConfirm',
  'EvalSessionProject',
  'evalSessionProjects'
];

EvalSessionProjectsSearchCtrl.$resolve = {
  evalSessionProjects: [
    'EvalSessionProject',
    '$stateParams',
    function(EvalSessionProject, $stateParams) {
      return EvalSessionProject.query($stateParams).$promise;
    }
  ]
};

export { EvalSessionProjectsSearchCtrl };
