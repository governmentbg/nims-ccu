import angular from 'angular';
import _ from 'lodash';
function EvalSessionProjectsViewCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scConfirm,
  scModal,
  ProjectFile,
  ProjectCommunicationFile,
  ProjectVersion,
  ProjectCommunication,
  projectRegistration,
  projectVersions,
  projectCommunications,
  projectEvaulations,
  evalSessionProject,
  EvalSessionProject,
  EvalSessionProjectStanding,
  projectStandings,
  ProjectMonitorstatRequest,
  monitorstatRequests,
  ProjectMonitorstatRequestFile,
  ProjectMonitorstatResponseFile
) {
  $scope.evalSessionId = parseInt($stateParams.id, 10);
  $scope.projectId = $stateParams.ind;

  $scope.sessionIsActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.procedureHasMonitorstatInquiries = $scope.evalSessionInfo.procedureHasMonitorstatInquiries;
  $scope.projectRegistration = projectRegistration;
  $scope.projectEvaulations = projectEvaulations;
  $scope.evalSessionProject = evalSessionProject;
  $scope.projectStandings = projectStandings;

  $scope.monitorstatRequests = _.map(monitorstatRequests, function(item) {
    item.fileUrl = ProjectMonitorstatRequestFile.getUrl({
      id: $scope.projectId,
      fileKey: item.fileKey
    });
    item.responses = _.map(item.responses, function(item) {
      item.fileUrl = ProjectMonitorstatResponseFile.getUrl({
        id: $scope.projectId,
        fileKey: item.fileKey
      });
      return item;
    });
    return item;
  });

  $scope.projectVersions = _.map(projectVersions, function(item) {
    if (item.projectFile) {
      item.projectFile.url = ProjectFile.getUrl({
        id: $stateParams.ind,
        projectFileId: item.projectFile.id
      });

      _(item.projectFileSignatures).forEach(function(pfs) {
        pfs.url = ProjectFile.getUrl({
          id: $stateParams.ind,
          projectFileId: item.projectFile.id,
          projectFileSignatureId: pfs.id
        });
      });
    }
    return item;
  });

  $scope.currentSessionCommunications = _.filter(projectCommunications, function(m) {
    return m.sessionId === $scope.evalSessionId;
  });

  $scope.otherCommunications = _.filter(projectCommunications, function(m) {
    return m.sessionId !== $scope.evalSessionId;
  });

  $scope.projectVersionFromRegData = function() {
    return ProjectVersion.createFromRegData(
      {
        evalSessionId: $stateParams.id,
        id: $stateParams.ind
      },
      {}
    ).$promise.then(function() {
      return $state.partialReload();
    });
  };

  $scope.newProjectVersion = function() {
    return scConfirm({
      resource: 'ProjectVersion',
      validationAction: 'canCreate',
      params: {
        evalSessionId: $stateParams.id,
        id: $stateParams.ind
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.evalSessions.view.projects.view.versions.new');
      }
    });
  };

  $scope.newCommunication = function() {
    return scConfirm({
      resource: 'ProjectCommunication',
      validationAction: 'canCreate',
      action: 'save',
      params: {
        evalSessionId: $stateParams.id,
        id: $stateParams.ind
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.evalSessions.view.projects.view.communications.edit', {
          mid: result.result.communicationId
        });
      }
    });
  };

  $scope.cancelProject = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_viewEvalSessionProject_confirmCancel',
      noteLabel: 'evalSessions_viewEvalSessionProject_cancelMessage',
      resource: 'EvalSessionProject',
      validationAction: 'canCancel',
      action: 'cancel',
      params: {
        id: $scope.evalSessionProject.evalSessionId,
        ind: $scope.evalSessionProject.projectId,
        version: $scope.evalSessionInfo.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.restoreProject = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_viewEvalSessionProject_confirmRestore',
      resource: 'EvalSessionProject',
      validationAction: 'canRestore',
      action: 'restore',
      params: {
        id: $scope.evalSessionProject.evalSessionId,
        ind: $scope.evalSessionProject.projectId,
        version: $scope.evalSessionInfo.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.createStanding = function(isPreliminary) {
    return scConfirm({
      resource: 'EvalSessionProjectStanding',
      validationAction: 'canCreate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        isPreliminary: isPreliminary
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.evalSessions.view.projects.view.standings.new', {
          p: isPreliminary
        });
      }
    });
  };

  $scope.createEvaluation = function(evaluationType) {
    return scConfirm({
      resource: 'EvalSessionEvaluation',
      validationAction: 'canCreate',
      params: {
        id: $stateParams.id,
        projectId: $stateParams.ind,
        evalTableType: evaluationType
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.evalSessions.view.projects.evaluations.new', {
          esId: $stateParams.id,
          pId: $stateParams.ind,
          t: evaluationType
        });
      }
    });
  };

  $scope.removeMonitorstatRequestItem = function(item) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProjectMonitorstatRequest',
      action: 'remove',
      params: {
        id: $scope.projectId,
        version: item.version,
        ind: item.projectMonitorstatRequestId
      }
    }).then(function() {
      return $state.reload();
    });
  };

  $scope.createAutomaticMonitorstatRequests = function() {
    var modalInstance = scModal.open('chooseMonitorstatRequestCompaniesModal', {
      projectId: $scope.projectId
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };
}

EvalSessionProjectsViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scConfirm',
  'scModal',
  'ProjectFile',
  'ProjectCommunicationFile',
  'ProjectVersion',
  'ProjectCommunication',
  'projectRegistration',
  'projectVersions',
  'projectCommunications',
  'projectEvaulations',
  'evalSessionProject',
  'EvalSessionProject',
  'EvalSessionProjectStanding',
  'projectStandings',
  'ProjectMonitorstatRequest',
  'monitorstatRequests',
  'ProjectMonitorstatRequestFile',
  'ProjectMonitorstatResponseFile'
];

EvalSessionProjectsViewCtrl.$resolve = {
  projectRegistration: [
    'Project',
    '$stateParams',
    function(Project, $stateParams) {
      return Project.get({ id: $stateParams.ind }).$promise;
    }
  ],
  projectVersions: [
    'ProjectVersion',
    '$stateParams',
    function(ProjectVersion, $stateParams) {
      return ProjectVersion.query({ id: $stateParams.ind }).$promise;
    }
  ],
  projectCommunications: [
    'ProjectCommunication',
    '$stateParams',
    function(ProjectCommunication, $stateParams) {
      return ProjectCommunication.query({ id: $stateParams.ind }).$promise;
    }
  ],
  projectEvaulations: [
    'EvalSessionEvaluation',
    '$stateParams',
    function(EvalSessionEvaluation, $stateParams) {
      return EvalSessionEvaluation.query({
        id: $stateParams.id,
        projectId: $stateParams.ind
      }).$promise;
    }
  ],
  evalSessionProject: [
    'EvalSessionProject',
    '$stateParams',
    function(EvalSessionProject, $stateParams) {
      return EvalSessionProject.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ],
  projectStandings: [
    'EvalSessionProjectStanding',
    '$stateParams',
    function(EvalSessionProjectStanding, $stateParams) {
      return EvalSessionProjectStanding.query({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ],
  monitorstatRequests: [
    'ProjectMonitorstatRequest',
    '$stateParams',
    function(ProjectMonitorstatRequest, $stateParams) {
      return ProjectMonitorstatRequest.query({
        id: $stateParams.ind
      }).$promise;
    }
  ]
};

export { EvalSessionProjectsViewCtrl };
