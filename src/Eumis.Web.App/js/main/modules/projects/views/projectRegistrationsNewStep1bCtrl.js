function ProjectRegistrationsNewStep1bCtrl($q, $scope, $state, Project) {
  $scope.model = {};

  $scope.next = function() {
    return $scope.projectRegistrationsNewStep1bForm.$validate().then(function() {
      if ($scope.projectRegistrationsNewStep1bForm.$valid) {
        return $state.go('root.projects.newStep2', {
          code: $scope.model.code
        });
      }
    });
  };

  $scope.isCodeExisting = function(code) {
    if (!code) {
      return $q.resolve();
    }
    return Project.isCodeExisting({ code: code }).$promise.then(function(isExisting) {
      return isExisting ? $q.resolve() : $q.reject();
    });
  };

  $scope.cancel = function() {
    return $state.go('root.projects.search');
  };
}

ProjectRegistrationsNewStep1bCtrl.$inject = ['$q', '$scope', '$state', 'Project'];

export { ProjectRegistrationsNewStep1bCtrl };
