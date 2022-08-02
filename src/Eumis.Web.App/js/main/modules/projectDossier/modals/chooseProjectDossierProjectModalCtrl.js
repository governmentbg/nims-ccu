import _ from 'lodash';

function ChooseProjectDossierProjectModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ProjectDossier,
  projects
) {
  $scope.projects = projects;
  $scope.filters = {
    procedureId: scModalParams.procedureId,
    projectNumber: scModalParams.projectNumber
  };

  $scope.search = function() {
    return ProjectDossier.getProjects($scope.filters).$promise.then(function(result) {
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

ChooseProjectDossierProjectModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ProjectDossier',
  'projects'
];

ChooseProjectDossierProjectModalCtrl.$resolve = {
  projects: [
    'ProjectDossier',
    'scModalParams',
    function(ProjectDossier, scModalParams) {
      return ProjectDossier.getProjects(scModalParams).$promise.then(function(projects) {
        return _.map(projects, function(project) {
          project.company =
            project.companyName + ' (' + project.companyUinType + ': ' + project.companyUin + ')';

          return project;
        });
      });
    }
  ]
};

export { ChooseProjectDossierProjectModalCtrl };
