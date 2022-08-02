import angular from 'angular';
import _ from 'lodash';

function MyEvalSessionSheetsProjectCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  ProjectFile,
  ProjectCommunicationFile,
  projectRegistration,
  projectVersions,
  projectCommunications,
  standpoints,
  monitorstatRequests,
  ProjectMonitorstatRequestFile,
  ProjectMonitorstatResponseFile
) {
  var evalSessionId = parseInt($stateParams.id, 10);

  $scope.projectRegistration = projectRegistration;
  $scope.standpoints = standpoints;

  $scope.projectVersions = _.map(projectVersions, function(item) {
    if (item.projectFile) {
      item.projectFile.url = ProjectFile.getUrl({
        id: $scope.projectRegistration.projectId,
        projectFileId: item.projectFile.id
      });

      _(item.projectFileSignatures).forEach(function(pfs) {
        pfs.url = ProjectFile.getUrl({
          id: $scope.projectRegistration.projectId,
          projectFileId: item.projectFile.id,
          projectFileSignatureId: pfs.id
        });
      });
    }
    return item;
  });

  $scope.monitorstatRequests = _.map(monitorstatRequests, function(item) {
    item.fileUrl = ProjectMonitorstatRequestFile.getUrl({
      id: $scope.projectRegistration.projectId,
      fileKey: item.fileKey
    });
    item.responses = _.map(item.responses, function(item) {
      item.fileUrl = ProjectMonitorstatResponseFile.getUrl({
        id: $scope.projectRegistration.projectId,
        fileKey: item.fileKey
      });
      return item;
    });
    return item;
  });

  $scope.back = function() {
    return $state.go('root.evalSessions.my.view.sheets.edit', {
      id: $stateParams.id,
      ind: $stateParams.ind
    });
  };

  projectCommunications = _.map(projectCommunications, function(item) {
    if (item.projectCommunicationFile) {
      item.projectCommunicationFile.url = ProjectCommunicationFile.getUrl({
        id: $scope.projectRegistration.projectId,
        ind: item.communicationId,
        projectCommunicationFileId: item.projectCommunicationFile.id
      });

      _(item.projectCommunicationFileSignatures).forEach(function(pcfs) {
        pcfs.url = ProjectCommunicationFile.getUrl({
          id: $scope.projectRegistration.projectId,
          ind: item.communicationId,
          projectCommunicationFileId: item.projectCommunicationFile.id,
          projectCommunicationFileSignatureId: pcfs.id
        });
      });
    }
    return item;
  });

  $scope.currentSessionCommunications = _.filter(projectCommunications, function(m) {
    return m.sessionId === evalSessionId;
  });

  $scope.otherCommunications = _.filter(projectCommunications, function(m) {
    return m.sessionId !== evalSessionId;
  });

  $scope.viewProjectVersion = function(projectVersion) {
    var modalInstance = scModal.open('portalIntegrationModal', {
      doc: 'projectVersion',
      action: 'view',
      xmlGid: projectVersion.xmlGid
    });
    modalInstance.result.catch(angular.noop);
    return modalInstance.opened;
  };

  $scope.viewStandpoint = function(standpoint) {
    var modalInstance = scModal.open('portalIntegrationModal', {
      doc: 'evalSessionStandpoint',
      action: 'view',
      xmlGid: standpoint.xmlGid
    });
    modalInstance.result.catch(angular.noop);
    return modalInstance.opened;
  };
}

MyEvalSessionSheetsProjectCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'ProjectFile',
  'ProjectCommunicationFile',
  'projectRegistration',
  'projectVersions',
  'projectCommunications',
  'standpoints',
  'monitorstatRequests',
  'ProjectMonitorstatRequestFile',
  'ProjectMonitorstatResponseFile'
];

MyEvalSessionSheetsProjectCtrl.$resolve = {
  projectRegistration: [
    'Project',
    '$stateParams',
    function(Project, $stateParams) {
      return Project.getForSheet({ id: $stateParams.ind }).$promise;
    }
  ],
  projectVersions: [
    'ProjectVersion',
    '$stateParams',
    function(ProjectVersion, $stateParams) {
      return ProjectVersion.getVersionsForSheet({ id: $stateParams.ind }).$promise;
    }
  ],
  projectCommunications: [
    'ProjectCommunication',
    '$stateParams',
    function(ProjectCommunication, $stateParams) {
      return ProjectCommunication.getCommunicationsForSheet({ id: $stateParams.ind }).$promise;
    }
  ],
  standpoints: [
    'EvalSessionStandpoint',
    '$stateParams',
    function(EvalSessionStandpoint, $stateParams) {
      return EvalSessionStandpoint.getForSheet({ id: $stateParams.ind }).$promise;
    }
  ],
  monitorstatRequests: [
    'ProjectMonitorstatRequest',
    '$stateParams',
    function(ProjectMonitorstatRequest, $stateParams) {
      return ProjectMonitorstatRequest.getMonitorstatRequestsForSheet({
        id: $stateParams.ind
      }).$promise;
    }
  ]
};

export { MyEvalSessionSheetsProjectCtrl };
