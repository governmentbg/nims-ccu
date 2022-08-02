function PTemplatesNewCtrl($scope, $state, PermissionTemplate, newPermissionTemplate) {
  $scope.newPermissionTemplate = newPermissionTemplate;

  $scope.save = function() {
    return $scope.newTemplateForm.$validate().then(function() {
      if ($scope.newTemplateForm.$valid) {
        return PermissionTemplate.save($scope.newPermissionTemplate).$promise.then(function() {
          return $state.go('root.pTemplates.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.pTemplates.search');
  };

  $scope.isNameExist = function(name) {
    if (!name) {
      return true;
    } else {
      return PermissionTemplate.isNameExist({
        name: name
      }).$promise.then(function(result) {
        return !result;
      });
    }
  };
}

PTemplatesNewCtrl.$inject = ['$scope', '$state', 'PermissionTemplate', 'newPermissionTemplate'];

PTemplatesNewCtrl.$resolve = {
  newPermissionTemplate: [
    'PermissionTemplate',
    function(PermissionTemplate) {
      return PermissionTemplate.newTemplate().$promise;
    }
  ]
};

export { PTemplatesNewCtrl };
