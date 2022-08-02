function ProjectRegistrationsNewStep3Ctrl(
  $scope,
  $state,
  $stateParams,
  Project,
  projectRegistration
) {
  $scope.projectRegistration = projectRegistration;

  if ($stateParams.xmlId) {
    $scope.hasRegProjectXml = true;
  }

  $scope.save = function() {
    return $scope.projectRegistrationsNewStep3Form.$validate().then(function() {
      if ($scope.projectRegistrationsNewStep3Form.$valid) {
        return Project.save($scope.projectRegistration).$promise.then(function(projectResult) {
          return $state.go('root.projects.view.edit', { id: projectResult.projectId });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.projects.search');
  };
}

ProjectRegistrationsNewStep3Ctrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'Project',
  'projectRegistration'
];

ProjectRegistrationsNewStep3Ctrl.$resolve = {
  projectRegistration: [
    '$stateParams',
    'Project',
    '$state',
    function($stateParams, Project, $state) {
      if ($stateParams.pId && $stateParams.cId) {
        return Project.newProjectRegistration({
          procedureId: $stateParams.pId,
          companyId: $stateParams.cId,
          regProjectXmlId: $stateParams.xmlId
        }).$promise;
      } else {
        return $state.go('root.projects.search');
      }
    }
  ]
};

export { ProjectRegistrationsNewStep3Ctrl };
