import _ from 'lodash';

function ProjectRegistrationsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  projectRegistrations
) {
  $scope.filters = {
    programmePriorityId: null,
    procedureId: null,
    fromDate: null,
    toDate: null
  };

  $scope.projectsExportUrl = $interpolate(
    'api/projects/excelExport?' +
      'programmePriorityId={{programmePriorityId}}&procedureId={{procedureId}}&' +
      'fromDate={{fromDate}}&toDate={{toDate}}'
  )({
    programmePriorityId: $stateParams.programmePriorityId,
    procedureId: $stateParams.procedureId,
    fromDate: $stateParams.fromDate,
    toDate: $stateParams.toDate
  });

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.projects.search', {
      programmePriorityId: $scope.filters.programmePriorityId,
      procedureId: $scope.filters.procedureId,
      fromDate: $scope.filters.fromDate,
      toDate: $scope.filters.toDate
    });
  };

  $scope.projectRegistrations = projectRegistrations;
}

ProjectRegistrationsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'projectRegistrations'
];

ProjectRegistrationsSearchCtrl.$resolve = {
  projectRegistrations: [
    'Project',
    '$stateParams',
    function(Project, $stateParams) {
      return Project.query($stateParams).$promise.then(function(projects) {
        return _.map(projects, function(project) {
          project.company =
            project.companyName + ' (' + project.companyUinType + ': ' + project.companyUin + ')';

          return project;
        });
      });
    }
  ]
};

export { ProjectRegistrationsSearchCtrl };
