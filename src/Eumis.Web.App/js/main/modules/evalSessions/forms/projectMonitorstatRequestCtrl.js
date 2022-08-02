function ProjectMonitorstatRequestCtrl($scope, ProjectMonitorstatRequestFile) {
  $scope.editMode = null;

  $scope.$parent.$watch('editMode', function(value) {
    $scope.editMode = value;
  });

  $scope.getUrl = function(fileKey) {
    return ProjectMonitorstatRequestFile.getUrl({
      id: $scope.model.projectId,
      fileKey: fileKey
    });
  };
}

ProjectMonitorstatRequestCtrl.$inject = ['$scope', 'ProjectMonitorstatRequestFile'];

export { ProjectMonitorstatRequestCtrl };
