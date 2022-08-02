function ProjectRegistrationsViewCtrl($scope) {
  $scope.tabList = {
    projects_tabs_projectEdit: 'root.projects.view.edit',
    projects_tabs_communication: 'root.projects.view.communications'
  };
}

ProjectRegistrationsViewCtrl.$inject = ['$scope'];

export { ProjectRegistrationsViewCtrl };
