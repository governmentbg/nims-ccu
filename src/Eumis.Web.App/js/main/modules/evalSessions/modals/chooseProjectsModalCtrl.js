import _ from 'lodash';

function ChooseProjectsModalCtrl(
  $scope,
  $uibModalInstance,
  scMessage,
  $q,
  scModalParams,
  EvalSessionProject,
  EvalSession,
  projects
) {
  $scope.chosenProjectIds = [];
  $scope.hasChoosenAll = true;
  $scope.projects = projects;
  $scope.tableControl = {};

  $scope.filters = {
    fromDate: null,
    toDate: null,
    companySizeTypeId: null,
    companyKidCodeId: null,
    projectKidCodeId: null
  };

  $scope.search = function() {
    return EvalSession.getProjects({
      id: scModalParams.evalSessionId,
      fromDate: $scope.filters.fromDate,
      toDate: $scope.filters.toDate,
      companySizeTypeId: $scope.filters.companySizeTypeId,
      companyKidCodeId: $scope.filters.companyKidCodeId,
      projectKidCodeId: $scope.filters.projectKidCodeId
    }).$promise.then(function(filteredProjects) {
      $scope.chosenProjectIds = _.intersection(
        $scope.chosenProjectIds,
        _.map(filteredProjects, 'projectId')
      );

      _.map(filteredProjects, function(project) {
        if (_.includes($scope.chosenProjectIds, project.projectId)) {
          project.isChosen = true;
        }

        return project;
      });

      $scope.projects = _.map(filteredProjects, function(project) {
        project.company =
          project.companyName + ' (' + project.companyUinType + ': ' + project.companyUin + ')';

        return project;
      });
    });
  };

  $scope.chooseProject = function(project) {
    project.isChosen = true;
    $scope.chosenProjectIds.push(project.projectId);
  };

  $scope.removeProject = function(project) {
    project.isChosen = false;
    $scope.chosenProjectIds = _.without($scope.chosenProjectIds, project.projectId);
  };

  $scope.ok = function() {
    return EvalSessionProject.save(
      {
        id: scModalParams.evalSessionId,
        version: scModalParams.version
      },
      $scope.chosenProjectIds
    ).$promise.then(function(res) {
      if (res && res.error === 'projectAlreadyIncludedInEvalSession') {
        var deferred = $q.defer(),
          promise;
        promise = deferred.promise;

        promise.then(function() {
          return scMessage('evalSessions_chooseProjectsModal_cannotAddProject');
        });

        deferred.resolve($uibModalInstance.close());
      } else {
        return $uibModalInstance.close();
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };

  $scope.chooseAll = function() {
    var filteredProjects = $scope.tableControl.getFilteredItems();
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
}

ChooseProjectsModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scMessage',
  '$q',
  'scModalParams',
  'EvalSessionProject',
  'EvalSession',
  'projects'
];

ChooseProjectsModalCtrl.$resolve = {
  projects: [
    'EvalSession',
    'scModalParams',
    function(EvalSession, scModalParams) {
      return EvalSession.getProjects({
        id: scModalParams.evalSessionId
      }).$promise.then(function(projects) {
        return _.map(projects, function(project) {
          project.company =
            project.companyName + ' (' + project.companyUinType + ': ' + project.companyUin + ')';

          return project;
        });
      });
    }
  ]
};

export { ChooseProjectsModalCtrl };
