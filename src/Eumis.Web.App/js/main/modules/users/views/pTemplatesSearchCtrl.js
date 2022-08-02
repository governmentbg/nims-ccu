function PTemplatesSearchCtrl($scope, $state, PermissionTemplate, permissionTemplates) {
  $scope.permissionTemplates = permissionTemplates;
}

PTemplatesSearchCtrl.$inject = ['$scope', '$state', 'PermissionTemplate', 'permissionTemplates'];

PTemplatesSearchCtrl.$resolve = {
  permissionTemplates: [
    'PermissionTemplate',
    function(PermissionTemplate) {
      return PermissionTemplate.query({}).$promise;
    }
  ]
};

export { PTemplatesSearchCtrl };
