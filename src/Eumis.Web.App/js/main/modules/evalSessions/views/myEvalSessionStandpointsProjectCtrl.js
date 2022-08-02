import angular from 'angular';
import _ from 'lodash';

function MyEvalSessionStandpointsProjectCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  projectRegistration,
  projectVersions,
  projectCommunications,
  standpoints
) {
  var evalSessionId = parseInt($stateParams.id, 10);

  $scope.projectRegistration = projectRegistration;
  $scope.projectVersions = projectVersions;
  $scope.standpoints = standpoints;

  $scope.currentSessionCommunications = _.filter(projectCommunications, function(m) {
    return m.sessionId === evalSessionId;
  });

  $scope.otherCommunications = _.filter(projectCommunications, function(m) {
    return m.sessionId !== evalSessionId;
  });

  $scope.back = function() {
    return $state.go('root.evalSessions.my.view.standpoints.edit', {
      id: $stateParams.id,
      ind: $stateParams.ind
    });
  };

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

MyEvalSessionStandpointsProjectCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'projectRegistration',
  'projectVersions',
  'projectCommunications',
  'standpoints'
];

MyEvalSessionStandpointsProjectCtrl.$resolve = {
  projectRegistration: [
    'Project',
    '$stateParams',
    function(Project, $stateParams) {
      return Project.getForStandpoint({
        id: $stateParams.ind
      }).$promise;
    }
  ],
  projectVersions: [
    'ProjectVersion',
    'ProjectFile',
    '$stateParams',
    function(ProjectVersion, ProjectFile, $stateParams) {
      return ProjectVersion.getVersionsForStandpoint({
        id: $stateParams.ind
      }).$promise.then(function(versions) {
        return _.map(versions, function(item) {
          _(item.projectFileSignatures).forEach(function(pfs) {
            pfs.url = ProjectFile.getUrl({
              id: item.projectId,
              projectFileId: item.projectFile.id,
              projectFileSignatureId: pfs.id
            });
          });

          return item;
        });
      });
    }
  ],
  projectCommunications: [
    'ProjectCommunication',
    'ProjectCommunicationFile',
    '$stateParams',
    function(ProjectCommunication, ProjectCommunicationFile, $stateParams) {
      return ProjectCommunication.getCommunicationsForStandpoint({
        id: $stateParams.ind
      }).$promise.then(function(communications) {
        return _.map(communications, function(item) {
          if (item.projectCommunicationFile) {
            item.projectCommunicationFile.url = ProjectCommunicationFile.getUrl({
              id: item.projectId,
              ind: item.communicationId,
              projectCommunicationFileId: item.projectCommunicationFile.id
            });

            _(item.projectCommunicationFileSignatures).forEach(function(pcfs) {
              pcfs.url = ProjectCommunicationFile.getUrl({
                id: item.projectId,
                ind: item.communicationId,
                projectCommunicationFileId: item.projectCommunicationFile.id,
                projectCommunicationFileSignatureId: pcfs.id
              });
            });
          }
          return item;
        });
      });
    }
  ],
  standpoints: [
    'EvalSessionStandpoint',
    '$stateParams',
    function(EvalSessionStandpoint, $stateParams) {
      return EvalSessionStandpoint.getForStandpointProject({
        id: $stateParams.ind
      }).$promise;
    }
  ]
};

export { MyEvalSessionStandpointsProjectCtrl };
