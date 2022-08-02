import _ from 'lodash';

function ChooseProjectModalCtrl($scope, $uibModalInstance, scModalParams, Contract, projects) {
  $scope.projects = projects;
  $scope.filters = {
    procedureId: scModalParams.procedureId,
    projectNumber: scModalParams.projectNumber
  };

  $scope.search = function() {
    return Contract.getProjects($scope.filters).$promise.then(function(result) {
      $scope.projects = _.map(result, function(project) {
        project.company =
          project.companyName + ' (' + project.companyUinType + ': ' + project.companyUin + ')';

        return project;
      });
    });
  };

  $scope.choose = function(project) {
    return $uibModalInstance.close(project);
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

ChooseProjectModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'Contract',
  'projects'
];

ChooseProjectModalCtrl.$resolve = {
  projects: [
    'Contract',
    'scModalParams',
    function(Contract, scModalParams) {
      return Contract.getProjects(scModalParams).$promise.then(function(projects) {
        return _.map(projects, function(project) {
          project.company =
            project.companyName + ' (' + project.companyUinType + ': ' + project.companyUin + ')';

          return project;
        });
      });
    }
  ]
};

export { ChooseProjectModalCtrl };
