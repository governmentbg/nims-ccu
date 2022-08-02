import * as _ from 'lodash';

function ChooseProjectsForAutomaticMonitorstatRequestModalCtrl(
  $scope,
  $state,
  $uibModalInstance,
  scConfirm,
  scModalParams,
  EvalSessionAutomaticProjectMonitorstatRequest,
  projects,
  evalSessionInfo
) {
  $scope.chosenProjectIds = [];
  $scope.errors = [];
  $scope.hasChoosenAll = true;
  $scope.projects = projects;
  $scope.tableControl = {};
  $scope.evalSessionId = scModalParams.evalSessionId;
  $scope.procedureId = evalSessionInfo.procedureId;

  $scope.loadProjectsErrors = [];
  $scope.uploadMode = null;
  $scope.file = null;

  $scope.chooseProject = function(project) {
    project.isChosen = true;
    $scope.chosenProjectIds.push(project.projectId);
  };

  $scope.removeProject = function(project) {
    project.isChosen = false;
    $scope.chosenProjectIds = _.without($scope.chosenProjectIds, project.projectId);
  };

  $scope.ok = function() {
    $scope.chooseProjectsForAutomaticProjectMonitorstatRequestsForm.$validate().then(function() {
      if ($scope.chooseProjectsForAutomaticProjectMonitorstatRequestsForm.$valid) {
        $scope.isReadonly = true;

        return scConfirm({
          confirmMessage: 'evalSessions_chooseProjectsForMonitorstatRequestModal_confirm',
          resource: 'EvalSessionAutomaticProjectMonitorstatRequest',
          action: 'sendAutomaticRequests',
          params: {
            id: scModalParams.evalSessionId
          },
          data: {
            projectIds: $scope.chosenProjectIds,
            procedureMonitorstatRequestId: $scope.procedureMonitorstatRequestId,
            procedureApplicationDocId: $scope.procedureApplicationDocId,
            programmeDeclarationId: $scope.programmeDeclarationId
          }
        }).then(function(result) {
          $scope.isReadonly = false;
          if (result.executed) {
            $state.partialReload();
            $scope.errors = result.result.errors;

            if (result.result.errors.length === 0) {
              return $uibModalInstance.dismiss('cancel');
            }
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };

  $scope.chooseAll = function() {
    const filteredProjects = $scope.tableControl.getFilteredItems();
    _.forEach(filteredProjects, function(proj) {
      if (_.includes($scope.chosenProjectIds, proj.projectId)) {
        proj.isChosen = true;
      } else {
        $scope.chooseProject(proj);
      }
    });
    $scope.hasChoosenAll = false;
  };

  $scope.removeAll = function() {
    _.forEach($scope.projects, function(proj) {
      $scope.removeProject(proj);
    });
    $scope.hasChoosenAll = true;
  };

  $scope.uploadFile = function() {
    $scope.uploadMode = 'upload';
  };

  $scope.cancelFileUpload = function() {
    $scope.uploadMode = null;
    $scope.file = null;
    $scope.loadProjectsErrors = [];
  };

  $scope.loadProjects = function() {
    return EvalSessionAutomaticProjectMonitorstatRequest.loadProjectsFromFile({
      id: scModalParams.evalSessionId,
      fileKey: $scope.file.key
    }).$promise.then(function(result) {
      $scope.loadProjectsErrors = result.errors;

      if ($scope.loadProjectsErrors.length === 0) {
        if (result.projectIds.length > 0) {
          _.forEach($scope.projects, function(proj) {
            $scope.removeProject(proj);

            if (_.includes(result.projectIds, proj.projectId)) {
              $scope.chooseProject(proj);
            }
          });
        }

        $scope.uploadMode = null;
        $scope.file = null;
      }
    });
  };
}

ChooseProjectsForAutomaticMonitorstatRequestModalCtrl.$inject = [
  '$scope',
  '$state',
  '$uibModalInstance',
  'scConfirm',
  'scModalParams',
  'EvalSessionAutomaticProjectMonitorstatRequest',
  'projects',
  'evalSessionInfo'
];

ChooseProjectsForAutomaticMonitorstatRequestModalCtrl.$resolve = {
  projects: [
    'EvalSessionAutomaticProjectMonitorstatRequest',
    'scModalParams',
    function(EvalSessionAutomaticProjectMonitorstatRequest, scModalParams) {
      return EvalSessionAutomaticProjectMonitorstatRequest.getProjects({
        id: scModalParams.evalSessionId
      }).$promise.then(function(projects) {
        return _.map(projects, function(project) {
          project.company =
            project.companyName + ' (' + project.companyUinType + ': ' + project.companyUin + ')';

          return project;
        });
      });
    }
  ],
  evalSessionInfo: [
    'EvalSession',
    '$stateParams',
    function(EvalSession, $stateParams) {
      return EvalSession.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ChooseProjectsForAutomaticMonitorstatRequestModalCtrl };
